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
        Me.CustomersBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.btn_Delete = New System.Windows.Forms.Button()
        Me.btn_close = New System.Windows.Forms.Button()
        Me.EmployeesBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.ItemsSubItemsBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.EmployeesTableAdapter = New TimeLive.Quickbooks.Integrator.QB_TL_IDsTableAdapters.EmployeesTableAdapter()
        Me.Items_SubItemsTableAdapter = New TimeLive.Quickbooks.Integrator.QB_TL_IDsTableAdapters.Items_SubItemsTableAdapter()
        Me.ChargingRelationshipsTableAdapter = New TimeLive.Quickbooks.Integrator.QB_TL_IDsTableAdapters.ChargingRelationshipsTableAdapter()
        Me.CustomersTableAdapter = New TimeLive.Quickbooks.Integrator.QB_TL_IDsTableAdapters.CustomersTableAdapter()
        Me.EmployeesBindingSource1 = New System.Windows.Forms.BindingSource(Me.components)
        Me.IDDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.EmployeeQBIDDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.JobSubJobQBIDDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PayrollItemQBIDDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ItemSubItemQBIDDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.ChargingRelationshipsBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.QBTLIDsBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.QB_TL_IDs, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.JobsSubJobsBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CustomersBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmployeesBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ItemsSubItemsBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.EmployeesBindingSource1, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.btn_Save.Location = New System.Drawing.Point(442, 270)
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
        Me.DataGridView1.Location = New System.Drawing.Point(0, 1)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.[Single]
        Me.DataGridView1.Size = New System.Drawing.Size(637, 249)
        Me.DataGridView1.TabIndex = 0
        '
        'CustomersBindingSource
        '
        Me.CustomersBindingSource.DataMember = "Customers"
        Me.CustomersBindingSource.DataSource = Me.QBTLIDsBindingSource
        '
        'btn_Delete
        '
        Me.btn_Delete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn_Delete.Location = New System.Drawing.Point(345, 270)
        Me.btn_Delete.Name = "btn_Delete"
        Me.btn_Delete.Size = New System.Drawing.Size(75, 23)
        Me.btn_Delete.TabIndex = 2
        Me.btn_Delete.Text = "Delete"
        Me.btn_Delete.UseVisualStyleBackColor = True
        '
        'btn_close
        '
        Me.btn_close.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn_close.Location = New System.Drawing.Point(534, 270)
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
        'ChargingRelationship
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(638, 305)
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
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents QB_TL_IDs As QB_TL_IDs
    Friend WithEvents EmployeesBindingSource As BindingSource
    Friend WithEvents EmployeesTableAdapter As QB_TL_IDsTableAdapters.EmployeesTableAdapter
    Friend WithEvents QBTLIDsBindingSource As BindingSource
    Friend WithEvents JobsSubJobsBindingSource As BindingSource
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
End Class
