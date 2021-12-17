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
        Me.SuspendLayout()
        '
        'ServiceItemQbTreeView
        '
        Me.ServiceItemQbTreeView.Location = New System.Drawing.Point(260, 52)
        Me.ServiceItemQbTreeView.Name = "ServiceItemQbTreeView"
        Me.ServiceItemQbTreeView.Size = New System.Drawing.Size(336, 310)
        Me.ServiceItemQbTreeView.TabIndex = 0
        '
        'ServiceItemTreeView
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(800, 450)
        Me.Controls.Add(Me.ServiceItemQbTreeView)
        Me.Name = "ServiceItemTreeView"
        Me.Text = "ServiceItemTreeView"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents ServiceItemQbTreeView As TreeView
End Class
