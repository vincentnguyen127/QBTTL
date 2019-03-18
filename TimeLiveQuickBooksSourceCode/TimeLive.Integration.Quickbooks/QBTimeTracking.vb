Imports Interop.QBFC10
Imports System.Globalization
Public Class QBTimeTracking
    Dim timer As Integer = 0
    Private p_token As String
    Private p_AccountId As String

    Public my_payroll
    Public my_class
    Public my_cbType
    Public sat, tgif, cur_week


    'Not needed
    Public Overloads Sub Show(ByVal token As String, ByVal AccountId As String)
        If token Is Nothing Then
            MsgBox("Please Login")
        Else
            p_AccountId = AccountId
            p_token = token
            MyBase.Show()
        End If
    End Sub

    'Saves variables  some of it will go into main others into integrated ui
    Private Sub QBTimeTracking_Close(ByVal sender As Object,
    ByVal e As System.EventArgs) Handles MyBase.Closed


        My.Settings.QBWageType = cbWageType.SelectedIndex
        My.Settings.Save()


    End Sub

    'Saves variables  some of it will go into main others into integrated ui
    Private Sub QBTimeTracking_Load(ByVal sender As Object,
    ByVal e As System.EventArgs) Handles MyBase.Load

        If AppSettings.chkPayrollTimesheet.Checked = True Then

            Me.cbWageType.Enabled = True
        Else

            Me.cbWageType.Enabled = False
        End If

        '-------- restore wageType 
        If My.Settings.QBWageType = "" Then
            cbWageType.SelectedIndex = 0
        Else
            cbWageType.SelectedIndex = My.Settings.QBWageType
        End If


    End Sub
    '
    Private Sub EvlOrCute()

        For Each row As DataGridViewRow In DataGridView1.Rows
            'check which 
            Dim chk As DataGridViewCheckBoxCell = row.Cells("checkbox1")

            If chk.Value = True Then

                My.Forms.MAIN.History("Checked:" + chk.RowIndex.ToString(), "n")
                My.Forms.MAIN.History("FullName:" + DataGridView1.Rows(chk.RowIndex).Cells("FullName").Value, "n")
                My.Forms.MAIN.History("Account ID:" + DataGridView1.Rows(chk.RowIndex).Cells("AccountEmployeeId").Value.ToString, "n")



                pgbar.Value = 0
                Try
                    Dim objTimeTrackingServices As New Services.TimeLive.TimeEntries.TimeEntries
                    Dim authentication As New Services.TimeLive.TimeEntries.SecuredWebServiceHeader
                    authentication.AuthenticatedToken = p_token
                    objTimeTrackingServices.SecuredWebServiceHeaderValue = authentication
                    Dim objTimeEntryArray() As Object


                    objTimeEntryArray = objTimeTrackingServices.GetTimeEntriesByEmployeeIdAndDateRange(DataGridView1.Rows(chk.RowIndex).Cells("AccountEmployeeId").Value, CDate(dpStartDate.Value).Date, CDate(dpEndDate.Value).Date)

                    Dim objTimeEntry As New Services.TimeLive.TimeEntries.TimeEntry
                    Dim pblenth As Integer = objTimeEntryArray.Length - 1
                    If pblenth >= 0 Then
                        pgbar.Maximum = pblenth
                    End If
                    For n As Integer = 0 To objTimeEntryArray.Length - 1
                        pgbar.Increment(n)
                        objTimeEntry = objTimeEntryArray(n)
                        With objTimeEntry
                            AddTimeEntryInQB(.ClientName, .EmployeeName, .IsBillable, .ProjectName, .TaskWithParent, .TotalTime,
                                             .TimeEntryDate,
                                             IIf(chkPayrollTimesheet.Checked = True, GetClass(objTimeEntry), "<None>"),
                                             IIf(chkPayrollTimesheet.Checked = True, GetPayrollItem(objTimeEntry), "<None>"))

                            IIf(chkPayrollTimesheet.Checked = True, GetPayrollItem(objTimeEntry), "<None>")
                            My.Forms.MAIN.History("After Processing: " +
                                                  IIf(chkPayrollTimesheet.Checked = True, GetPayrollItem(objTimeEntry), "<None>"), "i")


                        End With
                    Next


                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try

            End If

        Next


        'Dim i As Integer
        '        For i = 0 To DataGridView1.RowCount - 1
        '            If Not (DataGridView1.Rows(i).Cells("FullName").Value Is Nothing) Then
        '                '  MsgBox(DataGridView1.Rows(i).Cells("FullName").Value)
        '                My.Forms.StatusWindow.TBStatus.Text += ">>  " + DataGridView1.Rows(i).Cells("FullName").Value + vbNewLine
        '            End If
        '        Next


    End Sub

    'Adds entries to QB based on Job Item Selection
    Public Sub AddTimeEntryInQB(ByVal CustomerName As String, ByVal EmployeeName As String, ByVal IsBillable As Boolean, ByVal ProjectName As String, ByVal ServiceItemName As String, ByVal TotalTime As DateTime, ByVal TimeEntryDate As Date, ByVal TimeEntryClass As String, ByVal PayrollItem As String)

        If rbtJobitems.Checked = True Then
            AddTimeEntryInQBJobItem(CustomerName, EmployeeName, IsBillable, ProjectName, ServiceItemName, TotalTime, TimeEntryDate, TimeEntryClass, PayrollItem)
        ElseIf rbJob.Checked = True Then
            AddTimeEntryInQBJobSubJob(CustomerName, EmployeeName, IsBillable, ProjectName, ServiceItemName, TotalTime, TimeEntryDate, TimeEntryClass, PayrollItem)
        ElseIf rbItem.Checked = True Then
            AddTimeEntryInQBItemSubItem(CustomerName, EmployeeName, IsBillable, ProjectName, ServiceItemName, TotalTime, TimeEntryDate, TimeEntryClass, PayrollItem)
        End If

    End Sub

    Public Sub AddTimeEntryInQBJobItem(ByVal CustomerName As String, ByVal EmployeeName As String, ByVal IsBillable As Boolean, ByVal ProjectName As String, ByVal ServiceItemName As String, ByVal TotalTime As DateTime, ByVal TimeEntryDate As Date, ByVal TimeEntryClass As String, ByVal PayrollItem As String)

        'step1: create QBFC session manager and prepare the request
        Dim sessManager As QBSessionManager
        Dim msgSetRs As IMsgSetResponse
        Try
            ProjectName = SetLength(ProjectName)
            sessManager = New QBSessionManagerClass()
            Dim msgSetRq As IMsgSetRequest = sessManager.CreateMsgSetRequest("US", 2, 0)
            msgSetRq.Attributes.OnError = ENRqOnError.roeContinue
            Dim timeAdd As ITimeTrackingAdd = msgSetRq.AppendTimeTrackingAddRq
            timeAdd.CustomerRef.FullName.SetValue(CustomerName & ": " & ProjectName)
            timeAdd.Duration.SetValue(TotalTime.Hour, TotalTime.Minute, 0, False)
            timeAdd.EntityRef.FullName.SetValue(EmployeeName)
            timeAdd.IsBillable.SetValue(IsBillable)
            timeAdd.ItemServiceRef.FullName.SetValue(ServiceItemName)
            timeAdd.TxnDate.SetValue(TimeEntryDate)
            If Not TimeEntryClass = "<None>" Then
                AddClass(TimeEntryClass)
                timeAdd.ClassRef.FullName.SetValue(TimeEntryClass)
            End If
            If Not PayrollItem = "<None>" Then
                AddPayrollItem(PayrollItem)
                timeAdd.PayrollItemWageRef.FullName.SetValue(PayrollItem)
            End If

            'step2: begin QB session and send the request
            sessManager.OpenConnection("App", "TimeLive Quickbooks")
            sessManager.BeginSession("", ENOpenMode.omDontCare)
            msgSetRs = sessManager.DoRequests(msgSetRq)
            If msgSetRs.ResponseList.GetAt(0).StatusSeverity = "Error" Then
                Throw New Exception(msgSetRs.ResponseList.GetAt(0).StatusMessage)
            End If
        Catch ex As Exception
            Throw ex
        Finally
            If Not sessManager Is Nothing Then
                sessManager.EndSession()
                sessManager.CloseConnection()
            End If
        End Try
    End Sub
    Public Sub AddTimeEntryInQBItemSubItem(ByVal CustomerName As String, ByVal EmployeeName As String, ByVal IsBillable As Boolean, ByVal ProjectName As String, ByVal ServiceItemName As String, ByVal TotalTime As DateTime, ByVal TimeEntryDate As Date, ByVal TimeEntryClass As String, ByVal PayrollItem As String)

        'step1: create QBFC session manager and prepare the request
        Dim sessManager As QBSessionManager
        Dim msgSetRs As IMsgSetResponse
        Try
            ProjectName = SetLength(ProjectName)
            sessManager = New QBSessionManagerClass()
            Dim msgSetRq As IMsgSetRequest = sessManager.CreateMsgSetRequest("US", 2, 0)
            msgSetRq.Attributes.OnError = ENRqOnError.roeContinue
            Dim timeAdd As ITimeTrackingAdd = msgSetRq.AppendTimeTrackingAddRq
            timeAdd.CustomerRef.FullName.SetValue(CustomerName)
            timeAdd.Duration.SetValue(TotalTime.Hour, TotalTime.Minute, 0, False)
            timeAdd.EntityRef.FullName.SetValue(EmployeeName)
            timeAdd.IsBillable.SetValue(IsBillable)
            timeAdd.ItemServiceRef.FullName.SetValue(ProjectName & ":" & ServiceItemName)
            timeAdd.TxnDate.SetValue(TimeEntryDate)
            If Not TimeEntryClass = "<None>" Then
                timeAdd.ClassRef.FullName.SetValue(TimeEntryClass)
            End If
            If Not PayrollItem = "<None>" Then
                timeAdd.PayrollItemWageRef.FullName.SetValue(PayrollItem)
            End If

            'step2: begin QB session and send the request
            sessManager.OpenConnection("App", "TimeLive Quickbooks")
            sessManager.BeginSession("", ENOpenMode.omDontCare)
            msgSetRs = sessManager.DoRequests(msgSetRq)
            If msgSetRs.ResponseList.GetAt(0).StatusSeverity = "Error" Then
                Throw New Exception(msgSetRs.ResponseList.GetAt(0).StatusMessage)
            End If
        Catch ex As Exception
            Throw ex
        Finally
            If Not sessManager Is Nothing Then
                sessManager.EndSession()
                sessManager.CloseConnection()
            End If
        End Try
    End Sub
    Public Sub AddTimeEntryInQBJobSubJob(ByVal CustomerName As String, ByVal EmployeeName As String, ByVal IsBillable As Boolean, ByVal ProjectName As String, ByVal ServiceItemName As String, ByVal TotalTime As DateTime, ByVal TimeEntryDate As Date, ByVal TimeEntryClass As String, ByVal PayrollItem As String)

        'step1: create QBFC session manager and prepare the request
        Dim sessManager As QBSessionManager
        Dim msgSetRs As IMsgSetResponse
        Try
            ProjectName = SetLength(ProjectName)
            sessManager = New QBSessionManagerClass()
            Dim msgSetRq As IMsgSetRequest = sessManager.CreateMsgSetRequest("US", 2, 0)
            msgSetRq.Attributes.OnError = ENRqOnError.roeContinue
            Dim timeAdd As ITimeTrackingAdd = msgSetRq.AppendTimeTrackingAddRq
            timeAdd.CustomerRef.FullName.SetValue(CustomerName & ":" & ProjectName & ":" & ServiceItemName)
            timeAdd.Duration.SetValue(TotalTime.Hour, TotalTime.Minute, 0, False)
            timeAdd.EntityRef.FullName.SetValue(EmployeeName)
            timeAdd.IsBillable.SetValue(IsBillable)
            AddNoneItemInQB("<None>", "<None>")
            timeAdd.ItemServiceRef.FullName.SetValue("<None>")
            timeAdd.TxnDate.SetValue(TimeEntryDate)
            If Not TimeEntryClass = "<None>" Then
                timeAdd.ClassRef.FullName.SetValue(TimeEntryClass)
            End If
            If Not PayrollItem = "<None>" Then
                timeAdd.PayrollItemWageRef.FullName.SetValue(PayrollItem)
            End If

            'step2: begin QB session and send the request
            sessManager.OpenConnection("App", "TimeLive Quickbooks")
            sessManager.BeginSession("", ENOpenMode.omDontCare)
            msgSetRs = sessManager.DoRequests(msgSetRq)
            If msgSetRs.ResponseList.GetAt(0).StatusSeverity = "Error" Then
                Throw New Exception(msgSetRs.ResponseList.GetAt(0).StatusMessage)
            End If
        Catch ex As Exception
            Throw ex
        Finally
            If Not sessManager Is Nothing Then
                sessManager.EndSession()
                sessManager.CloseConnection()
            End If
        End Try
    End Sub

    ' 
    Public Sub AddNoneItemInQB(ByVal ItemName As String, ByVal ServiceItemAccount As String)
        'step1: create QBFC session manager and prepare the request
        Dim sessManager As QBSessionManager
        Dim msgSetRs As IMsgSetResponse
        Try
            ItemName = SetLength(ItemName)
            sessManager = New QBSessionManagerClass()
            Dim msgSetRq As IMsgSetRequest = sessManager.CreateMsgSetRequest("US", 2, 0)
            msgSetRq.Attributes.OnError = ENRqOnError.roeContinue
            Dim AccountAdd As IAccountAdd = msgSetRq.AppendAccountAddRq
            AccountAdd.Name.SetValue(ServiceItemAccount)
            AccountAdd.AccountType.SetValue(ENAccountType.atExpense)
            Dim ItemAdd As IItemServiceAdd = msgSetRq.AppendItemServiceAddRq
            ItemAdd.Name.SetValue(ItemName)
            ItemAdd.ORSalesPurchase.SalesOrPurchase.AccountRef.FullName.SetValue(ServiceItemAccount)
            'step2: begin QB session and send the request
            sessManager.OpenConnection("App", "TimeLive Quickbooks")
            sessManager.BeginSession("", ENOpenMode.omDontCare)
            msgSetRs = sessManager.DoRequests(msgSetRq)
            'If msgSetRs.ResponseList.GetAt(0).StatusSeverity = "Error" Then
            '    Throw New Exception(msgSetRs.ResponseList.GetAt(0).StatusMessage)
            'End If
        Catch ex As Exception
            Throw ex
        Finally
            If Not sessManager Is Nothing Then
                sessManager.EndSession()
                sessManager.CloseConnection()
            End If
        End Try
    End Sub
    Public Function SetLength(ByVal Name As String) As String
        If Name.Length > 31 Then
            Name = Name.Substring(0, 31)
        End If
        Return Name
    End Function


    Public Function GetClass(ByVal objTimeEntry As Services.TimeLive.TimeEntries.TimeEntry) As String
        Dim TimeEntryClass As String = "<None>"
        With objTimeEntry
            If AppSettings.cbClass.SelectedItem = "CostCenter" Then
                Return .CostCenter
            ElseIf AppSettings.cbClass.SelectedItem = "Department" Then
                Return .EmployeeDepartment
            ElseIf AppSettings.cbClass.SelectedItem = "EmployeeType" Then
                Return .EmployeeType
            ElseIf AppSettings.cbClass.SelectedItem = "Milestone" Then
                Return .Milestone
            ElseIf AppSettings.cbClass.SelectedItem = "WorkType" Then
                Return .WorkType
            End If
        End With
        Return TimeEntryClass
    End Function
    Public Function GetPayrollItem(ByVal objTimeEntry As Services.TimeLive.TimeEntries.TimeEntry) As String
        Dim PayrollItem As String = "<None>"

        With objTimeEntry
            If Me.cbPayrollItem.SelectedItem = "CostCenter" Then
                Return .CostCenter
            ElseIf Me.cbPayrollItem.SelectedItem = "Department" Then
                Return .EmployeeDepartment
            ElseIf Me.cbPayrollItem.SelectedItem = "EmployeeType" Then
                Return .EmployeeType
            ElseIf Me.cbPayrollItem.SelectedItem = "Milestone" Then
                Return .Milestone
            ElseIf Me.cbPayrollItem.SelectedItem = "WorkType" Then
                Return .WorkType
            End If
        End With
        Return PayrollItem
    End Function

    '*****************Transferred to integrated UI
    Private Sub TimeTracking_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim objEmployeeServices As New Services.TimeLive.Employees.Employees
        Dim authentication As New Services.TimeLive.Employees.SecuredWebServiceHeader
        authentication.AuthenticatedToken = p_token
        objEmployeeServices.SecuredWebServiceHeaderValue = authentication
        Dim dv As New DataView(objEmployeeServices.GetEmployeesData)

        'dv = New DataView(objEmployeeServices.GetEmployeesData)
        'Dim something As New Services.TimeLive.Employees.Employee
        'Dim loo = objEmployeeServices.GetEmployees()
        'Dim ro = dv.Table.TableName

        ' this is how to display it 
        'Me.cbEmployee.DataSource = dv
        'Me.cbEmployee.DisplayMember = "FullName"
        'Me.cbEmployee.ValueMember = "AccountEmployeeId"
        'Me.cbEmployee.SelectedIndex = 0
        Me.DataGridView1.DataSource = dv
        For i As Integer = 0 To DataGridView1.Columns.Count - 1
            DataGridView1.Columns(i).Visible = False
        Next
        DataGridView1.Columns(0).Visible = True

        DataGridView1.Columns("FullName").Visible = True
        DataGridView1.Columns("AccountEmployeeId").Visible = True

        'Me.DataGridView1.display
        'Me.cbClass.SelectedIndex = 0
        'Me.cbPayrollItem.SelectedIndex = 0
        'Me.cbWageType.SelectedIndex = 3
    End Sub




    'Add class to Quickbooks if it does not exist (should be prevented)
    Public Sub AddClass(ByVal ClassName As String)
        'step1: create QBFC session manager and prepare the request
        Dim sessManager As QBSessionManager
        Dim msgSetRs As IMsgSetResponse
        Try
            sessManager = New QBSessionManagerClass()
            Dim msgSetRq As IMsgSetRequest = sessManager.CreateMsgSetRequest("US", 2, 0)
            msgSetRq.Attributes.OnError = ENRqOnError.roeContinue
            Dim ClassAdd As IClassAdd = msgSetRq.AppendClassAddRq
            ClassAdd.Name.SetValue(ClassName)

            'step2: begin QB session and send the request
            sessManager.OpenConnection("App", "TimeLive Quickbooks")
            sessManager.BeginSession("", ENOpenMode.omDontCare)
            msgSetRs = sessManager.DoRequests(msgSetRq)
            If Not msgSetRs.ResponseList.GetAt(0).StatusMessage.Contains("already in use") Then
                If msgSetRs.ResponseList.GetAt(0).StatusSeverity = "Error" Then
                    Throw New Exception(msgSetRs.ResponseList.GetAt(0).StatusMessage)
                End If
            End If
        Catch ex As Exception
            Throw ex
        Finally
            If Not sessManager Is Nothing Then
                sessManager.EndSession()
                sessManager.CloseConnection()
            End If
        End Try
    End Sub

    'Add payroll item to Quickbooks if it does not exist (should be prevented)
    Public Sub AddPayrollItem(ByVal PayrollItem As String)
        'step1: create QBFC session manager and prepare the request
        Dim sessManager As QBSessionManager
        Dim msgSetRs As IMsgSetResponse
        Try
            sessManager = New QBSessionManagerClass()
            Dim msgSetRq As IMsgSetRequest = sessManager.CreateMsgSetRequest("US", 2, 0)
            msgSetRq.Attributes.OnError = ENRqOnError.roeContinue
            Dim PayrollItemAdd As IPayrollItemWageAdd = msgSetRq.AppendPayrollItemWageAddRq
            PayrollItemAdd.Name.SetValue(PayrollItem)
            PayrollItemAdd.ExpenseAccountRef.FullName.SetValue("Payroll Expenses")
            PayrollItemAdd.WageType.SetValue(GetWageType(cbWageType.SelectedItem))

            'step2: begin QB session and send the request
            sessManager.OpenConnection("App", "TimeLive Quickbooks")
            sessManager.BeginSession("", ENOpenMode.omDontCare)
            msgSetRs = sessManager.DoRequests(msgSetRq)
            If Not msgSetRs.ResponseList.GetAt(0).StatusMessage.Contains("already in use") Then
                If msgSetRs.ResponseList.GetAt(0).StatusSeverity = "Error" Then
                    Throw New Exception(msgSetRs.ResponseList.GetAt(0).StatusMessage)
                End If
            End If
        Catch ex As Exception
            Throw ex
        Finally
            If Not sessManager Is Nothing Then
                sessManager.EndSession()
                sessManager.CloseConnection()
            End If
        End Try
    End Sub
    Public Function GetWageType(ByVal val As String) As ENWageType
        If val = "Bonus" Then
            Return ENWageType.wtBonus
        ElseIf val = "Comission" Then
            Return ENWageType.wtCommission
        ElseIf val = "Hourly" Then
            Return ENWageType.wtHourly
        ElseIf val = "Hourly-Overtime" Then
            Return ENWageType.wtHourlyOvertime
        ElseIf val = "Hourly-Regular" Then
            Return ENWageType.wtHourlyRegular
        ElseIf val = "Hourly-Sick" Then
            Return ENWageType.wtHourlySick
        ElseIf val = "Hourly-Vacation" Then
            Return ENWageType.wtHourlyVacation
        ElseIf val = "Salary" Then
            Return ENWageType.wtSalary
        ElseIf val = "Salary-Regular" Then
            Return ENWageType.wtSalaryRegular
        ElseIf val = "Salary-Sick" Then
            Return ENWageType.wtSalarySick
        ElseIf val = "Salary-Vacation" Then
            Return ENWageType.wtSalaryVacation
        End If
    End Function

    'Dont really need to assigned here
    Private Sub cbClass_SelectedIndexChanged(sender As Object, e As EventArgs)
        my_class = AppSettings.cbClass.SelectedItem
    End Sub



    'Ye's modifications
    Private Sub btn_currentweek_Click(sender As Object, e As EventArgs) Handles btn_currentweek.Click
        'errors  gonna debug later
        'dpStartDate   dpEndDate
        'work only withnin a month
        'Dim today As Date = Date.Today
        'Dim dayDiff As Integer = today.DayOfWeek - DayOfWeek.Saturday
        'sat = today.AddDays(-dayDiff)
        'dpStartDate.Value = sat
        'Dim tgifdiff As Integer = today.DayOfWeek - DayOfWeek.Friday
        'tgif = sat.AddDays(6)
        'dpEndDate.Value = tgif


        ' new version
        Dim myCI As New CultureInfo("en-US")
        Dim myCal As Calendar = myCI.Calendar
        Dim myCWR As CalendarWeekRule = myCI.DateTimeFormat.CalendarWeekRule
        Dim myFirstDOW As DayOfWeek = myCI.DateTimeFormat.FirstDayOfWeek

        ' sun day as the first day of week 
        'Dim test As New DateTime(2017, 11, 5)
        cur_week = myCal.GetWeekOfYear(DateTime.Now, myCWR, myFirstDOW)

        'Dim start_day_in_cur_week = GetWeekStartDate(cur_week, Now.Year)

        'If (Today) Then
        '    End
        'Dim d As Date
        'd = "11/12/2017"
        'Now.DayOfWeek.GetName()

        'Dim start_day_in_cur_week = FirstDateOfWeek(Now.Year.ToString, cur_week, DayOfWeek.Saturday)
        Dim sat As Date = GetFirstDayOfWeek(Now.Year, cur_week)

        If Now.DayOfWeek.CompareTo(sat.DayOfWeek) Then
            dpStartDate.Value = sat.AddDays(-1)
        Else
            dpStartDate.Value = sat.AddDays(-8)
        End If

        dpEndDate.Value = sat.AddDays(5)
        timer += 1
        Me.selectAllBtn_Click(sender, e)

    End Sub

    'Ye's modifications
    Public Function GetFirstDayOfWeek(year As Integer, weekNumber As Integer) As DateTime
        Return GetFirstDayOfWeek(year, weekNumber, Application.CurrentCulture)
    End Function
    'Ye's modifications
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

    'Ye's modifications
    'Private Sub Button1_Click_1(sender As Object, e As EventArgs) Handles preWeek.Click

    '    'dpStartDate.Value = sat.AddDays(-7)
    '    'dpEndDate.Value = tgif.AddDays(-7)
    '    cur_week = cur_week - 1
    '    Dim start_day_in_cur_week = FirstDateOfWeek(Now.Year.ToString, cur_week, DayOfWeek.Saturday)
    '    Dim sat As Date = GetFirstDayOfWeek(Now.Year, cur_week)

    '    If Now.DayOfWeek.CompareTo(sat.DayOfWeek) Then
    '        dpStartDate.Value = sat.AddDays(-1)
    '    Else
    '        dpStartDate.Value = sat.AddDays(-8)
    '    End If

    '    dpEndDate.Value = sat.AddDays(5)


    'End Sub
    Private Sub nextWeek_Click(sender As Object, e As EventArgs) Handles nextWeek.Click
        cur_week = cur_week + 1
        Dim start_day_in_cur_week = FirstDateOfWeek(Now.Year.ToString, cur_week, DayOfWeek.Saturday)
        Dim sat As Date = GetFirstDayOfWeek(Now.Year, cur_week)

        If Now.DayOfWeek.CompareTo(sat.DayOfWeek) Then
            dpStartDate.Value = sat.AddDays(-1)
        Else
            dpStartDate.Value = sat.AddDays(-8)
        End If

        dpEndDate.Value = sat.AddDays(5)
    End Sub
    'Ye's modifications
    Public Function FirstDateOfWeek(ByVal Year As Integer, ByVal Week As Integer, Optional FirstDayOfWeek As DayOfWeek = DayOfWeek.Monday) As Date
        Dim dt As Date = New Date(Year, 1, 1)
        If dt.DayOfWeek > 4 Then dt = dt.AddDays(7 - dt.DayOfWeek) Else dt = dt.AddDays(-dt.DayOfWeek)
        dt = dt.AddDays(FirstDayOfWeek)
        Return dt.AddDays(7 * (Week - 1))
    End Function


    'Ye's modifications
    Private Sub btn_Ok_Click(sender As Object, e As EventArgs) Handles btn_Ok.Click

        'localTB1.Text += "------------eval_states is 1" + vbNewLine
        My.Forms.MAIN.History("Time of day: " + TimeOfDay.ToString, "i")
        My.Forms.MAIN.History("Transfering Time to QuickBooks", "i")


        EvlOrCute()
        For Each row As DataGridViewRow In DataGridView1.Rows
            'check which 
            row.Cells("checkbox1").Value = False
        Next

        timer += 1
    End Sub

    'Ye's modifications
    Private Sub btnCancel_Click(sender As Object, e As EventArgs)

        'eval_states = 0
        'localTB1.Text += "----------Change: eval_states is 0" + vbNewLine
        'localTB1.Text += "<<" + "Cancel transfer" + vbNewLine
        'emu_bt.Text = "Emulate"
        timer += 1
        MsgBox("Total called " + timer.ToString)
        Me.Close()

    End Sub

    'Ye's modifications
    Private Sub selectAllBtn_Click(sender As Object, e As EventArgs) Handles selectAllBtn.Click
        Dim lastone As Integer = Convert.ToInt32(DataGridView1.Rows.Count.ToString) - 1
        ' MsgBox(lastone)
        For Each row As DataGridViewRow In DataGridView1.Rows
            'check which 
            row.Cells("checkbox1").Value = True
            Dim check As Integer = row.Cells("checkbox1").RowIndex
            If check = lastone Then

                row.Cells("checkbox1").Value = False

            End If
        Next
        timer += 1
    End Sub


End Class