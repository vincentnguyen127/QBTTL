Imports Interop.QBFC10
Public Class QBEmployee
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
    Private Sub btnGetAndAddEmployee_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddEmployeesInQB.Click
        pgbar.Value = 0
        Try
            Dim objEmployeeServices As New Services.TimeLive.Employees.Employees
            Dim authentication As New Services.TimeLive.Employees.SecuredWebServiceHeader
            authentication.AuthenticatedToken = p_token
            objEmployeeServices.SecuredWebServiceHeaderValue = authentication
            Dim objEmployeeArray() As Object
            objEmployeeArray = objEmployeeServices.GetEmployees()
            Dim objEmployee As New Services.TimeLive.Employees.Employee
            Dim pblenth As Integer = objEmployeeArray.Length - 1
            If pblenth >= 0 Then
                pgbar.Maximum = pblenth
            End If
            For n As Integer = 0 To objEmployeeArray.Length - 1
                pgbar.Increment(n)
                objEmployee = objEmployeeArray(n)
                With objEmployee
                    If .IsVendor = False Then
                        Try
                            AddEmployeeInQB(.EmployeeId, .EmployeeName, .FirstName, .MiddleName, .LastName, .EmailAddress, _
                                            .Address1, .Address2, .City, .Country, .PostalCode, .State, .HiredDate, _
                                            .Mobile, .Phone)
                        Catch ex As Exception
                            MessageBox.Show(ex.Message)
                            Exit Sub
                        End Try
                    End If
                End With
            Next
            MessageBox.Show("Record(s) transferred successfully")
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Public Sub AddEmployeeInQB(ByVal EmployeeId As Integer, ByVal EmployeeName As String, ByVal FirstName As String, ByVal MiddleName As String, _
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
            Dim empAdd As IEmployeeAdd = msgSetRq.AppendEmployeeAddRq
            empAdd.AccountNumber.SetValue(EmployeeId)
            empAdd.Name.SetValue(EmployeeName)
            empAdd.FirstName.SetValue(FirstName)
            empAdd.MiddleName.SetValue(MiddleName)
            empAdd.LastName.SetValue(LastName)
            empAdd.Email.SetValue(EmailAddress)
            empAdd.EmployeeAddress.Addr1.SetValue(Address1)
            empAdd.EmployeeAddress.Addr2.SetValue(Address2)
            empAdd.EmployeeAddress.City.SetValue(City)
            empAdd.EmployeeAddress.Country.SetValue(Country)
            empAdd.EmployeeAddress.PostalCode.SetValue(PostalCode)
            empAdd.EmployeeAddress.State.SetValue(State)
            empAdd.HiredDate.SetValue(HiredDate)
            empAdd.EmployeePayrollInfo.IsUsingTimeDataToCreatePaychecks.SetValue(False)

            'empAdd.Mobile.SetValue(Mobile)
            'empAdd.Phone.SetValue(Phone)

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

    Private Sub btnAddEmployeesInQB_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddEmployeesInQB.Disposed
        'MainMenu.Enabled = True
    End Sub
End Class