Imports Interop.QBFC10
Public Class TLEmployee
    Private p_token As String
    Private p_AccountId As String
    Dim empRet As IEmployeeRet
    Dim vendorRet As IVendorRet
    Public Overloads Sub Show(ByVal token As String, ByVal AccountId As String)
        If token Is Nothing Then
            MsgBox("Please Login")
        Else
            p_AccountId = AccountId
            p_token = token
            MyBase.Show()
            RichTextBox1.Text = "Click button to start transfer" & vbCrLf
        End If
    End Sub
    ' Queries QuickBooks for all the employees and displays
    Public Sub GetAndAddEmployeeInTimeLive()
        Dim objEmployeeServices As New Services.TimeLive.Employees.Employees
        Dim authentication As New Services.TimeLive.Employees.SecuredWebServiceHeader
        authentication.AuthenticatedToken = p_token
        objEmployeeServices.SecuredWebServiceHeaderValue = authentication

        Dim objServices As New Services.TimeLiveServices
        Dim authentication2 As New Services.SecuredWebServiceHeader
        authentication2.AuthenticatedToken = p_token
        objServices.SecuredWebServiceHeaderValue = authentication2

        'step1: create QBFC session manager and prepare the request
        Dim sessManager As QBSessionManager
        Dim empmsgSetRs As IMsgSetResponse


        Try
            sessManager = New QBSessionManagerClass()
            Dim empmsgSetRq As IMsgSetRequest = sessManager.CreateMsgSetRequest("US", 2, 0)
            empmsgSetRq.Attributes.OnError = ENRqOnError.roeContinue

        
            Dim syncemployee As IEmployeeQuery = empmsgSetRq.AppendEmployeeQueryRq
        
            'step2: begin QB session and send the request
            sessManager.OpenConnection("App", "TimeLive Quickbooks")
            sessManager.BeginSession("", ENOpenMode.omDontCare)
            empmsgSetRs = sessManager.DoRequests(empmsgSetRq)

            Dim emprespList As IResponseList
            emprespList = empmsgSetRs.ResponseList

            If (emprespList Is Nothing) Then
                Exit Sub
            End If

            ' Should only expect 1 response
            Dim empresp As IResponse
            empresp = emprespList.GetAt(0)


            Dim empRetList As IEmployeeRetList
            empRetList = empresp.Detail

            RichTextBox1.Text = "Click button to start transfer" & vbCrLf
            Dim nDepartmentId As Integer = objServices.GetDepartmentId()
            Dim nRoleId As Integer = objEmployeeServices.GetUserRoleId()
            Dim nLocationId As Integer = objServices.GetLocationId()
            Dim nEmployeeTypeId As Guid = objEmployeeServices.GetEmployeeTypeId()
            Dim nEmployeeStatusId As Integer = objEmployeeServices.GetEmployeeStatusId()
            Dim nWorkingDayTypeId As Guid = objEmployeeServices.GetEmployeeWorkingDayTypeId()
            Dim nBillingTypeId As Integer = objEmployeeServices.GetEmployeeBillingTypeId()

            Dim empRetListCount As Integer
            Dim vendorRetListCount As Integer

            If empRetList Is Nothing Then
                empRetListCount = 0
            Else
                empRetListCount = empRetList.Count
            End If


            Dim EmailAddress As String
            Dim pblenth As Integer = empRetListCount + vendorRetListCount - 1
            If pblenth >= 0 Then
                pgbar.Maximum = pblenth
            End If

            If (empresp.StatusCode = 0) Then
                ' Should only be 1 CustomerRet object returned

                For i As Integer = 0 To empRetList.Count - 1
                    pgbar.Increment(i)
                    empRet = empRetList.GetAt(i)
                    With empRet
                        Try
                            EmailAddress = GetEmailAddress(.Email, False)
                            objEmployeeServices.InsertEmployee(EmailAddress, _
                            EmailAddress, GetValue(.FirstName, "FirstName", False), _
                            GetValue(.LastName, "LastName", False), EmailAddress, "", nDepartmentId, nRoleId, nLocationId, _
                            233, nBillingTypeId, Now.Date, -1, 0, 6, 0, 0, nEmployeeTypeId, nEmployeeStatusId, _
                            "", GetValue(.HiredDate, "HiredDate", False), Now.Date, nWorkingDayTypeId, System.Guid.Empty, 0, System.Guid.Empty, False, "", "", "", "", "", "", "", "", "", "Mr.", True)
                            'TextBox1.Text = TextBox1.Text & vbCrLf & "[" & empRet.Name.GetValue & "] transferred successfully."
                            RichTextBox1.Text = RichTextBox1.Text & vbCrLf & "[" & empRet.Name.GetValue & "] transferred successfully."
                        Catch ex As Exception
                            'TextBox1.Text = TextBox1.Text & vbCrLf & "[" & empRet.Name.GetValue & "] transferred failed."
                            RichTextBox1.Text = RichTextBox1.Text & vbCrLf & "[" & empRet.Name.GetValue & "] transferred failed."
                        End Try
                    End With
                Next
            End If
            If empmsgSetRs.ResponseList.GetAt(0).StatusSeverity = "Error" Then
                Throw New Exception(empmsgSetRs.ResponseList.GetAt(0).StatusMessage)
            End If

        Catch ex As Exception
            If ex.Message.Contains("Specified email already") Then
                Throw New Exception("Specified user already exist.")
            Else
                Throw ex
            End If
        Finally
            If Not sessManager Is Nothing Then
                sessManager.EndSession()
                sessManager.CloseConnection()
            End If
        End Try
    End Sub
    Public Function SetLength(ByVal str As String) As String
        If str.Length > 50 Then
            str = str.Substring(0, 50)
        End If
        Return str
    End Function
    Private Sub btnAddEmployeeInTimeLive_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddEmployeeInTimeLive.Click
        pgbar.Value = 0
        Try
            GetAndAddEmployeeInTimeLive()
            MessageBox.Show("Record(s) transferred successfully")
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub AddEmployeeInTimeLive_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        'MainMenu.Enabled = True
    End Sub
    Public Function GetValue(ByVal Value As Object, ColumnName As String, IsFromVendor As Boolean) As Object
        If Not Value Is Nothing And ColumnName = "HiredDate" Then
            Return Value.GetValue
        ElseIf Value Is Nothing And ColumnName = "HiredDate" Then
            Return Now.Date
        Else
            If Not IsFromVendor Then
                Return GetEmployeeValue(ColumnName)
            Else
                Return GetVendorValue(ColumnName)
            End If
        End If
    End Function
    Public Function GetEmployeeValue(ColumnName As String)
        Dim EmployeeName() As String = empRet.Name.GetValue.Split(" ")
        If EmployeeName.Length = 2 Then
            If ColumnName = "FirstName" Then
                Return EmployeeName(0)
            ElseIf ColumnName = "LastName" Then
                Return EmployeeName(1)
            End If
        ElseIf EmployeeName.Length = 1 Then
            If ColumnName = "FirstName" Then
                Return EmployeeName(0)
            ElseIf ColumnName = "LastName" Then
                Return EmployeeName(0)
            End If
        ElseIf EmployeeName.Length = 3 Then
            If ColumnName = "FirstName" Then
                Return EmployeeName(0)
            ElseIf ColumnName = "LastName" Then
                Return EmployeeName(2)
            End If
        End If
    End Function
    Public Function GetVendorValue(ColumnName As String)
        Dim EmployeeName() As String = vendorRet.Name.GetValue.Split(" ")
        If EmployeeName.Length = 2 Then
            If ColumnName = "FirstName" Then
                Return EmployeeName(0)
            ElseIf ColumnName = "LastName" Then
                Return EmployeeName(1)
            End If
        ElseIf EmployeeName.Length = 1 Then
            If ColumnName = "FirstName" Then
                Return EmployeeName(0)
            ElseIf ColumnName = "LastName" Then
                Return EmployeeName(0)
            End If
        ElseIf EmployeeName.Length = 3 Then
            If ColumnName = "FirstName" Then
                Return EmployeeName(0)
            ElseIf ColumnName = "LastName" Then
                Return EmployeeName(2)
            End If
        End If
    End Function
    Public Function GetEmailAddress(Value As Object, IsFromVendor As Boolean) As String
        Dim objEmployeeServices As New Services.TimeLive.Employees.Employees
        Dim authentication As New Services.TimeLive.Employees.SecuredWebServiceHeader
        authentication.AuthenticatedToken = p_token
        objEmployeeServices.SecuredWebServiceHeaderValue = authentication
        If Not Value Is Nothing Then
            Dim EmailAddress As String = Value.GetValue()
            If objEmployeeServices.IsEmployeeExistsByEmailAddress(EmailAddress) Then
                If Not IsFromVendor Then
                    Return empRet.ListID.GetValue()
                Else
                    Return vendorRet.ListID.GetValue()
                End If
            Else
                Return EmailAddress
            End If
        Else
            If Not IsFromVendor Then
                Return empRet.ListID.GetValue()
            Else
                Return vendorRet.ListID.GetValue()
            End If
        End If
    End Function
    'Public Function GetMessage(ColumnName As String, IsFromVendor As Boolean)
    '    Dim msg As String = "Employee """ & ColumnName & """ is missing."
    '    If IsFromVendor Then
    '        If Not vendorRet.FirstName Is Nothing Then
    '            msg = IIf(msg <> "", msg & " ", msg) & "First name is " & vendorRet.FirstName.GetValue & "."
    '        End If
    '        If Not vendorRet.LastName Is Nothing Then
    '            msg = IIf(msg <> "", msg & " ", msg) & "Last name is " & vendorRet.LastName.GetValue & "."
    '        End If
    '        If Not vendorRet.Email Is Nothing Then
    '            msg = IIf(msg <> "", msg & " ", msg) & "Email Address is " & vendorRet.Email.GetValue & "."
    '        End If
    '    Else
    '        If Not empRet.FirstName Is Nothing Then
    '            msg = IIf(msg <> "", msg & " ", msg) & "First name is " & empRet.FirstName.GetValue & "."
    '        End If
    '        If Not empRet.LastName Is Nothing Then
    '            msg = IIf(msg <> "", msg & " ", msg) & "Last name is " & empRet.LastName.GetValue & "."
    '        End If
    '        If Not empRet.Email Is Nothing Then
    '            msg = IIf(msg <> "", msg & " ", msg) & "Email Address is " & empRet.Email.GetValue & "."
    '        End If
    '    End If
    '    Return msg
    'End Function
End Class