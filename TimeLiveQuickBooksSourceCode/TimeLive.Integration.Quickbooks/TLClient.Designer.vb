<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class TLClient
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(TLClient))
        Me.btnAddClientInTimeLive = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.checkbox1 = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.dataV_custRet = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dataV_Name = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dataV_ID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.localTB1 = New System.Windows.Forms.TextBox()
        Me.pgbar = New System.Windows.Forms.ProgressBar()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.sellectAll = New System.Windows.Forms.Button()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnAddClientInTimeLive
        '
        Me.btnAddClientInTimeLive.Location = New System.Drawing.Point(448, 405)
        Me.btnAddClientInTimeLive.Name = "btnAddClientInTimeLive"
        Me.btnAddClientInTimeLive.Size = New System.Drawing.Size(75, 23)
        Me.btnAddClientInTimeLive.TabIndex = 0
        Me.btnAddClientInTimeLive.Text = "Emulate"
        Me.btnAddClientInTimeLive.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(539, 405)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 27
        Me.Button1.Text = "Cancel"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Location = New System.Drawing.Point(13, 146)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(613, 247)
        Me.TabControl1.TabIndex = 28
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.DataGridView1)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(605, 221)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Customers"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'DataGridView1
        '
        Me.DataGridView1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.checkbox1, Me.dataV_custRet, Me.dataV_Name, Me.dataV_ID})
        Me.DataGridView1.Location = New System.Drawing.Point(29, 19)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.Size = New System.Drawing.Size(546, 182)
        Me.DataGridView1.TabIndex = 37
        '
        'checkbox1
        '
        Me.checkbox1.HeaderText = "Check Name"
        Me.checkbox1.Name = "checkbox1"
        '
        'dataV_custRet
        '
        Me.dataV_custRet.HeaderText = "CustomerObj"
        Me.dataV_custRet.Name = "dataV_custRet"
        '
        'dataV_Name
        '
        Me.dataV_Name.HeaderText = "Name"
        Me.dataV_Name.Name = "dataV_Name"
        '
        'dataV_ID
        '
        Me.dataV_ID.HeaderText = "ListID"
        Me.dataV_ID.Name = "dataV_ID"
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.localTB1)
        Me.TabPage2.Controls.Add(Me.pgbar)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(605, 221)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Logs"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'localTB1
        '
        Me.localTB1.BackColor = System.Drawing.SystemColors.ControlText
        Me.localTB1.ForeColor = System.Drawing.SystemColors.Window
        Me.localTB1.Location = New System.Drawing.Point(0, 0)
        Me.localTB1.Multiline = True
        Me.localTB1.Name = "localTB1"
        Me.localTB1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.localTB1.Size = New System.Drawing.Size(599, 193)
        Me.localTB1.TabIndex = 1
        '
        'pgbar
        '
        Me.pgbar.Location = New System.Drawing.Point(0, 199)
        Me.pgbar.MarqueeAnimationSpeed = 1
        Me.pgbar.Name = "pgbar"
        Me.pgbar.Size = New System.Drawing.Size(598, 10)
        Me.pgbar.TabIndex = 26
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.GroupBox2)
        Me.GroupBox1.Location = New System.Drawing.Point(16, 7)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(612, 136)
        Me.GroupBox1.TabIndex = 29
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Parameters"
        '
        'GroupBox2
        '
        Me.GroupBox2.Location = New System.Drawing.Point(10, 72)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(341, 47)
        Me.GroupBox2.TabIndex = 0
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "TBA"
        '
        'sellectAll
        '
        Me.sellectAll.Location = New System.Drawing.Point(55, 405)
        Me.sellectAll.Name = "sellectAll"
        Me.sellectAll.Size = New System.Drawing.Size(75, 23)
        Me.sellectAll.TabIndex = 30
        Me.sellectAll.Text = "Sellect All"
        Me.sellectAll.UseVisualStyleBackColor = True
        '
        'TLClient
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(635, 445)
        Me.Controls.Add(Me.sellectAll)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.btnAddClientInTimeLive)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "TLClient"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "TimeLive QuickBooks Integration Manager"
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage2.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnAddClientInTimeLive As System.Windows.Forms.Button
    Friend WithEvents Button1 As Button
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents localTB1 As TextBox
    Friend WithEvents pgbar As ProgressBar
    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents checkbox1 As DataGridViewCheckBoxColumn
    Friend WithEvents dataV_custRet As DataGridViewTextBoxColumn
    Friend WithEvents dataV_Name As DataGridViewTextBoxColumn
    Friend WithEvents dataV_ID As DataGridViewTextBoxColumn
    Friend WithEvents sellectAll As Button
End Class
