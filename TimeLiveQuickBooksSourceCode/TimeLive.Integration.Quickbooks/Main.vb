Imports System.Windows.Forms
Imports System.Threading
Imports QBFC11Lib
Imports System.Net.Mail
Public Class MAIN

    Private p_token As String
    Private p_AccountId As String
    Public Shared SESSMANAGER As QBSessionManager

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
                My.Forms.MAIN.History(ItemsProcessed.ToString() + " TmeLive record(s) was created or updated", "i")


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
            SESSMANAGER.OpenConnection("APP", "TIMELIVE QUICKBOOKS")
            SESSMANAGER.BeginSession("", ENOpenMode.omDontCare)
        Catch EX As Exception
            Throw New Exception("QUICKBOOKS IS NOT OPEN. PLEASE OPEN IT AND THEN TRY AGAIN.")
        End Try
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
        SESSMANAGER.EndSession() 'Maybe?
        SESSMANAGER.CloseConnection() 'Maybe?
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


    Public Shared Sub SendGMail(Subject__1 As String, BodyText As String)
        'Specify senders gmail address
        Dim SendersAddress As String = "teltrium@gmail.com"
        'Specify The Address You want to sent Email To(can be any valid email address)
        Dim ReceiversAddress As String = "ywang@teltrium.com"
        'Specify The password of gmial account u are using to sent mail(pw of sender@gmail.com)
        MsgBox("--> Sending email to: " + ReceiversAddress + " From: " + SendersAddress)
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
            MsgBox("Sent!")
        Catch ex As Exception

            MsgBox(ex.Message)
        End Try
    End Sub
    Public Sub FlagChangedItemsResults(Name As String, result As Integer)
        My.Forms.MAIN.History("Item: " + Name, "i")
        Dim relationship As String

        If result < 0 Then
            relationship = "Not modified before last run"
        Else
            relationship = "******* Created or modified after last run *******"
        End If
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

        If My.Settings.SyncCustomers = True Then
            Dim customer_qbtotl As QBtoTL_Customer = New QBtoTL_Customer
            Dim customerData As QBtoTL_Customer.CustomerDataStructureQB

            Dim ItemLastSync As DateTime = Convert.ToDateTime(My.Settings.CustomerLastSync)
            My.Forms.MAIN.History("Synchonizing modified customers since: " + ItemLastSync.ToString(), "n")

            customerData = customer_qbtotl.GetCustomerQBData(Nothing, False)
            My.Forms.MAIN.History(customerData.NoItems.ToString() + " items were read from Quickbooks", "i")

            For Each element As QBtoTL_Customer.Customer In customerData.DataArray
                Dim result As Integer = DateTime.Compare(Convert.ToDateTime(element.QBModTime.ToString()),
            ItemLastSync)

                If result >= 0 Then
                    element.RecSelect = True
                End If
                FlagChangedItemsResults(element.QB_Name.ToString(), result)

            Next

            ItemsProcessed = customer_qbtotl.QBTransferCustomerToTL(customerData, p_token, Nothing, False)
            My.Settings.CustomerLastSync = DateTime.Now.ToString()
            My.Settings.Save()

        End If

        Return ItemsProcessed
    End Function

    Private Sub travelexpenses_btn_Click(sender As Object, e As EventArgs)

    End Sub

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

    Private Sub ToolStrip1_ItemClicked(sender As Object, e As ToolStripItemClickedEventArgs) Handles ToolStrip1.ItemClicked

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
End Class



