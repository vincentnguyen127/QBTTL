<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class QBAccount
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(QBAccount))
        Me.btnAddAccountsInQB = New System.Windows.Forms.Button()
        Me.pgbar = New System.Windows.Forms.ProgressBar()
        Me.SuspendLayout()
        '
        'btnAddAccountsInQB
        '
        Me.btnAddAccountsInQB.Location = New System.Drawing.Point(3, 3)
        Me.btnAddAccountsInQB.Name = "btnAddAccountsInQB"
        Me.btnAddAccountsInQB.Size = New System.Drawing.Size(349, 75)
        Me.btnAddAccountsInQB.TabIndex = 0
        Me.btnAddAccountsInQB.Text = "Transfer TimeLive Expense Codes to QuickBooks as Accounts"
        Me.btnAddAccountsInQB.UseVisualStyleBackColor = True
        '
        'pgbar
        '
        Me.pgbar.Location = New System.Drawing.Point(3, 83)
        Me.pgbar.MarqueeAnimationSpeed = 1
        Me.pgbar.Name = "pgbar"
        Me.pgbar.Size = New System.Drawing.Size(349, 10)
        Me.pgbar.TabIndex = 2
        '
        'QBAccount
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(353, 94)
        Me.Controls.Add(Me.pgbar)
        Me.Controls.Add(Me.btnAddAccountsInQB)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "QBAccount"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "TimeLive QuickBooks Integration Manager"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnAddAccountsInQB As System.Windows.Forms.Button
    Friend WithEvents pgbar As System.Windows.Forms.ProgressBar
End Class
