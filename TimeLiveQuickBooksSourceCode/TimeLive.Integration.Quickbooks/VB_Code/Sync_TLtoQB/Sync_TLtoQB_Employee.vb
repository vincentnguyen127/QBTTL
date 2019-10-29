Imports QBFC13Lib

Public Class Sync_TLtoQB_Employee
    '---------------------Sync Employee TL Data to QB---------------------------------------
    ''' <summary>
    ''' Sync the employee data from QB. Print out employees that are in TL but not QB
    ''' </summary>
    Function SyncEmployeeData(ByVal p_token As String, Optional ByVal MainForm As MAIN = Nothing,
                              Optional ByVal UI As Boolean = True, Optional ByVal nameList As List(Of String) = Nothing)
        Dim numSynced As Integer = 0
        My.Forms.MAIN.History("Syncing Employees Data", "n")

        Try
            Dim objEmployeeServices As Services.TimeLive.Employees.Employees = MAIN.connect_TL_employees(p_token)
            Dim objEmployeeArray() As Object
            objEmployeeArray = objEmployeeServices.GetEmployees
            Dim objEmployee As New Services.TimeLive.Employees.Employee

            If Not MainForm Is Nothing Then
                MainForm.ProgressBar1.Maximum = objEmployeeArray.Length
                MainForm.ProgressBar1.Value = 0
            End If

            ' Print employees within TimeLive that are not in QB
            For n As Integer = 0 To objEmployeeArray.Length - 1
                objEmployee = objEmployeeArray(n)
                With objEmployee
                    Dim create As Boolean = If(nameList Is Nothing, True, nameList.Contains(objEmployee.EmployeeName))
                    If create Then
                        ' Check if in QB, adds to QB and sync data table if not
                        If .IsVendor Then
                            My.Forms.MAIN.History("Not adding vendor " + objEmployee.EmployeeName + " to employees in Quickbooks. Add as a vendor instead.", "i")
                        Else
                            numSynced += If(checkQBEmployeeExist(.EmployeeName.ToString, .EmployeeId, objEmployee, UI), 0, 1)
                        End If
                    End If
                End With
                If Not MainForm Is Nothing Then MainForm.ProgressBar1.Value += 1
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
                    employAdd.HiredDate.SetValue(If(objEmployee.HiredDate = Nothing, Today, objEmployee.HiredDate))
                    employAdd.Phone.SetValue(If(objEmployee.Phone = Nothing, "", objEmployee.Phone))
                    'employAdd.Mobile.SetValue(If(objEmployee.Mobile = Nothing, "", objEmployee.Mobile)) ' Errors for some reason
                    ' Employee Address
                    employAdd.EmployeeAddress.Addr1.SetValue(If(objEmployee.Address1 = Nothing, "", objEmployee.Address1))
                    employAdd.EmployeeAddress.Addr2.SetValue(If(objEmployee.Address2 = Nothing, "", objEmployee.Address2))
                    employAdd.EmployeeAddress.City.SetValue(If(objEmployee.City = Nothing, "", objEmployee.City))
                    employAdd.EmployeeAddress.PostalCode.SetValue(If(objEmployee.PostalCode = Nothing, "", objEmployee.PostalCode))
                    Dim state As String = If(objEmployee.State = Nothing, "", If(objEmployee.State.Length = 2, objEmployee.State.ToUpper(), If(objEmployee.State.ToLower() = "maryland", "MD", "")))
                    employAdd.EmployeeAddress.State.SetValue(If(objEmployee.State = Nothing, "", state))
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
                    If IsQBID_In_EmployeeDataTable(.Name.GetValue.ToString, .ListID.GetValue) = 0 Then
                        If IsTLID_In_EmployeeDataTable(TL_ID) = 0 Then
                            ' Not in local database
                            My.Forms.MAIN.History("Adding employee to local database: " + TLEmployeeName, "i")
                            EmployeeAdapter.Insert(.ListID.GetValue, TL_ID, .Name.GetValue, TLEmployeeName)
                        Else
                            ' In local database with a different QuickBooks ID
                            My.Forms.MAIN.History("Updating employee QuickBooks ID in local database: " + TLEmployeeName, "i")
                            EmployeeAdapter.UpdateQBID(.ListID.GetValue, TL_ID)
                        End If
                    Else
                        If IsTLID_In_EmployeeDataTable(TL_ID) = 0 Then
                            ' In local database with a different TimeLive ID
                            My.Forms.MAIN.History("Updating employee TimeLive ID in local database: " + TLEmployeeName, "i")
                            EmployeeAdapter.UpdateTLID(TL_ID, .ListID.GetValue)
                        End If
                    End If
                End With
            End If
            Return inQB
            'End If

        Catch ex As Exception
            Throw ex
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
    Private Function IsQBID_In_EmployeeDataTable(ByVal myqbName As String, ByVal myqbID As String) As Int16
        Dim EmployeeAdapter As New QB_TL_IDsTableAdapters.EmployeesTableAdapter
        Dim TimeLiveIDs As QB_TL_IDs.EmployeesDataTable = EmployeeAdapter.GetCorrespondingTL_ID(myqbID)

        Dim result As Int16 = Math.Min(2, TimeLiveIDs.Count)

        If TimeLiveIDs.Count = 1 Then
            My.Forms.MAIN.History("One record found in local database for: " + myqbName, "i")
        ElseIf TimeLiveIDs.Count = 0 Then
            My.Forms.MAIN.History("No records found in local database for:" + myqbName, "i")
        ElseIf TimeLiveIDs.Count > 1 Then
            My.Forms.MAIN.History("More than one record found in local database for:" + myqbName, "I")
        End If

        Return result
    End Function

    ''' <summary>
    ''' Check if TL ID is in employee data table
    ''' </summary>
    ''' <param name="mytlID"></param>
    ''' <returns>
    ''' 0 -> not in data table
    ''' 1 -> one record in data table
    ''' 2 -> more than one record in data table
    ''' </returns>
    Private Function IsTLID_In_EmployeeDataTable(ByVal mytlID As String) As Int16
        Dim EmployeeAdapter As New QB_TL_IDsTableAdapters.EmployeesTableAdapter
        Dim quickbooksIDs As QB_TL_IDs.EmployeesDataTable = EmployeeAdapter.GetEmployeesByTLID(mytlID)
        Return Math.Min(2, quickbooksIDs.Count)
    End Function
End Class