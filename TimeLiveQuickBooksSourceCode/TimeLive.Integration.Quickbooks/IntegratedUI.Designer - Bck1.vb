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
        Me.sellectAll = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.btnTransfer = New System.Windows.Forms.Button()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.ckBox = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.localTB1 = New System.Windows.Forms.TextBox()
        Me.pgbar = New System.Windows.Forms.ProgressBar()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabPage2.SuspendLayout()
        Me.SuspendLayout()
        '
        'sellectAll
        '
        Me.sellectAll.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.sellectAll.Location = New System.Drawing.Point(44, 404)
        Me.sellectAll.Name = "sellectAll"
        Me.sellectAll.Size = New System.Drawing.Size(75, 23)
        Me.sellectAll.TabIndex = 33
        Me.sellectAll.Text = "Sellect All"
        Me.sellectAll.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Button1.Location = New System.Drawing.Point(528, 404)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 32
        Me.Button1.Text = "Cancel"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'btnTransfer
        '
        Me.btnTransfer.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnTransfer.Location = New System.Drawing.Point(437, 404)
        Me.btnTransfer.Name = "btnTransfer"
        Me.btnTransfer.Size = New System.Drawing.Size(75, 23)
        Me.btnTransfer.TabIndex = 31
        Me.btnTransfer.Text = "Emulate"
        Me.btnTransfer.UseVisualStyleBackColor = True
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Location = New System.Drawing.Point(13, 146)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(613, 247)
        Me.TabControl1.TabIndex = 34
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
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.ckBox})
        Me.DataGridView1.Location = New System.Drawing.Point(29, 19)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.Size = New System.Drawing.Size(546, 182)
        Me.DataGridView1.TabIndex = 37
        '
        'ckBox
        '
        Me.ckBox.HeaderText = "Check Name"
        Me.ckBox.Name = "ckBox"
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
        Me.localTB1.Size = New System.Drawing.Size(605, 193)
        Me.localTB1.TabIndex = 1
        '
        'pgbar
        '
        Me.pgbar.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.pgbar.Location = New System.Drawing.Point(3, 199)
        Me.pgbar.MarqueeAnimationSpeed = 1
        Me.pgbar.Name = "pgbar"
        Me.pgbar.Size = New System.Drawing.Size(598, 10)
        Me.pgbar.TabIndex = 26
        '
        'IntegratedUI
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(639, 449)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.sellectAll)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.btnTransfer)
        Me.Name = "IntegratedUI"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "IntegratedUI"
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents sellectAll As Button
    Friend WithEvents Button1 As Button
    Friend WithEvents btnTransfer As Button
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents localTB1 As TextBox
    Friend WithEvents pgbar As ProgressBar
    Friend WithEvents ckBox As DataGridViewCheckBoxColumn
End Class
