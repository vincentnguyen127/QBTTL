<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ChargingRelationship
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.ChargingRelationshipsBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.QBTLIDsBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.QB_TL_IDs = New TimeLive.Quickbooks.Integrator.QB_TL_IDs()
        Me.btn_Save = New System.Windows.Forms.Button()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.IDDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.EmployeeQBIDDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.JobSubJobQBIDDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PayrollItemQBIDDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ItemSubItemQBIDDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CustomersBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.btn_Delete = New System.Windows.Forms.Button()
        Me.btn_close = New System.Windows.Forms.Button()
        Me.EmployeeFilterBox = New System.Windows.Forms.ComboBox()
        Me.ChargingRelationshipsBindingSource1 = New System.Windows.Forms.BindingSource(Me.components)
        Me.JobFilterBox = New System.Windows.Forms.ComboBox()
        Me.PayrollFilterBox = New System.Windows.Forms.ComboBox()
        Me.ItemFilterBox = New System.Windows.Forms.ComboBox()
        Me.ChargingRelationshipsBindingSource2 = New System.Windows.Forms.BindingSource(Me.components)
        Me.EmployeeFilterLabel = New System.Windows.Forms.Label()
        Me.JobFilterLabel = New System.Windows.Forms.Label()
        Me.PayrollFilterLabel = New System.Windows.Forms.Label()
        Me.ItemFilterLabel = New System.Windows.Forms.Label()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.SplitContainer3 = New System.Windows.Forms.SplitContainer()
        Me.EmployeesBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.ItemsSubItemsBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.EmployeesTableAdapter = New TimeLive.Quickbooks.Integrator.QB_TL_IDsTableAdapters.EmployeesTableAdapter()
        Me.Jobs_SubJobsTableAdapter = New TimeLive.Quickbooks.Integrator.QB_TL_IDsTableAdapters.Jobs_SubJobsTableAdapter()
        Me.Items_SubItemsTableAdapter = New TimeLive.Quickbooks.Integrator.QB_TL_IDsTableAdapters.Items_SubItemsTableAdapter()
        Me.VendorsTableAdapter = New TimeLive.Quickbooks.Integrator.QB_TL_IDsTableAdapters.VendorsTableAdapter()
        Me.ChargingRelationshipsTableAdapter = New TimeLive.Quickbooks.Integrator.QB_TL_IDsTableAdapters.ChargingRelationshipsTableAdapter()
        Me.CustomersTableAdapter = New TimeLive.Quickbooks.Integrator.QB_TL_IDsTableAdapters.CustomersTableAdapter()
        Me.EmployeesBindingSource1 = New System.Windows.Forms.BindingSource(Me.components)
        Me.JobsSubJobsBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        CType(Me.ChargingRelationshipsBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.QBTLIDsBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.QB_TL_IDs, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CustomersBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ChargingRelationshipsBindingSource1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ChargingRelationshipsBindingSource2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.SplitContainer3, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer3.Panel1.SuspendLayout()
        Me.SplitContainer3.Panel2.SuspendLayout()
        Me.SplitContainer3.SuspendLayout()
        CType(Me.EmployeesBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ItemsSubItemsBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmployeesBindingSource1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.JobsSubJobsBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ChargingRelationshipsBindingSource
        '
        Me.ChargingRelationshipsBindingSource.DataMember = "ChargingRelationships"
        Me.ChargingRelationshipsBindingSource.DataSource = Me.QBTLIDsBindingSource
        '
        'QBTLIDsBindingSource
        '
        Me.QBTLIDsBindingSource.DataSource = Me.QB_TL_IDs
        Me.QBTLIDsBindingSource.Position = 0
        '
        'QB_TL_IDs
        '
        Me.QB_TL_IDs.DataSetName = "QB_TL_IDs"
        Me.QB_TL_IDs.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'btn_Save
        '
        Me.btn_Save.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn_Save.Location = New System.Drawing.Point(621, 374)
        Me.btn_Save.Name = "btn_Save"
        Me.btn_Save.Size = New System.Drawing.Size(75, 23)
        Me.btn_Save.TabIndex = 1
        Me.btn_Save.Text = "Save"
        Me.btn_Save.UseVisualStyleBackColor = True
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToResizeRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        DataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.White
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.Black
        Me.DataGridView1.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.DataGridView1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DataGridView1.AutoGenerateColumns = False
        Me.DataGridView1.BackgroundColor = System.Drawing.SystemColors.ActiveBorder
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.IDDataGridViewTextBoxColumn, Me.EmployeeQBIDDataGridViewTextBoxColumn, Me.JobSubJobQBIDDataGridViewTextBoxColumn, Me.PayrollItemQBIDDataGridViewTextBoxColumn, Me.ItemSubItemQBIDDataGridViewTextBoxColumn})
        Me.DataGridView1.Cursor = System.Windows.Forms.Cursors.Default
        Me.DataGridView1.DataSource = Me.ChargingRelationshipsBindingSource
        Me.DataGridView1.Location = New System.Drawing.Point(0, 63)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.[Single]
        Me.DataGridView1.Size = New System.Drawing.Size(783, 285)
        Me.DataGridView1.TabIndex = 0
        '
        'IDDataGridViewTextBoxColumn
        '
        Me.IDDataGridViewTextBoxColumn.DataPropertyName = "ID"
        Me.IDDataGridViewTextBoxColumn.HeaderText = "ID"
        Me.IDDataGridViewTextBoxColumn.Name = "IDDataGridViewTextBoxColumn"
        Me.IDDataGridViewTextBoxColumn.ReadOnly = True
        Me.IDDataGridViewTextBoxColumn.Width = 50
        '
        'EmployeeQBIDDataGridViewTextBoxColumn
        '
        Me.EmployeeQBIDDataGridViewTextBoxColumn.DataPropertyName = "EmployeeQB_ID"
        Me.EmployeeQBIDDataGridViewTextBoxColumn.HeaderText = "Employee"
        Me.EmployeeQBIDDataGridViewTextBoxColumn.Name = "EmployeeQBIDDataGridViewTextBoxColumn"
        Me.EmployeeQBIDDataGridViewTextBoxColumn.ReadOnly = True
        Me.EmployeeQBIDDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        '
        'JobSubJobQBIDDataGridViewTextBoxColumn
        '
        Me.JobSubJobQBIDDataGridViewTextBoxColumn.DataPropertyName = "JobSubJobQB_ID"
        Me.JobSubJobQBIDDataGridViewTextBoxColumn.HeaderText = "JobSubJobQB_ID"
        Me.JobSubJobQBIDDataGridViewTextBoxColumn.Name = "JobSubJobQBIDDataGridViewTextBoxColumn"
        Me.JobSubJobQBIDDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        '
        'PayrollItemQBIDDataGridViewTextBoxColumn
        '
        Me.PayrollItemQBIDDataGridViewTextBoxColumn.DataPropertyName = "PayrollItemQB_ID"
        Me.PayrollItemQBIDDataGridViewTextBoxColumn.HeaderText = "PayrollItemQB_ID"
        Me.PayrollItemQBIDDataGridViewTextBoxColumn.Name = "PayrollItemQBIDDataGridViewTextBoxColumn"
        Me.PayrollItemQBIDDataGridViewTextBoxColumn.ReadOnly = True
        '
        'ItemSubItemQBIDDataGridViewTextBoxColumn
        '
        Me.ItemSubItemQBIDDataGridViewTextBoxColumn.DataPropertyName = "ItemSubItemQB_ID"
        Me.ItemSubItemQBIDDataGridViewTextBoxColumn.HeaderText = "ItemSubItemQB_ID"
        Me.ItemSubItemQBIDDataGridViewTextBoxColumn.Name = "ItemSubItemQBIDDataGridViewTextBoxColumn"
        '
        'CustomersBindingSource
        '
        Me.CustomersBindingSource.DataMember = "Customers"
        Me.CustomersBindingSource.DataSource = Me.QBTLIDsBindingSource
        '
        'btn_Delete
        '
        Me.btn_Delete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn_Delete.Location = New System.Drawing.Point(541, 374)
        Me.btn_Delete.Name = "btn_Delete"
        Me.btn_Delete.Size = New System.Drawing.Size(75, 23)
        Me.btn_Delete.TabIndex = 2
        Me.btn_Delete.Text = "Delete"
        Me.btn_Delete.UseVisualStyleBackColor = True
        '
        'btn_close
        '
        Me.btn_close.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn_close.Location = New System.Drawing.Point(701, 374)
        Me.btn_close.Name = "btn_close"
        Me.btn_close.Size = New System.Drawing.Size(75, 23)
        Me.btn_close.TabIndex = 3
        Me.btn_close.Text = "Close"
        Me.btn_close.UseVisualStyleBackColor = True
        '
        'EmployeeFilterBox
        '
        Me.EmployeeFilterBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.EmployeeFilterBox.BackColor = System.Drawing.SystemColors.Control
        Me.EmployeeFilterBox.FormattingEnabled = True
        Me.EmployeeFilterBox.Items.AddRange(New Object() {""})
        Me.EmployeeFilterBox.Location = New System.Drawing.Point(2, 17)
        Me.EmployeeFilterBox.Margin = New System.Windows.Forms.Padding(2)
        Me.EmployeeFilterBox.Name = "EmployeeFilterBox"
        Me.EmployeeFilterBox.Size = New System.Drawing.Size(182, 21)
        Me.EmployeeFilterBox.TabIndex = 4
        '
        'ChargingRelationshipsBindingSource1
        '
        Me.ChargingRelationshipsBindingSource1.DataMember = "ChargingRelationships"
        Me.ChargingRelationshipsBindingSource1.DataSource = Me.QBTLIDsBindingSource
        '
        'JobFilterBox
        '
        Me.JobFilterBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.JobFilterBox.BackColor = System.Drawing.SystemColors.Control
        Me.JobFilterBox.FormattingEnabled = True
        Me.JobFilterBox.Items.AddRange(New Object() {""})
        Me.JobFilterBox.Location = New System.Drawing.Point(3, 17)
        Me.JobFilterBox.Margin = New System.Windows.Forms.Padding(2)
        Me.JobFilterBox.Name = "JobFilterBox"
        Me.JobFilterBox.Size = New System.Drawing.Size(188, 21)
        Me.JobFilterBox.TabIndex = 6
        '
        'PayrollFilterBox
        '
        Me.PayrollFilterBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PayrollFilterBox.BackColor = System.Drawing.SystemColors.Control
        Me.PayrollFilterBox.DropDownWidth = 180
        Me.PayrollFilterBox.FormattingEnabled = True
        Me.PayrollFilterBox.Items.AddRange(New Object() {""})
        Me.PayrollFilterBox.Location = New System.Drawing.Point(2, 17)
        Me.PayrollFilterBox.Margin = New System.Windows.Forms.Padding(2)
        Me.PayrollFilterBox.Name = "PayrollFilterBox"
        Me.PayrollFilterBox.Size = New System.Drawing.Size(180, 21)
        Me.PayrollFilterBox.TabIndex = 6
        '
        'ItemFilterBox
        '
        Me.ItemFilterBox.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ItemFilterBox.BackColor = System.Drawing.SystemColors.Control
        Me.ItemFilterBox.DropDownWidth = 200
        Me.ItemFilterBox.FormattingEnabled = True
        Me.ItemFilterBox.Items.AddRange(New Object() {""})
        Me.ItemFilterBox.Location = New System.Drawing.Point(2, 17)
        Me.ItemFilterBox.Margin = New System.Windows.Forms.Padding(2)
        Me.ItemFilterBox.Name = "ItemFilterBox"
        Me.ItemFilterBox.Size = New System.Drawing.Size(209, 21)
        Me.ItemFilterBox.TabIndex = 7
        '
        'ChargingRelationshipsBindingSource2
        '
        Me.ChargingRelationshipsBindingSource2.DataMember = "ChargingRelationships"
        Me.ChargingRelationshipsBindingSource2.DataSource = Me.QBTLIDsBindingSource
        '
        'EmployeeFilterLabel
        '
        Me.EmployeeFilterLabel.AutoSize = True
        Me.EmployeeFilterLabel.Location = New System.Drawing.Point(2, 2)
        Me.EmployeeFilterLabel.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.EmployeeFilterLabel.Name = "EmployeeFilterLabel"
        Me.EmployeeFilterLabel.Size = New System.Drawing.Size(53, 13)
        Me.EmployeeFilterLabel.TabIndex = 8
        Me.EmployeeFilterLabel.Text = "Employee"
        '
        'JobFilterLabel
        '
        Me.JobFilterLabel.AutoSize = True
        Me.JobFilterLabel.Location = New System.Drawing.Point(2, 2)
        Me.JobFilterLabel.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.JobFilterLabel.Name = "JobFilterLabel"
        Me.JobFilterLabel.Size = New System.Drawing.Size(60, 13)
        Me.JobFilterLabel.TabIndex = 9
        Me.JobFilterLabel.Text = "Job Subjob"
        '
        'PayrollFilterLabel
        '
        Me.PayrollFilterLabel.AutoSize = True
        Me.PayrollFilterLabel.Location = New System.Drawing.Point(2, 2)
        Me.PayrollFilterLabel.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.PayrollFilterLabel.Name = "PayrollFilterLabel"
        Me.PayrollFilterLabel.Size = New System.Drawing.Size(61, 13)
        Me.PayrollFilterLabel.TabIndex = 10
        Me.PayrollFilterLabel.Text = "Payroll Item"
        '
        'ItemFilterLabel
        '
        Me.ItemFilterLabel.AutoSize = True
        Me.ItemFilterLabel.Location = New System.Drawing.Point(2, 2)
        Me.ItemFilterLabel.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.ItemFilterLabel.Name = "ItemFilterLabel"
        Me.ItemFilterLabel.Size = New System.Drawing.Size(69, 13)
        Me.ItemFilterLabel.TabIndex = 11
        Me.ItemFilterLabel.Text = "Item SubItem"
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 7)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.SplitContainer2)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.SplitContainer3)
        Me.SplitContainer1.Size = New System.Drawing.Size(783, 50)
        Me.SplitContainer1.SplitterDistance = 382
        Me.SplitContainer1.TabIndex = 12
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer2.Name = "SplitContainer2"
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.EmployeeFilterBox)
        Me.SplitContainer2.Panel1.Controls.Add(Me.EmployeeFilterLabel)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.JobFilterBox)
        Me.SplitContainer2.Panel2.Controls.Add(Me.JobFilterLabel)
        Me.SplitContainer2.Size = New System.Drawing.Size(382, 50)
        Me.SplitContainer2.SplitterDistance = 186
        Me.SplitContainer2.TabIndex = 0
        '
        'SplitContainer3
        '
        Me.SplitContainer3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer3.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer3.Name = "SplitContainer3"
        '
        'SplitContainer3.Panel1
        '
        Me.SplitContainer3.Panel1.Controls.Add(Me.PayrollFilterBox)
        Me.SplitContainer3.Panel1.Controls.Add(Me.PayrollFilterLabel)
        '
        'SplitContainer3.Panel2
        '
        Me.SplitContainer3.Panel2.Controls.Add(Me.ItemFilterBox)
        Me.SplitContainer3.Panel2.Controls.Add(Me.ItemFilterLabel)
        Me.SplitContainer3.Size = New System.Drawing.Size(397, 50)
        Me.SplitContainer3.SplitterDistance = 182
        Me.SplitContainer3.TabIndex = 0
        '
        'EmployeesBindingSource
        '
        Me.EmployeesBindingSource.DataMember = "Employees"
        Me.EmployeesBindingSource.DataSource = Me.QB_TL_IDs
        '
        'ItemsSubItemsBindingSource
        '
        Me.ItemsSubItemsBindingSource.DataMember = "Items_SubItems"
        Me.ItemsSubItemsBindingSource.DataSource = Me.QB_TL_IDs
        '
        'EmployeesTableAdapter
        '
        Me.EmployeesTableAdapter.ClearBeforeFill = True
        '
        'Jobs_SubJobsTableAdapter
        '
        Me.Jobs_SubJobsTableAdapter.ClearBeforeFill = True
        '
        'Items_SubItemsTableAdapter
        '
        Me.Items_SubItemsTableAdapter.ClearBeforeFill = True
        '
        'VendorsTableAdapter
        '
        Me.VendorsTableAdapter.ClearBeforeFill = True
        '
        'ChargingRelationshipsTableAdapter
        '
        Me.ChargingRelationshipsTableAdapter.ClearBeforeFill = True
        '
        'CustomersTableAdapter
        '
        Me.CustomersTableAdapter.ClearBeforeFill = True
        '
        'EmployeesBindingSource1
        '
        Me.EmployeesBindingSource1.DataMember = "Employees"
        Me.EmployeesBindingSource1.DataSource = Me.QB_TL_IDs
        '
        'JobsSubJobsBindingSource
        '
        '
        'ChargingRelationship
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(784, 403)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.btn_close)
        Me.Controls.Add(Me.btn_Delete)
        Me.Controls.Add(Me.btn_Save)
        Me.Controls.Add(Me.DataGridView1)
        Me.Name = "ChargingRelationship"
        Me.Text = "ChargingRelationship"
        CType(Me.ChargingRelationshipsBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.QBTLIDsBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.QB_TL_IDs, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CustomersBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ChargingRelationshipsBindingSource1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ChargingRelationshipsBindingSource2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.PerformLayout()
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.Panel2.PerformLayout()
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer2.ResumeLayout(False)
        Me.SplitContainer3.Panel1.ResumeLayout(False)
        Me.SplitContainer3.Panel1.PerformLayout()
        Me.SplitContainer3.Panel2.ResumeLayout(False)
        Me.SplitContainer3.Panel2.PerformLayout()
        CType(Me.SplitContainer3, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer3.ResumeLayout(False)
        CType(Me.EmployeesBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ItemsSubItemsBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmployeesBindingSource1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.JobsSubJobsBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents QB_TL_IDs As QB_TL_IDs
    Friend WithEvents EmployeesBindingSource As BindingSource
    Friend WithEvents EmployeesTableAdapter As QB_TL_IDsTableAdapters.EmployeesTableAdapter
    Friend WithEvents QBTLIDsBindingSource As BindingSource
    Friend WithEvents JobsSubJobsBindingSource As BindingSource
    Friend WithEvents Jobs_SubJobsTableAdapter As QB_TL_IDsTableAdapters.Jobs_SubJobsTableAdapter
    Friend WithEvents ItemsSubItemsBindingSource As BindingSource
    Friend WithEvents Items_SubItemsTableAdapter As QB_TL_IDsTableAdapters.Items_SubItemsTableAdapter
    Friend WithEvents VendorsBindingSource As BindingSource
    Friend WithEvents VendorsTableAdapter As QB_TL_IDsTableAdapters.VendorsTableAdapter
    Friend WithEvents btn_Save As Button
    Friend WithEvents ChargingRelationshipsBindingSource As BindingSource
    Friend WithEvents ChargingRelationshipsTableAdapter As QB_TL_IDsTableAdapters.ChargingRelationshipsTableAdapter
    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents CustomersBindingSource As BindingSource
    Friend WithEvents CustomersTableAdapter As QB_TL_IDsTableAdapters.CustomersTableAdapter
    Friend WithEvents EmployeesBindingSource1 As BindingSource
    Friend WithEvents btn_Delete As Button
    Friend WithEvents btn_close As Button
    Friend WithEvents IDDataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents EmployeeQBIDDataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents JobSubJobQBIDDataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents PayrollItemQBIDDataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents ItemSubItemQBIDDataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents EmployeeFilterBox As ComboBox
    Friend WithEvents ChargingRelationshipsBindingSource1 As BindingSource
    Friend WithEvents JobFilterBox As ComboBox
    Friend WithEvents PayrollFilterBox As ComboBox
    Friend WithEvents ItemFilterBox As ComboBox
    Friend WithEvents ChargingRelationshipsBindingSource2 As BindingSource
    Friend WithEvents EmployeeFilterLabel As Label
    Friend WithEvents JobFilterLabel As Label
    Friend WithEvents PayrollFilterLabel As Label
    Friend WithEvents ItemFilterLabel As Label
    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents SplitContainer2 As SplitContainer
    Friend WithEvents SplitContainer3 As SplitContainer
End Class
