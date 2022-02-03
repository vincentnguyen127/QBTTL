'Imports QBFC11Lib
Imports QBFC13Lib

Imports System.Net.Mail
Imports System.Data.SqlClient


Public Class QBtoTL_Employee
    Dim empRet As IEmployeeRet
    Dim vendorRet As IVendorRet

    Public Class EmployeeDataStructureQB
        Public NoItems As Integer = 0
        'Public NoInactive As Integer = 0 'Add this once we start tracking inactive employees
        Public DataArray As New List(Of Employee)
    End Class
    Public Class Employee
        Public RecSelect As Boolean
        Public NewlyAdded As String
        Public QB_Name As String
        Public QB_ID As String
        Public QBModTime As String
        Public QBCreateTime As String
        Public FirstName As String
        Public LastName As String
        Public HiredDate As Date
        Public Email As String
        Public Enabled As Boolean
        Public EditSequence As String


        Sub New(ByVal NewlyAdded As String, ByVal QB_Name As String, ByVal Email As String, ByVal QB_ID As String, ByVal FirstName As String,
                ByVal LastName As String, ByVal HiredDate As Date, ByVal ModTime As String, ByVal CreateTime As String, ByVal Enabled As Boolean)
            RecSelect = False
            Me.NewlyAdded = NewlyAdded
            Me.QBModTime = ModTime
            Me.QBCreateTime = CreateTime
            Me.QB_Name = QB_Name
            Me.QB_ID = QB_ID
            Me.Email = Email
            Me.FirstName = FirstName
            Me.LastName = LastName
            Me.HiredDate = HiredDate
            Me.Enabled = Enabled
        End Sub
    End Class

    Public Function GetEmployeeQBData(MainForm As MAIN, UI As Boolean) As EmployeeDataStructureQB
        Dim EmailAddress As String
        Dim FirstName As String
        Dim LastName As String
        Dim HiredDate As String
        Dim ModTime As String
        Dim CreateTime As String
        Dim EmployeeData As New EmployeeDataStructureQB
        Dim NewlyAdd As String

        'step1: prepare the request
        Dim msgSetRs As IMsgSetResponse
        Try
            Dim msgSetRq As IMsgSetRequest = MAIN.SESSMANAGER.CreateMsgSetRequest("US", 2, 0) 'sessManager
            msgSetRq.Attributes.OnError = ENRqOnError.roeContinue

            '------------------------------1-----------------------------------
            Dim employeequery As IEmployeeQuery = msgSetRq.AppendEmployeeQueryRq
            employeequery.ORListQuery.ListFilter.ActiveStatus.SetValue(ENActiveStatus.asActiveOnly)

            'step2: send the request
            msgSetRs = MAIN.SESSMANAGER.DoRequests(msgSetRq)
            Dim respList As IResponseList = msgSetRs.ResponseList

            If respList Is Nothing Then
                ' no data
                My.Forms.MAIN.History("No Employees found...", "i")
                Return Nothing
            End If
            ' Should only expect 1 response
            Dim resp As IResponse
            resp = respList.GetAt(0)
            If resp.StatusCode = 0 Then
                '------------------------------2-----------------------------------
                Dim empRetList As IEmployeeRetList
                empRetList = resp.Detail

                '------------------------------3-----------------------------------
                Dim empRet As IEmployeeRet
                'sets status bar, If no, UI skip
                Dim pblength As Integer = If(empRetList Is Nothing, 0, empRetList.Count)
                If UI Then
                    My.Forms.MAIN.ProgressBar1.Maximum += pblength
                End If

                For i As Integer = 0 To pblength - 1
                    empRet = empRetList.GetAt(i)

                    With empRet
                        EmailAddress = If(.Email Is Nothing, "", .Email.GetValue)
                        FirstName = If(.FirstName Is Nothing, "", .FirstName.GetValue)
                        LastName = If(.LastName Is Nothing, "", .LastName.GetValue)
                        HiredDate = If(.HiredDate Is Nothing, "", .HiredDate.GetValue)
                        CreateTime = If(.TimeCreated Is Nothing, "", .TimeCreated.GetValue.ToString)
                        ModTime = If(.TimeModified Is Nothing, CreateTime, .TimeModified.GetValue.ToString)

                        Dim TL_ID_Count = ISQBID_In_DataTable(.Name.GetValue, .ListID.GetValue)

                        NewlyAdd = If(TL_ID_Count, "", "N") ' N if new

                        ' will check which type data should be added 
                        EmployeeData.NoItems += 1
                        EmployeeData.DataArray.Add(New Employee(NewlyAdd, .Name.GetValue, EmailAddress, .ListID.GetValue, FirstName, LastName, HiredDate, ModTime, CreateTime, .IsActive.GetValue))
                    End With
                    If UI Then
                        My.Forms.MAIN.ProgressBar1.Value = i + 1
                    End If
                Next
            End If
            If msgSetRs.ResponseList.GetAt(0).StatusSeverity = "Error" Then
                My.Forms.MAIN.History(msgSetRs.ResponseList.GetAt(0).StatusMessage, "C")
                Throw New Exception(msgSetRs.ResponseList.GetAt(0).StatusMessage)
            End If
        Catch ex As Exception
            My.Forms.MAIN.History(ex.ToString, "C")
            Throw ex
        End Try

        Return EmployeeData
    End Function

    ' Note: Will need to add things similar to QBTransferCustomerToTL if we add Enabled as variable
    Public Function QBTransferEmployeeToTL(ByRef objData As QBtoTL_Employee.EmployeeDataStructureQB,
                                   ByVal token As String, MainForm As MAIN, UI As Boolean) As Integer
        Dim objEmployeeServices As Services.TimeLive.Employees.Employees = MAIN.connect_TL_employees(token)

        Dim objServices As New Services.TimeLiveServices
        Dim authentication As New Services.SecuredWebServiceHeader
        authentication.AuthenticatedToken = token
        objServices.SecuredWebServiceHeaderValue = authentication

        Dim nDepartmentId As Integer = objServices.GetDepartmentId()
        Dim nRoleId As Integer = objEmployeeServices.GetUserRoleId()
        Dim nLocationId As Integer = objServices.GetLocationId()
        Dim nEmployeeTypeId As Guid = objEmployeeServices.GetEmployeeTypeId()
        Dim nEmployeeStatusId As Integer = objEmployeeServices.GetEmployeeStatusId()
        Dim nWorkingDayTypeId As Guid = objEmployeeServices.GetEmployeeWorkingDayTypeId()
        Dim nBillingTypeId As Integer = objEmployeeServices.GetEmployeeBillingTypeId()

        'sets status bar. If no, UI skip
        Dim incrementbar As Integer = 0
        If UI Then
            Dim pblenth As Integer = objData.DataArray.Count
            'If pblenth >= 0 Then
            My.Forms.MAIN.ProgressBar1.Maximum = pblenth
            My.Forms.MAIN.ProgressBar1.Value = 0
            'End If
        End If

        Dim NoRecordsCreatedorUpdated = 0
        For Each element As QBtoTL_Employee.Employee In objData.DataArray

            ' check if the check value is true
            If element.RecSelect Then
                'Check number of QB records that match ID
                My.Forms.MAIN.History("Processing:  " + element.QB_Name, "n")

                Dim EmailAddress As String
                Dim FirstName As String
                Dim LastName As String
                Dim EmployeeName As String
                Dim HiredDate As String
                ' 0: QuickBooks ID is not in Data Table
                ' 1: QuickBooks ID is in DataTable
                Dim DT_has_QBID = ISQBID_In_DataTable(element.QB_Name, element.QB_ID)

                Dim create As Boolean = True
                ' Do not show Message Box when no UI or when QB ID is in our Data Table, just create employee
                If UI And Not CBool(DT_has_QBID) Then
                    'create = MsgBox("New employee found: " + element.QB_Name + ". Create?", MsgBoxStyle.YesNo, "Warning!") = MsgBoxResult.Yes
                End If
                If create Then
                    ' If QB_ID is in the DB, check if TL ID is too
                    If DT_has_QBID Then
                        Dim TL_ID As String = ISTLID_In_DataTable(element.QB_ID)
                        If TL_ID Is Nothing Then
                            My.Forms.MAIN.History("Detected empty sync record (No TL ID). Needs to be manually sync or deleted." + element.QB_Name, "i")
                        End If
                        Dim employeeInTL As Boolean = False
                        ' Check if TL has the TL ID that the DB has
                        Array.ForEach(objEmployeeServices.GetEmployees,
                                      Sub(e As Services.TimeLive.Employees.Employee)
                                          If objEmployeeServices.GetEmployeeId(e.EmployeeName) = Trim(TL_ID) Then
                                              employeeInTL = True
                                              ' QB and TL have different names, change in DB and alert the user
                                              ' Note: If we change this, then might need to change jobs/subjobs DB
                                              If Not e.EmployeeName = element.QB_Name Then
                                                  Dim EmployeeAdapter As New QB_TL_IDsTableAdapters.EmployeesTableAdapter()
                                                  EmployeeAdapter.updateEmployeeNames(element.QB_Name, e.EmployeeName, element.QB_ID, Trim(TL_ID))
                                                  My.Forms.MAIN.History("Name Conflict: TL Name: " + e.EmployeeName + " QB Name: " + element.QB_Name, "N")
                                              End If
                                          End If
                                      End Sub)
                        If employeeInTL Then
                            ' TL already has this value and so does our DB, so just move to next element after updating Progress Bar
                            If UI Then
                                incrementbar += 1
                                My.Forms.MAIN.ProgressBar1.Value = incrementbar
                            End If
                            ' TODO: Update TL, based on commented out code below
                            Continue For
                        End If
                    Else
                        ' QB ID is not in local Database
                        If Array.Exists(objEmployeeServices.GetEmployees, Function(e As Services.TimeLive.Employees.Employee) e.EmployeeName = element.QB_Name) Then
                            ' TimeLive has a data entry with the same name, treat as the same and add into DB
                            Dim EmployeeAdapter As New QB_TL_IDsTableAdapters.EmployeesTableAdapter()
                            Dim QB_ID_fromDB As QB_TL_IDs.EmployeesDataTable = EmployeeAdapter.GetCorrespondingQB_IDbyQB_Name(element.QB_Name)
                            If QB_ID_fromDB.Count = 0 Then
                                ' No record of the data entry in our data table, then add it
                                EmployeeAdapter.Insert(element.QB_ID, objEmployeeServices.GetEmployeeId(element.QB_Name), element.QB_Name, element.QB_Name)
                                My.Forms.MAIN.History("Employee '" + element.QB_Name + "' found in both TimeLive and Quickbooks added to local database", "i")
                            Else
                                ' Record exists just with an incorrect QB ID, so update it
                                Dim correctTL_ID As String = QB_ID_fromDB(0)(1)
                                If correctTL_ID IsNot Nothing Then
                                    EmployeeAdapter.UpdateQBID(element.QB_ID, Trim(correctTL_ID))
                                    My.Forms.MAIN.History("Updated QuickBooks ID of employee '" + element.QB_Name + "' in local database", "i")
                                End If
                            End If
                            Continue For ' Already in TL, so just continue to next element in QB
                        End If
                    End If
                    ' Create the element in TL:
                    NoRecordsCreatedorUpdated += 1
                    Dim whereToInsert As String = If(DT_has_QBID, "TimeLive: ", "local database and TimeLive: ")
                    My.Forms.MAIN.History("Inserting employee into " + whereToInsert + element.QB_Name, "i")

                    'Insert record into TimeLive
                    With element
                        Try
                            FirstName = GetValue(.QB_Name, "FirstName")
                            FirstName = FirstName.Replace(",", "")
                            LastName = GetValue(.QB_Name, "LastName")
                            LastName = LastName.Replace(",", "")
                            HiredDate = GetValue(.HiredDate, "HiredDate")
                            EmployeeName = FirstName + " " + LastName ' Changed "," to " "
                            EmailAddress = GetEmailAddress(.Email, token, FirstName(0) + LastName)

                            objEmployeeServices.InsertEmployee(EmailAddress, CreatePassword(HiredDate), FirstName, LastName, EmailAddress, .QB_Name,
                                                               nDepartmentId, nRoleId, nLocationId, 233, nBillingTypeId, Now.Date,
                                                               -1, 0, 6, 0, 0, nEmployeeTypeId, nEmployeeStatusId, "", HiredDate,
                                                               Now.Date, nWorkingDayTypeId, System.Guid.Empty, 0, System.Guid.Empty,
                                                               False, "", "", "", "", "", "", "", "", "", "Mr.", True)
                            My.Forms.MAIN.History("Transfer to TimeLive was successful.", "i")

                            'Insert record into sync database if not in it
                            If Not CBool(DT_has_QBID) Then
                                Dim employeeInTL As Boolean = Array.Exists(objEmployeeServices.GetEmployees,
                                                                   Function(e As Services.TimeLive.Employees.Employee)
                                                                       Return e.EmployeeName = EmployeeName
                                                                   End Function)
                                If employeeInTL Then
                                    'Note: if EmployeeName is changed back to "firstName,lastName", change to GetEmployeeID(firstName + " " + lastName)
                                    Dim TLClientID As String = objEmployeeServices.GetEmployeeId(EmployeeName)
                                    My.Forms.MAIN.History("TimeLive Employee ID: " + TLClientID, "i")
                                    My.Forms.MAIN.History("Inserting new employee into sync db.", "i")
                                    Dim EmployeesAdapter As New QB_TL_IDsTableAdapters.EmployeesTableAdapter()
                                    EmployeesAdapter.Insert(.QB_ID, TLClientID, .QB_Name, EmployeeName)
                                Else
                                    My.Forms.MAIN.History("Error creating record in TimeLive", "N")
                                End If
                            End If

                        Catch ex As System.Web.Services.Protocols.SoapException
                            My.Forms.MAIN.History("Employee Already has that email address, try a different one", "N")
                        Catch ex As Exception
                            My.Forms.MAIN.History("Transfer failed." + ex.ToString, "N")
                        End Try
                    End With
                End If

                'Dim foundRows() As DataRow

                '' Use the Select method to find all rows matching the filter.
                'foundRows = employees.Select(String.Format("AccountEmployeeId = '{0}'", 16))


                'Dim AccountEmployeeId As Integer = 16
                'If Not IsDBNull(foundRows(0)("AccountEmployeeId")) Then
                '    AccountEmployeeId = foundRows(0)("AccountEmployeeId")
                'End If
                ''My.Forms.Main.History("AccountEmployeeId: " + AccountEmployeeId.ToString, "n")

                'Dim Password As String = ""
                'If Not IsDBNull(foundRows(0)("Password")) Then
                '    Password = foundRows(0)("Password")
                'End If
                ''My.Forms.Main.History("Password: " + Password, "n")

                'Dim Prefix As String = ""
                'If Not IsDBNull(foundRows(0)("Prefix")) Then
                '    Prefix = foundRows(0)("Prefix")
                'End If
                ''My.Forms.Main.History("Prefix: " + Prefix, "n")

                'Dim EmployeeCode As String = ""
                'If Not IsDBNull(foundRows(0)("EmployeeCode")) Then
                '    EmployeeCode = foundRows(0)("EmployeeCode")
                'End If
                ''My.Forms.Main.History("EmployeeCode: " + EmployeeCode, "n")

                'Dim MiddleName As String = ""
                'If Not IsDBNull(foundRows(0)("MiddleName")) Then
                '    MiddleName = foundRows(0)("MiddleName")
                'End If
                ''My.Forms.Main.History("MiddleName: " + MiddleName, "n")

                'Dim AccountDepartmentID As Integer = nDepartmentId
                'If Not IsDBNull(foundRows(0)("AccountDepartmentID")) Then
                '    AccountDepartmentID = foundRows(0)("AccountDepartmentID")
                'End If
                ''My.Forms.Main.History("AccountDepartmentID: " + AccountDepartmentID.ToString, "n")

                'Dim AccountRoleID As Integer = nRoleId
                'If Not IsDBNull(foundRows(0)("AccountRoleID")) Then
                '    AccountRoleID = foundRows(0)("AccountRoleID")
                'End If
                ''My.Forms.Main.History("AccountRoleID: " + AccountRoleID.ToString, "n")

                'Dim AccountLocationID As Integer = nLocationId
                'If Not IsDBNull(foundRows(0)("AccountLocationID")) Then
                '    AccountLocationID = foundRows(0)("AccountLocationID")
                'End If
                ''My.Forms.Main.History("AccountLocationID: " + AccountLocationID.ToString, "n")

                'Dim CountryId As Short = 233
                'If Not IsDBNull(foundRows(0)("CountryId")) Then
                '    CountryId = foundRows(0)("CountryId")
                'End If
                ''My.Forms.Main.History("CountryId: " + CountryId.ToString, "n")

                'Dim BillingTypeId As Integer = nBillingTypeId
                'If Not IsDBNull(foundRows(0)("BillingTypeId")) Then
                '    BillingTypeId = foundRows(0)("BillingTypeId")
                'End If
                ''My.Forms.Main.History("BillingTypeId: " + BillingTypeId.ToString, "n")

                'Dim StartDate As DateTime = Now()
                'If Not IsDBNull(foundRows(0)("StartDate")) Then
                '    StartDate = foundRows(0)("StartDate")
                'End If
                ''My.Forms.Main.History("StartDate: " + StartDate.ToString, "n")

                'Dim TerminationDate As DateTime = Now()
                'If Not IsDBNull(foundRows(0)("TerminationDate")) Then
                '    StartDate = foundRows(0)("TerminationDate")
                'End If
                ''My.Forms.Main.History("TerminationDate: " + TerminationDate.ToString, "n")

                'Dim StatusId As Integer = nEmployeeStatusId
                'If Not IsDBNull(foundRows(0)("StatusId")) Then
                '    StatusId = foundRows(0)("StatusId")
                'End If
                ''My.Forms.Main.History("StatusId: " + StatusId.ToString, "n")

                'Dim IsDeleted As Boolean = False
                'If Not IsDBNull(foundRows(0)("IsDeleted")) Then
                '    IsDeleted = foundRows(0)("IsDeleted")
                'End If
                ''My.Forms.Main.History("IsDeleted: " + IsDeleted.ToString, "n")

                'Dim IsDisabled As Boolean = False
                'If Not IsDBNull(foundRows(0)("IsDisabled")) Then
                '    IsDisabled = foundRows(0)("IsDisabled")
                'End If
                ''My.Forms.Main.History("Password: " + IsDisabled.ToString, "n")

                'Dim DefaultProjectId As Integer = -1
                'If Not IsDBNull(foundRows(0)("DefaultProjectId")) Then
                '    DefaultProjectId = foundRows(0)("DefaultProjectId")
                'End If
                ''My.Forms.Main.History("DefaultProjectId: " + DefaultProjectId.ToString, "n")

                'Dim EmployeeManagerId As Integer = 0
                'If Not IsDBNull(foundRows(0)("EmployeeManagerId")) Then
                '    EmployeeManagerId = foundRows(0)("EmployeeManagerId")
                'End If
                ''My.Forms.Main.History("EmployeeManagerId: " + EmployeeManagerId.ToString, "n")

                'Dim TimeZoneId As Integer = 6
                'If Not IsDBNull(foundRows(0)("TimeZoneId")) Then
                '    TimeZoneId = foundRows(0)("TimeZoneId")
                'End If
                ''My.Forms.Main.History("TimeZoneId: " + TimeZoneId.ToString, "n")

                'Dim CreatedByEmployeeId As Integer = 0
                'If Not IsDBNull(foundRows(0)("CreatedByEmployeeId")) Then
                '    CreatedByEmployeeId = foundRows(0)("CreatedByEmployeeId")
                'End If
                ''My.Forms.Main.History("CreatedByEmployeeId: " + CreatedByEmployeeId.ToString, "n")

                'Dim ModifiedByEmployeeId As Integer = 0
                'If Not IsDBNull(foundRows(0)("ModifiedByEmployeeId")) Then
                '    ModifiedByEmployeeId = foundRows(0)("ModifiedByEmployeeId")
                'End If
                ''My.Forms.Main.History("ModifiedByEmployeeId: " + ModifiedByEmployeeId.ToString, "n")

                'Dim AllowedAccessFromIP As String = ""
                'If Not IsDBNull(foundRows(0)("AllowedAccessFromIP")) Then
                '    AllowedAccessFromIP = foundRows(0)("AllowedAccessFromIP")
                'End If
                ''My.Forms.Main.History("AllowedAccessFromIP: " + AllowedAccessFromIP, "n")

                'Dim EmployeePayTypeId As Guid = nEmployeeTypeId
                'If Not IsDBNull(foundRows(0)("EmployeePayTypeId")) Then
                '    EmployeePayTypeId = foundRows(0)("EmployeePayTypeId")
                'End If
                ''My.Forms.Main.History("EmployeePayTypeId: " + EmployeePayTypeId.ToString, "n")

                'Dim JobTitle As String = ""
                'If Not IsDBNull(foundRows(0)("JobTitle")) Then
                '    JobTitle = foundRows(0)("JobTitle")
                'End If
                ''My.Forms.Main.History("JobTitle: " + JobTitle, "n")

                'Dim AccountWorkingDayTypeId As Guid = nWorkingDayTypeId
                'If Not IsDBNull(foundRows(0)("AccountWorkingDayTypeId")) Then
                '    AccountWorkingDayTypeId = foundRows(0)("AccountWorkingDayTypeId")
                'End If
                ''My.Forms.Main.History("AccountWorkingDayTypeId " + AccountWorkingDayTypeId.ToString, "n")

                'Dim AccountTimeOffPolicyId As Guid = System.Guid.Empty
                'If Not IsDBNull(foundRows(0)("AccountTimeOffPolicyId")) Then
                '    AccountTimeOffPolicyId = foundRows(0)("AccountTimeOffPolicyId")
                'End If
                ''My.Forms.Main.History("AccountTimeOffPolicyId: " + AccountTimeOffPolicyId.ToString, "n")

                'Dim TimeOffApprovalTypeId As Integer = 0
                'If Not IsDBNull(foundRows(0)("TimeOffApprovalTypeId")) Then
                '    TimeOffApprovalTypeId = foundRows(0)("TimeOffApprovalTypeId")
                'End If
                ''My.Forms.Main.History("TimeOffApprovalTypeId: " + TimeOffApprovalTypeId.ToString, "n")

                'Dim AccountHolidayTypeId As Guid = System.Guid.Empty
                'If Not IsDBNull(foundRows(0)("AccountHolidayTypeId")) Then
                '    AccountHolidayTypeId = foundRows(0)("AccountHolidayTypeId")
                'End If
                ''My.Forms.Main.History("AccountHolidayTypeId: " + AccountHolidayTypeId.ToString, "n")

                'Dim IsForcePasswordChange As Boolean = False
                'If Not IsDBNull(foundRows(0)("IsForcePasswordChange")) Then
                '    IsForcePasswordChange = foundRows(0)("IsForcePasswordChange")
                'End If
                ''My.Forms.Main.History("IsForcePasswordChange: " + IsForcePasswordChange.ToString, "n")

            End If
            'if no UI, then skip
            If UI Then
                incrementbar += 1
                My.Forms.MAIN.ProgressBar1.Value = incrementbar
            End If
        Next
        Return NoRecordsCreatedorUpdated
    End Function

    ''' <summary>
    ''' 'Checks if QBID is in sync database
    ''' </summary>
    ''' <param name="myqbID"></param>
    ''' <returns></returns>
    Private Function ISQBID_In_DataTable(ByVal myqbName As String, ByVal myqbID As String) As Int16
        Dim EmployeeAdapter As New QB_TL_IDsTableAdapters.EmployeesTableAdapter()
        Dim TimeLiveIDs As QB_TL_IDs.EmployeesDataTable = EmployeeAdapter.GetCorrespondingTL_ID(myqbID)

        Dim result As Int16 = Math.Min(TimeLiveIDs.Count, 2)

        Dim recordsFoundStr As String = ""
        Select Case result
            Case 0
                recordsFoundStr = "No records"
            Case 1
                recordsFoundStr = "One records"
            Case 2
                recordsFoundStr = "More than one record"
        End Select

        My.Forms.MAIN.History(recordsFoundStr + " found in local database for: " + myqbName, "i")

        Return result
    End Function



    ''' <summary>
    ''' Check if QBID is in sync database and if it has a corresponding TLID
    ''' </summary>
    ''' <param name="myqbID"></param>
    ''' <returns>
    ''' 0 if not found
    ''' 1 if found
    ''' 2 if more than one is found
    ''' </returns>
    Public Function ISTLID_In_DataTable(ByVal myqbID As String) As String
        Dim result As String = Nothing
        Dim EmployeeAdapter As New QB_TL_IDsTableAdapters.EmployeesTableAdapter()
        Dim TimeLiveIDs As QB_TL_IDs.EmployeesDataTable = EmployeeAdapter.GetCorrespondingTL_ID(myqbID)

        If TimeLiveIDs.Rows.Count = 0 Then Return Nothing

        If String.IsNullOrEmpty(Trim(TimeLiveIDs(0).TimeLive_ID.ToString())) Then
            My.Forms.MAIN.History("Record has a TLID of Nothing", "I")
        Else
            My.Forms.MAIN.History("Record has a TLID of: " + TimeLiveIDs(0).TimeLive_ID.ToString(), "i")
            result = Trim(TimeLiveIDs(0).TimeLive_ID.ToString())
        End If

        Return result
    End Function

    Public Function ModifyEmployee(employeeName As String)
        'find employee name in QB
        Dim employeeFullName As String = MAIN.ShowNamesWithoutComma(employeeName)

        Dim msgSetRq As IMsgSetRequest = MAIN.SESSMANAGER.CreateMsgSetRequest("US", 2, 0)
        msgSetRq.Attributes.OnError = ENRqOnError.roeContinue

        Dim EmployeeQueryRq As IEmployeeQuery = msgSetRq.AppendEmployeeQueryRq
        EmployeeQueryRq.ORListQuery.FullNameList.Add(employeeFullName)
        'sessManager.OpenConnection("App", "TimeLive Quickbooks")
        'sessManager.BeginSession("", ENOpenMode.omDontCare)
        Dim msgSetRs As IMsgSetResponse = MAIN.SESSMANAGER.DoRequests(msgSetRq)

        Dim response As IResponse = msgSetRs.ResponseList.GetAt(0)
        Dim empRetList As IEmployeeRetList = response.Detail

        Dim empRet As IEmployeeRet = empRetList.GetAt(0)


        Dim EmailAddress As String
        Dim FirstName As String
        Dim LastName As String
        Dim HiredDate As String
        Dim ModTime As String
        Dim CreateTime As String
        Dim NewlyAdd As String
        Dim EditSequence As String

        With empRet
            EmailAddress = If(.Email Is Nothing, "", .Email.GetValue)
            FirstName = If(.FirstName Is Nothing, "", .FirstName.GetValue)
            LastName = If(.LastName Is Nothing, "", .LastName.GetValue)
            HiredDate = If(.HiredDate Is Nothing, "", .HiredDate.GetValue)
            CreateTime = If(.TimeCreated Is Nothing, "", .TimeCreated.GetValue.ToString)
            ModTime = If(.TimeModified Is Nothing, CreateTime, .TimeModified.GetValue.ToString)
        End With

        Dim employee As QBtoTL_Employee.Employee = New QBtoTL_Employee.Employee(NewlyAdd, empRet.Name.GetValue, EmailAddress, empRet.ListID.GetValue, FirstName, LastName, HiredDate, ModTime, CreateTime, empRet.IsActive.GetValue)
        employee.EditSequence = empRet.EditSequence.GetValue

        MAIN.Get_Employee_Form(employee)

        Try
            Dim newMsgSetRq As IMsgSetRequest = MAIN.SESSMANAGER.CreateMsgSetRequest("US", 2, 0)
            Dim employeeMod As IEmployeeMod = newMsgSetRq.AppendEmployeeModRq

            employeeMod.FirstName.SetValue(employee.FirstName)
            employeeMod.LastName.SetValue(employee.LastName)
            employeeMod.ListID.SetValue(employee.QB_ID)
            employeeMod.EditSequence.SetValue(employee.EditSequence)

            msgSetRq = MAIN.SESSMANAGER.DoRequests(newMsgSetRq)
            Dim res As IResponse = msgSetRq.ResponseList.GetAt(0)
        Catch ex As Exception

        End Try

    End Function


    Public Function SetLength(ByVal str As String) As String
        Return str.Substring(0, Math.Min(50, str.Length))
    End Function

    Public Function CreatePassword(Date_string As String) As String
        Dim Date_array() As String = Date_string.Split("/")
        If Date_array.Length < 3 Then
            Return Date_string
        End If

        Return Date_array(1) + MonthName(Date_array(0)) + Date_array(2)
    End Function


    Public Shared Function GetValue(Value As String, ColumnName As String) As Object
        If ColumnName = "HiredDate" Then
            Return If(Value Is Nothing Or Value = "", Now.Date, Value)
        Else
            Return GetEmployeeValue(Value, ColumnName)
        End If
    End Function

    Public Shared Function GetEmployeeValue(Value As String, ColumnName As String)
        Dim EmployeeName() As String = If(Value.Contains(","), Value.Split(","), Value.Split(" "))
        If EmployeeName.Length = 2 Then
            If ColumnName = "FirstName" Then
                Return If(Value.Contains(","), Trim(EmployeeName(1)), Trim(EmployeeName(0)))
            ElseIf ColumnName = "LastName" Then
                Return If(Value.Contains(","), Trim(EmployeeName(0)), Trim(EmployeeName(1)))
            End If
        ElseIf EmployeeName.Length = 1 Then
            If ColumnName = "FirstName" Then
                Return EmployeeName(0)
            ElseIf ColumnName = "LastName" Then
                Return EmployeeName(0)
            End If
        ElseIf EmployeeName.Length = 3 And Not Value.Contains(",") Then
            If ColumnName = "FirstName" Then
                Return EmployeeName(0)
            ElseIf ColumnName = "LastName" Then
                Return EmployeeName(2)
            End If
        End If
        Return EmployeeName(0) ' Should never get here
    End Function

    Public Shared Function GetEmailAddress(EmailAddress As String, p_token As String, default_addr As String) As String
        ' Return default if email address is empty of nothing

        ' Connect to TimeLive
        Dim objEmployeeServices As Services.TimeLive.Employees.Employees = MAIN.connect_TL_employees(p_token)

        ' For when more than one employee has the same last name and first letter of first name
        Dim counter As Integer = 0
        If objEmployeeServices.IsEmployeeExistsByEmailAddress(default_addr + "@teltrium.com") Then
            default_addr += CStr(counter)
            While objEmployeeServices.IsEmployeeExistsByEmailAddress(default_addr + "@teltrium.com")
                default_addr.Remove(default_addr.Length - 1, 1)
                default_addr += CStr(counter)
                counter += 1
            End While
        End If
        default_addr += "@teltrium.com"

        If EmailAddress Is Nothing Or EmailAddress = "" Then
            Return default_addr
        End If

        Return If(objEmployeeServices.IsEmployeeExistsByEmailAddress(EmailAddress), EmailAddress, default_addr)
    End Function
End Class


