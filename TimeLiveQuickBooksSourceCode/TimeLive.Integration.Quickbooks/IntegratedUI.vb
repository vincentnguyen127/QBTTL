'Imports QBFC11Lib
Imports QBFC13Lib

Imports System.Net.Mail
Imports System.Data.SqlClient
Imports System.Text
Imports System.Globalization


Public Class IntegratedUI
    Inherits System.Windows.Forms.Form
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

    Private Sub handleTag(sender As Object, e As EventArgs) Handles AttributeTabControl.SelectedIndexChanged
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

        If Type = 20 Then
            display_TimeEntry_UI()
        Else
            display_UI()
        End If
    End Sub

    Private Function display_TimeEntry_UI()
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
        col3.Name = "Employee ID"
        DataGridView1.Columns.Add(col3)

        SelectAllCheckBox.Checked = True

        If String.IsNullOrEmpty(My.Settings.TimeTrackingLastSync.ToString()) Then
            ItemLastSync = #1/1/2000#
        Else
            ItemLastSync = Convert.ToDateTime(My.Settings.TimeTrackingLastSync)
        End If

        My.Forms.MAIN.History("Synchonizing time entries items since:   " + ItemLastSync.ToString(), "n")

        Dim objEmployeeServices As New Services.TimeLive.Employees.Employees
        Dim authentication As New Services.TimeLive.Employees.SecuredWebServiceHeader
        authentication.AuthenticatedToken = p_token
        objEmployeeServices.SecuredWebServiceHeaderValue = authentication
        Dim dv As New DataView(objEmployeeServices.GetEmployeesData)

        Dim employees As New DataTable
        employees = objEmployeeServices.GetEmployeesData

        For Each row As DataRow In employees.Rows
            selectedEmployeeData.NoItems = selectedEmployeeData.NoItems + 1
            selectedEmployeeData.DataArray.Add(New TLtoQB_TimeEntry.Employee(True, row("FullName"), row("AccountEmployeeId")))
        Next
        Dim items_read As Integer = selectedEmployeeData.NoItems
        For Each element As TLtoQB_TimeEntry.Employee In selectedEmployeeData.DataArray
            'My.Forms.MAIN.History("Debug:   " + element.RecSelect.ToString(), "n")
            DataGridView1.Rows.Add(element.RecSelect, element.FullName.ToString(), element.AccountEmployeeId.ToString())
        Next

        Time_Entry_Times()

        System.Threading.Thread.Sleep(150)
        System.Windows.Forms.Application.DoEvents()
        Me.ProgressBar1.Value = 0

        Return items_read
    End Function

    ' Note: This is the new, up to date one that does TL -> QB too, replace display_UI with this once done testing
    Private Function display_UI() Handles QBtoTLCustomerRadioButton.CheckedChanged, QBtoTLEmployeeRadioButton.CheckedChanged,
                                           QBtoTLVendorRadioButton.CheckedChanged, QBtoTLJobItemRadioButton.CheckedChanged,
                                           RefreshCustomers.Click, RefreshEmployees.Click, RefreshVendors.Click, RefreshJobsOrItems.Click
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
        My.Forms.MAIN.History("Synchonizing modified " + attribute + " since: " + ItemLastSync.ToString(), "n")

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
            My.Forms.MAIN.History("No QuickBooks " + attribute + " data", "n")
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
        If Me.ProgressBar1.Maximum = Nothing Then
            Me.ProgressBar1.Maximum = 0
        End If

        Me.ProgressBar1.Maximum += If(TLItemsArray Is Nothing, 0, TLItemsArray.Length)

        If TLItemsArray Is Nothing Or TLItemsArray.Length = 0 Then
            My.Forms.MAIN.History("No TimeLive " + attribute + " data", "n")
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
                            My.Forms.MAIN.History("Could not get TL ID of TL task '" + name +
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
        Me.ProgressBar1.Value = 0
        Return readItems
    End Function

    ' Depreciated. Will remove once display_UI is fully integrated
    Private Function display_UI_Old()
        Dim ItemLastSync As DateTime
        Dim lastSync As String
        Dim Data
        Dim attribute As String

        Select Case Type
            ' Customers
            Case 10
                TabPageCustomers.Visible = True
                AttributeTabControl.SelectedIndex = 0
                lastSync = My.Settings.CustomerLastSync
                customerData = customer_qbtotl.GetCustomerQBData(Me, True)
                Data = customerData
                attribute = "customer"

            ' Employees
            Case 11
                TabPageEmployees.Visible = True
                AttributeTabControl.SelectedIndex = 1
                lastSync = My.Settings.EmployeeLastSync
                employeeData = employee_qbtotl.GetEmployeeQBData(Me, True)
                Data = employeeData
                attribute = "employee"

            ' Vendors
            Case 12
                TabPageVendor.Visible = True
                AttributeTabControl.SelectedIndex = 2
                lastSync = My.Settings.VendorLastSync
                vendorData = vendor_qbtotl.GetVendorQBData(Me, True)
                Data = vendorData
                attribute = "vendor"

            ' Jobs / Subjobs
            Case 13
                TabPageJobsItems.Visible = True
                AttributeTabControl.SelectedIndex = 3
                lastSync = My.Settings.JobLastSync
                JobData = job_qbtotl.GetJobSubJobData(Me, p_token, True)
                Data = JobData
                attribute = "job/subjob"

            ' Items / SubItems
            Case 14
                TabPageJobsItems.Visible = True
                AttributeTabControl.SelectedIndex = 3
                lastSync = My.Settings.ItemlastSync
                JobData = job_qbtotl.GetItemSubItemData(Me, p_token, True)
                Data = JobData
                attribute = "item/subitem"

            Case Else
                Return 0
        End Select

        If String.IsNullOrEmpty(lastSync) Then
            ItemLastSync = #1/1/2000#
        Else
            ItemLastSync = Convert.ToDateTime(lastSync)
        End If
        My.Forms.MAIN.History("Synchonizing modified " + attribute + " since: " + ItemLastSync.ToString(), "n")

        Dim readItems As Integer = Data.NoItems


        'load grid (there might be an easire way)
        ' Add Full Name Column for Job/Subjob and Item/SubItem
        If Type = 13 Or Type = 14 Then
            Dim col0 As New DataGridViewTextBoxColumn
            col0.Name = "Full Name"
            DataGridView1.Columns.Add(col0)
        End If
        Dim col2 As New DataGridViewTextBoxColumn
        col2.Name = "Name"
        DataGridView1.Columns.Add(col2)
        Dim col3 As New DataGridViewTextBoxColumn
        col3.Name = "Last Modified"
        DataGridView1.Columns.Add(col3)
        Dim col4 As New DataGridViewTextBoxColumn
        col4.Name = "New"
        DataGridView1.Columns.Add(col4)

        If Data Is Nothing Then
            My.Forms.MAIN.History("No " + attribute + " data", "n")
        End If


        Dim element
        For Each element In Data.DataArray
            Dim result As Integer = DateTime.Compare(Convert.ToDateTime(element.QBModTime.ToString()),
        ItemLastSync)
            If result >= 0 Then
                element.RecSelect = True
            End If
            'MAIN.FlagChangedItemsResults(element.QB_Name.ToString(), result)
        Next
        For Each element In Data.DataArray
            ' Full Name column for Job/Subjob and Item/Subitem
            If Type = 13 Or Type = 14 Then
                DataGridView1.Rows.Add(element.RecSelect, element.FullName.ToString(), element.QB_Name.ToString(), element.QBModTime.ToString(), element.NewlyAdded)
            Else
                DataGridView1.Rows.Add(element.RecSelect, element.QB_Name.ToString(), element.QBModTime.ToString(), element.NewlyAdded)
            End If
        Next

        ' Might not be wanted for every attribute, check if that is the case
        System.Threading.Thread.Sleep(150)
        System.Windows.Forms.Application.DoEvents()
        Me.ProgressBar1.Value = 0
        Return readItems
    End Function

    Private Sub Time_Entry_Times()
        DataGridView2.AutoSize = False
        DataGridView2.AutoSizeRowsMode = False

        DataGridView2.Rows.Clear()
        DataGridView2.Columns.Clear()

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

    Public Sub IntegratedUI_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim ItemLastSync As DateTime
        Dim ReadItems As Integer = 0
        'hide all tabstrips
        TabPageCustomers.Visible = False
        TabPageEmployees.Visible = False
        TabPageTimeTransfer.Visible = False

        Me.Show()
        DataGridView1.AutoSize = False
        DataGridView1.AutoSizeRowsMode = False
        DataGridView2.AutoSize = False
        DataGridView2.AutoSizeRowsMode = False
        btn_currentweek_Click(sender, e)

        'for type Customers, Employees, Vendors, Jobs/Subjobs, and Items/Subitems
        If Type >= 10 And Type < 15 Then
            ReadItems = display_UI()
        End If

        'for type Time Items
        ' Might add this to display_UI() or as its own private function
        If Type = 20 Then
            ReadItems = display_TimeEntry_UI()
        End If

        My.Forms.MAIN.History(ReadItems.ToString() + " items were read from Quickbooks", "n")
    End Sub


    Public Overloads Sub Show(ByVal token As String, ByVal AccountId As String, TypeSelected As Integer)

        Type = TypeSelected
        If token Is Nothing Then
            MsgBox("Please Login")
        Else
            p_AccountId = AccountId
            p_token = token
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
        End If
    End Sub

    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles bntclose.Click
        'TransferTimeButton.Visible = False
        TimeEntrySelectAll.Visible = False
        Me.Close()
    End Sub

    Private Sub btnRefreshTimeTransfer_Click(sender As Object, e As EventArgs) Handles RefreshTimeTransfer.Click
        'TransferTimeButton.Visible = True
        TimeEntrySelectAll.Visible = True
        TimeEntrySelectAll.Checked = True
        Reset_Checked_SelectedEmployee_Value(selectedEmployeeData)
        Set_Selected_SelectedEmployee()
        'Dim IntUI_2ndSelect As New IntUI_2ndSelect
        Time_Entry_Times()
        'IntUI_2ndSelect.init_vars(p_token, p_AccountId, selectedEmployeeData, CDate(dpStartDate.Value).Date, CDate(dpEndDate.Value).Date.ToString)
        Dim StartDate As DateTime = CDate(dpStartDate.Value).Date
        Dim endDate As DateTime = CDate(dpEndDate.Value).Date
        TimeEntryData.clear()

        Me.ProgressBar1.Maximum = selectedEmployeeData.NoItems
        For Each element As TLtoQB_TimeEntry.Employee In selectedEmployeeData.DataArray
            With element
                If element.RecSelect = True Then
                    My.Forms.MAIN.History("Processing: " + element.FullName.ToString(), "n")
                    LoadSelectedTimeEntryItems(element.AccountEmployeeId, element.FullName, DataGridView2, StartDate, endDate, True)
                    'deselect as not to load again
                    element.RecSelect = False
                    'Exit For
                End If
            End With
            Me.ProgressBar1.Value += 1
        Next
        'IntUI_2ndSelect.Owner = Me
        'IntUI_2ndSelect.Show(p_token, p_AccountId, selectedEmployeeData,
        'CDate(dpStartDate.Value).Date,
        'CDate(dpEndDate.Value).Date.ToString, 201)
        'wait for one second so user can see progress bar
        System.Threading.Thread.Sleep(150)
        System.Windows.Forms.Application.DoEvents()

        Me.ProgressBar1.Value = 0
        My.Settings.TimeTrackingLastSync = DateTime.Now.ToString()
        My.Settings.Save()
    End Sub

    Private Sub btnTransfer_Click(sender As Object, e As EventArgs) Handles btnTransfer.Click
        Dim ItemsProcessed As Integer = 0

        'When processing customers
        If Type = 10 Then
            Reset_Checked_Customer_Value(customerData)
            If QBtoTLCustomerRadioButton.Checked Then
                QB_Set_Selected_Customer()
                ItemsProcessed = customer_qbtotl.QBTransferCustomerToTL(customerData, p_token, Me, True)
                My.Forms.MAIN.History(ItemsProcessed.ToString() + " TimeLive record" + If(ItemsProcessed = 1, " was", "s were") + " created or updated", "i")
                My.Settings.CustomerLastSync = DateTime.Now.ToString()
                My.Settings.Save()
            Else
                Dim customersToCheck As List(Of String) = TL_Set_Selected_Items()
                ItemsProcessed = customer_TLSync.SyncCustomerData(p_token, Me, True, customersToCheck)
                My.Forms.MAIN.History(ItemsProcessed.ToString() + " QuickBooks customer" + If(ItemsProcessed = 1, " was", "s were") + " created or updated", "i")
            End If
        End If

        'When processing Employees
        If Type = 11 Then
            Reset_Checked_Employee_Value(employeeData)
            If QBtoTLEmployeeRadioButton.Checked Then
                Set_Selected_Employee()
                ItemsProcessed = employee_qbtotl.QBTransferEmployeeToTL(employeeData, p_token, Me, True)
                My.Forms.MAIN.History(ItemsProcessed.ToString() + " TimeLive records were created or updated", "i")
                My.Settings.EmployeeLastSync = DateTime.Now.ToString()
                My.Settings.Save()
            Else
                Dim employeesToCheck As List(Of String) = TL_Set_Selected_Items()
                ItemsProcessed = employee_TLSync.SyncEmployeeData(p_token, Me, True, employeesToCheck)
                My.Forms.MAIN.History(ItemsProcessed.ToString() + " QuickBooks employee" + If(ItemsProcessed = 1, " was", "s were") + " created or updated", "i")
            End If
        End If

        'When processing vendor
        If Type = 12 Then
            Reset_Checked_Vendor_Value(vendorData)
            If QBtoTLVendorRadioButton.Checked Then
                Set_Selected_Vendor()
                ItemsProcessed = vendor_qbtotl.QBTransferVendorToTL(vendorData, p_token, Me, True)
                My.Forms.MAIN.History(ItemsProcessed.ToString() + " TimeLive records were created or updated", "i")
                My.Settings.VendorLastSync = DateTime.Now.ToString()
                My.Settings.Save()
            Else
                Dim vendorsToCheck As List(Of String) = TL_Set_Selected_Items()
                ItemsProcessed = vendor_TLSync.SyncVendorData(p_token, Me, True, vendorsToCheck)
                My.Forms.MAIN.History(ItemsProcessed.ToString() + " QuickBooks vendor" + If(ItemsProcessed = 1, " was", "s were") + " created or updated", "i")
            End If
        End If

        'When processing Jobs or Items
        If Type = 13 Or Type = 14 Then
            Reset_Checked_Job_Value(JobData)
            If QBtoTLJobItemRadioButton.Checked Then
                Set_Selected_Job_Item()
                If Type = 13 Then
                    ItemsProcessed = job_qbtotl.QBTransferJobstoTL(JobData, p_token, Me, True)
                    My.Forms.MAIN.History(ItemsProcessed.ToString() + " TimeLive records were created or updated", "i")
                    My.Settings.JobLastSync = DateTime.Now.ToString()
                Else
                    ItemsProcessed = job_qbtotl.QBTransferItemsToTL(JobData, p_token, Me, True)
                    My.Forms.MAIN.History(ItemsProcessed.ToString() + " TimeLive records were created or updated", "i")
                    My.Settings.ItemlastSync = DateTime.Now.ToString()
                End If
                My.Settings.Save()
            Else
                Dim jobsToCheck As List(Of String) = TL_Set_Selected_Items()
                ItemsProcessed = job_TLSync.SyncJobsSubJobData(p_token, Me, True, jobsToCheck)
                My.Forms.MAIN.History(ItemsProcessed.ToString() + " QuickBooks job/item" + If(ItemsProcessed = 1, " was", "s were") + " created or updated", "i")
            End If
        End If

        'When processing items
        'If Type = 14 Then
        '    Reset_Checked_Job_Value(JobData)
        '    If QBtoTLJobItemRadioButton.Checked Then
        '        Set_Selected_Job_Item()
        '        ItemsProcessed = job_qbtotl.QBTransferItemsToTL(JobData, p_token, Me, True)
        '        My.Forms.MAIN.History(ItemsProcessed.ToString() + " TimeLive records were created or updated", "i")
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
            My.Forms.MAIN.History(ItemsProcessed.ToString() + If(ItemsProcessed = 1, " Time Entry was", " Time Entries were") + " created or updated", "i")

        Else
            ' Refresh after processing
            display_UI()
        End If

        'wait for one second so user can see progress bar
        System.Threading.Thread.Sleep(150)
        System.Windows.Forms.Application.DoEvents()

        Me.ProgressBar1.Value = 0
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
                'My.Forms.MAIN.History("Time selection:  Reseting employees:   " + element.RecSelect.ToString(), "n")
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
        Me.ProgressBar1.Maximum = DataGridView.Rows.Count
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
                My.Forms.MAIN.History("Selected for processing: " + row.Cells("Employee").Value.ToString + " with task " + row.Cells("Task").Value.ToString + " on " + row.Cells("Date").Value.ToString, "n")
                Me.ProgressBar1.Value += 1
            End If
        Next
    End Sub

    ' Returns a list of the full names for the selected TimeLive entities
    Private Function TL_Set_Selected_Items()
        Dim TL_Names As List(Of String) = New List(Of String)
        For Each row As DataGridViewRow In DataGridView1.Rows
            If row.Cells("Name").Value IsNot Nothing And row.Cells("ckBox").Value Then
                Dim full_name As String = row.Cells("Name").Value.ToString.Replace(MAIN.colonReplacer, ":")
                TL_Names.Add(full_name)
                My.Forms.MAIN.History("Item selected for processing: " + row.Cells("Name").Value, "n")
            End If
        Next

        Return TL_Names
    End Function

    Private Sub QB_Set_Selected_Customer()
        For Each row As DataGridViewRow In DataGridView1.Rows
            If row.Cells("Name").Value IsNot Nothing And row.Cells("ckBox").Value = True Then
                customerData.DataArray.ForEach(
                    Sub(customer)
                        If customer.QB_Name = row.Cells("Name").Value.ToString Then
                            customer.RecSelect = True
                        End If
                    End Sub
                )
                My.Forms.MAIN.History("Customers selected for processing: " + row.Cells("Name").Value, "n")
            End If
        Next
    End Sub

    Private Sub Set_Selected_Employee()
        For Each row As DataGridViewRow In DataGridView1.Rows
            If row.Cells("Name").Value IsNot Nothing And row.Cells("ckBox").Value = True Then
                employeeData.DataArray.ForEach(
                    Sub(employee)
                        If employee.QB_Name = row.Cells("Name").Value.ToString Then
                            employee.RecSelect = True
                        End If
                    End Sub
                )
                My.Forms.MAIN.History("Employees selected for processing: " + row.Cells("Name").Value, "n")
            End If
        Next
    End Sub

    ' For Time Transfer
    Private Sub Set_Selected_SelectedEmployee()
        For Each row As DataGridViewRow In DataGridView1.Rows
            If row.Cells("Name").Value IsNot Nothing And row.Cells("ckBox").Value = True Then
                selectedEmployeeData.DataArray.ForEach(
                    Sub(selectedEmployee)
                        My.Forms.MAIN.History(selectedEmployee.FullName + " vs " + row.Cells("Name").Value.ToString, "i")
                        If selectedEmployee.FullName = row.Cells("Name").Value.ToString Then
                            selectedEmployee.RecSelect = True
                        End If
                    End Sub
                )
                My.Forms.MAIN.History("Selected employee for time transfer: " + row.Cells("Name").Value, "n")
            End If
        Next
    End Sub

    Private Sub Set_Selected_Vendor()
        For Each row As DataGridViewRow In DataGridView1.Rows
            If row.Cells("Name").Value IsNot Nothing And row.Cells("ckBox").Value Then
                vendorData.DataArray.ForEach(
                    Sub(vendor)
                        If vendor.QB_Name = row.Cells("Name").Value.ToString Then
                            vendor.RecSelect = True
                        End If
                    End Sub
                )
                My.Forms.MAIN.History("Vendors selected for processing: " + row.Cells("Name").Value, "n")
            End If
        Next
    End Sub

    Private Sub Set_Selected_Job_Item()
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
                My.Forms.MAIN.History("Job or items selected for processing: " + row.Cells("Full Name").Value, "n")
            End If
        Next
    End Sub

    Private Sub selectall_checkbox(sender As Object, e As EventArgs) Handles SelectAllCheckBox.CheckedChanged
        For Each row As DataGridViewRow In DataGridView1.Rows
            If row.Cells("Name").Value IsNot Nothing Then
                row.Cells("ckBox").Value = SelectAllCheckBox.Checked
            End If
        Next
    End Sub

    Private Sub timeEntry_selectall_checkbox(sender As Object, e As EventArgs) Handles TimeEntrySelectAll.CheckedChanged
        For Each row As DataGridViewRow In DataGridView2.Rows
            If row.Cells("Employee").Value IsNot Nothing Then
                row.Cells("ckBox").Value = TimeEntrySelectAll.Checked
            End If
        Next
    End Sub

    Public Sub LoadSelectedTimeEntryItems(AccountEmployeeId As String, EmployeeName As String, ByRef DataGridView As DataGridView, ByVal StartDate As DateTime, ByVal EndDate As DateTime, Optional combine As Boolean = False)
        Dim temp As New TLtoQB_TimeEntry.TimeEntryDataStructureQB

        Dim emplTLData As TLtoQB_TimeEntry.TimeEntryDataStructureQB = timeentry_tltoqb.GetTimeEntryTLData(AccountEmployeeId, StartDate, EndDate, Me, p_token, False)
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

        Dim sat As Date = FirstDateOfWeek(Now.Year, cur_week, DayOfWeek.Saturday)
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
        Dim sat = FirstDateOfWeek(Now.Year.ToString, cur_week, DayOfWeek.Saturday)
        If Now.DayOfWeek.CompareTo(sat.DayOfWeek) > 0 Then
            dpStartDate.Value = sat.AddDays(-1)
            dpEndDate.Value = sat.AddDays(6)

        Else
            dpStartDate.Value = sat.AddDays(-7)
            dpEndDate.Value = sat.AddDays(-1)
        End If

    End Sub

    Public Function FirstDateOfWeek(ByVal Year As Integer, ByVal Week As Integer, Optional FirstDayOfWeek As DayOfWeek = DayOfWeek.Monday) As Date
        Dim dt As Date = New Date(Year, 1, 1)
        If dt.DayOfWeek > 4 Then dt = dt.AddDays(7 - dt.DayOfWeek) Else dt = dt.AddDays(-dt.DayOfWeek)
        dt = dt.AddDays(FirstDayOfWeek)
        Return dt.AddDays(7 * (Week - 1))
    End Function

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

        Dim sat = FirstDateOfWeek(Now.Year.ToString, cur_week, DayOfWeek.Saturday)

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
    Private Function ISQBID_In_DataTable(ByVal myqbID As String) As Int16

        'Dim CustomerAdapter As New QB_TL_IDsTableAdapters.CustomersTableAdapter()
        'For Each TimeLiveID As QB_TL_IDs.CustomersRow In CustomerAdapter.GetCustomers()
        '    If String.Compare(Trim(TimeLiveID.QuickBooks_ID), Trim(myqbID)) = 0 Then
        '        Return True
        '        Exit For
        '    End If
        'Next
        Dim CustomerAdapter As New QB_TL_IDsTableAdapters.CustomersTableAdapter()
        Dim TimeLiveIDs As QB_TL_IDs.CustomersDataTable = CustomerAdapter.GetCorrespondingTL_ID(myqbID)

        Dim result As Int16 = Math.Min(TimeLiveIDs.Count, 2)

        If TimeLiveIDs.Count = 1 Then
            My.Forms.MAIN.History("One record found in local database", "i")
        ElseIf TimeLiveIDs.Count = 0 Then
            My.Forms.MAIN.History("No records found in local database", "i")
        ElseIf TimeLiveIDs.Count > 1 Then
            My.Forms.MAIN.History("More than one record found in local database", "I")
        End If

        Return result
    End Function

    Private Sub cbWageType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbWageType.SelectedIndexChanged
        My.Settings.QBWageType = cbWageType.SelectedIndex
        My.Settings.Save()
    End Sub

    Private Sub display_UI(sender As Object, e As EventArgs) Handles RefreshVendors.Click, RefreshJobsOrItems.Click, RefreshEmployees.Click, RefreshCustomers.Click, QBtoTLVendorRadioButton.CheckedChanged, QBtoTLJobItemRadioButton.CheckedChanged, QBtoTLEmployeeRadioButton.CheckedChanged, QBtoTLCustomerRadioButton.CheckedChanged

    End Sub
End Class