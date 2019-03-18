<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class QBVendor
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(QBVendor))
        Me.btnAddVendorInQuickBooks = New System.Windows.Forms.Button()
        Me.pgbar = New System.Windows.Forms.ProgressBar()
        Me.SuspendLayout()
        '
        'btnAddVendorInQuickBooks
        '
        Me.btnAddVendorInQuickBooks.Location = New System.Drawing.Point(2, 3)
        Me.btnAddVendorInQuickBooks.Name = "btnAddVendorInQuickBooks"
        Me.btnAddVendorInQuickBooks.Size = New System.Drawing.Size(358, 75)
        Me.btnAddVendorInQuickBooks.TabIndex = 2
        Me.btnAddVendorInQuickBooks.Text = "Transfer TimeLive Vendors to QuickBooks as Vendor"
        Me.btnAddVendorInQuickBooks.UseVisualStyleBackColor = True
        '
        'pgbar
        '
        Me.pgbar.Location = New System.Drawing.Point(2, 83)
        Me.pgbar.MarqueeAnimationSpeed = 1
        Me.pgbar.Name = "pgbar"
        Me.pgbar.Size = New System.Drawing.Size(358, 10)
        Me.pgbar.TabIndex = 24
        '
        'QBVendor
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(360, 94)
        Me.Controls.Add(Me.pgbar)
        Me.Controls.Add(Me.btnAddVendorInQuickBooks)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "QBVendor"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "TimeLive QuickBooks Integration Manager"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnAddVendorInQuickBooks As System.Windows.Forms.Button
    Friend WithEvents pgbar As System.Windows.Forms.ProgressBar
End Class
