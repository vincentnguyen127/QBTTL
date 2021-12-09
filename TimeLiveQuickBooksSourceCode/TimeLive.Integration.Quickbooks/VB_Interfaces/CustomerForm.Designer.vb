<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ModifyForm
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
        CType(Me.ErrorProvider1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(176, 123)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(78, 30)
        Me.btnCancel.TabIndex = 13
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'lblPassword
        '
        Me.lblPassword.AutoSize = True
        Me.lblPassword.Location = New System.Drawing.Point(37, 99)
        Me.lblPassword.Name = "lblPassword"
        Me.lblPassword.Size = New System.Drawing.Size(64, 13)
        Me.lblPassword.TabIndex = 14
        Me.lblPassword.Text = "Telephone: " & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10)
        '
        'btnOK
        '
        Me.btnOK.Location = New System.Drawing.Point(260, 123)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(78, 30)
        Me.btnOK.TabIndex = 7
        Me.btnOK.Text = "OK"
        Me.btnOK.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(71, 80)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(27, 13)
        Me.Label3.TabIndex = 20
        Me.Label3.Text = "Fax:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(63, 55)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(35, 13)
        Me.Label4.TabIndex = 19
        Me.Label4.Text = "Email:"
        '
        'txtEmail
        '
        Me.txtEmail.Location = New System.Drawing.Point(108, 53)
        Me.txtEmail.Name = "txtEmail"
        Me.txtEmail.Size = New System.Drawing.Size(231, 20)
        Me.txtEmail.TabIndex = 2
        '
        'lbName
        '
        Me.lbName.AutoSize = True
        Me.lbName.Location = New System.Drawing.Point(55, 29)
        Me.lbName.Name = "lbName"
        Me.lbName.Size = New System.Drawing.Size(45, 13)
        Me.lbName.TabIndex = 24
        Me.lbName.Text = "* Name:"
        '
        'TxtName
        '
        Me.TxtName.Location = New System.Drawing.Point(108, 29)
        Me.TxtName.Name = "TxtName"
        Me.TxtName.Size = New System.Drawing.Size(231, 20)
        Me.TxtName.TabIndex = 1
        '
        'txtFax
        '
        Me.txtFax.Location = New System.Drawing.Point(108, 76)
        Me.txtFax.Name = "txtFax"
        Me.txtFax.Size = New System.Drawing.Size(231, 20)
        Me.txtFax.TabIndex = 3
        '
        'txtTelephone2
        '
        Me.txtTelephone2.Location = New System.Drawing.Point(108, 99)
        Me.txtTelephone2.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.txtTelephone2.Mask = "000-000-0000"
        Me.txtTelephone2.Name = "txtTelephone2"
        Me.txtTelephone2.Size = New System.Drawing.Size(231, 20)
        Me.txtTelephone2.TabIndex = 4
        '
        'ErrorProvider1
        '
        Me.ErrorProvider1.ContainerControl = Me
        '
        'ModifyForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(393, 175)
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
        Me.Name = "ModifyForm"
        Me.Text = "Customer Entry"
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
End Class
