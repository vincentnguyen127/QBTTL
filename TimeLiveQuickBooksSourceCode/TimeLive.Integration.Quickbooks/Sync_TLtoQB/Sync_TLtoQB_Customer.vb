Imports QBFC13Lib

Public Class Sync_TLtoQB_Customer


    '---------------------Sync Customer TL Data to QB---------------------------------------
    ''' <summary>
    ''' Sync the customer data from QB. Print out customers that are in TL but not QB
    ''' </summary>
    Function SyncCustomerData(ByVal p_token As String, Optional ByVal IntegratedUIForm As IntegratedUI = Nothing,
                              Optional ByVal UI As Boolean = True, Optional ByVal nameList As List(Of String) = Nothing)
        Dim numSynced As Integer = 0
        My.Forms.MAIN.History("Syncing Clients Data", "n")
        Try
            ' connect to Timelive
            Dim objClientServices As New Services.TimeLive.Clients.Clients
            Dim authentication As New Services.TimeLive.Clients.SecuredWebServiceHeader
            authentication.AuthenticatedToken = p_token
            objClientServices.SecuredWebServiceHeaderValue = authentication
            Dim objClientArray() As Object
            objClientArray = objClientServices.GetClients()
            Dim objClient As New Services.TimeLive.Clients.Client

            If Not IntegratedUIForm Is Nothing Then IntegratedUIForm.ProgressBar1.Maximum = objClientArray.Length

            For n As Integer = 0 To objClientArray.Length - 1
                objClient = objClientArray(n)
                Dim clientID As Integer = objClientServices.GetClientIdByName(objClient.ClientName)
                ' Only run for a name if it was selected, or we are syncing all of the names
                Dim create As Boolean = If(nameList Is Nothing, True, nameList.Contains(objClient.ClientName))
                If create Then
                    numSynced += If(checkQBCustomerExist(objClient.ClientName.ToString, clientID, objClient, UI), 0, 1)
                End If
                If Not IntegratedUIForm Is Nothing Then IntegratedUIForm.ProgressBar1.Value += 1
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

        'Dim sessManager As QBSessionManager
        Dim custRet As ICustomerRet

        Try
            'sessManager = New QBSessionManagerClass()
            Dim msgSetRq As IMsgSetRequest = MAIN.SESSMANAGER.CreateMsgSetRequest("US", 2, 0)

            msgSetRq.Attributes.OnError = ENRqOnError.roeContinue
            Dim CustomerQueryRq As ICustomerQuery = msgSetRq.AppendCustomerQueryRq

            CustomerQueryRq.ORCustomerListQuery.FullNameList.Add(TLClientName)
            'sessManager.OpenConnection("App", "TimeLive Quickbooks")
            'sessManager.BeginSession("", ENOpenMode.omDontCare)
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
                    ' Add TL Customer to QB
                    Dim custAdd As ICustomerAdd = newMsgSetRq.AppendCustomerAddRq
                    custAdd.CompanyName.SetValue(TLClientName.ToString)
                    custAdd.Name.SetValue(TLClientName.ToString)
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
                    ' My.Forms.MAIN.History("Added: " + TimeEntryRet.FullName, "i")
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

                'Return False
                'Else
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
                    If Not CBool(ISQBID_In_CustomerDataTable(.Name.GetValue.ToString, .ListID.GetValue)) Then
                        My.Forms.MAIN.History("Adding customer to local database: " + TLClientName, "i")
                        CustomerAdapter.Insert(.ListID.GetValue, TL_ID, .Name.GetValue, TLClientName)
                    Else
                        CustomerAdapter.Update(.ListID.GetValue, TL_ID, .Name.GetValue, TLClientName)
                    End If
                End With
            End If

            Return inQB
            'End If
        Catch ex As Exception
            Throw ex
            '     'Finally
            '    If Not sessManager Is Nothing Then
            '       sessManager.EndSession()
            '       sessManager.CloseConnection()
            '    End If
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
    Private Function ISQBID_In_CustomerDataTable(ByVal myqbName As String, ByVal myqbID As String) As Int16

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

End Class
