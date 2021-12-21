<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class RelationshipTreeView
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
        Me.TreeViewRelationship = New System.Windows.Forms.TreeView()
        Me.LabelRelationshipTreeView = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'TreeViewRelationship
        '
        Me.TreeViewRelationship.Location = New System.Drawing.Point(32, 39)
        Me.TreeViewRelationship.Name = "TreeViewRelationship"
        Me.TreeViewRelationship.Size = New System.Drawing.Size(241, 379)
        Me.TreeViewRelationship.TabIndex = 0
        '
        'LabelRelationshipTreeView
        '
        Me.LabelRelationshipTreeView.AutoSize = True
        Me.LabelRelationshipTreeView.Location = New System.Drawing.Point(132, 9)
        Me.LabelRelationshipTreeView.Name = "LabelRelationshipTreeView"
        Me.LabelRelationshipTreeView.Size = New System.Drawing.Size(39, 13)
        Me.LabelRelationshipTreeView.TabIndex = 1
        Me.LabelRelationshipTreeView.Text = "Label1"
        '
        'RelationshipTreeView
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(336, 454)
        Me.Controls.Add(Me.LabelRelationshipTreeView)
        Me.Controls.Add(Me.TreeViewRelationship)
        Me.Name = "RelationshipTreeView"
        Me.Text = "RelationshipTreeView"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents TreeViewRelationship As TreeView
    Friend WithEvents LabelRelationshipTreeView As Label
End Class
