Imports QBFC13Lib
Imports System.Threading

Public Class Login
    Dim TimerThread As Threading.Thread
    Dim p1 As String
    Dim a1 As String
    Public Property ReturnValue1 As String
    Public Property ReturnValue2 As String
    Public LastRunDateAndTime As String
    Private LoginForm As Login

    Private Sub btnLogin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLogin.Click
        SaveUserSettings(txtURL.Text)
        Dim objServices As New Services.TimeLiveServices
        Dim objSoapHeader As New Services.SecuredWebServiceHeader
        Dim AccountId As Integer

        LoginForm = Me

        Try
            ValidateRequiredFields()
            ValidateURL(objServices)
            objSoapHeader.Username = txtUsername.Text
            objSoapHeader.Password = txtPassword.Text
            objServices.SecuredWebServiceHeaderValue = objSoapHeader
            objSoapHeader.AuthenticatedToken = objServices.AuthenticateUser()
            objServices.GetAccountId(txtUsername.Text, txtPassword.Text)
            If objSoapHeader.AuthenticatedToken Is Nothing Then
                MsgBox("Invalid Username or Password")
            Else
                'Me.Hide()
                p1 = objSoapHeader.AuthenticatedToken
                a1 = AccountId
                Me.ReturnValue1 = objSoapHeader.AuthenticatedToken
                Me.ReturnValue2 = AccountId
                Me.DialogResult = DialogResult.OK
                Me.Close()
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Public Sub ValidateRequiredFields()
        If txtUsername.Text = "" Or txtPassword.Text = "" Or txtURL.Text = "" Then
            Throw New Exception("URL, Username and Password are required fields. Please provide the information and continue.")
        End If
    End Sub

    Public Sub ValidateURL(ByVal objServices As Services.TimeLiveServices)
        Try
            objServices.Url = txtURL.Text & "/Services/TimeLiveServices.asmx"
        Catch ex As Exception
            Throw New Exception("Could not connect to TimeLive QuickBooks. Please check URL.")
        End Try
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub Login_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        SetValues()
    End Sub

    Public Sub SaveUserSettings(ByVal WSURL As String)
        SetWebServicesURL(WSURL)
        My.Settings.WebServiceURL = WSURL
        My.Settings.Username = txtUsername.Text
        My.Settings.Password = txtPassword.Text
        My.Settings.Save()
    End Sub

    Public Sub SetValues()
        If My.Settings.WebServiceURL = "" Then
            txtURL.Text = ""
        Else
            txtURL.Text = My.Settings.WebServiceURL
        End If
        If My.Settings.Username = "" Then
            txtUsername.Text = ""
        Else
            txtUsername.Text = My.Settings.Username
        End If

        If My.Settings.Password = "" Then
            txtPassword.Text = ""
        Else
            txtPassword.Text = My.Settings.Password
        End If

        If My.Settings.AutoRunTime = "" Then
            My.Settings.AutoRunTime = Now.Date.ToString()
        Else
            LastRunDateAndTime = My.Settings.AutoRunTime
        End If
    End Sub

    '-------------------------------------------------------------------------------------------------------
    '---------------------------   https://myaccount.google.com/lesssecureapps   ---------------------------
    '-------------------------------------------------------------------------------------------------------

    'emailBody use this to passing emailBody
    ' for email passing local history is fine or customer another text?
    Public Sub SetWebServicesURL(WSURL As String)
        With My.Settings
            .TimeLive_Integration_Quickbooks_Services_TimeLive_Clients_Clients = WSURL + WebServiceUtilities.GetClientServiceURL
            .TimeLive_Integration_Quickbooks_Services_TimeLive_Employees_Employees = WSURL + WebServiceUtilities.GetEmployeeServiceURL
            .TimeLive_Integration_Quickbooks_Services_TimeLive_ExpenseEntries_ExpenseEntries = WSURL + WebServiceUtilities.GetExpenseEntryServiceURL
            .TimeLive_Integration_Quickbooks_Services_TimeLive_Projects_Projects = WSURL + WebServiceUtilities.GetProjectServiceURL
            .TimeLive_Integration_Quickbooks_Services_TimeLive_Tasks_Tasks = WSURL + WebServiceUtilities.GetTaskServiceURL
            .TimeLive_Integration_Quickbooks_Services_TimeLive_TimeEntries_TimeEntries = WSURL + WebServiceUtilities.GetTimesEntryServiceURL
            .TimeLive_Integration_Quickbooks_Services_TimeLiveServices = WSURL + WebServiceUtilities.GetTimeLiveServiceURL
        End With
    End Sub
End Class