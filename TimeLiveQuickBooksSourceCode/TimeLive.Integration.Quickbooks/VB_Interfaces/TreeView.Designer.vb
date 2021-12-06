<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class TreeView
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
        Me.CustomerJobQBTreeView = New System.Windows.Forms.TreeView()
        Me.TextBoxKey = New System.Windows.Forms.TextBox()
        Me.AccountExpenseEntryTableAdapter1 = New TimeLive.Quickbooks.Integrator.TimeLiveDataSetTableAdapters.AccountExpenseEntryTableAdapter()
        Me.txtKey = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TextBoxName = New System.Windows.Forms.TextBox()
        Me.CustomerJobTLTreeView = New System.Windows.Forms.TreeView()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'CustomerJobQBTreeView
        '
        Me.CustomerJobQBTreeView.Location = New System.Drawing.Point(30, 54)
        Me.CustomerJobQBTreeView.Name = "CustomerJobQBTreeView"
        Me.CustomerJobQBTreeView.Size = New System.Drawing.Size(259, 329)
        Me.CustomerJobQBTreeView.TabIndex = 0
        '
        'TextBoxKey
        '
        Me.TextBoxKey.Location = New System.Drawing.Point(358, 54)
        Me.TextBoxKey.Name = "TextBoxKey"
        Me.TextBoxKey.Size = New System.Drawing.Size(131, 20)
        Me.TextBoxKey.TabIndex = 1
        '
        'AccountExpenseEntryTableAdapter1
        '
        Me.AccountExpenseEntryTableAdapter1.ClearBeforeFill = True
        '
        'txtKey
        '
        Me.txtKey.AutoSize = True
        Me.txtKey.Location = New System.Drawing.Point(323, 57)
        Me.txtKey.Name = "txtKey"
        Me.txtKey.Size = New System.Drawing.Size(28, 13)
        Me.txtKey.TabIndex = 20
        Me.txtKey.Text = "Key:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(313, 83)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(38, 13)
        Me.Label1.TabIndex = 22
        Me.Label1.Text = "Name:"
        '
        'TextBoxName
        '
        Me.TextBoxName.Location = New System.Drawing.Point(358, 80)
        Me.TextBoxName.Name = "TextBoxName"
        Me.TextBoxName.Size = New System.Drawing.Size(131, 20)
        Me.TextBoxName.TabIndex = 21
        '
        'CustomerJobTLTreeView
        '
        Me.CustomerJobTLTreeView.Location = New System.Drawing.Point(663, 57)
        Me.CustomerJobTLTreeView.Name = "CustomerJobTLTreeView"
        Me.CustomerJobTLTreeView.Size = New System.Drawing.Size(259, 329)
        Me.CustomerJobTLTreeView.TabIndex = 23
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(129, 24)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(65, 13)
        Me.Label2.TabIndex = 24
        Me.Label2.Text = "QuickBooks"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(759, 24)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(50, 13)
        Me.Label3.TabIndex = 25
        Me.Label3.Text = "TimeLive"
        '
        'TreeView
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(982, 451)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.CustomerJobTLTreeView)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TextBoxName)
        Me.Controls.Add(Me.txtKey)
        Me.Controls.Add(Me.TextBoxKey)
        Me.Controls.Add(Me.CustomerJobQBTreeView)
        Me.Name = "TreeView"
        Me.Text = "TreeView"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents CustomerJobQBTreeView As Windows.Forms.TreeView
    Friend WithEvents TextBoxKey As TextBox
    Friend WithEvents AccountExpenseEntryTableAdapter1 As TimeLiveDataSetTableAdapters.AccountExpenseEntryTableAdapter
    Friend WithEvents txtKey As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents TextBoxName As TextBox
    Friend WithEvents CustomerJobTLTreeView As Windows.Forms.TreeView
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
End Class
