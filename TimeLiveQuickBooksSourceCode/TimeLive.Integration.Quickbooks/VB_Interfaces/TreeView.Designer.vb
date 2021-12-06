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
        Me.CustomerJobTreeView = New System.Windows.Forms.TreeView()
        Me.SuspendLayout()
        '
        'CustomerJobTreeView
        '
        Me.CustomerJobTreeView.Location = New System.Drawing.Point(30, 54)
        Me.CustomerJobTreeView.Name = "CustomerJobTreeView"
        Me.CustomerJobTreeView.Size = New System.Drawing.Size(623, 336)
        Me.CustomerJobTreeView.TabIndex = 0
        '
        'TreeView
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(756, 451)
        Me.Controls.Add(Me.CustomerJobTreeView)
        Me.Name = "TreeView"
        Me.Text = "TreeView"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents CustomerJobTreeView As Windows.Forms.TreeView
End Class
