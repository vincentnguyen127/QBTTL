Public Class JobSubJobTreeView
    Dim obj_main As New MAIN
    Dim job_TLSync As Sync_TLtoQB_JoborItem = New Sync_TLtoQB_JoborItem()
    Dim customer_TLSync As Sync_TLtoQB_Customer = New Sync_TLtoQB_Customer()

    Private Sub TreeViewJobSubJob_AfterSelect(sender As Object, e As TreeViewEventArgs) Handles TreeViewJobSubJob.AfterSelect
        Dim selected_node = TreeViewJobSubJob.SelectedNode.Text
        Dim obj_main As New MAIN

        'Retreiving customers, projects, task from timelive 
        ' Connect to TimeLive Client
        Dim objClientServices As Services.TimeLive.Clients.Clients = MAIN.connect_TL_clients(obj_main.p_token)
        Dim objClientArray() As Object = objClientServices.GetClients()


        ' Connect to TimeLive Tasks
        Dim objTaskServices As Services.TimeLive.Tasks.Tasks = MAIN.connect_TL_tasks(obj_main.p_token)
        Dim objTaskArray() As Object = objTaskServices.GetTasks
        'Dim objTask As New Services.TimeLive.Tasks.Task

        'Connect to TimeLive Projects
        Dim objProjectServices As Services.TimeLive.Projects.Projects = MAIN.connect_TL_projects(obj_main.p_token)
        Dim objProjectArray() As Object = objProjectServices.GetProjects

        Dim obj_client As Services.TimeLive.Clients.Client

        For Each client As Services.TimeLive.Clients.Client In objClientArray
            If client.ClientName = selected_node Then
                TextBoxTimeLiveID.Text = objClientServices.GetClientIdByName(client.ClientName)
                TextBoxTimeLiveName.Text = client.ClientName
                TextBoxFullNameTL.Text = client.ClientName
                obj_client = client
                Exit For
            End If
        Next

        For Each project As Services.TimeLive.Projects.Project In objProjectArray
                If project.ProjectName = selected_node Then
                    TextBoxTimeLiveID.Text = objProjectServices.GetProjectId(project.ProjectName)
                    TextBoxTimeLiveName.Text = project.ProjectName
                    TextBoxFullNameTL.Text = project.ClientName + ":" + project.ProjectName
                    Exit For
                End If
            Next

        For Each task As Services.TimeLive.Tasks.Task In objTaskArray
            If task.TaskName = selected_node Then
                TextBoxTimeLiveID.Text = objTaskServices.GetTaskId(task.TaskName)
                TextBoxTimeLiveName.Text = task.TaskName
                TextBoxFullNameTL.Text = task.JobParent + ":" + task.TaskName
                Exit For
            End If
        Next


    End Sub

    Private Sub btnAddNode_Click(sender As Object, e As EventArgs) Handles btnAddNode.Click
        'Check the the node is selected or not
        If String.IsNullOrEmpty(TextBoxTimeLiveName.Text.Trim()) Then
            MessageBox.Show("Please select a node first")
            Exit Sub
        End If
        'Gather the inputs 
        Dim selected_node As String = TextBoxTimeLiveName.Text.Trim()
        Dim timeLive_node_id As String = TextBoxTimeLiveID.Text.Trim()
        Dim fullname As String = TextBoxFullNameTL.Text.Trim()
        Dim name() As String = fullname.Split(":")
        If name.Length > 4 Then
            MessageBox.Show("Can't create more than 5 level")
            Exit Sub
        End If

        Dim new_node As String = InputBox("Enter Node Name").Trim()
        'Check if the new node is empty or not
        If String.IsNullOrEmpty(new_node) Then
            MessageBox.Show("New node can not be empty or null")
            Exit Sub
        End If



        'connecting timelive


        Dim objProjectServices As Services.TimeLive.Projects.Projects = MAIN.connect_TL_projects(obj_main.p_token)
        Dim objTaskServices As Services.TimeLive.Tasks.Tasks = MAIN.connect_TL_tasks(obj_main.p_token)
        Dim objClientServices As Services.TimeLive.Clients.Clients = MAIN.connect_TL_clients(obj_main.p_token)

        Dim objServices As New Services.TimeLiveServices
        Dim authentication As New Services.SecuredWebServiceHeader
        authentication.AuthenticatedToken = obj_main.p_token
        objServices.SecuredWebServiceHeaderValue = authentication

        'Open session for TL
        Dim nProjectTypeId As Integer = objProjectServices.GetProjectTypeId()
        Dim nProjectBillingTypeId As Integer = objProjectServices.GetProjectBillingTypeId()
        Dim nProjectStatusId As Integer = objProjectServices.GetProjectStatusId()
        Dim nTeamLeadId As Integer = objProjectServices.GetTeamLeadId()
        Dim nProjectManagerId As Integer = objProjectServices.GetProjectManagerId()
        Dim nProjectBillingRateTypeId As Integer = objProjectServices.GetProjectBillingRateTypeId()



        Dim nTaskTypeId As Integer = objTaskServices.GetTaskTypeId()
        Dim nTaskStatusId As Integer = objTaskServices.GetTaskStatusId()
        Dim nPriorityId As Integer = objTaskServices.GetTaskPriorityId()
        Dim nCurrencyId As Integer = objServices.GetCurrencyId()

        'identify the selected node is client, project, or tasks 
        'if the selected node is a client, create project
        Dim nodes As New List(Of String)
        If Array.Exists(objClientServices.GetClients, Function(clnt As Services.TimeLive.Clients.Client) clnt.ClientName = selected_node) Then
            Try
                objProjectServices.InsertProject(nProjectTypeId, Convert.ToInt32(timeLive_node_id), 0, 0, nProjectBillingTypeId, new_node, "",
                                                 Now.Date, Now.AddMonths(1).Date, nProjectStatusId, nTeamLeadId, nProjectManagerId, 0, 0, 1, "Months", "",
                                                 0, nProjectBillingRateTypeId, False, True, 0, Now.Date, nTeamLeadId, Now.Date, nTeamLeadId, False)



            Catch ex As System.Web.Services.Protocols.SoapException
                ' Do Nothing
            Catch ex As Exception
                Throw ex
            End Try


            'if the selected node is a project, create task 
        ElseIf Array.Exists(objProjectServices.GetProjects, Function(proj As Services.TimeLive.Projects.Project) proj.ProjectName = selected_node) Then
            Dim nProjectId As Integer = objProjectServices.GetProjectId(name(1))
            Dim nProjectMilestoneId As Integer = objProjectServices.GetProjectMilestoneIdByProjectId(nProjectId)
            Try
                objTaskServices.InsertTask(nProjectId, 0, new_node, new_node, nTaskTypeId, 1, "Months", 0, False, Now.AddMonths(1).Date, nTaskStatusId,
                                               nPriorityId, nProjectMilestoneId, False, True, Now.Date, nTeamLeadId, Now.Date, nTeamLeadId, 0, 0,
                                               "Days", True, new_node, 0, False, nCurrencyId)

            Catch ex As System.Web.Services.Protocols.SoapException
                ' Do Nothing
            Catch ex As Exception
                Throw ex
            End Try

            ' if the selected node is a task, create sub stask 
        Else
            Dim nProjectId As Integer = objProjectServices.GetProjectId(name(1))
            Dim nParentTaskId As Integer = objTaskServices.GetTaskId(name(name.Length - 1))
            Dim nProjectMilestoneId As Integer = objProjectServices.GetProjectMilestoneIdByProjectId(nProjectId)
            Try
                objTaskServices.InsertTask(nProjectId, nParentTaskId, new_node, new_node, nTaskTypeId, 1, "Months", 0, False, Now.AddMonths(1).Date, nTaskStatusId,
                                               nPriorityId, nProjectMilestoneId, False, True, Now.Date, nTeamLeadId, Now.Date, nTeamLeadId, 0, 0,
                                               "Days", True, new_node, 0, False, nCurrencyId)
            Catch ex As System.Web.Services.Protocols.SoapException
                ' Do Nothing
            Catch ex As Exception
                Throw ex
            End Try

        End If


        nodes.Add(fullname + ":" + new_node)
        job_TLSync.SyncJobsSubJobData(obj_main.p_token, obj_main, True, nodes)
        Me.Close()
    End Sub

    Private Sub ButtonTreeViewClose_Click(sender As Object, e As EventArgs) Handles ButtonTreeViewClose.Click
        Me.Close()
    End Sub
End Class