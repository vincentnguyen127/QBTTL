Imports Interop.QBFC10
Public Class QBVendor
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
    Private Sub btnAddVendorInQuickBooks_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddVendorInQuickBooks.Click
        pgbar.Value = 0
        Try
            Dim objVendorServices As New Services.TimeLive.Employees.Employees
            Dim authentication As New Services.TimeLive.Employees.SecuredWebServiceHeader
            authentication.AuthenticatedToken = p_token
            objVendorServices.SecuredWebServiceHeaderValue = authentication
            Dim objVendorArray() As Object
            objVendorArray = objVendorServices.GetEmployees()
            Dim objVendor As New Services.TimeLive.Employees.Employee
            Dim pblenth As Integer = objVendorArray.Length - 1
            If pblenth >= 0 Then
                pgbar.Maximum = pblenth
            End If
            For n As Integer = 0 To objVendorArray.Length - 1
                pgbar.Increment(n)
                objVendor = objVendorArray(n)
                With objVendor
                    If .IsVendor = True Then
                        AddVendorInQB(.EmployeeId, .EmployeeName, .FirstName, .MiddleName, .LastName, .EmailAddress, _
                                        .Address1, .Address2, .City, .Country, .PostalCode, .State, .HiredDate, _
                                        .Mobile, .Phone)
                    End If
                End With
            Next
            MessageBox.Show("Record(s) transferred successfully")
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Public Sub AddVendorInQB(ByVal EmployeeId As Integer, ByVal EmployeeName As String, ByVal FirstName As String, ByVal MiddleName As String, _
                       ByVal LastName As String, ByVal EmailAddress As String, ByVal Address1 As String, _
                       ByVal Address2 As String, ByVal City As String, ByVal Country As String, ByVal PostalCode As String, _
                       ByVal State As String, ByVal HiredDate As String, ByVal Mobile As String, ByVal Phone As String)
        'step1: create QBFC session manager and prepare the request
        Dim sessManager As QBSessionManager
        Dim msgSetRs As IMsgSetResponse
        Try
            sessManager = New QBSessionManagerClass()
            Dim msgSetRq As IMsgSetRequest = sessManager.CreateMsgSetRequest("US", 2, 0)
            msgSetRq.Attributes.OnError = ENRqOnError.roeContinue
            Dim vendorAdd As IVendorAdd = msgSetRq.AppendVendorAddRq
            With vendorAdd
                .AccountNumber.SetValue(EmployeeId)
                .Name.SetValue(EmployeeName)
                .FirstName.SetValue(FirstName)
                .MiddleName.SetValue(MiddleName)
                .LastName.SetValue(LastName)
                .Email.SetValue(EmailAddress)
                .VendorAddress.Addr1.SetValue(Address1)
                .VendorAddress.Addr2.SetValue(Address2)
                .VendorAddress.City.SetValue(City)
                .VendorAddress.Country.SetValue(Country)
                .VendorAddress.PostalCode.SetValue(PostalCode)
                .VendorAddress.State.SetValue(State)
            End With
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

    Private Sub Vendor_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        'MainMenu.Enabled = True
    End Sub

    Private Sub QBVendor_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class