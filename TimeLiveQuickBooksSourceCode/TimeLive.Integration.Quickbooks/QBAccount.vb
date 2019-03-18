Imports Interop.QBFC10
Public Class QBAccount
    Private p_token As String
    Private p_AccountId As String
    Public Overloads Sub Show(ByVal token As String, ByVal AccountId As String)
        If token Is Nothing Then
            MsgBox("Please Login")
        Else
            p_AccountId = AccountId
            p_token = token            
            MyBase.Show()
        End If
    End Sub
    Public Sub AddAccountsInQB(ByVal AccountName As String)
        'step1: create QBFC session manager and prepare the request
        Dim sessManager As QBSessionManager
        Dim msgSetRs As IMsgSetResponse
        Try
            sessManager = New QBSessionManagerClass()
            Dim msgSetRq As IMsgSetRequest = sessManager.CreateMsgSetRequest("US", 2, 0)
            msgSetRq.Attributes.OnError = ENRqOnError.roeContinue
            Dim AccountAdd As IAccountAdd = msgSetRq.AppendAccountAddRq
            AccountAdd.Name.SetValue(AccountName)
            AccountAdd.AccountType.SetValue(ENAccountType.atExpense)
            'step2: begin QB session and send the request
            sessManager.OpenConnection("App", "TimeLive Quickbooks")
            sessManager.BeginSession("", ENOpenMode.omDontCare)
            msgSetRs = sessManager.DoRequests(msgSetRq)
            If Not msgSetRs.ResponseList.GetAt(0).StatusMessage.Contains("already in use") Then
                If msgSetRs.ResponseList.GetAt(0).StatusSeverity = "Error" Then
                    Throw New Exception(msgSetRs.ResponseList.GetAt(0).StatusMessage)
                End If
            End If
        Catch ex As Exception
            Throw ex
        Finally
            If Not sessManager Is Nothing Then
                sessManager.EndSession()
                sessManager.CloseConnection()
            End If
        End Try
    End Sub

    Private Sub btnAddAccountsInQB_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddAccountsInQB.Click
        pgbar.Value = 0
        Try
            Dim objExpenseServices As New Services.TimeLive.ExpenseEntries.ExpenseEntries
            Dim authentication As New Services.TimeLive.ExpenseEntries.SecuredWebServiceHeader
            authentication.AuthenticatedToken = p_token
            objExpenseServices.SecuredWebServiceHeaderValue = authentication
            Dim objExpenseNameArray() As Object
            objExpenseNameArray = objExpenseServices.GetExpenseNames()
            Dim objExpenseName As New Services.TimeLive.ExpenseEntries.ExpenseName
            Dim pblenth As Integer = objExpenseNameArray.Length - 1
            If pblenth >= 0 Then
                pgbar.Maximum = pblenth
            End If
            For n As Integer = 0 To objExpenseNameArray.Length - 1
                pgbar.Increment(n)
                objExpenseName = objExpenseNameArray(n)
                With objExpenseName
                    AddAccountsInQB(.ExpenseName1)
                End With
            Next
            MessageBox.Show("Record(s) transferred successfully")
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub AddAccountsInQB_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        'MainMenu.Enabled = True
    End Sub
End Class