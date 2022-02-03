<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ModifyForm
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
        Me.components = New System.ComponentModel.Container()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.lblPassword = New System.Windows.Forms.Label()
        Me.btnOK = New System.Windows.Forms.Button()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtEmail = New System.Windows.Forms.TextBox()
        Me.lbName = New System.Windows.Forms.Label()
        Me.TxtName = New System.Windows.Forms.TextBox()
        Me.txtFax = New System.Windows.Forms.TextBox()
        Me.txtTelephone2 = New System.Windows.Forms.MaskedTextBox()
        Me.ErrorProvider1 = New System.Windows.Forms.ErrorProvider(Me.components)
        Me.RadioButtonBothQBTL = New System.Windows.Forms.RadioButton()
        Me.RadioButtonQuickBooks = New System.Windows.Forms.RadioButton()
        Me.RadioButtonTimeLive = New System.Windows.Forms.RadioButton()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        CType(Me.ErrorProvider1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(222, 189)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(78, 30)
        Me.btnCancel.TabIndex = 13
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'lblPassword
        '
        Me.lblPassword.AutoSize = True
        Me.lblPassword.Location = New System.Drawing.Point(38, 143)
        Me.lblPassword.Name = "lblPassword"
        Me.lblPassword.Size = New System.Drawing.Size(64, 13)
        Me.lblPassword.TabIndex = 14
        Me.lblPassword.Text = "Telephone: " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        '
        'btnOK
        '
        Me.btnOK.Location = New System.Drawing.Point(306, 189)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(78, 30)
        Me.btnOK.TabIndex = 7
        Me.btnOK.Text = "OK"
        Me.btnOK.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(72, 124)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(27, 13)
        Me.Label3.TabIndex = 20
        Me.Label3.Text = "Fax:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(64, 99)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(35, 13)
        Me.Label4.TabIndex = 19
        Me.Label4.Text = "Email:"
        '
        'txtEmail
        '
        Me.txtEmail.Location = New System.Drawing.Point(109, 97)
        Me.txtEmail.Name = "txtEmail"
        Me.txtEmail.Size = New System.Drawing.Size(231, 20)
        Me.txtEmail.TabIndex = 2
        '
        'lbName
        '
        Me.lbName.AutoSize = True
        Me.lbName.Location = New System.Drawing.Point(56, 73)
        Me.lbName.Name = "lbName"
        Me.lbName.Size = New System.Drawing.Size(45, 13)
        Me.lbName.TabIndex = 24
        Me.lbName.Text = "* Name:"
        '
        'TxtName
        '
        Me.TxtName.Location = New System.Drawing.Point(109, 73)
        Me.TxtName.Name = "TxtName"
        Me.TxtName.Size = New System.Drawing.Size(231, 20)
        Me.TxtName.TabIndex = 1
        '
        'txtFax
        '
        Me.txtFax.Location = New System.Drawing.Point(109, 120)
        Me.txtFax.Name = "txtFax"
        Me.txtFax.Size = New System.Drawing.Size(231, 20)
        Me.txtFax.TabIndex = 3
        '
        'txtTelephone2
        '
        Me.txtTelephone2.Location = New System.Drawing.Point(109, 143)
        Me.txtTelephone2.Margin = New System.Windows.Forms.Padding(2)
        Me.txtTelephone2.Mask = "000-000-0000"
        Me.txtTelephone2.Name = "txtTelephone2"
        Me.txtTelephone2.Size = New System.Drawing.Size(231, 20)
        Me.txtTelephone2.TabIndex = 4
        '
        'ErrorProvider1
        '
        Me.ErrorProvider1.ContainerControl = Me
        '
        'RadioButtonBothQBTL
        '
        Me.RadioButtonBothQBTL.AutoSize = True
        Me.RadioButtonBothQBTL.Checked = True
        Me.RadioButtonBothQBTL.Location = New System.Drawing.Point(22, 22)
        Me.RadioButtonBothQBTL.Name = "RadioButtonBothQBTL"
        Me.RadioButtonBothQBTL.Size = New System.Drawing.Size(150, 17)
        Me.RadioButtonBothQBTL.TabIndex = 25
        Me.RadioButtonBothQBTL.TabStop = True
        Me.RadioButtonBothQBTL.Text = "Both Quicks and TimeLive"
        Me.RadioButtonBothQBTL.UseVisualStyleBackColor = True
        '
        'RadioButtonQuickBooks
        '
        Me.RadioButtonQuickBooks.AutoSize = True
        Me.RadioButtonQuickBooks.Location = New System.Drawing.Point(178, 22)
        Me.RadioButtonQuickBooks.Name = "RadioButtonQuickBooks"
        Me.RadioButtonQuickBooks.Size = New System.Drawing.Size(107, 17)
        Me.RadioButtonQuickBooks.TabIndex = 26
        Me.RadioButtonQuickBooks.Text = "QuickBooks Only"
        Me.RadioButtonQuickBooks.UseVisualStyleBackColor = True
        '
        'RadioButtonTimeLive
        '
        Me.RadioButtonTimeLive.AutoSize = True
        Me.RadioButtonTimeLive.Location = New System.Drawing.Point(291, 22)
        Me.RadioButtonTimeLive.Name = "RadioButtonTimeLive"
        Me.RadioButtonTimeLive.Size = New System.Drawing.Size(92, 17)
        Me.RadioButtonTimeLive.TabIndex = 27
        Me.RadioButtonTimeLive.Text = "TimeLive Only"
        Me.RadioButtonTimeLive.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Location = New System.Drawing.Point(12, 3)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(372, 48)
        Me.GroupBox1.TabIndex = 28
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Application"
        '
        'GroupBox2
        '
        Me.GroupBox2.Location = New System.Drawing.Point(12, 57)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(372, 126)
        Me.GroupBox2.TabIndex = 29
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Parameters"
        '
        'ModifyForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(396, 231)
        Me.Controls.Add(Me.RadioButtonTimeLive)
        Me.Controls.Add(Me.RadioButtonQuickBooks)
        Me.Controls.Add(Me.RadioButtonBothQBTL)
        Me.Controls.Add(Me.txtTelephone2)
        Me.Controls.Add(Me.txtFax)
        Me.Controls.Add(Me.lbName)
        Me.Controls.Add(Me.TxtName)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.txtEmail)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.lblPassword)
        Me.Controls.Add(Me.btnOK)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.GroupBox2)
        Me.Name = "ModifyForm"
        Me.Text = "Create Customer"
        CType(Me.ErrorProvider1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnCancel As Button
    Friend WithEvents lblPassword As Label
    Friend WithEvents btnOK As Button
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents txtEmail As TextBox
    Friend WithEvents lbName As Label
    Friend WithEvents TxtName As TextBox
    Friend WithEvents txtFax As TextBox
    Friend WithEvents txtTelephone2 As MaskedTextBox
    Friend WithEvents ErrorProvider1 As ErrorProvider
    Friend WithEvents RadioButtonTimeLive As RadioButton
    Friend WithEvents RadioButtonQuickBooks As RadioButton
    Friend WithEvents RadioButtonBothQBTL As RadioButton
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents GroupBox2 As GroupBox
End Class
