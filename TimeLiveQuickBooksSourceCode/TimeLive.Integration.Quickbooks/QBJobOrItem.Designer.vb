<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class QBJobOrItem
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(QBJobOrItem))
        Me.btnGetAndAddJobOrItem = New System.Windows.Forms.Button()
        Me.rbJob = New System.Windows.Forms.RadioButton()
        Me.rbItem = New System.Windows.Forms.RadioButton()
        Me.rbtJobitems = New System.Windows.Forms.RadioButton()
        Me.pgbar = New System.Windows.Forms.ProgressBar()
        Me.SuspendLayout()
        '
        'btnGetAndAddJobOrItem
        '
        Me.btnGetAndAddJobOrItem.Location = New System.Drawing.Point(3, 32)
        Me.btnGetAndAddJobOrItem.Name = "btnGetAndAddJobOrItem"
        Me.btnGetAndAddJobOrItem.Size = New System.Drawing.Size(361, 75)
        Me.btnGetAndAddJobOrItem.TabIndex = 0
        Me.btnGetAndAddJobOrItem.Text = "Transfer TimeLive Projects/Tasks to QuickBooks as Jobs/Items"
        Me.btnGetAndAddJobOrItem.UseVisualStyleBackColor = True
        '
        'rbJob
        '
        Me.rbJob.AutoSize = True
        Me.rbJob.Location = New System.Drawing.Point(88, 8)
        Me.rbJob.Name = "rbJob"
        Me.rbJob.Size = New System.Drawing.Size(96, 17)
        Me.rbJob.TabIndex = 1
        Me.rbJob.TabStop = True
        Me.rbJob.Text = "Jobs/Sub Jobs"
        Me.rbJob.UseVisualStyleBackColor = True
        '
        'rbItem
        '
        Me.rbItem.AutoSize = True
        Me.rbItem.Location = New System.Drawing.Point(192, 8)
        Me.rbItem.Name = "rbItem"
        Me.rbItem.Size = New System.Drawing.Size(102, 17)
        Me.rbItem.TabIndex = 2
        Me.rbItem.TabStop = True
        Me.rbItem.Text = "Items/Sub Items"
        Me.rbItem.UseVisualStyleBackColor = True
        '
        'rbtJobitems
        '
        Me.rbtJobitems.AutoSize = True
        Me.rbtJobitems.Checked = True
        Me.rbtJobitems.Location = New System.Drawing.Point(3, 8)
        Me.rbtJobitems.Name = "rbtJobitems"
        Me.rbtJobitems.Size = New System.Drawing.Size(77, 17)
        Me.rbtJobitems.TabIndex = 3
        Me.rbtJobitems.TabStop = True
        Me.rbtJobitems.Text = "Jobs/Items"
        Me.rbtJobitems.UseVisualStyleBackColor = True
        '
        'pgbar
        '
        Me.pgbar.Location = New System.Drawing.Point(3, 111)
        Me.pgbar.MarqueeAnimationSpeed = 1
        Me.pgbar.Name = "pgbar"
        Me.pgbar.Size = New System.Drawing.Size(361, 10)
        Me.pgbar.TabIndex = 4
        '
        'QBJobOrItem
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(365, 124)
        Me.Controls.Add(Me.pgbar)
        Me.Controls.Add(Me.rbtJobitems)
        Me.Controls.Add(Me.rbItem)
        Me.Controls.Add(Me.rbJob)
        Me.Controls.Add(Me.btnGetAndAddJobOrItem)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "QBJobOrItem"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "TimeLive QuickBooks Integration Manager"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnGetAndAddJobOrItem As System.Windows.Forms.Button
    Friend WithEvents rbJob As System.Windows.Forms.RadioButton
    Friend WithEvents rbItem As System.Windows.Forms.RadioButton
    Friend WithEvents rbtJobitems As System.Windows.Forms.RadioButton
    Friend WithEvents pgbar As System.Windows.Forms.ProgressBar
End Class
