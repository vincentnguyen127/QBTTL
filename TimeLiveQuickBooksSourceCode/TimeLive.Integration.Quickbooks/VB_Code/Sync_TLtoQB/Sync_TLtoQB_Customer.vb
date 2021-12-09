Imports QBFC13Lib

Public Class Sync_TLtoQB_Customer


    '---------------------Sync Customer TL Data to QB---------------------------------------
    ''' <summary>
    ''' Sync the customer data from QB. Print out customers that are in TL but not QB
    ''' </summary>
    Function SyncCustomerData(ByVal p_token As String, Optional ByVal MainForm As MAIN = Nothing,
                              Optional ByVal UI As Boolean = True, Optional ByVal nameList As List(Of String) = Nothing)
        Dim numSynced As Integer = 0
        My.Forms.MAIN.History("Syncing Clients Data", "n")

        Try
            ' connect to Timelive
            Dim objClientServices As Services.TimeLive.Clients.Clients = MAIN.connect_TL_clients(p_token)
            Dim objClientArray() As Object = objClientServices.GetClients()
            Dim objClient As New Services.TimeLive.Clients.Client

            If Not MainForm Is Nothing Then
                MainForm.ProgressBar1.Maximum = objClientArray.Length
                MainForm.ProgressBar1.Value = 0
            End If

            For n As Integer = 0 To objClientArray.Length - 1
                objClient = objClientArray(n)
                Dim clientID As Integer = objClientServices.GetClientIdByName(objClient.ClientName)
                ' Only run for a name if it was selected, or we are syncing all of the names
                Dim create As Boolean = If(nameList Is Nothing, True, nameList.Contains(objClient.ClientName))
                If create Then
                    numSynced += If(checkQBCustomerExist(objClient.ClientName.ToString, clientID, objClient, UI), 0, 1)
                End If
                If Not MainForm Is Nothing Then MainForm.ProgressBar1.Value += 1
            Next

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

        Return numSynced
    End Function

    ''' <summary>
    ''' Verifies a client exists in QB, adding it to the Data Table if necessary
    ''' </summary>
    ''' <param name="TLClientName"></param>
    ''' <param name="TL_ID"></param>
    ''' <returns>
    ''' False: does not exist in QB
    ''' True: does exist in QB, and we add it to the Data Table if not present
    ''' </returns>
    Public Function checkQBCustomerExist(ByRef TLClientName As String, ByVal TL_ID As Integer, ByVal objClient As Services.TimeLive.Clients.Client, ByVal UI As Boolean) As Boolean
        Dim custRet As ICustomerRet

        Try
            Dim msgSetRq As IMsgSetRequest = MAIN.SESSMANAGER.CreateMsgSetRequest("US", 2, 0)

            msgSetRq.Attributes.OnError = ENRqOnError.roeContinue
            Dim CustomerQueryRq As ICustomerQuery = msgSetRq.AppendCustomerQueryRq

            CustomerQueryRq.ORCustomerListQuery.FullNameList.Add(TLClientName)
            Dim msgSetRs As IMsgSetResponse = MAIN.SESSMANAGER.DoRequests(msgSetRq)

            Dim response As IResponse = msgSetRs.ResponseList.GetAt(0)
            Dim custRetList As ICustomerRetList
            custRetList = response.Detail

            Dim inQB As Boolean = Not custRetList Is Nothing

            If Not inQB Then
                Dim newMsgSetRq As IMsgSetRequest = MAIN.SESSMANAGER.CreateMsgSetRequest("US", 2, 0)

                newMsgSetRq.Attributes.OnError = ENRqOnError.roeContinue
                Dim create As Boolean = True

                If UI Then
                    create = MsgBox("New customer found in TimeLive: " + TLClientName + ". Create in QuickBooks?", MsgBoxStyle.YesNo, "Warning!") = MsgBoxResult.Yes
                End If

                If create Then
                    ' modify Customer before adding TL Customer to QB
                    Dim tlName, tlEmail, tlFax, tlPhone As String

                    Using newForm As ModifyForm = New ModifyForm()
                        newForm.TxtName.Text = TLClientName 'objClient.ClientName
                        newForm.txtFax.Text = objClient.Fax
                        newForm.txtEmail.Text = objClient.EmailAddress
                        newForm.txtTelephone2.Text = objClient.Telephone1
                        If DialogResult.OK = newForm.ShowDialog() Then
                            tlName = newForm.TxtName.Text
                            tlEmail = newForm.txtEmail.Text
                            tlFax = newForm.txtFax.Text
                            tlPhone = newForm.txtTelephone2.Text
                        Else
                            Exit Function
                        End If

                    End Using
                    objClient.ClientName = If(Not String.IsNullOrEmpty(tlName), tlName, objClient.ClientName)
                    objClient.Fax = If(Not String.IsNullOrEmpty(tlFax), tlFax, objClient.Fax)
                    objClient.EmailAddress = If(Not String.IsNullOrEmpty(tlEmail), tlEmail, objClient.EmailAddress)
                    objClient.Telephone1 = If(Not String.IsNullOrEmpty(tlPhone), tlPhone, objClient.Telephone1)

                    ' Add TL Customer to QB
                    Dim custAdd As ICustomerAdd = newMsgSetRq.AppendCustomerAddRq
                    custAdd.CompanyName.SetValue(TLClientName.ToString)
                    custAdd.Name.SetValue(objClient.ClientName.ToString)
                    custAdd.Fax.SetValue(If(objClient.Fax = Nothing, "", objClient.Fax))
                    custAdd.Email.SetValue(If(objClient.EmailAddress = Nothing, "", objClient.EmailAddress))
                    custAdd.Phone.SetValue(If(objClient.Telephone1 = Nothing, "", objClient.Telephone1))

                    'step2: send the request
                    msgSetRs = MAIN.SESSMANAGER.DoRequests(newMsgSetRq)

                    ' Interpret the response
                    Dim res As IResponse
                    res = msgSetRs.ResponseList.GetAt(0)

                    If res.StatusSeverity = "Error" Then
                        Throw New Exception(res.StatusMessage)
                    End If

                    My.Forms.MAIN.History("Added Name:" + TLClientName + " with TL ID: " + TL_ID.ToString + " to QuickBooks", "N")

                    ' The response detail for Add and Mod requests is a 'Ret' object
                    ' In our case, it's ICustomerRet
                    ' Dim TimeEntryRet As ICustomerRet
                    ' TimeEntryRet = res.Detail
                    ' My.Forms.MainHistory("Added: " + TimeEntryRet.FullName, "i")
                Else
                    Return False ' Do not add to QB nor sync table
                End If

                ' Make request again to make sure that we added to QB, then add to sync table
                msgSetRq = MAIN.SESSMANAGER.CreateMsgSetRequest("US", 2, 0)
                msgSetRq.Attributes.OnError = ENRqOnError.roeContinue

                CustomerQueryRq = msgSetRq.AppendCustomerQueryRq
                CustomerQueryRq.ORCustomerListQuery.FullNameList.Add(TLClientName)

                msgSetRs = MAIN.SESSMANAGER.DoRequests(msgSetRq)
                response = msgSetRs.ResponseList.GetAt(0)
                custRetList = response.Detail
            End If

            If Not custRetList Is Nothing Then
                'Assume only one return
                custRet = custRetList.GetAt(0)
                With custRet
                    If inQB Then
                        My.Forms.MAIN.History("Found name in QB: " + .Name.GetValue.ToString + vbTab + "--> ID: " + .ListID.GetValue.ToString, "i")
                    End If
                    ' check if its in our database if not then add to it.
                    Dim CustomerAdapter As New QB_TL_IDsTableAdapters.CustomersTableAdapter()
                    If IsQBID_In_CustomerDataTable(.Name.GetValue.ToString, .ListID.GetValue) = 0 Then
                        If IsTLID_In_CustomerDataTable(TL_ID) = 0 Then
                            ' Not in local database
                            My.Forms.MAIN.History("Adding customer to local database: " + TLClientName, "i")
                            CustomerAdapter.Insert(.ListID.GetValue, TL_ID, .Name.GetValue, TLClientName)
                        Else
                            ' In local database with a different QuickBooks ID
                            My.Forms.MAIN.History("Updating customer QuickBooks ID in local database: " + TLClientName, "i")
                            CustomerAdapter.UpdateQBID(.ListID.GetValue, TL_ID)
                        End If
                    Else
                        If IsTLID_In_CustomerDataTable(TL_ID) = 0 Then
                            ' In local database with a different TimeLive ID
                            My.Forms.MAIN.History("Updating customer TimeLive ID in local database: " + TLClientName, "i")
                            CustomerAdapter.UpdateTLID(TL_ID, .ListID.GetValue)
                        End If
                    End If
                End With
            End If

            Return inQB
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    ''' <summary>
    ''' Check if QB ID is in customer data table
    ''' </summary>
    ''' <param name="myqbName">qb name to be printed</param>
    ''' <param name="myqbID">qb id to be verified</param>
    ''' <returns>
    ''' 0 -> not in data table
    ''' 1 -> one record in data table
    ''' 2 -> more than one record in data table
    ''' </returns>
    Private Function IsQBID_In_CustomerDataTable(ByVal myqbName As String, ByVal myqbID As String) As Int16
        Dim CustomerAdapter As New QB_TL_IDsTableAdapters.CustomersTableAdapter()
        Dim TimeLiveIDs As QB_TL_IDs.CustomersDataTable = CustomerAdapter.GetCorrespondingTL_ID(myqbID)

        Dim result As Int16 = Math.Min(TimeLiveIDs.Count, 2)
        If TimeLiveIDs.Count = 1 Then
            My.Forms.MAIN.History("One record found in local database for: " + myqbName, "i")
        ElseIf TimeLiveIDs.Count = 0 Then
            My.Forms.MAIN.History("No records found in local database for:" + myqbName, "i")
        ElseIf TimeLiveIDs.Count > 1 Then
            My.Forms.MAIN.History("More than one record found for:" + myqbName, "I")
        End If

        Return result
    End Function

    ''' <summary>
    ''' Check if TL ID is in customer data table
    ''' </summary>
    ''' <param name="mytlID"></param>
    ''' <returns>
    ''' 0 -> not in data table
    ''' 1 -> one record in data table
    ''' 2 -> more than one record in data table
    ''' </returns>
    Private Function IsTLID_In_CustomerDataTable(ByVal mytlID As String) As Int16
        Dim CustomerAdapter As New QB_TL_IDsTableAdapters.CustomersTableAdapter
        Dim quickbooksIDs As QB_TL_IDs.CustomersDataTable = CustomerAdapter.GetCustomersByTLID(mytlID)
        Return Math.Min(2, quickbooksIDs.Count)
    End Function

End Class
