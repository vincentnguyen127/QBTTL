Imports QBFC13Lib

Imports System.Net.Mail
Imports System.Data.SqlClient
Imports System.Text
Imports System.Globalization


Public Class IntegratedUIFuntions
    Private p_token As String
    Private p_AccountId As String
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



    'Public Function display_TimeEntry_UI() As Integer
    '    Dim ItemLastSync As DateTime
    '    ' Delete all rows and columns from the DataGridView's except the "Check Name" column in DataGridView1
    '    Try
    '        My.Forms.Main.DataGridView1.Rows.Clear()
    '    Catch nullEx As NullReferenceException
    '        ' Do Nothing - just don't throw the exception
    '    End Try
    '    While My.Forms.Main.DataGridView1.ColumnCount > 1
    '        Try
    '            My.Forms.Main.DataGridView1.Columns.RemoveAt(1)
    '        Catch nullEx As NullReferenceException
    '            ' Do nothing - just don't throw the exception
    '        End Try
    '    End While

    '    Try
    '        My.Forms.Main.DataGridView2.Rows.Clear()
    '        My.Forms.Main.DataGridView2.Columns.Clear()
    '    Catch nullEx As NullReferenceException
    '        My.Forms.Main.DataGridView2 = New DataGridView()
    '    End Try

    '    My.Forms.Main.TabPageTimeTransfer.Visible = True
    '    My.Forms.Main.AttributeTabControl.SelectedIndex = 4

    '    My.Forms.Main.SyncFromLabel.Text = "Employees"
    '    My.Forms.Main.SyncToLabel.Text = "Time"

    '    'load grid 1
    '    Dim col2 As New DataGridViewTextBoxColumn
    '    col2.Name = "Name"
    '    My.Forms.Main.DataGridView1.Columns.Add(col2)
    '    Dim col3 As New DataGridViewTextBoxColumn
    '    col3.Name = "Employee ID"
    '    My.Forms.Main.DataGridView1.Columns.Add(col3)

    '    My.Forms.Main.SelectAllCheckBox.Checked = True

    '    If String.IsNullOrEmpty(My.Settings.TimeTrackingLastSync.ToString()) Then
    '        ItemLastSync = #1/1/2000#
    '    Else
    '        ItemLastSync = Convert.ToDateTime(My.Settings.TimeTrackingLastSync)
    '    End If

    '    My.Forms.Main.History("Synchonizing time entries items since:   " + ItemLastSync.ToString(), "n")

    '    Dim objEmployeeServices As New Services.TimeLive.Employees.Employees
    '    Dim authentication As New Services.TimeLive.Employees.SecuredWebServiceHeader
    '    authentication.AuthenticatedToken = p_token
    '    objEmployeeServices.SecuredWebServiceHeaderValue = authentication
    '    Dim dv As New DataView(objEmployeeServices.GetEmployeesData)

    '    Dim employees As New DataTable
    '    employees = objEmployeeServices.GetEmployeesData

    '    For Each row As DataRow In employees.Rows
    '        selectedEmployeeData.NoItems = selectedEmployeeData.NoItems + 1
    '        selectedEmployeeData.DataArray.Add(New TLtoQB_TimeEntry.Employee(True, row("FullName"), row("AccountEmployeeId")))
    '    Next
    '    Dim items_read As Integer = selectedEmployeeData.NoItems
    '    For Each element As TLtoQB_TimeEntry.Employee In selectedEmployeeData.DataArray
    '        'History("Debug:   " + element.RecSelect.ToString(), "n")
    '        My.Forms.Main.DataGridView1.Rows.Add(element.RecSelect, element.FullName.ToString(), element.AccountEmployeeId.ToString())
    '    Next

    '    Time_Entry_Times()

    '    System.Threading.Thread.Sleep(150)
    '    System.Windows.Forms.Application.DoEvents()
    '    My.Forms.Main.ProgressBar1.Value = 0

    '    Return items_read
    'End Function



    ' Depreciated. Will remove once display_UI is fully integrated
    'Public Function display_UI_Old() As Integer
    '    Dim ItemLastSync As DateTime
    '    Dim lastSync As String
    '    Dim Data
    '    Dim attribute As String

    '    Select Case Type
    '        ' Customers
    '        Case 10
    '            My.Forms.Main.TabPageCustomers.Visible = True
    '            My.Forms.Main.AttributeTabControl.SelectedIndex = 0
    '            lastSync = My.Settings.CustomerLastSync
    '            customerData = customer_qbtotl.GetCustomerQBData(My.Forms.Main, True)
    '            Data = customerData
    '            attribute = "customer"

    '        ' Employees
    '        Case 11
    '            My.Forms.Main.TabPageEmployees.Visible = True
    '            My.Forms.Main.AttributeTabControl.SelectedIndex = 1
    '            lastSync = My.Settings.EmployeeLastSync
    '            employeeData = employee_qbtotl.GetEmployeeQBData(My.Forms.Main, True)
    '            Data = employeeData
    '            attribute = "employee"

    '        ' Vendors
    '        Case 12
    '            My.Forms.Main.TabPageVendor.Visible = True
    '            My.Forms.Main.AttributeTabControl.SelectedIndex = 2
    '            lastSync = My.Settings.VendorLastSync
    '            vendorData = vendor_qbtotl.GetVendorQBData(My.Forms.Main, True)
    '            Data = vendorData
    '            attribute = "vendor"

    '        ' Jobs / Subjobs
    '        Case 13
    '            My.Forms.Main.TabPageJobsItems.Visible = True
    '            My.Forms.Main.AttributeTabControl.SelectedIndex = 3
    '            lastSync = My.Settings.JobLastSync
    '            JobData = job_qbtotl.GetJobSubJobData(My.Forms.Main, p_token, True)
    '            Data = JobData
    '            attribute = "job/subjob"

    '        ' Items / SubItems
    '        Case 14
    '            My.Forms.Main.TabPageJobsItems.Visible = True
    '            My.Forms.Main.AttributeTabControl.SelectedIndex = 3
    '            lastSync = My.Settings.ItemlastSync
    '            JobData = job_qbtotl.GetItemSubItemData(My.Forms.Main, p_token, True)
    '            Data = JobData
    '            attribute = "item/subitem"

    '        Case Else
    '            Return 0
    '    End Select

    '    If String.IsNullOrEmpty(lastSync) Then
    '        ItemLastSync = #1/1/2000#
    '    Else
    '        ItemLastSync = Convert.ToDateTime(lastSync)
    '    End If
    '    My.Forms.Main.History("Synchonizing modified " + attribute + " since: " + ItemLastSync.ToString(), "n")

    '    Dim readItems As Integer = Data.NoItems


    '    'load grid (there might be an easire way)
    '    ' Add Full Name Column for Job/Subjob and Item/SubItem
    '    If Type = 13 Or Type = 14 Then
    '        Dim col0 As New DataGridViewTextBoxColumn
    '        col0.Name = "Full Name"
    '        My.Forms.Main.DataGridView1.Columns.Add(col0)
    '    End If
    '    Dim col2 As New DataGridViewTextBoxColumn
    '    col2.Name = "Name"
    '    My.Forms.Main.DataGridView1.Columns.Add(col2)
    '    Dim col3 As New DataGridViewTextBoxColumn
    '    col3.Name = "Last Modified"
    '    My.Forms.Main.DataGridView1.Columns.Add(col3)
    '    Dim col4 As New DataGridViewTextBoxColumn
    '    col4.Name = "New"
    '    My.Forms.Main.DataGridView1.Columns.Add(col4)

    '    If Data Is Nothing Then
    '        My.Forms.Main.History("No " + attribute + " data", "n")
    '    End If


    '    Dim element
    '    For Each element In Data.DataArray
    '        Dim result As Integer = DateTime.Compare(Convert.ToDateTime(element.QBModTime.ToString()),
    '    ItemLastSync)
    '        If result >= 0 Then
    '            element.RecSelect = True
    '        End If
    '        'MAIN.FlagChangedItemsResults(element.QB_Name.ToString(), result)
    '    Next
    '    For Each element In Data.DataArray
    '        ' Full Name column for Job/Subjob and Item/Subitem
    '        If Type = 13 Or Type = 14 Then
    '            My.Forms.Main.DataGridView1.Rows.Add(element.RecSelect, element.FullName.ToString(), element.QB_Name.ToString(), element.QBModTime.ToString(), element.NewlyAdded)
    '        Else
    '            My.Forms.Main.DataGridView1.Rows.Add(element.RecSelect, element.QB_Name.ToString(), element.QBModTime.ToString(), element.NewlyAdded)
    '        End If
    '    Next

    '    ' Might not be wanted for every attribute, check if that is the case
    '    System.Threading.Thread.Sleep(150)
    '    System.Windows.Forms.Application.DoEvents()
    '    My.Forms.Main.ProgressBar1.Value = 0
    '    Return readItems
    'End Function

    Public Sub Time_Entry_Times()
        My.Forms.Main.DataGridView2.AutoSize = False
        My.Forms.Main.DataGridView2.AutoSizeRowsMode = False

        My.Forms.Main.DataGridView2.Rows.Clear()
        My.Forms.Main.DataGridView2.Columns.Clear()

        ' load grid 2
        Dim col1 As New DataGridViewCheckBoxColumn
        col1.Name = "ckBox"
        col1.HeaderText = "Check Box"
        My.Forms.Main.DataGridView2.Columns.Add(col1)
        Dim col2 As New DataGridViewTextBoxColumn
        col2.Name = "Employee"
        My.Forms.Main.DataGridView2.Columns.Add(col2)
        Dim col3 As New DataGridViewTextBoxColumn
        col3.Name = "Date"
        My.Forms.Main.DataGridView2.Columns.Add(col3)
        Dim col4 As New DataGridViewTextBoxColumn
        col4.Name = "Task"
        My.Forms.Main.DataGridView2.Columns.Add(col4)
        'Dim col3 As New DataGridViewTextBoxColumn
        'col3.Name = "Customer"
        ' My.Forms.Main.DataGridView2.Columns.Add(col3)
        'Dim col4 As New DataGridViewTextBoxColumn
        'col4.Name = "Job"
        ' My.Forms.Main.DataGridView2.Columns.Add(col4)
        'Dim col5 As New DataGridViewTextBoxColumn
        'col5.Name = "SubJob"
        ' My.Forms.Main.DataGridView2.Columns.Add(col5)
        Dim col5 As New DataGridViewTextBoxColumn
        col5.Name = "Time"
        My.Forms.Main.DataGridView2.Columns.Add(col5)
        Dim col6 As New DataGridViewTextBoxColumn
        col6.Name = "Class"
        My.Forms.Main.DataGridView2.Columns.Add(col6)
        Dim col7 As New DataGridViewTextBoxColumn
        col7.Name = "Payroll Item"
        My.Forms.Main.DataGridView2.Columns.Add(col7)
        Dim col8 As New DataGridViewTextBoxColumn
        col8.Name = "Item SubItem"
        My.Forms.Main.DataGridView2.Columns.Add(col8)
    End Sub

    'Public Sub IntegratedUI_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    '    Dim ItemLastSync As DateTime
    '    Dim ReadItems As Integer = 0
    '    'hide all tabstrips
    '    My.Forms.Main.TabPageCustomers.Visible = False
    '    My.Forms.Main.TabPageEmployees.Visible = False
    '    My.Forms.Main.TabPageTimeTransfer.Visible = False

    '    Me.Show()
    '    My.Forms.Main.DataGridView1.AutoSize = False
    '    My.Forms.Main.DataGridView1.AutoSizeRowsMode = False
    '    My.Forms.Main.DataGridView2.AutoSize = False
    '    My.Forms.Main.DataGridView2.AutoSizeRowsMode = False
    '    btn_currentweek_Click(sender, e)

    '    'for type Customers, Employees, Vendors, Jobs/Subjobs, and Items/Subitems
    '    If Type >= 10 And Type < 15 Then
    '        ReadItems = display_UI()
    '    End If

    '    'for type Time Items
    '    ' Might add this to display_UI() or as its own private function
    '    If Type = 20 Then
    '        ReadItems = display_TimeEntry_UI()
    '    End If

    '    History(ReadItems.ToString() + " items were read from Quickbooks", "n")
    'End Sub


    'Public Overloads Sub Show(ByVal token As String, ByVal AccountId As String, TypeSelected As Integer)


    '    My.Forms.Main.History("Welcome to QB2TL Sync by Telrium", "n")
    '    Using newForm = New Login()
    '        If DialogResult.OK = newForm.ShowDialog() Then
    '            p_token = newForm.ReturnValue1
    '            p_AccountId = newForm.ReturnValue2
    '            My.Forms.Main.History("You are logged into TimeLive", "n")
    '            My.Forms.Main.History("", "n")
    '        Else
    '            My.Forms.Main.History("You will need to log into TimeLive before using this utility.", "n")
    '            My.Forms.Main.History("", "n")
    '        End If
    '    End Using




    '    Type = TypeSelected
    '    'If token Is Nothing Then
    '    'MsgBox("Please Login")
    '    'Else
    '    'p_AccountId = AccountId
    '    'p_token = token
    '    MyBase.Show()

    '    'Commented out, changes the display ordering which affects the check box matching with the respective row
    '    'DataGridView1.Sort(DataGridView1.Columns(1), System.ComponentModel.ListSortDirection.Ascending)

    '    'Change the number to the column index that you want to sort
    '    '--------   cbWageType 
    '    If My.Settings.QBClass = "" Then
    '        My.Forms.Main.cbWageType.SelectedIndex = 0
    '    Else
    '        My.Forms.Main.cbWageType.SelectedIndex = My.Settings.QBClass
    '    End If
    '    'End If
    'End Sub

    'Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles bntclose.Click
    '    'TransferTimeButton.Visible = False
    '    My.Forms.Main.TimeEntrySelectAll.Visible = False
    '    My.Forms.Main.Close()
    'End Sub

    'Private Sub btnRefreshTimeTransfer_Click(sender As Object, e As EventArgs) Handles RefreshTimeTransfer.Click
    '    'TransferTimeButton.Visible = True
    '    My.Forms.Main.TimeEntrySelectAll.Visible = True
    '    My.Forms.Main.TimeEntrySelectAll.Checked = True
    '    Reset_Checked_SelectedEmployee_Value(selectedEmployeeData)
    '    Set_Selected_SelectedEmployee()
    '    'Dim IntUI_2ndSelect As New IntUI_2ndSelect
    '    Time_Entry_Times()
    '    'IntUI_2ndSelect.init_vars(p_token, p_AccountId, selectedEmployeeData, CDate(dpStartDate.Value).Date, CDate(dpEndDate.Value).Date.ToString)
    '    Dim StartDate As DateTime = CDate(My.Forms.Main.dpStartDate.Value).Date
    '    Dim endDate As DateTime = CDate(My.Forms.Main.dpEndDate.Value).Date
    '    TimeEntryData.clear()

    '    My.Forms.Main.ProgressBar1.Maximum = selectedEmployeeData.NoItems
    '    For Each element As TLtoQB_TimeEntry.Employee In selectedEmployeeData.DataArray
    '        With element
    '            If element.RecSelect = True Then
    '                My.Forms.Main.History("Processing: " + element.FullName.ToString(), "n")
    '                LoadSelectedTimeEntryItems(element.AccountEmployeeId, element.FullName, My.Forms.Main.DataGridView2, StartDate, endDate, True)
    '                'deselect as not to load again
    '                element.RecSelect = False
    '                'Exit For
    '            End If
    '        End With
    '        My.Forms.Main.ProgressBar1.Value += 1
    '    Next
    '    'IntUI_2ndSelect.Owner = Me
    '    'IntUI_2ndSelect.Show(p_token, p_AccountId, selectedEmployeeData,
    '    'CDate(dpStartDate.Value).Date,
    '    'CDate(dpEndDate.Value).Date.ToString, 201)
    '    'wait for one second so user can see progress bar
    '    System.Threading.Thread.Sleep(150)
    '    System.Windows.Forms.Application.DoEvents()

    '    My.Forms.Main.ProgressBar1.Value = 0
    '    My.Settings.TimeTrackingLastSync = DateTime.Now.ToString()
    '    My.Settings.Save()
    'End Sub

    'Private Sub btnTransfer_Click(sender As Object, e As EventArgs) Handles btnTransfer.Click
    '    Dim ItemsProcessed As Integer = 0

    '    'When processing customers
    '    If Type = 10 Then
    '        Reset_Checked_Customer_Value(customerData)
    '        If My.Forms.Main.QBtoTLCustomerRadioButton.Checked Then
    '            QB_Set_Selected_Customer()
    '            ItemsProcessed = customer_qbtotl.QBTransferCustomerToTL(customerData, p_token, Me, True)
    '            History(ItemsProcessed.ToString() + " TimeLive record" + If(ItemsProcessed = 1, " was", "s were") + " created or updated", "i")
    '            My.Settings.CustomerLastSync = DateTime.Now.ToString()
    '            My.Settings.Save()
    '        Else
    '            Dim customersToCheck As List(Of String) = TL_Set_Selected_Items()
    '            ItemsProcessed = customer_TLSync.SyncCustomerData(p_token, Me, True, customersToCheck)
    '            History(ItemsProcessed.ToString() + " QuickBooks customer" + If(ItemsProcessed = 1, " was", "s were") + " created or updated", "i")
    '        End If
    '    End If

    '    'When processing Employees
    '    If Type = 11 Then
    '        Reset_Checked_Employee_Value(employeeData)
    '        If My.Forms.Main.QBtoTLEmployeeRadioButton.Checked Then
    '            Set_Selected_Employee()
    '            ItemsProcessed = employee_qbtotl.QBTransferEmployeeToTL(employeeData, p_token, Me, True)
    '            My.Forms.Main.History(ItemsProcessed.ToString() + " TimeLive records were created or updated", "i")
    '            My.Settings.EmployeeLastSync = DateTime.Now.ToString()
    '            My.Settings.Save()
    '        Else
    '            Dim employeesToCheck As List(Of String) = TL_Set_Selected_Items()
    '            ItemsProcessed = employee_TLSync.SyncEmployeeData(p_token, Me, True, employeesToCheck)
    '            My.Forms.Main.History(ItemsProcessed.ToString() + " QuickBooks employee" + If(ItemsProcessed = 1, " was", "s were") + " created or updated", "i")
    '        End If
    '    End If

    '    'When processing vendor
    '    If Type = 12 Then
    '        Reset_Checked_Vendor_Value(vendorData)
    '        If My.Forms.Main.QBtoTLVendorRadioButton.Checked Then
    '            Set_Selected_Vendor()
    '            ItemsProcessed = vendor_qbtotl.QBTransferVendorToTL(vendorData, p_token, Me, True)
    '            My.Forms.Main.History(ItemsProcessed.ToString() + " TimeLive records were created or updated", "i")
    '            My.Settings.VendorLastSync = DateTime.Now.ToString()
    '            My.Settings.Save()
    '        Else
    '            Dim vendorsToCheck As List(Of String) = TL_Set_Selected_Items()
    '            ItemsProcessed = vendor_TLSync.SyncVendorData(p_token, Me, True, vendorsToCheck)
    '            My.Forms.Main.History(ItemsProcessed.ToString() + " QuickBooks vendor" + If(ItemsProcessed = 1, " was", "s were") + " created or updated", "i")
    '        End If
    '    End If

    '    'When processing Jobs or Items
    '    If Type = 13 Or Type = 14 Then
    '        Reset_Checked_Job_Value(JobData)
    '        If My.Forms.Main.QBtoTLJobItemRadioButton.Checked Then
    '            Set_Selected_Job_Item()
    '            If Type = 13 Then
    '                ItemsProcessed = job_qbtotl.QBTransferJobstoTL(JobData, p_token, Me, True)
    '                My.Forms.Main.History(ItemsProcessed.ToString() + " TimeLive records were created or updated", "i")
    '                My.Settings.JobLastSync = DateTime.Now.ToString()
    '            Else
    '                ItemsProcessed = job_qbtotl.QBTransferItemsToTL(JobData, p_token, Me, True)
    '                My.Forms.Main.History(ItemsProcessed.ToString() + " TimeLive records were created or updated", "i")
    '                My.Settings.ItemlastSync = DateTime.Now.ToString()
    '            End If
    '            My.Settings.Save()
    '        Else
    '            Dim jobsToCheck As List(Of String) = TL_Set_Selected_Items()
    '            ItemsProcessed = job_TLSync.SyncJobsSubJobData(p_token, Me, True, jobsToCheck)
    '            My.Forms.Main.History(ItemsProcessed.ToString() + " QuickBooks job/item" + If(ItemsProcessed = 1, " was", "s were") + " created or updated", "i")
    '        End If
    '    End If

    '    'When processing items
    '    'If Type = 14 Then
    '    '    Reset_Checked_Job_Value(JobData)
    '    '    If QBtoTLJobItemRadioButton.Checked Then
    '    '        Set_Selected_Job_Item()
    '    '        ItemsProcessed = job_qbtotl.QBTransferItemsToTL(JobData, p_token, Me, True)
    '    '        History(ItemsProcessed.ToString() + " TimeLive records were created or updated", "i")
    '    '        My.Settings.ItemlastSync = DateTime.Now.ToString()
    '    '        My.Settings.Save()
    '    '    Else
    '    '    End If
    '    'End If

    '    'When processing Time Transfer
    '    If Type = 20 Then
    '        Reset_Checked_TimeEntry_Value(TimeEntryData)
    '        Set_Selected_TimeEntry(My.Forms.Main.DataGridView2)

    '        ' Transfer Time Entry data from TL to QB
    '        ItemsProcessed = timeentry_tltoqb.TLTransferTimeToQB(TimeEntryData, p_token, Me, True)
    '        'IntUI_2ndSelect.time_transfer( My.Forms.Main.DataGridView2, Me)
    '        My.Forms.Main.History(ItemsProcessed.ToString() + If(ItemsProcessed = 1, " Time Entry was", " Time Entries were") + " created or updated", "i")

    '    Else
    '        ' Refresh after processing
    '        display_UI()
    '    End If

    '    'wait for one second so user can see progress bar
    '    System.Threading.Thread.Sleep(150)
    '    System.Windows.Forms.Application.DoEvents()

    '    Me.My.Forms.Main.ProgressBar1.Value = 0
    'End Sub

    Public Sub Reset_Checked_Customer_Value(ByRef customerObj As QBtoTL_Customer.CustomerDataStructureQB)
        ' reset the check value to zero
        If customerObj IsNot Nothing Then
            For Each element As QBtoTL_Customer.Customer In customerObj.DataArray
                element.RecSelect = False
            Next
        End If
    End Sub

    Public Sub Reset_Checked_Vendor_Value(ByRef vendorObj As QBtoTL_Vendor.VendorDataStructureQB)
        ' reset the check value to zero
        If vendorObj IsNot Nothing Then
            For Each element As QBtoTL_Vendor.Vendor In vendorObj.DataArray
                element.RecSelect = False
            Next
        End If
    End Sub

    Public Sub Reset_Checked_Job_Value(ByRef jobObj As QBtoTL_JobOrItem.JobDataStructureQB)
        ' reset the check value to zero
        If jobObj IsNot Nothing Then
            For Each element As QBtoTL_JobOrItem.Job_Subjob In jobObj.DataArray
                element.RecSelect = False
            Next
        End If
    End Sub

    Public Sub Reset_Checked_Employee_Value(ByRef EmployeeObj As QBtoTL_Employee.EmployeeDataStructureQB)
        ' reset the check value to zero
        If EmployeeObj IsNot Nothing Then
            For Each element As QBtoTL_Employee.Employee In EmployeeObj.DataArray
                element.RecSelect = False
            Next
        End If
    End Sub

    Public Sub Reset_Checked_SelectedEmployee_Value(ByRef SelectedEmployeeObj As TLtoQB_TimeEntry.EmployeeDataStructure)
        ' reset the check value to zero
        If SelectedEmployeeObj IsNot Nothing Then
            For Each element As TLtoQB_TimeEntry.Employee In SelectedEmployeeObj.DataArray
                element.RecSelect = False
                'History("Time selection:  Reseting employees:   " + element.RecSelect.ToString(), "n")
            Next
        End If
    End Sub

    Public Sub Reset_Checked_TimeEntry_Value(ByRef TimeEntryObj As TLtoQB_TimeEntry.TimeEntryDataStructureQB)
        ' reset the check value to zero
        For Each element As TLtoQB_TimeEntry.TimeEntry In TimeEntryObj.DataArray
            element.RecSelect = False
        Next
    End Sub

    Public Sub Set_Selected_TimeEntry(ByRef DataGridView As DataGridView)
        My.Forms.Main.ProgressBar1.Maximum = DataGridView.Rows.Count
        For Each row As DataGridViewRow In DataGridView.Rows
            If row.Cells("Date").Value IsNot Nothing And row.Cells("ckBox").Value And TimeEntryData.NoItems Then
                Dim i = 0
                Dim full_name As String = row.Cells("Task").Value.ToString()

                TimeEntryData.DataArray.ForEach(
                    Sub(timeentry)
                        If (timeentry.EmployeeName = row.Cells("Employee").Value.ToString And
                           timeentry.CustomerName + MAIN.colonReplacer + timeentry.ProjectName + MAIN.colonReplacer + timeentry.TaskWithParent = full_name And
                           timeentry.TimeEntryDate.ToString("MM/dd/yyyy") = row.Cells("Date").Value.ToString) Then
                            i += 1
                            timeentry.RecSelect = True
                        End If
                    End Sub
                )
                'TimeEntryData.DataArray(row.Index).RecSelect = True
                My.Forms.Main.History("Selected for processing: " + row.Cells("Employee").Value.ToString + " with task " + row.Cells("Task").Value.ToString + " on " + row.Cells("Date").Value.ToString, "n")
                My.Forms.Main.ProgressBar1.Value += 1
            End If
        Next
    End Sub

    ' Returns a list of the full names for the selected TimeLive entities
    Public Function TL_Set_Selected_Items()
        Dim TL_Names As List(Of String) = New List(Of String)
        For Each row As DataGridViewRow In My.Forms.Main.DataGridView1.Rows
            If row.Cells("Name").Value IsNot Nothing And row.Cells("ckBox").Value Then
                Dim full_name As String = row.Cells("Name").Value.ToString.Replace(MAIN.colonReplacer, ":")
                TL_Names.Add(full_name)
                My.Forms.Main.History("Item selected for processing: " + row.Cells("Name").Value, "n")
            End If
        Next

        Return TL_Names
    End Function

    Public Sub QB_Set_Selected_Customer()
        For Each row As DataGridViewRow In My.Forms.Main.DataGridView1.Rows
            If row.Cells("Name").Value IsNot Nothing And row.Cells("ckBox").Value = True Then
                customerData.DataArray.ForEach(
                    Sub(customer)
                        If customer.QB_Name = row.Cells("Name").Value.ToString Then
                            customer.RecSelect = True
                        End If
                    End Sub
                )
                My.Forms.Main.History("Customers selected for processing: " + row.Cells("Name").Value, "n")
            End If
        Next
    End Sub

    Public Sub Set_Selected_Employee()
        For Each row As DataGridViewRow In My.Forms.Main.DataGridView1.Rows
            If row.Cells("Name").Value IsNot Nothing And row.Cells("ckBox").Value = True Then
                employeeData.DataArray.ForEach(
                    Sub(employee)
                        If employee.QB_Name = row.Cells("Name").Value.ToString Then
                            employee.RecSelect = True
                        End If
                    End Sub
                )
                My.Forms.Main.History("Employees selected for processing: " + row.Cells("Name").Value, "n")
            End If
        Next
    End Sub

    ' For Time Transfer
    Public Sub Set_Selected_SelectedEmployee()
        For Each row As DataGridViewRow In My.Forms.Main.DataGridView1.Rows
            If row.Cells("Name").Value IsNot Nothing And row.Cells("ckBox").Value = True Then
                selectedEmployeeData.DataArray.ForEach(
                    Sub(selectedEmployee)
                        My.Forms.Main.History(selectedEmployee.FullName + " vs " + row.Cells("Name").Value.ToString, "i")
                        If selectedEmployee.FullName = row.Cells("Name").Value.ToString Then
                            selectedEmployee.RecSelect = True
                        End If
                    End Sub
                )
                My.Forms.Main.History("Selected employee for time transfer: " + row.Cells("Name").Value, "n")
            End If
        Next
    End Sub

    Public Sub Set_Selected_Vendor()
        For Each row As DataGridViewRow In My.Forms.Main.DataGridView1.Rows
            If row.Cells("Name").Value IsNot Nothing And row.Cells("ckBox").Value Then
                vendorData.DataArray.ForEach(
                    Sub(vendor)
                        If vendor.QB_Name = row.Cells("Name").Value.ToString Then
                            vendor.RecSelect = True
                        End If
                    End Sub
                )
                My.Forms.Main.History("Vendors selected for processing: " + row.Cells("Name").Value, "n")
            End If
        Next
    End Sub

    Public Sub Set_Selected_Job_Item()
        For Each row As DataGridViewRow In My.Forms.Main.DataGridView1.Rows
            If row.Cells("Name").Value IsNot Nothing And row.Cells("ckBox").Value = True Then
                JobData.DataArray.ForEach(
                    Sub(job)
                        Dim full_name As String = row.Cells("Full Name").Value.ToString.Replace(MAIN.colonReplacer, ":")
                        If job.FullName = full_name Then
                            job.RecSelect = True
                        End If
                    End Sub
                )
                My.Forms.Main.History("Job or items selected for processing: " + row.Cells("Full Name").Value, "n")
            End If
        Next
    End Sub

    'Private Sub selectall_checkbox(sender As Object, e As EventArgs) Handles My.Forms.Main.SelectAllCheckBox.CheckedChanged
    '    For Each row As DataGridViewRow In My.Forms.Main.DataGridView1.Rows
    '        If row.Cells("Name").Value IsNot Nothing Then
    '            row.Cells("ckBox").Value = My.Forms.Main.SelectAllCheckBox.Checked
    '        End If
    '    Next
    'End Sub

    'Private Sub timeEntry_selectall_checkbox(sender As Object, e As EventArgs) Handles My.Forms.Main.TimeEntrySelectAll.CheckedChanged
    '    For Each row As DataGridViewRow In My.Forms.Main.DataGridView2.Rows
    '        If row.Cells("Employee").Value IsNot Nothing Then
    '            row.Cells("ckBox").Value = My.Forms.Main.TimeEntrySelectAll.Checked
    '        End If
    '    Next
    'End Sub

    Public Sub LoadSelectedTimeEntryItems(AccountEmployeeId As String, EmployeeName As String, ByRef DataGridView As DataGridView, ByVal StartDate As DateTime, ByVal EndDate As DateTime, Optional combine As Boolean = False)
        Dim temp As New TLtoQB_TimeEntry.TimeEntryDataStructureQB

        Dim emplTLData As TLtoQB_TimeEntry.TimeEntryDataStructureQB = timeentry_tltoqb.GetTimeEntryTLData(AccountEmployeeId, StartDate, EndDate, My.Forms.Main, p_token, False)
        If combine Then
            TimeEntryData.combine(emplTLData)
        Else
            TimeEntryData = emplTLData
        End If

        Dim TotalHour As Integer
        Dim TotalMin As Double
        If TimeEntryData IsNot Nothing Then
            For Each element As TLtoQB_TimeEntry.TimeEntry In emplTLData.DataArray
                With element
                    Dim Item_SubItemID As String = Nothing

                    TotalHour = .TotalTime.ToString("%h")
                    'Turn hour to 24 hour time
                    TotalHour = TotalHour Mod 12
                    If Not .TotalTime.ToString.Contains("AM") Then
                        TotalHour += 12
                    End If

                    TotalMin = .TotalTime.ToString("%m")
                    TotalMin = (TotalMin / 60).ToString("00.00")

                    Dim payrollDisp As String = If(.PayrollName Is Nothing, .PayrollItem, .PayrollName)
                    Dim ServiceDisp As String = If(.ServiceName Is Nothing, .ServiceItem, .ServiceName)

                    Dim full_name As String = .CustomerName.ToString() + MAIN.colonReplacer + .ProjectName.ToString() + MAIN.colonReplacer + .TaskWithParent.ToString()

                    DataGridView.Rows.Add(.RecSelect, .EmployeeName, .TimeEntryDate.ToString("MM/dd/yyyy"), full_name,
                                           (TotalHour + TotalMin).ToString, .TimeEntryClass, payrollDisp, ServiceDisp)
                    element.RecSelect = True
                End With
            Next
        End If

        'Select All
        For Each row As DataGridViewRow In DataGridView.Rows
            If row.Cells("Date").Value IsNot Nothing Then
                row.Cells("ckBox").Value = True
            End If
        Next

        'MsgBox(" at the end " + TimeEntryData.NoItems.ToString)
    End Sub

    '--- Timer Options Functions
    'Private Sub btn_currentweek_Click(sender As Object, e As EventArgs) Handles btn_currentweek.Click
    '    Dim myCI As New CultureInfo("en-US")
    '    Dim myCal As Calendar = myCI.Calendar
    '    Dim myCWR As CalendarWeekRule = myCI.DateTimeFormat.CalendarWeekRule
    '    Dim myFirstDOW As DayOfWeek = myCI.DateTimeFormat.FirstDayOfWeek

    '    cur_week = myCal.GetWeekOfYear(DateTime.Now, myCWR, myFirstDOW)
    '    '-----------------------------
    '    'Dim sat As Date = GetFirstDayOfWeek(Now.Year, cur_week)
    '    'If Now.DayOfWeek.CompareTo(sat.DayOfWeek) > 0 Then
    '    '    'MsgBox("Day of week Now: " + Now.DayOfWeek.ToString + "sat Day of week: " + sat.DayOfWeek.ToString + " compare =>" + Now.DayOfWeek.CompareTo(sat.DayOfWeek).ToString)
    '    '    dpStartDate.Value = sat.AddDays(-1)
    '    'Else
    '    '    'MsgBox("-8" + Now.DayOfWeek.CompareTo(sat.DayOfWeek).ToString)
    '    '    dpStartDate.Value = sat.AddDays(-8)
    '    'End If
    '    'dpEndDate.Value = sat.AddDays(5)
    '    ''3 
    '    ' Friday to Saturday
    '    '-----------------------------

    '    Dim sat As Date = FirstDateOfWeek(Now.Year, cur_week, DayOfWeek.Saturday)
    '    'sat.AddDays(-1)

    '    If Now.DayOfWeek.CompareTo(sat.DayOfWeek) > 0 Then
    '        'MsgBox("Day of week Now: " + Now.DayOfWeek.ToString + "sat Day of week: " + sat.DayOfWeek.ToString + " compare =>" + Now.DayOfWeek.CompareTo(sat.DayOfWeek).ToString)
    '        'MsgBox(sat.ToString)
    '        My.Forms.Main.dpStartDate.Value = sat.AddDays(-1)
    '        My.Forms.Main.dpEndDate.Value = sat.AddDays(6)
    '    Else
    '        'MsgBox("-8" + Now.DayOfWeek.CompareTo(sat.DayOfWeek).ToString)
    '        'MsgBox(sat.ToString + "  second")
    '        My.Forms.Main.dpStartDate.Value = sat.AddDays(-7)
    '        My.Forms.Main.dpEndDate.Value = sat.AddDays(-1)
    '    End If
    'End Sub

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

    'Private Sub nextWeek_Click(sender As Object, e As EventArgs) Handles nextWeek.Click
    '    cur_week = cur_week + 1
    '    Dim sat = FirstDateOfWeek(Now.Year.ToString, cur_week, DayOfWeek.Saturday)
    '    If Now.DayOfWeek.CompareTo(sat.DayOfWeek) > 0 Then
    '        My.Forms.Main.dpStartDate.Value = sat.AddDays(-1)
    '        My.Forms.Main.dpEndDate.Value = sat.AddDays(6)

    '    Else
    '        My.Forms.Main.dpStartDate.Value = sat.AddDays(-7)
    '        My.Forms.Main.dpEndDate.Value = sat.AddDays(-1)
    '    End If

    'End Sub

    Public Function FirstDateOfWeek(ByVal Year As Integer, ByVal Week As Integer, Optional FirstDayOfWeek As DayOfWeek = DayOfWeek.Monday) As Date
        Dim dt As Date = New Date(Year, 1, 1)
        If dt.DayOfWeek > 4 Then dt = dt.AddDays(7 - dt.DayOfWeek) Else dt = dt.AddDays(-dt.DayOfWeek)
        dt = dt.AddDays(FirstDayOfWeek)
        Return dt.AddDays(7 * (Week - 1))
    End Function

    'Private Sub preWeek_Click(sender As Object, e As EventArgs) Handles preWeek.Click
    '    'cur_week = cur_week - 1
    '    'Dim start_day_in_cur_week = FirstDateOfWeek(Now.Year.ToString, cur_week, DayOfWeek.Saturday)
    '    'Dim sat As Date = GetFirstDayOfWeek(Now.Year, cur_week)

    '    'If Now.DayOfWeek.CompareTo(sat.DayOfWeek) Then
    '    '    dpStartDate.Value = sat.AddDays(-1)
    '    'Else
    '    '    dpStartDate.Value = sat.AddDays(-8)
    '    'End If

    '    'dpEndDate.Value = sat.AddDays(5).
    '    cur_week -= 1

    '    Dim sat = FirstDateOfWeek(Now.Year.ToString, cur_week, DayOfWeek.Saturday)

    '    'Dim sat As Date = GetFirstDayOfWeek(Now.Year, cur_week)
    '    If Now.DayOfWeek.CompareTo(sat.DayOfWeek) > 0 Then
    '        'MsgBox("Day of week Now: " + Now.DayOfWeek.ToString + "sat Day of week: " + sat.DayOfWeek.ToString + " compare =>" + Now.DayOfWeek.CompareTo(sat.DayOfWeek).ToString)
    '        'MsgBox(sat.ToString)
    '        My.Forms.Main.dpStartDate.Value = sat.AddDays(-1)
    '        My.Forms.Main.dpEndDate.Value = sat.AddDays(6)

    '    Else
    '        'MsgBox("-8" + Now.DayOfWeek.CompareTo(sat.DayOfWeek).ToString)
    '        'MsgBox(sat.ToString + "  second")
    '        My.Forms.Main.dpStartDate.Value = sat.AddDays(-7)
    '        My.Forms.Main.dpEndDate.Value = sat.AddDays(-1)
    '    End If

    'End Sub
    'Public Function ISQBID_In_DataTable(ByVal myqbID As String) As Int16

    '    'Dim CustomerAdapter As New QB_TL_IDsTableAdapters.CustomersTableAdapter()
    '    'For Each TimeLiveID As QB_TL_IDs.CustomersRow In CustomerAdapter.GetCustomers()
    '    '    If String.Compare(Trim(TimeLiveID.QuickBooks_ID), Trim(myqbID)) = 0 Then
    '    '        Return True
    '    '        Exit For
    '    '    End If
    '    'Next
    '    Dim CustomerAdapter As New QB_TL_IDsTableAdapters.CustomersTableAdapter()
    '    Dim TimeLiveIDs As QB_TL_IDs.CustomersDataTable = CustomerAdapter.GetCorrespondingTL_ID(myqbID)

    '    Dim result As Int16 = Math.Min(TimeLiveIDs.Count, 2)

    '    If TimeLiveIDs.Count = 1 Then
    '        My.Forms.Main.History("One record found in local database", "i")
    '    ElseIf TimeLiveIDs.Count = 0 Then
    '        My.Forms.Main.History("No records found in local database", "i")
    '    ElseIf TimeLiveIDs.Count > 1 Then
    '        My.Forms.Main.History("More than one record found in local database", "I")
    '    End If

    '    Return result
    'End Function



    'Private Sub cbWageType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbWageType.SelectedIndexChanged
    '    My.Settings.QBWageType = My.Forms.Main.cbWageType.SelectedIndex
    '    My.Settings.Save()
    'End Sub
End Class
