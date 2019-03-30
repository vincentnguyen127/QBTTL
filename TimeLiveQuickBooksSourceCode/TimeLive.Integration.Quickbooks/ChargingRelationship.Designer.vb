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
        Me.ChargingRelationshipsBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.QBTLIDsBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.QB_TL_IDs = New TimeLive.Quickbooks.Integrator.QB_TL_IDs()
        Me.JobsSubJobsBindingSource = New System.Windows.Forms.BindingSource(Me.components)
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
        Me.EmployeesBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.ItemsSubItemsBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.EmployeesTableAdapter = New TimeLive.Quickbooks.Integrator.QB_TL_IDsTableAdapters.EmployeesTableAdapter()
        Me.Jobs_SubJobsTableAdapter = New TimeLive.Quickbooks.Integrator.QB_TL_IDsTableAdapters.Jobs_SubJobsTableAdapter()
        Me.Items_SubItemsTableAdapter = New TimeLive.Quickbooks.Integrator.QB_TL_IDsTableAdapters.Items_SubItemsTableAdapter()
        Me.ChargingRelationshipsTableAdapter = New TimeLive.Quickbooks.Integrator.QB_TL_IDsTableAdapters.ChargingRelationshipsTableAdapter()
        Me.CustomersTableAdapter = New TimeLive.Quickbooks.Integrator.QB_TL_IDsTableAdapters.CustomersTableAdapter()
        Me.EmployeesBindingSource1 = New System.Windows.Forms.BindingSource(Me.components)
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
        Me.Label1 = New System.Windows.Forms.Label()
        CType(Me.ChargingRelationshipsBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.QBTLIDsBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.QB_TL_IDs, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.JobsSubJobsBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CustomersBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmployeesBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ItemsSubItemsBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmployeesBindingSource1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ChargingRelationshipsBindingSource1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ChargingRelationshipsBindingSource2, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.btn_Save.Location = New System.Drawing.Point(542, 374)
        Me.btn_Save.Name = "btn_Save"
        Me.btn_Save.Size = New System.Drawing.Size(75, 23)
        Me.btn_Save.TabIndex = 1
        Me.btn_Save.Text = "Save"
        Me.btn_Save.UseVisualStyleBackColor = True
        '
        'DataGridView1
        '
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
        Me.DataGridView1.Size = New System.Drawing.Size(704, 285)
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
        Me.btn_Delete.Location = New System.Drawing.Point(462, 374)
        Me.btn_Delete.Name = "btn_Delete"
        Me.btn_Delete.Size = New System.Drawing.Size(75, 23)
        Me.btn_Delete.TabIndex = 2
        Me.btn_Delete.Text = "Delete"
        Me.btn_Delete.UseVisualStyleBackColor = True
        '
        'btn_close
        '
        Me.btn_close.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn_close.Location = New System.Drawing.Point(622, 374)
        Me.btn_close.Name = "btn_close"
        Me.btn_close.Size = New System.Drawing.Size(75, 23)
        Me.btn_close.TabIndex = 3
        Me.btn_close.Text = "Close"
        Me.btn_close.UseVisualStyleBackColor = True
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
        'EmployeeFilterBox
        '
        Me.EmployeeFilterBox.BackColor = System.Drawing.SystemColors.Control
        Me.EmployeeFilterBox.FormattingEnabled = True
        Me.EmployeeFilterBox.Items.AddRange(New Object() {""})
        Me.EmployeeFilterBox.Location = New System.Drawing.Point(92, 27)
        Me.EmployeeFilterBox.Margin = New System.Windows.Forms.Padding(2)
        Me.EmployeeFilterBox.Name = "EmployeeFilterBox"
        Me.EmployeeFilterBox.Size = New System.Drawing.Size(95, 21)
        Me.EmployeeFilterBox.TabIndex = 4
        '
        'ChargingRelationshipsBindingSource1
        '
        Me.ChargingRelationshipsBindingSource1.DataMember = "ChargingRelationships"
        Me.ChargingRelationshipsBindingSource1.DataSource = Me.QBTLIDsBindingSource
        '
        'JobFilterBox
        '
        Me.JobFilterBox.BackColor = System.Drawing.SystemColors.Control
        Me.JobFilterBox.FormattingEnabled = True
        Me.JobFilterBox.Items.AddRange(New Object() {""})
        Me.JobFilterBox.Location = New System.Drawing.Point(191, 27)
        Me.JobFilterBox.Margin = New System.Windows.Forms.Padding(2)
        Me.JobFilterBox.Name = "JobFilterBox"
        Me.JobFilterBox.Size = New System.Drawing.Size(95, 21)
        Me.JobFilterBox.TabIndex = 6
        '
        'PayrollFilterBox
        '
        Me.PayrollFilterBox.BackColor = System.Drawing.SystemColors.Control
        Me.PayrollFilterBox.FormattingEnabled = True
        Me.PayrollFilterBox.Items.AddRange(New Object() {""})
        Me.PayrollFilterBox.Location = New System.Drawing.Point(290, 27)
        Me.PayrollFilterBox.Margin = New System.Windows.Forms.Padding(2)
        Me.PayrollFilterBox.Name = "PayrollFilterBox"
        Me.PayrollFilterBox.Size = New System.Drawing.Size(95, 21)
        Me.PayrollFilterBox.TabIndex = 6
        '
        'ItemFilterBox
        '
        Me.ItemFilterBox.BackColor = System.Drawing.SystemColors.Control
        Me.ItemFilterBox.FormattingEnabled = True
        Me.ItemFilterBox.Items.AddRange(New Object() {""})
        Me.ItemFilterBox.Location = New System.Drawing.Point(394, 27)
        Me.ItemFilterBox.Margin = New System.Windows.Forms.Padding(2)
        Me.ItemFilterBox.Name = "ItemFilterBox"
        Me.ItemFilterBox.Size = New System.Drawing.Size(95, 21)
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
        Me.EmployeeFilterLabel.Location = New System.Drawing.Point(89, 9)
        Me.EmployeeFilterLabel.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.EmployeeFilterLabel.Name = "EmployeeFilterLabel"
        Me.EmployeeFilterLabel.Size = New System.Drawing.Size(53, 13)
        Me.EmployeeFilterLabel.TabIndex = 8
        Me.EmployeeFilterLabel.Text = "Employee"
        '
        'JobFilterLabel
        '
        Me.JobFilterLabel.AutoSize = True
        Me.JobFilterLabel.Location = New System.Drawing.Point(188, 9)
        Me.JobFilterLabel.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.JobFilterLabel.Name = "JobFilterLabel"
        Me.JobFilterLabel.Size = New System.Drawing.Size(60, 13)
        Me.JobFilterLabel.TabIndex = 9
        Me.JobFilterLabel.Text = "Job Subjob"
        '
        'PayrollFilterLabel
        '
        Me.PayrollFilterLabel.AutoSize = True
        Me.PayrollFilterLabel.Location = New System.Drawing.Point(287, 9)
        Me.PayrollFilterLabel.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.PayrollFilterLabel.Name = "PayrollFilterLabel"
        Me.PayrollFilterLabel.Size = New System.Drawing.Size(61, 13)
        Me.PayrollFilterLabel.TabIndex = 10
        Me.PayrollFilterLabel.Text = "Payroll Item"
        '
        'ItemFilterLabel
        '
        Me.ItemFilterLabel.AutoSize = True
        Me.ItemFilterLabel.Location = New System.Drawing.Point(391, 9)
        Me.ItemFilterLabel.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.ItemFilterLabel.Name = "ItemFilterLabel"
        Me.ItemFilterLabel.Size = New System.Drawing.Size(69, 13)
        Me.ItemFilterLabel.TabIndex = 11
        Me.ItemFilterLabel.Text = "Item SubItem"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(26, 30)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(34, 13)
        Me.Label1.TabIndex = 12
        Me.Label1.Text = "Filters"
        '
        'ChargingRelationship
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(705, 403)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.ItemFilterLabel)
        Me.Controls.Add(Me.PayrollFilterLabel)
        Me.Controls.Add(Me.JobFilterLabel)
        Me.Controls.Add(Me.EmployeeFilterLabel)
        Me.Controls.Add(Me.ItemFilterBox)
        Me.Controls.Add(Me.PayrollFilterBox)
        Me.Controls.Add(Me.JobFilterBox)
        Me.Controls.Add(Me.EmployeeFilterBox)
        Me.Controls.Add(Me.btn_close)
        Me.Controls.Add(Me.btn_Delete)
        Me.Controls.Add(Me.btn_Save)
        Me.Controls.Add(Me.DataGridView1)
        Me.Name = "ChargingRelationship"
        Me.Text = "ChargingRelationship"
        CType(Me.ChargingRelationshipsBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.QBTLIDsBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.QB_TL_IDs, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.JobsSubJobsBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CustomersBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmployeesBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ItemsSubItemsBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.EmployeesBindingSource1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ChargingRelationshipsBindingSource1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ChargingRelationshipsBindingSource2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents QB_TL_IDs As QB_TL_IDs
    Friend WithEvents EmployeesBindingSource As BindingSource
    Friend WithEvents EmployeesTableAdapter As QB_TL_IDsTableAdapters.EmployeesTableAdapter
    Friend WithEvents QBTLIDsBindingSource As BindingSource
    Friend WithEvents JobsSubJobsBindingSource As BindingSource
    Friend WithEvents Jobs_SubJobsTableAdapter As QB_TL_IDsTableAdapters.Jobs_SubJobsTableAdapter
    Friend WithEvents ItemsSubItemsBindingSource As BindingSource
    Friend WithEvents Items_SubItemsTableAdapter As QB_TL_IDsTableAdapters.Items_SubItemsTableAdapter
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
    Friend WithEvents Label1 As Label
End Class
