<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class IntUI_2ndSelect
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
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.btnselectall = New System.Windows.Forms.Button()
        Me.bntclose = New System.Windows.Forms.Button()
        Me.btnTransfer = New System.Windows.Forms.Button()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.Lbel_processing = New System.Windows.Forms.Label()
        Me.Lbel_TotalHours = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.ckBox = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.GroupBox1.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ProgressBar1.Location = New System.Drawing.Point(26, 26)
        Me.ProgressBar1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.ProgressBar1.Maximum = 10
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(922, 22)
        Me.ProgressBar1.TabIndex = 0
        '
        'btnselectall
        '
        Me.btnselectall.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnselectall.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnselectall.Location = New System.Drawing.Point(22, 512)
        Me.btnselectall.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnselectall.Name = "btnselectall"
        Me.btnselectall.Size = New System.Drawing.Size(112, 35)
        Me.btnselectall.TabIndex = 42
        Me.btnselectall.Text = "Sellect All"
        Me.btnselectall.UseVisualStyleBackColor = True
        '
        'bntclose
        '
        Me.bntclose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.bntclose.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.bntclose.Location = New System.Drawing.Point(830, 512)
        Me.bntclose.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.bntclose.Name = "bntclose"
        Me.bntclose.Size = New System.Drawing.Size(112, 35)
        Me.bntclose.TabIndex = 41
        Me.bntclose.Text = "Close"
        Me.bntclose.UseVisualStyleBackColor = True
        '
        'btnTransfer
        '
        Me.btnTransfer.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnTransfer.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnTransfer.Location = New System.Drawing.Point(694, 512)
        Me.btnTransfer.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.btnTransfer.Name = "btnTransfer"
        Me.btnTransfer.Size = New System.Drawing.Size(112, 35)
        Me.btnTransfer.TabIndex = 40
        Me.btnTransfer.Text = "Process"
        Me.btnTransfer.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.ProgressBar1)
        Me.GroupBox1.Location = New System.Drawing.Point(4, 442)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.GroupBox1.Size = New System.Drawing.Size(952, 57)
        Me.GroupBox1.TabIndex = 44
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Progress Bar"
        '
        'Lbel_processing
        '
        Me.Lbel_processing.AutoSize = True
        Me.Lbel_processing.Location = New System.Drawing.Point(120, 14)
        Me.Lbel_processing.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Lbel_processing.Name = "Lbel_processing"
        Me.Lbel_processing.Size = New System.Drawing.Size(51, 20)
        Me.Lbel_processing.TabIndex = 45
        Me.Lbel_processing.Text = "Name"
        '
        'Lbel_TotalHours
        '
        Me.Lbel_TotalHours.AutoSize = True
        Me.Lbel_TotalHours.Location = New System.Drawing.Point(632, 14)
        Me.Lbel_TotalHours.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Lbel_TotalHours.Name = "Lbel_TotalHours"
        Me.Lbel_TotalHours.Size = New System.Drawing.Size(91, 20)
        Me.Lbel_TotalHours.TabIndex = 46
        Me.Lbel_TotalHours.Text = "Total Hours"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(18, 14)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(91, 20)
        Me.Label1.TabIndex = 47
        Me.Label1.Text = "Processing:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(528, 14)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(90, 20)
        Me.Label2.TabIndex = 48
        Me.Label2.Text = "Total  Time:"
        '
        'ckBox
        '
        Me.ckBox.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.ckBox.HeaderText = "Check Name"
        Me.ckBox.Name = "ckBox"
        '
        'DataGridView1
        '
        Me.DataGridView1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.ckBox})
        Me.DataGridView1.Location = New System.Drawing.Point(3, 46)
        Me.DataGridView1.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.Size = New System.Drawing.Size(958, 389)
        Me.DataGridView1.TabIndex = 43
        '
        'IntUI_2ndSelect
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 20.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(964, 571)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Lbel_TotalHours)
        Me.Controls.Add(Me.Lbel_processing)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.btnselectall)
        Me.Controls.Add(Me.bntclose)
        Me.Controls.Add(Me.btnTransfer)
        Me.Controls.Add(Me.GroupBox1)
        Me.Margin = New System.Windows.Forms.Padding(4, 5, 4, 5)
        Me.Name = "IntUI_2ndSelect"
        Me.Text = "Select Entries"
        Me.GroupBox1.ResumeLayout(False)
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ProgressBar1 As ProgressBar
    Friend WithEvents btnselectall As Button
    Friend WithEvents bntclose As Button
    Friend WithEvents btnTransfer As Button
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents Lbel_processing As Label
    Friend WithEvents Lbel_TotalHours As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents ckBox As DataGridViewCheckBoxColumn
    Friend WithEvents DataGridView1 As DataGridView
End Class
