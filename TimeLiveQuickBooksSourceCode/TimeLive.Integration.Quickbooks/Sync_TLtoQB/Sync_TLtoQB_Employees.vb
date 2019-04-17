Imports QBFC13Lib

Public Class Sync_TLtoQB_Employees
    '---------------------Sync Employee TL Data to QB---------------------------------------
    ''' <summary>
    ''' Sync the employee data from QB. Print out employees that are in TL but not QB
    ''' </summary>
    Sub SyncEmployeeData(ByVal p_token As String, Optional ByVal UI As Boolean = True)
        Dim create As Boolean = True

        If UI Then
            create = MsgBox("Sync Employees?", MsgBoxStyle.YesNo, "Warning!") = MsgBoxResult.Yes
        End If

        If create Then
            My.Forms.MAIN.History("Syncing Employees Data", "n")
            Try
                ' connect to Time live
                Dim objEmployeeServices As New Services.TimeLive.Employees.Employees
                Dim authentication As New Services.TimeLive.Employees.SecuredWebServiceHeader
                authentication.AuthenticatedToken = p_token
                objEmployeeServices.SecuredWebServiceHeaderValue = authentication
                Dim objClientArray() As Object
                objClientArray = objEmployeeServices.GetEmployees
                Dim objEmployee As New Services.TimeLive.Employees.Employee

                ' Print employees within TimeLive that are not in QB
                For n As Integer = 0 To objClientArray.Length - 1
                    objEmployee = objClientArray(n)
                    With objEmployee
                        If Not .IsVendor Then
                            ' Check if in QB, adds to QB and sync data table if not
                            checkQBEmployeeExist(.EmployeeName.ToString, .EmployeeId, objEmployee, UI)
                        End If
                    End With
                Next
            Catch ex As Exception
                If UI Then
                    MsgBox(ex.Message)
                Else
                    Throw ex
                End If
            End Try
        End If

    End Sub

    ''' <summary>
    ''' Verifies an employee exists in QB, adding it to the Data Table if necessary
    ''' </summary>
    ''' <param name="TLEmployeeName"></param>
    ''' <param name="TL_ID"></param>
    ''' <returns>
    ''' False: does not exist in QB
    ''' True: does exist in QB, and we add it to the Data Table if not present
    ''' </returns>
    Public Function checkQBEmployeeExist(ByRef TLEmployeeName As String, ByVal TL_ID As Integer, ByVal objEmployee As Services.TimeLive.Employees.Employee, ByVal UI As Boolean) As Boolean
        'Dim sessManager As QBSessionManager

        Try
            'sessManager = New QBSessionManagerClass()
            Dim msgSetRq As IMsgSetRequest = MAIN.SESSMANAGER.CreateMsgSetRequest("US", 2, 0)
            msgSetRq.Attributes.OnError = ENRqOnError.roeContinue

            Dim EmployeeQueryRq As IEmployeeQuery = msgSetRq.AppendEmployeeQueryRq
            EmployeeQueryRq.ORListQuery.FullNameList.Add(TLEmployeeName)
            'sessManager.OpenConnection("App", "TimeLive Quickbooks")
            'sessManager.BeginSession("", ENOpenMode.omDontCare)
            Dim msgSetRs As IMsgSetResponse = MAIN.SESSMANAGER.DoRequests(msgSetRq)

            Dim response As IResponse = msgSetRs.ResponseList.GetAt(0)
            Dim empRetList As IEmployeeRetList = response.Detail

            Dim inQB = Not empRetList Is Nothing

            If Not inQB Then
                Dim newMsgSetRq As IMsgSetRequest = MAIN.SESSMANAGER.CreateMsgSetRequest("US", 2, 0)
                newMsgSetRq.Attributes.OnError = ENRqOnError.roeContinue

                Dim create As Boolean = True
                If UI Then
                    create = MsgBox("New employee found in TimeLive: " + TLEmployeeName + ". Create in QuickBooks?", MsgBoxStyle.YesNo, "Warning!") = MsgBoxResult.Yes
                End If

                If create Then
                    ' Add TL Employee to QB
                    Dim employAdd As IEmployeeAdd = newMsgSetRq.AppendEmployeeAddRq
                    employAdd.FirstName.SetValue(If(objEmployee.FirstName = Nothing, "", objEmployee.FirstName))
                    employAdd.LastName.SetValue(If(objEmployee.LastName = Nothing, "", objEmployee.LastName))
                    employAdd.MiddleName.SetValue(If(objEmployee.MiddleName = Nothing, "", objEmployee.MiddleName))
                    employAdd.HiredDate.SetValue(If(objEmployee.HiredDate = Nothing, Date.Now, objEmployee.HiredDate))
                    employAdd.Phone.SetValue(If(objEmployee.Phone = Nothing, "", objEmployee.Phone))
                    'employAdd.Mobile.SetValue(If(objEmployee.Mobile = Nothing, "", objEmployee.Mobile)) ' Errors for some reason
                    ' Employee Address
                    employAdd.EmployeeAddress.Addr1.SetValue(If(objEmployee.Address1 = Nothing, "", objEmployee.Address1))
                    employAdd.EmployeeAddress.Addr2.SetValue(If(objEmployee.Address2 = Nothing, "", objEmployee.Address2))
                    employAdd.EmployeeAddress.City.SetValue(If(objEmployee.City = Nothing, "", objEmployee.City))
                    employAdd.EmployeeAddress.PostalCode.SetValue(If(objEmployee.PostalCode = Nothing, "", objEmployee.PostalCode))
                    employAdd.EmployeeAddress.State.SetValue(If(objEmployee.State = Nothing, "", objEmployee.State))
                    employAdd.EmployeeAddress.Country.SetValue(If(objEmployee.Country = Nothing, "", objEmployee.Country))


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

                    EmployeeQueryRq = msgSetRq.AppendEmployeeQueryRq
                    EmployeeQueryRq.ORListQuery.FullNameList.Add(TLEmployeeName)

                    msgSetRs = MAIN.SESSMANAGER.DoRequests(msgSetRq)
                    response = msgSetRs.ResponseList.GetAt(0)
                    empRetList = response.Detail
                Else
                    Return False ' Do not add to QB or sync data table
                End If
                'Return False
                'Else
            End If
            'Assume only one return
            If Not empRetList Is Nothing Then
                Dim EmployeeRet As IEmployeeRet
                EmployeeRet = empRetList.GetAt(0)

                With EmployeeRet
                    If inQB Then
                        My.Forms.MAIN.History("Found name in QB: " + .Name.GetValue.ToString + " --> ID: " + .ListID.GetValue.ToString, "i")
                    End If
                    ' check if its in our database if not then add to it.
                    Dim EmployeeAdapter As New QB_TL_IDsTableAdapters.EmployeesTableAdapter()

                    ' Add QB employee to Data Table if not present
                    If ISQBID_In_EmployeeDataTable(.Name.GetValue.ToString, .ListID.GetValue) = 0 Then

                        'Dim firstmiddlelastname() As String = TLEmployeeName.Split(" ")
                        'Dim employeename As String = Nothing
                        'If firstmiddlelastname.Length = 3 Then
                        '    'employeename = firstmiddlelastname(2) + ", " + firstmiddlelastname(0) + " " + firstmiddlelastname(1)
                        'End If
                        'If firstmiddlelastname.Length = 2 Then
                        '    employeename = firstmiddlelastname(1) + ", " + firstmiddlelastname(0)
                        'End If
                        'If firstmiddlelastname.Length = 1 Then
                        '    employeename = firstmiddlelastname(0)
                        'End If

                        My.Forms.MAIN.History("Adding employee to sync database: " + TLEmployeeName, "i")
                        EmployeeAdapter.Insert(.ListID.GetValue, TL_ID, .Name.GetValue, TLEmployeeName)
                    Else
                        ' EmployeeAdapter.Update(.ListID.GetValue, TL_ID, .Name.GetValue, TLEmployeeName)
                    End If
                End With
            End If
            Return inQB
            'End If

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
    ''' Check if QB ID is in employee data table
    ''' </summary>
    ''' <param name="myqbName"></param>
    ''' <param name="myqbID"></param>
    ''' <returns>
    ''' 0 -> not in data table
    ''' 1 -> one record in data table
    ''' 2 -> more than one record in data table
    ''' </returns>
    Private Function ISQBID_In_EmployeeDataTable(ByVal myqbName As String, ByVal myqbID As String) As Int16
        Dim result As Int16 = 0
        Dim EmployeeAdapter As New QB_TL_IDsTableAdapters.EmployeesTableAdapter
        Dim TimeLiveIDs As QB_TL_IDs.EmployeesDataTable = EmployeeAdapter.GetCorrespondingTL_ID(myqbID)

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