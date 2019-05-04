<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class IntegratedUI
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
        Me.bntclose = New System.Windows.Forms.Button()
        Me.btnTransfer = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.TabPageTimeTransfer = New System.Windows.Forms.TabPage()
        Me.RefreshTimeTransfer = New System.Windows.Forms.Button()
        Me.cbWageType = New System.Windows.Forms.ComboBox()
        Me.lblWageType = New System.Windows.Forms.Label()
        Me.preWeek = New System.Windows.Forms.Button()
        Me.lblUptoDate = New System.Windows.Forms.Label()
        Me.lblFromDate = New System.Windows.Forms.Label()
        Me.dpEndDate = New System.Windows.Forms.DateTimePicker()
        Me.dpStartDate = New System.Windows.Forms.DateTimePicker()
        Me.nextWeek = New System.Windows.Forms.Button()
        Me.btn_currentweek = New System.Windows.Forms.Button()
        Me.AttributeTabControl = New System.Windows.Forms.TabControl()
        Me.TabPageCustomers = New System.Windows.Forms.TabPage()
        Me.RefreshCustomers = New System.Windows.Forms.Button()
        Me.CustomerSyncDirection = New System.Windows.Forms.GroupBox()
        Me.TLtoQBCustomerRadioButton = New System.Windows.Forms.RadioButton()
        Me.QBtoTLCustomerRadioButton = New System.Windows.Forms.RadioButton()
        Me.TabPageEmployees = New System.Windows.Forms.TabPage()
        Me.RefreshEmployees = New System.Windows.Forms.Button()
        Me.EmployeeSyncDirection = New System.Windows.Forms.GroupBox()
        Me.TLtoQBEmployeeRadioButton = New System.Windows.Forms.RadioButton()
        Me.QBtoTLEmployeeRadioButton = New System.Windows.Forms.RadioButton()
        Me.TabPageVendor = New System.Windows.Forms.TabPage()
        Me.RefreshVendors = New System.Windows.Forms.Button()
        Me.VendorSyncDirection = New System.Windows.Forms.GroupBox()
        Me.TLtoQBVendorRadioButton = New System.Windows.Forms.RadioButton()
        Me.QBtoTLVendorRadioButton = New System.Windows.Forms.RadioButton()
        Me.TabPageJobsItems = New System.Windows.Forms.TabPage()
        Me.RefreshJobsOrItems = New System.Windows.Forms.Button()
        Me.JobItemSyncDirection = New System.Windows.Forms.GroupBox()
        Me.TLtoQBJobItemRadioButton = New System.Windows.Forms.RadioButton()
        Me.QBtoTLJobItemRadioButton = New System.Windows.Forms.RadioButton()
        Me.SyncFromLabel = New System.Windows.Forms.Label()
        Me.SyncToLabel = New System.Windows.Forms.Label()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.SelectAllCheckBox = New System.Windows.Forms.CheckBox()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.ckBox = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.TimeEntrySelectAll = New System.Windows.Forms.CheckBox()
        Me.DataGridView2 = New System.Windows.Forms.DataGridView()
        Me.GroupBox1.SuspendLayout()
        Me.TabPageTimeTransfer.SuspendLayout()
        Me.AttributeTabControl.SuspendLayout()
        Me.TabPageCustomers.SuspendLayout()
        Me.CustomerSyncDirection.SuspendLayout()
        Me.TabPageEmployees.SuspendLayout()
        Me.EmployeeSyncDirection.SuspendLayout()
        Me.TabPageVendor.SuspendLayout()
        Me.VendorSyncDirection.SuspendLayout()
        Me.TabPageJobsItems.SuspendLayout()
        Me.JobItemSyncDirection.SuspendLayout()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'bntclose
        '
        Me.bntclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.bntclose.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.bntclose.Location = New System.Drawing.Point(903, 597)
        Me.bntclose.Name = "bntclose"
        Me.bntclose.Size = New System.Drawing.Size(75, 23)
        Me.bntclose.TabIndex = 32
        Me.bntclose.Text = "Close"
        Me.bntclose.UseVisualStyleBackColor = True
        '
        'btnTransfer
        '
        Me.btnTransfer.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnTransfer.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnTransfer.Location = New System.Drawing.Point(813, 597)
        Me.btnTransfer.Name = "btnTransfer"
        Me.btnTransfer.Size = New System.Drawing.Size(75, 23)
        Me.btnTransfer.TabIndex = 31
        Me.btnTransfer.Text = "Process"
        Me.btnTransfer.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.ProgressBar1)
        Me.GroupBox1.Location = New System.Drawing.Point(2, 554)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(986, 37)
        Me.GroupBox1.TabIndex = 39
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Progress Bar"
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ProgressBar1.Location = New System.Drawing.Point(10, 17)
        Me.ProgressBar1.Maximum = 10
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(966, 14)
        Me.ProgressBar1.TabIndex = 0
        '
        'TabPageTimeTransfer
        '
        Me.TabPageTimeTransfer.Controls.Add(Me.RefreshTimeTransfer)
        Me.TabPageTimeTransfer.Controls.Add(Me.cbWageType)
        Me.TabPageTimeTransfer.Controls.Add(Me.lblWageType)
        Me.TabPageTimeTransfer.Controls.Add(Me.preWeek)
        Me.TabPageTimeTransfer.Controls.Add(Me.lblUptoDate)
        Me.TabPageTimeTransfer.Controls.Add(Me.lblFromDate)
        Me.TabPageTimeTransfer.Controls.Add(Me.dpEndDate)
        Me.TabPageTimeTransfer.Controls.Add(Me.dpStartDate)
        Me.TabPageTimeTransfer.Controls.Add(Me.nextWeek)
        Me.TabPageTimeTransfer.Controls.Add(Me.btn_currentweek)
        Me.TabPageTimeTransfer.Location = New System.Drawing.Point(4, 22)
        Me.TabPageTimeTransfer.Name = "TabPageTimeTransfer"
        Me.TabPageTimeTransfer.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageTimeTransfer.Size = New System.Drawing.Size(978, 82)
        Me.TabPageTimeTransfer.TabIndex = 2
        Me.TabPageTimeTransfer.Text = "Time Transfer Options"
        Me.TabPageTimeTransfer.UseVisualStyleBackColor = True
        '
        'RefreshTimeTransfer
        '
        Me.RefreshTimeTransfer.Location = New System.Drawing.Point(691, 21)
        Me.RefreshTimeTransfer.Name = "RefreshTimeTransfer"
        Me.RefreshTimeTransfer.Size = New System.Drawing.Size(97, 30)
        Me.RefreshTimeTransfer.TabIndex = 45
        Me.RefreshTimeTransfer.Text = "Refresh"
        Me.RefreshTimeTransfer.UseVisualStyleBackColor = True
        '
        'cbWageType
        '
        Me.cbWageType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbWageType.FormattingEnabled = True
        Me.cbWageType.Items.AddRange(New Object() {"Bonus", "Comission", "Hourly-Overtime", "Hourly-Regular", "Hourly-Sick", "Hourly-Vacation", "Salary-Regular", "Salary-Sick", "Salary-Vacation"})
        Me.cbWageType.Location = New System.Drawing.Point(402, 18)
        Me.cbWageType.Name = "cbWageType"
        Me.cbWageType.Size = New System.Drawing.Size(219, 21)
        Me.cbWageType.TabIndex = 43
        '
        'lblWageType
        '
        Me.lblWageType.AutoSize = True
        Me.lblWageType.Location = New System.Drawing.Point(330, 22)
        Me.lblWageType.Name = "lblWageType"
        Me.lblWageType.Size = New System.Drawing.Size(66, 13)
        Me.lblWageType.TabIndex = 44
        Me.lblWageType.Text = "Wage Type:"
        '
        'preWeek
        '
        Me.preWeek.Location = New System.Drawing.Point(48, 40)
        Me.preWeek.Name = "preWeek"
        Me.preWeek.Size = New System.Drawing.Size(50, 23)
        Me.preWeek.TabIndex = 41
        Me.preWeek.Text = "<<"
        Me.preWeek.UseVisualStyleBackColor = True
        '
        'lblUptoDate
        '
        Me.lblUptoDate.AutoSize = True
        Me.lblUptoDate.Location = New System.Drawing.Point(141, 21)
        Me.lblUptoDate.Name = "lblUptoDate"
        Me.lblUptoDate.Size = New System.Drawing.Size(24, 13)
        Me.lblUptoDate.TabIndex = 39
        Me.lblUptoDate.Text = "Up:"
        '
        'lblFromDate
        '
        Me.lblFromDate.AutoSize = True
        Me.lblFromDate.Location = New System.Drawing.Point(9, 22)
        Me.lblFromDate.Name = "lblFromDate"
        Me.lblFromDate.Size = New System.Drawing.Size(33, 13)
        Me.lblFromDate.TabIndex = 38
        Me.lblFromDate.Text = "From:"
        '
        'dpEndDate
        '
        Me.dpEndDate.CustomFormat = ""
        Me.dpEndDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dpEndDate.Location = New System.Drawing.Point(171, 14)
        Me.dpEndDate.Name = "dpEndDate"
        Me.dpEndDate.Size = New System.Drawing.Size(87, 20)
        Me.dpEndDate.TabIndex = 37
        '
        'dpStartDate
        '
        Me.dpStartDate.CustomFormat = ""
        Me.dpStartDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dpStartDate.Location = New System.Drawing.Point(48, 15)
        Me.dpStartDate.Name = "dpStartDate"
        Me.dpStartDate.Size = New System.Drawing.Size(87, 20)
        Me.dpStartDate.TabIndex = 36
        '
        'nextWeek
        '
        Me.nextWeek.Location = New System.Drawing.Point(208, 40)
        Me.nextWeek.Name = "nextWeek"
        Me.nextWeek.Size = New System.Drawing.Size(50, 23)
        Me.nextWeek.TabIndex = 42
        Me.nextWeek.Text = ">>"
        Me.nextWeek.UseVisualStyleBackColor = True
        '
        'btn_currentweek
        '
        Me.btn_currentweek.Location = New System.Drawing.Point(104, 40)
        Me.btn_currentweek.Name = "btn_currentweek"
        Me.btn_currentweek.Size = New System.Drawing.Size(98, 23)
        Me.btn_currentweek.TabIndex = 40
        Me.btn_currentweek.Text = "Current Week"
        Me.btn_currentweek.UseVisualStyleBackColor = True
        '
        'AttributeTabControl
        '
        Me.AttributeTabControl.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.AttributeTabControl.Controls.Add(Me.TabPageCustomers)
        Me.AttributeTabControl.Controls.Add(Me.TabPageEmployees)
        Me.AttributeTabControl.Controls.Add(Me.TabPageVendor)
        Me.AttributeTabControl.Controls.Add(Me.TabPageJobsItems)
        Me.AttributeTabControl.Controls.Add(Me.TabPageTimeTransfer)
        Me.AttributeTabControl.Location = New System.Drawing.Point(2, -1)
        Me.AttributeTabControl.Name = "AttributeTabControl"
        Me.AttributeTabControl.SelectedIndex = 0
        Me.AttributeTabControl.Size = New System.Drawing.Size(986, 108)
        Me.AttributeTabControl.TabIndex = 40
        '
        'TabPageCustomers
        '
        Me.TabPageCustomers.Controls.Add(Me.RefreshCustomers)
        Me.TabPageCustomers.Controls.Add(Me.CustomerSyncDirection)
        Me.TabPageCustomers.Location = New System.Drawing.Point(4, 22)
        Me.TabPageCustomers.Name = "TabPageCustomers"
        Me.TabPageCustomers.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageCustomers.Size = New System.Drawing.Size(978, 82)
        Me.TabPageCustomers.TabIndex = 3
        Me.TabPageCustomers.Text = "Customers Options"
        Me.TabPageCustomers.UseVisualStyleBackColor = True
        '
        'RefreshCustomers
        '
        Me.RefreshCustomers.Location = New System.Drawing.Point(429, 26)
        Me.RefreshCustomers.Name = "RefreshCustomers"
        Me.RefreshCustomers.Size = New System.Drawing.Size(97, 30)
        Me.RefreshCustomers.TabIndex = 1
        Me.RefreshCustomers.Text = "Refresh"
        Me.RefreshCustomers.UseVisualStyleBackColor = True
        '
        'CustomerSyncDirection
        '
        Me.CustomerSyncDirection.Controls.Add(Me.TLtoQBCustomerRadioButton)
        Me.CustomerSyncDirection.Controls.Add(Me.QBtoTLCustomerRadioButton)
        Me.CustomerSyncDirection.Location = New System.Drawing.Point(6, 6)
        Me.CustomerSyncDirection.Name = "CustomerSyncDirection"
        Me.CustomerSyncDirection.Size = New System.Drawing.Size(344, 69)
        Me.CustomerSyncDirection.TabIndex = 0
        Me.CustomerSyncDirection.TabStop = False
        Me.CustomerSyncDirection.Text = "Sync Direction"
        '
        'TLtoQBCustomerRadioButton
        '
        Me.TLtoQBCustomerRadioButton.AutoSize = True
        Me.TLtoQBCustomerRadioButton.Location = New System.Drawing.Point(170, 28)
        Me.TLtoQBCustomerRadioButton.Name = "TLtoQBCustomerRadioButton"
        Me.TLtoQBCustomerRadioButton.Size = New System.Drawing.Size(68, 17)
        Me.TLtoQBCustomerRadioButton.TabIndex = 1
        Me.TLtoQBCustomerRadioButton.TabStop = True
        Me.TLtoQBCustomerRadioButton.Text = "TL -> QB"
        Me.TLtoQBCustomerRadioButton.UseVisualStyleBackColor = True
        '
        'QBtoTLCustomerRadioButton
        '
        Me.QBtoTLCustomerRadioButton.AutoSize = True
        Me.QBtoTLCustomerRadioButton.Checked = True
        Me.QBtoTLCustomerRadioButton.Location = New System.Drawing.Point(29, 28)
        Me.QBtoTLCustomerRadioButton.Name = "QBtoTLCustomerRadioButton"
        Me.QBtoTLCustomerRadioButton.Size = New System.Drawing.Size(68, 17)
        Me.QBtoTLCustomerRadioButton.TabIndex = 0
        Me.QBtoTLCustomerRadioButton.TabStop = True
        Me.QBtoTLCustomerRadioButton.Text = "QB -> TL"
        Me.QBtoTLCustomerRadioButton.UseVisualStyleBackColor = True
        '
        'TabPageEmployees
        '
        Me.TabPageEmployees.Controls.Add(Me.RefreshEmployees)
        Me.TabPageEmployees.Controls.Add(Me.EmployeeSyncDirection)
        Me.TabPageEmployees.Location = New System.Drawing.Point(4, 22)
        Me.TabPageEmployees.Name = "TabPageEmployees"
        Me.TabPageEmployees.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageEmployees.Size = New System.Drawing.Size(978, 82)
        Me.TabPageEmployees.TabIndex = 4
        Me.TabPageEmployees.Text = "Employees Options"
        Me.TabPageEmployees.UseVisualStyleBackColor = True
        '
        'RefreshEmployees
        '
        Me.RefreshEmployees.Location = New System.Drawing.Point(429, 26)
        Me.RefreshEmployees.Name = "RefreshEmployees"
        Me.RefreshEmployees.Size = New System.Drawing.Size(97, 30)
        Me.RefreshEmployees.TabIndex = 2
        Me.RefreshEmployees.Text = "Refresh"
        Me.RefreshEmployees.UseVisualStyleBackColor = True
        '
        'EmployeeSyncDirection
        '
        Me.EmployeeSyncDirection.Controls.Add(Me.TLtoQBEmployeeRadioButton)
        Me.EmployeeSyncDirection.Controls.Add(Me.QBtoTLEmployeeRadioButton)
        Me.EmployeeSyncDirection.Location = New System.Drawing.Point(6, 6)
        Me.EmployeeSyncDirection.Name = "EmployeeSyncDirection"
        Me.EmployeeSyncDirection.Size = New System.Drawing.Size(344, 69)
        Me.EmployeeSyncDirection.TabIndex = 1
        Me.EmployeeSyncDirection.TabStop = False
        Me.EmployeeSyncDirection.Text = "Sync Direction"
        '
        'TLtoQBEmployeeRadioButton
        '
        Me.TLtoQBEmployeeRadioButton.AutoSize = True
        Me.TLtoQBEmployeeRadioButton.Location = New System.Drawing.Point(170, 28)
        Me.TLtoQBEmployeeRadioButton.Name = "TLtoQBEmployeeRadioButton"
        Me.TLtoQBEmployeeRadioButton.Size = New System.Drawing.Size(68, 17)
        Me.TLtoQBEmployeeRadioButton.TabIndex = 1
        Me.TLtoQBEmployeeRadioButton.TabStop = True
        Me.TLtoQBEmployeeRadioButton.Text = "TL -> QB"
        Me.TLtoQBEmployeeRadioButton.UseVisualStyleBackColor = True
        '
        'QBtoTLEmployeeRadioButton
        '
        Me.QBtoTLEmployeeRadioButton.AutoSize = True
        Me.QBtoTLEmployeeRadioButton.Checked = True
        Me.QBtoTLEmployeeRadioButton.Location = New System.Drawing.Point(29, 28)
        Me.QBtoTLEmployeeRadioButton.Name = "QBtoTLEmployeeRadioButton"
        Me.QBtoTLEmployeeRadioButton.Size = New System.Drawing.Size(68, 17)
        Me.QBtoTLEmployeeRadioButton.TabIndex = 0
        Me.QBtoTLEmployeeRadioButton.TabStop = True
        Me.QBtoTLEmployeeRadioButton.Text = "QB -> TL"
        Me.QBtoTLEmployeeRadioButton.UseVisualStyleBackColor = True
        '
        'TabPageVendor
        '
        Me.TabPageVendor.Controls.Add(Me.RefreshVendors)
        Me.TabPageVendor.Controls.Add(Me.VendorSyncDirection)
        Me.TabPageVendor.Location = New System.Drawing.Point(4, 22)
        Me.TabPageVendor.Name = "TabPageVendor"
        Me.TabPageVendor.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageVendor.Size = New System.Drawing.Size(978, 82)
        Me.TabPageVendor.TabIndex = 5
        Me.TabPageVendor.Text = "Vendors Options"
        Me.TabPageVendor.UseVisualStyleBackColor = True
        '
        'RefreshVendors
        '
        Me.RefreshVendors.Location = New System.Drawing.Point(429, 26)
        Me.RefreshVendors.Name = "RefreshVendors"
        Me.RefreshVendors.Size = New System.Drawing.Size(97, 30)
        Me.RefreshVendors.TabIndex = 3
        Me.RefreshVendors.Text = "Refresh"
        Me.RefreshVendors.UseVisualStyleBackColor = True
        '
        'VendorSyncDirection
        '
        Me.VendorSyncDirection.Controls.Add(Me.TLtoQBVendorRadioButton)
        Me.VendorSyncDirection.Controls.Add(Me.QBtoTLVendorRadioButton)
        Me.VendorSyncDirection.Location = New System.Drawing.Point(6, 6)
        Me.VendorSyncDirection.Name = "VendorSyncDirection"
        Me.VendorSyncDirection.Size = New System.Drawing.Size(344, 69)
        Me.VendorSyncDirection.TabIndex = 2
        Me.VendorSyncDirection.TabStop = False
        Me.VendorSyncDirection.Text = "Sync Direction"
        '
        'TLtoQBVendorRadioButton
        '
        Me.TLtoQBVendorRadioButton.AutoSize = True
        Me.TLtoQBVendorRadioButton.Location = New System.Drawing.Point(170, 28)
        Me.TLtoQBVendorRadioButton.Name = "TLtoQBVendorRadioButton"
        Me.TLtoQBVendorRadioButton.Size = New System.Drawing.Size(68, 17)
        Me.TLtoQBVendorRadioButton.TabIndex = 1
        Me.TLtoQBVendorRadioButton.TabStop = True
        Me.TLtoQBVendorRadioButton.Text = "TL -> QB"
        Me.TLtoQBVendorRadioButton.UseVisualStyleBackColor = True
        '
        'QBtoTLVendorRadioButton
        '
        Me.QBtoTLVendorRadioButton.AutoSize = True
        Me.QBtoTLVendorRadioButton.Checked = True
        Me.QBtoTLVendorRadioButton.Location = New System.Drawing.Point(29, 28)
        Me.QBtoTLVendorRadioButton.Name = "QBtoTLVendorRadioButton"
        Me.QBtoTLVendorRadioButton.Size = New System.Drawing.Size(68, 17)
        Me.QBtoTLVendorRadioButton.TabIndex = 0
        Me.QBtoTLVendorRadioButton.TabStop = True
        Me.QBtoTLVendorRadioButton.Text = "QB -> TL"
        Me.QBtoTLVendorRadioButton.UseVisualStyleBackColor = True
        '
        'TabPageJobsItems
        '
        Me.TabPageJobsItems.Controls.Add(Me.RefreshJobsOrItems)
        Me.TabPageJobsItems.Controls.Add(Me.JobItemSyncDirection)
        Me.TabPageJobsItems.Location = New System.Drawing.Point(4, 22)
        Me.TabPageJobsItems.Name = "TabPageJobsItems"
        Me.TabPageJobsItems.Size = New System.Drawing.Size(978, 82)
        Me.TabPageJobsItems.TabIndex = 6
        Me.TabPageJobsItems.Text = "Jobs/Items Options"
        Me.TabPageJobsItems.UseVisualStyleBackColor = True
        '
        'RefreshJobsOrItems
        '
        Me.RefreshJobsOrItems.Location = New System.Drawing.Point(429, 26)
        Me.RefreshJobsOrItems.Name = "RefreshJobsOrItems"
        Me.RefreshJobsOrItems.Size = New System.Drawing.Size(97, 30)
        Me.RefreshJobsOrItems.TabIndex = 4
        Me.RefreshJobsOrItems.Text = "Refresh"
        Me.RefreshJobsOrItems.UseVisualStyleBackColor = True
        '
        'JobItemSyncDirection
        '
        Me.JobItemSyncDirection.Controls.Add(Me.TLtoQBJobItemRadioButton)
        Me.JobItemSyncDirection.Controls.Add(Me.QBtoTLJobItemRadioButton)
        Me.JobItemSyncDirection.Location = New System.Drawing.Point(6, 6)
        Me.JobItemSyncDirection.Name = "JobItemSyncDirection"
        Me.JobItemSyncDirection.Size = New System.Drawing.Size(344, 69)
        Me.JobItemSyncDirection.TabIndex = 3
        Me.JobItemSyncDirection.TabStop = False
        Me.JobItemSyncDirection.Text = "Sync Direction"
        '
        'TLtoQBJobItemRadioButton
        '
        Me.TLtoQBJobItemRadioButton.AutoSize = True
        Me.TLtoQBJobItemRadioButton.Location = New System.Drawing.Point(170, 28)
        Me.TLtoQBJobItemRadioButton.Name = "TLtoQBJobItemRadioButton"
        Me.TLtoQBJobItemRadioButton.Size = New System.Drawing.Size(68, 17)
        Me.TLtoQBJobItemRadioButton.TabIndex = 1
        Me.TLtoQBJobItemRadioButton.TabStop = True
        Me.TLtoQBJobItemRadioButton.Text = "TL -> QB"
        Me.TLtoQBJobItemRadioButton.UseVisualStyleBackColor = True
        '
        'QBtoTLJobItemRadioButton
        '
        Me.QBtoTLJobItemRadioButton.AutoSize = True
        Me.QBtoTLJobItemRadioButton.Checked = True
        Me.QBtoTLJobItemRadioButton.Location = New System.Drawing.Point(29, 28)
        Me.QBtoTLJobItemRadioButton.Name = "QBtoTLJobItemRadioButton"
        Me.QBtoTLJobItemRadioButton.Size = New System.Drawing.Size(68, 17)
        Me.QBtoTLJobItemRadioButton.TabIndex = 0
        Me.QBtoTLJobItemRadioButton.TabStop = True
        Me.QBtoTLJobItemRadioButton.Text = "QB -> TL"
        Me.QBtoTLJobItemRadioButton.UseVisualStyleBackColor = True
        '
        'SyncFromLabel
        '
        Me.SyncFromLabel.AutoSize = True
        Me.SyncFromLabel.Location = New System.Drawing.Point(7, 10)
        Me.SyncFromLabel.Name = "SyncFromLabel"
        Me.SyncFromLabel.Size = New System.Drawing.Size(65, 13)
        Me.SyncFromLabel.TabIndex = 41
        Me.SyncFromLabel.Text = "QuickBooks"
        '
        'SyncToLabel
        '
        Me.SyncToLabel.AutoSize = True
        Me.SyncToLabel.Location = New System.Drawing.Point(3, 10)
        Me.SyncToLabel.Name = "SyncToLabel"
        Me.SyncToLabel.Size = New System.Drawing.Size(50, 13)
        Me.SyncToLabel.TabIndex = 43
        Me.SyncToLabel.Text = "TimeLive"
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SplitContainer1.Location = New System.Drawing.Point(2, 126)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.SelectAllCheckBox)
        Me.SplitContainer1.Panel1.Controls.Add(Me.SyncFromLabel)
        Me.SplitContainer1.Panel1.Controls.Add(Me.DataGridView1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.TimeEntrySelectAll)
        Me.SplitContainer1.Panel2.Controls.Add(Me.SyncToLabel)
        Me.SplitContainer1.Panel2.Controls.Add(Me.DataGridView2)
        Me.SplitContainer1.Size = New System.Drawing.Size(986, 422)
        Me.SplitContainer1.SplitterDistance = 473
        Me.SplitContainer1.TabIndex = 44
        '
        'SelectAllCheckBox
        '
        Me.SelectAllCheckBox.AutoSize = True
        Me.SelectAllCheckBox.Location = New System.Drawing.Point(88, 10)
        Me.SelectAllCheckBox.Name = "SelectAllCheckBox"
        Me.SelectAllCheckBox.Size = New System.Drawing.Size(70, 17)
        Me.SelectAllCheckBox.TabIndex = 42
        Me.SelectAllCheckBox.Text = "Select All"
        Me.SelectAllCheckBox.UseVisualStyleBackColor = True
        '
        'DataGridView1
        '
        Me.DataGridView1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.ckBox})
        Me.DataGridView1.Location = New System.Drawing.Point(4, 31)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.Size = New System.Drawing.Size(466, 388)
        Me.DataGridView1.TabIndex = 38
        '
        'ckBox
        '
        Me.ckBox.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.ckBox.HeaderText = "Check Name"
        Me.ckBox.Name = "ckBox"
        '
        'TimeEntrySelectAll
        '
        Me.TimeEntrySelectAll.AutoSize = True
        Me.TimeEntrySelectAll.Location = New System.Drawing.Point(69, 9)
        Me.TimeEntrySelectAll.Name = "TimeEntrySelectAll"
        Me.TimeEntrySelectAll.Size = New System.Drawing.Size(70, 17)
        Me.TimeEntrySelectAll.TabIndex = 44
        Me.TimeEntrySelectAll.Text = "Select All"
        Me.TimeEntrySelectAll.UseVisualStyleBackColor = True
        '
        'DataGridView2
        '
        Me.DataGridView2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DataGridView2.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.DataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView2.Location = New System.Drawing.Point(3, 31)
        Me.DataGridView2.Name = "DataGridView2"
        Me.DataGridView2.Size = New System.Drawing.Size(496, 388)
        Me.DataGridView2.TabIndex = 42
        '
        'IntegratedUI
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(990, 631)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.AttributeTabControl)
        Me.Controls.Add(Me.bntclose)
        Me.Controls.Add(Me.btnTransfer)
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "IntegratedUI"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "IntegratedUI"
        Me.GroupBox1.ResumeLayout(False)
        Me.TabPageTimeTransfer.ResumeLayout(False)
        Me.TabPageTimeTransfer.PerformLayout()
        Me.AttributeTabControl.ResumeLayout(False)
        Me.TabPageCustomers.ResumeLayout(False)
        Me.CustomerSyncDirection.ResumeLayout(False)
        Me.CustomerSyncDirection.PerformLayout()
        Me.TabPageEmployees.ResumeLayout(False)
        Me.EmployeeSyncDirection.ResumeLayout(False)
        Me.EmployeeSyncDirection.PerformLayout()
        Me.TabPageVendor.ResumeLayout(False)
        Me.VendorSyncDirection.ResumeLayout(False)
        Me.VendorSyncDirection.PerformLayout()
        Me.TabPageJobsItems.ResumeLayout(False)
        Me.JobItemSyncDirection.ResumeLayout(False)
        Me.JobItemSyncDirection.PerformLayout()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.Panel2.PerformLayout()
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents bntclose As Button
    Friend WithEvents btnTransfer As Button
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents ProgressBar1 As ProgressBar
    Friend WithEvents TabPageTimeTransfer As TabPage
    Friend WithEvents cbWageType As ComboBox
    Friend WithEvents lblWageType As Label
    Friend WithEvents preWeek As Button
    Friend WithEvents lblUptoDate As Label
    Friend WithEvents lblFromDate As Label
    Friend WithEvents dpEndDate As DateTimePicker
    Friend WithEvents dpStartDate As DateTimePicker
    Friend WithEvents nextWeek As Button
    Friend WithEvents btn_currentweek As Button
    Friend WithEvents AttributeTabControl As TabControl
    Friend WithEvents TabPageCustomers As TabPage
    Friend WithEvents TabPageEmployees As TabPage
    Friend WithEvents TabPageJobsItems As TabPage
    Friend WithEvents SyncFromLabel As Label
    Friend WithEvents CustomerSyncDirection As GroupBox
    Friend WithEvents SyncToLabel As Label
    Friend WithEvents TabPageVendor As TabPage
    Friend WithEvents EmployeeSyncDirection As GroupBox
    Friend WithEvents TLtoQBEmployeeRadioButton As RadioButton
    Friend WithEvents QBtoTLEmployeeRadioButton As RadioButton
    Friend WithEvents QBtoTLVendorRadioButton As RadioButton
    Friend WithEvents TLtoQBVendorRadioButton As RadioButton
    Friend WithEvents VendorSyncDirection As GroupBox
    Friend WithEvents JobItemSyncDirection As GroupBox
    Friend WithEvents TLtoQBJobItemRadioButton As RadioButton
    Friend WithEvents QBtoTLJobItemRadioButton As RadioButton
    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents ckBox As DataGridViewCheckBoxColumn
    Friend WithEvents DataGridView2 As DataGridView
    Friend WithEvents TLtoQBCustomerRadioButton As RadioButton
    Friend WithEvents QBtoTLCustomerRadioButton As RadioButton
    Friend WithEvents SelectAllCheckBox As CheckBox
    Friend WithEvents RefreshCustomers As Button
    Friend WithEvents RefreshEmployees As Button
    Friend WithEvents RefreshVendors As Button
    Friend WithEvents RefreshJobsOrItems As Button
    Friend WithEvents TimeEntrySelectAll As CheckBox
    Friend WithEvents RefreshTimeTransfer As Button
End Class
