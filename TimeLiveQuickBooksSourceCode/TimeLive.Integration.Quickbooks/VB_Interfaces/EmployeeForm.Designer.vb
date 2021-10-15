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
        Me.SuspendLayout()
        '
        'mTxtHiredDate
        '
        Me.mTxtHiredDate.Location = New System.Drawing.Point(143, 97)
        Me.mTxtHiredDate.Mask = "00/00/0000"
        Me.mTxtHiredDate.Name = "mTxtHiredDate"
        Me.mTxtHiredDate.Size = New System.Drawing.Size(307, 22)
        Me.mTxtHiredDate.TabIndex = 28
        Me.mTxtHiredDate.ValidatingType = GetType(Date)
        '
        'lbName
        '
        Me.lbName.AutoSize = True
        Me.lbName.Location = New System.Drawing.Point(44, 36)
        Me.lbName.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbName.Name = "lbName"
        Me.lbName.Size = New System.Drawing.Size(84, 16)
        Me.lbName.TabIndex = 34
        Me.lbName.Text = "* First Name:"
        '
        'txtFirstName
        '
        Me.txtFirstName.Location = New System.Drawing.Point(143, 36)
        Me.txtFirstName.Margin = New System.Windows.Forms.Padding(4)
        Me.txtFirstName.Name = "txtFirstName"
        Me.txtFirstName.Size = New System.Drawing.Size(307, 22)
        Me.txtFirstName.TabIndex = 25
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(234, 126)
        Me.btnCancel.Margin = New System.Windows.Forms.Padding(4)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(104, 37)
        Me.btnCancel.TabIndex = 30
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'lblPassword
        '
        Me.lblPassword.AutoSize = True
        Me.lblPassword.Location = New System.Drawing.Point(48, 97)
        Me.lblPassword.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblPassword.Name = "lblPassword"
        Me.lblPassword.Size = New System.Drawing.Size(76, 16)
        Me.lblPassword.TabIndex = 31
        Me.lblPassword.Text = "Hired Date:"
        '
        'btnOK
        '
        Me.btnOK.Location = New System.Drawing.Point(346, 126)
        Me.btnOK.Margin = New System.Windows.Forms.Padding(4)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(104, 37)
        Me.btnOK.TabIndex = 29
        Me.btnOK.Text = "OK"
        Me.btnOK.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(44, 66)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(84, 16)
        Me.Label2.TabIndex = 38
        Me.Label2.Text = "* Last Name:"
        '
        'txtLastName
        '
        Me.txtLastName.Location = New System.Drawing.Point(143, 66)
        Me.txtLastName.Margin = New System.Windows.Forms.Padding(4)
        Me.txtLastName.Name = "txtLastName"
        Me.txtLastName.Size = New System.Drawing.Size(307, 22)
        Me.txtLastName.TabIndex = 37
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(61, 4)
        '
        'EmployeeForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(498, 238)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtLastName)
        Me.Controls.Add(Me.mTxtHiredDate)
        Me.Controls.Add(Me.lbName)
        Me.Controls.Add(Me.txtFirstName)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.lblPassword)
        Me.Controls.Add(Me.btnOK)
        Me.Name = "EmployeeForm"
        Me.Text = "EmployeeForm"
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
End Class
