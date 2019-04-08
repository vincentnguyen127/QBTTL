<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class MAIN
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MAIN))
        Me.StatusStrip = New System.Windows.Forms.StatusStrip()
        Me.CurrentTime = New System.Windows.Forms.ToolStripStatusLabel()
        Me.NextProcessingTime = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ToolTip = New System.Windows.Forms.ToolTip(Me.components)
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip()
        Me.ToolStripLabel1 = New System.Windows.Forms.ToolStripLabel()
        Me.customer_btn = New System.Windows.Forms.ToolStripButton()
        Me.Exitbtn = New System.Windows.Forms.ToolStripButton()
        Me.clearlogbtn = New System.Windows.Forms.ToolStripButton()
        Me.btn_relationships = New System.Windows.Forms.ToolStripButton()
        Me.QBTime2SQLbtn = New System.Windows.Forms.ToolStripButton()
        Me.btn_systemsync = New System.Windows.Forms.ToolStripButton()
        Me.settingbtn = New System.Windows.Forms.ToolStripButton()
        Me.loginbtn = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripLabel2 = New System.Windows.Forms.ToolStripLabel()
        Me.employees_btn = New System.Windows.Forms.ToolStripButton()
        Me.vendor_btn = New System.Windows.Forms.ToolStripButton()
        Me.jobs_items_btn = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.ToolStripLabel3 = New System.Windows.Forms.ToolStripLabel()
        Me.timeentries_btn = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripButton2 = New System.Windows.Forms.ToolStripButton()
        Me.StatusWindow = New System.Windows.Forms.TextBox()
        Me.StatusStrip.SuspendLayout()
        Me.ToolStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'StatusStrip
        '
        Me.StatusStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.CurrentTime, Me.NextProcessingTime})
        Me.StatusStrip.Location = New System.Drawing.Point(0, 549)
        Me.StatusStrip.Name = "StatusStrip"
        Me.StatusStrip.Size = New System.Drawing.Size(860, 24)
        Me.StatusStrip.TabIndex = 7
        Me.StatusStrip.Text = "StatusStrip"
        '
        'CurrentTime
        '
        Me.CurrentTime.AutoSize = False
        Me.CurrentTime.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.CurrentTime.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenInner
        Me.CurrentTime.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.CurrentTime.Name = "CurrentTime"
        Me.CurrentTime.Size = New System.Drawing.Size(100, 19)
        Me.CurrentTime.Text = "Current Time"
        Me.CurrentTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.CurrentTime.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal
        '
        'NextProcessingTime
        '
        Me.NextProcessingTime.AutoSize = False
        Me.NextProcessingTime.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.NextProcessingTime.BorderStyle = System.Windows.Forms.Border3DStyle.SunkenInner
        Me.NextProcessingTime.Name = "NextProcessingTime"
        Me.NextProcessingTime.Size = New System.Drawing.Size(300, 19)
        Me.NextProcessingTime.Text = "Next Procesing Time"
        Me.NextProcessingTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.NextProcessingTime.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Dock = System.Windows.Forms.DockStyle.Left
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripLabel1, Me.customer_btn, Me.Exitbtn, Me.clearlogbtn, Me.btn_relationships, Me.QBTime2SQLbtn, Me.btn_systemsync, Me.settingbtn, Me.loginbtn, Me.ToolStripLabel2, Me.employees_btn, Me.vendor_btn, Me.jobs_items_btn, Me.ToolStripSeparator1, Me.ToolStripLabel3, Me.timeentries_btn})
        Me.ToolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.VerticalStackWithOverflow
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(93, 549)
        Me.ToolStrip1.TabIndex = 9
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'ToolStripLabel1
        '
        Me.ToolStripLabel1.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.ToolStripLabel1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ToolStripLabel1.Name = "ToolStripLabel1"
        Me.ToolStripLabel1.Size = New System.Drawing.Size(90, 15)
        Me.ToolStripLabel1.Text = "QB TO TL SYNC"
        Me.ToolStripLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'customer_btn
        '
        Me.customer_btn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.customer_btn.Image = CType(resources.GetObject("customer_btn.Image"), System.Drawing.Image)
        Me.customer_btn.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.customer_btn.Name = "customer_btn"
        Me.customer_btn.Size = New System.Drawing.Size(90, 19)
        Me.customer_btn.Text = "Customers"
        Me.customer_btn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Exitbtn
        '
        Me.Exitbtn.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.Exitbtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.Exitbtn.Image = CType(resources.GetObject("Exitbtn.Image"), System.Drawing.Image)
        Me.Exitbtn.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.Exitbtn.Name = "Exitbtn"
        Me.Exitbtn.Size = New System.Drawing.Size(90, 19)
        Me.Exitbtn.Text = "Exit"
        Me.Exitbtn.TextAlign = System.Drawing.ContentAlignment.TopLeft
        '
        'clearlogbtn
        '
        Me.clearlogbtn.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.clearlogbtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.clearlogbtn.Image = CType(resources.GetObject("clearlogbtn.Image"), System.Drawing.Image)
        Me.clearlogbtn.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.clearlogbtn.Name = "clearlogbtn"
        Me.clearlogbtn.Size = New System.Drawing.Size(90, 19)
        Me.clearlogbtn.Text = "Clear Log"
        Me.clearlogbtn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btn_relationships
        '
        Me.btn_relationships.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.btn_relationships.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.btn_relationships.Image = CType(resources.GetObject("btn_relationships.Image"), System.Drawing.Image)
        Me.btn_relationships.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btn_relationships.Name = "btn_relationships"
        Me.btn_relationships.Size = New System.Drawing.Size(90, 19)
        Me.btn_relationships.Text = "Relationships"
        Me.btn_relationships.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btn_relationships.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal
        '
        'QBTime2SQLbtn
        '
        Me.QBTime2SQLbtn.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.QBTime2SQLbtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.QBTime2SQLbtn.Image = CType(resources.GetObject("QBTime2SQLbtn.Image"), System.Drawing.Image)
        Me.QBTime2SQLbtn.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.QBTime2SQLbtn.Name = "QBTime2SQLbtn"
        Me.QBTime2SQLbtn.Size = New System.Drawing.Size(90, 19)
        Me.QBTime2SQLbtn.Text = "QBTime to SQL"
        Me.QBTime2SQLbtn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btn_systemsync
        '
        Me.btn_systemsync.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.btn_systemsync.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.btn_systemsync.Image = CType(resources.GetObject("btn_systemsync.Image"), System.Drawing.Image)
        Me.btn_systemsync.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btn_systemsync.Name = "btn_systemsync"
        Me.btn_systemsync.Size = New System.Drawing.Size(90, 19)
        Me.btn_systemsync.Text = "System Sync"
        Me.btn_systemsync.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'settingbtn
        '
        Me.settingbtn.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.settingbtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.settingbtn.Image = CType(resources.GetObject("settingbtn.Image"), System.Drawing.Image)
        Me.settingbtn.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.settingbtn.Name = "settingbtn"
        Me.settingbtn.Size = New System.Drawing.Size(90, 19)
        Me.settingbtn.Text = "Settings"
        Me.settingbtn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.settingbtn.ToolTipText = "Settings"
        '
        'loginbtn
        '
        Me.loginbtn.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.loginbtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.loginbtn.Image = CType(resources.GetObject("loginbtn.Image"), System.Drawing.Image)
        Me.loginbtn.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.loginbtn.Name = "loginbtn"
        Me.loginbtn.Size = New System.Drawing.Size(90, 19)
        Me.loginbtn.Text = "Login"
        Me.loginbtn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'ToolStripLabel2
        '
        Me.ToolStripLabel2.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right
        Me.ToolStripLabel2.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.ToolStripLabel2.Name = "ToolStripLabel2"
        Me.ToolStripLabel2.Size = New System.Drawing.Size(90, 15)
        Me.ToolStripLabel2.Text = "GENERAL"
        Me.ToolStripLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'employees_btn
        '
        Me.employees_btn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.employees_btn.Image = CType(resources.GetObject("employees_btn.Image"), System.Drawing.Image)
        Me.employees_btn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.employees_btn.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.employees_btn.Name = "employees_btn"
        Me.employees_btn.Size = New System.Drawing.Size(90, 19)
        Me.employees_btn.Text = "Employees"
        Me.employees_btn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'vendor_btn
        '
        Me.vendor_btn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.vendor_btn.Image = CType(resources.GetObject("vendor_btn.Image"), System.Drawing.Image)
        Me.vendor_btn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.vendor_btn.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.vendor_btn.Name = "vendor_btn"
        Me.vendor_btn.Size = New System.Drawing.Size(90, 19)
        Me.vendor_btn.Text = "Vendors"
        Me.vendor_btn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'jobs_items_btn
        '
        Me.jobs_items_btn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.jobs_items_btn.Image = CType(resources.GetObject("jobs_items_btn.Image"), System.Drawing.Image)
        Me.jobs_items_btn.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.jobs_items_btn.Name = "jobs_items_btn"
        Me.jobs_items_btn.Size = New System.Drawing.Size(90, 19)
        Me.jobs_items_btn.Text = "Jobs/Items"
        Me.jobs_items_btn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(90, 6)
        '
        'ToolStripLabel3
        '
        Me.ToolStripLabel3.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold)
        Me.ToolStripLabel3.Name = "ToolStripLabel3"
        Me.ToolStripLabel3.Size = New System.Drawing.Size(90, 15)
        Me.ToolStripLabel3.Text = "TL TO QB SYNC"
        '
        'timeentries_btn
        '
        Me.timeentries_btn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.timeentries_btn.Image = CType(resources.GetObject("timeentries_btn.Image"), System.Drawing.Image)
        Me.timeentries_btn.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.timeentries_btn.Name = "timeentries_btn"
        Me.timeentries_btn.Size = New System.Drawing.Size(90, 19)
        Me.timeentries_btn.Text = "Time Entries"
        Me.timeentries_btn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'ToolStripButton2
        '
        Me.ToolStripButton2.Name = "ToolStripButton2"
        Me.ToolStripButton2.Size = New System.Drawing.Size(82, 4)
        '
        'StatusWindow
        '
        Me.StatusWindow.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.StatusWindow.BackColor = System.Drawing.SystemColors.ActiveCaptionText
        Me.StatusWindow.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.StatusWindow.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.StatusWindow.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.StatusWindow.Location = New System.Drawing.Point(93, 0)
        Me.StatusWindow.Multiline = True
        Me.StatusWindow.Name = "StatusWindow"
        Me.StatusWindow.ReadOnly = True
        Me.StatusWindow.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.StatusWindow.Size = New System.Drawing.Size(767, 548)
        Me.StatusWindow.TabIndex = 11
        '
        'MAIN
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSize = True
        Me.ClientSize = New System.Drawing.Size(860, 573)
        Me.Controls.Add(Me.StatusWindow)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.StatusStrip)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.IsMdiContainer = True
        Me.Name = "MAIN"
        Me.Text = "TimeLive Quickbooks Integrator"
        Me.StatusStrip.ResumeLayout(False)
        Me.StatusStrip.PerformLayout()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ToolTip As System.Windows.Forms.ToolTip
    Friend WithEvents CurrentTime As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents StatusStrip As System.Windows.Forms.StatusStrip
    Friend WithEvents ToolStrip1 As ToolStrip
    Friend WithEvents customer_btn As ToolStripButton
    Friend WithEvents ToolStripLabel1 As ToolStripLabel
    Friend WithEvents settingbtn As ToolStripButton
    Friend WithEvents ToolStripButton2 As ToolStripButton
    Friend WithEvents loginbtn As ToolStripButton
    Friend WithEvents clearlogbtn As ToolStripButton
    Friend WithEvents Exitbtn As ToolStripButton
    Friend WithEvents ToolStripLabel2 As ToolStripLabel
    Friend WithEvents StatusWindow As TextBox
    Friend WithEvents employees_btn As ToolStripButton
    Friend WithEvents vendor_btn As ToolStripButton
    Friend WithEvents jobs_items_btn As ToolStripButton
    Friend WithEvents ToolStripSeparator1 As ToolStripSeparator
    Friend WithEvents ToolStripLabel3 As ToolStripLabel
    Friend WithEvents timeentries_btn As ToolStripButton
    Friend WithEvents NextProcessingTime As ToolStripStatusLabel
    Friend WithEvents btn_systemsync As ToolStripButton
    Friend WithEvents btn_relationships As ToolStripButton
    Friend WithEvents QBTime2SQLbtn As ToolStripButton
End Class
