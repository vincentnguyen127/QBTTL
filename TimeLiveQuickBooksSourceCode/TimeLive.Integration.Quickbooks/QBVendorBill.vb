Imports Interop.QBFC10
Public Class QBVendorBill
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
    Private Sub btnGetAndAddTimeEntries_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetAndAddTimeEntries.Click
        pgbar.Value = 0
        Try
            Dim objExpenseTrackingServices As New Services.TimeLive.ExpenseEntries.ExpenseEntries
            Dim authentication As New Services.TimeLive.ExpenseEntries.SecuredWebServiceHeader
            authentication.AuthenticatedToken = p_token
            objExpenseTrackingServices.SecuredWebServiceHeaderValue = authentication
            Dim objExpenseEntryArray() As Object
            objExpenseEntryArray = objExpenseTrackingServices.GetExpenseEntriesByEmployeeIdAndDateRange(Me.cbVendor.SelectedValue, CDate(dpStartDate.Value).Date, CDate(dpEndDate.Value).Date)            
            Dim objExpenseEntry As New Services.TimeLive.ExpenseEntries.ExpenseEntry
            Dim pblenth As Integer = objExpenseEntryArray.Length - 1
            If pblenth >= 0 Then
                pgbar.Maximum = pblenth
            End If
            For n As Integer = 0 To objExpenseEntryArray.Length - 1
                pgbar.Increment(n)
                objExpenseEntry = objExpenseEntryArray(n)
                With objExpenseEntry
                    AddExpenseEntryInQB(.EmployeeName, .ExpenseEntryDate, .Amount, .ClientWithProject, .ExpenseName)
                End With
            Next
            MessageBox.Show("Record(s) transferred successfully")
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Public Sub AddExpenseEntryInQB(ByVal Vendor As String, ByVal EntryDate As Date, ByVal Amount As Double, ByVal CustomerJob As String, ByVal AccountRef As String)
        'step1: create QBFC session manager and prepare the request
        Dim sessManager As QBSessionManager
        Dim msgSetRs As IMsgSetResponse
        Try
            sessManager = New QBSessionManagerClass()
            Dim msgSetRq As IMsgSetRequest = sessManager.CreateMsgSetRequest("US", 2, 0)
            msgSetRq.Attributes.OnError = ENRqOnError.roeContinue
            Dim BillAdd As IBillAdd = msgSetRq.AppendBillAddRq
            BillAdd.VendorRef.FullName.SetValue(Vendor)
            BillAdd.TxnDate.SetValue(EntryDate)
            Dim ExpenseLine As IExpenseLineAdd
            ExpenseLine = BillAdd.ExpenseLineAddList.Append()
            ExpenseLine.AccountRef.FullName.SetValue(AccountRef)
            ExpenseLine.Amount.SetValue(Amount)
            ExpenseLine.CustomerRef.FullName.SetValue(CustomerJob)

            'step2: begin QB session and send the request
            sessManager.OpenConnection("App", "TimeLive Quickbooks")
            sessManager.BeginSession("", ENOpenMode.omDontCare)
            msgSetRs = sessManager.DoRequests(msgSetRq)
            If msgSetRs.ResponseList.GetAt(0).StatusSeverity = "Error" Then
                Throw New Exception(msgSetRs.ResponseList.GetAt(0).StatusMessage)
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

    Private Sub ExpenseTracking_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        'MainMenu.Enabled = True
    End Sub

    Private Sub QBVendorBill_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim objEmployeeServices As New Services.TimeLive.Employees.Employees
        Dim authentication As New Services.TimeLive.Employees.SecuredWebServiceHeader
        authentication.AuthenticatedToken = p_token
        objEmployeeServices.SecuredWebServiceHeaderValue = authentication
        Dim dv As New DataView(objEmployeeServices.GetEmployeesData)
        dv.RowFilter = "IsVendor=1"
        Me.cbVendor.DataSource = dv
        Me.cbVendor.DisplayMember = "FullName"
        Me.cbVendor.ValueMember = "AccountEmployeeId"
        If Me.cbVendor.Items.Count > 0 Then
            Me.cbVendor.SelectedIndex = 0
        End If
    End Sub
End Class