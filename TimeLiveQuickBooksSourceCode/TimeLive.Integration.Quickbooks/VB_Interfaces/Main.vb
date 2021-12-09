Imports System.Windows.Forms
Imports System.Threading
Imports QBFC13Lib

Imports System.Data.SqlClient
Imports System.Text
Imports System.Globalization

Imports System.Net.Mail
Public Class MAIN

    Public Shared p_token As String
    Private p_AccountId As String
    <ThreadStatic> Public Shared SESSMANAGER As QBSessionManager
    Public Shared colonReplacer = " --> "
    Public Shared showNamesWithComma As Boolean = True
    Public Shared TIMERTHREAD As Threading.Thread

    Private LoggedIn As Boolean
    Private Type As Integer
    Private emailBody As String

    Dim customer_qbtotl As QBtoTL_Customer = New QBtoTL_Customer
    Dim customerData As New QBtoTL_Customer.CustomerDataStructureQB
    Dim customer_TLSync As Sync_TLtoQB_Customer = New Sync_TLtoQB_Customer

    Dim job_qbtotl As QBtoTL_JobOrItem = New QBtoTL_JobOrItem
    Dim JobData As New QBtoTL_JobOrItem.JobDataStructureQB
    Dim job_TLSync As Sync_TLtoQB_JoborItem = New Sync_TLtoQB_JoborItem

    Dim employee_qbtotl As QBtoTL_Employee = New QBtoTL_Employee
    Dim employeeData As New QBtoTL_Employee.EmployeeDataStructureQB
    Dim employee_TLSync As Sync_TLtoQB_Employee = New Sync_TLtoQB_Employee

    Dim vendor_qbtotl As QBtoTL_Vendor = New QBtoTL_Vendor
    Dim vendorData As New QBtoTL_Vendor.VendorDataStructureQB
    Dim vendor_TLSync As Sync_TLtoQB_Vendor = New Sync_TLtoQB_Vendor

    Dim timeentry_tltoqb As TLtoQB_TimeEntry = New TLtoQB_TimeEntry
    Dim TimeEntryData As New TLtoQB_TimeEntry.TimeEntryDataStructureQB

    Dim expensereport_tltoqb As TLtoQB_ExpenseReports = New TLtoQB_ExpenseReports
    Dim expenseReportData As New TLtoQB_ExpenseReports.ExpenseReportDataStructureQB

    Dim selectedEmployeeData As New TLtoQB_TimeEntry.EmployeeDataStructure
    Dim selectedExpenseSheetData As New TLtoQB_ExpenseReports.ExpenseSheetDataStructure

    'Dim UIRelatedFunctions As IntegratedUIFuntions = New IntegratedUIFuntions
    Private Sub PARENT_LOAD(SENDER As System.Object, E As System.EventArgs) Handles MyBase.Load
        Try
            VALIDATEQBSESSION()
            Me.Show()
            ' Only show StatusWindow when in Debug Mode
            If My.Settings.DebugMode IsNot Nothing And Not My.Settings.DebugMode = "" Then
                SplitContainer2.Panel2Collapsed = Not Convert.ToBoolean(My.Settings.DebugMode)
            End If

            History("Welcome to QB2TL Sync by Telrium", "n")



            Using newForm = New Login()
                If DialogResult.OK = newForm.ShowDialog() Then
                    p_token = newForm.ReturnValue1
                    p_AccountId = newForm.ReturnValue2
                    LoggedIn = True
                    History("You are logged into TimeLive", "n")
                Else
                    LoggedIn = False
                    History("You will need to log into TimeLive before using this utility.", "n")
                End If
            End Using

            Dim ChargingRelationship As New ChargingRelationship

            'SPIN OFF THREAD FOR TIMER
            TIMERTHREAD_func()
            Dim NextRunDateTime As Date = Convert.ToDateTime(My.Settings.AutoRunTime)
            NextProcessingTime.Text = "Auto Processing Time: " + NextRunDateTime.ToString("MM/dd/yy HH:mm")

            Type = 10

            Dim ReadItems As Integer = 0
            'hide all tabstrips
            TabPageCustomers.Visible = False
            TabPageEmployees.Visible = False
            TabPageTimeTransfer.Visible = False

            Show()
            DataGridView1.AutoSize = False
            DataGridView1.AutoSizeRowsMode = False
            DataGridView2.AutoSize = False
            DataGridView2.AutoSizeRowsMode = False
            time_btn_currentweek_Click(SENDER, E)
            expense_btn_currentweek_Click(SENDER, E)


            'for type Customers, Employees, Vendors, Jobs/Subjobs, and Items/Subitems
            ReadItems = display_UI()

            If LoggedIn Then
                History(ReadItems.ToString() + " items were read from Quickbooks", "n")
            End If

        Catch EX As Exception
            MsgBox(EX.Message)
            Exitbtn_Click() ' Close
        End Try
    End Sub

    Private Sub handleTag(sender As Object, e As EventArgs) Handles AttributeTabControl.SelectedIndexChanged
        Dim ReadItems As Integer = 0

        Select Case AttributeTabControl.SelectedIndex
            ' Customers
            Case 0
                If Type = 10 Then Exit Sub
                Type = 10
            ' Employees
            Case 1
                If Type = 11 Then Exit Sub
                Type = 11
            ' Vendors
            Case 2
                If Type = 12 Then Exit Sub
                Type = 12
            ' Jobs / Items
            Case 3
                If My.Settings.JobORItemHierarchy Is Nothing Or My.Settings.JobORItemHierarchy = "" Then
                    My.Settings.JobORItemHierarchy = 0
                End If

                If My.Settings.JobORItemHierarchy Then
                    If Type = 14 Then Exit Sub
                    Type = 14
                Else
                    If Type = 13 Then Exit Sub
                    Type = 13
                End If
            ' Time Entries
            Case 4
                If Type = 20 Then Exit Sub
                Type = 20
            ' Expense Reports
            Case 5
                If Type = 21 Then Exit Sub
                Type = 21
        End Select

        'for type Customers, Employees, Vendors, Jobs/Subjobs, and Items/Subitems
        My.Forms.MAIN.History("Refreshing", "n")
        ReadItems = display_UI()
    End Sub

    Public Sub TIMERTHREAD_func()
        TIMERTHREAD = New Threading.Thread(AddressOf TIMERMULTITHREADING)
        TIMERTHREAD.Start()
    End Sub

    Public Sub TIMERMULTITHREADING()
        While True
            SETTEXT()
            Thread.Sleep(61000)
        End While
    End Sub


    Delegate Sub StatusWindowCALLBACK(ByVal TEXT As String)
    Private Sub SETTEXT()
        If StatusStrip.InvokeRequired Then
            Dim D As StatusWindowCALLBACK = New StatusWindowCALLBACK(AddressOf SETTEXT)
            Me.Invoke(D, New Object() {Text})
        Else
            Dim AutoRundt As Date = Convert.ToDateTime(My.Settings.AutoRunTime)
            AutoRundt = AutoRundt.AddSeconds(-AutoRundt.Second)
            Dim Currentdt As Date = DateTime.Now
            Currentdt = Currentdt.AddSeconds(-Currentdt.Second)
            Dim result As Integer = String.Compare(AutoRundt.ToString(), Currentdt.ToString())

            If result = 0 Then
                History("Executing selected sync processes: " + Currentdt.ToString(), "n")

                'Execution code goes here
                Dim ItemsProcessed As Integer = AutoExecute()
                History(ItemsProcessed.ToString() + " TimeLive record(s) was created or updated", "N")

                Dim NextRunDateTime As Date = Convert.ToDateTime(My.Settings.AutoRunTime)
                NextRunDateTime = NextRunDateTime.AddHours(Convert.ToInt16(My.Settings.AutoRunInterval))
                My.Settings.AutoRunTime = NextRunDateTime
                My.Settings.Save()

                NextProcessingTime.Text = "Auto Processing Time: " + Convert.ToDateTime(My.Settings.AutoRunTime)
            End If
            CurrentTime.Text = "Time: " + DateTime.Now.ToString("HH:mm")
        End If
    End Sub


    Public Sub VALIDATEQBSESSION()
        Try
            SESSMANAGER = New QBSessionManager()
            SESSMANAGER.OpenConnection("App", "Timelive Quickbooks")
            SESSMANAGER.BeginSession("", ENOpenMode.omDontCare)
        Catch EX As Exception
            Throw New Exception("QUICKBOOKS IS NOT OPEN. PLEASE OPEN IT AND THEN TRY AGAIN.")
        End Try
    End Sub

    Public Sub QUITQBSESSION()
        ' close the session manager if it is open
        Dim val = SESSMANAGER.ConnectionType
        If MAIN.SESSMANAGER IsNot Nothing Then
            MAIN.SESSMANAGER.EndSession()
            MAIN.SESSMANAGER.CloseConnection()
        End If
    End Sub

    Public Sub TIMERTHREADSESSION()
        If TIMERTHREAD IsNot Nothing Then
            TIMERTHREAD.Abort()
        End If
    End Sub

    Public Shared Function connect_TL_employees(ByVal p_token As String) As Services.TimeLive.Employees.Employees
        Dim objEmployeeServices As New Services.TimeLive.Employees.Employees
        Dim authentication As New Services.TimeLive.Employees.SecuredWebServiceHeader
        authentication.AuthenticatedToken = p_token
        objEmployeeServices.SecuredWebServiceHeaderValue = authentication

        Return objEmployeeServices
    End Function

    Public Shared Function connect_TL_clients(ByVal p_token As String) As Services.TimeLive.Clients.Clients
        Dim objClientServices As New Services.TimeLive.Clients.Clients
        Dim authentication As New Services.TimeLive.Clients.SecuredWebServiceHeader
        authentication.AuthenticatedToken = p_token
        objClientServices.SecuredWebServiceHeaderValue = authentication

        Return objClientServices
    End Function

    Public Shared Function connect_TL_projects(ByVal p_token As String) As Services.TimeLive.Projects.Projects
        Dim objProjectServices As New Services.TimeLive.Projects.Projects
        Dim authentication As New Services.TimeLive.Projects.SecuredWebServiceHeader
        authentication.AuthenticatedToken = p_token
        objProjectServices.SecuredWebServiceHeaderValue = authentication

        Return objProjectServices
    End Function

    Public Shared Function connect_TL_tasks(ByVal p_token As String) As Services.TimeLive.Tasks.Tasks
        Dim objTaskServices As New Services.TimeLive.Tasks.Tasks
        Dim authentication As New Services.TimeLive.Tasks.SecuredWebServiceHeader
        authentication.AuthenticatedToken = p_token
        objTaskServices.SecuredWebServiceHeaderValue = authentication

        Return objTaskServices
    End Function

    Public Shared Function connect_TL_time_entries(ByVal p_token As String) As Services.TimeLive.TimeEntries.TimeEntries
        Dim objTimeTrackingServices As New Services.TimeLive.TimeEntries.TimeEntries
        Dim authentication As New Services.TimeLive.TimeEntries.SecuredWebServiceHeader
        authentication.AuthenticatedToken = p_token
        objTimeTrackingServices.SecuredWebServiceHeaderValue = authentication

        Return objTimeTrackingServices
    End Function

    Public Shared Function connect_TL_expense_reports(ByVal p_token As String) As Services.TimeLive.ExpenseEntries.ExpenseEntries
        Dim objExpenseEntriesServices As New Services.TimeLive.ExpenseEntries.ExpenseEntries
        Dim authentication As New Services.TimeLive.ExpenseEntries.SecuredWebServiceHeader
        authentication.AuthenticatedToken = p_token
        objExpenseEntriesServices.SecuredWebServiceHeaderValue = authentication
        Return objExpenseEntriesServices
    End Function

    Public Shared Function equalNames(ByVal name1 As String, ByVal name2 As String)
        If name1 Is Nothing Or name2 Is Nothing Then
            Return False
        End If

        If name1.Trim = name2.Trim Then
            Return True
        End If

        If name1.Contains(",") Xor name2.Contains(",") Then
            Dim splitCommaName As String() = If(name1.Contains(","), name1.Split(","), name2.Split(","))
            Dim nonCommaName As String = If(name1.Contains(","), name2, name1)
            Return splitCommaName(1).Trim + " " + splitCommaName(0).Trim = nonCommaName.Trim
        End If

        Return False
    End Function

    Public Shared Function commaSeperatedNameFromName(ByVal name As String)
        If name.Contains(",") Then Return name

        Dim commaSepName As String = ""
        Return commaSepName
    End Function

    Public Shared Function getTimeLiveEmployeeIDFromName(ByVal Name As String)
        Dim commaSeperatedName As String
        If Name.Contains(",") Then
            commaSeperatedName = Name
            Name = Name.Split(",")(1) + " " + Name.Split(" ")(0)
        Else
            Dim splitName As String() = Name.Split(" ")
            If splitName.Length = 2 Then
                commaSeperatedName = splitName(1).Trim + ", " + splitName(0).Trim
            Else
                Dim firstMiddle As String = splitName(0).Trim + " " + splitName(1).Trim()
                Dim last As String = splitName(2).Trim
                For i As Integer = 3 To splitName.Length - 1
                    last += " " + splitName(i).Trim
                Next

                commaSeperatedName = last + ", " + firstMiddle
            End If
        End If

        Dim emplAdapter As New QB_TL_IDsTableAdapters.EmployeesTableAdapter
        Dim empID As String = emplAdapter.GetCorrespondingTL_IDbyTL_Name(commaSeperatedName)
        If empID Is Nothing Then
            empID = emplAdapter.GetCorrespondingTL_IDbyTL_Name(Name)
        End If

        ' Check if the employee is actually a vendor
        If empID Is Nothing Then
            Dim vendAdapter As New QB_TL_IDsTableAdapters.VendorsTableAdapter
            empID = vendAdapter.GetCorrespondingTL_IDbyTL_Name(commaSeperatedName)
            If empID Is Nothing Then
                empID = vendAdapter.GetCorrespondingTL_IDbyTL_Name(Name)
            End If
        End If

        Return empID
    End Function

    ''' <summary>
    ''' Get the email of an employee based on their id, using TimeLive Web Services
    ''' </summary>
    ''' <param name="EmployeeId">ID in TimeLive of the desired Employee</param>
    ''' <returns></returns>
    Protected Shared Function GetEmailFromTLID(EmployeeId As String) As String
        ' Connect to TimeLive employees
        Dim objEmployeeServices As Services.TimeLive.Employees.Employees = connect_TL_employees(MAIN.p_token)
        Dim employees = objEmployeeServices.GetEmployees()

        For Each employee As Services.TimeLive.Employees.Employee In employees
            If employee.EmployeeId = EmployeeId Then
                Return employee.EmailAddress
            End If
        Next

        Dim emplAdapter As New QB_TL_IDsTableAdapters.EmployeesTableAdapter
        Dim employeeName As String = emplAdapter.GetNamefromTLID(EmployeeId)
        My.Forms.MAIN.History("Could not find the Email email address of " + employeeName, "N")
        Return Nothing
    End Function

    ''' <summary>
    ''' Get the ID of the manager of an employee based on their id, using TimeLive Web Services
    ''' </summary>
    ''' <param name="EmployeeId">ID in TimeLive of the desired Employee</param>
    ''' <returns></returns>
    Public Shared Function GetManagerTLID(EmployeeId As String) As String
        ' Connect to TimeLive employees
        Dim objEmployeeServices As Services.TimeLive.Employees.Employees = connect_TL_employees(MAIN.p_token)

        Dim employees As DataTable = objEmployeeServices.GetEmployeesData
        Dim search = "AccountEmployeeId = " + EmployeeId
        Dim view As DataView = New DataView(employees, search, "", DataViewRowState.CurrentRows)

        If view.Count > 0 Then
            Dim row As DataRow = view.Item(0).Row
            Dim managerLocation As Integer = 42
            Dim managerID As String = ""
            Try
                managerID = row.Item(managerLocation)
            Catch ex As Exception
                Return ""
            End Try
            Return managerID
        End If

        Return ""
    End Function

    Public Shared Sub SendEmployeeGMail(Subject As String, BodyText As String, UI As Boolean, EmployeeId As String)
        Dim EmployeeEmail As String = GetEmailFromTLID(EmployeeId)

        If EmployeeEmail IsNot Nothing Then
            SendGMail(Subject, BodyText, UI, EmployeeEmail)
        End If
    End Sub

    Public Shared Sub SendGMail(Subject As String, BodyText As String, UI As Boolean, Optional Receiver As String = Nothing)
        Dim SendersAddress As String = My.Settings.FromEmailAddress ' senders gmail address "teltrium@gmail.com"
        Dim SendersPassword As String = My.Settings.EmailPassword ' "1October2014"
        Dim ReceiversAddress As String = If(Receiver Is Nothing, My.Settings.ToEmailAddress, Receiver) ' "operations@teltrium.com"

        'Write the contents of your mail
        Try
            Dim smtp As New SmtpClient()
            With smtp
                'used to be Key.Host

                .Host = My.Settings.EmailHost ' "smtp.gmail.com"
                .Port = My.Settings.EmailPort ' 587
                Try
                    .EnableSsl = My.Settings.SSLEncryption
                Catch ex As Exception
                    ' In case My.Settings.SSLEncryption is wrongly formatted
                    .EnableSsl = True
                End Try

                .DeliveryMethod = SmtpDeliveryMethod.Network

                .Credentials = New Net.NetworkCredential(SendersAddress, SendersPassword)
                .Timeout = 3000
            End With
            'MailMessage represents a mail message
            'it is 4 parameters(From,TO,subject,body)
            Dim message As New MailMessage(SendersAddress, ReceiversAddress, Subject, BodyText)
            'WE use smtp sever we specified above to send the message(MailMessage message)

            smtp.Send(message)
            If UI Then
                MsgBox("Sent email to: " + ReceiversAddress + "!")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Public Sub FlagChangedItemsResults(Name As String, result As Integer)
        History("Item: " + Name, "i")
        Dim relationship As String = If(result < 0, "Not modified before last run",
                                        "******* Created or modified after last run *******")
        History(relationship, "c")
    End Sub

    Public Sub History(ByVal input As String, Type As Char) ' Change to Type to "Char" after testing
        Dim can_display As Boolean = If(My.Settings.DebugMode Is Nothing Or My.Settings.DebugMode = "", True, CBool(My.Settings.DebugMode))
        If can_display Then
            Select Case True
                Case Type.Equals("n"c)
                    StatusWindow.Text += vbNewLine + ">> " + input
                Case Type.Equals("N"c)
                    Dim s As String = vbNewLine + "***********************" + vbNewLine + ">> " + input + vbNewLine + "***********************"
                    StatusWindow.Text += s
                Case Type.Equals("c"c) Or Type.Equals("C"c)
                    StatusWindow.Text += ", " + input
                Case Type.Equals("i"c)
                    StatusWindow.Text += vbNewLine + vbTab + "- " + input
                Case Type.Equals("I"c)
                    Dim s As String = vbNewLine + vbTab + "***********************" + vbNewLine + vbTab + "- " + input + vbNewLine + vbTab + "***********************"
                    StatusWindow.Text += s
            End Select

            StatusWindow.SelectionStart = StatusWindow.TextLength
            StatusWindow.ScrollToCaret()
        End If
    End Sub

    Private Function AutoExecute() As Integer
        Dim ItemsProcessed As Integer = 0
        Dim emailBody As String = ""
        If (MAIN.SESSMANAGER Is Nothing) Then
            VALIDATEQBSESSION()
        End If

        ' Customers
        If My.Settings.SyncCustomers Then
            Dim customer_qbtotl As QBtoTL_Customer = New QBtoTL_Customer
            Dim customerData As QBtoTL_Customer.CustomerDataStructureQB
            Dim ItemLastSync As DateTime = Convert.ToDateTime(My.Settings.CustomerLastSync)
            History("Synchonizing modified customers since: " + ItemLastSync.ToString(), "n")
            customerData = customer_qbtotl.GetCustomerQBData(Nothing, False)
            History((customerData.NoItems - customerData.NoInactive).ToString() + " active items were read from Quickbooks", "i")

            For Each element As QBtoTL_Customer.Customer In customerData.DataArray
                Dim result As Integer = DateTime.Compare(Convert.ToDateTime(element.QBModTime.ToString()),
            ItemLastSync)

                If result >= 0 Then
                    element.RecSelect = True
                End If
                If element.Enabled Then ' Note: Not sure if this is entirely right
                    FlagChangedItemsResults(element.QB_Name.ToString(), result)
                End If
            Next

            Dim customersProcessed As Integer = customer_qbtotl.QBTransferCustomerToTL(customerData, p_token, Nothing, False)
            emailBody += "Customers Processed: " + customersProcessed.ToString & vbCrLf
            My.Settings.CustomerLastSync = DateTime.Now.ToString()
            History(customersProcessed.ToString() + " TimeLive customer(s) was created or updated", "i")
            ItemsProcessed += customersProcessed
        End If

        ' Employees
        If My.Settings.SyncEmployees Then
            Dim employee_qbtotl As QBtoTL_Employee = New QBtoTL_Employee
            Dim employeeData As QBtoTL_Employee.EmployeeDataStructureQB

            Dim ItemLastSync As DateTime = Convert.ToDateTime(My.Settings.EmployeeLastSync)
            History("Synchonizing modified employees since: " + ItemLastSync.ToString(), "n")
            employeeData = employee_qbtotl.GetEmployeeQBData(Nothing, False)
            ' Change to "employeeData.NoItems - employeeData.NoInactive" once we begin storing inactive employees
            History(employeeData.NoItems.ToString() + " active items were read from Quickbooks", "i")

            For Each element As QBtoTL_Employee.Employee In employeeData.DataArray
                Dim result As Integer = DateTime.Compare(Convert.ToDateTime(element.QBModTime.ToString()),
            ItemLastSync)

                If result >= 0 Then
                    element.RecSelect = True
                End If
                'If element.Enabled Then ' Note: Will need to check if employee is enabled first once we start tracking inactive employees
                FlagChangedItemsResults(element.QB_Name.ToString(), result)
                'End If
            Next

            Dim employeesProcessed As Integer = employee_qbtotl.QBTransferEmployeeToTL(employeeData, p_token, Nothing, False)
            emailBody += "Employees Processed: " + employeesProcessed.ToString & vbCrLf
            My.Settings.EmployeeLastSync = DateTime.Now.ToString()
            History(employeesProcessed.ToString() + " TimeLive employee(s) was created or updated", "i")
            ItemsProcessed += employeesProcessed
        End If

        ' Vendors/Consultants
        If My.Settings.SyncConsultants Then ' Sync Vendors
            Dim vendor_qbtotl As QBtoTL_Vendor = New QBtoTL_Vendor
            Dim vendorData As QBtoTL_Vendor.VendorDataStructureQB

            Dim ItemLastSync As DateTime = Convert.ToDateTime(My.Settings.VendorLastSync)
            History("Synchonizing modified vendors since: " + ItemLastSync.ToString(), "n")
            vendorData = vendor_qbtotl.GetVendorQBData(Nothing, False)
            ' Change to "employeeData.NoItems - employeeData.NoInactive" if we begin storing inactive vendors
            History(vendorData.NoItems.ToString() + " active items were read from Quickbooks", "i")

            For Each element As QBtoTL_Vendor.Vendor In vendorData.DataArray
                Dim result As Integer = DateTime.Compare(Convert.ToDateTime(element.QBModTime.ToString()),
            ItemLastSync)

                If result >= 0 Then
                    element.RecSelect = True
                End If
                'If element.Enabled Then ' Note: Will need to check if vendor is enabled first if we start tracking inactive vendors
                FlagChangedItemsResults(element.QB_Name.ToString(), result)
                'End If
            Next

            Dim vendorsProcessed As Integer = vendor_qbtotl.QBTransferVendorToTL(vendorData, p_token, Nothing, False)
            emailBody += "Vendors Processed: " + vendorsProcessed.ToString & vbCrLf
            My.Settings.VendorLastSync = DateTime.Now.ToString()
            History(vendorsProcessed.ToString() + " TimeLive employee(s) was created or updated from vendors", "i")
            ItemsProcessed += vendorsProcessed
        End If

        ' Jobs/Subjobs
        If My.Settings.SyncJobs_Items Then
            Dim job_item_qbtotl As QBtoTL_JobOrItem = New QBtoTL_JobOrItem
            Dim job_itemData As QBtoTL_JobOrItem.JobDataStructureQB

            Dim ItemLastSync As DateTime = Convert.ToDateTime(My.Settings.JobLastSync)
            History("Synchonizing modified jobs since: " + ItemLastSync.ToString(), "n")
            If True Then
                job_itemData = job_item_qbtotl.GetJobSubJobData(Nothing, "", False)
            Else
                job_itemData = job_item_qbtotl.GetItemSubItemData(Nothing, "", False)
            End If

            ' Change to "employeeData.NoItems - employeeData.NoInactive" if we begin storing inactive vendors
            History(job_itemData.NoItems.ToString() + " active items were read from Quickbooks", "i")

            For Each element As QBtoTL_JobOrItem.Job_Subjob In job_itemData.DataArray ' Or should this be job_item?
                Dim result As Integer = DateTime.Compare(Convert.ToDateTime(element.QBModTime.ToString()),
            ItemLastSync)

                If result >= 0 Then
                    element.RecSelect = True
                End If
                'If element.Enabled Then ' Note: Will need to check if vendor is enabled first if we start tracking inactive vendors
                FlagChangedItemsResults(element.QB_Name.ToString(), result)
                'End If
            Next

            Dim jobsProcessed As Integer = job_item_qbtotl.QBTransferJobstoTL(job_itemData, p_token, Nothing, False)
            If True Then
                My.Settings.JobLastSync = DateTime.Now.ToString()
            Else
                My.Settings.ItemlastSync = DateTime.Now.ToString()
            End If

            emailBody += "Jobs Processed: " + jobsProcessed.ToString & vbCrLf
            History(jobsProcessed.ToString() + " TimeLive project(s)/task(s) was created or updated from jobs", "i")
            ItemsProcessed += jobsProcessed
        End If

        emailBody += "Total items Processed: " + ItemsProcessed.ToString
        MAIN.SendGMail("QuickBooks to TimeLive Auto transfer", emailBody, False)
        My.Settings.Save()

        Return ItemsProcessed
    End Function

    Private Sub MAIN_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        My.Forms.MAIN.QUITQBSESSION() ' Close QB session before exiting
        My.Forms.MAIN.TIMERTHREADSESSION() ' Close TimerThread session before exiting
        Me.Dispose()
    End Sub

    Private Sub QBTime2SQLbtn_Click(sender As Object, e As EventArgs)
        Dim CurrentSystemSync As New CurrentSystemSync
        CurrentSystemSync.GetTime()
    End Sub

    Private Function GetTotalHours(emplID As String, TLTimeTracker As Services.TimeLive.TimeEntries.TimeEntries,
                                  startDate As DateTime, endDate As DateTime) As Double
        Dim times() As Object = TLTimeTracker.GetTimeEntriesByEmployeeIdAndDateRange(emplID, startDate, endDate)
        Dim totalHours As Double = 0
        Dim time
        For Each time In times
            totalHours += TotalTimeToHours(time.TotalTime)
        Next

        Return totalHours
    End Function

    Private Sub Time_Entry_Times()
        DataGridView2.AutoSize = False
        DataGridView2.AutoSizeRowsMode = False

        DataGridView2.Rows.Clear()
        DataGridView2.Columns.Clear()

        ShowEntitiesBtn.Visible = True
        ShowEntitiesBtn.Text = "Show Times"
        SendEmailsButton.Visible = True

        ' load grid 2
        Dim col1 As New DataGridViewCheckBoxColumn
        col1.Name = "ckBox"
        col1.HeaderText = "Check Box"
        DataGridView2.Columns.Add(col1)

        Dim colNames As String() = {"Employee", "Date", "Task", "Time", "Class", "Payroll Item", "Item SubItem"}
        For Each colName As String In colNames
            Dim col As New DataGridViewTextBoxColumn
            col.Name = colName
            DataGridView2.Columns.Add(col)
        Next
    End Sub

    Private Function time_entry_row(ByVal TL_TimeEntries As TimeLiveDataSetTableAdapters.AccountEmployeeTimeEntryPeriodTableAdapter,
                                    ByVal objTimeTrackingServices As Services.TimeLive.TimeEntries.TimeEntries, ByVal Row As DataRow)
        Dim emplID = Row("AccountEmployeeId")
        Dim emplName = Row("FullName")

        Dim hoursWorked As Double = GetTotalHours(emplID, objTimeTrackingServices, dpStartDate.Value, dpEndDate.Value)
        selectedEmployeeData.DataArray.Add(New TLtoQB_TimeEntry.Employee(True, Row("FullName"), emplID, hoursWorked))

        Dim datagrid_row As DataGridViewRow = New DataGridViewRow()
        datagrid_row.CreateCells(DataGridView1)
        datagrid_row.SetValues(True, emplName, hoursWorked)

        ' Change color of the row based on submission status
        If TL_TimeEntries.GetTotalNumRejectedEntries(emplID, dpStartDate.Value, dpEndDate.Value) Then
            datagrid_row.DefaultCellStyle.BackColor = Color.Red
        ElseIf TL_TimeEntries.GetTotalNumUnsubmittedEntries(emplID, dpStartDate.Value, dpEndDate.Value) Then
            datagrid_row.DefaultCellStyle.BackColor = Color.DarkGray
        ElseIf TL_TimeEntries.GetTotalNumUnapprovedEntries(emplID, dpStartDate.Value, dpEndDate.Value) Then
            datagrid_row.DefaultCellStyle.BackColor = Color.LightGray
        ElseIf TL_TimeEntries.GetTotalNumEntries(emplID, dpStartDate.Value, dpEndDate.Value) Then
            datagrid_row.DefaultCellStyle.BackColor = Color.LightSteelBlue
        End If

        Return datagrid_row
    End Function

    Private Function GetNumExpenseReports(expenseSheetId As Guid, TLExpenseTracker As Services.TimeLive.ExpenseEntries.ExpenseEntries) As Integer
        Dim reports() As Object = TLExpenseTracker.GetExpenseEntriesByExpenseSheetIdForMobile(expenseSheetId)
        Return reports.Length
    End Function

    Private Sub Expense_Reports()
        DataGridView2.AutoSize = False
        DataGridView2.AutoSizeRowsMode = False

        DataGridView2.Rows.Clear()
        DataGridView2.Columns.Clear()

        ShowEntitiesBtn.Visible = True
        ShowEntitiesBtn.Text = "Show Expenses"
        ' load grid 2
        Dim col1 As New DataGridViewCheckBoxColumn
        col1.Name = "ckBox"
        col1.HeaderText = "Check Box"
        DataGridView2.Columns.Add(col1)

        Dim colNames As String() = {"Entry ID", "Employee", "Expense", "Date", "Project", "Amount", "Expense Sheet ID"}
        For Each colName As String In colNames
            Dim col As New DataGridViewTextBoxColumn
            col.Name = colName
            If colName = "Entry ID" Then col.Visible = False
            DataGridView2.Columns.Add(col)
        Next
    End Sub

    Private Function expense_report_row(ByVal Row As DataRow, ByVal TL_ExpenseEntries As TimeLiveDataSetTableAdapters.AccountExpenseEntryTableAdapter,
                                        ByVal TL_ExpenseSheets As TimeLiveDataSetTableAdapters.AccountEmployeeExpenseSheetTableAdapter,
                                        ByVal employeesDB As QB_TL_IDsTableAdapters.EmployeesTableAdapter, ByVal vendorsDB As QB_TL_IDsTableAdapters.VendorsTableAdapter)
        Dim expenseSheetId As Guid = Row("AccountEmployeeExpenseSheetId")
        Dim employeeName As String = employeesDB.GetNamefromTLID(Row("AccountEmployeeId"))
        If employeeName Is Nothing Then employeeName = vendorsDB.getNamefromTLID(Row("AccountEmployeeId"))
        If employeeName IsNot Nothing Then employeeName = employeeName.Trim
        Dim sheetDate As Date = Row("ExpenseSheetDate")
        Dim Description As String = Row("Description")
        Dim numEntries As Integer = TL_ExpenseEntries.getEntriesOfExpenseSheet(Row("AccountEmployeeExpenseSheetId")).Count
        selectedExpenseSheetData.add(New TLtoQB_ExpenseReports.ExpenseSheet(expenseSheetId, employeeName, sheetDate, Description, numEntries))
        Dim datagrid_row As DataGridViewRow = New DataGridViewRow()
        datagrid_row.CreateCells(DataGridView1)
        datagrid_row.SetValues(True, expenseSheetId, employeeName, Description, sheetDate, numEntries)
        Dim isApproved As Boolean = TL_ExpenseSheets.isApproved(expenseSheetId)
        Dim isSubmitted As Boolean = TL_ExpenseSheets.isSubmitted(expenseSheetId)
        Dim isRejected As Boolean = TL_ExpenseSheets.isRejected(expenseSheetId)

        If isRejected Then
            datagrid_row.DefaultCellStyle.BackColor = Color.Red
        ElseIf Not isSubmitted Then
            datagrid_row.DefaultCellStyle.BackColor = Color.DarkGray
        ElseIf Not isApproved Then
            datagrid_row.DefaultCellStyle.BackColor = Color.LightGray
        End If
        Return datagrid_row
    End Function

    Private Sub clear_grids()
        Try
            DataGridView1.Rows.Clear()
        Catch nullEx As NullReferenceException
            ' Do Nothing - just don't throw the exception
        End Try
        While DataGridView1.ColumnCount > 1
            Try
                DataGridView1.Columns.RemoveAt(1)
            Catch nullEx As NullReferenceException
                ' Do nothing - just don't throw the exception
            End Try
        End While

        Try
            DataGridView2.Rows.Clear()
            DataGridView2.Columns.Clear()
        Catch nullEx As NullReferenceException
            DataGridView2 = New DataGridView()
        End Try
    End Sub

    Private Function replaceColonsAndRemoveUnwantedSpaces(ByVal name As String)
        Sync_TLtoQB_JoborItem.removeSpacesBetweenColonsAndSetLengthOfFields(name)
        Return name.Replace(":", MAIN.colonReplacer)
    End Function

    Private Function display_ExpenseEntries_UI() Handles refreshExpenseReport.Click
        If Not LoggedIn Then
            Return 0
        End If

        clear_grids()
        TabPageExpenseReport.Visible = True
        AttributeTabControl.SelectedIndex = 5
        SyncFromLabel.Text = "Sheets"
        SyncToLabel.Text = "Entries"

        Dim colNames As String() = {"Sheet ID", "Employee", "Description", "Date", "Entries"}
        For Each colName As String In colNames
            Dim col As New DataGridViewTextBoxColumn
            col.Name = colName
            DataGridView1.Columns.Add(col)
            If colName = "Sheet ID" Then col.Visible = False
        Next

        SelectAllCheckBox.Checked = True

        ' TODO: Have Expense Report Last Sync
        ' Dim lastSync As DateTime = If(String.IsNullOrEmpty(My.Settings.TimeTrackingLastSync.ToString()), #1/1/2000#, Convert.ToDateTime(My.Settings.TimeTrackingLastSync))
        ' History("Synchonizing expense reports since: " + lastSync.ToString(), "n")
        selectedExpenseSheetData.clear()

        Dim objEmployeeServices As Services.TimeLive.Employees.Employees = connect_TL_employees(p_token)
        Dim employeesDB As New QB_TL_IDsTableAdapters.EmployeesTableAdapter()
        Dim vendorsDB As New QB_TL_IDsTableAdapters.VendorsTableAdapter()

        Dim TL_ExpenseEntries As New TimeLiveDataSetTableAdapters.AccountExpenseEntryTableAdapter
        Dim TL_ExpenseSheets As New TimeLiveDataSetTableAdapters.AccountEmployeeExpenseSheetTableAdapter
        Dim expenseSheets As DataTable = TL_ExpenseSheets.getExpenseSheetsWithinDateRange(expenseReportStartDate.Value, expenseReportEndDate.Value)

        ProgressBar1.Maximum = expenseSheets.Rows.Count
        ProgressBar1.Value = 0

        For Each row As DataRow In expenseSheets.Rows
            Dim datagrid_row As DataGridViewRow = expense_report_row(row, TL_ExpenseEntries, TL_ExpenseSheets, employeesDB, vendorsDB)
            DataGridView1.Rows.Add(datagrid_row)
            ProgressBar1.Value += 1
        Next

        Dim items_read As Integer = selectedExpenseSheetData.NumExpenseReports()
        Expense_Reports()

        If ProgressBar1.Maximum = 0 Then
            ProgressBar1.Maximum = 1
            ProgressBar1.Value = 1
        End If

        System.Threading.Thread.Sleep(150)
        System.Windows.Forms.Application.DoEvents()
        ProgressBar1.Value = 0

        Return items_read
    End Function

    Private Function display_TimeEntry_UI() Handles RefreshTimeTransfer.Click
        If Not LoggedIn Then
            Return 0
        End If

        clear_grids()
        TabPageTimeTransfer.Visible = True
        AttributeTabControl.SelectedIndex = 4
        SyncFromLabel.Text = "Employees"
        SyncToLabel.Text = "Time"

        'load grid 1
        Dim colNames As String() = {"Name", "Hours"}
        For Each colName As String In colNames
            Dim col As New DataGridViewTextBoxColumn
            col.Name = colName
            DataGridView1.Columns.Add(col)
        Next

        SelectAllCheckBox.Checked = True
        Dim lastSync As DateTime = If(String.IsNullOrEmpty(My.Settings.TimeTrackingLastSync.ToString()), #1/1/2000#, Convert.ToDateTime(My.Settings.TimeTrackingLastSync))
        History("Synchonizing time entries since: " + lastSync.ToString(), "n")

        ' Add all employees with their total hours worked to selectedEmployeeData only if it is empty
        selectedEmployeeData.NoItems = 0
        selectedEmployeeData.DataArray = New List(Of TLtoQB_TimeEntry.Employee)

        Dim objEmployeeServices As Services.TimeLive.Employees.Employees = connect_TL_employees(p_token)
        Dim objTimeTrackingServices As Services.TimeLive.TimeEntries.TimeEntries = connect_TL_time_entries(p_token)

        Dim employees As DataTable = objEmployeeServices.GetEmployeesData
        Dim TL_TimeEntries As New TimeLiveDataSetTableAdapters.AccountEmployeeTimeEntryPeriodTableAdapter

        ' Populate the table based on the TimeLive elements
        ProgressBar1.Maximum = employees.Rows.Count
        ProgressBar1.Value = 0

        For Each row As DataRow In employees.Rows
            selectedEmployeeData.NoItems += 1
            Dim datagrid_row As DataGridViewRow
            datagrid_row = time_entry_row(TL_TimeEntries, objTimeTrackingServices, row)
            DataGridView1.Rows.Add(datagrid_row)
            ProgressBar1.Value += 1
        Next

        Dim items_read As Integer = selectedEmployeeData.NoItems
        Time_Entry_Times()

        System.Threading.Thread.Sleep(150)
        System.Windows.Forms.Application.DoEvents()
        ProgressBar1.Value = 0

        Return items_read
    End Function

    Private Function loadQBdata(ByVal QBtoTLRadioButton As RadioButton, ByVal Type As Integer, ByVal Data As Object, ByVal attribute As String, ByVal ItemLastSync As DateTime)
        Dim QBDataGridView As DataGridView = If(QBtoTLRadioButton.Checked, DataGridView1, DataGridView2)

        Dim colNamesQB As New List(Of String)(New String() {"Name", "Last Modified", "New"})
        If Type = 13 Or Type = 14 Then colNamesQB.Insert(0, "Full Name")

        For Each colName As String In colNamesQB
            Dim col As New DataGridViewTextBoxColumn
            col.Name = colName
            QBDataGridView.Columns.Add(col)
        Next

        Dim readItems As Integer = 0

        If Data Is Nothing Then
            History("No QuickBooks " + attribute + " data", "n")
        Else
            ' Note: Currently only customer stored both active and inactive; If changed, then change this line accordingly
            readItems = If(Type = 10, Data.NoItems - Data.NoInactive, Data.NoItems)

            For Each element As Object In Data.DataArray
                Dim result As Integer = DateTime.Compare(Convert.ToDateTime(element.QBModTime.ToString()), ItemLastSync)
                If result >= 0 Then
                    element.RecSelect = True
                End If
            Next
            For Each element As Object In Data.DataArray
                ' Do not display rows that are disabled
                If Not element.Enabled Then
                    Continue For
                End If

                ' Jobs/Subjobs and Items/Subitems show full name too
                If Type = 13 Or Type = 14 Then
                    Dim fullName As String = replaceColonsAndRemoveUnwantedSpaces(element.FullName.ToString())
                    If QBtoTLRadioButton.Checked Then
                        QBDataGridView.Rows.Add(element.RecSelect, fullName, element.QB_Name.ToString(), element.QBModTime.ToString(), element.NewlyAdded)
                    Else
                        QBDataGridView.Rows.Add(fullName, element.QB_Name.ToString(), element.QBModTime.ToString(), element.NewlyAdded)
                    End If
                Else
                    Dim name As String = If(Type = 11 Or Type = 12, If(showNamesWithComma, element.LastName + ", " + element.FirstName, element.FirstName + " " + element.LastName), element.QB_Name)
                    If QBtoTLRadioButton.Checked Then
                        QBDataGridView.Rows.Add(element.RecSelect, name, element.QBModTime.ToString(), element.NewlyAdded)
                    Else
                        QBDataGridView.Rows.Add(name, element.QBModTime.ToString(), element.NewlyAdded)
                    End If
                End If
                ProgressBar1.Value += 1
            Next
        End If
        Return readItems
    End Function

    Private Function getNameAndIsNew(ByVal Type As Integer, ByVal element As Object, ByVal objServices As Object, ByVal objServices2 As Object, ByVal datagrid_row As DataGridViewRow,
                                     ByVal CustomerAdapter As QB_TL_IDsTableAdapters.CustomersTableAdapter, ByVal EmployeeAdapter As QB_TL_IDsTableAdapters.EmployeesTableAdapter,
                                     ByVal VendorAdapter As QB_TL_IDsTableAdapters.VendorsTableAdapter, ByVal Job_SubJobAdapter As QB_TL_IDsTableAdapters.Jobs_SubJobsTableAdapter,
                                     ByVal Item_SubItemAdapter As QB_TL_IDsTableAdapters.Items_SubItemsTableAdapter)
        Dim ID As Integer = 0
        Dim name As String = ""
        Dim isNew As String = ""

        Select Case Type
                ' .ClientID always returns 0, so always do: GetClientIdByName(.ClientName)
                ' Customers
            Case 10
                name = element.ClientName
                ID = objServices.GetClientIdByName(name)
                isNew = If(CustomerAdapter.numCustomersWithTL_ID(ID), "", "N")

                ' Employees
            Case 11 ' Will need to change this a little bit
                name = If(showNamesWithComma, element.LastName + ", " + element.FirstName, element.FirstName + " " + element.LastName)
                ID = objServices.GetEmployeeId(element.EmployeeName)
                ' Do not show vendors
                If element.isVendor Then
                    Return Nothing
                End If
                isNew = If(EmployeeAdapter.numEmployeesWithTL_ID(ID), "", "N")

                ' Vendors 
            Case 12
                name = If(showNamesWithComma, element.LastName + ", " + element.FirstName, element.FirstName + " " + element.LastName)
                ID = objServices.GetEmployeeId(element.EmployeeName)
                ' Do not show employees
                If Not element.isVendor Then
                    Return Nothing
                End If
                isNew = If(VendorAdapter.numVendorsWithTL_ID(ID), "", "N")

                ' Jobs/Subjobs
            Case 13
                If element.GetType Is (New Services.TimeLive.Projects.Project).GetType Then
                    name = element.ClientName + MAIN.colonReplacer + element.projectName
                    ID = objServices.GetProjectId(element.projectName)

                    If element.projectName.contains(":") Then ' Case where project name contains name of task as well
                        datagrid_row.DefaultCellStyle.BackColor = Color.LightYellow
                    End If
                Else
                    Dim firstColon As Integer = element.JobParent.indexOf(":")
                    Dim projectName As String = element.JobParent.substring(0, firstColon) + colonReplacer + element.JobParent.substring(firstColon + 1)
                    name = projectName + MAIN.colonReplacer + element.TaskName
                    ' Checks if it is formatted with Task in Project name, and Item in Task name

                    If Sync_TLtoQB_JoborItem.storedAsTaskItem(element) Then
                        datagrid_row.DefaultCellStyle.BackColor = Color.LightYellow
                    Else
                        name = name.Replace(":", MAIN.colonReplacer)
                    End If

                    Try
                        ID = objServices2.GetTaskId(element.TaskName)
                    Catch ex As System.Web.Services.Protocols.SoapException
                        History("Could not get TL ID of TL task '" + name +
                                                  "' Make sure that it has a 'code' attribute in TimeLive", "i")
                        ID = -1
                    End Try
                End If

                ' Do not show Items
                If (Item_SubItemAdapter.numItemsSubItemsWithTL_ID(ID)) Then
                    Return Nothing
                End If
                isNew = If(Job_SubJobAdapter.numTasksSubTasksWithTL_ID(ID), "", "N")

                ' Items/SubItems
            Case 14
                If element.GetType Is (New Services.TimeLive.Projects.Project).GetType Then
                    name = element.ClientName + MAIN.colonReplacer + element.projectName
                    ID = objServices.GetProjectId(element.projectName)
                Else
                    name = element.TaskName
                    ID = objServices2.GetTaskId(element.TaskName)
                End If
                ' Do not show Jobs
                If Job_SubJobAdapter.numTasksSubTasksWithTL_ID(ID) Then
                    Return Nothing
                End If
                isNew = If(Item_SubItemAdapter.numItemsSubItemsWithTL_ID(ID), "", "N")
        End Select

        Return {name, isNew}
    End Function

    Private Function display_UI(Optional sender As Object = Nothing, Optional e As EventArgs = Nothing)
        If Not LoggedIn Then
            Return 0
        End If

        ShowEntitiesBtn.Visible = False
        SendEmailsButton.Visible = False

        ' For QuickBooks Display
        Dim ItemLastSync As DateTime
        Dim lastSync As String = ""
        Dim Data = Nothing
        Dim attribute As String = ""
        Dim QBtoTLRadioButton As RadioButton = Nothing

        'TransferTimeButton.Visible = False
        EntitiesSelectAll.Visible = False

        ' For TimeLive Display
        Dim TLItemsArray() As Object = {}
        Dim objServices = Nothing
        Dim objServices2 = Nothing

        Select Case Type
            ' Time Entries OR Expense Entires
            Case 20
                Return display_TimeEntry_UI()
            Case 21
                Return display_ExpenseEntries_UI()
            ' Customers
            Case 10
                'TimeLive Params
                Dim objClientServices As Services.TimeLive.Clients.Clients = connect_TL_clients(p_token)
                objServices = objClientServices
                TLItemsArray = objServices.GetClients()
                ProgressBar1.Maximum = TLItemsArray.Length
                'QuickBooks Params
                TabPageCustomers.Visible = True
                AttributeTabControl.SelectedIndex = 0
                lastSync = My.Settings.CustomerLastSync
                customerData = customer_qbtotl.GetCustomerQBData(Me, True)
                Data = customerData
                attribute = "customer"
                QBtoTLRadioButton = QBtoTLCustomerRadioButton

            ' Employees
            Case 11
                'TimeLive Params
                Dim objEmployeeServices As Services.TimeLive.Employees.Employees = connect_TL_employees(p_token)
                objServices = objEmployeeServices
                TLItemsArray = objServices.GetEmployees()
                ProgressBar1.Maximum = TLItemsArray.Length
                'QuickBooks Params
                TabPageEmployees.Visible = True
                CustomerSyncDirection.Visible = True
                AttributeTabControl.SelectedIndex = 1
                lastSync = My.Settings.EmployeeLastSync
                employeeData = employee_qbtotl.GetEmployeeQBData(Me, True)
                Data = employeeData
                attribute = "employee"
                QBtoTLRadioButton = QBtoTLEmployeeRadioButton

            ' Vendors
            Case 12
                'TimeLive Params
                Dim objEmployeeServices As Services.TimeLive.Employees.Employees = connect_TL_employees(p_token)
                objServices = objEmployeeServices
                TLItemsArray = objServices.GetEmployees()
                ProgressBar1.Maximum = TLItemsArray.Length
                'QuickBooks Params
                TabPageVendor.Visible = True
                AttributeTabControl.SelectedIndex = 2
                lastSync = My.Settings.VendorLastSync
                vendorData = vendor_qbtotl.GetVendorQBData(Me, True)
                Data = vendorData
                attribute = "vendor"
                QBtoTLRadioButton = QBtoTLVendorRadioButton

            ' Jobs/SubJobs | Items/SubItems (Projects/Tasks in TimeLive)
            Case 13, 14
                'TimeLive Params
                Dim list As New List(Of Object)
                Dim objProjectServices As Services.TimeLive.Projects.Projects = connect_TL_projects(p_token)
                objServices = objProjectServices
                list.AddRange(objServices.GetProjects())
                Dim objTaskServices As Services.TimeLive.Tasks.Tasks = connect_TL_tasks(p_token)
                objServices2 = objTaskServices
                list.AddRange(objServices2.GetTasks())
                TLItemsArray = list.ToArray()
                ProgressBar1.Maximum = TLItemsArray.Length
                'QuickBooks Params
                If Type = 13 Then
                    lastSync = My.Settings.JobLastSync
                    JobData = job_qbtotl.GetJobSubJobData(Me, p_token, True)
                    attribute = "job/subjob"
                Else
                    lastSync = My.Settings.ItemlastSync
                    JobData = job_qbtotl.GetItemSubItemData(Me, p_token, True)
                    attribute = "item/subitem"
                End If
                TabPageJobsItems.Visible = True
                AttributeTabControl.SelectedIndex = 3
                QBtoTLRadioButton = QBtoTLJobItemRadioButton
                Data = JobData
            Case Else
                Return 0
        End Select

        SyncFromLabel.Text = If(Type = 20, "Employees", If(QBtoTLRadioButton.Checked, "QuickBooks", "TimeLive"))
        SyncToLabel.Text = If(Type = 20, "Time", If(QBtoTLRadioButton.Checked, "TimeLive", "QuickBooks"))

        If String.IsNullOrEmpty(lastSync) Then
            ItemLastSync = #1/1/2000#
        Else
            ItemLastSync = Convert.ToDateTime(lastSync)
        End If

        ProgressBar1.Value = 0
        ProgressBar1.Maximum = If(TLItemsArray Is Nothing, 0, TLItemsArray.Length) + Data.DataArray.Count

        History("Synchonizing modified " + attribute + " since: " + ItemLastSync.ToString(), "n")
        clear_grids()
        Dim readItems As Integer = loadQBdata(QBtoTLRadioButton, Type, Data, attribute, ItemLastSync)

        '-----------------------------------------
        ' Load Grid for TimeLive
        '-----------------------------------------
        Dim TLDataGridView As DataGridView = If(QBtoTLRadioButton.Checked, DataGridView2, DataGridView1)

        Dim colNamesTL As String() = {"Name", "New"}
        For Each colName As String In colNamesTL
            Dim col As New DataGridViewTextBoxColumn
            col.Name = colName
            TLDataGridView.Columns.Add(col)
        Next

        If TLItemsArray Is Nothing Or TLItemsArray.Length = 0 Then
            History("No TimeLive " + attribute + " data", "n")
        End If

        Dim CustomerAdapter As New QB_TL_IDsTableAdapters.CustomersTableAdapter()
        Dim EmployeeAdapter As New QB_TL_IDsTableAdapters.EmployeesTableAdapter()
        Dim VendorAdapter As New QB_TL_IDsTableAdapters.VendorsTableAdapter()
        Dim Job_SubJobAdapter As New QB_TL_IDsTableAdapters.Jobs_SubJobsTableAdapter()
        Dim Item_SubItemAdapter As New QB_TL_IDsTableAdapters.Items_SubItemsTableAdapter()

        For Each element As Object In TLItemsArray
            Dim datagrid_row As DataGridViewRow = New DataGridViewRow()
            datagrid_row.CreateCells(TLDataGridView)

            Dim nameAndIsNew = getNameAndIsNew(Type, element, objServices, objServices2, datagrid_row, CustomerAdapter, EmployeeAdapter, VendorAdapter, Job_SubJobAdapter, Item_SubItemAdapter)
            If nameAndIsNew Is Nothing Then Continue For
            Dim name As String = nameAndIsNew(0)
            Dim isNew As String = nameAndIsNew(1)

            ' Checks if TLDataGridView is DataGridView1 or DataGridView2
            If QBtoTLRadioButton.Checked Then
                datagrid_row.SetValues(name, isNew)
            Else
                datagrid_row.SetValues(False, name, isNew)
            End If
            TLDataGridView.Rows.Add(datagrid_row)
            ProgressBar1.Value += 1
        Next

        SelectAllCheckBox.Checked = True

        System.Threading.Thread.Sleep(150)
        System.Windows.Forms.Application.DoEvents()
        ProgressBar1.Value = 0
        Return readItems
    End Function

    Private Function refresh_display_UI(sender As Object, e As EventArgs) Handles RefreshCustomers.Click, RefreshEmployees.Click, RefreshVendors.Click, RefreshJobsOrItems.Click,
                                                                                  QBtoTLCustomerRadioButton.CheckedChanged, QBtoTLEmployeeRadioButton.CheckedChanged,
                                                                                  QBtoTLVendorRadioButton.CheckedChanged, QBtoTLJobItemRadioButton.CheckedChanged
        Try
            My.Forms.MAIN.History("Refreshing", "n")
        Catch ex As Exception
            ' Need this to catch exception on start-up: harmless and does nothing
        End Try

        Return display_UI()
    End Function

    Public Overloads Sub Show(ByVal token As String, ByVal AccountId As String, TypeSelected As Integer)


        History("Welcome to QB2TL Sync by Telrium", "n")
        Using newForm = New Login()
            If DialogResult.OK = newForm.ShowDialog() Then
                p_token = newForm.ReturnValue1
                p_AccountId = newForm.ReturnValue2
                History("You are logged into TimeLive", "n")
                History("", "n")
            Else
                History("You will need to log into TimeLive before using this utility.", "n")
                History("", "n")
            End If
        End Using

        Type = TypeSelected
        MyBase.Show()

        'Change the number to the column index that you want to sort
        '--------   cbWageType 
        If My.Settings.QBClass = "" Then
            cbWageType.SelectedIndex = 0
        Else
            cbWageType.SelectedIndex = My.Settings.QBClass
        End If

        'End If
    End Sub

    ''' <summary>
    ''' Connects to QuickBooks to get a dictionary with key value pairs being {Payroll_Id: Payroll_Name}
    ''' </summary>
    ''' <returns>A Dictionary with keys being Payroll IDs and values being the corresponding Payroll Name</returns>
    Private Function Payroll_IDName_Dict() As Dictionary(Of String, String)
        Dim Payroll_Dict As Dictionary(Of String, String) = New Dictionary(Of String, String)

        Dim chargRel As New ChargingRelationship
        Dim Payroll_DataTable As DataTable = chargRel.QBPayrollItems()

        For Each row As DataRow In Payroll_DataTable.Rows
            Payroll_Dict.Add(row(1), row(0))
        Next

        Return Payroll_Dict
    End Function

    ''' <summary>
    ''' Connects to QuickBooks to get a dictionary with key value pairs being {Item_Id: Item_Name}
    ''' </summary>
    ''' <returns>A Dictionary with keys being Item/SubItem IDs and values being the corresponding Item/SubItem Name</returns>
    Private Function ItemSubItem_IDName_Dict() As Dictionary(Of String, String)
        Dim Item_Dict As Dictionary(Of String, String) = New Dictionary(Of String, String)

        Dim chargRel As New ChargingRelationship
        Dim Item_DataTable As DataTable = chargRel.QBItemsSubItems()

        For Each row As DataRow In Item_DataTable.Rows
            Item_Dict.Add(row(1), row(0))
        Next

        Return Item_Dict
    End Function


    Private Sub btnUpdateEntityTransfer_Click(sender As Object, e As EventArgs) Handles ShowEntitiesBtn.Click
        If Not LoggedIn Then
            Exit Sub
        End If

        EntitiesSelectAll.Visible = True
        EntitiesSelectAll.Checked = True

        If If(My.Settings.DebugMode Is Nothing Or My.Settings.DebugMode = "", False, CBool(My.Settings.DebugMode)) Then
            AppSettings.chk_debugMode.Checked = MsgBox("Printing to the debug window will slow down this operation. Turn Debug Mode off?", MsgBoxStyle.YesNo, "Debug Mode") = MsgBoxResult.No
            My.Settings.DebugMode = Convert.ToString(AppSettings.chk_debugMode.Checked)
            ' Only show StatusWindow when in Debug Mode
            SplitContainer2.Panel2Collapsed = Not Convert.ToBoolean(My.Settings.DebugMode)
        End If

        If Type = 20 Then
            Reset_Checked_SelectedEmployee_Value(selectedEmployeeData)
            Set_Selected_SelectedEmployee()
            btnUpdateTimeTransfer_Click(sender, e)
        ElseIf Type = 21 Then
            Reset_Checked_SelectedExpenseSheets_Value(selectedExpenseSheetData)
            Set_Selected_SelectedExpenseSheet()
            btnUpdateExpenseReport_Click(sender, e)
        End If

        System.Threading.Thread.Sleep(150)
        System.Windows.Forms.Application.DoEvents()

        ProgressBar1.Value = 0
        My.Settings.TimeTrackingLastSync = DateTime.Now.ToString()
        My.Settings.Save()
    End Sub

    ''' <summary>
    ''' Displays the Time Entries for the selected employees within the specified time range
    ''' - White rows correspond to approved time entries
    ''' - light grey rows correspond to non-approved time entries
    ''' - dark grey rows correspond to non-submitted time entries
    ''' - red rows correspond to rejected time entries
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub btnUpdateTimeTransfer_Click(sender As Object, e As EventArgs)
        Time_Entry_Times()
        TimeEntryData.clear()
        Dim startDate As DateTime = dpStartDate.Value.Date
        Dim endDate As DateTime = dpEndDate.Value.Date

        Dim payroll_id_names As Dictionary(Of String, String) = Payroll_IDName_Dict()
        Dim items_id_names As Dictionary(Of String, String) = ItemSubItem_IDName_Dict()
        ProgressBar1.Value = 0
        ProgressBar1.Maximum = selectedEmployeeData.NoItems
        For Each element As TLtoQB_TimeEntry.Employee In selectedEmployeeData.DataArray
            If element.RecSelect = True Then
                History("Processing: " + element.FullName.ToString(), "n")
                LoadSelectedTimeEntryItems(element.AccountEmployeeId, DataGridView2, startDate, endDate, True, payroll_id_names, items_id_names)
                element.RecSelect = False
            End If
            ProgressBar1.Value += 1
        Next
    End Sub

    Private Sub btnUpdateExpenseReport_Click(sender As Object, e As EventArgs)
        Expense_Reports()
        expenseReportData.clear()
        ProgressBar1.Value = 0
        ProgressBar1.Maximum = selectedExpenseSheetData.NumExpenseReports()
        For Each element As TLtoQB_ExpenseReports.ExpenseSheet In selectedExpenseSheetData.DataArray
            With element
                If element.RecSelect = True Then
                    History("Processing Time Sheet for " + element.EmployeeName, "n")
                    LoadSelectedExpenseReportItems(element.SheetId, element.EmployeeName, element.SheetDate, DataGridView2)
                    element.RecSelect = False
                End If
            End With
            ProgressBar1.Value += 1
        Next
    End Sub

    ''' <summary>
    ''' Sends emails to employees that have not submitted time cards, and supervisors that have yet to approve time cards
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub SendEmailsButton_Click(sender As Object, e As EventArgs) Handles SendEmailsButton.Click
        If Not LoggedIn Then
            Exit Sub
        End If

        ' Connect to TimeLive employees
        Dim objEmployeeServices As Services.TimeLive.Employees.Employees = connect_TL_employees(p_token)

        Reset_Checked_SelectedEmployee_Value(selectedEmployeeData)
        Set_Selected_SelectedEmployee()

        Dim EmployeeUnsubmittedDict As New Dictionary(Of String, List(Of Date))
        Dim SupervisorUnapprovedDict As New Dictionary(Of String, List(Of Tuple(Of String, Date)))

        ProgressBar1.Maximum = selectedEmployeeData.NoItems
        For Each employee As TLtoQB_TimeEntry.Employee In selectedEmployeeData.DataArray
            If employee.RecSelect = True And TimeEntryData IsNot Nothing Then
                Dim emplTLData As TLtoQB_TimeEntry.TimeEntryDataStructureQB = timeentry_tltoqb.GetTimeEntryTLData(employee.AccountEmployeeId, dpStartDate.Value, dpEndDate.Value, Me, p_token, False)
                Dim TL_TimeEntries As New TimeLiveDataSetTableAdapters.AccountEmployeeTimeEntryPeriodTableAdapter

                For Each entry As TLtoQB_TimeEntry.TimeEntry In emplTLData.DataArray
                    With entry
                        ' Check if a time entry has yet to be approved
                        Dim TimeEntryEnterred As Integer = TL_TimeEntries.GetTotalNumUnsubmittedEntries(employee.AccountEmployeeId, dpStartDate.Value, dpEndDate.Value)
                        Dim TimeEntryApproved As Boolean = TL_TimeEntries.GetTimeApproval(employee.AccountEmployeeId, .TimeEntryDate)
                        Dim TimeEntrySubmitted As Boolean = TL_TimeEntries.GetTimeSubmission(employee.AccountEmployeeId, .TimeEntryDate)

                        Dim full_name As String = .CustomerName.ToString() + MAIN.colonReplacer + .ProjectName.ToString() + MAIN.colonReplacer + replaceColonsAndRemoveUnwantedSpaces(.TaskWithParent.ToString())


                        If Not TimeEntrySubmitted Then
                            My.Forms.MAIN.History("Time entry not submitted for " + .EmployeeName + " on the week of " + .TimeEntryDate, "N")
                            If Not EmployeeUnsubmittedDict.ContainsKey(employee.AccountEmployeeId) Then
                                EmployeeUnsubmittedDict(employee.AccountEmployeeId) = New List(Of Date)
                            End If
                            EmployeeUnsubmittedDict(employee.AccountEmployeeId).Add(.TimeEntryDate)
                        ElseIf Not TimeEntryApproved Then
                            My.Forms.MAIN.History("Time entry not approved for " + .EmployeeName + " on the week of " + .TimeEntryDate, "N")
                            Dim supervisor As String = GetManagerTLID(employee.AccountEmployeeId)
                            If supervisor.Length Then ' If not the empty string
                                If Not SupervisorUnapprovedDict.ContainsKey(supervisor) Then
                                    SupervisorUnapprovedDict(supervisor) = New List(Of Tuple(Of String, Date))
                                End If
                                SupervisorUnapprovedDict(supervisor).Add(Tuple.Create(employee.FullName, .TimeEntryDate))
                            End If
                        End If
                    End With
                Next

                ' Send email to employee about time entries if there are uncompleted entries
                If emplTLData.DataArray.Count = 0 Then
                    Dim message As String = InputBox("Email to: " + employee.FullName, "Uncompleted Time Card", My.Settings.UncompletedMessage)
                    If message <> "" Then
                        SendEmployeeGMail("Uncompleted Time Card Message", message, True, employee.AccountEmployeeId)
                    End If
                End If

                ' Send email to employee about time entries if there are un-submitted entries
                If EmployeeUnsubmittedDict.ContainsKey(employee.AccountEmployeeId) Then
                    Dim numUnsubmitted As Integer = EmployeeUnsubmittedDict(employee.AccountEmployeeId).Count
                    Dim resp As MsgBoxResult = MsgBox("Email " + employee.FullName + " about their " + Convert.ToString(numUnsubmitted) + " unsubmitted time entries?", MsgBoxStyle.YesNo, "Email Employee?")
                    If resp = MsgBoxResult.Yes Then
                        Dim message As String = "Hi " + employee.FullName + "," + vbNewLine + My.Settings.UnsubmittedMessage + vbNewLine + vbNewLine + "Unsubmitted Time entry dates:"

                        For Each d As Date In EmployeeUnsubmittedDict(employee.AccountEmployeeId)
                            message += vbNewLine + d.DayOfWeek.ToString + " " + MonthName(d.Month) + " " + d.Day.ToString
                        Next

                        SendEmployeeGMail("Unsubmitted Time Card", message, True, employee.AccountEmployeeId)
                    End If
                End If
            End If
            ProgressBar1.Value += 1
        Next

        For Each id As String In SupervisorUnapprovedDict.Keys
            Dim employeeAdapter As New QB_TL_IDsTableAdapters.EmployeesTableAdapter
            Dim supervisorName As String = employeeAdapter.GetNamefromTLID(id)
            If supervisorName IsNot Nothing Then
                supervisorName = supervisorName.Trim
            End If
            Dim numUnapproved As Integer = SupervisorUnapprovedDict(id).Count
            Dim resp As MsgBoxResult = MsgBox("Email " + supervisorName + " about the " + Convert.ToString(numUnapproved) + " time card(s) waiting for their approval?", MsgBoxStyle.YesNo, "Email Employee?")
            If resp = MsgBoxResult.Yes Then
                Dim message As String = "Hi " + supervisorName + "," + vbNewLine + My.Settings.UnapprovedMessage + vbNewLine + vbNewLine + "Unsubmitted Time entries:"

                For Each t As Tuple(Of String, Date) In SupervisorUnapprovedDict(id)
                    Dim emplName As String = t.Item1
                    Dim d As Date = t.Item2
                    message += vbNewLine + "Employee: " + emplName + "    Date: " + d.DayOfWeek.ToString + " " + MonthName(d.Month) + " " + d.Day.ToString
                Next

                SendEmployeeGMail("Unapproved Time Card", message, True, id)
            End If
        Next

        System.Threading.Thread.Sleep(150)
        System.Windows.Forms.Application.DoEvents()
        My.Forms.MAIN.History("Done sending emails", "n")
        ProgressBar1.Value = 0
    End Sub

    ''' <summary>
    ''' Transfers the selected customers, employees, vendors, jobs/subjobs or item/subitems, from TL to QB, or QB to TL depending on which is selected
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub btnTransfer_Click(sender As Object, e As EventArgs) Handles btnTransfer.Click
        If Not LoggedIn Then
            Exit Sub
        End If

        Dim ItemsProcessed As Integer = 0

        If Type = 10 Then 'When processing customers
            Reset_Checked_Customer_Value(customerData)
            If QBtoTLCustomerRadioButton.Checked Then
                QB_Set_Selected_Customer()

                ItemsProcessed = customer_qbtotl.QBTransferCustomerToTL(customerData, p_token, Me, True)
                History(ItemsProcessed.ToString() + " TimeLive record" + If(ItemsProcessed = 1, " was", "s were") + " created or updated", "i")
                My.Settings.CustomerLastSync = DateTime.Now.ToString()
                My.Settings.Save()
            Else
                Dim customersToCheck As List(Of String) = TL_Set_Selected_Items()
                ItemsProcessed = customer_TLSync.SyncCustomerData(p_token, Me, True, customersToCheck)
                History(ItemsProcessed.ToString() + " QuickBooks customer" + If(ItemsProcessed = 1, " was", "s were") + " created or updated", "i")
            End If
        ElseIf Type = 11 Then 'When processing Employees
            Reset_Checked_Employee_Value(employeeData)
            If QBtoTLEmployeeRadioButton.Checked Then
                Set_Selected_Employee()
                ItemsProcessed = employee_qbtotl.QBTransferEmployeeToTL(employeeData, p_token, Me, True)
                History(ItemsProcessed.ToString() + " TimeLive records were created or updated", "i")
                My.Settings.EmployeeLastSync = DateTime.Now.ToString()
                My.Settings.Save()
            Else
                Dim employeesToCheck As List(Of String) = TL_Set_Selected_Items()
                ItemsProcessed = employee_TLSync.SyncEmployeeData(p_token, Me, True, employeesToCheck)
                History(ItemsProcessed.ToString() + " QuickBooks employee" + If(ItemsProcessed = 1, " was", "s were") + " created or updated", "i")
            End If
        ElseIf Type = 12 Then 'When processing vendor
            Reset_Checked_Vendor_Value(vendorData)
            If QBtoTLVendorRadioButton.Checked Then
                Set_Selected_Vendor()
                ItemsProcessed = vendor_qbtotl.QBTransferVendorToTL(vendorData, p_token, Me, True)
                History(ItemsProcessed.ToString() + " TimeLive records were created or updated", "i")
                My.Settings.VendorLastSync = DateTime.Now.ToString()
                My.Settings.Save()
            Else
                Dim vendorsToCheck As List(Of String) = TL_Set_Selected_Items()
                ItemsProcessed = vendor_TLSync.SyncVendorData(p_token, Me, True, vendorsToCheck)
                History(ItemsProcessed.ToString() + " QuickBooks vendor" + If(ItemsProcessed = 1, " was", "s were") + " created or updated", "i")
            End If
        ElseIf Type = 13 Or Type = 14 Then 'When processing Jobs or Items
            Reset_Checked_Job_Value(JobData)
            If QBtoTLJobItemRadioButton.Checked Then
                Set_Selected_Job_Item()
                If Type = 13 Then
                    ItemsProcessed = job_qbtotl.QBTransferJobstoTL(JobData, p_token, Me, True)
                    History(ItemsProcessed.ToString() + " TimeLive records were created or updated", "i")
                    My.Settings.JobLastSync = DateTime.Now.ToString()
                Else
                    ItemsProcessed = job_qbtotl.QBTransferItemsToTL(JobData, p_token, Me, True)
                    History(ItemsProcessed.ToString() + " TimeLive records were created or updated", "i")
                    My.Settings.ItemlastSync = DateTime.Now.ToString()
                End If
                My.Settings.Save()
            Else
                Dim jobsToCheck As List(Of String) = TL_Set_Selected_Items()
                ItemsProcessed = job_TLSync.SyncJobsSubJobData(p_token, Me, True, jobsToCheck)
                History(ItemsProcessed.ToString() + " QuickBooks job/item" + If(ItemsProcessed = 1, " was", "s were") + " created or updated", "i")
            End If
        ElseIf Type = 20 Then 'When processing Time Transfer
            If MsgBox("Do you want to transfer times?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                Reset_Checked_TimeEntry_Value(TimeEntryData)
                Set_Selected_TimeEntry(DataGridView2)

                ' Transfer Time Entry data from TL to QB
                ItemsProcessed = timeentry_tltoqb.TLTransferTimeToQB(TimeEntryData, p_token, Me, True)
                History(ItemsProcessed.ToString() + If(ItemsProcessed = 1, " Time Entry was", " Time Entries were") + " created or updated", "i")
                My.Settings.TimeTrackingLastSync = DateTime.Now.ToString()
            End If
        ElseIf Type = 21 Then 'When processing Expense Reports
            If MsgBox("Do you want to transfer expenses?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                Reset_Checked_ExpenseReport_Value(expenseReportData)
                Set_Selected_ExpenseReports(DataGridView2)

                ItemsProcessed = expensereport_tltoqb.TLTransferExpensesToQB(expenseReportData, selectedExpenseSheetData, Me, True)
                History(ItemsProcessed.ToString() + " Expense Report" + If(ItemsProcessed = 1, " was", "s were") + " created or updated", "i")
            End If
        End If

        ' Refresh after processing
        My.Forms.MAIN.History("Refreshing after processing", "n")
        display_UI()

        System.Threading.Thread.Sleep(150)
        System.Windows.Forms.Application.DoEvents()
        ProgressBar1.Value = 0
    End Sub

    Private Sub Reset_Checked_Customer_Value(ByRef customerObj As QBtoTL_Customer.CustomerDataStructureQB)
        ' reset the check value to zero
        If customerObj IsNot Nothing Then
            For Each element As QBtoTL_Customer.Customer In customerObj.DataArray
                element.RecSelect = False
            Next
        End If
    End Sub

    Private Sub Reset_Checked_Vendor_Value(ByRef vendorObj As QBtoTL_Vendor.VendorDataStructureQB)
        ' reset the check value to zero
        If vendorObj IsNot Nothing Then
            For Each element As QBtoTL_Vendor.Vendor In vendorObj.DataArray
                element.RecSelect = False
            Next
        End If
    End Sub

    Private Sub Reset_Checked_Job_Value(ByRef jobObj As QBtoTL_JobOrItem.JobDataStructureQB)
        ' reset the check value to zero
        If jobObj IsNot Nothing Then
            For Each element As QBtoTL_JobOrItem.Job_Subjob In jobObj.DataArray
                element.RecSelect = False
            Next
        End If
    End Sub

    Private Sub Reset_Checked_Employee_Value(ByRef EmployeeObj As QBtoTL_Employee.EmployeeDataStructureQB)
        ' reset the check value to zero
        If EmployeeObj IsNot Nothing Then
            For Each element As QBtoTL_Employee.Employee In EmployeeObj.DataArray
                element.RecSelect = False
            Next
        End If
    End Sub

    Private Sub Reset_Checked_SelectedEmployee_Value(ByRef SelectedEmployeeObj As TLtoQB_TimeEntry.EmployeeDataStructure)
        ' reset the check value to zero
        If SelectedEmployeeObj IsNot Nothing Then
            For Each element As TLtoQB_TimeEntry.Employee In SelectedEmployeeObj.DataArray
                element.RecSelect = False
            Next
        End If
    End Sub

    Private Sub Reset_Checked_SelectedExpenseSheets_Value(ByRef SelectExpensesObj As TLtoQB_ExpenseReports.ExpenseSheetDataStructure)
        ' reset the check value to zero
        If SelectExpensesObj IsNot Nothing Then
            For Each element As TLtoQB_ExpenseReports.ExpenseSheet In SelectExpensesObj.DataArray
                element.RecSelect = False
            Next
        End If
    End Sub

    Private Sub Reset_Checked_TimeEntry_Value(ByRef TimeEntryObj As TLtoQB_TimeEntry.TimeEntryDataStructureQB)
        ' reset the check value to zero
        If TimeEntryObj IsNot Nothing Then
            For Each element As TLtoQB_TimeEntry.TimeEntry In TimeEntryObj.DataArray
                element.RecSelect = False
            Next
        End If
    End Sub

    Private Sub Reset_Checked_ExpenseReport_Value(ByRef ExpenseReportObj As TLtoQB_ExpenseReports.ExpenseReportDataStructureQB)
        ' reset the check value to zero
        If ExpenseReportObj IsNot Nothing Then
            For Each element As TLtoQB_ExpenseReports.ExpenseReport In ExpenseReportObj.getDataArray()
                element.RecSelect = False
            Next
        End If
    End Sub

    Private Sub Set_Selected_TimeEntry(ByRef DataGridView As DataGridView)
        Dim chargingRelAdapter As New QB_TL_IDsTableAdapters.ChargingRelationshipsTableAdapter
        Dim emplAdapter As New QB_TL_IDsTableAdapters.EmployeesTableAdapter
        Dim jobAdapter As New QB_TL_IDsTableAdapters.Jobs_SubJobsTableAdapter
        ProgressBar1.Maximum = DataGridView.Rows.Count
        For Each row As DataGridViewRow In DataGridView.Rows
            If row.Cells("Date").Value IsNot Nothing And row.Cells("ckBox").Value And TimeEntryData.NoItems Then
                Dim fullTaskName As String = row.Cells("Task").Value.ToString()
                ' Checks for a time entry in our data array which has the correct employee, job/subjob, and date
                Dim itemName As String = Nothing
                TimeEntryData.DataArray.ForEach(
                    Sub(timeentry)
                        Dim jobName As String
                        If (timeentry.ProjectName.Contains(":")) Then
                            ' Project Name is the task name as well
                            Sync_TLtoQB_JoborItem.removeSpacesBetweenColonsAndSetLengthOfFields(timeentry.ProjectName)
                            jobName = timeentry.CustomerName + MAIN.colonReplacer + replaceColonsAndRemoveUnwantedSpaces(timeentry.ProjectName)
                            itemName = timeentry.CustomerName + ":" + timeentry.ProjectName.Split(":")(0) + ":" + timeentry.TaskWithParent
                            Sync_TLtoQB_JoborItem.checkQBItemExists(timeentry.CustomerName, timeentry.ProjectName.Split(":")(0), timeentry.TaskWithParent, UI:=True, p_token:=p_token, cancel_opt:=True)
                        Else
                            jobName = timeentry.CustomerName + MAIN.colonReplacer + timeentry.ProjectName +
                                      MAIN.colonReplacer + replaceColonsAndRemoveUnwantedSpaces(timeentry.TaskWithParent)
                        End If

                        If (equalNames(timeentry.EmployeeName, row.Cells("Employee").Value.ToString) And jobName = fullTaskName And
                          timeentry.TimeEntryDate.ToString("MM/dd/yyyy") = row.Cells("Date").Value.ToString) Then
                            History("Selected for processing: " + row.Cells("Employee").Value.ToString + " with task " + row.Cells("Task").Value.ToString + " on " + row.Cells("Date").Value.ToString, "n")
                            Dim empID As String = getTimeLiveEmployeeIDFromName(timeentry.EmployeeName)
                            Dim jobID As String = jobAdapter.GetCorrespondingTL_IDbyTL_Name(jobName.Replace(MAIN.colonReplacer, ":"))
                            ' Add the relationship between employee and job for time entry if not present
                            Dim syncRel As Sync_TLtoQB_Relationships = New Sync_TLtoQB_Relationships()
                            If syncRel.Add_Relationship(chargingRelAdapter, jobID, empID, itemName) > 0 Then
                                timeentry.RecSelect = True
                            Else
                                If jobID Is Nothing Then My.Forms.MAIN.History("Could not find the QB ID for job " + jobName, "n")
                                If empID Is Nothing Then My.Forms.MAIN.History("Could not find the QB ID for employee " + timeentry.EmployeeName, "n")
                                My.Forms.MAIN.History("No Relationship exists between " + timeentry.EmployeeName + " and " + jobName, "n")
                                timeentry.RecSelect = True ' Remove if we do not want these time transfers to transfer
                            End If
                        End If
                    End Sub
                )
                ProgressBar1.Value += 1
            End If
        Next
    End Sub

    'Returns a list Of the full names For the selected TimeLive entities
    Private Function TL_Set_Selected_Items()
        Dim TL_Names As List(Of String) = New List(Of String)
        If DataGridView1 IsNot Nothing Then
            For Each row As DataGridViewRow In DataGridView1.Rows
                If row.Cells("Name").Value IsNot Nothing And row.Cells("ckBox").Value Then
                    Dim full_name As String = row.Cells("Name").Value.ToString.Replace(MAIN.colonReplacer, ":")
                    TL_Names.Add(full_name)
                    History("Item selected for processing: " + row.Cells("Name").Value, "n")
                End If
            Next
        End If

        Return TL_Names
    End Function

    Private Sub Get_Customer_Form(ByRef customer As QBtoTL_Customer.Customer)


        Using newForm As ModifyForm = New ModifyForm()
            newForm.TxtName.Text = customer.QB_Name
            newForm.txtEmail.Text = customer.Email
            newForm.txtTelephone2.Text = customer.Telephone1
            newForm.txtFax.Text = customer.Fax

            If DialogResult.OK = newForm.ShowDialog() Then
                customer.QB_Name = newForm.TxtName.Text.Trim()
                customer.Email = newForm.txtEmail.Text.Trim()
                customer.Telephone1 = newForm.txtTelephone2.Text.Trim()
                customer.Fax = newForm.txtFax.Text.Trim()
                customer.RecSelect = True
            Else
                Exit Sub
            End If
        End Using


    End Sub
    Private Sub QB_Set_Selected_Customer()
        If DataGridView1 IsNot Nothing Then
            For Each row As DataGridViewRow In DataGridView1.Rows
                If row.Cells("Name").Value IsNot Nothing And row.Cells("ckBox").Value = True Then
                    customerData.DataArray.ForEach(
                        Sub(customer)
                            If customer.QB_Name = row.Cells("Name").Value.ToString Then

                                ' Ask user if they want to modify the fields before send it to timelive 
                                Get_Customer_Form(customer)

                            End If
                        End Sub
                    )
                    History("Customers selected for processing: " + row.Cells("Name").Value, "n")
                End If
            Next
        End If
    End Sub

    Private Sub Get_Employee_Form(ByRef employee As QBtoTL_Employee.Employee)

        Using newForm As EmployeeForm = New EmployeeForm()
            newForm.txtFirstName.Text = employee.FirstName
            newForm.txtLastName.Text = employee.LastName
            newForm.mTxtHiredDate.Text = employee.HiredDate.ToString("MM/dd/yyyy")


            If DialogResult.OK = newForm.ShowDialog() Then
                'empFirstName = newForm.txtFirstName.Text.Trim()
                'empLastName = newForm.txtLastName.Text.Trim()
                'empHiredDate = newForm.mTxtHiredDate.Text.Trim()

                employee.FirstName = newForm.txtFirstName.Text.Trim()
                employee.LastName = newForm.txtLastName.Text.Trim()
                employee.HiredDate = newForm.mTxtHiredDate.Text.Trim()
                employee.QB_Name = employee.FirstName + " " + employee.LastName
                employee.RecSelect = True
                employee.NewlyAdded = ""
            Else
                Exit Sub
            End If
        End Using
    End Sub
    Private Sub Set_Selected_Employee()
        If DataGridView1 IsNot Nothing Then
            For Each row As DataGridViewRow In DataGridView1.Rows
                If row.Cells("Name").Value IsNot Nothing And row.Cells("ckBox").Value = True Then
                    employeeData.DataArray.ForEach(
                        Sub(employee)

                            ' format name to compare
                            'Dim qbNameArray() As String = row.Cells("Name").Value.ToString.Split(",")
                            'Dim formatQbName As String = qbNameArray(1) + " " + qbNameArray(0)

                            'row.Cells("Name").Value.ToString
                            'If employee.QB_Name = row.Cells("Name").Value.ToString.Replace(",", "").Trim() Then 'formatQbName.Trim Then
                            If employee.LastName + " " + employee.FirstName = row.Cells("Name").Value.ToString.Replace(",", "").Trim() Then

                                ' Edit the data before tranfer if need it 
                                Get_Employee_Form(employee)
                            End If
                        End Sub
                    )
                    History("Employees selected for processing: " + row.Cells("Name").Value, "n")
                End If
            Next
        End If
    End Sub

    'For Time Transfer
    Private Sub Set_Selected_SelectedEmployee()
        For Each row As DataGridViewRow In DataGridView1.Rows
            If row.Cells("Name").Value IsNot Nothing And row.Cells("ckBox").Value = True Then
                selectedEmployeeData.DataArray.ForEach(
                    Sub(selectedEmployee)
                        If selectedEmployee.FullName = row.Cells("Name").Value.ToString Then
                            selectedEmployee.RecSelect = True
                        End If
                    End Sub
                )
                History("Selected employee for time transfer: " + row.Cells("Name").Value, "n")
            End If
        Next
    End Sub

    ' For Expense Reports
    Private Sub Set_Selected_SelectedExpenseSheet()
        For Each row As DataGridViewRow In DataGridView1.Rows
            If row.Cells("Sheet ID").Value IsNot Nothing And row.Cells("ckBox").Value = True Then
                selectedExpenseSheetData.DataArray.ForEach(
                    Sub(selectedExpenseSheet)
                        If selectedExpenseSheet.SheetId = row.Cells("Sheet ID").Value Then
                            selectedExpenseSheet.RecSelect = True
                            History("Selected expense sheet for time transfer: " + selectedExpenseSheet.SheetId.ToString, "n")
                        End If
                    End Sub
                )
            End If
        Next
    End Sub

    Private Sub Get_Vendor_Form(ByRef vendor As QBtoTL_Vendor.Vendor)

        Using newForm As VendorForm = New VendorForm()
            newForm.txtFirstName.Text = vendor.FirstName
            newForm.txtLastName.Text = vendor.LastName
            'newForm.txtEmail.Text = vendor.Email
            'newForm.mTxtHiredDate.Text = vendor.HiredDate.ToString("MM/dd/yyyy")
            'newForm.txtEmail.Text = vendor.Email


            If DialogResult.OK = newForm.ShowDialog() Then
                vendor.FirstName = newForm.txtFirstName.Text.Trim()
                vendor.LastName = newForm.txtLastName.Text.Trim()
                'vendor.Email = newForm.txtEmail.Text.Trim()
                'vendor.HiredDate = newForm.mTxtHiredDate.Text.Trim()
                vendor.QB_Name = vendor.FirstName + " " + vendor.LastName
                vendor.RecSelect = True
                'vendor.NewlyAdded = ""
            Else
                Exit Sub
            End If
        End Using
    End Sub

    Private Sub Set_Selected_Vendor()
        If DataGridView1 IsNot Nothing Then
            For Each row As DataGridViewRow In DataGridView1.Rows
                If row.Cells("Name").Value IsNot Nothing And row.Cells("ckBox").Value Then
                    vendorData.DataArray.ForEach(
                        Sub(vendor)
                            If vendor.LastName + " " + vendor.FirstName = row.Cells("Name").Value.ToString.Replace(",", "").Trim() Then

                                Get_Vendor_Form(vendor)

                            End If
                        End Sub
                    )
                    History("Vendors selected for processing: " + row.Cells("Name").Value, "n")
                End If
            Next
        End If
    End Sub

    Private Sub Set_Selected_Job_Item()
        If DataGridView1 IsNot Nothing Then
            For Each row As DataGridViewRow In DataGridView1.Rows
                If row.Cells("Name").Value IsNot Nothing And row.Cells("ckBox").Value = True Then
                    JobData.DataArray.ForEach(
                        Sub(job)
                            Dim full_name As String = row.Cells("Full Name").Value.ToString.Replace(MAIN.colonReplacer, ":")
                            If job.FullName = full_name Then
                                job.RecSelect = True
                            End If
                        End Sub
                    )
                    History("Job or items selected for processing: " + row.Cells("Full Name").Value, "n")
                End If
            Next
        End If
    End Sub

    Private Sub Set_Selected_ExpenseReports(ByRef DataGridView As DataGridView)
        If DataGridView IsNot Nothing Then
            For Each row As DataGridViewRow In DataGridView.Rows
                If row.Cells("Entry ID").Value IsNot Nothing And row.Cells("ckBox").Value = True Then
                    expenseReportData.DataArray.ForEach(
                        Sub(expenseReport)
                            If expenseReport.EntryID = row.Cells("Entry ID").Value.ToString Then
                                expenseReport.RecSelect = True
                                History("Expense report of " + expenseReport.ExpenseName + " for " + expenseReport.EmployeeName, "n")
                            End If
                        End Sub
                    )
                End If
            Next
        End If
    End Sub

    Private Sub selectall_checkbox(sender As Object, e As EventArgs) Handles SelectAllCheckBox.CheckedChanged
        If DataGridView1 IsNot Nothing Then
            For Each row As DataGridViewRow In DataGridView1.Rows
                If row.Cells.Item(1).Value IsNot Nothing Then
                    row.Cells("ckBox").Value = SelectAllCheckBox.Checked
                End If
            Next
        End If
    End Sub

    Private Sub timeEntry_selectall_checkbox(sender As Object, e As EventArgs) Handles EntitiesSelectAll.CheckedChanged
        If DataGridView2 IsNot Nothing Then
            For Each row As DataGridViewRow In DataGridView2.Rows
                row.Cells("ckBox").Value = EntitiesSelectAll.Checked
            Next
        End If
    End Sub

    Public Sub LoadSelectedExpenseReportItems(ByVal ExpenseSheetId As Guid, ByVal EmployeeName As String, ByVal SheetDate As Date, ByRef DataGridView As DataGridView)
        Dim temp As New TLtoQB_TimeEntry.TimeEntryDataStructureQB
        Dim emplTLData As TLtoQB_ExpenseReports.ExpenseReportDataStructureQB = expensereport_tltoqb.GetExpenseReportTLData(ExpenseSheetId, EmployeeName, SheetDate, Me, p_token, False)
        expenseReportData.combine(emplTLData)

        Dim TL_ExpenseSheets As New TimeLiveDataSetTableAdapters.AccountEmployeeExpenseSheetTableAdapter
        Dim isApproved As Boolean = TL_ExpenseSheets.isApproved(ExpenseSheetId)
        Dim isSubmitted As Boolean = TL_ExpenseSheets.isSubmitted(ExpenseSheetId)
        Dim isRejected As Boolean = TL_ExpenseSheets.isRejected(ExpenseSheetId)
        If expenseReportData IsNot Nothing Then
            For Each element As TLtoQB_ExpenseReports.ExpenseReport In emplTLData.getDataArray()
                With element
                    element.RecSelect = isApproved And isSubmitted And Not isRejected
                    Dim datagrid_row As DataGridViewRow = New DataGridViewRow()
                    datagrid_row.CreateCells(DataGridView)
                    datagrid_row.SetValues(.RecSelect, .EntryID, .EmployeeName, .ExpenseName, .ExpenseReportDate.ToString("MM/dd/yyyy"), .ProjectName, "$" + .Amount.ToString("N2"), ExpenseSheetId)
                    Dim errorMsgDescription As String = " for " + .EmployeeName + " for the expense of " + .ExpenseName + "on the week of " + .ExpenseReportDate

                    If isRejected Then
                        My.Forms.MAIN.History("Expense report rejected" + errorMsgDescription, "N")
                        datagrid_row.Cells(0).ReadOnly = True
                        datagrid_row.DefaultCellStyle.BackColor = Color.DarkGray
                    ElseIf Not isSubmitted Then
                        My.Forms.MAIN.History("Expense report not submitted" + errorMsgDescription, "N")
                        datagrid_row.Cells(0).ReadOnly = True
                        datagrid_row.DefaultCellStyle.BackColor = Color.DarkGray
                    ElseIf Not isApproved Then
                        My.Forms.MAIN.History("Expense report not approved" + errorMsgDescription, "N")
                        datagrid_row.Cells(0).ReadOnly = True
                        datagrid_row.DefaultCellStyle.BackColor = Color.LightGray
                    End If

                    DataGridView.Rows.Add(datagrid_row)
                End With
            Next
        End If
    End Sub

    Private Sub LoadSelectedTimeEntryItems(AccountEmployeeId As String, ByRef DataGridView As DataGridView,
                                          ByVal StartDate As DateTime, ByVal EndDate As DateTime, Optional combine As Boolean = False,
                                          Optional payroll_dict As Dictionary(Of String, String) = Nothing, Optional item_dict As Dictionary(Of String, String) = Nothing)
        Dim temp As New TLtoQB_TimeEntry.TimeEntryDataStructureQB
        Dim emplTLData As TLtoQB_TimeEntry.TimeEntryDataStructureQB = timeentry_tltoqb.GetTimeEntryTLData(AccountEmployeeId, StartDate, EndDate, Me, p_token, False)
        If combine Then
            TimeEntryData.combine(emplTLData)
        Else
            TimeEntryData = emplTLData
        End If

        Dim TL_TimeEntries As New TimeLiveDataSetTableAdapters.AccountEmployeeTimeEntryPeriodTableAdapter

        If TimeEntryData IsNot Nothing Then
            For Each element As TLtoQB_TimeEntry.TimeEntry In emplTLData.DataArray
                With element
                    Dim Item_SubItemID As String = Nothing
                    Dim TotalHours As Double = TotalTimeToHours(.TotalTime)
                    Dim payrollDisp As String = If(payroll_dict Is Nothing, .PayrollItem, If(payroll_dict.ContainsKey(.PayrollItem), payroll_dict(.PayrollItem), .PayrollItem))
                    Dim ServiceDisp As String
                    Dim fullName As String

                    If .ProjectName.Contains(":") Then
                        ' Project name has task in it, and task has the item
                        ServiceDisp = .CustomerName.ToString() + MAIN.colonReplacer + replaceColonsAndRemoveUnwantedSpaces(.ProjectName.Split(":")(0)) + MAIN.colonReplacer + .TaskWithParent.ToString()
                        fullName = .CustomerName.ToString() + MAIN.colonReplacer + replaceColonsAndRemoveUnwantedSpaces(.ProjectName.ToString())
                    Else
                        ServiceDisp = If(item_dict Is Nothing, If(.ServiceName Is Nothing, .ServiceItem, .ServiceName),
                                                                         If(item_dict.ContainsKey(.ServiceItem), item_dict(.ServiceItem), .ServiceItem))
                        fullName = .CustomerName.ToString() + MAIN.colonReplacer + .ProjectName.ToString() + MAIN.colonReplacer + replaceColonsAndRemoveUnwantedSpaces(.TaskWithParent.ToString())
                    End If

                    ' Check if a time entry has yet to be submitted or approved
                    Dim TimeEntryApproved = TL_TimeEntries.GetTimeApproval(AccountEmployeeId, .TimeEntryDate)
                    Dim TimeEntrySubmitted = TL_TimeEntries.GetTimeSubmission(AccountEmployeeId, .TimeEntryDate)

                    element.RecSelect = TimeEntryApproved And TimeEntrySubmitted

                    Dim datagrid_row As DataGridViewRow = New DataGridViewRow()
                    datagrid_row.CreateCells(DataGridView)
                    datagrid_row.SetValues(.RecSelect, .EmployeeName, .TimeEntryDate.ToString("MM/dd/yyyy"), fullName,
                                           TotalHours.ToString, .TimeEntryClass, payrollDisp, ServiceDisp)

                    If Not TimeEntrySubmitted Then
                        My.Forms.MAIN.History("Time entry not submitted for " + .EmployeeName + " on the week of " + .TimeEntryDate, "N")
                        ' Checkbox is cell 0
                        datagrid_row.Cells(0).ReadOnly = True
                        datagrid_row.DefaultCellStyle.BackColor = Color.DarkGray
                    ElseIf Not TimeEntryApproved Then
                        My.Forms.MAIN.History("Time entry not approved for " + .EmployeeName + " on the week of " + .TimeEntryDate, "N")
                        datagrid_row.Cells(0).ReadOnly = True
                        datagrid_row.DefaultCellStyle.BackColor = Color.LightGray
                    End If
                    DataGridView.Rows.Add(datagrid_row)
                End With
            Next
        End If
    End Sub

    Private Function TotalTimeToHours(TotalTime As Date) As Double
        Dim TotalHour As Integer
        Dim TotalMin As Double
        TotalHour = TotalTime.ToString("%h")
        'Turn hour to 24 hour time
        TotalHour = TotalHour Mod 12
        If Not TotalTime.ToString.Contains("AM") Then
            TotalHour += 12
        End If

        TotalMin = TotalTime.ToString("%m")
        TotalMin = (TotalMin / 60).ToString("00.00")

        Return TotalHour + TotalMin
    End Function

    Private Sub time_btn_currentweek_Click(sender As Object, e As EventArgs) Handles time_btn_currentweek.Click
        Dim sat As Date = firstdateofthisweek()
        dpStartDate.Value = sat.AddDays(-7)
        dpEndDate.Value = sat.AddDays(-1)
    End Sub

    Private Sub time_nextWeek_Click(sender As Object, e As EventArgs) Handles time_nextWeek.Click
        dpStartDate.Value = dpStartDate.Value.AddDays(7)
        dpEndDate.Value = dpEndDate.Value.AddDays(7)
    End Sub

    Private Sub time_preWeek_Click(sender As Object, e As EventArgs) Handles time_prevWeek.Click
        dpStartDate.Value = dpStartDate.Value.AddDays(-7)
        dpEndDate.Value = dpEndDate.Value.AddDays(-7)
    End Sub

    Private Sub expense_btn_currentweek_Click(sender As Object, e As EventArgs) Handles expense_btn_currWeek.Click
        Dim sat As Date = firstdateofthisweek()
        expenseReportStartDate.Value = sat.AddDays(-7)
        expenseReportEndDate.Value = sat.AddDays(-1)
    End Sub

    Private Sub report_nextWeek_Click(sender As Object, e As EventArgs) Handles expense_nextWeek.Click
        expenseReportStartDate.Value = expenseReportStartDate.Value.AddDays(7)
        expenseReportEndDate.Value = expenseReportEndDate.Value.AddDays(7)
    End Sub

    Private Sub report_preWeek_Click(sender As Object, e As EventArgs) Handles expense_prevWeek.Click
        expenseReportStartDate.Value = expenseReportStartDate.Value.AddDays(-7)
        expenseReportEndDate.Value = expenseReportEndDate.Value.AddDays(-7)
    End Sub

    Private Function firstdateofthisweek()
        Dim myCI As New CultureInfo("en-US")
        Dim myCal As Calendar = myCI.Calendar
        Dim myCWR As CalendarWeekRule = myCI.DateTimeFormat.CalendarWeekRule
        Dim myFirstDOW As DayOfWeek = myCI.DateTimeFormat.FirstDayOfWeek

        Return firstdateofweek(Now.Year, myCal.GetWeekOfYear(DateTime.Now, myCWR, myFirstDOW), DayOfWeek.Saturday)
    End Function

    Private Function firstdateofweek(ByVal year As Integer, ByVal week As Integer, Optional firstdayofweek As DayOfWeek = DayOfWeek.Monday) As Date
        Dim dt As Date = New Date(year, 1, 1)
        If dt.DayOfWeek > 4 Then dt = dt.AddDays(7 - dt.DayOfWeek) Else dt = dt.AddDays(-dt.DayOfWeek)
        dt = dt.AddDays(firstdayofweek)
        Return dt.AddDays(7 * (week - 1))
    End Function

    Private Sub cbWageType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbWageType.SelectedIndexChanged
        My.Settings.QBWageType = cbWageType.SelectedIndex
        My.Settings.Save()
    End Sub

    Private Sub settingbtn_Click(sender As Object, e As EventArgs) Handles settingbtn.Click
        Using newsettingsForm = New AppSettings()
            If DialogResult.OK = newsettingsForm.ShowDialog() Then
                Dim NextRunDateTime As Date = Convert.ToDateTime(My.Settings.AutoRunTime)
                NextProcessingTime.Text = "Auto Processing Time: " + NextRunDateTime.ToString("MM/dd/yy HH:mm")
            End If
        End Using
    End Sub

    Private Sub loginbtn_Click(sender As Object, e As EventArgs) Handles loginbtn.Click
        Dim readItems As Integer
        If p_token Is Nothing Then
            Using newForm = New Login()
                If DialogResult.OK = newForm.ShowDialog() Then
                    LoggedIn = True
                    p_token = newForm.ReturnValue1
                    p_AccountId = newForm.ReturnValue2

                    'for type Customers, Employees, Vendors, Jobs/Subjobs, and Items/Subitems
                    readItems = display_UI()

                    If LoggedIn Then
                        History(readItems.ToString() + " items were read from Quickbooks", "n")
                    End If
                End If
            End Using
        Else
            MsgBox("You are already logged in")
        End If
    End Sub

    Private Sub clearlogbtn_Click(sender As Object, e As EventArgs) Handles clearlogbtn.Click
        StatusWindow.Clear()
    End Sub

    Private Sub Exitbtn_Click(Optional sender As Object = Nothing, Optional e As EventArgs = Nothing) Handles Exitbtn.Click
        My.Forms.MAIN.QUITQBSESSION() ' Close QB session before exiting
        My.Forms.MAIN.TIMERTHREADSESSION() ' Close TimerThread session before exiting
        Me.Close()
    End Sub

    Private Sub btn_systemsync_Click(sender As Object, e As EventArgs) Handles btn_systemsync.Click
        If MsgBox("Would you like to perform a sync from TimeLive?", MsgBoxStyle.YesNo, "Warning!") = MsgBoxResult.Yes Then ' If you select yes in the MsgBox then it will close the window
            Dim CurrentSystemSync As New CurrentSystemSync
            CurrentSystemSync.PassToken(p_token, p_AccountId)
        End If
    End Sub

    Private Sub btn_relationships_Click(sender As Object, e As EventArgs) Handles btn_relationships.Click
        ChargingRelationship.Owner = Me
        ChargingRelationship.Show()
    End Sub

    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click



        Dim objCustomer As New QBtoTL_Customer.Customer(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)
        Get_Customer_Form(objCustomer)
        ' If user want to cancel, stop it
        If objCustomer.RecSelect = False Then
            Exit Sub
        End If

        Dim UI As Boolean = True

        Try
            Dim msgSetRq As IMsgSetRequest = MAIN.SESSMANAGER.CreateMsgSetRequest("US", 2, 0)

            msgSetRq.Attributes.OnError = ENRqOnError.roeContinue
            Dim CustomerQueryRq As ICustomerQuery = msgSetRq.AppendCustomerQueryRq

            CustomerQueryRq.ORCustomerListQuery.FullNameList.Add(name)
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
                    create = MsgBox("New customer " + objCustomer.QB_Name + " is not existing in QuickBooks" + ". Create in QuickBooks?", MsgBoxStyle.YesNo, "Warning!") = MsgBoxResult.Yes
                End If

                If create Then

                    ' Add new Customer to QB
                    Dim custAdd As ICustomerAdd = newMsgSetRq.AppendCustomerAddRq
                    custAdd.CompanyName.SetValue(objCustomer.QB_Name.ToString)
                    custAdd.Name.SetValue(objCustomer.QB_Name.ToString)
                    custAdd.Fax.SetValue(If(String.IsNullOrEmpty(objCustomer.Fax), "", objCustomer.Fax))
                    custAdd.Email.SetValue(If(String.IsNullOrEmpty(objCustomer.Email), "", objCustomer.Email))
                    custAdd.Phone.SetValue(If(String.IsNullOrEmpty(objCustomer.Telephone1), "", objCustomer.Telephone1))

                    'step2: send the request
                    msgSetRs = MAIN.SESSMANAGER.DoRequests(newMsgSetRq)

                    ' Interpret the response
                    Dim res As IResponse
                    res = msgSetRs.ResponseList.GetAt(0)

                    If res.StatusSeverity = "Error" Then
                        Throw New Exception(res.StatusMessage)
                    End If


                End If
                ' Make request again to make sure that we added to QB, then add to sync table
                msgSetRq = MAIN.SESSMANAGER.CreateMsgSetRequest("US", 2, 0)
                msgSetRq.Attributes.OnError = ENRqOnError.roeContinue

                CustomerQueryRq = msgSetRq.AppendCustomerQueryRq
                CustomerQueryRq.ORCustomerListQuery.FullNameList.Add(name)

                msgSetRs = MAIN.SESSMANAGER.DoRequests(msgSetRq)
                response = msgSetRs.ResponseList.GetAt(0)
                custRetList = response.Detail
            End If

        Catch ex As Exception
            Throw ex
        End Try

        ' request data from quickbook to get new customer that just created to create customer in timelive
        Dim obj_QBtoTL_Customer As New QBtoTL_Customer
        Dim customerData As New QBtoTL_Customer.CustomerDataStructureQB

        'Get the current List of Customer in Quickbooks
        customerData = obj_QBtoTL_Customer.GetCustomerQBData(Me, True)
        Reset_Checked_Customer_Value(customerData)
        If employeeData IsNot Nothing Then
            For Each element As QBtoTL_Customer.Customer In customerData.DataArray
                If element.QB_Name = objCustomer.QB_Name Then
                    element.RecSelect = True
                    Exit For
                End If
            Next
        End If
        'Select the new created vendor 
        obj_QBtoTL_Customer.QBTransferCustomerToTL(customerData, p_token, Me, True)
        display_UI()
        '=======================
        'Dim msgSetRsTimeLive As IMsgSetResponse
        'Try
        '    Dim msgSetRq As IMsgSetRequest = MAIN.SESSMANAGER.CreateMsgSetRequest("US", 2, 0) 'sessManager
        '    msgSetRq.Attributes.OnError = ENRqOnError.roeContinue

        '    '-------------------------1---------------------------------------------
        '    Dim synccust As ICustomerQuery = msgSetRq.AppendCustomerQueryRq
        '    synccust.ORCustomerListQuery.CustomerListFilter.ActiveStatus.SetValue(ENActiveStatus.asAll) 'asActiveOnly)

        '    'step2: send the request
        '    msgSetRsTimeLive = MAIN.SESSMANAGER.DoRequests(msgSetRq) 'sessManager
        '    Dim respList As IResponseList = msgSetRsTimeLive.ResponseList
        '    If (respList Is Nothing Or respList.GetAt(0).Detail Is Nothing) Then
        '        ' no data
        '        My.Forms.MAIN.History("No customers found...", "i")

        '    End If

        '    ' Should only expect 1 response
        '    Dim resp As IResponse
        '    resp = respList.GetAt(0)
        '    'If (resp.StatusCode = 0) Then

        '    '----------------------2------------------------------------------------
        '    Dim custRetList As ICustomerRetList
        '    custRetList = resp.Detail
        '    '------------------------------3-----------------------------------
        '    Dim custRet As ICustomerRet
        '    For i As Integer = 0 To If(custRetList Is Nothing, -1, custRetList.Count - 1)
        '        custRet = custRetList.GetAt(i)
        '        With custRet
        '            If .ParentRef Is Nothing Then
        '                'for debuging 
        '                Dim checkingName = custRet.Name.GetValue
        '                'If custRet.Name.GetValue.ToString = qbName Then
        '                If String.Compare(custRet.Name.GetValue.ToString, name) = 0 Then
        '                    Dim objCustomerDataStructureQB As QBtoTL_Customer.CustomerDataStructureQB = New QBtoTL_Customer.CustomerDataStructureQB()
        '                    Dim objQbCustomer As QBtoTL_Customer.Customer = New QBtoTL_Customer.Customer("", name, email, custRet.ListID.GetValue, telephone1, fax, custRet.TimeModified.GetValue.ToString, custRet.TimeCreated.GetValue.ToString, True)
        '                    objQbCustomer.Enabled = True
        '                    objQbCustomer.RecSelect = True
        '                    objCustomerDataStructureQB.DataArray.Add(objQbCustomer)

        '                    customer_qbtotl.QBTransferCustomerToTL(objCustomerDataStructureQB, p_token, Me, False)
        '                    Exit For
        '                End If
        '            End If

        '        End With
        '        If UI Then
        '            My.Forms.MAIN.ProgressBar1.Value = i
        '        End If
        '    Next
        'Catch ex As Exception

        'End Try
        '' Refresh after processing
        'My.Forms.MAIN.History("Refreshing after processing", "n")
        'display_UI()

        'System.Threading.Thread.Sleep(150)
        'System.Windows.Forms.Application.DoEvents()
        'ProgressBar1.Value = 0

    End Sub

    Private Sub btnNewEmployee_Click(sender As Object, e As EventArgs) Handles btnNewEmployee.Click

        Dim objEmployee As New QBtoTL_Employee.Employee(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)

        Get_Employee_Form(objEmployee)
        ' if user would like to cancel create new employee
        If objEmployee.RecSelect = False Then
            Exit Sub
        End If

        Dim UI As Boolean = True
        Dim TLEmployeeName As String = objEmployee.FirstName + " " + objEmployee.LastName


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
                If UI Then create = MsgBox("New employee found in TimeLive: " + TLEmployeeName + ". Create in QuickBooks?", MsgBoxStyle.YesNo, "Warning!") = MsgBoxResult.Yes

                If create Then

                    ' Add TL Employee to QB
                    Dim employAdd As IEmployeeAdd = newMsgSetRq.AppendEmployeeAddRq
                    employAdd.FirstName.SetValue(If(objEmployee.FirstName = Nothing, "", objEmployee.FirstName))
                    employAdd.LastName.SetValue(If(objEmployee.LastName = Nothing, "", objEmployee.LastName))
                    'employAdd.MiddleName.SetValue(If(objEmployee.MiddleName = Nothing, "", objEmployee.MiddleName))
                    employAdd.HiredDate.SetValue(If(objEmployee.HiredDate = Nothing, Today, objEmployee.HiredDate))
                    'employAdd.Phone.SetValue(If(objEmployee.Phone = Nothing, "", objEmployee.Phone))
                    'employAdd.SSN.SetValue("123-45-6789")
                    'employAdd.Mobile.SetValue(If(objEmployee.Mobile = Nothing, "", objEmployee.Mobile)) ' Throws an error for some reason
                    'employAdd.EmployeeAddress.Addr1.SetValue(If(objEmployee.Address1 = Nothing, "123 Example Street", objEmployee.Address1))
                    'employAdd.EmployeeAddress.Addr2.SetValue(If(objEmployee.Address2 = Nothing, "", objEmployee.Address2))
                    'employAdd.EmployeeAddress.City.SetValue(If(objEmployee.City = Nothing, "Default", objEmployee.City))
                    'employAdd.EmployeeAddress.PostalCode.SetValue(If(objEmployee.PostalCode = Nothing, "12345", objEmployee.PostalCode))
                    'Dim state As String = If(objEmployee.State = Nothing, "MD", If(objEmployee.State.Length = 2, objEmployee.State.ToUpper(), "MD"))
                    'employAdd.EmployeeAddress.State.SetValue(state)
                    'employAdd.EmployeeAddress.Country.SetValue(If(objEmployee.Country = Nothing, "", objEmployee.Country))

                    msgSetRs = MAIN.SESSMANAGER.DoRequests(newMsgSetRq)
                    'Dim res As IResponse = msgSetRs.ResponseList.GetAt(0)

                    'If res.StatusSeverity = "Error" Then
                    '    If UI Then
                    '        MsgBox("Error adding employee " + objEmployee.EmployeeName + " Error Message: " + res.StatusMessage, MsgBoxStyle.OkOnly, "Error Adding Employee to Quickbooks")
                    '    Else
                    '        Throw New Exception(res.StatusMessage)
                    '    End If
                    'Else
                    '    My.Forms.MAIN.History("Added Name: " + TLEmployeeName.ToString + " with TimeLive ID: " + TL_ID.ToString + " to QuickBooks", "N")
                    'End If

                    msgSetRq = MAIN.SESSMANAGER.CreateMsgSetRequest("US", 2, 0)
                    msgSetRq.Attributes.OnError = ENRqOnError.roeContinue

                    EmployeeQueryRq = msgSetRq.AppendEmployeeQueryRq
                    EmployeeQueryRq.ORListQuery.FullNameList.Add(TLEmployeeName)

                    msgSetRs = MAIN.SESSMANAGER.DoRequests(msgSetRq)
                    response = msgSetRs.ResponseList.GetAt(0)
                    empRetList = response.Detail

                End If
            End If
            'Assume only one return

        Catch ex As Exception
            Throw ex
        End Try
        '========================
        'Send new created employee in QuckBook to TimeLive
        Dim obj_QBtoTl_Employee As New QBtoTL_Employee
        Dim employeeData As New QBtoTL_Employee.EmployeeDataStructureQB

        'Get the current List of Employee in Quickbooks
        employeeData = obj_QBtoTl_Employee.GetEmployeeQBData(Me, True)
        Reset_Checked_Employee_Value(employeeData)
        If employeeData IsNot Nothing Then
            For Each element As QBtoTL_Employee.Employee In employeeData.DataArray
                If element.QB_Name = objEmployee.QB_Name Then
                    element.RecSelect = True
                    Exit For
                End If
            Next
        End If
        'Select the new created vendor 
        obj_QBtoTl_Employee.QBTransferEmployeeToTL(employeeData, p_token, Me, True)
        display_UI()
    End Sub

    Private Sub btnNewVendor_Click(sender As Object, e As EventArgs) Handles btnNewVendor.Click

        Dim objEmployee As New QBtoTL_Vendor.Vendor(Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, Nothing)

        Get_Vendor_Form(objEmployee)
        'if users want to cancel create new vendor
        If objEmployee.RecSelect = False Then
            Exit Sub
        End If

        Dim UI As Boolean
        Dim TLEmployeeName As String = objEmployee.QB_Name

        Try
            Dim msgSetRq As IMsgSetRequest = MAIN.SESSMANAGER.CreateMsgSetRequest("US", 2, 0)

            msgSetRq.Attributes.OnError = ENRqOnError.roeContinue
            Dim VendorQueryRq As IVendorQuery = msgSetRq.AppendVendorQueryRq

            VendorQueryRq.ORVendorListQuery.FullNameList.Add(TLEmployeeName)
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
                    'vendorAdd.MiddleName.SetValue(If(objEmployee.MiddleName = Nothing, "", objEmployee.MiddleName))
                    'vendorAdd.OpenBalanceDate.SetValue(If(objEmployee.HiredDate = Nothing, Date.Now, objEmployee.HiredDate))
                    'vendorAdd.Phone.SetValue(If(objEmployee.Phone = Nothing, "", objEmployee.Phone))
                    'vendorAdd.Mobile.SetValue(If(objEmployee.Mobile = Nothing, "", objEmployee.Mobile)) ' Errors for some reason

                    ' Employee Address
                    'vendorAdd.VendorAddress.Addr1.SetValue(If(objEmployee.Address1 = Nothing, "", objEmployee.Address1))
                    'vendorAdd.VendorAddress.Addr2.SetValue(If(objEmployee.Address2 = Nothing, "", objEmployee.Address2))
                    'vendorAdd.VendorAddress.City.SetValue(If(objEmployee.City = Nothing, "", objEmployee.City))
                    'vendorAdd.VendorAddress.PostalCode.SetValue(If(objEmployee.PostalCode = Nothing, "", objEmployee.PostalCode))
                    'vendorAdd.VendorAddress.State.SetValue(If(objEmployee.State = Nothing, "", objEmployee.State))
                    'vendorAdd.VendorAddress.Country.SetValue(If(objEmployee.Country = Nothing, "", objEmployee.Country))


                    'step2: send the request
                    msgSetRs = MAIN.SESSMANAGER.DoRequests(newMsgSetRq)

                    ' Interpret the response
                    Dim res As IResponse
                    res = msgSetRs.ResponseList.GetAt(0)

                    If res.StatusSeverity = "Error" Then
                        Throw New Exception(res.StatusMessage)
                    End If

                    My.Forms.MAIN.History("Added Name: " + TLEmployeeName.ToString + " to QuickBooks", "N")

                    msgSetRq = MAIN.SESSMANAGER.CreateMsgSetRequest("US", 2, 0)
                    msgSetRq.Attributes.OnError = ENRqOnError.roeContinue

                    VendorQueryRq = msgSetRq.AppendVendorQueryRq
                    VendorQueryRq.ORVendorListQuery.FullNameList.Add(TLEmployeeName)

                    msgSetRs = MAIN.SESSMANAGER.DoRequests(msgSetRq)
                    response = msgSetRs.ResponseList.GetAt(0)
                    vendorRetList = response.Detail
                    'Else
                    '    Return False
                End If
                'Else
            End If

        Catch ex As Exception
            Throw ex
        End Try
        'Send new created vendor in QuckBook to TimeLive
        Dim obj_QBtoTl_Vendor As New QBtoTL_Vendor
        Dim VendorData As New QBtoTL_Vendor.VendorDataStructureQB
        'Get the current List of Vendor in Quickbooks
        VendorData = obj_QBtoTl_Vendor.GetVendorQBData(Me, True)
        Reset_Checked_Vendor_Value(VendorData)
        If VendorData IsNot Nothing Then
            For Each element As QBtoTL_Vendor.Vendor In VendorData.DataArray
                If element.QB_Name = objEmployee.QB_Name Then
                    element.RecSelect = True
                    Exit For
                End If
            Next
        End If
        'Select the new created vendor 
        obj_QBtoTl_Vendor.QBTransferVendorToTL(VendorData, p_token, Me, True)
        display_UI()

    End Sub

    Private Sub BtnTreeView_Click(sender As Object, e As EventArgs) Handles btnTreeView.Click


        'data for populating timelive treeview
        '==============================================
        ' Connect to TimeLive Client
        Dim objClientServices As Services.TimeLive.Clients.Clients = MAIN.connect_TL_clients(p_token)
        Dim objClientArray() As Object = objClientServices.GetClients()
        'Dim objClient As New Services.TimeLive.Clients.Client

        ' Connect to TimeLive Tasks
        Dim objTaskServices As Services.TimeLive.Tasks.Tasks = MAIN.connect_TL_tasks(p_token)
        Dim objTaskArray() As Object = objTaskServices.GetTasks
        'Dim objTask As New Services.TimeLive.Tasks.Task

        'Connect to TimeLive Projects
        Dim objProjectServices As Services.TimeLive.Projects.Projects = MAIN.connect_TL_projects(p_token)
        Dim objProjectArray() As Object = objProjectServices.GetProjects
        ' Dim objProject As New Services.TimeLive.Projects.Project

        'Create data structure in vb for task data  
        Dim taskDataArray As New List(Of Array)
        Dim firstTaskLevel As New List(Of Array)
        Dim secondTaskLevel As New List(Of Array)
        Dim thirdTaskLevel As New List(Of Array)



        'For Each item As Services.TimeLive.Tasks.Task In objTaskArray
        '    item.JobParent += ":" + item.TaskName
        '    Dim TaskArray() As String = Split(item.JobParent, ":")
        '    taskDataArray.Add(TaskArray)
        'Next

        For Each item As Services.TimeLive.Tasks.Task In objTaskArray
            item.JobParent += ":" + item.TaskName
            Dim TaskArray() As String = Split(item.JobParent, ":")
            Dim lengthTaskArray As Integer = TaskArray.Length
            If lengthTaskArray = 3 Then
                firstTaskLevel.Add(TaskArray)
            ElseIf lengthTaskArray = 4 Then
                secondTaskLevel.Add(TaskArray)
            Else
                thirdTaskLevel.Add(TaskArray)
            End If
            'taskDataArray.Add(TaskArray)
        Next
        taskDataArray.AddRange(firstTaskLevel)
        taskDataArray.AddRange(secondTaskLevel)
        taskDataArray.AddRange(thirdTaskLevel)
        ' limit only 5 level in tree view
        'Using tlTreeView As TreeView = New TreeView()

        '    tlTreeView.ShowDialog()
        'End Using

        'data for populating QuickBooks treeview
        '========================================================
        Dim obj1_QBtoTL_Customer As New QBtoTL_Customer
        Dim customerData As New QBtoTL_Customer.CustomerDataStructureQB

        Dim obj_QBtoTL_JobOrItem As New QBtoTL_JobOrItem
        Dim jobData As New QBtoTL_JobOrItem.JobDataStructureQB

        ' get customers and jobs data from quickbook
        customerData = obj1_QBtoTL_Customer.GetCustomerQBData()
        jobData = obj_QBtoTL_JobOrItem.GetJobSubJobData()


        Dim jobDataArray As New List(Of Array)
        For Each item As QBtoTL_JobOrItem.Job_Subjob In jobData.DataArray
            Dim TaskArray() As String = Split(item.FullName, ":")
            jobDataArray.Add(TaskArray)
        Next

        ' populate in QuickBook and TimeLive Tree View
        Using treeView As TreeView = New TreeView()
            'TimeLive-----------
            For Each client As Services.TimeLive.Clients.Client In objClientArray
                treeView.CustomerJobTLTreeView.Nodes.Add(client.ClientName, client.ClientName)
                For Each project As Services.TimeLive.Projects.Project In objProjectArray
                    If project.ClientName = client.ClientName Then
                        treeView.CustomerJobTLTreeView.Nodes(client.ClientName).Nodes.Add(project.ProjectName, project.ProjectName)
                        For i As Integer = 0 To taskDataArray.Count - 1
                            If taskDataArray(i)(1) = project.ProjectName Then
                                If taskDataArray(i).Length = 3 Then
                                    ' adding project node
                                    treeView.CustomerJobTLTreeView.Nodes(client.ClientName).Nodes(project.ProjectName).Nodes.Add(taskDataArray(i)(2), taskDataArray(i)(2))
                                ElseIf taskDataArray(i).Length = 4 Then
                                    treeView.CustomerJobTLTreeView.Nodes(client.ClientName).Nodes(project.ProjectName).Nodes(taskDataArray(i)(2)).Nodes.Add(taskDataArray(i)(3), taskDataArray(i)(3))
                                ElseIf taskDataArray(i).Length = 5 Then
                                    treeView.CustomerJobTLTreeView.Nodes(client.ClientName).Nodes(project.ProjectName.Trim).Nodes(taskDataArray(i)(2)).Nodes(taskDataArray(i)(3)).Nodes.Add(taskDataArray(i)(4), taskDataArray(i)(4))
                                    'ElseIf taskDataArray(i).Length = 6 Then
                                    '    tlTreeView.CustomerJobTLTreeView.Nodes(client.ClientName).Nodes(project.ProjectName).Nodes(taskDataArray(i)(2)).Nodes(taskDataArray(i)(3)).Nodes(taskDataArray(i)(4)).Nodes.Add(taskDataArray(i)(5), taskDataArray(i)(5))
                                Else
                                    'In timelive, users can create more than 5 level. However, Quickbook allows only 5 level including clients. Therefore, we should do the same thing in timelive  
                                    Throw New Exception("Created More than 5 level of a tree")
                                End If
                            End If
                        Next
                    End If
                Next
            Next
            'QuickBooks-------
            For Each customer As QBtoTL_Customer.Customer In customerData.DataArray
                treeView.CustomerJobQBTreeView.Nodes.Add(customer.QB_Name, customer.QB_Name)
                For i As Integer = 0 To jobDataArray.Count - 1
                    If jobDataArray(i)(0) = customer.QB_Name Then
                        Dim jobLength As Integer = jobDataArray(i).Length
                        If jobLength = 2 Then
                            treeView.CustomerJobQBTreeView.Nodes(customer.QB_Name).Nodes.Add(jobDataArray(i)(1), jobDataArray(i)(1))
                            Continue For
                        ElseIf jobLength = 3 Then
                            treeView.CustomerJobQBTreeView.Nodes(customer.QB_Name).Nodes(jobDataArray(i)(1)).Nodes.Add(jobDataArray(i)(2), jobDataArray(i)(2))
                            Continue For
                        ElseIf jobLength = 4 Then
                            treeView.CustomerJobQBTreeView.Nodes(customer.QB_Name).Nodes(jobDataArray(i)(1)).Nodes(jobDataArray(i)(2)).Nodes.Add(jobDataArray(i)(3), jobDataArray(i)(3))
                            Continue For
                        Else
                            treeView.CustomerJobQBTreeView.Nodes(customer.QB_Name).Nodes(jobDataArray(i)(1)).Nodes(jobDataArray(i)(2)).Nodes(jobDataArray(i)(3)).Nodes.Add(jobDataArray(i)(4), jobDataArray(i)(4))
                            Continue For
                        End If
                    End If
                Next
            Next
            ' expand tree 
            'For Each n As TreeNode In treeView.CustomerJobQBTreeView.Nodes
            '    n.Expand()
            'Next

            treeView.ShowDialog()

        End Using

    End Sub


End Class