Public Class AppSettings

    Public which_rbj As Integer
    Public job_item_btn_option As Integer
    'Public debug_mode As Boolean

    Private Sub Settings_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        dtp_autoruntime.Format = DateTimePickerFormat.Custom
        dtp_autoruntime.CustomFormat = "HH:mm"
        dtp_autoruntime.ShowUpDown = True
        If My.Settings.AutoRunTime = Nothing Then
            dtp_autoruntime.Text = ""
        Else
            dtp_autoruntime.Text = My.Settings.AutoRunTime
        End If

        If My.Settings.AutoRunInterval = Nothing Then
            autoruninterval_btn.Value = 24
        Else
            autoruninterval_btn.Value = Convert.ToInt16(My.Settings.AutoRunInterval)
        End If

        If My.Settings.SyncCustomers = Nothing Then
            chk_customers.Checked = False
        Else
            chk_customers.Checked = Convert.ToBoolean(My.Settings.SyncCustomers)
        End If

        If My.Settings.SyncLaborItems = Nothing Then
            chk_laboritems.Checked = False
        Else
            chk_laboritems.Checked = Convert.ToBoolean(My.Settings.SyncLaborItems)
        End If

        If My.Settings.SyncEmployees = Nothing Then
            chk_employees.Checked = False
        Else
            chk_employees.Checked = Convert.ToBoolean(My.Settings.SyncEmployees)
        End If

        If My.Settings.SyncConsultants = Nothing Then
            chk_consultans.Checked = False
        Else
            chk_consultans.Checked = Convert.ToBoolean(My.Settings.SyncConsultants)
        End If

        If My.Settings.SyncAccounts = Nothing Then
            chk_accounts.Checked = False
        Else
            chk_accounts.Checked = Convert.ToBoolean(My.Settings.SyncAccounts)
        End If

        If My.Settings.SyncTimeEntries = Nothing Then
            chk_timeentries.Checked = False
        Else
            chk_timeentries.Checked = Convert.ToBoolean(My.Settings.SyncTimeEntries)
        End If

        If My.Settings.SyncExpenseEntries = Nothing Then
            chk_expenseentries.Checked = False
        Else
            chk_expenseentries.Checked = Convert.ToBoolean(My.Settings.SyncExpenseEntries)
        End If

        If My.Settings.SyncElbVendor = Nothing Then
            chk_syncElgibleVendor.Checked = False
        Else
            chk_syncElgibleVendor.Checked = Convert.ToBoolean(My.Settings.SyncElbVendor)
        End If

        If My.Settings.DebugMode = Nothing Then
            chk_debugMode.Checked = True
        Else
            chk_debugMode.Checked = Convert.ToBoolean(My.Settings.DebugMode)
        End If

        If My.Settings.SyncJobOrItem = Nothing Then
            chk_jobsitems.Checked = False
        Else
            chk_jobsitems.Checked = Convert.ToBoolean(My.Settings.SyncJobOrItem)
        End If

        '-----Time trasfer settings

        If My.Settings.JobHierarchy = "" Then
            rbtJobitems.Checked = True
        Else
            If My.Settings.JobHierarchy = 0 Then
                rbtJobitems.Checked = True
            ElseIf My.Settings.JobHierarchy = 1 Then
                rbJob.Checked = True
            ElseIf My.Settings.JobHierarchy = 2 Then
                rbItem.Checked = True
            End If
        End If

        '----------Job, Item Settings (Project, task) data----
        If My.Settings.JobORItemHierarchy = "" Then
            job_subJobs_btn.Checked = True
        Else
            If My.Settings.JobORItemHierarchy = 0 Then
                job_subJobs_btn.Checked = True
            End If
            If My.Settings.JobORItemHierarchy = 1 Then
                item_subItems_btn.Checked = True
            End If
        End If

        '--------   cbClass 
        If My.Settings.QBClass = "" Then
            cbClass.SelectedIndex = 0
        Else
            cbClass.SelectedIndex = My.Settings.QBClass
        End If

        '-------- cbPayrollItem 
        If My.Settings.QBPayrollItem = "" Then
            cbPayrollItem.SelectedIndex = 0
        Else
            cbPayrollItem.SelectedIndex = My.Settings.QBPayrollItem
        End If

        If My.Settings.TransferToPayroll = "" Then
            chkPayrollTimesheet.Checked = False
        Else
            chkPayrollTimesheet.Checked = My.Settings.TransferToPayroll
        End If
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click, chk_customers.BackColorChanged
        Dim NextRunDateTime As Date = Convert.ToDateTime(dtp_autoruntime.Text)
        Dim result As Integer = DateTime.Compare(Convert.ToDateTime(NextRunDateTime), DateTime.Now)

        If result <= 0 Then
            NextRunDateTime = NextRunDateTime.AddHours(24)
        End If

        My.Settings.AutoRunTime = NextRunDateTime
        My.Settings.AutoRunInterval = autoruninterval_btn.Value.ToString()

        My.Settings.SyncCustomers = chk_customers.Checked
        My.Settings.SyncEmployees = chk_employees.Checked
        My.Settings.SyncConsultants = chk_consultans.Checked
        My.Settings.SyncJobs_Items = chk_jobsitems.Checked
        My.Settings.SyncAccounts = chk_accounts.Checked
        My.Settings.SyncTimeEntries = chk_timeentries.Checked
        My.Settings.SyncExpenseEntries = chk_expenseentries.Checked
        My.Settings.SyncLaborItems = chk_laboritems.Checked

        '------Save time transfer options
        If rbtJobitems.Checked Then
            which_rbj = 0
        ElseIf rbJob.Checked Then
            which_rbj = 1
        ElseIf rbItem.Checked Then
            which_rbj = 2
        End If
        ' saving all the parameters 
        My.Settings.JobHierarchy = which_rbj

        '------Save job or item data transfer options
        If job_subJobs_btn.Checked Then
            job_item_btn_option = 0
        ElseIf item_subItems_btn.Checked Then
            job_item_btn_option = 1
        End If

        My.Settings.JobORItemHierarchy = job_item_btn_option

        My.Settings.QBClass = cbClass.SelectedIndex
        My.Settings.QBPayrollItem = cbPayrollItem.SelectedIndex
        My.Settings.TransferToPayroll = chkPayrollTimesheet.Checked

        My.Settings.DebugMode = chk_debugMode.Checked

        ' Only show StatusWindow when in Debug Mode
        MAIN.SplitContainer2.Panel2Collapsed = Not Convert.ToBoolean(My.Settings.DebugMode)

        My.Settings.SyncElbVendor = chk_syncElgibleVendor.Checked
        My.Settings.SyncJobOrItem = chk_jobsitems.Checked

        My.Settings.Save()
        Me.DialogResult = DialogResult.OK
        Me.Close()
    End Sub

    '---- Time Transfer Controls Options
    Private Sub rbJob_CheckedChanged(sender As Object, e As EventArgs)
        which_rbj = 1
    End Sub

    Private Sub rbItem_CheckedChanged(sender As Object, e As EventArgs)
        which_rbj = 2
    End Sub

    Private Sub rbtJobitems_CheckedChanged(sender As Object, e As EventArgs)
        which_rbj = 0
    End Sub

    Private Sub chkPayrollTimesheet_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkPayrollTimesheet.CheckedChanged
        cbClass.Enabled = chkPayrollTimesheet.Checked
        cbPayrollItem.Enabled = chkPayrollTimesheet.Checked
    End Sub

    Private Sub CKB_syncElgibleVendor_CheckedChanged(sender As Object, e As EventArgs) Handles chk_syncElgibleVendor.CheckedChanged
        If chk_syncElgibleVendor.Checked = True Then
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
    End Sub

    Private Sub Label4_Click(sender As Object, e As EventArgs) Handles Label4.Click

    End Sub

    Private Sub chk_debugMode_CheckedChanged(sender As Object, e As EventArgs) Handles chk_debugMode.CheckedChanged

    End Sub
End Class