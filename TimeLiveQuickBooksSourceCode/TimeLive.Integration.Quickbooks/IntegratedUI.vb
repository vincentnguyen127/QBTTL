Imports QBFC11Lib
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
    Private SelectAll As Boolean

    Private cur_week

    Dim customer_qbtotl As QBtoTL_Customer = New QBtoTL_Customer
    Dim customerData As New QBtoTL_Customer.CustomerDataStructureQB

    Dim job_qbtotl As QBtoTL_JobOrItem = New QBtoTL_JobOrItem
    Dim JobData As New QBtoTL_JobOrItem.JobDataStructureQB

    Dim employee_qbtotl As QBtoTL_Employee = New QBtoTL_Employee
    Dim employeeData As New QBtoTL_Employee.EmployeeDataStructureQB

    Dim vendor_qbtotl As QBtoTL_Vendor = New QBtoTL_Vendor
    Dim vendorData As New QBtoTL_Vendor.VendorDataStructureQB

    Dim timetry_tltoqb As TLtoQB_TimeEntry = New TLtoQB_TimeEntry
    Dim selectedEmployeeData As New TLtoQB_TimeEntry.EmployeeDataStructure

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
        btn_currentweek_Click(sender, e)

        '-------------------------10---------------------------------------------
        'for type customers
        If Type = 10 Then
            TabPageCustomers.Visible = True
            TabControl1.SelectedIndex = 0

            'load grid (there might be an easire way)
            Dim col2 As New DataGridViewTextBoxColumn
            col2.Name = "Name"
            DataGridView1.Columns.Add(col2)
            Dim col3 As New DataGridViewTextBoxColumn
            col3.Name = "Last Modified"
            DataGridView1.Columns.Add(col3)
            Dim col4 As New DataGridViewTextBoxColumn
            col4.Name = "New"
            DataGridView1.Columns.Add(col4)

            If String.IsNullOrEmpty(My.Settings.CustomerLastSync.ToString()) Then
                ItemLastSync = #1/1/2000#
            Else
                ItemLastSync = Convert.ToDateTime(My.Settings.CustomerLastSync)
            End If

            My.Forms.MAIN.History("Synchonizing modified customers since:   " + ItemLastSync.ToString(), "n")

            customerData = customer_qbtotl.GetCustomerQBData(Me, True)
            ReadItems = customerData.NoItems - customerData.NoInactive

            If customerData Is Nothing Then
                My.Forms.MAIN.History("No customer data", "n")
                Exit Sub
            End If

            For Each element As QBtoTL_Customer.Customer In customerData.DataArray
                Dim result As Integer = DateTime.Compare(Convert.ToDateTime(element.QBModTime.ToString()),
            ItemLastSync)
                If result >= 0 Then
                    element.RecSelect = True
                End If
                'MAIN.FlagChangedItemsResults(element.QB_Name.ToString(), result)
            Next
            For Each element As QBtoTL_Customer.Customer In customerData.DataArray
                'Dim count As Int16 = ISQBID_In_DataTable(element.QB_ID)
                If element.Enabled Then ' Only show active customers
                    DataGridView1.Rows.Add(element.RecSelect, element.QB_Name.ToString(), element.QBModTime.ToString(), element.NewlyAdded)
                End If
            Next
        End If


        '-------------------------11---------------------------------------------
        'for type Employees
        If Type = 11 Then
            TabPageEmployees.Visible = True
            TabControl1.SelectedIndex = 1

            'load grid (there might be an easire way)
            Dim col2 As New DataGridViewTextBoxColumn
            col2.Name = "Name"
            DataGridView1.Columns.Add(col2)
            Dim col3 As New DataGridViewTextBoxColumn
            col3.Name = "Last Modified"
            DataGridView1.Columns.Add(col3)
            Dim col4 As New DataGridViewTextBoxColumn
            col4.Name = "New"
            DataGridView1.Columns.Add(col4)

            If String.IsNullOrEmpty(My.Settings.EmployeeLastSync.ToString()) Then
                ItemLastSync = #1/1/2000#
            Else
                ItemLastSync = Convert.ToDateTime(My.Settings.EmployeeLastSync)
            End If
            My.Forms.MAIN.History("Synchonizing modified employees since:   " + ItemLastSync.ToString(), "n")

            employeeData = employee_qbtotl.GetEmployeeQBData(Me, True)
            ReadItems = employeeData.NoItems

            If employeeData Is Nothing Then
                My.Forms.MAIN.History("No employee data", "n")
                Exit Sub
            End If

            For Each element As QBtoTL_Employee.Employee In employeeData.DataArray
                Dim result As Integer = DateTime.Compare(Convert.ToDateTime(element.QBModTime.ToString()),
            ItemLastSync)
                If result >= 0 Then
                    element.RecSelect = True
                End If
                'MAIN.FlagChangedItemsResults(element.QB_Name.ToString(), result)
            Next
            For Each element As QBtoTL_Employee.Employee In employeeData.DataArray
                DataGridView1.Rows.Add(element.RecSelect, element.QB_Name.ToString(), element.QBModTime.ToString(), element.NewlyAdded)
            Next
        End If

        '-------------------------12---------------------------------------------
        'for type Vendors
        If Type = 12 Then
            TabControl1.SelectedIndex = 2

            'load grid (there might be an easire way)
            Dim col2 As New DataGridViewTextBoxColumn
            col2.Name = "Name"
            DataGridView1.Columns.Add(col2)
            Dim col3 As New DataGridViewTextBoxColumn
            col3.Name = "Last Modified"
            DataGridView1.Columns.Add(col3)
            Dim col4 As New DataGridViewTextBoxColumn
            col4.Name = "New"
            DataGridView1.Columns.Add(col4)

            If String.IsNullOrEmpty(My.Settings.VendorLastSync.ToString()) Then
                ItemLastSync = #1/1/2000#
            Else
                ItemLastSync = Convert.ToDateTime(My.Settings.VendorLastSync)
            End If
            My.Forms.MAIN.History("Synchonizing modified vendors since:   " + ItemLastSync.ToString(), "n")

            vendorData = vendor_qbtotl.GetVendorQBData(Me, True)
            ReadItems = vendorData.NoItems

            If vendorData Is Nothing Then
                My.Forms.MAIN.History("No Vendor data", "n")
                Exit Sub
            End If

            For Each element As QBtoTL_Vendor.Vendor In vendorData.DataArray
                Dim result As Integer = DateTime.Compare(Convert.ToDateTime(element.QBModTime.ToString()),
                                                         ItemLastSync)
                If result >= 0 Then
                    element.RecSelect = True
                End If
                'MAIN.FlagChangedItemsResults(element.QB_Name.ToString(), result)
            Next
            For Each element As QBtoTL_Vendor.Vendor In vendorData.DataArray
                DataGridView1.Rows.Add(element.RecSelect, element.QB_Name.ToString(), element.QBModTime.ToString(), element.NewlyAdded)
            Next
            SelectAll = False
        End If

        '-------------------------13---------------------------------------------
        'for type Jobs_Sub Jobs
        If Type = 13 Then
            TabControl1.SelectedIndex = 3

            'load grid (there might be an easier way)
            Dim col2 As New DataGridViewTextBoxColumn
            col2.Name = "Full Name"
            DataGridView1.Columns.Add(col2)
            Dim col3 As New DataGridViewTextBoxColumn
            col3.Name = "Name"
            DataGridView1.Columns.Add(col3)
            Dim col4 As New DataGridViewTextBoxColumn
            col4.Name = "Last Modified"
            DataGridView1.Columns.Add(col4)
            Dim col5 As New DataGridViewTextBoxColumn
            col5.Name = "New"
            DataGridView1.Columns.Add(col5)

            ' check here 
            If String.IsNullOrEmpty(My.Settings.JobLastSync.ToString()) Then
                ItemLastSync = #1/1/2000#
            Else
                ItemLastSync = Convert.ToDateTime(My.Settings.JobLastSync)
            End If
            My.Forms.MAIN.History("Synchonizing modified jobs since:   " + ItemLastSync.ToString(), "n")

            JobData = job_qbtotl.GetJobSubJobData(Me, p_token, True)
            ReadItems = jobData.NoItems

            If jobData Is Nothing Then
                My.Forms.MAIN.History("No Jobs_SubJobs data", "n")
                Exit Sub
            End If

            For Each element As QBtoTL_JobOrItem.Job_Subjob In JobData.DataArray
                Dim result As Integer = DateTime.Compare(Convert.ToDateTime(element.QBModTime.ToString()),
                                                         ItemLastSync)
                If result >= 0 Then
                    element.RecSelect = True
                End If
            Next

            'Load DataGrid
            For Each element As QBtoTL_JobOrItem.Job_Subjob In JobData.DataArray
                DataGridView1.Rows.Add(element.RecSelect, element.FullName.ToString(), element.QB_Name.ToString(), element.QBModTime.ToString(), element.NewlyAdded)
            Next
            SelectAll = False
        End If

        '-------------------------14---------------------------------------------
        '  For Type items_SubItems
        If Type = 14 Then
            TabPageEmployees.Visible = True
            TabControl1.SelectedIndex = 3

            'load grid (there might be an easier way)
            Dim col2 As New DataGridViewTextBoxColumn
            col2.Name = "Full Name"
            DataGridView1.Columns.Add(col2)
            Dim col3 As New DataGridViewTextBoxColumn
            col3.Name = "Name"
            DataGridView1.Columns.Add(col3)
            Dim col4 As New DataGridViewTextBoxColumn
            col4.Name = "Last Modified"
            DataGridView1.Columns.Add(col4)
            Dim col5 As New DataGridViewTextBoxColumn
            col5.Name = "New"
            DataGridView1.Columns.Add(col5)

            If String.IsNullOrEmpty(My.Settings.ItemLastSync.ToString()) Then
                ItemLastSync = #1/1/2000#
            Else
                ItemLastSync = Convert.ToDateTime(My.Settings.ItemLastSync)
            End If

            My.Forms.MAIN.History("Synchonizing modified items since:   " + ItemLastSync.ToString(), "n")

            JobData = job_qbtotl.GetItemSubItemData(Me, p_token, True)
            ReadItems = JobData.NoItems

            If JobData Is Nothing Then
                My.Forms.MAIN.History("No Item_SubItem Data", "n")
                Exit Sub
            End If

            For Each element As QBtoTL_JobOrItem.Job_Subjob In JobData.DataArray
                Dim result As Integer = DateTime.Compare(Convert.ToDateTime(element.QBModTime.ToString()),
                ItemLastSync)
                If result >= 0 Then
                    element.RecSelect = True
                End If
                'MAIN.FlagChangedItemsResults(element.QB_Name.ToString(), result)
            Next

            For Each element As QBtoTL_JobOrItem.Job_Subjob In JobData.DataArray
                DataGridView1.Rows.Add(element.RecSelect, element.FullName.ToString(), element.QB_Name.ToString(), element.QBModTime.ToString(), element.NewlyAdded)
            Next
            SelectAll = False
        End If

        '-------------------------20---------------------------------------------
        'for type Time Items
        If Type = 20 Then
            TabPageTimeTransfer.Visible = True
            TabControl1.SelectedIndex = 4

            'load grid (there might be an easire way)
            Dim col2 As New DataGridViewTextBoxColumn
            col2.Name = "Name"
            DataGridView1.Columns.Add(col2)
            Dim col3 As New DataGridViewTextBoxColumn
            col3.Name = "Employee ID"
            DataGridView1.Columns.Add(col3)

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
            ReadItems = selectedEmployeeData.NoItems
            For Each element As TLtoQB_TimeEntry.Employee In selectedEmployeeData.DataArray
                'My.Forms.MAIN.History("Debug:   " + element.RecSelect.ToString(), "n")
                DataGridView1.Rows.Add(element.RecSelect, element.FullName.ToString(), element.AccountEmployeeId.ToString())
            Next
        End If

        My.Forms.MAIN.History(ReadItems.ToString() + " items were read from Quickbooks", "n")

        'wait for one second so user can see progress bar
        System.Threading.Thread.Sleep(500)
        System.Windows.Forms.Application.DoEvents()

        Me.ProgressBar1.Value = 0
        SelectAll = False
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
        Me.Close()
    End Sub

    Private Sub btnTransfer_Click(sender As Object, e As EventArgs) Handles btnTransfer.Click
        Dim ItemsProcessed As Integer = 0

        'When processing customers
        If Type = 10 Then
            Reset_Checked_Customer_Value(customerData)
            Set_Selected_Customer()
            ItemsProcessed = customer_qbtotl.QBTransferCustomerToTL(customerData, p_token, Me, True)
            My.Forms.MAIN.History(ItemsProcessed.ToString() + " TimeLive records were created or updated", "i")
            My.Settings.CustomerLastSync = DateTime.Now.ToString()
            My.Settings.Save()
        End If

        'When processing Employees
        If Type = 11 Then
            Reset_Checked_Employee_Value(employeeData)
            Set_Selected_Employee()
            ItemsProcessed = employee_qbtotl.QBTransferEmployeeToTL(employeeData, p_token, Me, True)
            My.Forms.MAIN.History(ItemsProcessed.ToString() + " TimeLive records were created or updated", "i")
            My.Settings.EmployeeLastSync = DateTime.Now.ToString()
            My.Settings.Save()
        End If

        'When processing vendor
        If Type = 12 Then
            Reset_Checked_Vendor_Value(vendorData)
            Set_Selected_Vendor()
            ItemsProcessed = vendor_qbtotl.QBTransferVendorToTL(vendorData, p_token, Me, True)
            My.Forms.MAIN.History(ItemsProcessed.ToString() + " TimeLive records were created or updated", "i")
            My.Settings.VendorLastSync = DateTime.Now.ToString()
            My.Settings.Save()
        End If

        'When processing Jobs
        If Type = 13 Then
            Reset_Checked_Job_Value(JobData)
            Set_Selected_Job_Item()
            ItemsProcessed = job_qbtotl.QBTransferJobstoTL(JobData, p_token, Me, True)
            My.Forms.MAIN.History(ItemsProcessed.ToString() + " TimeLive records were created or updated", "i")
            My.Settings.JobLastSync = DateTime.Now.ToString()
            My.Settings.Save()
        End If

        'When processing items
        If Type = 14 Then
            Reset_Checked_Job_Value(JobData)
            Set_Selected_Job_Item()
            ItemsProcessed = job_qbtotl.QBTransferItemsToTL(JobData, p_token, Me, True)
            My.Forms.MAIN.History(ItemsProcessed.ToString() + " TimeLive records were created or updated", "i")
            My.Settings.ItemLastSync = DateTime.Now.ToString()
            My.Settings.Save()
        End If

        'When processing Time Transfer
        If Type = 20 Then
            Reset_Checked_SelectedEmployee_Value(selectedEmployeeData)
            Set_Selected_SelectedEmployee()
            Dim IntUI_2ndSelect As New IntUI_2ndSelect
            IntUI_2ndSelect.Owner = Me
            IntUI_2ndSelect.Show(p_token, p_AccountId, selectedEmployeeData,
                                             CDate(dpStartDate.Value).Date,
                                             CDate(dpEndDate.Value).Date.ToString, 201)

            My.Settings.TimeTrackingLastSync = DateTime.Now.ToString()
            My.Settings.Save()
        End If

        'wait for one second so user can see progress bar
        System.Threading.Thread.Sleep(500)
        System.Windows.Forms.Application.DoEvents()

        Me.ProgressBar1.Value = 0
    End Sub

    Private Sub Reset_Checked_Customer_Value(ByRef customerObj As QBtoTL_Customer.CustomerDataStructureQB)
        ' reset the check value to zero
        For Each element As QBtoTL_Customer.Customer In customerObj.DataArray
            element.RecSelect = False
        Next
    End Sub

    Private Sub Reset_Checked_Vendor_Value(ByRef vendorObj As QBtoTL_Vendor.VendorDataStructureQB)
        ' reset the check value to zero
        For Each element As QBtoTL_Vendor.Vendor In vendorObj.DataArray
            element.RecSelect = False
        Next
    End Sub

    Private Sub Reset_Checked_Job_Value(ByRef jobObj As QBtoTL_JobOrItem.JobDataStructureQB)
        ' reset the check value to zero
        For Each element As QBtoTL_JobOrItem.Job_Subjob In jobObj.DataArray
            element.RecSelect = False
        Next
    End Sub

    Private Sub Reset_Checked_Employee_Value(ByRef EmployeeObj As QBtoTL_Employee.EmployeeDataStructureQB)
        ' reset the check value to zero
        For Each element As QBtoTL_Employee.Employee In EmployeeObj.DataArray
            element.RecSelect = False
        Next
    End Sub

    Private Sub Reset_Checked_SelectedEmployee_Value(ByRef SelectedEmployeeObj As TLtoQB_TimeEntry.EmployeeDataStructure)
        ' reset the check value to zero
        For Each element As TLtoQB_TimeEntry.Employee In SelectedEmployeeObj.DataArray
            element.RecSelect = False
            'My.Forms.MAIN.History("Time selection:  Reseting employees:   " + element.RecSelect.ToString(), "n")
        Next
    End Sub

    Private Sub Set_Selected_Customer()
        For Each row As DataGridViewRow In DataGridView1.Rows
            If row.Cells("Name").Value IsNot Nothing And row.Cells("ckBox").Value = True Then
                customerData.DataArray.ForEach(
                    Sub(customer)
                        If customer.QB_Name = row.Cells("Name").Value.ToString Then
                            customer.RecSelect = True
                        End If
                    End Sub
                )
                'customerData.DataArray(row.Index).RecSelect = True
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
                'employeeData.DataArray(row.Index).RecSelect = True
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
                'selectedEmployeeData.DataArray(row.Index).RecSelect = True
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
                'vendorData.DataArray(row.Index).RecSelect = True
                My.Forms.MAIN.History("Vendors selected for processing: " + row.Cells("Name").Value, "n")
            End If
        Next
    End Sub

    Private Sub Set_Selected_Job_Item()
        For Each row As DataGridViewRow In DataGridView1.Rows
            If row.Cells("Name").Value IsNot Nothing And row.Cells("ckBox").Value = True Then
                JobData.DataArray.ForEach(
                    Sub(job)
                        If job.QB_Name = row.Cells("Name").Value.ToString Then
                            job.RecSelect = True
                        End If
                    End Sub
                )
                'JobData.DataArray(row.Index).RecSelect = True
                My.Forms.MAIN.History("Job or items selected for processing: " + row.Cells("Name").Value, "n")
            End If
        Next
    End Sub

    Private Sub btnselectall_Click(sender As Object, e As EventArgs) Handles btnselectall.Click
        For Each row As DataGridViewRow In DataGridView1.Rows
            If row.Cells("Name").Value IsNot Nothing Then
                row.Cells("ckBox").Value = Not SelectAll
            End If
        Next
        SelectAll = Not SelectAll
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
        Dim result As Int16 = 0

        'Dim CustomerAdapter As New QB_TL_IDsTableAdapters.CustomersTableAdapter()
        'For Each TimeLiveID As QB_TL_IDs.CustomersRow In CustomerAdapter.GetCustomers()
        '    If String.Compare(Trim(TimeLiveID.QuickBooks_ID), Trim(myqbID)) = 0 Then
        '        Return True
        '        Exit For
        '    End If
        'Next
        Dim CustomerAdapter As New QB_TL_IDsTableAdapters.CustomersTableAdapter()
        Dim TimeLiveIDs As QB_TL_IDs.CustomersDataTable = CustomerAdapter.GetCorrespondingTL_ID(myqbID)

        If TimeLiveIDs.Count = 1 Then
            result = 1
            My.Forms.MAIN.History("One record found in QB sync table", "i")
        End If

        If TimeLiveIDs.Count = 0 Then
            result = 0
            My.Forms.MAIN.History("No records found on QB sync table", "i")
        End If

        If TimeLiveIDs.Count > 1 Then
            result = 2
            My.Forms.MAIN.History("More than one record found", "I")
        End If

        Return result
    End Function

    Private Sub cbWageType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbWageType.SelectedIndexChanged
        My.Settings.QBWageType = cbWageType.SelectedIndex
        My.Settings.Save()
    End Sub
End Class