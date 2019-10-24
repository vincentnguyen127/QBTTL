<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class AppSettings
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.Tab_HoursOptions = New System.Windows.Forms.TabPage()
        Me.chkPayrollTimesheet = New System.Windows.Forms.CheckBox()
        Me.cbClass = New System.Windows.Forms.ComboBox()
        Me.lblPayrollItem = New System.Windows.Forms.Label()
        Me.lblClass = New System.Windows.Forms.Label()
        Me.cbPayrollItem = New System.Windows.Forms.ComboBox()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.rbItem = New System.Windows.Forms.RadioButton()
        Me.rbtJobitems = New System.Windows.Forms.RadioButton()
        Me.rbJob = New System.Windows.Forms.RadioButton()
        Me.Tab_SyncOptions = New System.Windows.Forms.TabPage()
        Me.Job_or_Item_Selection = New System.Windows.Forms.GroupBox()
        Me.item_subItems_btn = New System.Windows.Forms.RadioButton()
        Me.job_subJobs_btn = New System.Windows.Forms.RadioButton()
        Me.chk_debugMode = New System.Windows.Forms.CheckBox()
        Me.chk_syncElgibleVendor = New System.Windows.Forms.CheckBox()
        Me.Tab_AutoSync = New System.Windows.Forms.TabPage()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.chk_expenseentries = New System.Windows.Forms.CheckBox()
        Me.chk_timeentries = New System.Windows.Forms.CheckBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.chk_laboritems = New System.Windows.Forms.CheckBox()
        Me.chk_accounts = New System.Windows.Forms.CheckBox()
        Me.chk_jobsitems = New System.Windows.Forms.CheckBox()
        Me.chk_consultans = New System.Windows.Forms.CheckBox()
        Me.chk_employees = New System.Windows.Forms.CheckBox()
        Me.chk_customers = New System.Windows.Forms.CheckBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.autoruninterval_btn = New System.Windows.Forms.NumericUpDown()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.dtp_autoruntime = New System.Windows.Forms.DateTimePicker()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.Tab_Email = New System.Windows.Forms.TabPage()
        Me.ShowEmailPasswordCheckBox = New System.Windows.Forms.CheckBox()
        Me.SSLCheckBox = New System.Windows.Forms.CheckBox()
        Me.SSLLabel = New System.Windows.Forms.Label()
        Me.PortTextBox = New System.Windows.Forms.TextBox()
        Me.PortLabel = New System.Windows.Forms.Label()
        Me.HostTextBox = New System.Windows.Forms.TextBox()
        Me.HostLabel = New System.Windows.Forms.Label()
        Me.ToLabel = New System.Windows.Forms.Label()
        Me.PasswordLabel = New System.Windows.Forms.Label()
        Me.FromLabel = New System.Windows.Forms.Label()
        Me.ToEmailTextBox = New System.Windows.Forms.TextBox()
        Me.EmailPasswordTextBox = New System.Windows.Forms.TextBox()
        Me.FromEmailTextBox = New System.Windows.Forms.TextBox()
        Me.Tab_Email_text = New System.Windows.Forms.TabPage()
        Me.UncompletedMessageTextBox = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.UnapprovedMessageTextBox = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.UnsubmittedMessageTextBox = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Tab_HoursOptions.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.Tab_SyncOptions.SuspendLayout()
        Me.Job_or_Item_Selection.SuspendLayout()
        Me.Tab_AutoSync.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.autoruninterval_btn, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabControl1.SuspendLayout()
        Me.Tab_Email.SuspendLayout()
        Me.Tab_Email_text.SuspendLayout()
        Me.SuspendLayout()
        '
        'Button1
        '
        Me.Button1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Button1.Location = New System.Drawing.Point(398, 239)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 34
        Me.Button1.Text = "Cancel"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'btnSave
        '
        Me.btnSave.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnSave.Location = New System.Drawing.Point(307, 239)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(75, 23)
        Me.btnSave.TabIndex = 33
        Me.btnSave.Text = "Save"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'Tab_HoursOptions
        '
        Me.Tab_HoursOptions.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Tab_HoursOptions.Controls.Add(Me.chkPayrollTimesheet)
        Me.Tab_HoursOptions.Controls.Add(Me.cbClass)
        Me.Tab_HoursOptions.Controls.Add(Me.lblPayrollItem)
        Me.Tab_HoursOptions.Controls.Add(Me.lblClass)
        Me.Tab_HoursOptions.Controls.Add(Me.cbPayrollItem)
        Me.Tab_HoursOptions.Controls.Add(Me.GroupBox3)
        Me.Tab_HoursOptions.Location = New System.Drawing.Point(4, 22)
        Me.Tab_HoursOptions.Name = "Tab_HoursOptions"
        Me.Tab_HoursOptions.Padding = New System.Windows.Forms.Padding(3)
        Me.Tab_HoursOptions.Size = New System.Drawing.Size(453, 198)
        Me.Tab_HoursOptions.TabIndex = 2
        Me.Tab_HoursOptions.Text = "Hours"
        '
        'chkPayrollTimesheet
        '
        Me.chkPayrollTimesheet.AutoSize = True
        Me.chkPayrollTimesheet.Location = New System.Drawing.Point(24, 81)
        Me.chkPayrollTimesheet.Name = "chkPayrollTimesheet"
        Me.chkPayrollTimesheet.Size = New System.Drawing.Size(219, 17)
        Me.chkPayrollTimesheet.TabIndex = 48
        Me.chkPayrollTimesheet.Text = "Transfer time entries to payroll timesheets"
        Me.chkPayrollTimesheet.UseVisualStyleBackColor = True
        '
        'cbClass
        '
        Me.cbClass.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbClass.FormattingEnabled = True
        Me.cbClass.Items.AddRange(New Object() {"<None>", "CostCenter", "Department", "EmployeeType", "Milestone", "WorkType"})
        Me.cbClass.Location = New System.Drawing.Point(88, 104)
        Me.cbClass.Name = "cbClass"
        Me.cbClass.Size = New System.Drawing.Size(219, 21)
        Me.cbClass.TabIndex = 51
        '
        'lblPayrollItem
        '
        Me.lblPayrollItem.AutoSize = True
        Me.lblPayrollItem.Location = New System.Drawing.Point(21, 134)
        Me.lblPayrollItem.Name = "lblPayrollItem"
        Me.lblPayrollItem.Size = New System.Drawing.Size(64, 13)
        Me.lblPayrollItem.TabIndex = 50
        Me.lblPayrollItem.Text = "Payroll Item:"
        '
        'lblClass
        '
        Me.lblClass.AutoSize = True
        Me.lblClass.Location = New System.Drawing.Point(50, 107)
        Me.lblClass.Name = "lblClass"
        Me.lblClass.Size = New System.Drawing.Size(35, 13)
        Me.lblClass.TabIndex = 49
        Me.lblClass.Text = "Class:"
        '
        'cbPayrollItem
        '
        Me.cbPayrollItem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbPayrollItem.Enabled = False
        Me.cbPayrollItem.FormattingEnabled = True
        Me.cbPayrollItem.Items.AddRange(New Object() {"<None>", "CostCenter", "Department", "EmployeeType", "Milestone", "WorkType"})
        Me.cbPayrollItem.Location = New System.Drawing.Point(88, 130)
        Me.cbPayrollItem.Name = "cbPayrollItem"
        Me.cbPayrollItem.Size = New System.Drawing.Size(219, 21)
        Me.cbPayrollItem.TabIndex = 47
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.rbItem)
        Me.GroupBox3.Controls.Add(Me.rbtJobitems)
        Me.GroupBox3.Controls.Add(Me.rbJob)
        Me.GroupBox3.Location = New System.Drawing.Point(18, 17)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(409, 46)
        Me.GroupBox3.TabIndex = 46
        Me.GroupBox3.TabStop = False
        '
        'rbItem
        '
        Me.rbItem.AutoSize = True
        Me.rbItem.Location = New System.Drawing.Point(287, 19)
        Me.rbItem.Name = "rbItem"
        Me.rbItem.Size = New System.Drawing.Size(102, 17)
        Me.rbItem.TabIndex = 44
        Me.rbItem.Text = "Items/Sub Items"
        Me.rbItem.UseVisualStyleBackColor = True
        '
        'rbtJobitems
        '
        Me.rbtJobitems.AutoSize = True
        Me.rbtJobitems.Location = New System.Drawing.Point(19, 19)
        Me.rbtJobitems.Name = "rbtJobitems"
        Me.rbtJobitems.Size = New System.Drawing.Size(77, 17)
        Me.rbtJobitems.TabIndex = 42
        Me.rbtJobitems.Text = "Jobs/Items"
        Me.rbtJobitems.UseVisualStyleBackColor = True
        '
        'rbJob
        '
        Me.rbJob.AutoSize = True
        Me.rbJob.Checked = True
        Me.rbJob.Location = New System.Drawing.Point(140, 19)
        Me.rbJob.Name = "rbJob"
        Me.rbJob.Size = New System.Drawing.Size(96, 17)
        Me.rbJob.TabIndex = 43
        Me.rbJob.TabStop = True
        Me.rbJob.Text = "Jobs/Sub Jobs"
        Me.rbJob.UseVisualStyleBackColor = True
        '
        'Tab_SyncOptions
        '
        Me.Tab_SyncOptions.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Tab_SyncOptions.Controls.Add(Me.Job_or_Item_Selection)
        Me.Tab_SyncOptions.Controls.Add(Me.chk_debugMode)
        Me.Tab_SyncOptions.Controls.Add(Me.chk_syncElgibleVendor)
        Me.Tab_SyncOptions.Location = New System.Drawing.Point(4, 22)
        Me.Tab_SyncOptions.Name = "Tab_SyncOptions"
        Me.Tab_SyncOptions.Padding = New System.Windows.Forms.Padding(3)
        Me.Tab_SyncOptions.Size = New System.Drawing.Size(453, 198)
        Me.Tab_SyncOptions.TabIndex = 1
        Me.Tab_SyncOptions.Text = "Sync Options"
        '
        'Job_or_Item_Selection
        '
        Me.Job_or_Item_Selection.Controls.Add(Me.item_subItems_btn)
        Me.Job_or_Item_Selection.Controls.Add(Me.job_subJobs_btn)
        Me.Job_or_Item_Selection.Location = New System.Drawing.Point(22, 104)
        Me.Job_or_Item_Selection.Name = "Job_or_Item_Selection"
        Me.Job_or_Item_Selection.Size = New System.Drawing.Size(409, 46)
        Me.Job_or_Item_Selection.TabIndex = 48
        Me.Job_or_Item_Selection.TabStop = False
        '
        'item_subItems_btn
        '
        Me.item_subItems_btn.AutoSize = True
        Me.item_subItems_btn.Location = New System.Drawing.Point(167, 18)
        Me.item_subItems_btn.Name = "item_subItems_btn"
        Me.item_subItems_btn.Size = New System.Drawing.Size(102, 17)
        Me.item_subItems_btn.TabIndex = 44
        Me.item_subItems_btn.Text = "Items/Sub Items"
        Me.item_subItems_btn.UseVisualStyleBackColor = True
        '
        'job_subJobs_btn
        '
        Me.job_subJobs_btn.AutoSize = True
        Me.job_subJobs_btn.Checked = True
        Me.job_subJobs_btn.Location = New System.Drawing.Point(27, 18)
        Me.job_subJobs_btn.Name = "job_subJobs_btn"
        Me.job_subJobs_btn.Size = New System.Drawing.Size(96, 17)
        Me.job_subJobs_btn.TabIndex = 43
        Me.job_subJobs_btn.TabStop = True
        Me.job_subJobs_btn.Text = "Jobs/Sub Jobs"
        Me.job_subJobs_btn.UseVisualStyleBackColor = True
        '
        'chk_debugMode
        '
        Me.chk_debugMode.AutoSize = True
        Me.chk_debugMode.Checked = True
        Me.chk_debugMode.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chk_debugMode.Location = New System.Drawing.Point(24, 27)
        Me.chk_debugMode.Name = "chk_debugMode"
        Me.chk_debugMode.Size = New System.Drawing.Size(88, 17)
        Me.chk_debugMode.TabIndex = 1
        Me.chk_debugMode.Text = "Debug Mode"
        Me.chk_debugMode.UseVisualStyleBackColor = True
        '
        'chk_syncElgibleVendor
        '
        Me.chk_syncElgibleVendor.AutoSize = True
        Me.chk_syncElgibleVendor.Location = New System.Drawing.Point(24, 69)
        Me.chk_syncElgibleVendor.Name = "chk_syncElgibleVendor"
        Me.chk_syncElgibleVendor.Size = New System.Drawing.Size(226, 17)
        Me.chk_syncElgibleVendor.TabIndex = 0
        Me.chk_syncElgibleVendor.Text = "Synchronize vendors eligible for 1099 Only"
        Me.chk_syncElgibleVendor.UseVisualStyleBackColor = True
        '
        'Tab_AutoSync
        '
        Me.Tab_AutoSync.Controls.Add(Me.GroupBox2)
        Me.Tab_AutoSync.Controls.Add(Me.GroupBox1)
        Me.Tab_AutoSync.Controls.Add(Me.Label2)
        Me.Tab_AutoSync.Controls.Add(Me.autoruninterval_btn)
        Me.Tab_AutoSync.Controls.Add(Me.Label1)
        Me.Tab_AutoSync.Controls.Add(Me.dtp_autoruntime)
        Me.Tab_AutoSync.Location = New System.Drawing.Point(4, 22)
        Me.Tab_AutoSync.Name = "Tab_AutoSync"
        Me.Tab_AutoSync.Padding = New System.Windows.Forms.Padding(3)
        Me.Tab_AutoSync.Size = New System.Drawing.Size(453, 198)
        Me.Tab_AutoSync.TabIndex = 0
        Me.Tab_AutoSync.Text = "Auto Sync"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.chk_expenseentries)
        Me.GroupBox2.Controls.Add(Me.chk_timeentries)
        Me.GroupBox2.Location = New System.Drawing.Point(23, 132)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(400, 49)
        Me.GroupBox2.TabIndex = 7
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "TimeLive to Quickbooks Sync"
        '
        'chk_expenseentries
        '
        Me.chk_expenseentries.AutoSize = True
        Me.chk_expenseentries.Location = New System.Drawing.Point(105, 19)
        Me.chk_expenseentries.Name = "chk_expenseentries"
        Me.chk_expenseentries.Size = New System.Drawing.Size(102, 17)
        Me.chk_expenseentries.TabIndex = 1
        Me.chk_expenseentries.Text = "Expense Entries"
        Me.chk_expenseentries.UseVisualStyleBackColor = True
        '
        'chk_timeentries
        '
        Me.chk_timeentries.AutoSize = True
        Me.chk_timeentries.Location = New System.Drawing.Point(11, 19)
        Me.chk_timeentries.Name = "chk_timeentries"
        Me.chk_timeentries.Size = New System.Drawing.Size(84, 17)
        Me.chk_timeentries.TabIndex = 0
        Me.chk_timeentries.Text = "Time Entries"
        Me.chk_timeentries.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.chk_laboritems)
        Me.GroupBox1.Controls.Add(Me.chk_accounts)
        Me.GroupBox1.Controls.Add(Me.chk_jobsitems)
        Me.GroupBox1.Controls.Add(Me.chk_consultans)
        Me.GroupBox1.Controls.Add(Me.chk_employees)
        Me.GroupBox1.Controls.Add(Me.chk_customers)
        Me.GroupBox1.Location = New System.Drawing.Point(23, 61)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(400, 65)
        Me.GroupBox1.TabIndex = 6
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Quickbooks to TimeLive Sync"
        '
        'chk_laboritems
        '
        Me.chk_laboritems.AutoSize = True
        Me.chk_laboritems.Location = New System.Drawing.Point(105, 42)
        Me.chk_laboritems.Name = "chk_laboritems"
        Me.chk_laboritems.Size = New System.Drawing.Size(81, 17)
        Me.chk_laboritems.TabIndex = 5
        Me.chk_laboritems.Text = "Labor Items"
        Me.chk_laboritems.UseVisualStyleBackColor = True
        '
        'chk_accounts
        '
        Me.chk_accounts.AutoSize = True
        Me.chk_accounts.Location = New System.Drawing.Point(11, 42)
        Me.chk_accounts.Name = "chk_accounts"
        Me.chk_accounts.Size = New System.Drawing.Size(71, 17)
        Me.chk_accounts.TabIndex = 4
        Me.chk_accounts.Text = "Accounts"
        Me.chk_accounts.UseVisualStyleBackColor = True
        '
        'chk_jobsitems
        '
        Me.chk_jobsitems.AutoSize = True
        Me.chk_jobsitems.Location = New System.Drawing.Point(300, 19)
        Me.chk_jobsitems.Name = "chk_jobsitems"
        Me.chk_jobsitems.Size = New System.Drawing.Size(78, 17)
        Me.chk_jobsitems.TabIndex = 3
        Me.chk_jobsitems.Text = "Jobs/Items"
        Me.chk_jobsitems.UseVisualStyleBackColor = True
        '
        'chk_consultans
        '
        Me.chk_consultans.AllowDrop = True
        Me.chk_consultans.AutoSize = True
        Me.chk_consultans.Location = New System.Drawing.Point(201, 19)
        Me.chk_consultans.Name = "chk_consultans"
        Me.chk_consultans.Size = New System.Drawing.Size(81, 17)
        Me.chk_consultans.TabIndex = 2
        Me.chk_consultans.Text = "Consultants"
        Me.chk_consultans.UseVisualStyleBackColor = True
        '
        'chk_employees
        '
        Me.chk_employees.AutoSize = True
        Me.chk_employees.Location = New System.Drawing.Point(105, 19)
        Me.chk_employees.Name = "chk_employees"
        Me.chk_employees.Size = New System.Drawing.Size(77, 17)
        Me.chk_employees.TabIndex = 1
        Me.chk_employees.Text = "Employees"
        Me.chk_employees.UseVisualStyleBackColor = True
        '
        'chk_customers
        '
        Me.chk_customers.AutoSize = True
        Me.chk_customers.Location = New System.Drawing.Point(11, 19)
        Me.chk_customers.Name = "chk_customers"
        Me.chk_customers.Size = New System.Drawing.Size(75, 17)
        Me.chk_customers.TabIndex = 0
        Me.chk_customers.Text = "Customers"
        Me.chk_customers.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(243, 30)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(87, 13)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "Auto run interval:"
        '
        'autoruninterval_btn
        '
        Me.autoruninterval_btn.AllowDrop = True
        Me.autoruninterval_btn.Location = New System.Drawing.Point(336, 23)
        Me.autoruninterval_btn.Maximum = New Decimal(New Integer() {24, 0, 0, 0})
        Me.autoruninterval_btn.Name = "autoruninterval_btn"
        Me.autoruninterval_btn.Size = New System.Drawing.Size(43, 20)
        Me.autoruninterval_btn.TabIndex = 4
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(20, 30)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(95, 13)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Auto run time start:"
        '
        'dtp_autoruntime
        '
        Me.dtp_autoruntime.Format = System.Windows.Forms.DateTimePickerFormat.Time
        Me.dtp_autoruntime.Location = New System.Drawing.Point(121, 24)
        Me.dtp_autoruntime.Name = "dtp_autoruntime"
        Me.dtp_autoruntime.Size = New System.Drawing.Size(87, 20)
        Me.dtp_autoruntime.TabIndex = 2
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.Tab_AutoSync)
        Me.TabControl1.Controls.Add(Me.Tab_SyncOptions)
        Me.TabControl1.Controls.Add(Me.Tab_HoursOptions)
        Me.TabControl1.Controls.Add(Me.Tab_Email)
        Me.TabControl1.Controls.Add(Me.Tab_Email_text)
        Me.TabControl1.Location = New System.Drawing.Point(12, 12)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(461, 224)
        Me.TabControl1.TabIndex = 35
        '
        'Tab_Email
        '
        Me.Tab_Email.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Tab_Email.Controls.Add(Me.ShowEmailPasswordCheckBox)
        Me.Tab_Email.Controls.Add(Me.SSLCheckBox)
        Me.Tab_Email.Controls.Add(Me.SSLLabel)
        Me.Tab_Email.Controls.Add(Me.PortTextBox)
        Me.Tab_Email.Controls.Add(Me.PortLabel)
        Me.Tab_Email.Controls.Add(Me.HostTextBox)
        Me.Tab_Email.Controls.Add(Me.HostLabel)
        Me.Tab_Email.Controls.Add(Me.ToLabel)
        Me.Tab_Email.Controls.Add(Me.PasswordLabel)
        Me.Tab_Email.Controls.Add(Me.FromLabel)
        Me.Tab_Email.Controls.Add(Me.ToEmailTextBox)
        Me.Tab_Email.Controls.Add(Me.EmailPasswordTextBox)
        Me.Tab_Email.Controls.Add(Me.FromEmailTextBox)
        Me.Tab_Email.Location = New System.Drawing.Point(4, 22)
        Me.Tab_Email.Name = "Tab_Email"
        Me.Tab_Email.Padding = New System.Windows.Forms.Padding(3)
        Me.Tab_Email.Size = New System.Drawing.Size(453, 198)
        Me.Tab_Email.TabIndex = 4
        Me.Tab_Email.Text = "eMail Server"
        '
        'ShowEmailPasswordCheckBox
        '
        Me.ShowEmailPasswordCheckBox.AutoSize = True
        Me.ShowEmailPasswordCheckBox.Location = New System.Drawing.Point(424, 22)
        Me.ShowEmailPasswordCheckBox.Name = "ShowEmailPasswordCheckBox"
        Me.ShowEmailPasswordCheckBox.Size = New System.Drawing.Size(15, 14)
        Me.ShowEmailPasswordCheckBox.TabIndex = 12
        Me.ShowEmailPasswordCheckBox.UseVisualStyleBackColor = True
        '
        'SSLCheckBox
        '
        Me.SSLCheckBox.AutoSize = True
        Me.SSLCheckBox.Location = New System.Drawing.Point(305, 92)
        Me.SSLCheckBox.Name = "SSLCheckBox"
        Me.SSLCheckBox.Size = New System.Drawing.Size(15, 14)
        Me.SSLCheckBox.TabIndex = 11
        Me.SSLCheckBox.UseVisualStyleBackColor = True
        '
        'SSLLabel
        '
        Me.SSLLabel.AutoSize = True
        Me.SSLLabel.Location = New System.Drawing.Point(216, 92)
        Me.SSLLabel.Name = "SSLLabel"
        Me.SSLLabel.Size = New System.Drawing.Size(83, 13)
        Me.SSLLabel.TabIndex = 10
        Me.SSLLabel.Text = "SSL Encryption:"
        '
        'PortTextBox
        '
        Me.PortTextBox.Location = New System.Drawing.Point(278, 55)
        Me.PortTextBox.Name = "PortTextBox"
        Me.PortTextBox.Size = New System.Drawing.Size(140, 20)
        Me.PortTextBox.TabIndex = 9
        '
        'PortLabel
        '
        Me.PortLabel.AutoSize = True
        Me.PortLabel.Location = New System.Drawing.Point(216, 57)
        Me.PortLabel.Name = "PortLabel"
        Me.PortLabel.Size = New System.Drawing.Size(29, 13)
        Me.PortLabel.TabIndex = 8
        Me.PortLabel.Text = "Port:"
        '
        'HostTextBox
        '
        Me.HostTextBox.Location = New System.Drawing.Point(68, 90)
        Me.HostTextBox.Name = "HostTextBox"
        Me.HostTextBox.Size = New System.Drawing.Size(140, 20)
        Me.HostTextBox.TabIndex = 7
        '
        'HostLabel
        '
        Me.HostLabel.AutoSize = True
        Me.HostLabel.Location = New System.Drawing.Point(6, 92)
        Me.HostLabel.Name = "HostLabel"
        Me.HostLabel.Size = New System.Drawing.Size(32, 13)
        Me.HostLabel.TabIndex = 6
        Me.HostLabel.Text = "Host:"
        '
        'ToLabel
        '
        Me.ToLabel.AutoSize = True
        Me.ToLabel.Location = New System.Drawing.Point(4, 57)
        Me.ToLabel.Name = "ToLabel"
        Me.ToLabel.Size = New System.Drawing.Size(47, 13)
        Me.ToLabel.TabIndex = 5
        Me.ToLabel.Text = "Send to:"
        '
        'PasswordLabel
        '
        Me.PasswordLabel.AutoSize = True
        Me.PasswordLabel.Location = New System.Drawing.Point(216, 22)
        Me.PasswordLabel.Name = "PasswordLabel"
        Me.PasswordLabel.Size = New System.Drawing.Size(56, 13)
        Me.PasswordLabel.TabIndex = 4
        Me.PasswordLabel.Text = "Password:"
        '
        'FromLabel
        '
        Me.FromLabel.AutoSize = True
        Me.FromLabel.Location = New System.Drawing.Point(4, 22)
        Me.FromLabel.Name = "FromLabel"
        Me.FromLabel.Size = New System.Drawing.Size(62, 13)
        Me.FromLabel.TabIndex = 3
        Me.FromLabel.Text = "From Gmail:"
        '
        'ToEmailTextBox
        '
        Me.ToEmailTextBox.Location = New System.Drawing.Point(68, 55)
        Me.ToEmailTextBox.Name = "ToEmailTextBox"
        Me.ToEmailTextBox.Size = New System.Drawing.Size(140, 20)
        Me.ToEmailTextBox.TabIndex = 2
        '
        'EmailPasswordTextBox
        '
        Me.EmailPasswordTextBox.Location = New System.Drawing.Point(278, 20)
        Me.EmailPasswordTextBox.Name = "EmailPasswordTextBox"
        Me.EmailPasswordTextBox.Size = New System.Drawing.Size(140, 20)
        Me.EmailPasswordTextBox.TabIndex = 1
        Me.EmailPasswordTextBox.UseSystemPasswordChar = True
        '
        'FromEmailTextBox
        '
        Me.FromEmailTextBox.Location = New System.Drawing.Point(68, 20)
        Me.FromEmailTextBox.Name = "FromEmailTextBox"
        Me.FromEmailTextBox.Size = New System.Drawing.Size(140, 20)
        Me.FromEmailTextBox.TabIndex = 0
        '
        'Tab_Email_text
        '
        Me.Tab_Email_text.BackColor = System.Drawing.Color.WhiteSmoke
        Me.Tab_Email_text.Controls.Add(Me.UncompletedMessageTextBox)
        Me.Tab_Email_text.Controls.Add(Me.Label5)
        Me.Tab_Email_text.Controls.Add(Me.UnapprovedMessageTextBox)
        Me.Tab_Email_text.Controls.Add(Me.Label4)
        Me.Tab_Email_text.Controls.Add(Me.UnsubmittedMessageTextBox)
        Me.Tab_Email_text.Controls.Add(Me.Label3)
        Me.Tab_Email_text.Location = New System.Drawing.Point(4, 22)
        Me.Tab_Email_text.Name = "Tab_Email_text"
        Me.Tab_Email_text.Padding = New System.Windows.Forms.Padding(3)
        Me.Tab_Email_text.Size = New System.Drawing.Size(453, 198)
        Me.Tab_Email_text.TabIndex = 5
        Me.Tab_Email_text.Text = "eMail Text"
        '
        'UncompletedMessageTextBox
        '
        Me.UncompletedMessageTextBox.Location = New System.Drawing.Point(77, 23)
        Me.UncompletedMessageTextBox.Multiline = True
        Me.UncompletedMessageTextBox.Name = "UncompletedMessageTextBox"
        Me.UncompletedMessageTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.UncompletedMessageTextBox.Size = New System.Drawing.Size(140, 67)
        Me.UncompletedMessageTextBox.TabIndex = 22
        '
        'Label5
        '
        Me.Label5.Location = New System.Drawing.Point(3, 23)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(78, 64)
        Me.Label5.TabIndex = 21
        Me.Label5.Text = "Uncompleted Time Card Message:"
        '
        'UnapprovedMessageTextBox
        '
        Me.UnapprovedMessageTextBox.Location = New System.Drawing.Point(307, 102)
        Me.UnapprovedMessageTextBox.Multiline = True
        Me.UnapprovedMessageTextBox.Name = "UnapprovedMessageTextBox"
        Me.UnapprovedMessageTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.UnapprovedMessageTextBox.Size = New System.Drawing.Size(140, 67)
        Me.UnapprovedMessageTextBox.TabIndex = 20
        '
        'Label4
        '
        Me.Label4.Location = New System.Drawing.Point(237, 105)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(70, 50)
        Me.Label4.TabIndex = 19
        Me.Label4.Text = "Unapproved Time Card Message::"
        '
        'UnsubmittedMessageTextBox
        '
        Me.UnsubmittedMessageTextBox.Location = New System.Drawing.Point(77, 105)
        Me.UnsubmittedMessageTextBox.Multiline = True
        Me.UnsubmittedMessageTextBox.Name = "UnsubmittedMessageTextBox"
        Me.UnsubmittedMessageTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.UnsubmittedMessageTextBox.Size = New System.Drawing.Size(140, 67)
        Me.UnsubmittedMessageTextBox.TabIndex = 18
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(3, 105)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(68, 64)
        Me.Label3.TabIndex = 17
        Me.Label3.Text = "Unsubmitted Time Card Message:"
        '
        'AppSettings
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(485, 274)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.btnSave)
        Me.Name = "AppSettings"
        Me.Text = "AppSettings"
        Me.Tab_HoursOptions.ResumeLayout(False)
        Me.Tab_HoursOptions.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.Tab_SyncOptions.ResumeLayout(False)
        Me.Tab_SyncOptions.PerformLayout()
        Me.Job_or_Item_Selection.ResumeLayout(False)
        Me.Job_or_Item_Selection.PerformLayout()
        Me.Tab_AutoSync.ResumeLayout(False)
        Me.Tab_AutoSync.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.autoruninterval_btn, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabControl1.ResumeLayout(False)
        Me.Tab_Email.ResumeLayout(False)
        Me.Tab_Email.PerformLayout()
        Me.Tab_Email_text.ResumeLayout(False)
        Me.Tab_Email_text.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Button1 As Button
    Friend WithEvents btnSave As Button
    Friend WithEvents Tab_HoursOptions As TabPage
    Friend WithEvents chkPayrollTimesheet As CheckBox
    Friend WithEvents cbClass As ComboBox
    Friend WithEvents lblPayrollItem As Label
    Friend WithEvents lblClass As Label
    Friend WithEvents cbPayrollItem As ComboBox
    Friend WithEvents rbtJobitems As RadioButton
    Friend WithEvents GroupBox3 As GroupBox
    Friend WithEvents rbItem As RadioButton
    Friend WithEvents rbJob As RadioButton
    Friend WithEvents Tab_SyncOptions As TabPage
    Friend WithEvents chk_debugMode As CheckBox
    Friend WithEvents chk_syncElgibleVendor As CheckBox
    Friend WithEvents Tab_AutoSync As TabPage
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents chk_expenseentries As CheckBox
    Friend WithEvents chk_timeentries As CheckBox
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents chk_laboritems As CheckBox
    Friend WithEvents chk_accounts As CheckBox
    Friend WithEvents chk_jobsitems As CheckBox
    Friend WithEvents chk_consultans As CheckBox
    Friend WithEvents chk_employees As CheckBox
    Friend WithEvents chk_customers As CheckBox
    Friend WithEvents Label2 As Label
    Friend WithEvents autoruninterval_btn As NumericUpDown
    Friend WithEvents Label1 As Label
    Friend WithEvents dtp_autoruntime As DateTimePicker
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents Tab_Email As TabPage
    Friend WithEvents ToEmailTextBox As TextBox
    Friend WithEvents EmailPasswordTextBox As TextBox
    Friend WithEvents FromEmailTextBox As TextBox
    Friend WithEvents PasswordLabel As Label
    Friend WithEvents FromLabel As Label
    Friend WithEvents ToLabel As Label
    Friend WithEvents HostLabel As Label
    Friend WithEvents HostTextBox As TextBox
    Friend WithEvents PortTextBox As TextBox
    Friend WithEvents PortLabel As Label
    Friend WithEvents SSLLabel As Label
    Friend WithEvents SSLCheckBox As CheckBox
    Friend WithEvents Job_or_Item_Selection As GroupBox
    Friend WithEvents item_subItems_btn As RadioButton
    Friend WithEvents job_subJobs_btn As RadioButton
    Friend WithEvents ShowEmailPasswordCheckBox As CheckBox
    Friend WithEvents Tab_Email_text As TabPage
    Friend WithEvents UnapprovedMessageTextBox As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents UnsubmittedMessageTextBox As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents UncompletedMessageTextBox As TextBox
    Friend WithEvents Label5 As Label
End Class
