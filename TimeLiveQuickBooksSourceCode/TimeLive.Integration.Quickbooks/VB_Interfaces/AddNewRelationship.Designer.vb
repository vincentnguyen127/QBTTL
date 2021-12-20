<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AddNewRelationship
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
        Me.Label2 = New System.Windows.Forms.Label()
        Me.TextBoxJob = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TextBoxItem = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnOK = New System.Windows.Forms.Button()
        Me.ComboBoxEmployee = New System.Windows.Forms.ComboBox()
        Me.ComboBoxPayroll = New System.Windows.Forms.ComboBox()
        Me.SuspendLayout()
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(75, 60)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(62, 15)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Employee"
        '
        'TextBoxJob
        '
        Me.TextBoxJob.Location = New System.Drawing.Point(160, 95)
        Me.TextBoxJob.Name = "TextBoxJob"
        Me.TextBoxJob.Size = New System.Drawing.Size(184, 20)
        Me.TextBoxJob.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(50, 100)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(87, 15)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "Jobs / Subjobs"
        '
        'TextBoxItem
        '
        Me.TextBoxItem.Location = New System.Drawing.Point(160, 173)
        Me.TextBoxItem.Name = "TextBoxItem"
        Me.TextBoxItem.Size = New System.Drawing.Size(184, 20)
        Me.TextBoxItem.TabIndex = 3
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(39, 178)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(98, 15)
        Me.Label3.TabIndex = 8
        Me.Label3.Text = "Items / Subitems"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(66, 143)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(71, 15)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "Payroll Item"
        '
        'btnCancel
        '
        Me.btnCancel.Location = New System.Drawing.Point(174, 211)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnCancel.TabIndex = 5
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'btnOK
        '
        Me.btnOK.Location = New System.Drawing.Point(269, 211)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(75, 23)
        Me.btnOK.TabIndex = 4
        Me.btnOK.Text = "OK"
        Me.btnOK.UseVisualStyleBackColor = True
        '
        'ComboBoxEmployee
        '
        Me.ComboBoxEmployee.FormattingEnabled = True
        Me.ComboBoxEmployee.Location = New System.Drawing.Point(160, 59)
        Me.ComboBoxEmployee.Name = "ComboBoxEmployee"
        Me.ComboBoxEmployee.Size = New System.Drawing.Size(184, 21)
        Me.ComboBoxEmployee.TabIndex = 0
        '
        'ComboBoxPayroll
        '
        Me.ComboBoxPayroll.FormattingEnabled = True
        Me.ComboBoxPayroll.Location = New System.Drawing.Point(160, 137)
        Me.ComboBoxPayroll.Name = "ComboBoxPayroll"
        Me.ComboBoxPayroll.Size = New System.Drawing.Size(184, 21)
        Me.ComboBoxPayroll.TabIndex = 2
        '
        'AddNewRelationship
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(448, 321)
        Me.Controls.Add(Me.ComboBoxPayroll)
        Me.Controls.Add(Me.ComboBoxEmployee)
        Me.Controls.Add(Me.btnOK)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.TextBoxItem)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.TextBoxJob)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label2)
        Me.Name = "AddNewRelationship"
        Me.Text = "AddNewRelationship"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label2 As Label
    Friend WithEvents TextBoxJob As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents TextBoxItem As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents btnCancel As Button
    Friend WithEvents btnOK As Button
    Friend WithEvents ComboBoxEmployee As ComboBox
    Friend WithEvents ComboBoxPayroll As ComboBox
End Class
