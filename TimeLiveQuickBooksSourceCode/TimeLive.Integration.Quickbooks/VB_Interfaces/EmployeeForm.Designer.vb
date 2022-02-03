<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class EmployeeForm
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
        Me.mTxtHiredDate = New System.Windows.Forms.MaskedTextBox()
        Me.lbName = New System.Windows.Forms.Label()
        Me.txtFirstName = New System.Windows.Forms.TextBox()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.lblPassword = New System.Windows.Forms.Label()
        Me.btnOK = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtLastName = New System.Windows.Forms.TextBox()
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtAdressLine1 = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtMiddleName = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtState = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtCity = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtAddressLine2 = New System.Windows.Forms.TextBox()
        Me.txtZipCode = New System.Windows.Forms.TextBox()
        Me.txtHomePhone = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtWorkPhone = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txtMobilePhone = New System.Windows.Forms.TextBox()
        Me.RadioButtonTimeLive = New System.Windows.Forms.RadioButton()
        Me.RadioButtonQuickBooks = New System.Windows.Forms.RadioButton()
        Me.RadioButtonBothQBTL = New System.Windows.Forms.RadioButton()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'mTxtHiredDate
        '
        Me.mTxtHiredDate.Location = New System.Drawing.Point(109, 123)
        Me.mTxtHiredDate.Margin = New System.Windows.Forms.Padding(2)
        Me.mTxtHiredDate.Mask = "00/00/0000"
        Me.mTxtHiredDate.Name = "mTxtHiredDate"
        Me.mTxtHiredDate.Size = New System.Drawing.Size(128, 20)
        Me.mTxtHiredDate.TabIndex = 3
        Me.mTxtHiredDate.ValidatingType = GetType(Date)
        '
        'lbName
        '
        Me.lbName.AutoSize = True
        Me.lbName.Location = New System.Drawing.Point(36, 82)
        Me.lbName.Name = "lbName"
        Me.lbName.Size = New System.Drawing.Size(67, 13)
        Me.lbName.TabIndex = 34
        Me.lbName.Text = "* First Name:"
        '
        'txtFirstName
        '
        Me.txtFirstName.Location = New System.Drawing.Point(109, 75)
        Me.txtFirstName.Name = "txtFirstName"
        Me.txtFirstName.Size = New System.Drawing.Size(128, 20)
        Me.txtFirstName.TabIndex = 0
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(326, 237)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(78, 30)
        Me.btnCancel.TabIndex = 13
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'lblPassword
        '
        Me.lblPassword.AutoSize = True
        Me.lblPassword.Location = New System.Drawing.Point(35, 130)
        Me.lblPassword.Name = "lblPassword"
        Me.lblPassword.Size = New System.Drawing.Size(68, 13)
        Me.lblPassword.TabIndex = 31
        Me.lblPassword.Text = "* Hired Date:"
        '
        'btnOK
        '
        Me.btnOK.Location = New System.Drawing.Point(410, 237)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(78, 30)
        Me.btnOK.TabIndex = 12
        Me.btnOK.Text = "OK"
        Me.btnOK.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(35, 106)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(68, 13)
        Me.Label2.TabIndex = 38
        Me.Label2.Text = "* Last Name:"
        '
        'txtLastName
        '
        Me.txtLastName.Location = New System.Drawing.Point(109, 99)
        Me.txtLastName.Name = "txtLastName"
        Me.txtLastName.Size = New System.Drawing.Size(128, 20)
        Me.txtLastName.TabIndex = 2
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(61, 4)
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(244, 100)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(83, 13)
        Me.Label1.TabIndex = 44
        Me.Label1.Text = "Address Line 1: "
        '
        'txtAdressLine1
        '
        Me.txtAdressLine1.Location = New System.Drawing.Point(330, 99)
        Me.txtAdressLine1.Name = "txtAdressLine1"
        Me.txtAdressLine1.Size = New System.Drawing.Size(128, 20)
        Me.txtAdressLine1.TabIndex = 4
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(255, 75)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(72, 13)
        Me.Label3.TabIndex = 42
        Me.Label3.Text = "Middle Name:"
        '
        'txtMiddleName
        '
        Me.txtMiddleName.Location = New System.Drawing.Point(330, 75)
        Me.txtMiddleName.Name = "txtMiddleName"
        Me.txtMiddleName.Size = New System.Drawing.Size(128, 20)
        Me.txtMiddleName.TabIndex = 1
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(249, 130)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(80, 13)
        Me.Label4.TabIndex = 41
        Me.Label4.Text = "Address Line 2:"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(292, 173)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(35, 13)
        Me.Label5.TabIndex = 50
        Me.Label5.Text = "State:"
        '
        'txtState
        '
        Me.txtState.Location = New System.Drawing.Point(330, 171)
        Me.txtState.Name = "txtState"
        Me.txtState.Size = New System.Drawing.Size(44, 20)
        Me.txtState.TabIndex = 7
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(299, 148)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(27, 13)
        Me.Label6.TabIndex = 48
        Me.Label6.Text = "City:"
        '
        'txtCity
        '
        Me.txtCity.Location = New System.Drawing.Point(330, 147)
        Me.txtCity.Name = "txtCity"
        Me.txtCity.Size = New System.Drawing.Size(128, 20)
        Me.txtCity.TabIndex = 6
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(379, 175)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(27, 13)
        Me.Label7.TabIndex = 47
        Me.Label7.Text = "ZIP:"
        '
        'txtAddressLine2
        '
        Me.txtAddressLine2.Location = New System.Drawing.Point(330, 123)
        Me.txtAddressLine2.Name = "txtAddressLine2"
        Me.txtAddressLine2.Size = New System.Drawing.Size(128, 20)
        Me.txtAddressLine2.TabIndex = 5
        '
        'txtZipCode
        '
        Me.txtZipCode.Location = New System.Drawing.Point(403, 171)
        Me.txtZipCode.Name = "txtZipCode"
        Me.txtZipCode.Size = New System.Drawing.Size(55, 20)
        Me.txtZipCode.TabIndex = 8
        '
        'txtHomePhone
        '
        Me.txtHomePhone.Location = New System.Drawing.Point(109, 147)
        Me.txtHomePhone.Name = "txtHomePhone"
        Me.txtHomePhone.Size = New System.Drawing.Size(128, 20)
        Me.txtHomePhone.TabIndex = 9
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(36, 178)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(67, 13)
        Me.Label8.TabIndex = 55
        Me.Label8.Text = "WorkPhone:"
        '
        'txtWorkPhone
        '
        Me.txtWorkPhone.Location = New System.Drawing.Point(109, 171)
        Me.txtWorkPhone.Name = "txtWorkPhone"
        Me.txtWorkPhone.Size = New System.Drawing.Size(128, 20)
        Me.txtWorkPhone.TabIndex = 10
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(31, 154)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(72, 13)
        Me.Label9.TabIndex = 53
        Me.Label9.Text = "Home Phone:"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(28, 202)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(75, 13)
        Me.Label10.TabIndex = 58
        Me.Label10.Text = "Mobile Phone:"
        '
        'txtMobilePhone
        '
        Me.txtMobilePhone.Location = New System.Drawing.Point(109, 195)
        Me.txtMobilePhone.Name = "txtMobilePhone"
        Me.txtMobilePhone.Size = New System.Drawing.Size(128, 20)
        Me.txtMobilePhone.TabIndex = 11
        '
        'RadioButtonTimeLive
        '
        Me.RadioButtonTimeLive.AutoSize = True
        Me.RadioButtonTimeLive.Location = New System.Drawing.Point(354, 20)
        Me.RadioButtonTimeLive.Name = "RadioButtonTimeLive"
        Me.RadioButtonTimeLive.Size = New System.Drawing.Size(92, 17)
        Me.RadioButtonTimeLive.TabIndex = 61
        Me.RadioButtonTimeLive.TabStop = True
        Me.RadioButtonTimeLive.Text = "TimeLive Only"
        Me.RadioButtonTimeLive.UseVisualStyleBackColor = True
        '
        'RadioButtonQuickBooks
        '
        Me.RadioButtonQuickBooks.AutoSize = True
        Me.RadioButtonQuickBooks.Location = New System.Drawing.Point(210, 20)
        Me.RadioButtonQuickBooks.Name = "RadioButtonQuickBooks"
        Me.RadioButtonQuickBooks.Size = New System.Drawing.Size(107, 17)
        Me.RadioButtonQuickBooks.TabIndex = 60
        Me.RadioButtonQuickBooks.TabStop = True
        Me.RadioButtonQuickBooks.Text = "QuickBooks Only"
        Me.RadioButtonQuickBooks.UseVisualStyleBackColor = True
        '
        'RadioButtonBothQBTL
        '
        Me.RadioButtonBothQBTL.AutoSize = True
        Me.RadioButtonBothQBTL.Checked = True
        Me.RadioButtonBothQBTL.Location = New System.Drawing.Point(48, 22)
        Me.RadioButtonBothQBTL.Name = "RadioButtonBothQBTL"
        Me.RadioButtonBothQBTL.Size = New System.Drawing.Size(150, 17)
        Me.RadioButtonBothQBTL.TabIndex = 59
        Me.RadioButtonBothQBTL.TabStop = True
        Me.RadioButtonBothQBTL.Text = "Both Quicks and TimeLive"
        Me.RadioButtonBothQBTL.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.RadioButtonTimeLive)
        Me.GroupBox1.Controls.Add(Me.RadioButtonQuickBooks)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 2)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(476, 53)
        Me.GroupBox1.TabIndex = 62
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Application"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(28, 246)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(251, 13)
        Me.Label11.TabIndex = 63
        Me.Label11.Text = "Note: Hired date is used as the TimeLive Password."
        '
        'GroupBox2
        '
        Me.GroupBox2.Location = New System.Drawing.Point(12, 61)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(476, 170)
        Me.GroupBox2.TabIndex = 64
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Parameters"
        '
        'EmployeeForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(500, 279)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.RadioButtonBothQBTL)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnOK)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.txtMobilePhone)
        Me.Controls.Add(Me.txtHomePhone)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.txtWorkPhone)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.txtZipCode)
        Me.Controls.Add(Me.txtAddressLine2)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.txtState)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.txtCity)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtAdressLine1)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txtMiddleName)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtLastName)
        Me.Controls.Add(Me.mTxtHiredDate)
        Me.Controls.Add(Me.lbName)
        Me.Controls.Add(Me.txtFirstName)
        Me.Controls.Add(Me.lblPassword)
        Me.Controls.Add(Me.GroupBox2)
        Me.Margin = New System.Windows.Forms.Padding(2)
        Me.Name = "EmployeeForm"
        Me.Text = "Create Employee"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents mTxtHiredDate As MaskedTextBox
    Friend WithEvents lbName As Label
    Friend WithEvents txtFirstName As TextBox
    Friend WithEvents btnCancel As Button
    Friend WithEvents lblPassword As Label
    Friend WithEvents btnOK As Button
    Friend WithEvents Label2 As Label
    Friend WithEvents txtLastName As TextBox
    Friend WithEvents ContextMenuStrip1 As ContextMenuStrip
    Friend WithEvents Label1 As Label
    Friend WithEvents txtAdressLine1 As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents txtMiddleName As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents txtState As TextBox
    Friend WithEvents Label6 As Label
    Friend WithEvents txtCity As TextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents txtAddressLine2 As TextBox
    Friend WithEvents txtZipCode As TextBox
    Friend WithEvents txtHomePhone As TextBox
    Friend WithEvents Label8 As Label
    Friend WithEvents txtWorkPhone As TextBox
    Friend WithEvents Label9 As Label
    Friend WithEvents Label10 As Label
    Friend WithEvents txtMobilePhone As TextBox
    Friend WithEvents RadioButtonTimeLive As RadioButton
    Friend WithEvents RadioButtonQuickBooks As RadioButton
    Friend WithEvents RadioButtonBothQBTL As RadioButton
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents Label11 As Label
    Friend WithEvents GroupBox2 As GroupBox
End Class
