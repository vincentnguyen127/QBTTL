﻿<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class MAIN
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MAIN))
        Me.ToolTip = New System.Windows.Forms.ToolTip(Me.components)
        Me.ToolStripButton2 = New System.Windows.Forms.ToolStripButton()
        Me.Exitbtn = New System.Windows.Forms.ToolStripButton()
        Me.SyncFromLabel = New System.Windows.Forms.Label()
        Me.SyncToLabel = New System.Windows.Forms.Label()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.SelectAllCheckBox = New System.Windows.Forms.CheckBox()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.ckBox = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.EditLink_MenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.EntitiesSelectAll = New System.Windows.Forms.CheckBox()
        Me.DataGridView2 = New System.Windows.Forms.DataGridView()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.StatusWindow = New System.Windows.Forms.TextBox()
        Me.StatusStrip = New System.Windows.Forms.StatusStrip()
        Me.CurrentTime = New System.Windows.Forms.ToolStripStatusLabel()
        Me.NextProcessingTime = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.settingbtn = New System.Windows.Forms.ToolStripButton()
        Me.loginbtn = New System.Windows.Forms.ToolStripButton()
        Me.btn_systemsync = New System.Windows.Forms.ToolStripButton()
        Me.btn_relationships = New System.Windows.Forms.ToolStripButton()
        Me.btn_relationships2 = New System.Windows.Forms.ToolStripButton()
        Me.clearlogbtn = New System.Windows.Forms.ToolStripButton()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.btnNewEmployee = New System.Windows.Forms.Button()
        Me.btnNewVendor = New System.Windows.Forms.Button()
        Me.btnNewCustomer = New System.Windows.Forms.Button()
        Me.ShowEntitiesBtn = New System.Windows.Forms.Button()
        Me.btnTransfer = New System.Windows.Forms.Button()
        Me.SendEmailsButton = New System.Windows.Forms.Button()
        Me.btnCreateJobItem = New System.Windows.Forms.Button()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.btnServiceItemTreeView = New System.Windows.Forms.Button()
        Me.TabPageExpenseReport = New System.Windows.Forms.TabPage()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.refreshExpenseReport = New System.Windows.Forms.Button()
        Me.expense_prevWeek = New System.Windows.Forms.Button()
        Me.ToLabelExpenseReports = New System.Windows.Forms.Label()
        Me.fromLabelExpenseReports = New System.Windows.Forms.Label()
        Me.expenseReportEndDate = New System.Windows.Forms.DateTimePicker()
        Me.expenseReportStartDate = New System.Windows.Forms.DateTimePicker()
        Me.expense_nextWeek = New System.Windows.Forms.Button()
        Me.expense_btn_currWeek = New System.Windows.Forms.Button()
        Me.TabPageTimeTransfer = New System.Windows.Forms.TabPage()
        Me.RefreshTimeTransfer = New System.Windows.Forms.Button()
        Me.cbWageType = New System.Windows.Forms.ComboBox()
        Me.lblWageType = New System.Windows.Forms.Label()
        Me.time_prevWeek = New System.Windows.Forms.Button()
        Me.lblUptoDate = New System.Windows.Forms.Label()
        Me.lblFromDate = New System.Windows.Forms.Label()
        Me.dpEndDate = New System.Windows.Forms.DateTimePicker()
        Me.dpStartDate = New System.Windows.Forms.DateTimePicker()
        Me.time_nextWeek = New System.Windows.Forms.Button()
        Me.time_btn_currentweek = New System.Windows.Forms.Button()
        Me.TabPageJobsItems = New System.Windows.Forms.TabPage()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.RefreshJobsOrItems = New System.Windows.Forms.Button()
        Me.JobItemSyncDirection = New System.Windows.Forms.GroupBox()
        Me.TLtoQBJobItemRadioButton = New System.Windows.Forms.RadioButton()
        Me.QBtoTLJobItemRadioButton = New System.Windows.Forms.RadioButton()
        Me.TabPageVendor = New System.Windows.Forms.TabPage()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.RefreshVendors = New System.Windows.Forms.Button()
        Me.VendorSyncDirection = New System.Windows.Forms.GroupBox()
        Me.TLtoQBVendorRadioButton = New System.Windows.Forms.RadioButton()
        Me.QBtoTLVendorRadioButton = New System.Windows.Forms.RadioButton()
        Me.TabPageEmployees = New System.Windows.Forms.TabPage()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.RefreshEmployees = New System.Windows.Forms.Button()
        Me.EmployeeSyncDirection = New System.Windows.Forms.GroupBox()
        Me.TLtoQBEmployeeRadioButton = New System.Windows.Forms.RadioButton()
        Me.QBtoTLEmployeeRadioButton = New System.Windows.Forms.RadioButton()
        Me.TabPageCustomers = New System.Windows.Forms.TabPage()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.RefreshCustomers = New System.Windows.Forms.Button()
        Me.CustomerSyncDirection = New System.Windows.Forms.GroupBox()
        Me.TLtoQBCustomerRadioButton = New System.Windows.Forms.RadioButton()
        Me.QBtoTLCustomerRadioButton = New System.Windows.Forms.RadioButton()
        Me.AttributeTabControl = New System.Windows.Forms.TabControl()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.ContextMenuStrip1.SuspendLayout()
        CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        Me.StatusStrip.SuspendLayout()
        Me.ToolStrip1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPageExpenseReport.SuspendLayout()
        Me.TabPageTimeTransfer.SuspendLayout()
        Me.TabPageJobsItems.SuspendLayout()
        Me.JobItemSyncDirection.SuspendLayout()
        Me.TabPageVendor.SuspendLayout()
        Me.VendorSyncDirection.SuspendLayout()
        Me.TabPageEmployees.SuspendLayout()
        Me.EmployeeSyncDirection.SuspendLayout()
        Me.TabPageCustomers.SuspendLayout()
        Me.CustomerSyncDirection.SuspendLayout()
        Me.AttributeTabControl.SuspendLayout()
        Me.SuspendLayout()
        '
        'ToolStripButton2
        '
        Me.ToolStripButton2.Name = "ToolStripButton2"
        Me.ToolStripButton2.Size = New System.Drawing.Size(82, 4)
        '
        'Exitbtn
        '
        Me.Exitbtn.AutoSize = False
        Me.Exitbtn.Image = CType(resources.GetObject("Exitbtn.Image"), System.Drawing.Image)
        Me.Exitbtn.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.Exitbtn.Name = "Exitbtn"
        Me.Exitbtn.Size = New System.Drawing.Size(75, 50)
        Me.Exitbtn.Text = "Exit"
        Me.Exitbtn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
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
        Me.SplitContainer1.Location = New System.Drawing.Point(6, 3)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.AutoScroll = True
        Me.SplitContainer1.Panel1.Controls.Add(Me.SelectAllCheckBox)
        Me.SplitContainer1.Panel1.Controls.Add(Me.SyncFromLabel)
        Me.SplitContainer1.Panel1.Controls.Add(Me.DataGridView1)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.AutoScroll = True
        Me.SplitContainer1.Panel2.Controls.Add(Me.EntitiesSelectAll)
        Me.SplitContainer1.Panel2.Controls.Add(Me.SyncToLabel)
        Me.SplitContainer1.Panel2.Controls.Add(Me.DataGridView2)
        Me.SplitContainer1.Size = New System.Drawing.Size(1093, 398)
        Me.SplitContainer1.SplitterDistance = 519
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
        Me.DataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.ckBox})
        Me.DataGridView1.ContextMenuStrip = Me.ContextMenuStrip1
        Me.DataGridView1.Location = New System.Drawing.Point(0, 31)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.Size = New System.Drawing.Size(507, 245)
        Me.DataGridView1.TabIndex = 38
        '
        'ckBox
        '
        Me.ckBox.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.ckBox.HeaderText = "Check Name"
        Me.ckBox.Name = "ckBox"
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.EditLink_MenuItem})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(120, 26)
        '
        'EditLink_MenuItem
        '
        Me.EditLink_MenuItem.Name = "EditLink_MenuItem"
        Me.EditLink_MenuItem.Size = New System.Drawing.Size(119, 22)
        Me.EditLink_MenuItem.Text = "Edit Link"
        '
        'EntitiesSelectAll
        '
        Me.EntitiesSelectAll.AutoSize = True
        Me.EntitiesSelectAll.Location = New System.Drawing.Point(69, 9)
        Me.EntitiesSelectAll.Name = "EntitiesSelectAll"
        Me.EntitiesSelectAll.Size = New System.Drawing.Size(70, 17)
        Me.EntitiesSelectAll.TabIndex = 44
        Me.EntitiesSelectAll.Text = "Select All"
        Me.EntitiesSelectAll.UseVisualStyleBackColor = True
        '
        'DataGridView2
        '
        Me.DataGridView2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DataGridView2.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells
        Me.DataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView2.ContextMenuStrip = Me.ContextMenuStrip1
        Me.DataGridView2.Location = New System.Drawing.Point(0, 31)
        Me.DataGridView2.Name = "DataGridView2"
        Me.DataGridView2.Size = New System.Drawing.Size(554, 242)
        Me.DataGridView2.TabIndex = 42
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SplitContainer2.Location = New System.Drawing.Point(-2, 156)
        Me.SplitContainer2.Name = "SplitContainer2"
        Me.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.SplitContainer1)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.StatusWindow)
        Me.SplitContainer2.Size = New System.Drawing.Size(1093, 401)
        Me.SplitContainer2.SplitterDistance = 279
        Me.SplitContainer2.TabIndex = 45
        '
        'StatusWindow
        '
        Me.StatusWindow.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.StatusWindow.BackColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.StatusWindow.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.StatusWindow.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.StatusWindow.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.StatusWindow.Location = New System.Drawing.Point(4, 2)
        Me.StatusWindow.Multiline = True
        Me.StatusWindow.Name = "StatusWindow"
        Me.StatusWindow.ReadOnly = True
        Me.StatusWindow.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.StatusWindow.Size = New System.Drawing.Size(1093, 103)
        Me.StatusWindow.TabIndex = 13
        '
        'StatusStrip
        '
        Me.StatusStrip.ImageScalingSize = New System.Drawing.Size(24, 24)
        Me.StatusStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CurrentTime, Me.NextProcessingTime})
        Me.StatusStrip.Location = New System.Drawing.Point(0, 614)
        Me.StatusStrip.Name = "StatusStrip"
        Me.StatusStrip.Size = New System.Drawing.Size(1093, 24)
        Me.StatusStrip.TabIndex = 49
        Me.StatusStrip.Text = "StatusStrip"
        '
        'CurrentTime
        '
        Me.CurrentTime.AutoSize = False
        Me.CurrentTime.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.CurrentTime.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenInner
        Me.CurrentTime.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.CurrentTime.Name = "CurrentTime"
        Me.CurrentTime.Size = New System.Drawing.Size(100, 19)
        Me.CurrentTime.Text = "Current Time"
        Me.CurrentTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.CurrentTime.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal
        '
        'NextProcessingTime
        '
        Me.NextProcessingTime.AutoSize = False
        Me.NextProcessingTime.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.NextProcessingTime.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenInner
        Me.NextProcessingTime.Name = "NextProcessingTime"
        Me.NextProcessingTime.Size = New System.Drawing.Size(300, 19)
        Me.NextProcessingTime.Text = "Next Procesing Time"
        Me.NextProcessingTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.NextProcessingTime.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ToolStrip1.AutoSize = False
        Me.ToolStrip1.Dock = System.Windows.Forms.DockStyle.None
        Me.ToolStrip1.ImageScalingSize = New System.Drawing.Size(32, 32)
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.settingbtn, Me.loginbtn, Me.btn_systemsync, Me.btn_relationships, Me.btn_relationships2, Me.clearlogbtn, Me.Exitbtn})
        Me.ToolStrip1.Location = New System.Drawing.Point(-2, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(1097, 50)
        Me.ToolStrip1.TabIndex = 50
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'settingbtn
        '
        Me.settingbtn.AutoSize = False
        Me.settingbtn.Image = CType(resources.GetObject("settingbtn.Image"), System.Drawing.Image)
        Me.settingbtn.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.settingbtn.Name = "settingbtn"
        Me.settingbtn.Size = New System.Drawing.Size(75, 50)
        Me.settingbtn.Text = "Settings"
        Me.settingbtn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'loginbtn
        '
        Me.loginbtn.AutoSize = False
        Me.loginbtn.Image = CType(resources.GetObject("loginbtn.Image"), System.Drawing.Image)
        Me.loginbtn.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.loginbtn.Name = "loginbtn"
        Me.loginbtn.Size = New System.Drawing.Size(75, 50)
        Me.loginbtn.Text = "TL Login"
        Me.loginbtn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'btn_systemsync
        '
        Me.btn_systemsync.AutoSize = False
        Me.btn_systemsync.Image = CType(resources.GetObject("btn_systemsync.Image"), System.Drawing.Image)
        Me.btn_systemsync.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btn_systemsync.Name = "btn_systemsync"
        Me.btn_systemsync.Size = New System.Drawing.Size(75, 50)
        Me.btn_systemsync.Text = "Sync"
        Me.btn_systemsync.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'btn_relationships
        '
        Me.btn_relationships.AutoSize = False
        Me.btn_relationships.Image = CType(resources.GetObject("btn_relationships.Image"), System.Drawing.Image)
        Me.btn_relationships.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btn_relationships.Name = "btn_relationships"
        Me.btn_relationships.Size = New System.Drawing.Size(75, 50)
        Me.btn_relationships.Text = "Relationships"
        Me.btn_relationships.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'btn_relationships2
        '
        Me.btn_relationships2.AutoSize = False
        Me.btn_relationships2.Image = CType(resources.GetObject("btn_relationships2.Image"), System.Drawing.Image)
        Me.btn_relationships2.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btn_relationships2.Name = "btn_relationships2"
        Me.btn_relationships2.Size = New System.Drawing.Size(100, 50)
        Me.btn_relationships2.Text = "Relationships_2"
        Me.btn_relationships2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'clearlogbtn
        '
        Me.clearlogbtn.AutoSize = False
        Me.clearlogbtn.Image = CType(resources.GetObject("clearlogbtn.Image"), System.Drawing.Image)
        Me.clearlogbtn.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.clearlogbtn.Name = "clearlogbtn"
        Me.clearlogbtn.Size = New System.Drawing.Size(75, 50)
        Me.clearlogbtn.Text = "Clear Log"
        Me.clearlogbtn.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ProgressBar1.Location = New System.Drawing.Point(-2, 559)
        Me.ProgressBar1.Maximum = 10
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(1093, 15)
        Me.ProgressBar1.TabIndex = 44
        '
        'btnNewEmployee
        '
        Me.btnNewEmployee.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnNewEmployee.Location = New System.Drawing.Point(8, 583)
        Me.btnNewEmployee.Margin = New System.Windows.Forms.Padding(2)
        Me.btnNewEmployee.Name = "btnNewEmployee"
        Me.btnNewEmployee.Size = New System.Drawing.Size(92, 23)
        Me.btnNewEmployee.TabIndex = 3
        Me.btnNewEmployee.Text = "New Employee"
        Me.btnNewEmployee.UseVisualStyleBackColor = True
        '
        'btnNewVendor
        '
        Me.btnNewVendor.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnNewVendor.Location = New System.Drawing.Point(8, 583)
        Me.btnNewVendor.Margin = New System.Windows.Forms.Padding(2)
        Me.btnNewVendor.Name = "btnNewVendor"
        Me.btnNewVendor.Size = New System.Drawing.Size(92, 23)
        Me.btnNewVendor.TabIndex = 4
        Me.btnNewVendor.Text = "New Vendor"
        Me.btnNewVendor.UseVisualStyleBackColor = True
        '
        'btnNewCustomer
        '
        Me.btnNewCustomer.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnNewCustomer.Location = New System.Drawing.Point(8, 583)
        Me.btnNewCustomer.Margin = New System.Windows.Forms.Padding(2)
        Me.btnNewCustomer.Name = "btnNewCustomer"
        Me.btnNewCustomer.Size = New System.Drawing.Size(92, 23)
        Me.btnNewCustomer.TabIndex = 2
        Me.btnNewCustomer.Text = "New  Customer"
        Me.btnNewCustomer.UseVisualStyleBackColor = True
        '
        'ShowEntitiesBtn
        '
        Me.ShowEntitiesBtn.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ShowEntitiesBtn.Location = New System.Drawing.Point(896, 584)
        Me.ShowEntitiesBtn.Name = "ShowEntitiesBtn"
        Me.ShowEntitiesBtn.Size = New System.Drawing.Size(98, 23)
        Me.ShowEntitiesBtn.TabIndex = 45
        Me.ShowEntitiesBtn.Text = "Show"
        Me.ShowEntitiesBtn.UseVisualStyleBackColor = True
        Me.ShowEntitiesBtn.Visible = False
        '
        'btnTransfer
        '
        Me.btnTransfer.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnTransfer.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnTransfer.Location = New System.Drawing.Point(1000, 584)
        Me.btnTransfer.Name = "btnTransfer"
        Me.btnTransfer.Size = New System.Drawing.Size(75, 23)
        Me.btnTransfer.TabIndex = 46
        Me.btnTransfer.Text = "Process"
        Me.btnTransfer.UseVisualStyleBackColor = True
        '
        'SendEmailsButton
        '
        Me.SendEmailsButton.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.SendEmailsButton.Location = New System.Drawing.Point(8, 583)
        Me.SendEmailsButton.Name = "SendEmailsButton"
        Me.SendEmailsButton.Size = New System.Drawing.Size(92, 23)
        Me.SendEmailsButton.TabIndex = 52
        Me.SendEmailsButton.Text = "Send Emails"
        Me.SendEmailsButton.UseVisualStyleBackColor = True
        Me.SendEmailsButton.Visible = False
        '
        'btnCreateJobItem
        '
        Me.btnCreateJobItem.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnCreateJobItem.Location = New System.Drawing.Point(8, 583)
        Me.btnCreateJobItem.Name = "btnCreateJobItem"
        Me.btnCreateJobItem.Size = New System.Drawing.Size(92, 23)
        Me.btnCreateJobItem.TabIndex = 7
        Me.btnCreateJobItem.Text = "New Job/Items"
        Me.btnCreateJobItem.UseVisualStyleBackColor = True
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.btnServiceItemTreeView)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(1089, 82)
        Me.TabPage1.TabIndex = 8
        Me.TabPage1.Text = "Service Item"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'btnServiceItemTreeView
        '
        Me.btnServiceItemTreeView.Location = New System.Drawing.Point(625, 36)
        Me.btnServiceItemTreeView.Name = "btnServiceItemTreeView"
        Me.btnServiceItemTreeView.Size = New System.Drawing.Size(75, 23)
        Me.btnServiceItemTreeView.TabIndex = 0
        Me.btnServiceItemTreeView.Text = "Service Item Tree View"
        Me.btnServiceItemTreeView.UseVisualStyleBackColor = True
        '
        'TabPageExpenseReport
        '
        Me.TabPageExpenseReport.Controls.Add(Me.Label6)
        Me.TabPageExpenseReport.Controls.Add(Me.refreshExpenseReport)
        Me.TabPageExpenseReport.Controls.Add(Me.expense_prevWeek)
        Me.TabPageExpenseReport.Controls.Add(Me.ToLabelExpenseReports)
        Me.TabPageExpenseReport.Controls.Add(Me.fromLabelExpenseReports)
        Me.TabPageExpenseReport.Controls.Add(Me.expenseReportEndDate)
        Me.TabPageExpenseReport.Controls.Add(Me.expenseReportStartDate)
        Me.TabPageExpenseReport.Controls.Add(Me.expense_nextWeek)
        Me.TabPageExpenseReport.Controls.Add(Me.expense_btn_currWeek)
        Me.TabPageExpenseReport.Location = New System.Drawing.Point(4, 22)
        Me.TabPageExpenseReport.Name = "TabPageExpenseReport"
        Me.TabPageExpenseReport.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageExpenseReport.Size = New System.Drawing.Size(1089, 82)
        Me.TabPageExpenseReport.TabIndex = 7
        Me.TabPageExpenseReport.Text = "Expense Report Options"
        Me.TabPageExpenseReport.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(285, 65)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(56, 13)
        Me.Label6.TabIndex = 51
        Me.Label6.Text = "Auto Sync"
        '
        'refreshExpenseReport
        '
        Me.refreshExpenseReport.Image = CType(resources.GetObject("refreshExpenseReport.Image"), System.Drawing.Image)
        Me.refreshExpenseReport.Location = New System.Drawing.Point(281, 13)
        Me.refreshExpenseReport.Name = "refreshExpenseReport"
        Me.refreshExpenseReport.Size = New System.Drawing.Size(60, 50)
        Me.refreshExpenseReport.TabIndex = 50
        Me.refreshExpenseReport.UseVisualStyleBackColor = True
        '
        'expense_prevWeek
        '
        Me.expense_prevWeek.Location = New System.Drawing.Point(48, 40)
        Me.expense_prevWeek.Name = "expense_prevWeek"
        Me.expense_prevWeek.Size = New System.Drawing.Size(50, 23)
        Me.expense_prevWeek.TabIndex = 48
        Me.expense_prevWeek.Text = "<<"
        Me.expense_prevWeek.UseVisualStyleBackColor = True
        '
        'ToLabelExpenseReports
        '
        Me.ToLabelExpenseReports.AutoSize = True
        Me.ToLabelExpenseReports.Location = New System.Drawing.Point(141, 20)
        Me.ToLabelExpenseReports.Name = "ToLabelExpenseReports"
        Me.ToLabelExpenseReports.Size = New System.Drawing.Size(24, 13)
        Me.ToLabelExpenseReports.TabIndex = 46
        Me.ToLabelExpenseReports.Text = "Up:"
        '
        'fromLabelExpenseReports
        '
        Me.fromLabelExpenseReports.AutoSize = True
        Me.fromLabelExpenseReports.Location = New System.Drawing.Point(9, 20)
        Me.fromLabelExpenseReports.Name = "fromLabelExpenseReports"
        Me.fromLabelExpenseReports.Size = New System.Drawing.Size(33, 13)
        Me.fromLabelExpenseReports.TabIndex = 45
        Me.fromLabelExpenseReports.Text = "From:"
        '
        'expenseReportEndDate
        '
        Me.expenseReportEndDate.CustomFormat = ""
        Me.expenseReportEndDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.expenseReportEndDate.Location = New System.Drawing.Point(171, 14)
        Me.expenseReportEndDate.Name = "expenseReportEndDate"
        Me.expenseReportEndDate.Size = New System.Drawing.Size(87, 20)
        Me.expenseReportEndDate.TabIndex = 44
        '
        'expenseReportStartDate
        '
        Me.expenseReportStartDate.CustomFormat = ""
        Me.expenseReportStartDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.expenseReportStartDate.Location = New System.Drawing.Point(48, 15)
        Me.expenseReportStartDate.Name = "expenseReportStartDate"
        Me.expenseReportStartDate.Size = New System.Drawing.Size(87, 20)
        Me.expenseReportStartDate.TabIndex = 43
        '
        'expense_nextWeek
        '
        Me.expense_nextWeek.Location = New System.Drawing.Point(208, 40)
        Me.expense_nextWeek.Name = "expense_nextWeek"
        Me.expense_nextWeek.Size = New System.Drawing.Size(50, 23)
        Me.expense_nextWeek.TabIndex = 49
        Me.expense_nextWeek.Text = ">>"
        Me.expense_nextWeek.UseVisualStyleBackColor = True
        '
        'expense_btn_currWeek
        '
        Me.expense_btn_currWeek.Location = New System.Drawing.Point(104, 40)
        Me.expense_btn_currWeek.Name = "expense_btn_currWeek"
        Me.expense_btn_currWeek.Size = New System.Drawing.Size(98, 23)
        Me.expense_btn_currWeek.TabIndex = 47
        Me.expense_btn_currWeek.Text = "Current Week"
        Me.expense_btn_currWeek.UseVisualStyleBackColor = True
        '
        'TabPageTimeTransfer
        '
        Me.TabPageTimeTransfer.Controls.Add(Me.RefreshTimeTransfer)
        Me.TabPageTimeTransfer.Controls.Add(Me.cbWageType)
        Me.TabPageTimeTransfer.Controls.Add(Me.lblWageType)
        Me.TabPageTimeTransfer.Controls.Add(Me.time_prevWeek)
        Me.TabPageTimeTransfer.Controls.Add(Me.lblUptoDate)
        Me.TabPageTimeTransfer.Controls.Add(Me.lblFromDate)
        Me.TabPageTimeTransfer.Controls.Add(Me.dpEndDate)
        Me.TabPageTimeTransfer.Controls.Add(Me.dpStartDate)
        Me.TabPageTimeTransfer.Controls.Add(Me.time_nextWeek)
        Me.TabPageTimeTransfer.Controls.Add(Me.time_btn_currentweek)
        Me.TabPageTimeTransfer.Location = New System.Drawing.Point(4, 22)
        Me.TabPageTimeTransfer.Name = "TabPageTimeTransfer"
        Me.TabPageTimeTransfer.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageTimeTransfer.Size = New System.Drawing.Size(1089, 82)
        Me.TabPageTimeTransfer.TabIndex = 2
        Me.TabPageTimeTransfer.Text = "Time Transfer Options"
        Me.TabPageTimeTransfer.UseVisualStyleBackColor = True
        '
        'RefreshTimeTransfer
        '
        Me.RefreshTimeTransfer.Image = CType(resources.GetObject("RefreshTimeTransfer.Image"), System.Drawing.Image)
        Me.RefreshTimeTransfer.Location = New System.Drawing.Point(603, 13)
        Me.RefreshTimeTransfer.Name = "RefreshTimeTransfer"
        Me.RefreshTimeTransfer.Size = New System.Drawing.Size(60, 58)
        Me.RefreshTimeTransfer.TabIndex = 45
        Me.RefreshTimeTransfer.UseVisualStyleBackColor = True
        '
        'cbWageType
        '
        Me.cbWageType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbWageType.FormattingEnabled = True
        Me.cbWageType.Items.AddRange(New Object() {"Bonus", "Comission", "Hourly-Overtime", "Hourly-Regular", "Hourly-Sick", "Hourly-Vacation", "Salary-Regular", "Salary-Sick", "Salary-Vacation"})
        Me.cbWageType.Location = New System.Drawing.Point(351, 16)
        Me.cbWageType.Name = "cbWageType"
        Me.cbWageType.Size = New System.Drawing.Size(219, 21)
        Me.cbWageType.TabIndex = 43
        '
        'lblWageType
        '
        Me.lblWageType.AutoSize = True
        Me.lblWageType.Location = New System.Drawing.Point(279, 20)
        Me.lblWageType.Name = "lblWageType"
        Me.lblWageType.Size = New System.Drawing.Size(66, 13)
        Me.lblWageType.TabIndex = 44
        Me.lblWageType.Text = "Wage Type:"
        '
        'time_prevWeek
        '
        Me.time_prevWeek.Location = New System.Drawing.Point(48, 40)
        Me.time_prevWeek.Name = "time_prevWeek"
        Me.time_prevWeek.Size = New System.Drawing.Size(50, 23)
        Me.time_prevWeek.TabIndex = 41
        Me.time_prevWeek.Text = "<<"
        Me.time_prevWeek.UseVisualStyleBackColor = True
        '
        'lblUptoDate
        '
        Me.lblUptoDate.AutoSize = True
        Me.lblUptoDate.Location = New System.Drawing.Point(141, 20)
        Me.lblUptoDate.Name = "lblUptoDate"
        Me.lblUptoDate.Size = New System.Drawing.Size(24, 13)
        Me.lblUptoDate.TabIndex = 39
        Me.lblUptoDate.Text = "Up:"
        '
        'lblFromDate
        '
        Me.lblFromDate.AutoSize = True
        Me.lblFromDate.Location = New System.Drawing.Point(9, 20)
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
        'time_nextWeek
        '
        Me.time_nextWeek.Location = New System.Drawing.Point(208, 40)
        Me.time_nextWeek.Name = "time_nextWeek"
        Me.time_nextWeek.Size = New System.Drawing.Size(50, 23)
        Me.time_nextWeek.TabIndex = 42
        Me.time_nextWeek.Text = ">>"
        Me.time_nextWeek.UseVisualStyleBackColor = True
        '
        'time_btn_currentweek
        '
        Me.time_btn_currentweek.Location = New System.Drawing.Point(104, 40)
        Me.time_btn_currentweek.Name = "time_btn_currentweek"
        Me.time_btn_currentweek.Size = New System.Drawing.Size(98, 23)
        Me.time_btn_currentweek.TabIndex = 40
        Me.time_btn_currentweek.Text = "Current Week"
        Me.time_btn_currentweek.UseVisualStyleBackColor = True
        '
        'TabPageJobsItems
        '
        Me.TabPageJobsItems.Controls.Add(Me.Label5)
        Me.TabPageJobsItems.Controls.Add(Me.RefreshJobsOrItems)
        Me.TabPageJobsItems.Controls.Add(Me.JobItemSyncDirection)
        Me.TabPageJobsItems.Location = New System.Drawing.Point(4, 22)
        Me.TabPageJobsItems.Name = "TabPageJobsItems"
        Me.TabPageJobsItems.Size = New System.Drawing.Size(1089, 82)
        Me.TabPageJobsItems.TabIndex = 6
        Me.TabPageJobsItems.Text = "Jobs/Items Options"
        Me.TabPageJobsItems.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(372, 62)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(56, 13)
        Me.Label5.TabIndex = 45
        Me.Label5.Text = "Auto Sync"
        '
        'RefreshJobsOrItems
        '
        Me.RefreshJobsOrItems.Image = CType(resources.GetObject("RefreshJobsOrItems.Image"), System.Drawing.Image)
        Me.RefreshJobsOrItems.Location = New System.Drawing.Point(368, 13)
        Me.RefreshJobsOrItems.Name = "RefreshJobsOrItems"
        Me.RefreshJobsOrItems.Size = New System.Drawing.Size(60, 50)
        Me.RefreshJobsOrItems.TabIndex = 4
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
        'TabPageVendor
        '
        Me.TabPageVendor.Controls.Add(Me.Label4)
        Me.TabPageVendor.Controls.Add(Me.RefreshVendors)
        Me.TabPageVendor.Controls.Add(Me.VendorSyncDirection)
        Me.TabPageVendor.Location = New System.Drawing.Point(4, 22)
        Me.TabPageVendor.Name = "TabPageVendor"
        Me.TabPageVendor.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageVendor.Size = New System.Drawing.Size(1089, 82)
        Me.TabPageVendor.TabIndex = 5
        Me.TabPageVendor.Text = "Vendors Options"
        Me.TabPageVendor.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(372, 62)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(56, 13)
        Me.Label4.TabIndex = 45
        Me.Label4.Text = "Auto Sync"
        '
        'RefreshVendors
        '
        Me.RefreshVendors.Image = CType(resources.GetObject("RefreshVendors.Image"), System.Drawing.Image)
        Me.RefreshVendors.Location = New System.Drawing.Point(368, 13)
        Me.RefreshVendors.Name = "RefreshVendors"
        Me.RefreshVendors.Size = New System.Drawing.Size(60, 49)
        Me.RefreshVendors.TabIndex = 3
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
        'TabPageEmployees
        '
        Me.TabPageEmployees.Controls.Add(Me.Label3)
        Me.TabPageEmployees.Controls.Add(Me.RefreshEmployees)
        Me.TabPageEmployees.Controls.Add(Me.EmployeeSyncDirection)
        Me.TabPageEmployees.Location = New System.Drawing.Point(4, 22)
        Me.TabPageEmployees.Name = "TabPageEmployees"
        Me.TabPageEmployees.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageEmployees.Size = New System.Drawing.Size(1089, 82)
        Me.TabPageEmployees.TabIndex = 4
        Me.TabPageEmployees.Text = "Employees Options"
        Me.TabPageEmployees.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(372, 65)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(56, 13)
        Me.Label3.TabIndex = 54
        Me.Label3.Text = "Auto Sync"
        '
        'RefreshEmployees
        '
        Me.RefreshEmployees.Image = CType(resources.GetObject("RefreshEmployees.Image"), System.Drawing.Image)
        Me.RefreshEmployees.Location = New System.Drawing.Point(368, 13)
        Me.RefreshEmployees.Name = "RefreshEmployees"
        Me.RefreshEmployees.Size = New System.Drawing.Size(60, 50)
        Me.RefreshEmployees.TabIndex = 2
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
        'TabPageCustomers
        '
        Me.TabPageCustomers.Controls.Add(Me.Label2)
        Me.TabPageCustomers.Controls.Add(Me.RefreshCustomers)
        Me.TabPageCustomers.Controls.Add(Me.CustomerSyncDirection)
        Me.TabPageCustomers.Location = New System.Drawing.Point(4, 22)
        Me.TabPageCustomers.Name = "TabPageCustomers"
        Me.TabPageCustomers.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageCustomers.Size = New System.Drawing.Size(1089, 82)
        Me.TabPageCustomers.TabIndex = 3
        Me.TabPageCustomers.Text = "Customers Options"
        Me.TabPageCustomers.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(371, 62)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(56, 13)
        Me.Label2.TabIndex = 44
        Me.Label2.Text = "Auto Sync"
        '
        'RefreshCustomers
        '
        Me.RefreshCustomers.Image = CType(resources.GetObject("RefreshCustomers.Image"), System.Drawing.Image)
        Me.RefreshCustomers.Location = New System.Drawing.Point(368, 13)
        Me.RefreshCustomers.Name = "RefreshCustomers"
        Me.RefreshCustomers.Size = New System.Drawing.Size(60, 51)
        Me.RefreshCustomers.TabIndex = 1
        Me.RefreshCustomers.TextAlign = System.Drawing.ContentAlignment.BottomCenter
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
        'AttributeTabControl
        '
        Me.AttributeTabControl.AccessibleName = ""
        Me.AttributeTabControl.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.AttributeTabControl.Controls.Add(Me.TabPageCustomers)
        Me.AttributeTabControl.Controls.Add(Me.TabPageEmployees)
        Me.AttributeTabControl.Controls.Add(Me.TabPageVendor)
        Me.AttributeTabControl.Controls.Add(Me.TabPageJobsItems)
        Me.AttributeTabControl.Controls.Add(Me.TabPageTimeTransfer)
        Me.AttributeTabControl.Controls.Add(Me.TabPageExpenseReport)
        Me.AttributeTabControl.Controls.Add(Me.TabPage1)
        Me.AttributeTabControl.Location = New System.Drawing.Point(-2, 53)
        Me.AttributeTabControl.Name = "AttributeTabControl"
        Me.AttributeTabControl.SelectedIndex = 0
        Me.AttributeTabControl.Size = New System.Drawing.Size(1097, 108)
        Me.AttributeTabControl.TabIndex = 48
        '
        'MAIN
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSize = True
        Me.ClientSize = New System.Drawing.Size(1093, 638)
        Me.Controls.Add(Me.btnCreateJobItem)
        Me.Controls.Add(Me.btnNewVendor)
        Me.Controls.Add(Me.btnNewEmployee)
        Me.Controls.Add(Me.btnNewCustomer)
        Me.Controls.Add(Me.SendEmailsButton)
        Me.Controls.Add(Me.ShowEntitiesBtn)
        Me.Controls.Add(Me.SplitContainer2)
        Me.Controls.Add(Me.StatusStrip)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.ProgressBar1)
        Me.Controls.Add(Me.AttributeTabControl)
        Me.Controls.Add(Me.btnTransfer)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.IsMdiContainer = True
        Me.Name = "MAIN"
        Me.Text = "Time Live Quick Book"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel1.PerformLayout()
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.Panel2.PerformLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ContextMenuStrip1.ResumeLayout(False)
        CType(Me.DataGridView2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.Panel2.PerformLayout()
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer2.ResumeLayout(False)
        Me.StatusStrip.ResumeLayout(False)
        Me.StatusStrip.PerformLayout()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.TabPage1.ResumeLayout(False)
        Me.TabPageExpenseReport.ResumeLayout(False)
        Me.TabPageExpenseReport.PerformLayout()
        Me.TabPageTimeTransfer.ResumeLayout(False)
        Me.TabPageTimeTransfer.PerformLayout()
        Me.TabPageJobsItems.ResumeLayout(False)
        Me.TabPageJobsItems.PerformLayout()
        Me.JobItemSyncDirection.ResumeLayout(False)
        Me.JobItemSyncDirection.PerformLayout()
        Me.TabPageVendor.ResumeLayout(False)
        Me.TabPageVendor.PerformLayout()
        Me.VendorSyncDirection.ResumeLayout(False)
        Me.VendorSyncDirection.PerformLayout()
        Me.TabPageEmployees.ResumeLayout(False)
        Me.TabPageEmployees.PerformLayout()
        Me.EmployeeSyncDirection.ResumeLayout(False)
        Me.EmployeeSyncDirection.PerformLayout()
        Me.TabPageCustomers.ResumeLayout(False)
        Me.TabPageCustomers.PerformLayout()
        Me.CustomerSyncDirection.ResumeLayout(False)
        Me.CustomerSyncDirection.PerformLayout()
        Me.AttributeTabControl.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ToolTip As System.Windows.Forms.ToolTip
    Friend WithEvents ToolStripButton2 As ToolStripButton
    Friend WithEvents Exitbtn As ToolStripButton
    Friend WithEvents SyncFromLabel As Label
    Friend WithEvents SyncToLabel As Label
    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents SelectAllCheckBox As CheckBox
    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents ckBox As DataGridViewCheckBoxColumn
    Friend WithEvents EntitiesSelectAll As CheckBox
    Friend WithEvents DataGridView2 As DataGridView
    Friend WithEvents SplitContainer2 As SplitContainer
    Friend WithEvents StatusWindow As TextBox
    Friend WithEvents StatusStrip As StatusStrip
    Friend WithEvents CurrentTime As ToolStripStatusLabel
    Friend WithEvents NextProcessingTime As ToolStripStatusLabel
    Friend WithEvents ToolStrip1 As ToolStrip
    Friend WithEvents settingbtn As ToolStripButton
    Friend WithEvents loginbtn As ToolStripButton
    Friend WithEvents btn_systemsync As ToolStripButton
    Friend WithEvents btn_relationships2 As ToolStripButton
    Friend WithEvents clearlogbtn As ToolStripButton
    Friend WithEvents ProgressBar1 As ProgressBar
    Friend WithEvents ShowEntitiesBtn As Button
    Friend WithEvents btnTransfer As Button
    Friend WithEvents SendEmailsButton As Button
    Friend WithEvents btnNewCustomer As Button
    Friend WithEvents btnNewEmployee As Button
    Friend WithEvents btnNewVendor As Button
    Friend WithEvents btn_relationships As ToolStripButton
    Friend WithEvents btnCreateJobItem As Button
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents btnServiceItemTreeView As Button
    Friend WithEvents TabPageExpenseReport As TabPage
    Friend WithEvents Label6 As Label
    Friend WithEvents refreshExpenseReport As Button
    Friend WithEvents expense_prevWeek As Button
    Friend WithEvents ToLabelExpenseReports As Label
    Friend WithEvents fromLabelExpenseReports As Label
    Friend WithEvents expenseReportEndDate As DateTimePicker
    Friend WithEvents expenseReportStartDate As DateTimePicker
    Friend WithEvents expense_nextWeek As Button
    Friend WithEvents expense_btn_currWeek As Button
    Friend WithEvents TabPageTimeTransfer As TabPage
    Friend WithEvents RefreshTimeTransfer As Button
    Friend WithEvents cbWageType As ComboBox
    Friend WithEvents lblWageType As Label
    Friend WithEvents time_prevWeek As Button
    Friend WithEvents lblUptoDate As Label
    Friend WithEvents lblFromDate As Label
    Friend WithEvents dpEndDate As DateTimePicker
    Friend WithEvents dpStartDate As DateTimePicker
    Friend WithEvents time_nextWeek As Button
    Friend WithEvents time_btn_currentweek As Button
    Friend WithEvents TabPageJobsItems As TabPage
    Friend WithEvents Label5 As Label
    Friend WithEvents RefreshJobsOrItems As Button
    Friend WithEvents JobItemSyncDirection As GroupBox
    Friend WithEvents TLtoQBJobItemRadioButton As RadioButton
    Friend WithEvents QBtoTLJobItemRadioButton As RadioButton
    Friend WithEvents TabPageVendor As TabPage
    Friend WithEvents Label4 As Label
    Friend WithEvents RefreshVendors As Button
    Friend WithEvents VendorSyncDirection As GroupBox
    Friend WithEvents TLtoQBVendorRadioButton As RadioButton
    Friend WithEvents QBtoTLVendorRadioButton As RadioButton
    Friend WithEvents TabPageEmployees As TabPage
    Friend WithEvents Label3 As Label
    Friend WithEvents RefreshEmployees As Button
    Friend WithEvents EmployeeSyncDirection As GroupBox
    Friend WithEvents TLtoQBEmployeeRadioButton As RadioButton
    Friend WithEvents QBtoTLEmployeeRadioButton As RadioButton
    Friend WithEvents TabPageCustomers As TabPage
    Friend WithEvents Label2 As Label
    Friend WithEvents RefreshCustomers As Button
    Friend WithEvents CustomerSyncDirection As GroupBox
    Friend WithEvents TLtoQBCustomerRadioButton As RadioButton
    Friend WithEvents QBtoTLCustomerRadioButton As RadioButton
    Friend WithEvents AttributeTabControl As TabControl
    Friend WithEvents ContextMenuStrip1 As ContextMenuStrip
    Friend WithEvents EditLink_MenuItem As ToolStripMenuItem
End Class
