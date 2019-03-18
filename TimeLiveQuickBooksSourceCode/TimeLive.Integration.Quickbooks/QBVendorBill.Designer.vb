<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class QBVendorBill
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(QBVendorBill))
        Me.btnGetAndAddTimeEntries = New System.Windows.Forms.Button()
        Me.dpEndDate = New System.Windows.Forms.DateTimePicker()
        Me.dpStartDate = New System.Windows.Forms.DateTimePicker()
        Me.lblFromDate = New System.Windows.Forms.Label()
        Me.lblUptoDate = New System.Windows.Forms.Label()
        Me.lblVendorName = New System.Windows.Forms.Label()
        Me.cbVendor = New System.Windows.Forms.ComboBox()
        Me.pgbar = New System.Windows.Forms.ProgressBar()
        Me.SuspendLayout()
        '
        'btnGetAndAddTimeEntries
        '
        Me.btnGetAndAddTimeEntries.Location = New System.Drawing.Point(3, 75)
        Me.btnGetAndAddTimeEntries.Name = "btnGetAndAddTimeEntries"
        Me.btnGetAndAddTimeEntries.Size = New System.Drawing.Size(349, 75)
        Me.btnGetAndAddTimeEntries.TabIndex = 1
        Me.btnGetAndAddTimeEntries.Text = "Transfer TimeLive Expense Entries to QuickBooks as Vendor Bills"
        Me.btnGetAndAddTimeEntries.UseVisualStyleBackColor = True
        '
        'dpEndDate
        '
        Me.dpEndDate.CustomFormat = ""
        Me.dpEndDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dpEndDate.Location = New System.Drawing.Point(91, 50)
        Me.dpEndDate.Name = "dpEndDate"
        Me.dpEndDate.Size = New System.Drawing.Size(87, 20)
        Me.dpEndDate.TabIndex = 4
        '
        'dpStartDate
        '
        Me.dpStartDate.CustomFormat = ""
        Me.dpStartDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dpStartDate.Location = New System.Drawing.Point(91, 29)
        Me.dpStartDate.Name = "dpStartDate"
        Me.dpStartDate.Size = New System.Drawing.Size(87, 20)
        Me.dpStartDate.TabIndex = 3
        '
        'lblFromDate
        '
        Me.lblFromDate.AutoSize = True
        Me.lblFromDate.Location = New System.Drawing.Point(31, 33)
        Me.lblFromDate.Name = "lblFromDate"
        Me.lblFromDate.Size = New System.Drawing.Size(59, 13)
        Me.lblFromDate.TabIndex = 5
        Me.lblFromDate.Text = "From Date:"
        '
        'lblUptoDate
        '
        Me.lblUptoDate.AutoSize = True
        Me.lblUptoDate.Location = New System.Drawing.Point(31, 54)
        Me.lblUptoDate.Name = "lblUptoDate"
        Me.lblUptoDate.Size = New System.Drawing.Size(59, 13)
        Me.lblUptoDate.TabIndex = 6
        Me.lblUptoDate.Text = "Upto Date:"
        '
        'lblVendorName
        '
        Me.lblVendorName.AutoSize = True
        Me.lblVendorName.Location = New System.Drawing.Point(15, 10)
        Me.lblVendorName.Name = "lblVendorName"
        Me.lblVendorName.Size = New System.Drawing.Size(75, 13)
        Me.lblVendorName.TabIndex = 22
        Me.lblVendorName.Text = "Vendor Name:"
        '
        'cbVendor
        '
        Me.cbVendor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbVendor.FormattingEnabled = True
        Me.cbVendor.Location = New System.Drawing.Point(91, 6)
        Me.cbVendor.Name = "cbVendor"
        Me.cbVendor.Size = New System.Drawing.Size(261, 21)
        Me.cbVendor.TabIndex = 21
        '
        'pgbar
        '
        Me.pgbar.Location = New System.Drawing.Point(3, 155)
        Me.pgbar.MarqueeAnimationSpeed = 1
        Me.pgbar.Name = "pgbar"
        Me.pgbar.Size = New System.Drawing.Size(349, 10)
        Me.pgbar.TabIndex = 25
        '
        'QBVendorBill
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(352, 167)
        Me.Controls.Add(Me.pgbar)
        Me.Controls.Add(Me.lblVendorName)
        Me.Controls.Add(Me.cbVendor)
        Me.Controls.Add(Me.lblUptoDate)
        Me.Controls.Add(Me.lblFromDate)
        Me.Controls.Add(Me.dpEndDate)
        Me.Controls.Add(Me.dpStartDate)
        Me.Controls.Add(Me.btnGetAndAddTimeEntries)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "QBVendorBill"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "TimeLive QuickBooks Integration Manager"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnGetAndAddTimeEntries As System.Windows.Forms.Button
    Friend WithEvents dpEndDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents dpStartDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblFromDate As System.Windows.Forms.Label
    Friend WithEvents lblUptoDate As System.Windows.Forms.Label
    Friend WithEvents lblVendorName As System.Windows.Forms.Label
    Friend WithEvents cbVendor As System.Windows.Forms.ComboBox
    Friend WithEvents pgbar As System.Windows.Forms.ProgressBar
End Class
