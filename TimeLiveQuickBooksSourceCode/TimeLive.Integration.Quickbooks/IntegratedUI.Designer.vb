<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class IntegratedUI
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
        Me.btnselectall = New System.Windows.Forms.Button()
        Me.bntclose = New System.Windows.Forms.Button()
        Me.btnTransfer = New System.Windows.Forms.Button()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.ckBox = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.TabPageTimeTransfer = New System.Windows.Forms.TabPage()
        Me.cbWageType = New System.Windows.Forms.ComboBox()
        Me.lblWageType = New System.Windows.Forms.Label()
        Me.preWeek = New System.Windows.Forms.Button()
        Me.lblUptoDate = New System.Windows.Forms.Label()
        Me.lblFromDate = New System.Windows.Forms.Label()
        Me.dpEndDate = New System.Windows.Forms.DateTimePicker()
        Me.dpStartDate = New System.Windows.Forms.DateTimePicker()
        Me.nextWeek = New System.Windows.Forms.Button()
        Me.btn_currentweek = New System.Windows.Forms.Button()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPageCustomers = New System.Windows.Forms.TabPage()
        Me.TabPageEmployees = New System.Windows.Forms.TabPage()
        Me.TabPageVendor = New System.Windows.Forms.TabPage()
        Me.TabPageJobsItems = New System.Windows.Forms.TabPage()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox1.SuspendLayout()
        Me.TabPageTimeTransfer.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnselectall
        '
        Me.btnselectall.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnselectall.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnselectall.Location = New System.Drawing.Point(12, 399)
        Me.btnselectall.Name = "btnselectall"
        Me.btnselectall.Size = New System.Drawing.Size(75, 23)
        Me.btnselectall.TabIndex = 33
        Me.btnselectall.Text = "Select All"
        Me.btnselectall.UseVisualStyleBackColor = True
        '
        'bntclose
        '
        Me.bntclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.bntclose.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.bntclose.Location = New System.Drawing.Point(552, 399)
        Me.bntclose.Name = "bntclose"
        Me.bntclose.Size = New System.Drawing.Size(75, 23)
        Me.bntclose.TabIndex = 32
        Me.bntclose.Text = "Close"
        Me.bntclose.UseVisualStyleBackColor = True
        '
        'btnTransfer
        '
        Me.btnTransfer.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnTransfer.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnTransfer.Location = New System.Drawing.Point(462, 399)
        Me.btnTransfer.Name = "btnTransfer"
        Me.btnTransfer.Size = New System.Drawing.Size(75, 23)
        Me.btnTransfer.TabIndex = 31
        Me.btnTransfer.Text = "Process"
        Me.btnTransfer.UseVisualStyleBackColor = True
        '
        'DataGridView1
        '
        Me.DataGridView1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.ckBox})
        Me.DataGridView1.Location = New System.Drawing.Point(0, 109)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.Size = New System.Drawing.Size(639, 242)
        Me.DataGridView1.TabIndex = 38
        '
        'ckBox
        '
        Me.ckBox.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.ckBox.HeaderText = "Check Name"
        Me.ckBox.Name = "ckBox"
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.ProgressBar1)
        Me.GroupBox1.Location = New System.Drawing.Point(2, 356)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(635, 37)
        Me.GroupBox1.TabIndex = 39
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Progress Bar"
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ProgressBar1.Location = New System.Drawing.Point(10, 17)
        Me.ProgressBar1.Maximum = 10
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(615, 14)
        Me.ProgressBar1.TabIndex = 0
        '
        'TabPageTimeTransfer
        '
        Me.TabPageTimeTransfer.Controls.Add(Me.cbWageType)
        Me.TabPageTimeTransfer.Controls.Add(Me.lblWageType)
        Me.TabPageTimeTransfer.Controls.Add(Me.preWeek)
        Me.TabPageTimeTransfer.Controls.Add(Me.lblUptoDate)
        Me.TabPageTimeTransfer.Controls.Add(Me.lblFromDate)
        Me.TabPageTimeTransfer.Controls.Add(Me.dpEndDate)
        Me.TabPageTimeTransfer.Controls.Add(Me.dpStartDate)
        Me.TabPageTimeTransfer.Controls.Add(Me.nextWeek)
        Me.TabPageTimeTransfer.Controls.Add(Me.btn_currentweek)
        Me.TabPageTimeTransfer.Location = New System.Drawing.Point(4, 22)
        Me.TabPageTimeTransfer.Name = "TabPageTimeTransfer"
        Me.TabPageTimeTransfer.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageTimeTransfer.Size = New System.Drawing.Size(629, 82)
        Me.TabPageTimeTransfer.TabIndex = 2
        Me.TabPageTimeTransfer.Text = "Time Transfer Options"
        Me.TabPageTimeTransfer.UseVisualStyleBackColor = True
        '
        'cbWageType
        '
        Me.cbWageType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbWageType.FormattingEnabled = True
        Me.cbWageType.Items.AddRange(New Object() {"Bonus", "Comission", "Hourly-Overtime", "Hourly-Regular", "Hourly-Sick", "Hourly-Vacation", "Salary-Regular", "Salary-Sick", "Salary-Vacation"})
        Me.cbWageType.Location = New System.Drawing.Point(402, 18)
        Me.cbWageType.Name = "cbWageType"
        Me.cbWageType.Size = New System.Drawing.Size(219, 21)
        Me.cbWageType.TabIndex = 43
        '
        'lblWageType
        '
        Me.lblWageType.AutoSize = True
        Me.lblWageType.Location = New System.Drawing.Point(330, 22)
        Me.lblWageType.Name = "lblWageType"
        Me.lblWageType.Size = New System.Drawing.Size(66, 13)
        Me.lblWageType.TabIndex = 44
        Me.lblWageType.Text = "Wage Type:"
        '
        'preWeek
        '
        Me.preWeek.Location = New System.Drawing.Point(48, 40)
        Me.preWeek.Name = "preWeek"
        Me.preWeek.Size = New System.Drawing.Size(50, 23)
        Me.preWeek.TabIndex = 41
        Me.preWeek.Text = "<<"
        Me.preWeek.UseVisualStyleBackColor = True
        '
        'lblUptoDate
        '
        Me.lblUptoDate.AutoSize = True
        Me.lblUptoDate.Location = New System.Drawing.Point(141, 21)
        Me.lblUptoDate.Name = "lblUptoDate"
        Me.lblUptoDate.Size = New System.Drawing.Size(24, 13)
        Me.lblUptoDate.TabIndex = 39
        Me.lblUptoDate.Text = "Up:"
        '
        'lblFromDate
        '
        Me.lblFromDate.AutoSize = True
        Me.lblFromDate.Location = New System.Drawing.Point(9, 22)
        Me.lblFromDate.Name = "lblFromDate"
        Me.lblFromDate.Size = New System.Drawing.Size(33, 13)
        Me.lblFromDate.TabIndex = 38
        Me.lblFromDate.Text = "From:"
        '
        'dpEndDate
        '
        Me.dpEndDate.CustomFormat = ""
        Me.dpEndDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dpEndDate.Location = New System.Drawing.Point(171, 14)
        Me.dpEndDate.Name = "dpEndDate"
        Me.dpEndDate.Size = New System.Drawing.Size(87, 20)
        Me.dpEndDate.TabIndex = 37
        '
        'dpStartDate
        '
        Me.dpStartDate.CustomFormat = ""
        Me.dpStartDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dpStartDate.Location = New System.Drawing.Point(48, 15)
        Me.dpStartDate.Name = "dpStartDate"
        Me.dpStartDate.Size = New System.Drawing.Size(87, 20)
        Me.dpStartDate.TabIndex = 36
        '
        'nextWeek
        '
        Me.nextWeek.Location = New System.Drawing.Point(208, 40)
        Me.nextWeek.Name = "nextWeek"
        Me.nextWeek.Size = New System.Drawing.Size(50, 23)
        Me.nextWeek.TabIndex = 42
        Me.nextWeek.Text = ">>"
        Me.nextWeek.UseVisualStyleBackColor = True
        '
        'btn_currentweek
        '
        Me.btn_currentweek.Location = New System.Drawing.Point(104, 40)
        Me.btn_currentweek.Name = "btn_currentweek"
        Me.btn_currentweek.Size = New System.Drawing.Size(98, 23)
        Me.btn_currentweek.TabIndex = 40
        Me.btn_currentweek.Text = "Current Week"
        Me.btn_currentweek.UseVisualStyleBackColor = True
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPageCustomers)
        Me.TabControl1.Controls.Add(Me.TabPageEmployees)
        Me.TabControl1.Controls.Add(Me.TabPageVendor)
        Me.TabControl1.Controls.Add(Me.TabPageJobsItems)
        Me.TabControl1.Controls.Add(Me.TabPageTimeTransfer)
        Me.TabControl1.Location = New System.Drawing.Point(2, -1)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(637, 108)
        Me.TabControl1.TabIndex = 40
        '
        'TabPageCustomers
        '
        Me.TabPageCustomers.Location = New System.Drawing.Point(4, 22)
        Me.TabPageCustomers.Name = "TabPageCustomers"
        Me.TabPageCustomers.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageCustomers.Size = New System.Drawing.Size(629, 82)
        Me.TabPageCustomers.TabIndex = 3
        Me.TabPageCustomers.Text = "Customers Options"
        Me.TabPageCustomers.UseVisualStyleBackColor = True
        '
        'TabPageEmployees
        '
        Me.TabPageEmployees.Location = New System.Drawing.Point(4, 22)
        Me.TabPageEmployees.Name = "TabPageEmployees"
        Me.TabPageEmployees.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageEmployees.Size = New System.Drawing.Size(629, 82)
        Me.TabPageEmployees.TabIndex = 4
        Me.TabPageEmployees.Text = "Employees Options"
        Me.TabPageEmployees.UseVisualStyleBackColor = True
        '
        'TabPageVendor
        '
        Me.TabPageVendor.Location = New System.Drawing.Point(4, 22)
        Me.TabPageVendor.Name = "TabPageVendor"
        Me.TabPageVendor.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPageVendor.Size = New System.Drawing.Size(629, 82)
        Me.TabPageVendor.TabIndex = 5
        Me.TabPageVendor.Text = "Vendor"
        Me.TabPageVendor.UseVisualStyleBackColor = True
        '
        'TabPageJobsItems
        '
        Me.TabPageJobsItems.Location = New System.Drawing.Point(4, 22)
        Me.TabPageJobsItems.Name = "TabPageJobsItems"
        Me.TabPageJobsItems.Size = New System.Drawing.Size(629, 82)
        Me.TabPageJobsItems.TabIndex = 6
        Me.TabPageJobsItems.Text = "Jobs/Items"
        Me.TabPageJobsItems.UseVisualStyleBackColor = True
        '
        'IntegratedUI
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(639, 433)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.btnselectall)
        Me.Controls.Add(Me.bntclose)
        Me.Controls.Add(Me.btnTransfer)
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "IntegratedUI"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "IntegratedUI"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox1.ResumeLayout(False)
        Me.TabPageTimeTransfer.ResumeLayout(False)
        Me.TabPageTimeTransfer.PerformLayout()
        Me.TabControl1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents btnselectall As Button
    Friend WithEvents bntclose As Button
    Friend WithEvents btnTransfer As Button
    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents ckBox As DataGridViewCheckBoxColumn
    Friend WithEvents ProgressBar1 As ProgressBar
    Friend WithEvents TabPageTimeTransfer As TabPage
    Friend WithEvents cbWageType As ComboBox
    Friend WithEvents lblWageType As Label
    Friend WithEvents preWeek As Button
    Friend WithEvents lblUptoDate As Label
    Friend WithEvents lblFromDate As Label
    Friend WithEvents dpEndDate As DateTimePicker
    Friend WithEvents dpStartDate As DateTimePicker
    Friend WithEvents nextWeek As Button
    Friend WithEvents btn_currentweek As Button
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPageCustomers As TabPage
    Friend WithEvents TabPageEmployees As TabPage
    Friend WithEvents TabPageVendor As TabPage
    Friend WithEvents TabPageJobsItems As TabPage
End Class
