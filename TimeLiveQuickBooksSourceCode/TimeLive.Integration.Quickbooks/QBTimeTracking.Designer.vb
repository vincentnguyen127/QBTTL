<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class QBTimeTracking
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
        Dim btn_Cancel As System.Windows.Forms.Button
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(QBTimeTracking))
        Me.lblUptoDate = New System.Windows.Forms.Label()
        Me.lblFromDate = New System.Windows.Forms.Label()
        Me.dpEndDate = New System.Windows.Forms.DateTimePicker()
        Me.cbWageType = New System.Windows.Forms.ComboBox()
        Me.lblWageType = New System.Windows.Forms.Label()
        Me.btn_currentweek = New System.Windows.Forms.Button()
        Me.btn_Ok = New System.Windows.Forms.Button()
        Me.preWeek = New System.Windows.Forms.Button()
        Me.nextWeek = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.selectAllBtn = New System.Windows.Forms.Button()
        Me.pgbar = New System.Windows.Forms.ProgressBar()
        Me.checkbox1 = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.dpStartDate = New System.Windows.Forms.DateTimePicker()
        Me.chkPayrollTimesheet = New System.Windows.Forms.CheckBox()
        Me.cbClass = New System.Windows.Forms.ComboBox()
        Me.cbPayrollItem = New System.Windows.Forms.ComboBox()
        Me.lblPayrollItem = New System.Windows.Forms.Label()
        Me.lblClass = New System.Windows.Forms.Label()
        Me.rbJob = New System.Windows.Forms.RadioButton()
        Me.rbtJobitems = New System.Windows.Forms.RadioButton()
        Me.rbItem = New System.Windows.Forms.RadioButton()
        btn_Cancel = New System.Windows.Forms.Button()
        Me.GroupBox1.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btn_Cancel
        '
        btn_Cancel.Location = New System.Drawing.Point(539, 405)
        btn_Cancel.Name = "btn_Cancel"
        btn_Cancel.Size = New System.Drawing.Size(75, 23)
        btn_Cancel.TabIndex = 36
        btn_Cancel.Text = "Cancel"
        btn_Cancel.UseVisualStyleBackColor = True
        AddHandler btn_Cancel.Click, AddressOf Me.btnCancel_Click
        '
        'lblUptoDate
        '
        Me.lblUptoDate.AutoSize = True
        Me.lblUptoDate.Location = New System.Drawing.Point(169, 30)
        Me.lblUptoDate.Name = "lblUptoDate"
        Me.lblUptoDate.Size = New System.Drawing.Size(59, 13)
        Me.lblUptoDate.TabIndex = 10
        Me.lblUptoDate.Text = "Upto Date:"
        '
        'lblFromDate
        '
        Me.lblFromDate.AutoSize = True
        Me.lblFromDate.Location = New System.Drawing.Point(19, 30)
        Me.lblFromDate.Name = "lblFromDate"
        Me.lblFromDate.Size = New System.Drawing.Size(59, 13)
        Me.lblFromDate.TabIndex = 9
        Me.lblFromDate.Text = "From Date:"
        '
        'dpEndDate
        '
        Me.dpEndDate.CustomFormat = ""
        Me.dpEndDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dpEndDate.Location = New System.Drawing.Point(237, 26)
        Me.dpEndDate.Name = "dpEndDate"
        Me.dpEndDate.Size = New System.Drawing.Size(87, 20)
        Me.dpEndDate.TabIndex = 8
        '
        'cbWageType
        '
        Me.cbWageType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbWageType.FormattingEnabled = True
        Me.cbWageType.Items.AddRange(New Object() {"Bonus", "Comission", "Hourly-Overtime", "Hourly-Regular", "Hourly-Sick", "Hourly-Vacation", "Salary-Regular", "Salary-Sick", "Salary-Vacation"})
        Me.cbWageType.Location = New System.Drawing.Point(384, 97)
        Me.cbWageType.Name = "cbWageType"
        Me.cbWageType.Size = New System.Drawing.Size(219, 21)
        Me.cbWageType.TabIndex = 21
        '
        'lblWageType
        '
        Me.lblWageType.AutoSize = True
        Me.lblWageType.Location = New System.Drawing.Point(312, 105)
        Me.lblWageType.Name = "lblWageType"
        Me.lblWageType.Size = New System.Drawing.Size(66, 13)
        Me.lblWageType.TabIndex = 22
        Me.lblWageType.Text = "Wage Type:"
        '
        'btn_currentweek
        '
        Me.btn_currentweek.Location = New System.Drawing.Point(114, 53)
        Me.btn_currentweek.Name = "btn_currentweek"
        Me.btn_currentweek.Size = New System.Drawing.Size(87, 23)
        Me.btn_currentweek.TabIndex = 25
        Me.btn_currentweek.Text = "Current Week"
        Me.btn_currentweek.UseVisualStyleBackColor = True
        '
        'btn_Ok
        '
        Me.btn_Ok.Location = New System.Drawing.Point(448, 405)
        Me.btn_Ok.Name = "btn_Ok"
        Me.btn_Ok.Size = New System.Drawing.Size(75, 23)
        Me.btn_Ok.TabIndex = 26
        Me.btn_Ok.Text = "Ok"
        Me.btn_Ok.UseVisualStyleBackColor = True
        '
        'preWeek
        '
        Me.preWeek.Location = New System.Drawing.Point(80, 56)
        Me.preWeek.Name = "preWeek"
        Me.preWeek.Size = New System.Drawing.Size(38, 23)
        Me.preWeek.TabIndex = 34
        Me.preWeek.Text = "<<"
        Me.preWeek.UseVisualStyleBackColor = True
        '
        'nextWeek
        '
        Me.nextWeek.Location = New System.Drawing.Point(207, 53)
        Me.nextWeek.Name = "nextWeek"
        Me.nextWeek.Size = New System.Drawing.Size(38, 23)
        Me.nextWeek.TabIndex = 35
        Me.nextWeek.Text = ">>"
        Me.nextWeek.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.rbItem)
        Me.GroupBox1.Controls.Add(Me.rbtJobitems)
        Me.GroupBox1.Controls.Add(Me.rbJob)
        Me.GroupBox1.Controls.Add(Me.lblPayrollItem)
        Me.GroupBox1.Controls.Add(Me.lblClass)
        Me.GroupBox1.Controls.Add(Me.chkPayrollTimesheet)
        Me.GroupBox1.Controls.Add(Me.cbClass)
        Me.GroupBox1.Controls.Add(Me.cbPayrollItem)
        Me.GroupBox1.Controls.Add(Me.nextWeek)
        Me.GroupBox1.Controls.Add(Me.btn_currentweek)
        Me.GroupBox1.Controls.Add(Me.cbWageType)
        Me.GroupBox1.Controls.Add(Me.lblWageType)
        Me.GroupBox1.Location = New System.Drawing.Point(10, 3)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(612, 136)
        Me.GroupBox1.TabIndex = 38
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Parameters"
        '
        'selectAllBtn
        '
        Me.selectAllBtn.Location = New System.Drawing.Point(51, 407)
        Me.selectAllBtn.Name = "selectAllBtn"
        Me.selectAllBtn.Size = New System.Drawing.Size(75, 23)
        Me.selectAllBtn.TabIndex = 39
        Me.selectAllBtn.Text = "Select All"
        Me.selectAllBtn.UseVisualStyleBackColor = True
        '
        'pgbar
        '
        Me.pgbar.Location = New System.Drawing.Point(37, 351)
        Me.pgbar.MarqueeAnimationSpeed = 1
        Me.pgbar.Name = "pgbar"
        Me.pgbar.Size = New System.Drawing.Size(576, 10)
        Me.pgbar.TabIndex = 24
        '
        'checkbox1
        '
        Me.checkbox1.HeaderText = "Check Name"
        Me.checkbox1.Name = "checkbox1"
        '
        'DataGridView1
        '
        Me.DataGridView1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.checkbox1})
        Me.DataGridView1.Location = New System.Drawing.Point(37, 165)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.Size = New System.Drawing.Size(577, 171)
        Me.DataGridView1.TabIndex = 36
        '
        'dpStartDate
        '
        Me.dpStartDate.CustomFormat = ""
        Me.dpStartDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dpStartDate.Location = New System.Drawing.Point(79, 26)
        Me.dpStartDate.Name = "dpStartDate"
        Me.dpStartDate.Size = New System.Drawing.Size(87, 20)
        Me.dpStartDate.TabIndex = 7
        '
        'chkPayrollTimesheet
        '
        Me.chkPayrollTimesheet.AutoSize = True
        Me.chkPayrollTimesheet.Location = New System.Drawing.Point(320, 20)
        Me.chkPayrollTimesheet.Name = "chkPayrollTimesheet"
        Me.chkPayrollTimesheet.Size = New System.Drawing.Size(219, 17)
        Me.chkPayrollTimesheet.TabIndex = 53
        Me.chkPayrollTimesheet.Text = "Transfer time entries to payroll timesheets"
        Me.chkPayrollTimesheet.UseVisualStyleBackColor = True
        '
        'cbClass
        '
        Me.cbClass.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbClass.FormattingEnabled = True
        Me.cbClass.Items.AddRange(New Object() {"<None>", "CostCenter", "Department", "EmployeeType", "Milestone", "WorkType"})
        Me.cbClass.Location = New System.Drawing.Point(384, 43)
        Me.cbClass.Name = "cbClass"
        Me.cbClass.Size = New System.Drawing.Size(219, 21)
        Me.cbClass.TabIndex = 54
        '
        'cbPayrollItem
        '
        Me.cbPayrollItem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbPayrollItem.FormattingEnabled = True
        Me.cbPayrollItem.Items.AddRange(New Object() {"<None>", "CostCenter", "Department", "EmployeeType", "Milestone", "WorkType"})
        Me.cbPayrollItem.Location = New System.Drawing.Point(384, 69)
        Me.cbPayrollItem.Name = "cbPayrollItem"
        Me.cbPayrollItem.Size = New System.Drawing.Size(219, 21)
        Me.cbPayrollItem.TabIndex = 52
        '
        'lblPayrollItem
        '
        Me.lblPayrollItem.AutoSize = True
        Me.lblPayrollItem.Location = New System.Drawing.Point(314, 72)
        Me.lblPayrollItem.Name = "lblPayrollItem"
        Me.lblPayrollItem.Size = New System.Drawing.Size(64, 13)
        Me.lblPayrollItem.TabIndex = 56
        Me.lblPayrollItem.Text = "Payroll Item:"
        '
        'lblClass
        '
        Me.lblClass.AutoSize = True
        Me.lblClass.Location = New System.Drawing.Point(343, 45)
        Me.lblClass.Name = "lblClass"
        Me.lblClass.Size = New System.Drawing.Size(35, 13)
        Me.lblClass.TabIndex = 55
        Me.lblClass.Text = "Class:"
        '
        'rbJob
        '
        Me.rbJob.AutoSize = True
        Me.rbJob.Checked = True
        Me.rbJob.Location = New System.Drawing.Point(89, 103)
        Me.rbJob.Name = "rbJob"
        Me.rbJob.Size = New System.Drawing.Size(96, 17)
        Me.rbJob.TabIndex = 43
        Me.rbJob.TabStop = True
        Me.rbJob.Text = "Jobs/Sub Jobs"
        Me.rbJob.UseVisualStyleBackColor = True
        '
        'rbtJobitems
        '
        Me.rbtJobitems.AutoSize = True
        Me.rbtJobitems.Location = New System.Drawing.Point(6, 101)
        Me.rbtJobitems.Name = "rbtJobitems"
        Me.rbtJobitems.Size = New System.Drawing.Size(77, 17)
        Me.rbtJobitems.TabIndex = 57
        Me.rbtJobitems.Text = "Jobs/Items"
        Me.rbtJobitems.UseVisualStyleBackColor = True
        '
        'rbItem
        '
        Me.rbItem.AutoSize = True
        Me.rbItem.Location = New System.Drawing.Point(191, 103)
        Me.rbItem.Name = "rbItem"
        Me.rbItem.Size = New System.Drawing.Size(102, 17)
        Me.rbItem.TabIndex = 44
        Me.rbItem.Text = "Items/Sub Items"
        Me.rbItem.UseVisualStyleBackColor = True
        '
        'QBTimeTracking
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(635, 445)
        Me.Controls.Add(Me.pgbar)
        Me.Controls.Add(Me.selectAllBtn)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(btn_Cancel)
        Me.Controls.Add(Me.preWeek)
        Me.Controls.Add(Me.btn_Ok)
        Me.Controls.Add(Me.lblUptoDate)
        Me.Controls.Add(Me.lblFromDate)
        Me.Controls.Add(Me.dpEndDate)
        Me.Controls.Add(Me.dpStartDate)
        Me.Controls.Add(Me.GroupBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "QBTimeTracking"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "s"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblUptoDate As System.Windows.Forms.Label
    Friend WithEvents lblFromDate As System.Windows.Forms.Label
    Friend WithEvents dpEndDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents cbWageType As System.Windows.Forms.ComboBox
    Friend WithEvents lblWageType As System.Windows.Forms.Label
    Friend WithEvents btn_currentweek As Button
    Friend WithEvents btn_Ok As Button
    Friend WithEvents preWeek As Button
    Friend WithEvents nextWeek As Button
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents selectAllBtn As Button
    Friend WithEvents pgbar As ProgressBar
    Friend WithEvents checkbox1 As DataGridViewCheckBoxColumn
    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents dpStartDate As DateTimePicker
    Friend WithEvents chkPayrollTimesheet As CheckBox
    Friend WithEvents cbClass As ComboBox
    Friend WithEvents cbPayrollItem As ComboBox
    Friend WithEvents lblPayrollItem As Label
    Friend WithEvents lblClass As Label
    Friend WithEvents rbItem As RadioButton
    Friend WithEvents rbtJobitems As RadioButton
    Friend WithEvents rbJob As RadioButton
End Class
