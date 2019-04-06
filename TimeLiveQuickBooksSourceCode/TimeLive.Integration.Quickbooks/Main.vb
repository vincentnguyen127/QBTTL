Imports System.Windows.Forms
Imports System.Threading
'Imports QBFC11Lib
Imports QBFC13Lib

Imports System.Net.Mail
Public Class MAIN

    Private p_token As String
    Private p_AccountId As String
    <ThreadStatic> Public Shared SESSMANAGER As QBSessionManager



    Private Sub PARENT_LOAD(SENDER As System.Object, E As System.EventArgs) Handles MyBase.Load
        Try
            VALIDATEQBSESSION()
            Me.Show()
            History("Welcome to QB2TL Sync by Telrium", "n")
            Using newForm = New Login()
                If DialogResult.OK = newForm.ShowDialog() Then
                    p_token = newForm.ReturnValue1
                    p_AccountId = newForm.ReturnValue2
                    History("You are logged into TimeLive", "n")
                    History("", "n")
                Else
                    History("You will need to log into TimeLive before using this utility.", "n")
                    History("", "n")
                End If
            End Using

            Dim ChargingRelationship As New ChargingRelationship

            'SPIN OFF THREAD FOR TIMER
            TIMERTHREAD()
            Dim NextRunDateTime As Date = Convert.ToDateTime(My.Settings.AutoRunTime)
            NextProcessingTime.Text = "Auto Processing Time: " + NextRunDateTime.ToString("MM/dd/yy HH:mm")

        Catch EX As Exception
            MsgBox(EX.Message)
            Me.Close()
        End Try
    End Sub

    Public Sub TIMERTHREAD()
        Dim TIMERTHREAD _
        As New Threading.Thread(
            AddressOf TIMERMULTITHREADING)
        TIMERTHREAD.Start()
    End Sub

    Public Sub TIMERMULTITHREADING()
        While True
            SETTEXT()
            Thread.Sleep(61000)
        End While
    End Sub


    Delegate Sub StatusWindowCALLBACK(ByVal TEXT As String)
    Private Sub SETTEXT()
        If StatusStrip.InvokeRequired Then
            Dim D As StatusWindowCALLBACK = New StatusWindowCALLBACK(AddressOf SETTEXT)
            Me.Invoke(D, New Object() {Text})
        Else
            'Dim result As Integer = DateTime.Compare(Convert.ToDateTime(My.Settings.AutoRunTime), DateTime.Now)
            Dim AutoRundt As Date = Convert.ToDateTime(My.Settings.AutoRunTime)
            AutoRundt = AutoRundt.AddSeconds(-AutoRundt.Second)
            Dim Currentdt As Date = DateTime.Now
            Currentdt = Currentdt.AddSeconds(-Currentdt.Second)
            Dim result As Integer = String.Compare(AutoRundt.ToString(), Currentdt.ToString())

            If result = 0 Then
                History("Executing selected sync processes: " + Currentdt.ToString(), "n")

                'Execution code goes here
                Dim ItemsProcessed As Integer = AutoExecute()
                My.Forms.MAIN.History(ItemsProcessed.ToString() + " TimeLive record(s) was created or updated", "N")

                Dim NextRunDateTime As Date = Convert.ToDateTime(My.Settings.AutoRunTime)
                NextRunDateTime = NextRunDateTime.AddHours(Convert.ToInt16(My.Settings.AutoRunInterval))
                My.Settings.AutoRunTime = NextRunDateTime
                My.Settings.Save()

                NextProcessingTime.Text = "Auto Processing Time: " + Convert.ToDateTime(My.Settings.AutoRunTime)
            End If
            CurrentTime.Text = "Time: " + DateTime.Now.ToString("HH:mm")
        End If
    End Sub


    Public Sub VALIDATEQBSESSION()
        'Dim SESSMANAGER As QBSessionManager ' Made global
        Try
            SESSMANAGER = New QBSessionManagerClass()
            'SESSMANAGER.OpenConnection("APP", "TIMELIVE QUICKBOOKS") 
            SESSMANAGER.OpenConnection("App", "Timelive Quickbooks")
            SESSMANAGER.BeginSession("", ENOpenMode.omDontCare)
        Catch EX As Exception
            Throw New Exception("QUICKBOOKS IS NOT OPEN. PLEASE OPEN IT AND THEN TRY AGAIN.")
        End Try
    End Sub

    Public Sub QUITQBSESSION()
        ' close the session manager if it is open
        If Not MAIN.SESSMANAGER Is Nothing Then
            MAIN.SESSMANAGER.EndSession()
            MAIN.SESSMANAGER.CloseConnection()
        End If
    End Sub

    Private Sub settingbtn_Click(sender As Object, e As EventArgs) Handles settingbtn.Click
        Using newsettingsForm = New AppSettings()
            If DialogResult.OK = newsettingsForm.ShowDialog() Then
                Dim NextRunDateTime As Date = Convert.ToDateTime(My.Settings.AutoRunTime)
                NextProcessingTime.Text = "Auto Processing Time: " + NextRunDateTime.ToString("MM/dd/yy HH:mm")
            End If
        End Using
    End Sub

    Private Sub loginbtn_Click(sender As Object, e As EventArgs) Handles loginbtn.Click
        If p_token Is Nothing Then
            Using newForm = New Login()
                If DialogResult.OK = newForm.ShowDialog() Then
                    p_token = newForm.ReturnValue1
                    p_AccountId = newForm.ReturnValue2
                End If
            End Using
        Else
            MsgBox("You are already logged in")
        End If
    End Sub

    Private Sub clearlogbtn_Click(sender As Object, e As EventArgs) Handles clearlogbtn.Click
        StatusWindow.Clear()
    End Sub

    Private Sub Exitbtn_Click(sender As Object, e As EventArgs) Handles Exitbtn.Click
        QUITQBSESSION() ' Close QB session before exiting
        Me.Close()
    End Sub


    ' Will be using the following integers for the different types of items that are synched
    ' 0 - customers
    ' 1 - employees
    ' 2 - vendors
    ' 3 - jobs_items
    ' 4 - ODC and travel accounts 
    ' 5 - Time Transfer 
    ' 6 - Customer Expenses
    Private Sub customer_btn_Click(sender As Object, e As EventArgs) Handles customer_btn.Click
        Dim IntegratedUI As New IntegratedUI
        IntegratedUI.Owner = Me
        IntegratedUI.Show(p_token, p_AccountId, 10)
    End Sub

    Private Sub employees_btn_Click(sender As Object, e As EventArgs) Handles employees_btn.Click
        Dim IntegratedUI As New IntegratedUI
        IntegratedUI.Owner = Me
        IntegratedUI.Show(p_token, p_AccountId, 11)
    End Sub

    Private Sub timeentries_btn_Click(sender As Object, e As EventArgs) Handles timeentries_btn.Click
        ' excluded from this project. 

        'Dim QBTimeTracking As New QBTimeTracking
        IntegratedUI.Owner = Me
        IntegratedUI.Show(p_token, p_AccountId, 20)
    End Sub


    Public Shared Sub SendGMail(Subject__1 As String, BodyText As String, UI As Boolean)
        'Specify senders gmail address
        Dim SendersAddress As String = "teltrium@gmail.com"
        'Specify The Address You want to send Email To(can be any valid email address)
        Dim ReceiversAddress As String = "cpetrelle@teltrium.com" '"ywang@teltrium.com"

        'Specify The password of gmail account u are using to sent mail(pw of sender@gmail.com)
        If UI Then
            MsgBox("--> Sending email to: " + ReceiversAddress + " From: " + SendersAddress)
        End If
        Dim SendersPassword As String = "1October2014"

        'Write the contents of your mail
        Dim body As String = BodyText
        Try
            Dim smtp As New SmtpClient()
            With smtp
                'used to be Key.Host

                .Host = "smtp.gmail.com"
                .Port = 587
                .EnableSsl = True
                .DeliveryMethod = SmtpDeliveryMethod.Network

                .Credentials = New Net.NetworkCredential(SendersAddress, SendersPassword)
                .Timeout = 3000
            End With
            'MailMessage represents a mail message
            'it is 4 parameters(From,TO,subject,body)
            Dim message As New MailMessage(SendersAddress, ReceiversAddress, Subject__1, body)
            'WE use smtp sever we specified above to send the message(MailMessage message)

            smtp.Send(message)
            If UI Then
                MsgBox("Sent!")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Public Sub FlagChangedItemsResults(Name As String, result As Integer)
        My.Forms.MAIN.History("Item: " + Name, "i")
        Dim relationship As String = If(result < 0, "Not modified before last run",
                                        "******* Created or modified after last run *******")
        My.Forms.MAIN.History(relationship, "c")
    End Sub

    Public Sub History(ByVal input As String, Type As String)
        If String.Compare("n", Type, False) = 0 Then
            StatusWindow.Text += vbNewLine + ">> " + input
        End If
        If String.Compare("N", Type, False) = 0 Then
            StatusWindow.Text += vbNewLine + "***********************"
            StatusWindow.Text += vbNewLine + ">> " + input
            StatusWindow.Text += vbNewLine + "***********************"
        End If
        If String.Compare("c", Type, False) = 0 Then
            StatusWindow.Text += ", " + input
        End If
        If String.Compare("C", Type, False) = 0 Then
            StatusWindow.Text += ", " + input
        End If
        If String.Compare("i", Type, False) = 0 Then
            StatusWindow.Text += vbNewLine + vbTab + "- " + input
        End If
        If String.Compare("I", Type, False) = 0 Then
            StatusWindow.Text += vbNewLine + vbTab + "***********************"
            StatusWindow.Text += vbNewLine + vbTab + "- " + input
            StatusWindow.Text += vbNewLine + vbTab + "***********************"
        End If

        StatusWindow.SelectionStart = StatusWindow.TextLength
        StatusWindow.ScrollToCaret()
    End Sub

    Private Function AutoExecute() As Integer
        Dim ItemsProcessed As Integer = 0
        Dim emailBody As String = ""
        If (MAIN.SESSMANAGER Is Nothing) Then
            VALIDATEQBSESSION()
        End If

        ' Customers
        If My.Settings.SyncCustomers Then
            Dim customer_qbtotl As QBtoTL_Customer = New QBtoTL_Customer
            Dim customerData As QBtoTL_Customer.CustomerDataStructureQB
            Dim ItemLastSync As DateTime = Convert.ToDateTime(My.Settings.CustomerLastSync)
            My.Forms.MAIN.History("Synchonizing modified customers since: " + ItemLastSync.ToString(), "n")
            customerData = customer_qbtotl.GetCustomerQBData(Nothing, False)
            My.Forms.MAIN.History((customerData.NoItems - customerData.NoInactive).ToString() + " active items were read from Quickbooks", "i")

            For Each element As QBtoTL_Customer.Customer In customerData.DataArray
                Dim result As Integer = DateTime.Compare(Convert.ToDateTime(element.QBModTime.ToString()),
            ItemLastSync)

                If result >= 0 Then
                    element.RecSelect = True
                End If
                If element.Enabled Then ' Note: Not sure if this is entirely right
                    FlagChangedItemsResults(element.QB_Name.ToString(), result)
                End If
            Next

            Dim customersProcessed As Integer = customer_qbtotl.QBTransferCustomerToTL(customerData, p_token, Nothing, False)
            emailBody += "Customers Processed: " + customersProcessed.ToString & vbCrLf
            My.Settings.CustomerLastSync = DateTime.Now.ToString()
            My.Forms.MAIN.History(customersProcessed.ToString() + " TimeLive customer(s) was created or updated", "i")
            ItemsProcessed += customersProcessed
        End If

        ' Employees
        If My.Settings.SyncEmployees Then
            Dim employee_qbtotl As QBtoTL_Employee = New QBtoTL_Employee
            Dim employeeData As QBtoTL_Employee.EmployeeDataStructureQB

            Dim ItemLastSync As DateTime = Convert.ToDateTime(My.Settings.EmployeeLastSync)
            My.Forms.MAIN.History("Synchonizing modified employees since: " + ItemLastSync.ToString(), "n")
            employeeData = employee_qbtotl.GetEmployeeQBData(Nothing, False)
            ' Change to "employeeData.NoItems - employeeData.NoInactive" once we begin storing inactive employees
            My.Forms.MAIN.History(employeeData.NoItems.ToString() + " active items were read from Quickbooks", "i")

            For Each element As QBtoTL_Employee.Employee In employeeData.DataArray
                Dim result As Integer = DateTime.Compare(Convert.ToDateTime(element.QBModTime.ToString()),
            ItemLastSync)

                If result >= 0 Then
                    element.RecSelect = True
                End If
                'If element.Enabled Then ' Note: Will need to check if employee is enabled first once we start tracking inactive employees
                FlagChangedItemsResults(element.QB_Name.ToString(), result)
                'End If
            Next

            Dim employeesProcessed As Integer = employee_qbtotl.QBTransferEmployeeToTL(employeeData, p_token, Nothing, False)
            emailBody += "Employees Processed: " + employeesProcessed.ToString & vbCrLf
            My.Settings.EmployeeLastSync = DateTime.Now.ToString()
            My.Forms.MAIN.History(employeesProcessed.ToString() + " TimeLive employee(s) was created or updated", "i")
            ItemsProcessed += employeesProcessed
        End If

        ' Vendors/Consultants
        If My.Settings.SyncConsultants Then ' Sync Vendors
            Dim vendor_qbtotl As QBtoTL_Vendor = New QBtoTL_Vendor
            Dim vendorData As QBtoTL_Vendor.VendorDataStructureQB

            Dim ItemLastSync As DateTime = Convert.ToDateTime(My.Settings.VendorLastSync)
            My.Forms.MAIN.History("Synchonizing modified vendors since: " + ItemLastSync.ToString(), "n")
            vendorData = vendor_qbtotl.GetVendorQBData(Nothing, False)
            ' Change to "employeeData.NoItems - employeeData.NoInactive" if we begin storing inactive vendors
            My.Forms.MAIN.History(vendorData.NoItems.ToString() + " active items were read from Quickbooks", "i")

            For Each element As QBtoTL_Vendor.Vendor In vendorData.DataArray
                Dim result As Integer = DateTime.Compare(Convert.ToDateTime(element.QBModTime.ToString()),
            ItemLastSync)

                If result >= 0 Then
                    element.RecSelect = True
                End If
                'If element.Enabled Then ' Note: Will need to check if vendor is enabled first if we start tracking inactive vendors
                FlagChangedItemsResults(element.QB_Name.ToString(), result)
                'End If
            Next

            Dim vendorsProcessed As Integer = vendor_qbtotl.QBTransferVendorToTL(vendorData, p_token, Nothing, False)
            emailBody += "Vendors Processed: " + vendorsProcessed.ToString & vbCrLf
            My.Settings.VendorLastSync = DateTime.Now.ToString()
            My.Forms.MAIN.History(vendorsProcessed.ToString() + " TimeLive employee(s) was created or updated from vendors", "i")
            ItemsProcessed += vendorsProcessed
        End If

        ' Jobs/Subjobs
        If My.Settings.SyncJobs_Items Then
            Dim job_item_qbtotl As QBtoTL_JobOrItem = New QBtoTL_JobOrItem
            Dim job_itemData As QBtoTL_JobOrItem.JobDataStructureQB

            Dim ItemLastSync As DateTime = Convert.ToDateTime(My.Settings.JobLastSync)
            My.Forms.MAIN.History("Synchonizing modified jobs since: " + ItemLastSync.ToString(), "n")
            If True Then
                job_itemData = job_item_qbtotl.GetJobSubJobData(Nothing, "", False)
            Else
                job_itemData = job_item_qbtotl.GetItemSubItemData(Nothing, "", False)
            End If

            ' Change to "employeeData.NoItems - employeeData.NoInactive" if we begin storing inactive vendors
            My.Forms.MAIN.History(job_itemData.NoItems.ToString() + " active items were read from Quickbooks", "i")

            For Each element As QBtoTL_JobOrItem.Job_Subjob In job_itemData.DataArray ' Or should this be job_item?
                Dim result As Integer = DateTime.Compare(Convert.ToDateTime(element.QBModTime.ToString()),
            ItemLastSync)

                If result >= 0 Then
                    element.RecSelect = True
                End If
                'If element.Enabled Then ' Note: Will need to check if vendor is enabled first if we start tracking inactive vendors
                FlagChangedItemsResults(element.QB_Name.ToString(), result)
                'End If
            Next

            Dim jobsProcessed As Integer = job_item_qbtotl.QBTransferJobstoTL(job_itemData, p_token, Nothing, False)
            If True Then
                My.Settings.JobLastSync = DateTime.Now.ToString()
            Else
                My.Settings.ItemLastSync = DateTime.Now.ToString()
            End If

            emailBody += "Jobs Processed: " + jobsProcessed.ToString & vbCrLf
            My.Forms.MAIN.History(jobsProcessed.ToString() + " TimeLive project(s)/task(s) was created or updated from jobs", "i")
            ItemsProcessed += jobsProcessed
        End If

        emailBody += "Total items Processed: " + ItemsProcessed.ToString
        MAIN.SendGMail("QuickBooks to TimeLive Auto transfer", emailBody, False)
        My.Settings.Save()

        Return ItemsProcessed
    End Function

    Private Sub btn_systemsync_Click(sender As Object, e As EventArgs) Handles btn_systemsync.Click
        If MsgBox("Are you to perform a sync?", MsgBoxStyle.YesNo, "Warning!") = MsgBoxResult.Yes Then ' If you select yes in the MsgBox then it will close the window
            Dim CurrentSystemSync As New CurrentSystemSync
            CurrentSystemSync.PassToken(p_token, p_AccountId)
        End If
    End Sub

    Private Sub contractor_btn_Click(sender As Object, e As EventArgs) Handles vendor_btn.Click
        Dim IntegratedUI As New IntegratedUI
        IntegratedUI.Owner = Me
        IntegratedUI.Show(p_token, p_AccountId, 12)
    End Sub

    Private Sub jobs_items_btn_Click(sender As Object, e As EventArgs) Handles jobs_items_btn.Click
        Dim IntegratedUI As New IntegratedUI
        IntegratedUI.Owner = Me
        If Not (My.Settings.JobOrItemHierarchy Is Nothing Or My.Settings.JobOrItemHierarchy Is "") Then
            If My.Settings.JobOrItemHierarchy = 0 Then
                IntegratedUI.Show(p_token, p_AccountId, 13)
            Else
                IntegratedUI.Show(p_token, p_AccountId, 14)
            End If
        Else
            MsgBox("Seems that settings are not set. Please set application settings.")
        End If
    End Sub

    Private Sub btn_relationships_Click(sender As Object, e As EventArgs) Handles btn_relationships.Click
        ChargingRelationship.Owner = Me
        ChargingRelationship.Show()
    End Sub

    Private Sub MAIN_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        Me.Dispose()
    End Sub

    Private Sub QBTime2SQLbtn_Click(sender As Object, e As EventArgs) Handles QBTime2SQLbtn.Click
        Dim CurrentSystemSync As New CurrentSystemSync
        CurrentSystemSync.GetTime()
    End Sub

    Friend Sub History(v As Object)
        Throw New NotImplementedException()
    End Sub
End Class