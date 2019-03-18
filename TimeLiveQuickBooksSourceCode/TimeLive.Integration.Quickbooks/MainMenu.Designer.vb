<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MainMenu
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MainMenu))
        Me.btnQBEmployees = New System.Windows.Forms.Button()
        Me.btnQBCustomers = New System.Windows.Forms.Button()
        Me.btnQBJobOrItem = New System.Windows.Forms.Button()
        Me.btnQBTimeTracking = New System.Windows.Forms.Button()
        Me.btnTLClients = New System.Windows.Forms.Button()
        Me.btnTLEmployees = New System.Windows.Forms.Button()
        Me.btnQBExpenseTracking = New System.Windows.Forms.Button()
        Me.btnQBVendors = New System.Windows.Forms.Button()
        Me.btnTLProjectAndTask = New System.Windows.Forms.Button()
        Me.btnQBAccounts = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.btnTLVendors = New System.Windows.Forms.Button()
        Me.IntegratedUI_btn = New System.Windows.Forms.Button()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnQBEmployees
        '
        Me.btnQBEmployees.Location = New System.Drawing.Point(8, 16)
        Me.btnQBEmployees.Name = "btnQBEmployees"
        Me.btnQBEmployees.Size = New System.Drawing.Size(226, 36)
        Me.btnQBEmployees.TabIndex = 0
        Me.btnQBEmployees.Text = "TL Employees to QB Employees"
        Me.btnQBEmployees.UseVisualStyleBackColor = True
        '
        'btnQBCustomers
        '
        Me.btnQBCustomers.Location = New System.Drawing.Point(8, 88)
        Me.btnQBCustomers.Name = "btnQBCustomers"
        Me.btnQBCustomers.Size = New System.Drawing.Size(226, 36)
        Me.btnQBCustomers.TabIndex = 1
        Me.btnQBCustomers.Text = "TL Clients to QB Customers"
        Me.btnQBCustomers.UseVisualStyleBackColor = True
        '
        'btnQBJobOrItem
        '
        Me.btnQBJobOrItem.Location = New System.Drawing.Point(8, 124)
        Me.btnQBJobOrItem.Name = "btnQBJobOrItem"
        Me.btnQBJobOrItem.Size = New System.Drawing.Size(226, 36)
        Me.btnQBJobOrItem.TabIndex = 2
        Me.btnQBJobOrItem.Text = "TL Projects/Tasks to QB Jobs/Items"
        Me.btnQBJobOrItem.UseVisualStyleBackColor = True
        '
        'btnQBTimeTracking
        '
        Me.btnQBTimeTracking.Location = New System.Drawing.Point(8, 196)
        Me.btnQBTimeTracking.Name = "btnQBTimeTracking"
        Me.btnQBTimeTracking.Size = New System.Drawing.Size(226, 36)
        Me.btnQBTimeTracking.TabIndex = 3
        Me.btnQBTimeTracking.Text = "TL Time Entries to QB Time Entries"
        Me.btnQBTimeTracking.UseVisualStyleBackColor = True
        '
        'btnTLClients
        '
        Me.btnTLClients.Location = New System.Drawing.Point(8, 51)
        Me.btnTLClients.Name = "btnTLClients"
        Me.btnTLClients.Size = New System.Drawing.Size(226, 36)
        Me.btnTLClients.TabIndex = 7
        Me.btnTLClients.Text = "QB Customers to TL Clients"
        Me.btnTLClients.UseVisualStyleBackColor = True
        '
        'btnTLEmployees
        '
        Me.btnTLEmployees.Location = New System.Drawing.Point(8, 16)
        Me.btnTLEmployees.Name = "btnTLEmployees"
        Me.btnTLEmployees.Size = New System.Drawing.Size(226, 36)
        Me.btnTLEmployees.TabIndex = 6
        Me.btnTLEmployees.Text = "QB Employees to TL Employees"
        Me.btnTLEmployees.UseVisualStyleBackColor = True
        '
        'btnQBExpenseTracking
        '
        Me.btnQBExpenseTracking.Location = New System.Drawing.Point(8, 232)
        Me.btnQBExpenseTracking.Name = "btnQBExpenseTracking"
        Me.btnQBExpenseTracking.Size = New System.Drawing.Size(226, 36)
        Me.btnQBExpenseTracking.TabIndex = 5
        Me.btnQBExpenseTracking.Text = "TL Expense Entries to QB Vendor Bills"
        Me.btnQBExpenseTracking.UseVisualStyleBackColor = True
        '
        'btnQBVendors
        '
        Me.btnQBVendors.Location = New System.Drawing.Point(8, 52)
        Me.btnQBVendors.Name = "btnQBVendors"
        Me.btnQBVendors.Size = New System.Drawing.Size(226, 36)
        Me.btnQBVendors.TabIndex = 4
        Me.btnQBVendors.Text = "TL Vendors to QB as Vendors"
        Me.btnQBVendors.UseVisualStyleBackColor = True
        '
        'btnTLProjectAndTask
        '
        Me.btnTLProjectAndTask.Location = New System.Drawing.Point(8, 87)
        Me.btnTLProjectAndTask.Name = "btnTLProjectAndTask"
        Me.btnTLProjectAndTask.Size = New System.Drawing.Size(226, 36)
        Me.btnTLProjectAndTask.TabIndex = 8
        Me.btnTLProjectAndTask.Text = "QB Jobs/Items to TL Projects/Tasks"
        Me.btnTLProjectAndTask.UseVisualStyleBackColor = True
        '
        'btnQBAccounts
        '
        Me.btnQBAccounts.Location = New System.Drawing.Point(8, 160)
        Me.btnQBAccounts.Name = "btnQBAccounts"
        Me.btnQBAccounts.Size = New System.Drawing.Size(226, 36)
        Me.btnQBAccounts.TabIndex = 11
        Me.btnQBAccounts.Text = "TL Expense Codes to QB Accounts"
        Me.btnQBAccounts.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.btnQBEmployees)
        Me.GroupBox1.Controls.Add(Me.btnQBAccounts)
        Me.GroupBox1.Controls.Add(Me.btnQBVendors)
        Me.GroupBox1.Controls.Add(Me.btnQBCustomers)
        Me.GroupBox1.Controls.Add(Me.btnQBJobOrItem)
        Me.GroupBox1.Controls.Add(Me.btnQBTimeTracking)
        Me.GroupBox1.Controls.Add(Me.btnQBExpenseTracking)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 197)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(243, 272)
        Me.GroupBox1.TabIndex = 12
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Transfer TimeLive to Quickbooks"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.btnTLEmployees)
        Me.GroupBox2.Controls.Add(Me.btnTLClients)
        Me.GroupBox2.Controls.Add(Me.btnTLProjectAndTask)
        Me.GroupBox2.Controls.Add(Me.btnTLVendors)
        Me.GroupBox2.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(243, 179)
        Me.GroupBox2.TabIndex = 13
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Transfer QuickBooks to TimeLive"
        '
        'btnTLVendors
        '
        Me.btnTLVendors.Location = New System.Drawing.Point(8, 124)
        Me.btnTLVendors.Name = "btnTLVendors"
        Me.btnTLVendors.Size = New System.Drawing.Size(226, 36)
        Me.btnTLVendors.TabIndex = 14
        Me.btnTLVendors.Text = "QB Contractor to TL Employees"
        Me.btnTLVendors.UseVisualStyleBackColor = True
        '
        'IntegratedUI_btn
        '
        Me.IntegratedUI_btn.Location = New System.Drawing.Point(20, 462)
        Me.IntegratedUI_btn.Name = "IntegratedUI_btn"
        Me.IntegratedUI_btn.Size = New System.Drawing.Size(226, 33)
        Me.IntegratedUI_btn.TabIndex = 14
        Me.IntegratedUI_btn.Text = "IntegratedUI"
        Me.IntegratedUI_btn.UseVisualStyleBackColor = True
        '
        'MainMenu
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(265, 552)
        Me.Controls.Add(Me.IntegratedUI_btn)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "MainMenu"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Transfer Options"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnQBEmployees As System.Windows.Forms.Button
    Friend WithEvents btnQBCustomers As System.Windows.Forms.Button
    Friend WithEvents btnQBJobOrItem As System.Windows.Forms.Button
    Friend WithEvents btnQBTimeTracking As System.Windows.Forms.Button
    Friend WithEvents btnTLClients As System.Windows.Forms.Button
    Friend WithEvents btnTLEmployees As System.Windows.Forms.Button
    Friend WithEvents btnQBExpenseTracking As System.Windows.Forms.Button
    Friend WithEvents btnQBVendors As System.Windows.Forms.Button
    Friend WithEvents btnTLProjectAndTask As System.Windows.Forms.Button
    Friend WithEvents btnQBAccounts As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents btnTLVendors As System.Windows.Forms.Button
    Friend WithEvents IntegratedUI_btn As Button
End Class
