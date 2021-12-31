<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class JobSubJobTreeView
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
        Me.TreeViewJobSubJob = New System.Windows.Forms.TreeView()
        Me.ButtonTreeViewClose = New System.Windows.Forms.Button()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.btnAddNode = New System.Windows.Forms.Button()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.TextBoxFullNameTL = New System.Windows.Forms.TextBox()
        Me.TextBoxTimeLiveName = New System.Windows.Forms.TextBox()
        Me.TextBoxTimeLiveID = New System.Windows.Forms.TextBox()
        Me.SuspendLayout()
        '
        'TreeViewJobSubJob
        '
        Me.TreeViewJobSubJob.Location = New System.Drawing.Point(12, 12)
        Me.TreeViewJobSubJob.Name = "TreeViewJobSubJob"
        Me.TreeViewJobSubJob.Size = New System.Drawing.Size(239, 336)
        Me.TreeViewJobSubJob.TabIndex = 0
        '
        'ButtonTreeViewClose
        '
        Me.ButtonTreeViewClose.Location = New System.Drawing.Point(403, 116)
        Me.ButtonTreeViewClose.Name = "ButtonTreeViewClose"
        Me.ButtonTreeViewClose.Size = New System.Drawing.Size(71, 23)
        Me.ButtonTreeViewClose.TabIndex = 44
        Me.ButtonTreeViewClose.Text = "Close"
        Me.ButtonTreeViewClose.UseVisualStyleBackColor = True
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(263, 80)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(57, 13)
        Me.Label8.TabIndex = 43
        Me.Label8.Text = "Full Name:"
        '
        'btnAddNode
        '
        Me.btnAddNode.Location = New System.Drawing.Point(309, 116)
        Me.btnAddNode.Name = "btnAddNode"
        Me.btnAddNode.Size = New System.Drawing.Size(71, 23)
        Me.btnAddNode.TabIndex = 41
        Me.btnAddNode.Text = "Add Node"
        Me.btnAddNode.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(282, 54)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(38, 13)
        Me.Label6.TabIndex = 40
        Me.Label6.Text = "Name:"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(299, 31)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(21, 13)
        Me.Label7.TabIndex = 38
        Me.Label7.Text = "ID:"
        '
        'TextBoxFullNameTL
        '
        Me.TextBoxFullNameTL.Location = New System.Drawing.Point(326, 80)
        Me.TextBoxFullNameTL.Name = "TextBoxFullNameTL"
        Me.TextBoxFullNameTL.Size = New System.Drawing.Size(256, 20)
        Me.TextBoxFullNameTL.TabIndex = 47
        '
        'TextBoxTimeLiveName
        '
        Me.TextBoxTimeLiveName.Location = New System.Drawing.Point(326, 54)
        Me.TextBoxTimeLiveName.Name = "TextBoxTimeLiveName"
        Me.TextBoxTimeLiveName.Size = New System.Drawing.Size(256, 20)
        Me.TextBoxTimeLiveName.TabIndex = 46
        '
        'TextBoxTimeLiveID
        '
        Me.TextBoxTimeLiveID.Location = New System.Drawing.Point(326, 28)
        Me.TextBoxTimeLiveID.Name = "TextBoxTimeLiveID"
        Me.TextBoxTimeLiveID.Size = New System.Drawing.Size(256, 20)
        Me.TextBoxTimeLiveID.TabIndex = 45
        '
        'JobSubJobTreeView
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(642, 390)
        Me.Controls.Add(Me.TextBoxFullNameTL)
        Me.Controls.Add(Me.TextBoxTimeLiveName)
        Me.Controls.Add(Me.TextBoxTimeLiveID)
        Me.Controls.Add(Me.ButtonTreeViewClose)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.btnAddNode)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.TreeViewJobSubJob)
        Me.Name = "JobSubJobTreeView"
        Me.Text = "JobSubJobTreeView"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents TreeViewJobSubJob As TreeView
    Friend WithEvents ButtonTreeViewClose As Button
    Friend WithEvents Label8 As Label
    Friend WithEvents btnAddNode As Button
    Friend WithEvents Label6 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents TextBoxFullNameTL As TextBox
    Friend WithEvents TextBoxTimeLiveName As TextBox
    Friend WithEvents TextBoxTimeLiveID As TextBox
End Class
