<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class QBEmployee
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(QBEmployee))
        Me.btnAddEmployeesInQB = New System.Windows.Forms.Button()
        Me.pgbar = New System.Windows.Forms.ProgressBar()
        Me.SuspendLayout()
        '
        'btnAddEmployeesInQB
        '
        Me.btnAddEmployeesInQB.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAddEmployeesInQB.Location = New System.Drawing.Point(2, 2)
        Me.btnAddEmployeesInQB.Name = "btnAddEmployeesInQB"
        Me.btnAddEmployeesInQB.Size = New System.Drawing.Size(351, 75)
        Me.btnAddEmployeesInQB.TabIndex = 0
        Me.btnAddEmployeesInQB.Text = "Transfer TimeLive Employees to QuickBooks as Employees"
        Me.btnAddEmployeesInQB.UseVisualStyleBackColor = True
        '
        'pgbar
        '
        Me.pgbar.Location = New System.Drawing.Point(2, 81)
        Me.pgbar.MarqueeAnimationSpeed = 1
        Me.pgbar.Name = "pgbar"
        Me.pgbar.Size = New System.Drawing.Size(351, 10)
        Me.pgbar.TabIndex = 1
        '
        'QBEmployee
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(354, 93)
        Me.Controls.Add(Me.pgbar)
        Me.Controls.Add(Me.btnAddEmployeesInQB)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "QBEmployee"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "TimeLive QuickBooks Integration Manager"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnAddEmployeesInQB As System.Windows.Forms.Button
    Friend WithEvents pgbar As System.Windows.Forms.ProgressBar
End Class
