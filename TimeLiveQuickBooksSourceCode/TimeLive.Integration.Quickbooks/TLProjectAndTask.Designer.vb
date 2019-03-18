<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class TLProjectAndTask
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(TLProjectAndTask))
        Me.btnAddProjectAndTaskInTimeLive = New System.Windows.Forms.Button()
        Me.pgbar = New System.Windows.Forms.ProgressBar()
        Me.rbItem = New System.Windows.Forms.RadioButton()
        Me.rbJob = New System.Windows.Forms.RadioButton()
        Me.SuspendLayout()
        '
        'btnAddProjectAndTaskInTimeLive
        '
        Me.btnAddProjectAndTaskInTimeLive.Location = New System.Drawing.Point(2, 29)
        Me.btnAddProjectAndTaskInTimeLive.Name = "btnAddProjectAndTaskInTimeLive"
        Me.btnAddProjectAndTaskInTimeLive.Size = New System.Drawing.Size(359, 75)
        Me.btnAddProjectAndTaskInTimeLive.TabIndex = 0
        Me.btnAddProjectAndTaskInTimeLive.Text = "Transfer Jobs/Items to TimeLive as Projects/Tasks"
        Me.btnAddProjectAndTaskInTimeLive.UseVisualStyleBackColor = True
        '
        'pgbar
        '
        Me.pgbar.Location = New System.Drawing.Point(2, 109)
        Me.pgbar.MarqueeAnimationSpeed = 1
        Me.pgbar.Name = "pgbar"
        Me.pgbar.Size = New System.Drawing.Size(359, 10)
        Me.pgbar.TabIndex = 28
        '
        'rbItem
        '
        Me.rbItem.AutoSize = True
        Me.rbItem.Location = New System.Drawing.Point(106, 7)
        Me.rbItem.Name = "rbItem"
        Me.rbItem.Size = New System.Drawing.Size(102, 17)
        Me.rbItem.TabIndex = 30
        Me.rbItem.Text = "Items/Sub Items"
        Me.rbItem.UseVisualStyleBackColor = True
        '
        'rbJob
        '
        Me.rbJob.AutoSize = True
        Me.rbJob.Checked = True
        Me.rbJob.Location = New System.Drawing.Point(2, 7)
        Me.rbJob.Name = "rbJob"
        Me.rbJob.Size = New System.Drawing.Size(96, 17)
        Me.rbJob.TabIndex = 29
        Me.rbJob.TabStop = True
        Me.rbJob.Text = "Jobs/Sub Jobs"
        Me.rbJob.UseVisualStyleBackColor = True
        '
        'TLProjectAndTask
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(361, 121)
        Me.Controls.Add(Me.rbItem)
        Me.Controls.Add(Me.rbJob)
        Me.Controls.Add(Me.pgbar)
        Me.Controls.Add(Me.btnAddProjectAndTaskInTimeLive)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "TLProjectAndTask"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "TimeLive QuickBooks Integration Manager"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnAddProjectAndTaskInTimeLive As System.Windows.Forms.Button
    Friend WithEvents pgbar As System.Windows.Forms.ProgressBar
    Friend WithEvents rbItem As System.Windows.Forms.RadioButton
    Friend WithEvents rbJob As System.Windows.Forms.RadioButton
End Class
