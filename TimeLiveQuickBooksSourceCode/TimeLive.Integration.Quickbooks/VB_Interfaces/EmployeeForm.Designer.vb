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
        Me.SuspendLayout()
        '
        'mTxtHiredDate
        '
        Me.mTxtHiredDate.Location = New System.Drawing.Point(107, 77)
        Me.mTxtHiredDate.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.mTxtHiredDate.Mask = "00/00/0000"
        Me.mTxtHiredDate.Name = "mTxtHiredDate"
        Me.mTxtHiredDate.Size = New System.Drawing.Size(128, 20)
        Me.mTxtHiredDate.TabIndex = 3
        Me.mTxtHiredDate.ValidatingType = GetType(Date)
        '
        'lbName
        '
        Me.lbName.AutoSize = True
        Me.lbName.Location = New System.Drawing.Point(33, 29)
        Me.lbName.Name = "lbName"
        Me.lbName.Size = New System.Drawing.Size(67, 13)
        Me.lbName.TabIndex = 34
        Me.lbName.Text = "* First Name:"
        '
        'txtFirstName
        '
        Me.txtFirstName.Location = New System.Drawing.Point(107, 29)
        Me.txtFirstName.Name = "txtFirstName"
        Me.txtFirstName.Size = New System.Drawing.Size(128, 20)
        Me.txtFirstName.TabIndex = 0
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(293, 162)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(78, 30)
        Me.btnCancel.TabIndex = 13
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'lblPassword
        '
        Me.lblPassword.AutoSize = True
        Me.lblPassword.Location = New System.Drawing.Point(33, 77)
        Me.lblPassword.Name = "lblPassword"
        Me.lblPassword.Size = New System.Drawing.Size(68, 13)
        Me.lblPassword.TabIndex = 31
        Me.lblPassword.Text = "* Hired Date:"
        '
        'btnOK
        '
        Me.btnOK.Location = New System.Drawing.Point(377, 162)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(78, 30)
        Me.btnOK.TabIndex = 12
        Me.btnOK.Text = "OK"
        Me.btnOK.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(33, 54)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(68, 13)
        Me.Label2.TabIndex = 38
        Me.Label2.Text = "* Last Name:"
        '
        'txtLastName
        '
        Me.txtLastName.Location = New System.Drawing.Point(107, 54)
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
        Me.Label1.Location = New System.Drawing.Point(242, 54)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(83, 13)
        Me.Label1.TabIndex = 44
        Me.Label1.Text = "Address Line 1: "
        '
        'txtAdressLine1
        '
        Me.txtAdressLine1.Location = New System.Drawing.Point(328, 54)
        Me.txtAdressLine1.Name = "txtAdressLine1"
        Me.txtAdressLine1.Size = New System.Drawing.Size(128, 20)
        Me.txtAdressLine1.TabIndex = 4
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(253, 29)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(72, 13)
        Me.Label3.TabIndex = 42
        Me.Label3.Text = "Middle Name:"
        '
        'txtMiddleName
        '
        Me.txtMiddleName.Location = New System.Drawing.Point(328, 29)
        Me.txtMiddleName.Name = "txtMiddleName"
        Me.txtMiddleName.Size = New System.Drawing.Size(128, 20)
        Me.txtMiddleName.TabIndex = 1
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(247, 84)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(80, 13)
        Me.Label4.TabIndex = 41
        Me.Label4.Text = "Address Line 2:"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(290, 127)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(35, 13)
        Me.Label5.TabIndex = 50
        Me.Label5.Text = "State:"
        '
        'txtState
        '
        Me.txtState.Location = New System.Drawing.Point(328, 127)
        Me.txtState.Name = "txtState"
        Me.txtState.Size = New System.Drawing.Size(44, 20)
        Me.txtState.TabIndex = 7
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(297, 102)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(27, 13)
        Me.Label6.TabIndex = 48
        Me.Label6.Text = "City:"
        '
        'txtCity
        '
        Me.txtCity.Location = New System.Drawing.Point(328, 102)
        Me.txtCity.Name = "txtCity"
        Me.txtCity.Size = New System.Drawing.Size(128, 20)
        Me.txtCity.TabIndex = 6
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(382, 129)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(27, 13)
        Me.Label7.TabIndex = 47
        Me.Label7.Text = "ZIP:"
        '
        'txtAddressLine2
        '
        Me.txtAddressLine2.Location = New System.Drawing.Point(328, 79)
        Me.txtAddressLine2.Name = "txtAddressLine2"
        Me.txtAddressLine2.Size = New System.Drawing.Size(128, 20)
        Me.txtAddressLine2.TabIndex = 5
        '
        'txtZipCode
        '
        Me.txtZipCode.Location = New System.Drawing.Point(412, 127)
        Me.txtZipCode.Name = "txtZipCode"
        Me.txtZipCode.Size = New System.Drawing.Size(44, 20)
        Me.txtZipCode.TabIndex = 8
        '
        'txtHomePhone
        '
        Me.txtHomePhone.Location = New System.Drawing.Point(107, 101)
        Me.txtHomePhone.Name = "txtHomePhone"
        Me.txtHomePhone.Size = New System.Drawing.Size(128, 20)
        Me.txtHomePhone.TabIndex = 9
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(34, 124)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(67, 13)
        Me.Label8.TabIndex = 55
        Me.Label8.Text = "WorkPhone:"
        '
        'txtWorkPhone
        '
        Me.txtWorkPhone.Location = New System.Drawing.Point(107, 124)
        Me.txtWorkPhone.Name = "txtWorkPhone"
        Me.txtWorkPhone.Size = New System.Drawing.Size(128, 20)
        Me.txtWorkPhone.TabIndex = 10
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(28, 105)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(72, 13)
        Me.Label9.TabIndex = 53
        Me.Label9.Text = "Home Phone:"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(26, 150)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(75, 13)
        Me.Label10.TabIndex = 58
        Me.Label10.Text = "Mobile Phone:"
        '
        'txtMobilePhone
        '
        Me.txtMobilePhone.Location = New System.Drawing.Point(107, 150)
        Me.txtMobilePhone.Name = "txtMobilePhone"
        Me.txtMobilePhone.Size = New System.Drawing.Size(128, 20)
        Me.txtMobilePhone.TabIndex = 11
        '
        'EmployeeForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(502, 221)
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
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.lblPassword)
        Me.Controls.Add(Me.btnOK)
        Me.Margin = New System.Windows.Forms.Padding(2, 2, 2, 2)
        Me.Name = "EmployeeForm"
        Me.Text = "Employee Entry"
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
End Class
