<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class TimeLiveForm
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
        Me.txtFax = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtEmail = New System.Windows.Forms.TextBox()
        Me.txtTelephone1 = New System.Windows.Forms.TextBox()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.lblPassword = New System.Windows.Forms.Label()
        Me.btnOK = New System.Windows.Forms.Button()
        Me.TxtTimeLiveName = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'txtFax
        '
        Me.txtFax.Location = New System.Drawing.Point(142, 83)
        Me.txtFax.Margin = New System.Windows.Forms.Padding(4)
        Me.txtFax.Name = "txtFax"
        Me.txtFax.Size = New System.Drawing.Size(307, 22)
        Me.txtFax.TabIndex = 2
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(101, 86)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(33, 16)
        Me.Label3.TabIndex = 37
        Me.Label3.Text = "Fax:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(89, 58)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(45, 16)
        Me.Label4.TabIndex = 36
        Me.Label4.Text = "Email:"
        '
        'txtEmail
        '
        Me.txtEmail.Location = New System.Drawing.Point(142, 55)
        Me.txtEmail.Margin = New System.Windows.Forms.Padding(4)
        Me.txtEmail.Name = "txtEmail"
        Me.txtEmail.Size = New System.Drawing.Size(307, 22)
        Me.txtEmail.TabIndex = 1
        '
        'txtTelephone1
        '
        Me.txtTelephone1.Location = New System.Drawing.Point(142, 110)
        Me.txtTelephone1.Margin = New System.Windows.Forms.Padding(4)
        Me.txtTelephone1.Name = "txtTelephone1"
        Me.txtTelephone1.Size = New System.Drawing.Size(307, 22)
        Me.txtTelephone1.TabIndex = 3
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(233, 149)
        Me.btnCancel.Margin = New System.Windows.Forms.Padding(4)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(104, 37)
        Me.btnCancel.TabIndex = 31
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'lblPassword
        '
        Me.lblPassword.AutoSize = True
        Me.lblPassword.Location = New System.Drawing.Point(47, 116)
        Me.lblPassword.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblPassword.Name = "lblPassword"
        Me.lblPassword.Size = New System.Drawing.Size(87, 16)
        Me.lblPassword.TabIndex = 32
        Me.lblPassword.Text = "Telephone 1:"
        '
        'btnOK
        '
        Me.btnOK.Location = New System.Drawing.Point(345, 149)
        Me.btnOK.Margin = New System.Windows.Forms.Padding(4)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(104, 37)
        Me.btnOK.TabIndex = 4
        Me.btnOK.Text = "OK"
        Me.btnOK.UseVisualStyleBackColor = True
        '
        'TxtTimeLiveName
        '
        Me.TxtTimeLiveName.Location = New System.Drawing.Point(142, 28)
        Me.TxtTimeLiveName.Margin = New System.Windows.Forms.Padding(4)
        Me.TxtTimeLiveName.Name = "TxtTimeLiveName"
        Me.TxtTimeLiveName.Size = New System.Drawing.Size(307, 22)
        Me.TxtTimeLiveName.TabIndex = 0
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(27, 34)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(107, 16)
        Me.Label2.TabIndex = 38
        Me.Label2.Text = "TimeLive Name:"
        '
        'TimeLiveForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(474, 228)
        Me.Controls.Add(Me.txtFax)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.TxtTimeLiveName)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.txtEmail)
        Me.Controls.Add(Me.txtTelephone1)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.lblPassword)
        Me.Controls.Add(Me.btnOK)
        Me.Name = "TimeLiveForm"
        Me.Text = "TimeLiveForm"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents txtFax As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents txtEmail As TextBox
    Friend WithEvents txtTelephone1 As TextBox
    Friend WithEvents btnCancel As Button
    Friend WithEvents lblPassword As Label
    Friend WithEvents btnOK As Button
    Friend WithEvents TxtTimeLiveName As TextBox
    Friend WithEvents Label2 As Label
End Class
