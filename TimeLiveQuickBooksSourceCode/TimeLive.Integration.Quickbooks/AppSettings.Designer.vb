<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AppSettings
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
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
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.chk_syncElgibleVendor = New System.Windows.Forms.CheckBox()
        Me.Tab_Sync = New System.Windows.Forms.TabPage()
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
        Me.Tab_ItemsJobs = New System.Windows.Forms.TabPage()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.item_subItems_btn = New System.Windows.Forms.RadioButton()
        Me.job_subJobs_btn = New System.Windows.Forms.RadioButton()
        Me.Tab_DebugMode = New System.Windows.Forms.TabPage()
        Me.chk_debug = New System.Windows.Forms.CheckBox()
        Me.Tab_HoursOptions.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.Tab_Sync.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.autoruninterval_btn, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabControl1.SuspendLayout()
        Me.Tab_ItemsJobs.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.Tab_DebugMode.SuspendLayout()
        Me.SuspendLayout()
        '
        'Button1
        '
        Me.Button1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Button1.Location = New System.Drawing.Point(597, 368)
        Me.Button1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(112, 35)
        Me.Button1.TabIndex = 34
        Me.Button1.Text = "Cancel"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'btnSave
        '
        Me.btnSave.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnSave.Location = New System.Drawing.Point(460, 368)
        Me.btnSave.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(112, 35)
        Me.btnSave.TabIndex = 33
        Me.btnSave.Text = "Save"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'Tab_HoursOptions
        '
        Me.Tab_HoursOptions.Controls.Add(Me.chkPayrollTimesheet)
        Me.Tab_HoursOptions.Controls.Add(Me.cbClass)
        Me.Tab_HoursOptions.Controls.Add(Me.lblPayrollItem)
        Me.Tab_HoursOptions.Controls.Add(Me.lblClass)
        Me.Tab_HoursOptions.Controls.Add(Me.cbPayrollItem)
        Me.Tab_HoursOptions.Controls.Add(Me.GroupBox3)
        Me.Tab_HoursOptions.Location = New System.Drawing.Point(4, 29)
        Me.Tab_HoursOptions.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Tab_HoursOptions.Name = "Tab_HoursOptions"
        Me.Tab_HoursOptions.Padding = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Tab_HoursOptions.Size = New System.Drawing.Size(684, 312)
        Me.Tab_HoursOptions.TabIndex = 2
        Me.Tab_HoursOptions.Text = "Hours"
        Me.Tab_HoursOptions.UseVisualStyleBackColor = True
        '
        'chkPayrollTimesheet
        '
        Me.chkPayrollTimesheet.AutoSize = True
        Me.chkPayrollTimesheet.Location = New System.Drawing.Point(36, 125)
        Me.chkPayrollTimesheet.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.chkPayrollTimesheet.Name = "chkPayrollTimesheet"
        Me.chkPayrollTimesheet.Size = New System.Drawing.Size(329, 24)
        Me.chkPayrollTimesheet.TabIndex = 48
        Me.chkPayrollTimesheet.Text = "Transfer time entries to payroll timesheets"
        Me.chkPayrollTimesheet.UseVisualStyleBackColor = True
        '
        'cbClass
        '
        Me.cbClass.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbClass.FormattingEnabled = True
        Me.cbClass.Items.AddRange(New Object() {"<None>", "CostCenter", "Department", "EmployeeType", "Milestone", "WorkType"})
        Me.cbClass.Location = New System.Drawing.Point(132, 160)
        Me.cbClass.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cbClass.Name = "cbClass"
        Me.cbClass.Size = New System.Drawing.Size(326, 28)
        Me.cbClass.TabIndex = 51
        '
        'lblPayrollItem
        '
        Me.lblPayrollItem.AutoSize = True
        Me.lblPayrollItem.Location = New System.Drawing.Point(32, 206)
        Me.lblPayrollItem.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblPayrollItem.Name = "lblPayrollItem"
        Me.lblPayrollItem.Size = New System.Drawing.Size(95, 20)
        Me.lblPayrollItem.TabIndex = 50
        Me.lblPayrollItem.Text = "Payroll Item:"
        '
        'lblClass
        '
        Me.lblClass.AutoSize = True
        Me.lblClass.Location = New System.Drawing.Point(75, 165)
        Me.lblClass.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblClass.Name = "lblClass"
        Me.lblClass.Size = New System.Drawing.Size(52, 20)
        Me.lblClass.TabIndex = 49
        Me.lblClass.Text = "Class:"
        '
        'cbPayrollItem
        '
        Me.cbPayrollItem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbPayrollItem.Enabled = False
        Me.cbPayrollItem.FormattingEnabled = True
        Me.cbPayrollItem.Items.AddRange(New Object() {"<None>", "CostCenter", "Department", "EmployeeType", "Milestone", "WorkType"})
        Me.cbPayrollItem.Location = New System.Drawing.Point(132, 200)
        Me.cbPayrollItem.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.cbPayrollItem.Name = "cbPayrollItem"
        Me.cbPayrollItem.Size = New System.Drawing.Size(326, 28)
        Me.cbPayrollItem.TabIndex = 47
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.rbItem)
        Me.GroupBox3.Controls.Add(Me.rbtJobitems)
        Me.GroupBox3.Controls.Add(Me.rbJob)
        Me.GroupBox3.Location = New System.Drawing.Point(27, 26)
        Me.GroupBox3.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Padding = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.GroupBox3.Size = New System.Drawing.Size(614, 71)
        Me.GroupBox3.TabIndex = 46
        Me.GroupBox3.TabStop = False
        '
        'rbItem
        '
        Me.rbItem.AutoSize = True
        Me.rbItem.Location = New System.Drawing.Point(430, 29)
        Me.rbItem.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.rbItem.Name = "rbItem"
        Me.rbItem.Size = New System.Drawing.Size(151, 24)
        Me.rbItem.TabIndex = 44
        Me.rbItem.Text = "Items/Sub Items"
        Me.rbItem.UseVisualStyleBackColor = True
        '
        'rbtJobitems
        '
        Me.rbtJobitems.AutoSize = True
        Me.rbtJobitems.Location = New System.Drawing.Point(28, 29)
        Me.rbtJobitems.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.rbtJobitems.Name = "rbtJobitems"
        Me.rbtJobitems.Size = New System.Drawing.Size(112, 24)
        Me.rbtJobitems.TabIndex = 42
        Me.rbtJobitems.Text = "Jobs/Items"
        Me.rbtJobitems.UseVisualStyleBackColor = True
        '
        'rbJob
        '
        Me.rbJob.AutoSize = True
        Me.rbJob.Checked = True
        Me.rbJob.Location = New System.Drawing.Point(210, 29)
        Me.rbJob.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.rbJob.Name = "rbJob"
        Me.rbJob.Size = New System.Drawing.Size(139, 24)
        Me.rbJob.TabIndex = 43
        Me.rbJob.TabStop = True
        Me.rbJob.Text = "Jobs/Sub Jobs"
        Me.rbJob.UseVisualStyleBackColor = True
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.chk_syncElgibleVendor)
        Me.TabPage2.Location = New System.Drawing.Point(4, 29)
        Me.TabPage2.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.TabPage2.Size = New System.Drawing.Size(684, 312)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Vendor"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'chk_syncElgibleVendor
        '
        Me.chk_syncElgibleVendor.AutoSize = True
        Me.chk_syncElgibleVendor.Location = New System.Drawing.Point(36, 45)
        Me.chk_syncElgibleVendor.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.chk_syncElgibleVendor.Name = "chk_syncElgibleVendor"
        Me.chk_syncElgibleVendor.Size = New System.Drawing.Size(332, 24)
        Me.chk_syncElgibleVendor.TabIndex = 0
        Me.chk_syncElgibleVendor.Text = "Synchronize vendors eligible for 1099 Only"
        Me.chk_syncElgibleVendor.UseVisualStyleBackColor = True
        '
        'Tab_Sync
        '
        Me.Tab_Sync.Controls.Add(Me.GroupBox2)
        Me.Tab_Sync.Controls.Add(Me.GroupBox1)
        Me.Tab_Sync.Controls.Add(Me.Label2)
        Me.Tab_Sync.Controls.Add(Me.autoruninterval_btn)
        Me.Tab_Sync.Controls.Add(Me.Label1)
        Me.Tab_Sync.Controls.Add(Me.dtp_autoruntime)
        Me.Tab_Sync.Location = New System.Drawing.Point(4, 29)
        Me.Tab_Sync.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Tab_Sync.Name = "Tab_Sync"
        Me.Tab_Sync.Padding = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Tab_Sync.Size = New System.Drawing.Size(684, 312)
        Me.Tab_Sync.TabIndex = 0
        Me.Tab_Sync.Text = "Sync Options"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.chk_expenseentries)
        Me.GroupBox2.Controls.Add(Me.chk_timeentries)
        Me.GroupBox2.Location = New System.Drawing.Point(34, 203)
        Me.GroupBox2.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Padding = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.GroupBox2.Size = New System.Drawing.Size(600, 75)
        Me.GroupBox2.TabIndex = 7
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "TimeLive to Quickbooks Sync"
        '
        'chk_expenseentries
        '
        Me.chk_expenseentries.AutoSize = True
        Me.chk_expenseentries.Location = New System.Drawing.Point(158, 29)
        Me.chk_expenseentries.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.chk_expenseentries.Name = "chk_expenseentries"
        Me.chk_expenseentries.Size = New System.Drawing.Size(151, 24)
        Me.chk_expenseentries.TabIndex = 1
        Me.chk_expenseentries.Text = "Expense Entries"
        Me.chk_expenseentries.UseVisualStyleBackColor = True
        '
        'chk_timeentries
        '
        Me.chk_timeentries.AutoSize = True
        Me.chk_timeentries.Location = New System.Drawing.Point(16, 29)
        Me.chk_timeentries.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.chk_timeentries.Name = "chk_timeentries"
        Me.chk_timeentries.Size = New System.Drawing.Size(123, 24)
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
        Me.GroupBox1.Location = New System.Drawing.Point(34, 94)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.GroupBox1.Size = New System.Drawing.Size(600, 100)
        Me.GroupBox1.TabIndex = 6
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Quickbooks to TimeLive Sync"
        '
        'chk_laboritems
        '
        Me.chk_laboritems.AutoSize = True
        Me.chk_laboritems.Location = New System.Drawing.Point(158, 65)
        Me.chk_laboritems.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.chk_laboritems.Name = "chk_laboritems"
        Me.chk_laboritems.Size = New System.Drawing.Size(120, 24)
        Me.chk_laboritems.TabIndex = 5
        Me.chk_laboritems.Text = "Labor Items"
        Me.chk_laboritems.UseVisualStyleBackColor = True
        '
        'chk_accounts
        '
        Me.chk_accounts.AutoSize = True
        Me.chk_accounts.Location = New System.Drawing.Point(16, 65)
        Me.chk_accounts.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.chk_accounts.Name = "chk_accounts"
        Me.chk_accounts.Size = New System.Drawing.Size(102, 24)
        Me.chk_accounts.TabIndex = 4
        Me.chk_accounts.Text = "Accounts"
        Me.chk_accounts.UseVisualStyleBackColor = True
        '
        'chk_jobsitems
        '
        Me.chk_jobsitems.AutoSize = True
        Me.chk_jobsitems.Location = New System.Drawing.Point(450, 29)
        Me.chk_jobsitems.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.chk_jobsitems.Name = "chk_jobsitems"
        Me.chk_jobsitems.Size = New System.Drawing.Size(113, 24)
        Me.chk_jobsitems.TabIndex = 3
        Me.chk_jobsitems.Text = "Jobs/Items"
        Me.chk_jobsitems.UseVisualStyleBackColor = True
        '
        'chk_consultans
        '
        Me.chk_consultans.AllowDrop = True
        Me.chk_consultans.AutoSize = True
        Me.chk_consultans.Location = New System.Drawing.Point(302, 29)
        Me.chk_consultans.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.chk_consultans.Name = "chk_consultans"
        Me.chk_consultans.Size = New System.Drawing.Size(120, 24)
        Me.chk_consultans.TabIndex = 2
        Me.chk_consultans.Text = "Consultants"
        Me.chk_consultans.UseVisualStyleBackColor = True
        '
        'chk_employees
        '
        Me.chk_employees.AutoSize = True
        Me.chk_employees.Location = New System.Drawing.Point(158, 29)
        Me.chk_employees.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.chk_employees.Name = "chk_employees"
        Me.chk_employees.Size = New System.Drawing.Size(113, 24)
        Me.chk_employees.TabIndex = 1
        Me.chk_employees.Text = "Employees"
        Me.chk_employees.UseVisualStyleBackColor = True
        '
        'chk_customers
        '
        Me.chk_customers.AutoSize = True
        Me.chk_customers.Location = New System.Drawing.Point(16, 29)
        Me.chk_customers.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.chk_customers.Name = "chk_customers"
        Me.chk_customers.Size = New System.Drawing.Size(112, 24)
        Me.chk_customers.TabIndex = 0
        Me.chk_customers.Text = "Customers"
        Me.chk_customers.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(364, 46)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(128, 20)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "Auto run interval:"
        '
        'autoruninterval_btn
        '
        Me.autoruninterval_btn.AllowDrop = True
        Me.autoruninterval_btn.Location = New System.Drawing.Point(504, 35)
        Me.autoruninterval_btn.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.autoruninterval_btn.Maximum = New Decimal(New Integer() {24, 0, 0, 0})
        Me.autoruninterval_btn.Name = "autoruninterval_btn"
        Me.autoruninterval_btn.Size = New System.Drawing.Size(64, 26)
        Me.autoruninterval_btn.TabIndex = 4
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(30, 46)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(144, 20)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Auto run time start:"
        '
        'dtp_autoruntime
        '
        Me.dtp_autoruntime.Format = System.Windows.Forms.DateTimePickerFormat.Time
        Me.dtp_autoruntime.Location = New System.Drawing.Point(182, 37)
        Me.dtp_autoruntime.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.dtp_autoruntime.Name = "dtp_autoruntime"
        Me.dtp_autoruntime.Size = New System.Drawing.Size(128, 26)
        Me.dtp_autoruntime.TabIndex = 2
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.Tab_Sync)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Controls.Add(Me.Tab_HoursOptions)
        Me.TabControl1.Controls.Add(Me.Tab_ItemsJobs)
        Me.TabControl1.Controls.Add(Me.Tab_DebugMode)
        Me.TabControl1.Location = New System.Drawing.Point(18, 18)
        Me.TabControl1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(692, 345)
        Me.TabControl1.TabIndex = 35
        '
        'Tab_ItemsJobs
        '
        Me.Tab_ItemsJobs.Controls.Add(Me.GroupBox4)
        Me.Tab_ItemsJobs.Location = New System.Drawing.Point(4, 29)
        Me.Tab_ItemsJobs.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Tab_ItemsJobs.Name = "Tab_ItemsJobs"
        Me.Tab_ItemsJobs.Padding = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Tab_ItemsJobs.Size = New System.Drawing.Size(684, 312)
        Me.Tab_ItemsJobs.TabIndex = 3
        Me.Tab_ItemsJobs.Text = "Items/Jobs"
        Me.Tab_ItemsJobs.UseVisualStyleBackColor = True
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.item_subItems_btn)
        Me.GroupBox4.Controls.Add(Me.job_subJobs_btn)
        Me.GroupBox4.Location = New System.Drawing.Point(9, 35)
        Me.GroupBox4.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Padding = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.GroupBox4.Size = New System.Drawing.Size(614, 71)
        Me.GroupBox4.TabIndex = 47
        Me.GroupBox4.TabStop = False
        '
        'item_subItems_btn
        '
        Me.item_subItems_btn.AutoSize = True
        Me.item_subItems_btn.Location = New System.Drawing.Point(250, 28)
        Me.item_subItems_btn.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.item_subItems_btn.Name = "item_subItems_btn"
        Me.item_subItems_btn.Size = New System.Drawing.Size(151, 24)
        Me.item_subItems_btn.TabIndex = 44
        Me.item_subItems_btn.Text = "Items/Sub Items"
        Me.item_subItems_btn.UseVisualStyleBackColor = True
        '
        'job_subJobs_btn
        '
        Me.job_subJobs_btn.AutoSize = True
        Me.job_subJobs_btn.Checked = True
        Me.job_subJobs_btn.Location = New System.Drawing.Point(40, 28)
        Me.job_subJobs_btn.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.job_subJobs_btn.Name = "job_subJobs_btn"
        Me.job_subJobs_btn.Size = New System.Drawing.Size(139, 24)
        Me.job_subJobs_btn.TabIndex = 43
        Me.job_subJobs_btn.TabStop = True
        Me.job_subJobs_btn.Text = "Jobs/Sub Jobs"
        Me.job_subJobs_btn.UseVisualStyleBackColor = True
        '
        'Tab_DebugMode
        '
        Me.Tab_DebugMode.Controls.Add(Me.chk_debug)
        Me.Tab_DebugMode.Location = New System.Drawing.Point(4, 29)
        Me.Tab_DebugMode.Name = "Tab_DebugMode"
        Me.Tab_DebugMode.Size = New System.Drawing.Size(684, 312)
        Me.Tab_DebugMode.TabIndex = 4
        Me.Tab_DebugMode.Text = "Debug Mode"
        Me.Tab_DebugMode.UseVisualStyleBackColor = True
        '
        'chk_debug
        '
        Me.chk_debug.AutoSize = True
        Me.chk_debug.Location = New System.Drawing.Point(36, 45)
        Me.chk_debug.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.chk_debug.Name = "chk_debug"
        Me.chk_debug.Size = New System.Drawing.Size(223, 24)
        Me.chk_debug.TabIndex = 1
        Me.chk_debug.Text = "Display Debug Information"
        Me.chk_debug.UseVisualStyleBackColor = True
        '
        'AppSettings
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(728, 422)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.btnSave)
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Name = "AppSettings"
        Me.Text = "AppSettings"
        Me.Tab_HoursOptions.ResumeLayout(False)
        Me.Tab_HoursOptions.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage2.PerformLayout()
        Me.Tab_Sync.ResumeLayout(False)
        Me.Tab_Sync.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.autoruninterval_btn, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabControl1.ResumeLayout(False)
        Me.Tab_ItemsJobs.ResumeLayout(False)
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.Tab_DebugMode.ResumeLayout(False)
        Me.Tab_DebugMode.PerformLayout()
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
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents chk_syncElgibleVendor As CheckBox
    Friend WithEvents Tab_Sync As TabPage
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
    Friend WithEvents Tab_ItemsJobs As TabPage
    Friend WithEvents GroupBox4 As GroupBox
    Friend WithEvents item_subItems_btn As RadioButton
    Friend WithEvents job_subJobs_btn As RadioButton
    Friend WithEvents Tab_DebugMode As TabPage
    Friend WithEvents chk_debug As CheckBox
End Class
