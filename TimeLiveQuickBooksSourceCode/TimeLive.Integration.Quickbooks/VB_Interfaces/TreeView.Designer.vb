<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class TLQBTreeView
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
        Me.CustomerJobQBTreeView = New System.Windows.Forms.TreeView()
        Me.TextBoxKey = New System.Windows.Forms.TextBox()
        Me.AccountExpenseEntryTableAdapter1 = New TimeLive.Quickbooks.Integrator.TimeLiveDataSetTableAdapters.AccountExpenseEntryTableAdapter()
        Me.txtKey = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TextBoxName = New System.Windows.Forms.TextBox()
        Me.CustomerJobTLTreeView = New System.Windows.Forms.TreeView()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.TextBoxTimeLiveName = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.TextBoxTimeLiveID = New System.Windows.Forms.TextBox()
        Me.btnAddNodeTL = New System.Windows.Forms.Button()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.TextBoxFullNameTL = New System.Windows.Forms.TextBox()
        Me.ButtonTreeViewClose = New System.Windows.Forms.Button()
        Me.btnAddNodeQB = New System.Windows.Forms.Button()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.TextBoxFullNameQB = New System.Windows.Forms.TextBox()
        Me.SuspendLayout()
        '
        'CustomerJobQBTreeView
        '
        Me.CustomerJobQBTreeView.Location = New System.Drawing.Point(30, 54)
        Me.CustomerJobQBTreeView.Name = "CustomerJobQBTreeView"
        Me.CustomerJobQBTreeView.Size = New System.Drawing.Size(259, 329)
        Me.CustomerJobQBTreeView.TabIndex = 0
        '
        'TextBoxKey
        '
        Me.TextBoxKey.Location = New System.Drawing.Point(392, 53)
        Me.TextBoxKey.Name = "TextBoxKey"
        Me.TextBoxKey.Size = New System.Drawing.Size(235, 20)
        Me.TextBoxKey.TabIndex = 1
        '
        'AccountExpenseEntryTableAdapter1
        '
        Me.AccountExpenseEntryTableAdapter1.ClearBeforeFill = True
        '
        'txtKey
        '
        Me.txtKey.AutoSize = True
        Me.txtKey.Location = New System.Drawing.Point(312, 56)
        Me.txtKey.Name = "txtKey"
        Me.txtKey.Size = New System.Drawing.Size(74, 13)
        Me.txtKey.TabIndex = 20
        Me.txtKey.Text = "QuickBookID:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(295, 83)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(94, 13)
        Me.Label1.TabIndex = 22
        Me.Label1.Text = "QuickBook Name:"
        '
        'TextBoxName
        '
        Me.TextBoxName.Location = New System.Drawing.Point(392, 79)
        Me.TextBoxName.Name = "TextBoxName"
        Me.TextBoxName.Size = New System.Drawing.Size(235, 20)
        Me.TextBoxName.TabIndex = 21
        '
        'CustomerJobTLTreeView
        '
        Me.CustomerJobTLTreeView.Location = New System.Drawing.Point(660, 53)
        Me.CustomerJobTLTreeView.Name = "CustomerJobTLTreeView"
        Me.CustomerJobTLTreeView.Size = New System.Drawing.Size(259, 329)
        Me.CustomerJobTLTreeView.TabIndex = 23
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(129, 24)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(65, 13)
        Me.Label2.TabIndex = 24
        Me.Label2.Text = "QuickBooks"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(756, 20)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(50, 13)
        Me.Label3.TabIndex = 25
        Me.Label3.Text = "TimeLive"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(378, 24)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(65, 13)
        Me.Label4.TabIndex = 26
        Me.Label4.Text = "QuickBooks"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(1000, 27)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(50, 13)
        Me.Label5.TabIndex = 31
        Me.Label5.Text = "TimeLive"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(925, 86)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(84, 13)
        Me.Label6.TabIndex = 30
        Me.Label6.Text = "TimeLive Name:"
        '
        'TextBoxTimeLiveName
        '
        Me.TextBoxTimeLiveName.Location = New System.Drawing.Point(1015, 83)
        Me.TextBoxTimeLiveName.Name = "TextBoxTimeLiveName"
        Me.TextBoxTimeLiveName.Size = New System.Drawing.Size(260, 20)
        Me.TextBoxTimeLiveName.TabIndex = 29
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(945, 60)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(67, 13)
        Me.Label7.TabIndex = 28
        Me.Label7.Text = "TimeLive ID:"
        '
        'TextBoxTimeLiveID
        '
        Me.TextBoxTimeLiveID.Location = New System.Drawing.Point(1015, 57)
        Me.TextBoxTimeLiveID.Name = "TextBoxTimeLiveID"
        Me.TextBoxTimeLiveID.Size = New System.Drawing.Size(260, 20)
        Me.TextBoxTimeLiveID.TabIndex = 27
        '
        'btnAddNodeTL
        '
        Me.btnAddNodeTL.Location = New System.Drawing.Point(937, 162)
        Me.btnAddNodeTL.Name = "btnAddNodeTL"
        Me.btnAddNodeTL.Size = New System.Drawing.Size(75, 23)
        Me.btnAddNodeTL.TabIndex = 33
        Me.btnAddNodeTL.Text = "Add Node"
        Me.btnAddNodeTL.UseVisualStyleBackColor = True
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(945, 109)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(57, 13)
        Me.Label8.TabIndex = 35
        Me.Label8.Text = "Full Name:"
        '
        'TextBoxFullNameTL
        '
        Me.TextBoxFullNameTL.Location = New System.Drawing.Point(1015, 109)
        Me.TextBoxFullNameTL.Name = "TextBoxFullNameTL"
        Me.TextBoxFullNameTL.Size = New System.Drawing.Size(260, 20)
        Me.TextBoxFullNameTL.TabIndex = 34
        '
        'ButtonTreeViewClose
        '
        Me.ButtonTreeViewClose.Location = New System.Drawing.Point(1031, 162)
        Me.ButtonTreeViewClose.Name = "ButtonTreeViewClose"
        Me.ButtonTreeViewClose.Size = New System.Drawing.Size(75, 23)
        Me.ButtonTreeViewClose.TabIndex = 36
        Me.ButtonTreeViewClose.Text = "Close"
        Me.ButtonTreeViewClose.UseVisualStyleBackColor = True
        '
        'btnAddNodeQB
        '
        Me.btnAddNodeQB.Location = New System.Drawing.Point(311, 162)
        Me.btnAddNodeQB.Name = "btnAddNodeQB"
        Me.btnAddNodeQB.Size = New System.Drawing.Size(75, 23)
        Me.btnAddNodeQB.TabIndex = 37
        Me.btnAddNodeQB.Text = "Add Node"
        Me.btnAddNodeQB.UseVisualStyleBackColor = True
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(311, 109)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(57, 13)
        Me.Label9.TabIndex = 39
        Me.Label9.Text = "Full Name:"
        '
        'TextBoxFullNameQB
        '
        Me.TextBoxFullNameQB.Location = New System.Drawing.Point(392, 109)
        Me.TextBoxFullNameQB.Name = "TextBoxFullNameQB"
        Me.TextBoxFullNameQB.Size = New System.Drawing.Size(235, 20)
        Me.TextBoxFullNameQB.TabIndex = 38
        '
        'TLQBTreeView
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1351, 451)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.TextBoxFullNameQB)
        Me.Controls.Add(Me.btnAddNodeQB)
        Me.Controls.Add(Me.ButtonTreeViewClose)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.TextBoxFullNameTL)
        Me.Controls.Add(Me.btnAddNodeTL)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.TextBoxTimeLiveName)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.TextBoxTimeLiveID)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.CustomerJobTLTreeView)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.TextBoxName)
        Me.Controls.Add(Me.txtKey)
        Me.Controls.Add(Me.TextBoxKey)
        Me.Controls.Add(Me.CustomerJobQBTreeView)
        Me.Name = "TLQBTreeView"
        Me.Text = "TreeView"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents CustomerJobQBTreeView As Windows.Forms.TreeView
    Friend WithEvents TextBoxKey As TextBox
    Friend WithEvents AccountExpenseEntryTableAdapter1 As TimeLiveDataSetTableAdapters.AccountExpenseEntryTableAdapter
    Friend WithEvents txtKey As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents TextBoxName As TextBox
    Friend WithEvents CustomerJobTLTreeView As Windows.Forms.TreeView
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents TextBoxTimeLiveName As TextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents TextBoxTimeLiveID As TextBox
    Friend WithEvents btnAddNodeTL As Button
    Friend WithEvents Label8 As Label
    Friend WithEvents TextBoxFullNameTL As TextBox
    Friend WithEvents ButtonTreeViewClose As Button
    Friend WithEvents btnAddNodeQB As Button
    Friend WithEvents Label9 As Label
    Friend WithEvents TextBoxFullNameQB As TextBox
End Class
