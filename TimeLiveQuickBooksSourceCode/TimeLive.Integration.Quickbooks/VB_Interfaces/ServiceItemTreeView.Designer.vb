<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ServiceItemTreeView
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
        Me.ServiceItemQbTreeView = New System.Windows.Forms.TreeView()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.PayRollQbTreeView = New System.Windows.Forms.TreeView()
        Me.SuspendLayout()
        '
        'ServiceItemQbTreeView
        '
        Me.ServiceItemQbTreeView.Location = New System.Drawing.Point(393, 85)
        Me.ServiceItemQbTreeView.Name = "ServiceItemQbTreeView"
        Me.ServiceItemQbTreeView.Size = New System.Drawing.Size(336, 310)
        Me.ServiceItemQbTreeView.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(405, 51)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(122, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Service Items Tree View"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 51)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(117, 13)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Payroll Items Tree View"
        '
        'PayRollQbTreeView
        '
        Me.PayRollQbTreeView.Location = New System.Drawing.Point(12, 85)
        Me.PayRollQbTreeView.Name = "PayRollQbTreeView"
        Me.PayRollQbTreeView.Size = New System.Drawing.Size(336, 310)
        Me.PayRollQbTreeView.TabIndex = 2
        '
        'ServiceItemTreeView
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(752, 435)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.PayRollQbTreeView)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.ServiceItemQbTreeView)
        Me.Name = "ServiceItemTreeView"
        Me.Text = "ServiceItemTreeView"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents ServiceItemQbTreeView As TreeView
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents PayRollQbTreeView As TreeView
End Class
