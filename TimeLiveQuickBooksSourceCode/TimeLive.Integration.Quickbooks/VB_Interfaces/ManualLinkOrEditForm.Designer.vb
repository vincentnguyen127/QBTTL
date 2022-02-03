<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ManualLinkOrEditForm
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
        Me.btnOK = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.RadioButtonManualLink = New System.Windows.Forms.RadioButton()
        Me.RadioButtonEdit = New System.Windows.Forms.RadioButton()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnOK
        '
        Me.btnOK.Location = New System.Drawing.Point(148, 115)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(75, 23)
        Me.btnOK.TabIndex = 17
        Me.btnOK.Text = "OK"
        Me.btnOK.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(60, 115)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnCancel.TabIndex = 19
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.RadioButtonEdit)
        Me.GroupBox1.Controls.Add(Me.RadioButtonManualLink)
        Me.GroupBox1.Location = New System.Drawing.Point(25, 26)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(198, 83)
        Me.GroupBox1.TabIndex = 24
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Options"
        '
        'RadioButtonManualLink
        '
        Me.RadioButtonManualLink.AutoSize = True
        Me.RadioButtonManualLink.Location = New System.Drawing.Point(24, 42)
        Me.RadioButtonManualLink.Name = "RadioButtonManualLink"
        Me.RadioButtonManualLink.Size = New System.Drawing.Size(83, 17)
        Me.RadioButtonManualLink.TabIndex = 25
        Me.RadioButtonManualLink.TabStop = True
        Me.RadioButtonManualLink.Text = "Manual Link"
        Me.RadioButtonManualLink.UseVisualStyleBackColor = True
        '
        'RadioButtonEdit
        '
        Me.RadioButtonEdit.AutoSize = True
        Me.RadioButtonEdit.Location = New System.Drawing.Point(24, 19)
        Me.RadioButtonEdit.Name = "RadioButtonEdit"
        Me.RadioButtonEdit.Size = New System.Drawing.Size(43, 17)
        Me.RadioButtonEdit.TabIndex = 26
        Me.RadioButtonEdit.TabStop = True
        Me.RadioButtonEdit.Text = "Edit"
        Me.RadioButtonEdit.UseVisualStyleBackColor = True
        '
        'ManualLinkOrEditForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(253, 169)
        Me.Controls.Add(Me.btnOK)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "ManualLinkOrEditForm"
        Me.Text = "ManualLinkOrEditForm"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnOK As Button
    Friend WithEvents btnCancel As Button
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents RadioButtonManualLink As RadioButton
    Friend WithEvents RadioButtonEdit As RadioButton
End Class
