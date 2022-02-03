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
        Me.btnOK = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'TreeViewRelationship
        '
        Me.TreeViewRelationship.Location = New System.Drawing.Point(1, 28)
        Me.TreeViewRelationship.Name = "TreeViewRelationship"
        Me.TreeViewRelationship.Size = New System.Drawing.Size(245, 247)
        Me.TreeViewRelationship.TabIndex = 0
        '
        'LabelRelationshipTreeView
        '
        Me.LabelRelationshipTreeView.AutoSize = True
        Me.LabelRelationshipTreeView.Location = New System.Drawing.Point(6, 12)
        Me.LabelRelationshipTreeView.Name = "LabelRelationshipTreeView"
        Me.LabelRelationshipTreeView.Size = New System.Drawing.Size(39, 13)
        Me.LabelRelationshipTreeView.TabIndex = 1
        Me.LabelRelationshipTreeView.Text = "Label1"
        '
        'btnOK
        '
        Me.btnOK.Location = New System.Drawing.Point(168, 281)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(78, 30)
        Me.btnOK.TabIndex = 8
        Me.btnOK.Text = "OK"
        Me.btnOK.UseVisualStyleBackColor = True
        '
        'RelationshipTreeView
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(248, 322)
        Me.Controls.Add(Me.btnOK)
        Me.Controls.Add(Me.LabelRelationshipTreeView)
        Me.Controls.Add(Me.TreeViewRelationship)
        Me.Location = New System.Drawing.Point(100, 100)
        Me.Name = "RelationshipTreeView"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Quickbooks View"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents TreeViewRelationship As TreeView
    Friend WithEvents LabelRelationshipTreeView As Label
    Friend WithEvents btnOK As Button
End Class
