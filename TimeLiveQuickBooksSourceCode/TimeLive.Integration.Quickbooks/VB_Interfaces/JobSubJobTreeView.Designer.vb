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
        Me.RadioButtonTimeLive = New System.Windows.Forms.RadioButton()
        Me.RadioButtonQuickBooks = New System.Windows.Forms.RadioButton()
        Me.RadioButtonBothQBTL = New System.Windows.Forms.RadioButton()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'TreeViewJobSubJob
        '
        Me.TreeViewJobSubJob.Location = New System.Drawing.Point(12, 37)
        Me.TreeViewJobSubJob.Name = "TreeViewJobSubJob"
        Me.TreeViewJobSubJob.Size = New System.Drawing.Size(239, 336)
        Me.TreeViewJobSubJob.TabIndex = 0
        '
        'ButtonTreeViewClose
        '
        Me.ButtonTreeViewClose.Location = New System.Drawing.Point(404, 173)
        Me.ButtonTreeViewClose.Name = "ButtonTreeViewClose"
        Me.ButtonTreeViewClose.Size = New System.Drawing.Size(71, 23)
        Me.ButtonTreeViewClose.TabIndex = 44
        Me.ButtonTreeViewClose.Text = "Close"
        Me.ButtonTreeViewClose.UseVisualStyleBackColor = True
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(264, 137)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(57, 13)
        Me.Label8.TabIndex = 43
        Me.Label8.Text = "Full Name:"
        '
        'btnAddNode
        '
        Me.btnAddNode.Location = New System.Drawing.Point(310, 173)
        Me.btnAddNode.Name = "btnAddNode"
        Me.btnAddNode.Size = New System.Drawing.Size(71, 23)
        Me.btnAddNode.TabIndex = 41
        Me.btnAddNode.Text = "Add Node"
        Me.btnAddNode.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(283, 111)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(38, 13)
        Me.Label6.TabIndex = 40
        Me.Label6.Text = "Name:"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(300, 88)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(21, 13)
        Me.Label7.TabIndex = 38
        Me.Label7.Text = "ID:"
        '
        'TextBoxFullNameTL
        '
        Me.TextBoxFullNameTL.Location = New System.Drawing.Point(327, 137)
        Me.TextBoxFullNameTL.Name = "TextBoxFullNameTL"
        Me.TextBoxFullNameTL.Size = New System.Drawing.Size(256, 20)
        Me.TextBoxFullNameTL.TabIndex = 47
        '
        'TextBoxTimeLiveName
        '
        Me.TextBoxTimeLiveName.Location = New System.Drawing.Point(327, 111)
        Me.TextBoxTimeLiveName.Name = "TextBoxTimeLiveName"
        Me.TextBoxTimeLiveName.Size = New System.Drawing.Size(256, 20)
        Me.TextBoxTimeLiveName.TabIndex = 46
        '
        'TextBoxTimeLiveID
        '
        Me.TextBoxTimeLiveID.Location = New System.Drawing.Point(327, 85)
        Me.TextBoxTimeLiveID.Name = "TextBoxTimeLiveID"
        Me.TextBoxTimeLiveID.Size = New System.Drawing.Size(256, 20)
        Me.TextBoxTimeLiveID.TabIndex = 45
        '
        'RadioButtonTimeLive
        '
        Me.RadioButtonTimeLive.AutoSize = True
        Me.RadioButtonTimeLive.Location = New System.Drawing.Point(538, 37)
        Me.RadioButtonTimeLive.Name = "RadioButtonTimeLive"
        Me.RadioButtonTimeLive.Size = New System.Drawing.Size(92, 17)
        Me.RadioButtonTimeLive.TabIndex = 64
        Me.RadioButtonTimeLive.TabStop = True
        Me.RadioButtonTimeLive.Text = "TimeLive Only"
        Me.RadioButtonTimeLive.UseVisualStyleBackColor = True
        '
        'RadioButtonQuickBooks
        '
        Me.RadioButtonQuickBooks.AutoSize = True
        Me.RadioButtonQuickBooks.Location = New System.Drawing.Point(425, 37)
        Me.RadioButtonQuickBooks.Name = "RadioButtonQuickBooks"
        Me.RadioButtonQuickBooks.Size = New System.Drawing.Size(107, 17)
        Me.RadioButtonQuickBooks.TabIndex = 63
        Me.RadioButtonQuickBooks.TabStop = True
        Me.RadioButtonQuickBooks.Text = "QuickBooks Only"
        Me.RadioButtonQuickBooks.UseVisualStyleBackColor = True
        '
        'RadioButtonBothQBTL
        '
        Me.RadioButtonBothQBTL.AutoSize = True
        Me.RadioButtonBothQBTL.Checked = True
        Me.RadioButtonBothQBTL.Location = New System.Drawing.Point(269, 37)
        Me.RadioButtonBothQBTL.Name = "RadioButtonBothQBTL"
        Me.RadioButtonBothQBTL.Size = New System.Drawing.Size(150, 17)
        Me.RadioButtonBothQBTL.TabIndex = 62
        Me.RadioButtonBothQBTL.TabStop = True
        Me.RadioButtonBothQBTL.Text = "Both Quicks and TimeLive"
        Me.RadioButtonBothQBTL.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(97, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(39, 13)
        Me.Label1.TabIndex = 65
        Me.Label1.Text = "Label1"
        '
        'JobSubJobTreeView
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(642, 390)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.RadioButtonTimeLive)
        Me.Controls.Add(Me.RadioButtonQuickBooks)
        Me.Controls.Add(Me.RadioButtonBothQBTL)
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
    Friend WithEvents RadioButtonTimeLive As RadioButton
    Friend WithEvents RadioButtonQuickBooks As RadioButton
    Friend WithEvents RadioButtonBothQBTL As RadioButton
    Friend WithEvents Label1 As Label
End Class
