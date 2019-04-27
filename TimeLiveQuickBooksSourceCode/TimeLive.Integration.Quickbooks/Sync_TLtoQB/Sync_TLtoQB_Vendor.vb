Imports QBFC13Lib

Public Class Sync_TLtoQB_Vendor
    '---------------------Sync Vendor TL Data to QB---------------------------------------
    'Note need to detect contractors; these need to be linked as vendors
    ''' <summary>
    ''' Sync the vendor data from QB. Print out vendors that are in TL but not QB
    ''' </summary>
    Function SyncVendorData(ByVal p_token As String, Optional IntegratedUIForm As IntegratedUI = Nothing,
                            Optional ByVal UI As Boolean = True, Optional ByVal nameList As List(Of String) = Nothing)
        Dim numSynced As Integer = 0
        My.Forms.MAIN.History("Syncing Vendor Data", "n")
        Try
            ' connect to Time live
            'Dim play As New Services.TimeLive.Employees.Employees
            Dim objEmployeeServices As New Services.TimeLive.Employees.Employees
            Dim authentication As New Services.TimeLive.Employees.SecuredWebServiceHeader
            authentication.AuthenticatedToken = p_token
            objEmployeeServices.SecuredWebServiceHeaderValue = authentication
            Dim objEmployeeArray() As Object
            objEmployeeArray = objEmployeeServices.GetEmployees
            Dim objEmployee As New Services.TimeLive.Employees.Employee
            If Not IntegratedUIForm Is Nothing Then IntegratedUIForm.ProgressBar1.Maximum = objEmployeeArray.Length

            For n As Integer = 0 To objEmployeeArray.Length - 1
                objEmployee = objEmployeeArray(n)
                With objEmployee
                    Dim create As Boolean = If(nameList Is Nothing, True, nameList.Contains(objEmployee.EmployeeName))
                    If .IsVendor And create Then
                        numSynced += If(checkQBVendorExist(.EmployeeName.ToString, .EmployeeId, objEmployee, UI), 0, 1)
                    End If
                End With
                If Not IntegratedUIForm Is Nothing Then IntegratedUIForm.ProgressBar1.Value += 1
            Next
        Catch ex As Exception
            If UI Then
                MsgBox(ex.Message)
            Else
                Throw ex
            End If
        End Try

        Return numSynced
    End Function

    ''' <summary>
    ''' Verifies a vendor exists in QB, adding it to the Data Table if necessary
    ''' </summary>
    ''' <param name="TLEmployeeName"></param>
    ''' <param name="TL_ID"></param>
    ''' <returns>
    ''' False: does not exist in QB
    ''' True: does exist in QB, and we add it to the Data Table if not present
    ''' </returns>
    Public Function checkQBVendorExist(ByRef TLEmployeeName As String, ByVal TL_ID As Integer, ByVal objEmployee As Services.TimeLive.Employees.Employee, ByVal UI As Boolean) As Boolean
        'Dim sessManager As QBSessionManager

        Try
            'sessManager = New QBSessionManagerClass()
            Dim msgSetRq As IMsgSetRequest = MAIN.SESSMANAGER.CreateMsgSetRequest("US", 2, 0)

            msgSetRq.Attributes.OnError = ENRqOnError.roeContinue
            Dim VendorQueryRq As IVendorQuery = msgSetRq.AppendVendorQueryRq

            VendorQueryRq.ORVendorListQuery.FullNameList.Add(TLEmployeeName)
            'sessManager.OpenConnection("App", "TimeLive Quickbooks")
            'sessManager.BeginSession("", ENOpenMode.omDontCare)
            Dim msgSetRs As IMsgSetResponse = MAIN.SESSMANAGER.DoRequests(msgSetRq)

            Dim response As IResponse = msgSetRs.ResponseList.GetAt(0)
            Dim vendorRetList As IVendorRetList
            vendorRetList = response.Detail

            Dim inQB As Boolean = Not vendorRetList Is Nothing

            ' Add to QB if not present
            If Not inQB Then
                Dim create As Boolean = True
                If UI Then
                    create = MsgBox("New vendor found in TimeLive: " + TLEmployeeName + ". Create in QuickBooks?", MsgBoxStyle.YesNo, "Warning!") = MsgBoxResult.Yes
                End If
                If create Then
                    Dim newMsgSetRq As IMsgSetRequest = MAIN.SESSMANAGER.CreateMsgSetRequest("US", 2, 0)
                    newMsgSetRq.Attributes.OnError = ENRqOnError.roeContinue
                    ' Add TL Employee to QB as a Vendor
                    Dim vendorAdd As IVendorAdd = newMsgSetRq.AppendVendorAddRq
                    vendorAdd.IsVendorEligibleFor1099.SetValue(True)
                    vendorAdd.VendorTypeRef.FullName.SetValue("1099 contractor")
                    vendorAdd.Name.SetValue(objEmployee.FirstName + " " + objEmployee.LastName)
                    vendorAdd.FirstName.SetValue(If(objEmployee.FirstName = Nothing, "", objEmployee.FirstName))
                    vendorAdd.LastName.SetValue(If(objEmployee.LastName = Nothing, "", objEmployee.LastName))
                    vendorAdd.MiddleName.SetValue(If(objEmployee.MiddleName = Nothing, "", objEmployee.MiddleName))
                    vendorAdd.OpenBalanceDate.SetValue(If(objEmployee.HiredDate = Nothing, Date.Now, objEmployee.HiredDate))
                    vendorAdd.Phone.SetValue(If(objEmployee.Phone = Nothing, "", objEmployee.Phone))
                    'vendorAdd.Mobile.SetValue(If(objEmployee.Mobile = Nothing, "", objEmployee.Mobile)) ' Errors for some reason

                    ' Employee Address
                    vendorAdd.VendorAddress.Addr1.SetValue(If(objEmployee.Address1 = Nothing, "", objEmployee.Address1))
                    vendorAdd.VendorAddress.Addr2.SetValue(If(objEmployee.Address2 = Nothing, "", objEmployee.Address2))
                    vendorAdd.VendorAddress.City.SetValue(If(objEmployee.City = Nothing, "", objEmployee.City))
                    vendorAdd.VendorAddress.PostalCode.SetValue(If(objEmployee.PostalCode = Nothing, "", objEmployee.PostalCode))
                    vendorAdd.VendorAddress.State.SetValue(If(objEmployee.State = Nothing, "", objEmployee.State))
                    vendorAdd.VendorAddress.Country.SetValue(If(objEmployee.Country = Nothing, "", objEmployee.Country))


                    'step2: send the request
                    msgSetRs = MAIN.SESSMANAGER.DoRequests(newMsgSetRq)

                    ' Interpret the response
                    Dim res As IResponse
                    res = msgSetRs.ResponseList.GetAt(0)

                    If res.StatusSeverity = "Error" Then
                        Throw New Exception(res.StatusMessage)
                    End If

                    My.Forms.MAIN.History("Added Name: " + TLEmployeeName.ToString + " with TimeLive ID: " + TL_ID.ToString + " to QuickBooks", "N")

                    msgSetRq = MAIN.SESSMANAGER.CreateMsgSetRequest("US", 2, 0)
                    msgSetRq.Attributes.OnError = ENRqOnError.roeContinue

                    VendorQueryRq = msgSetRq.AppendVendorQueryRq
                    VendorQueryRq.ORVendorListQuery.FullNameList.Add(TLEmployeeName)

                    msgSetRs = MAIN.SESSMANAGER.DoRequests(msgSetRq)
                    response = msgSetRs.ResponseList.GetAt(0)
                    vendorRetList = response.Detail
                Else
                    Return False
                End If
                'Else
            End If
            'Assume only one return
            If Not vendorRetList Is Nothing Then
                Dim VendorRet As IVendorRet
                VendorRet = vendorRetList.GetAt(0)
                With VendorRet
                    If inQB Then
                        My.Forms.MAIN.History("Found vendor name in QB: " + .Name.GetValue + " --> ID: " + .ListID.GetValue.ToString, "i")
                    End If
                    ' check if its in our database if not then add to it.
                    Dim VendorAdapter As New QB_TL_IDsTableAdapters.VendorsTableAdapter()

                    If ISQBID_In_VendorDataTable(.Name.GetValue.ToString, .ListID.GetValue) <= 0 Then
                        My.Forms.MAIN.History("Adding to sync database: " + .Name.GetValue, "i")
                        VendorAdapter.Insert(.ListID.GetValue, TL_ID, .Name.GetValue, TLEmployeeName)
                    Else
                        VendorAdapter.Update(.ListID.GetValue, TL_ID, .Name.GetValue, TLEmployeeName)
                    End If
                End With
            Else
                My.Forms.MAIN.History("Error when adding " + TLEmployeeName + " to Quickbooks", "i")
            End If
            Return inQB

        Catch ex As Exception
            Throw ex
            'Finally
            '    If Not sessManager Is Nothing Then
            '       sessManager.EndSession()
            '       sessManager.CloseConnection()
            '    End If
        End Try


    End Function

    ''' <summary>
    ''' Check if QB ID is in vendor data table
    ''' </summary>
    ''' <param name="myqbName"></param>
    ''' <param name="myqbID"></param>
    ''' <returns>
    ''' 0 -> not in data table
    ''' 1 -> one record in data table
    ''' 2 -> more than one record in data table
    ''' </returns>
    Private Function ISQBID_In_VendorDataTable(ByVal myqbName As String, ByVal myqbID As String) As Int16
        Dim result As Int16 = 0
        Dim VendorAdapter As New QB_TL_IDsTableAdapters.VendorsTableAdapter
        Dim TimeLiveIDs As QB_TL_IDs.VendorsDataTable = VendorAdapter.GetCorrespondingTL_ID(myqbID)

        If TimeLiveIDs.Count = 1 Then
            result = 1
            My.Forms.MAIN.History("One record found in QB sync table for: " + myqbName, "i")
        End If

        If TimeLiveIDs.Count = 0 Then
            result = 0
            My.Forms.MAIN.History("No records found on QB sync table for:" + myqbName, "i")
        End If

        If TimeLiveIDs.Count > 1 Then
            result = 2
            My.Forms.MAIN.History("More than one record found for:" + myqbName, "I")
        End If

        Return result
    End Function
End Class