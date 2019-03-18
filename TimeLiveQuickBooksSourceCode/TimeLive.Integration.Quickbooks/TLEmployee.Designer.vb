<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class TLEmployee
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(TLEmployee))
        Me.btnAddEmployeeInTimeLive = New System.Windows.Forms.Button()
        Me.pgbar = New System.Windows.Forms.ProgressBar()
        Me.RichTextBox1 = New System.Windows.Forms.RichTextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'btnAddEmployeeInTimeLive
        '
        Me.btnAddEmployeeInTimeLive.Location = New System.Drawing.Point(2, 3)
        Me.btnAddEmployeeInTimeLive.Name = "btnAddEmployeeInTimeLive"
        Me.btnAddEmployeeInTimeLive.Size = New System.Drawing.Size(351, 75)
        Me.btnAddEmployeeInTimeLive.TabIndex = 0
        Me.btnAddEmployeeInTimeLive.Text = "Transfer QuickBooks Employees to TimeLive as Employees"
        Me.btnAddEmployeeInTimeLive.UseVisualStyleBackColor = True
        '
        'pgbar
        '
        Me.pgbar.Location = New System.Drawing.Point(2, 83)
        Me.pgbar.MarqueeAnimationSpeed = 1
        Me.pgbar.Name = "pgbar"
        Me.pgbar.RightToLeftLayout = True
        Me.pgbar.Size = New System.Drawing.Size(351, 10)
        Me.pgbar.TabIndex = 27
        '
        'RichTextBox1
        '
        Me.RichTextBox1.Location = New System.Drawing.Point(4, 23)
        Me.RichTextBox1.Name = "RichTextBox1"
        Me.RichTextBox1.Size = New System.Drawing.Size(346, 165)
        Me.RichTextBox1.TabIndex = 29
        Me.RichTextBox1.Text = ""
        Me.RichTextBox1.Visible = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(4, 5)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(137, 13)
        Me.Label1.TabIndex = 30
        Me.Label1.Text = "Results of the Transfer"
        Me.Label1.Visible = False
        '
        'TLEmployee
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(353, 95)
        Me.Controls.Add(Me.pgbar)
        Me.Controls.Add(Me.btnAddEmployeeInTimeLive)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.RichTextBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "TLEmployee"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "TimeLive QuickBooks Integration Manager"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnAddEmployeeInTimeLive As System.Windows.Forms.Button
    Friend WithEvents pgbar As System.Windows.Forms.ProgressBar
    Friend WithEvents RichTextBox1 As System.Windows.Forms.RichTextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
End Class
