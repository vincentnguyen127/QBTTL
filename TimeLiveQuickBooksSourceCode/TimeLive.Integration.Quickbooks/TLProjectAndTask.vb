Imports Interop.QBFC10
Public Class TLProjectAndTask
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
    ' Queries QuickBooks for all the customers and displays
    Public Sub GetAndAddProjectAndTaskInTimeLiveByItemSubItem()
        Dim objProjectServices As New Services.TimeLive.Projects.Projects
        Dim authentication As New Services.TimeLive.Projects.SecuredWebServiceHeader
        authentication.AuthenticatedToken = p_token
        objProjectServices.SecuredWebServiceHeaderValue = authentication

        Dim objTaskServices As New Services.TimeLive.Tasks.Tasks
        Dim authentication2 As New Services.TimeLive.Tasks.SecuredWebServiceHeader
        authentication2.AuthenticatedToken = p_token
        objTaskServices.SecuredWebServiceHeaderValue = authentication2

        Dim objClientServices As New Services.TimeLive.Clients.Clients
        Dim authentication3 As New Services.TimeLive.Clients.SecuredWebServiceHeader
        authentication3.AuthenticatedToken = p_token
        objClientServices.SecuredWebServiceHeaderValue = authentication3

        Dim objServices As New Services.TimeLiveServices
        Dim authentication4 As New Services.SecuredWebServiceHeader
        authentication4.AuthenticatedToken = p_token
        objServices.SecuredWebServiceHeaderValue = authentication4

        'step1: create QBFC session manager and prepare the request
        Dim sessManager As QBSessionManager
        Dim msgSetRs As IMsgSetResponse
        Try
            sessManager = New QBSessionManagerClass()
            Dim msgSetRq As IMsgSetRequest = sessManager.CreateMsgSetRequest("US", 2, 0)
            msgSetRq.Attributes.OnError = ENRqOnError.roeContinue
            Dim synccust As IItemServiceQuery = msgSetRq.AppendItemServiceQueryRq

            'step2: begin QB session and send the request
            sessManager.OpenConnection("App", "TimeLive Quickbooks")
            sessManager.BeginSession("", ENOpenMode.omDontCare)
            msgSetRs = sessManager.DoRequests(msgSetRq)
            Dim respList As IResponseList
            respList = msgSetRs.ResponseList
            If (respList Is Nothing) Then
                Exit Sub
            End If
            ' Should only expect 1 response
            Dim resp As IResponse
            resp = respList.GetAt(0)
            If (resp.StatusCode = 0) Then
                Dim ptRetList As IItemServiceRetList
                ptRetList = resp.Detail
                ' Should only be 1 CustomerRet object returned
                Dim ptRet As IItemServiceRet
                Dim nProjectTypeId As Integer = objProjectServices.GetProjectTypeId()
                Dim nClientId As Integer
                Try
                    nClientId = objClientServices.GetClientId()
                Catch ex As Exception
                    Throw New Exception("Client not exist.")
                End Try

                Dim nProjectBillingTypeId As Integer = objProjectServices.GetProjectBillingTypeId()
                Dim nProjectStatusId As Integer = objProjectServices.GetProjectStatusId()
                Dim nTeamLeadId As Integer = objProjectServices.GetTeamLeadId()
                Dim nProjectManagerId As Integer = objProjectServices.GetProjectManagerId()
                Dim nProjectBillingRateTypeId As Integer = objProjectServices.GetProjectBillingRateTypeId()
                Dim ProjectName As String

                Dim pblenth As Integer = ptRetList.Count - 1
                If pblenth >= 0 Then
                    pgbar.Maximum = pblenth
                End If

                For i As Integer = 0 To ptRetList.Count - 1
                    pgbar.Increment(i)
                    ptRet = ptRetList.GetAt(i)
                    With ptRet
                        If .ParentRef Is Nothing Then

                            objProjectServices.InsertProject(nProjectTypeId, nClientId, 0,
                            0, nProjectBillingTypeId, .FullName.GetValue, .FullName.GetValue,
                            Now.Date, Now.AddMonths(1).Date, nProjectStatusId, nTeamLeadId,
                            nProjectManagerId, 0, 0, 1, "Months", .FullName.GetValue, 0, nProjectBillingRateTypeId,
                            False, True, 0, Now.Date, nTeamLeadId, Now.Date, nTeamLeadId, False)
                            ProjectName = .FullName.GetValue
                        End If
                        If Not .ParentRef Is Nothing Then
                            Dim TaskArray() As String = Split(.FullName.GetValue, ":")
                            Dim ParentLevel As Integer = .Sublevel.GetValue - 1
                            Dim ParentTaskName As String = TaskArray(ParentLevel)
                            Dim nProjectId As Integer = objProjectServices.GetProjectId(ProjectName)
                            Dim nParentTaskId As Integer
                            If ParentLevel <> 0 Then
                                nParentTaskId = objTaskServices.GetParentTaskId(ParentTaskName)
                                objTaskServices.UpdateIsParentInTask(nParentTaskId, True)
                            Else
                                nParentTaskId = 0
                            End If
                            Dim nProjectMilestoneId As Integer = objProjectServices.GetProjectMilestoneIdByProjectId(nProjectId)
                            Dim nTaskTypeId As Integer = objTaskServices.GetTaskTypeId()
                            Dim nTaskStatusId As Integer = objTaskServices.GetTaskStatusId()
                            Dim nPriorityId As Integer = objTaskServices.GetTaskPriorityId()
                            Dim nCurrencyId As Integer = objServices.GetCurrencyId()
                            objTaskServices.InsertTask(nProjectId, nParentTaskId, .Name.GetValue, .Name.GetValue,
                            nTaskTypeId, 1, "Months", 0,
                            0, Now.AddMonths(1).Date, nTaskStatusId, nPriorityId,
                            nProjectMilestoneId, False, False, Now.Date, nTeamLeadId, Now.Date, nTeamLeadId, 0, 0, "Days",
                            True, .Name.GetValue, 0, False, nCurrencyId)
                        End If
                    End With
                Next
            End If
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
    Public Function SetLength(ByVal str As String) As String
        If str.Length > 50 Then
            str = str.Substring(0, 50)
        End If
        Return str
    End Function
    Private Sub btnAddProjectAndTaskInTimeLive_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddProjectAndTaskInTimeLive.Click
        Try
            pgbar.Value = 0
            If rbJob.Checked = True Then
                GetAndAddProjectAndTaskInTimeLiveByJobSubJob()
            ElseIf rbItem.Checked = True Then
                GetAndAddProjectAndTaskInTimeLiveByItemSubItem()
            End If
            MessageBox.Show("Record(s) transferred successfully")
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub AddProjectAndTaskInTimeLive_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        'MainMenu.Enabled = True
    End Sub
    ' Queries QuickBooks for all the customers and displays
    Public Sub GetAndAddProjectAndTaskInTimeLiveByJobSubJob()
        Dim objProjectServices As New Services.TimeLive.Projects.Projects
        Dim authentication As New Services.TimeLive.Projects.SecuredWebServiceHeader
        authentication.AuthenticatedToken = p_token
        objProjectServices.SecuredWebServiceHeaderValue = authentication

        Dim objTaskServices As New Services.TimeLive.Tasks.Tasks
        Dim authentication2 As New Services.TimeLive.Tasks.SecuredWebServiceHeader
        authentication2.AuthenticatedToken = p_token
        objTaskServices.SecuredWebServiceHeaderValue = authentication2

        Dim objClientServices As New Services.TimeLive.Clients.Clients
        Dim authentication3 As New Services.TimeLive.Clients.SecuredWebServiceHeader
        authentication3.AuthenticatedToken = p_token
        objClientServices.SecuredWebServiceHeaderValue = authentication3

        Dim objServices As New Services.TimeLiveServices
        Dim authentication4 As New Services.SecuredWebServiceHeader
        authentication4.AuthenticatedToken = p_token
        objServices.SecuredWebServiceHeaderValue = authentication4

        'step1: create QBFC session manager and prepare the request
        Dim sessManager As QBSessionManager
        Dim msgSetRs As IMsgSetResponse
        Try
            sessManager = New QBSessionManagerClass()
            Dim msgSetRq As IMsgSetRequest = sessManager.CreateMsgSetRequest("US", 2, 0)
            msgSetRq.Attributes.OnError = ENRqOnError.roeContinue
            Dim synccust As ICustomerQuery = msgSetRq.AppendCustomerQueryRq

            'step2: begin QB session and send the request
            sessManager.OpenConnection("App", "TimeLive Quickbooks")
            sessManager.BeginSession("", ENOpenMode.omDontCare)
            msgSetRs = sessManager.DoRequests(msgSetRq)
            Dim respList As IResponseList
            respList = msgSetRs.ResponseList
            If (respList Is Nothing) Then
                Exit Sub
            End If
            ' Should only expect 1 response
            Dim resp As IResponse
            resp = respList.GetAt(0)
            If (resp.StatusCode = 0) Then
                Dim ptRetList As ICustomerRetList
                ptRetList = resp.Detail
                ' Should only be 1 CustomerRet object returned
                Dim ptRet As ICustomerRet

                Dim nProjectTypeId As Integer = objProjectServices.GetProjectTypeId()
                Dim nProjectBillingTypeId As Integer = objProjectServices.GetProjectBillingTypeId()
                Dim nProjectStatusId As Integer = objProjectServices.GetProjectStatusId()
                Dim nTeamLeadId As Integer = objProjectServices.GetTeamLeadId()
                Dim nProjectManagerId As Integer = objProjectServices.GetProjectManagerId()
                Dim nProjectBillingRateTypeId As Integer = objProjectServices.GetProjectBillingRateTypeId()

                Dim pblenth As Integer = ptRetList.Count - 1
                If pblenth >= 0 Then
                    pgbar.Maximum = pblenth
                End If

                For i As Integer = 0 To ptRetList.Count - 1
                    pgbar.Increment(i)
                    ptRet = ptRetList.GetAt(i)
                    With ptRet
                        If Not .ParentRef Is Nothing Then
                            Dim PTArray() As String = Split(.FullName.GetValue, ":")
                            If PTArray.Length = 2 Then
                                Dim nClientId As Integer
                                Try
                                    nClientId = objClientServices.GetClientIdByName(PTArray(0))
                                Catch ex As Exception
                                    Throw New Exception("Client name """ + PTArray(0) + """ not exist")
                                End Try
                                objProjectServices.InsertProject(nProjectTypeId, nClientId, 0, _
                                0, nProjectBillingTypeId, .Name.GetValue, .Name.GetValue, _
                                Now.Date, Now.AddMonths(1).Date, nProjectStatusId, nTeamLeadId, _
                                nProjectManagerId, 0, 0, 1, "Months", .Name.GetValue, 0, nProjectBillingRateTypeId, _
                                False, True, 0, Now.Date, nTeamLeadId, Now.Date, nTeamLeadId, False)
                            End If
                            If PTArray.Length > 2 Then
                                Dim nProjectId As Integer = objProjectServices.GetProjectId(PTArray(1))
                                Dim nParentTaskId As Integer
                                If PTArray.Length > 3 Then
                                    nParentTaskId = objTaskServices.GetParentTaskId(PTArray(PTArray.Length - 2))
                                    objTaskServices.UpdateIsParentInTask(nParentTaskId, True)
                                Else
                                    nParentTaskId = 0
                                End If
                                Dim nTaskTypeId As Integer = objTaskServices.GetTaskTypeId()
                                Dim nTaskStatusId As Integer = objTaskServices.GetTaskStatusId()
                                Dim nPriorityId As Integer = objTaskServices.GetTaskPriorityId()
                                Dim nProjectMilestoneId As Integer = objProjectServices.GetProjectMilestoneIdByProjectId(nProjectId)
                                Dim nCurrencyId As Integer = objServices.GetCurrencyId()

                                objTaskServices.InsertTask(nProjectId, nParentTaskId, .Name.GetValue, .Name.GetValue, _
                                nTaskTypeId, 1, "Months", 0, _
                                0, Now.AddMonths(1).Date, nTaskStatusId, nPriorityId, _
                                nProjectMilestoneId, False, False, Now.Date, nTeamLeadId, Now.Date, nTeamLeadId, 0, 0, "Days", _
                                True, .Name.GetValue, 0, False, nCurrencyId)
                            End If
                        End If
                    End With
                Next
            End If
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

    Private Sub TLProjectAndTask_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class