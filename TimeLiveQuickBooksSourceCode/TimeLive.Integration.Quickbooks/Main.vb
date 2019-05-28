Imports System.Windows.Forms
Imports System.Threading
Imports QBFC13Lib

Imports System.Data.SqlClient
Imports System.Text
Imports System.Globalization

Imports System.Net.Mail
Public Class MAIN

    Private p_token As String
    Private p_AccountId As String
    <ThreadStatic> Public Shared SESSMANAGER As QBSessionManager
    Public Shared colonReplacer = " --> "
    Public Shared TIMERTHREAD As Threading.Thread

    Private LoggedIn As Boolean
    Private Type As Integer
    Private emailBody As String
    Private cur_week

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

    Dim selectedEmployeeData As New TLtoQB_TimeEntry.EmployeeDataStructure

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
                    History("", "n")
                Else
                    LoggedIn = False
                    History("You will need to log into TimeLive before using this utility.", "n")
                    History("", "n")
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
            btn_currentweek_Click(SENDER, E)

            'for type Customers, Employees, Vendors, Jobs/Subjobs, and Items/Subitems
            If Type >= 10 And Type < 15 Then
                ReadItems = display_UI()
            End If

            'for type Time Items
            ' Might add this to display_UI() or as its own private function
            If Type = 20 Then
                'ReadItems = display_TimeEntry_UI()
                ReadItems = display_TimeEntry_UI()
            End If

            If LoggedIn Then
                History(ReadItems.ToString() + " items were read from Quickbooks", "n")
            End If

        Catch EX As Exception
            MsgBox(EX.Message)
            Me.Close()
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
        End Select

        'for type Customers, Employees, Vendors, Jobs/Subjobs, and Items/Subitems
        If Type >= 10 And Type < 15 Then
            ReadItems = display_UI()
        End If

        'for type Time Items
        ' Might add this to display_UI() or as its own private function
        If Type = 20 Then
            'ReadItems = display_TimeEntry_UI()
            ReadItems = display_TimeEntry_UI()
        End If

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
            'Dim result As Integer = DateTime.Compare(Convert.ToDateTime(My.Settings.AutoRunTime), DateTime.Now)
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
        'Dim SESSMANAGER As QBSessionManager ' Made global
        Try
            SESSMANAGER = New QBSessionManagerClass()
            'SESSMANAGER.OpenConnection("APP", "TIMELIVE QUICKBOOKS") 
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

    'Private Sub settingbtn_Click(sender As Object, e As EventArgs) Handles settingbtn.Click
    '    Using newsettingsForm = New AppSettings()
    '        If DialogResult.OK = newsettingsForm.ShowDialog() Then
    '            Dim NextRunDateTime As Date = Convert.ToDateTime(My.Settings.AutoRunTime)
    '            NextProcessingTime.Text = "Auto Processing Time: " + NextRunDateTime.ToString("MM/dd/yy HH:mm")
    '        End If
    '    End Using
    'End Sub

    'Private Sub loginbtn_Click(sender As Object, e As EventArgs) Handles loginbtn.Click
    '    If p_token Is Nothing Then
    '        Using newForm = New Login()
    '            If DialogResult.OK = newForm.ShowDialog() Then
    '                p_token = newForm.ReturnValue1
    '                p_AccountId = newForm.ReturnValue2
    '            End If
    '        End Using
    '    Else
    '        MsgBox("You are already logged in")
    '    End If
    'End Sub

    'Private Sub clearlogbtn_Click(sender As Object, e As EventArgs) Handles clearlogbtn.Click
    '    StatusWindow.Clear()
    'End Sub

    'Private Sub Exitbtn_Click(sender As Object, e As EventArgs) Handles Exitbtn.Click
    '    QUITQBSESSION() ' Close QB session before exiting
    '    If TIMERTHREAD IsNot Nothing Then
    '        TIMERTHREAD.Abort()
    '    End If
    '    Me.Close()
    'End Sub


    ' Will be using the following integers for the different types of items that are synched
    ' 0 - customers
    ' 1 - employees
    ' 2 - vendors
    ' 3 - jobs_items
    ' 4 - ODC and travel accounts 
    ' 5 - Time Transfer 
    ' 6 - Customer Expenses
    'Private Sub customer_btn_Click(sender As Object, e As EventArgs) Handles customer_btn.Click
    '    Dim IntegratedUI As New IntegratedUI
    '    IntegratedUI.Owner = Me
    '    IntegratedUI.Show(p_token, p_AccountId, 10)
    'End Sub

    'Private Sub employees_btn_Click(sender As Object, e As EventArgs) Handles employees_btn.Click
    '    Dim IntegratedUI As New IntegratedUI
    '    IntegratedUI.Owner = Me
    '    IntegratedUI.Show(p_token, p_AccountId, 11)
    'End Sub

    'Private Sub timeentries_btn_Click(sender As Object, e As EventArgs) Handles timeentries_btn.Click
    '    ' excluded from this project. 

    '    'Dim QBTimeTracking As New QBTimeTracking
    '    IntegratedUI.Owner = Me
    '    IntegratedUI.Show(p_token, p_AccountId, 20)
    'End Sub


    Public Shared Sub SendGMail(Subject__1 As String, BodyText As String, UI As Boolean)
        'Specify senders gmail address
        Dim SendersAddress As String = "teltrium@gmail.com"
        'Specify The Address You want to send Email To(can be any valid email address)
        Dim ReceiversAddress As String = "operations@teltrium.com"

        'Specify The password of gmail account u are using to sent mail(pw of sender@gmail.com)
        If UI Then
            MsgBox("--> Sending email to: " + ReceiversAddress + " From: " + SendersAddress)
        End If
        Dim SendersPassword As String = "1October2014"

        'Write the contents of your mail
        Dim body As String = BodyText
        Try
            Dim smtp As New SmtpClient()
            With smtp
                'used to be Key.Host

                .Host = "smtp.gmail.com"
                .Port = 587
                .EnableSsl = True
                .DeliveryMethod = SmtpDeliveryMethod.Network

                .Credentials = New Net.NetworkCredential(SendersAddress, SendersPassword)
                .Timeout = 3000
            End With
            'MailMessage represents a mail message
            'it is 4 parameters(From,TO,subject,body)
            Dim message As New MailMessage(SendersAddress, ReceiversAddress, Subject__1, body)
            'WE use smtp sever we specified above to send the message(MailMessage message)

            smtp.Send(message)
            If UI Then
                MsgBox("Sent!")
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

    Public Sub History(ByVal input As String, Type As String) ' Change to Type to "Char" after testing
        Dim can_display As Boolean = If(My.Settings.DebugMode Is Nothing Or My.Settings.DebugMode = "", True, CBool(My.Settings.DebugMode))
        If can_display Then
            If String.Compare("n", Type, False) = 0 Then
                StatusWindow.Text += vbNewLine + ">> " + input
            ElseIf String.Compare("N", Type, False) = 0 Then
                Dim s As String = vbNewLine + "***********************" + vbNewLine + ">> " + input + vbNewLine + "***********************"
                StatusWindow.Text += s
            ElseIf String.Compare("c", Type, True) = 0 Then
                StatusWindow.Text += ", " + input
            ElseIf String.Compare("i", Type, False) = 0 Then
                StatusWindow.Text += vbNewLine + vbTab + "- " + input
            ElseIf String.Compare("I", Type, False) = 0 Then
                Dim s As String = vbNewLine + vbTab + "***********************" + vbNewLine + vbTab + "- " + input + vbNewLine + vbTab + "***********************"
                StatusWindow.Text += s
            End If

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

    'Private Sub btn_systemsync_Click(sender As Object, e As EventArgs) Handles btn_systemsync.Click
    '    If MsgBox("Are you to perform a sync?", MsgBoxStyle.YesNo, "Warning!") = MsgBoxResult.Yes Then ' If you select yes in the MsgBox then it will close the window
    '        Dim CurrentSystemSync As New CurrentSystemSync
    '        CurrentSystemSync.PassToken(p_token, p_AccountId)
    '    End If
    'End Sub

    'Private Sub contractor_btn_Click(sender As Object, e As EventArgs) Handles vendor_btn.Click
    '    Dim IntegratedUI As New IntegratedUI
    '    IntegratedUI.Owner = Me
    '    IntegratedUI.Show(p_token, p_AccountId, 12)
    'End Sub

    'Private Sub jobs_items_btn_Click(sender As Object, e As EventArgs) Handles jobs_items_btn.Click
    '    Dim IntegratedUI As New IntegratedUI
    '    IntegratedUI.Owner = Me
    '    If Not (My.Settings.JobORItemHierarchy Is Nothing Or My.Settings.JobORItemHierarchy = "") Then
    '        If My.Settings.JobORItemHierarchy = 0 Then
    '            IntegratedUI.Show(p_token, p_AccountId, 13)
    '        Else
    '            IntegratedUI.Show(p_token, p_AccountId, 14)
    '        End If
    '    Else
    '        MsgBox("Seems that settings are not set. Please set application settings.")
    '    End If
    'End Sub

    'Private Sub btn_relationships_Click(sender As Object, e As EventArgs) Handles btn_relationships.Click
    '    ChargingRelationship.Owner = Me
    '    ChargingRelationship.Show()
    'End Sub

    Private Sub MAIN_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        Me.Dispose()
    End Sub

    Private Sub QBTime2SQLbtn_Click(sender As Object, e As EventArgs)
        Dim CurrentSystemSync As New CurrentSystemSync
        CurrentSystemSync.GetTime()
    End Sub

    Private Function display_TimeEntry_UI() Handles RefreshTimeTransfer.Click
        If Not LoggedIn Then
            Return 0
        End If

        Dim ItemLastSync As DateTime
        ' Delete all rows and columns from the DataGridView's except the "Check Name" column in DataGridView1
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

        TabPageTimeTransfer.Visible = True
        AttributeTabControl.SelectedIndex = 4

        SyncFromLabel.Text = "Employees"
        SyncToLabel.Text = "Time"

        'load grid 1
        Dim col2 As New DataGridViewTextBoxColumn
        col2.Name = "Name"
        DataGridView1.Columns.Add(col2)
        Dim col3 As New DataGridViewTextBoxColumn
        col3.Name = "Hours"
        DataGridView1.Columns.Add(col3)

        SelectAllCheckBox.Checked = True

        If String.IsNullOrEmpty(My.Settings.TimeTrackingLastSync.ToString()) Then
            ItemLastSync = #1/1/2000#
        Else
            ItemLastSync = Convert.ToDateTime(My.Settings.TimeTrackingLastSync)
        End If

        History("Synchonizing time entries items since:   " + ItemLastSync.ToString(), "n")

        ' Add all employees with their total hours worked to selectedEmployeeData only if it is empty
        selectedEmployeeData.NoItems = 0
        selectedEmployeeData.DataArray = New List(Of TLtoQB_TimeEntry.Employee)
        'If selectedEmployeeData.NoItems = 0 Then
        ' Connect to TimeLive employees
        Dim objEmployeeServices As New Services.TimeLive.Employees.Employees
        Dim authentication As New Services.TimeLive.Employees.SecuredWebServiceHeader
        authentication.AuthenticatedToken = p_token
        objEmployeeServices.SecuredWebServiceHeaderValue = authentication

        ' Connect to TimeLive Time Entries to get total hours per employee
        Dim objTimeTrackingServices As New Services.TimeLive.TimeEntries.TimeEntries
        Dim authentication2 As New Services.TimeLive.TimeEntries.SecuredWebServiceHeader
        authentication2.AuthenticatedToken = p_token
        objTimeTrackingServices.SecuredWebServiceHeaderValue = authentication2

        Dim employees As New DataTable
        employees = objEmployeeServices.GetEmployeesData

        For Each row As DataRow In employees.Rows
            selectedEmployeeData.NoItems += 1
            Dim emplID = row("AccountEmployeeId")
            Dim emplName = row("FullName")
            Dim hoursWorked As Double = GetTotalHours(emplID, objTimeTrackingServices, dpStartDate.Value, dpEndDate.Value)
            selectedEmployeeData.DataArray.Add(New TLtoQB_TimeEntry.Employee(True, row("FullName"), emplID, hoursWorked))
            DataGridView1.Rows.Add(True, emplName, hoursWorked)
        Next
        'End If

        Dim items_read As Integer = selectedEmployeeData.NoItems

        'For Each element As TLtoQB_TimeEntry.Employee In selectedEmployeeData.DataArray
        'History("Debug:   " + element.RecSelect.ToString(), "n")
        'DataGridView1.Rows.Add(element.RecSelect, element.FullName.ToString(), element.HoursWorked.ToString())
        'Next

        Time_Entry_Times()

        System.Threading.Thread.Sleep(150)
        System.Windows.Forms.Application.DoEvents()
        ProgressBar1.Value = 0

        Return items_read
    End Function

    Private Function display_UI(Optional sender As Object = Nothing, Optional e As EventArgs = Nothing) Handles QBtoTLCustomerRadioButton.CheckedChanged, QBtoTLEmployeeRadioButton.CheckedChanged,
                                                                                                                QBtoTLVendorRadioButton.CheckedChanged, QBtoTLJobItemRadioButton.CheckedChanged,
                                                                                                                RefreshCustomers.Click, RefreshEmployees.Click, RefreshVendors.Click,
                                                                                                                RefreshJobsOrItems.Click
        If Not LoggedIn Then
            Return 0
        End If

        UpdateTimeTransfer.Visible = False

        Dim ItemLastSync As DateTime
        Dim lastSync As String
        Dim Data
        Dim attribute As String
        Dim QBtoTLRadioButton As RadioButton

        'TransferTimeButton.Visible = False
        TimeEntrySelectAll.Visible = False
        ' Unselect the select all check box
        SelectAllCheckBox.Checked = False

        Select Case Type
            ' Customers
            Case 10
                TabPageCustomers.Visible = True
                AttributeTabControl.SelectedIndex = 0
                lastSync = My.Settings.CustomerLastSync
                customerData = customer_qbtotl.GetCustomerQBData(Me, True)
                Data = customerData
                attribute = "customer"
                QBtoTLRadioButton = QBtoTLCustomerRadioButton

            ' Employees
            Case 11
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
                TabPageVendor.Visible = True
                AttributeTabControl.SelectedIndex = 2
                lastSync = My.Settings.VendorLastSync
                vendorData = vendor_qbtotl.GetVendorQBData(Me, True)
                Data = vendorData
                attribute = "vendor"
                QBtoTLRadioButton = QBtoTLVendorRadioButton

            ' Jobs / Subjobs
            Case 13
                TabPageJobsItems.Visible = True
                AttributeTabControl.SelectedIndex = 3
                lastSync = My.Settings.JobLastSync
                JobData = job_qbtotl.GetJobSubJobData(Me, p_token, True)
                Data = JobData
                attribute = "job/subjob"
                QBtoTLRadioButton = QBtoTLJobItemRadioButton

            ' Items / SubItems
            Case 14
                TabPageJobsItems.Visible = True
                AttributeTabControl.SelectedIndex = 3
                lastSync = My.Settings.ItemlastSync
                JobData = job_qbtotl.GetItemSubItemData(Me, p_token, True)
                Data = JobData
                attribute = "item/subitem"
                QBtoTLRadioButton = QBtoTLJobItemRadioButton

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
        History("Synchonizing modified " + attribute + " since: " + ItemLastSync.ToString(), "n")

        ' Delete all rows and columns from the DataGridView's except the "Check Name" column in  DataGridView1
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

        '-----------------------------------------
        'load grid for QuickBooks (might be easier way)
        '-----------------------------------------

        Dim QBDataGridView As DataGridView = If(QBtoTLRadioButton.Checked, DataGridView1, DataGridView2)
        Dim TLDataGridView As DataGridView = If(QBtoTLRadioButton.Checked, DataGridView2, DataGridView1)

        ' Add Full Name Column for Job/Subjob and Item/SubItem
        If Type = 13 Or Type = 14 Then
            Dim QBcol0 As New DataGridViewTextBoxColumn
            QBcol0.Name = "Full Name"
            QBDataGridView.Columns.Add(QBcol0)
        End If

        Dim QBcol1 As New DataGridViewTextBoxColumn
        QBcol1.Name = "Name"
        QBDataGridView.Columns.Add(QBcol1)
        Dim QBcol2 As New DataGridViewTextBoxColumn
        QBcol2.Name = "Last Modified"
        QBDataGridView.Columns.Add(QBcol2)
        Dim QBcol3 As New DataGridViewTextBoxColumn
        QBcol3.Name = "New"
        QBDataGridView.Columns.Add(QBcol3)

        Dim readItems As Integer = 0

        Dim element
        If Data Is Nothing Then
            History("No QuickBooks " + attribute + " data", "n")
        Else
            ' Note: Currently only customer stored both active and inactive; If changed, then change this line accordingly
            readItems = If(Type = 10, Data.NoItems - Data.NoInactive, Data.NoItems)

            For Each element In Data.DataArray
                Dim result As Integer = DateTime.Compare(Convert.ToDateTime(element.QBModTime.ToString()),
                ItemLastSync)
                If result >= 0 Then
                    element.RecSelect = True
                End If
                'MAIN.FlagChangedItemsResults(element.QB_Name.ToString(), result)
            Next
            For Each element In Data.DataArray
                ' Currently only customer has the enabled field, if all do then remove if type = 10
                If Type = 10 Then
                    If Not element.Enabled Then
                        Continue For
                    End If
                End If

                ' Jobs/Subjobs and Items/Subitems show full name too
                If Type = 13 Or Type = 14 Then
                    Dim full_name As String = element.FullName.ToString().Replace(":", MAIN.colonReplacer)
                    If QBtoTLRadioButton.Checked Then
                        QBDataGridView.Rows.Add(element.RecSelect, full_name, element.QB_Name.ToString(), element.QBModTime.ToString(), element.NewlyAdded)
                    Else
                        QBDataGridView.Rows.Add(full_name, element.QB_Name.ToString(), element.QBModTime.ToString(), element.NewlyAdded)
                    End If
                Else
                    If QBtoTLRadioButton.Checked Then
                        QBDataGridView.Rows.Add(element.RecSelect, element.QB_Name.ToString(), element.QBModTime.ToString(), element.NewlyAdded)
                    Else
                        QBDataGridView.Rows.Add(element.QB_Name.ToString(), element.QBModTime.ToString(), element.NewlyAdded)
                    End If
                End If
            Next
        End If

        '-----------------------------------------
        ' Load Grid for TimeLive
        '-----------------------------------------
        Dim TLcol1 As New DataGridViewTextBoxColumn
        TLcol1.Name = "Name"
        TLDataGridView.Columns.Add(TLcol1)

        Dim TLcol2 As New DataGridViewTextBoxColumn
        TLcol2.Name = "New"
        TLDataGridView.Columns.Add(TLcol2)

        Dim TLItemsArray() As Object = {}

        Dim objServices = Nothing
        ' objServices2 only used for Jobs/Subjobs (and maybe items/subitems too), will be Tasks and objServices wil be projects
        Dim objServices2 = Nothing

        ' Initialize the Service and get all items of the specified attribute
        Select Case Type
            ' Customers
            Case 10
                Dim objClientServices As New Services.TimeLive.Clients.Clients
                Dim authentication As New Services.TimeLive.Clients.SecuredWebServiceHeader
                authentication.AuthenticatedToken = p_token
                objClientServices.SecuredWebServiceHeaderValue = authentication
                objServices = objClientServices
                TLItemsArray = objServices.GetClients()

            ' Employees, Vendors
            Case 11, 12
                Dim objEmployeeServices As New Services.TimeLive.Employees.Employees
                Dim authentication As New Services.TimeLive.Employees.SecuredWebServiceHeader
                authentication.AuthenticatedToken = p_token
                objEmployeeServices.SecuredWebServiceHeaderValue = authentication
                objServices = objEmployeeServices
                TLItemsArray = objServices.GetEmployees()

            ' Jobs/SubJobs | Items/SubItems (Projects/Tasks in TimeLive)
            Case 13, 14
                Dim list As List(Of Object) = New List(Of Object)

                Dim objProjectServices As New Services.TimeLive.Projects.Projects
                Dim authentication1 As New Services.TimeLive.Projects.SecuredWebServiceHeader
                authentication1.AuthenticatedToken = p_token
                objProjectServices.SecuredWebServiceHeaderValue = authentication1
                objServices = objProjectServices
                TLItemsArray = objServices.GetProjects()
                list.AddRange(TLItemsArray)

                Dim objTaskServices As New Services.TimeLive.Tasks.Tasks
                Dim authentication2 As New Services.TimeLive.Tasks.SecuredWebServiceHeader
                authentication2.AuthenticatedToken = p_token
                objTaskServices.SecuredWebServiceHeaderValue = authentication2
                objServices2 = objTaskServices
                list.AddRange(objServices2.GetTasks())

                TLItemsArray = list.ToArray()

        End Select

        ' Populate the table based on the TineLive elements
        If ProgressBar1.Maximum = Nothing Then
            ProgressBar1.Maximum = 0
        End If

        ProgressBar1.Maximum += If(TLItemsArray Is Nothing, 0, TLItemsArray.Length)

        If TLItemsArray Is Nothing Or TLItemsArray.Length = 0 Then
            History("No TimeLive " + attribute + " data", "n")
        End If

        Dim CustomerAdapter As New QB_TL_IDsTableAdapters.CustomersTableAdapter()
        Dim EmployeeAdapter As New QB_TL_IDsTableAdapters.EmployeesTableAdapter()
        Dim VendorAdapter As New QB_TL_IDsTableAdapters.VendorsTableAdapter()
        Dim Job_SubJobAdapter As New QB_TL_IDsTableAdapters.Jobs_SubJobsTableAdapter()
        Dim Item_SubItemAdapter As New QB_TL_IDsTableAdapters.Items_SubItemsTableAdapter()

        For Each element In TLItemsArray
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
                    name = element.EmployeeName
                    ID = objServices.GetEmployeeId(name)
                    ' Do not show vendors
                    If (VendorAdapter.numVendorsWithTL_ID(ID)) Then
                        Continue For
                    End If
                    isNew = If(EmployeeAdapter.numEmployeesWithTL_ID(ID), "", "N")

                ' Vendors 
                Case 12
                    name = element.EmployeeName
                    ID = objServices.GetEmployeeId(name)
                    ' Do not show employees
                    If (EmployeeAdapter.numEmployeesWithTL_ID(ID)) Then
                        Continue For
                    End If
                    isNew = If(VendorAdapter.numVendorsWithTL_ID(ID), "", "N")

                ' Jobs/Subjobs
                Case 13
                    If element.GetType Is (New Services.TimeLive.Projects.Project).GetType Then
                        name = element.ClientName + MAIN.colonReplacer + element.projectName
                        ID = objServices.GetProjectId(element.projectName)
                    Else
                        name = element.JobParent.replace(":", MAIN.colonReplacer) + MAIN.colonReplacer + element.TaskName
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
                        Continue For
                    End If
                    isNew = If(Job_SubJobAdapter.numTasksSubTasksWithTL_ID(ID), "", "N")

                ' Items/SubItems
                Case 14
                    ' TODO: will need to change name to full path
                    If element.GetType Is (New Services.TimeLive.Projects.Project).GetType Then
                        name = element.projectName
                        ID = objServices.GetProjectId(element.projectName)
                    Else
                        name = element.TaskName
                        ID = objServices2.GetTaskId(element.TaskName)
                    End If
                    ' Do not show Jobs
                    If Job_SubJobAdapter.numTasksSubTasksWithTL_ID(ID) Then
                        Continue For
                    End If
                    isNew = If(Item_SubItemAdapter.numItemsSubItemsWithTL_ID(ID), "", "N")
            End Select

            ' Checks if TLDataGridView is DataGridView1 or DataGridView2
            If QBtoTLRadioButton.Checked Then
                TLDataGridView.Rows.Add(name, isNew)
            Else
                TLDataGridView.Rows.Add(False, name, isNew)
            End If

            ProgressBar1.Value += 1
        Next

        System.Threading.Thread.Sleep(150)
        System.Windows.Forms.Application.DoEvents()
        ProgressBar1.Value = 0
        Return readItems
    End Function

    Private Sub Time_Entry_Times()
        DataGridView2.AutoSize = False
        DataGridView2.AutoSizeRowsMode = False

        DataGridView2.Rows.Clear()
        DataGridView2.Columns.Clear()

        UpdateTimeTransfer.Visible = True

        ' load grid 2
        Dim col1 As New DataGridViewCheckBoxColumn
        col1.Name = "ckBox"
        col1.HeaderText = "Check Box"
        DataGridView2.Columns.Add(col1)
        Dim col2 As New DataGridViewTextBoxColumn
        col2.Name = "Employee"
        DataGridView2.Columns.Add(col2)
        Dim col3 As New DataGridViewTextBoxColumn
        col3.Name = "Date"
        DataGridView2.Columns.Add(col3)
        Dim col4 As New DataGridViewTextBoxColumn
        col4.Name = "Task"
        DataGridView2.Columns.Add(col4)
        'Dim col3 As New DataGridViewTextBoxColumn
        'col3.Name = "Customer"
        'DataGridView2.Columns.Add(col3)
        'Dim col4 As New DataGridViewTextBoxColumn
        'col4.Name = "Job"
        'DataGridView2.Columns.Add(col4)
        'Dim col5 As New DataGridViewTextBoxColumn
        'col5.Name = "SubJob"
        'DataGridView2.Columns.Add(col5)
        Dim col5 As New DataGridViewTextBoxColumn
        col5.Name = "Time"
        DataGridView2.Columns.Add(col5)
        Dim col6 As New DataGridViewTextBoxColumn
        col6.Name = "Class"
        DataGridView2.Columns.Add(col6)
        Dim col7 As New DataGridViewTextBoxColumn
        col7.Name = "Payroll Item"
        DataGridView2.Columns.Add(col7)
        Dim col8 As New DataGridViewTextBoxColumn
        col8.Name = "Item SubItem"
        DataGridView2.Columns.Add(col8)
    End Sub

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
        'If token Is Nothing Then
        'MsgBox("Please Login")
        'Else
        'p_AccountId = AccountId
        'p_token = token
        MyBase.Show()

        'Commented out, changes the display ordering which affects the check box matching with the respective row
        'DataGridView1.Sort(DataGridView1.Columns(1), System.ComponentModel.ListSortDirection.Ascending)

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

    ''' <summary>
    ''' Displays the Time Entries for the selected employees within the specified time range
    ''' - White rows correspond to approved time entries
    ''' - Gray rows correspond to non-approved time entries
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub btnUpdateTimeTransfer_Click(sender As Object, e As EventArgs) Handles UpdateTimeTransfer.Click
        If Not LoggedIn Then
            Exit Sub
        End If

        'TransferTimeButton.Visible = True
        TimeEntrySelectAll.Visible = True
        TimeEntrySelectAll.Checked = True
        Reset_Checked_SelectedEmployee_Value(selectedEmployeeData)
        Set_Selected_SelectedEmployee()
        Time_Entry_Times()
        TimeEntryData.clear()
        Dim StartDate As DateTime = CDate(dpStartDate.Value).Date
        Dim endDate As DateTime = CDate(dpEndDate.Value).Date

        Dim payroll_id_names As Dictionary(Of String, String) = Payroll_IDName_Dict()
        Dim items_id_names As Dictionary(Of String, String) = ItemSubItem_IDName_Dict()

        ProgressBar1.Maximum = selectedEmployeeData.NoItems
        For Each element As TLtoQB_TimeEntry.Employee In selectedEmployeeData.DataArray
            With element
                If element.RecSelect = True Then
                    History("Processing: " + element.FullName.ToString(), "n")
                    LoadSelectedTimeEntryItems(element.AccountEmployeeId, element.FullName, DataGridView2, StartDate, endDate, True, payroll_id_names, items_id_names)
                    'deselect as not to load again
                    element.RecSelect = False
                    'Exit For
                End If
            End With
            ProgressBar1.Value += 1
        Next
        'IntUI_2ndSelect.Owner = Me
        'IntUI_2ndSelect.Show(p_token, p_AccountId, selectedEmployeeData,
        'CDate(dpStartDate.Value).Date,
        'CDate(dpEndDate.Value).Date.ToString, 201)
        'wait for one second so user can see progress bar
        System.Threading.Thread.Sleep(150)
        System.Windows.Forms.Application.DoEvents()

        ProgressBar1.Value = 0
        My.Settings.TimeTrackingLastSync = DateTime.Now.ToString()
        My.Settings.Save()
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

        'When processing customers
        If Type = 10 Then
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
        End If

        'When processing Employees
        If Type = 11 Then
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
        End If

        'When processing vendor
        If Type = 12 Then
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
        End If

        'When processing Jobs or Items
        If Type = 13 Or Type = 14 Then
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
        End If

        'When processing items
        'If Type = 14 Then
        '    Reset_Checked_Job_Value(JobData)
        '    If QBtoTLJobItemRadioButton.Checked Then
        '        Set_Selected_Job_Item()
        '        ItemsProcessed = job_qbtotl.QBTransferItemsToTL(JobData, p_token, Me, True)
        '        History(ItemsProcessed.ToString() + " TimeLive records were created or updated", "i")
        '        My.Settings.ItemlastSync = DateTime.Now.ToString()
        '        My.Settings.Save()
        '    Else
        '    End If
        'End If

        'When processing Time Transfer
        If Type = 20 Then
            Reset_Checked_TimeEntry_Value(TimeEntryData)
            Set_Selected_TimeEntry(DataGridView2)

            ' Transfer Time Entry data from TL to QB
            ItemsProcessed = timeentry_tltoqb.TLTransferTimeToQB(TimeEntryData, p_token, Me, True)
            'IntUI_2ndSelect.time_transfer(DataGridView2, Me)
            History(ItemsProcessed.ToString() + If(ItemsProcessed = 1, " Time Entry was", " Time Entries were") + " created or updated", "i")

        Else
            ' Refresh after processing
            display_UI()
        End If

        'wait for one second so user can see progress bar
        System.Threading.Thread.Sleep(150)
        System.Windows.Forms.Application.DoEvents()

        ProgressBar1.Value = 0
    End Sub

    ''' <summary>
    ''' Gets the total number of hours worked for the employee with the given ID from startDate to endDate
    ''' </summary>
    ''' <param name="emplID"></param>
    ''' <param name="TLTimeTracker"></param>
    ''' <param name="startDate"></param>
    ''' <param name="endDate"></param>
    ''' <returns></returns>
    Private Function GetTotalHours(emplID As String, TLTimeTracker As Services.TimeLive.TimeEntries.TimeEntries,
                                  startDate As DateTime, endDate As DateTime) As Integer
        Dim times() As Object = TLTimeTracker.GetTimeEntriesByEmployeeIdAndDateRange(emplID, startDate, endDate)
        Dim totalHours As Double = 0
        Dim time
        For Each time In times
            totalHours += TotalTimeToHours(time.TotalTime)
        Next

        Return totalHours
    End Function

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
                'History("Time selection:  Reseting employees:   " + element.RecSelect.ToString(), "n")
            Next
        End If
    End Sub

    Private Sub Reset_Checked_TimeEntry_Value(ByRef TimeEntryObj As TLtoQB_TimeEntry.TimeEntryDataStructureQB)
        ' reset the check value to zero
        For Each element As TLtoQB_TimeEntry.TimeEntry In TimeEntryObj.DataArray
            element.RecSelect = False
        Next
    End Sub

    Private Sub Set_Selected_TimeEntry(ByRef DataGridView As DataGridView)
        ProgressBar1.Maximum = DataGridView.Rows.Count
        For Each row As DataGridViewRow In DataGridView.Rows
            If row.Cells("Date").Value IsNot Nothing And row.Cells("ckBox").Value And TimeEntryData.NoItems Then
                Dim i = 0
                Dim full_name As String = row.Cells("Task").Value.ToString()
                TimeEntryData.DataArray.ForEach(
                    Sub(timeentry)
                        If (timeentry.EmployeeName = row.Cells("Employee").Value.ToString And
                           timeentry.CustomerName + MAIN.colonReplacer + timeentry.ProjectName + MAIN.colonReplacer + timeentry.TaskWithParent.Replace(":", MAIN.colonReplacer) = full_name And
                           timeentry.TimeEntryDate.ToString("MM/dd/yyyy") = row.Cells("Date").Value.ToString) Then
                            i += 1
                            History("Selected for processing: " + row.Cells("Employee").Value.ToString + " with task " + row.Cells("Task").Value.ToString + " on " + row.Cells("Date").Value.ToString, "n")
                            timeentry.RecSelect = True
                        End If
                    End Sub
                )
                'TimeEntryData.DataArray(row.Index).RecSelect = True
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

    Private Sub QB_Set_Selected_Customer()
        If DataGridView1 IsNot Nothing Then
            For Each row As DataGridViewRow In DataGridView1.Rows
                If row.Cells("Name").Value IsNot Nothing And row.Cells("ckBox").Value = True Then
                    customerData.DataArray.ForEach(
                        Sub(customer)
                            If customer.QB_Name = row.Cells("Name").Value.ToString Then
                                customer.RecSelect = True
                            End If
                        End Sub
                    )
                    History("Customers selected for processing: " + row.Cells("Name").Value, "n")
                End If
            Next
        End If
    End Sub

    Private Sub Set_Selected_Employee()
        If DataGridView1 IsNot Nothing Then
            For Each row As DataGridViewRow In DataGridView1.Rows
                If row.Cells("Name").Value IsNot Nothing And row.Cells("ckBox").Value = True Then
                    employeeData.DataArray.ForEach(
                        Sub(employee)
                            If employee.QB_Name = row.Cells("Name").Value.ToString Then
                                employee.RecSelect = True
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

    Private Sub Set_Selected_Vendor()
        If DataGridView1 IsNot Nothing Then
            For Each row As DataGridViewRow In DataGridView1.Rows
                If row.Cells("Name").Value IsNot Nothing And row.Cells("ckBox").Value Then
                    vendorData.DataArray.ForEach(
                        Sub(vendor)
                            If vendor.QB_Name = row.Cells("Name").Value.ToString Then
                                vendor.RecSelect = True
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

    Private Sub selectall_checkbox(sender As Object, e As EventArgs) Handles SelectAllCheckBox.CheckedChanged
        If DataGridView1 IsNot Nothing Then
            For Each row As DataGridViewRow In DataGridView1.Rows
                If row.Cells("Name").Value IsNot Nothing Then
                    row.Cells("ckBox").Value = SelectAllCheckBox.Checked
                End If
            Next
        End If
    End Sub

    Private Sub timeEntry_selectall_checkbox(sender As Object, e As EventArgs) Handles TimeEntrySelectAll.CheckedChanged
        If DataGridView2 IsNot Nothing Then
            For Each row As DataGridViewRow In DataGridView2.Rows
                If row.Cells("Employee").Value IsNot Nothing Then
                    row.Cells("ckBox").Value = TimeEntrySelectAll.Checked
                End If
            Next
        End If
    End Sub

    Public Sub LoadSelectedTimeEntryItems(AccountEmployeeId As String, EmployeeName As String, ByRef DataGridView As DataGridView,
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

                    Dim ServiceDisp As String = If(item_dict Is Nothing, If(.ServiceName Is Nothing, .ServiceItem, .ServiceName),
                                                                         If(item_dict.ContainsKey(.ServiceItem), item_dict(.ServiceItem), .ServiceItem))

                    ' Check if a time entry has yet to be approved
                    Dim TimeEntryApproved As Boolean = TL_TimeEntries.GetTimeApproval(AccountEmployeeId, .TimeEntryDate)

                    Dim full_name As String = .CustomerName.ToString() + MAIN.colonReplacer + .ProjectName.ToString() + MAIN.colonReplacer + .TaskWithParent.ToString().Replace(":", MAIN.colonReplacer)
                    If TimeEntryApproved Then
                        DataGridView.Rows.Add(True, .EmployeeName, .TimeEntryDate.ToString("MM/dd/yyyy"), full_name,
                                              TotalHours.ToString, .TimeEntryClass, payrollDisp, ServiceDisp) ' .RecSelect replaced with true
                    Else
                        My.Forms.MAIN.History("Time entry not approved for " + .EmployeeName + " on the week of " + .TimeEntryDate, "N")
                        DataGridView.Rows.Add(False, .EmployeeName, .TimeEntryDate.ToString("MM/dd/yyyy"), full_name,
                                              TotalHours.ToString, .TimeEntryClass, payrollDisp, ServiceDisp)
                    End If
                    element.RecSelect = True
                End With
            Next
        End If

        'Make all non-approved rows have a check box that cannot be selected
        For Each row As DataGridViewRow In DataGridView.Rows
            If row.Cells("ckBox").Value = False And Not row.Index = DataGridView.Rows.Count - 1 Then
                'row.Cells("ckBox").Style.BackColor = Color.Black
                row.Cells("ckBox").ReadOnly = True
                row.DefaultCellStyle.BackColor = Color.DarkGray
            End If
        Next

        'MsgBox(" at the end " + TimeEntryData.NoItems.ToString)
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

    '--- Timer Options Functions
    Private Sub btn_currentweek_Click(sender As Object, e As EventArgs) Handles btn_currentweek.Click
        Dim myCI As New CultureInfo("en-US")
        Dim myCal As Calendar = myCI.Calendar
        Dim myCWR As CalendarWeekRule = myCI.DateTimeFormat.CalendarWeekRule
        Dim myFirstDOW As DayOfWeek = myCI.DateTimeFormat.FirstDayOfWeek

        cur_week = myCal.GetWeekOfYear(DateTime.Now, myCWR, myFirstDOW)
        '-----------------------------
        'Dim sat As Date = GetFirstDayOfWeek(Now.Year, cur_week)
        'If Now.DayOfWeek.CompareTo(sat.DayOfWeek) > 0 Then
        '    'MsgBox("Day of week Now: " + Now.DayOfWeek.ToString + "sat Day of week: " + sat.DayOfWeek.ToString + " compare =>" + Now.DayOfWeek.CompareTo(sat.DayOfWeek).ToString)
        '    dpStartDate.Value = sat.AddDays(-1)
        'Else
        '    'MsgBox("-8" + Now.DayOfWeek.CompareTo(sat.DayOfWeek).ToString)
        '    dpStartDate.Value = sat.AddDays(-8)
        'End If
        'dpEndDate.Value = sat.AddDays(5)
        ''3 
        ' Friday to Saturday
        '-----------------------------

        Dim sat As Date = firstdateofweek(Now.Year, cur_week, DayOfWeek.Saturday)
        'sat.AddDays(-1)

        If Now.DayOfWeek.CompareTo(sat.DayOfWeek) > 0 Then
            'MsgBox("Day of week Now: " + Now.DayOfWeek.ToString + "sat Day of week: " + sat.DayOfWeek.ToString + " compare =>" + Now.DayOfWeek.CompareTo(sat.DayOfWeek).ToString)
            'MsgBox(sat.ToString)
            dpStartDate.Value = sat.AddDays(-1)
            dpEndDate.Value = sat.AddDays(6)
        Else
            'MsgBox("-8" + Now.DayOfWeek.CompareTo(sat.DayOfWeek).ToString)
            'MsgBox(sat.ToString + "  second")
            dpStartDate.Value = sat.AddDays(-7)
            dpEndDate.Value = sat.AddDays(-1)
        End If
    End Sub

    Public Function GetFirstDayOfWeek(year As Integer, weekNumber As Integer) As DateTime
        Return GetFirstDayOfWeek(year, weekNumber, Application.CurrentCulture)
    End Function

    Public Function GetFirstDayOfWeek(year As Integer, weekNumber As Integer, culture As System.Globalization.CultureInfo) As DateTime
        Dim calendar As System.Globalization.Calendar = culture.Calendar
        Dim firstOfYear As New DateTime(year, 1, 1, calendar)
        Dim targetDay As DateTime = calendar.AddWeeks(firstOfYear, weekNumber)
        Dim firstDayOfWeek As DayOfWeek = culture.DateTimeFormat.FirstDayOfWeek

        While targetDay.DayOfWeek <> firstDayOfWeek
            targetDay = targetDay.AddDays(-1)
        End While

        Return targetDay
    End Function

    Private Sub nextWeek_Click(sender As Object, e As EventArgs) Handles nextWeek.Click
        cur_week = cur_week + 1
        Dim sat = firstdateofweek(Now.Year.ToString, cur_week, DayOfWeek.Saturday)
        If Now.DayOfWeek.CompareTo(sat.DayOfWeek) > 0 Then
            dpStartDate.Value = sat.AddDays(-1)
            dpEndDate.Value = sat.AddDays(6)

        Else
            dpStartDate.Value = sat.AddDays(-7)
            dpEndDate.Value = sat.AddDays(-1)
        End If

    End Sub

    Public Function firstdateofweek(ByVal year As Integer, ByVal week As Integer, Optional firstdayofweek As DayOfWeek = DayOfWeek.Monday) As Date
        Dim dt As Date = New Date(year, 1, 1)
        If dt.DayOfWeek > 4 Then dt = dt.AddDays(7 - dt.DayOfWeek) Else dt = dt.AddDays(-dt.DayOfWeek)
        dt = dt.AddDays(firstdayofweek)
        Return dt.AddDays(7 * (week - 1))
    End Function

    'Not Used
    Private Sub preWeek_Click(sender As Object, e As EventArgs) Handles preWeek.Click
        'cur_week = cur_week - 1
        'Dim start_day_in_cur_week = FirstDateOfWeek(Now.Year.ToString, cur_week, DayOfWeek.Saturday)
        'Dim sat As Date = GetFirstDayOfWeek(Now.Year, cur_week)

        'If Now.DayOfWeek.CompareTo(sat.DayOfWeek) Then
        '    dpStartDate.Value = sat.AddDays(-1)
        'Else
        '    dpStartDate.Value = sat.AddDays(-8)
        'End If

        'dpEndDate.Value = sat.AddDays(5).
        cur_week -= 1

        Dim sat = firstdateofweek(Now.Year.ToString, cur_week, DayOfWeek.Saturday)

        'Dim sat As Date = GetFirstDayOfWeek(Now.Year, cur_week)
        If Now.DayOfWeek.CompareTo(sat.DayOfWeek) > 0 Then
            'MsgBox("Day of week Now: " + Now.DayOfWeek.ToString + "sat Day of week: " + sat.DayOfWeek.ToString + " compare =>" + Now.DayOfWeek.CompareTo(sat.DayOfWeek).ToString)
            'MsgBox(sat.ToString)
            dpStartDate.Value = sat.AddDays(-1)
            dpEndDate.Value = sat.AddDays(6)

        Else
            'MsgBox("-8" + Now.DayOfWeek.CompareTo(sat.DayOfWeek).ToString)
            'MsgBox(sat.ToString + "  second")
            dpStartDate.Value = sat.AddDays(-7)
            dpEndDate.Value = sat.AddDays(-1)
        End If

    End Sub

    'Not Used
    'Private Function isqbid_in_datatable(ByVal myqbid As String) As Int16

    '    'dim customeradapter as new qb_tl_idstableadapters.customerstableadapter()
    '    'for each timeliveid as qb_tl_ids.customersrow in customeradapter.getcustomers()
    '    '    if string.compare(trim(timeliveid.quickbooks_id), trim(myqbid)) = 0 then
    '    '        return true
    '    '        exit for
    '    '    end if
    '    'next
    '    Dim customeradapter As New QB_TL_IDsTableAdapters.CustomersTableAdapter()
    '    Dim timeliveids As QB_TL_IDs.CustomersDataTable = customeradapter.GetCorrespondingTL_ID(myqbid)

    '    Dim result As Int16 = Math.Min(timeliveids.Count, 2)

    '    If timeliveids.Count = 1 Then
    '        History("one record found in local database", "i")
    '    ElseIf timeliveids.Count = 0 Then
    '        History("no records found in local database", "i")
    '    ElseIf timeliveids.Count > 1 Then
    '        History("more than one record found in local database", "i")
    '    End If

    '    Return result
    'End Function

    Private Sub cbWageType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbWageType.SelectedIndexChanged
        My.Settings.QBWageType = cbWageType.SelectedIndex
        My.Settings.Save()
    End Sub

    'Public Sub History(ByVal input As String, Type As String)
    '    If String.Compare("n", Type, False) = 0 Then
    '        StatusWindow.Text += vbNewLine + ">> " + input
    '    End If
    '    If String.Compare("N", Type, False) = 0 Then
    '        StatusWindow.Text += vbNewLine + "***********************"
    '        StatusWindow.Text += vbNewLine + ">> " + input
    '        StatusWindow.Text += vbNewLine + "***********************"
    '    End If
    '    If String.Compare("c", Type, False) = 0 Then
    '        StatusWindow.Text += ", " + input
    '    End If
    '    If String.Compare("C", Type, False) = 0 Then
    '        StatusWindow.Text += ", " + input
    '    End If
    '    If String.Compare("i", Type, False) = 0 Then
    '        StatusWindow.Text += vbNewLine + vbTab + "- " + input
    '    End If
    '    If String.Compare("I", Type, False) = 0 Then
    '        StatusWindow.Text += vbNewLine + vbTab + "***********************"
    '        StatusWindow.Text += vbNewLine + vbTab + "- " + input
    '        StatusWindow.Text += vbNewLine + vbTab + "***********************"
    '    End If

    '    StatusWindow.SelectionStart = StatusWindow.TextLength
    '    StatusWindow.ScrollToCaret()
    'End Sub



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
                    If Type >= 10 And Type < 15 Then
                        readItems = display_UI()
                    End If

                    'for type Time Items
                    ' Might add this to display_UI() or as its own private function
                    If Type = 20 Then
                        'ReadItems = display_TimeEntry_UI()
                        readItems = display_TimeEntry_UI()
                    End If

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

    Private Sub Exitbtn_Click(sender As Object, e As EventArgs) Handles Exitbtn.Click
        My.Forms.MAIN.QUITQBSESSION() ' Close QB session before exiting
        My.Forms.MAIN.TIMERTHREADSESSION() ' Close TimerThread session before exiting
        Me.Close()
    End Sub

    Private Sub btn_systemsync_Click(sender As Object, e As EventArgs) Handles btn_systemsync.Click
        If MsgBox("Are you to perform a sync?", MsgBoxStyle.YesNo, "Warning!") = MsgBoxResult.Yes Then ' If you select yes in the MsgBox then it will close the window
            Dim CurrentSystemSync As New CurrentSystemSync
            CurrentSystemSync.PassToken(p_token, p_AccountId)
        End If
    End Sub

    Private Sub btn_relationships_Click(sender As Object, e As EventArgs) Handles btn_relationships.Click
        ChargingRelationship.Owner = Me
        ChargingRelationship.Show()
    End Sub

    Private Sub SplitContainer2_SplitterMoved(sender As Object, e As SplitterEventArgs) Handles SplitContainer2.SplitterMoved

    End Sub

    Private Sub CustomerSyncDirection_Enter(sender As Object, e As EventArgs) Handles CustomerSyncDirection.Enter

    End Sub
End Class