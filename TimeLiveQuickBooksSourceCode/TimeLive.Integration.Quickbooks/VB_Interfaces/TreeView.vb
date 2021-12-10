Public Class TLQBTreeView
    Private Sub CustomerJobTreeView_AfterSelect(sender As Object, e As TreeViewEventArgs) Handles CustomerJobQBTreeView.AfterSelect
        'TextBoxKey.Text = CustomerJobTreeView.SelectedNode.Text
        Dim selected_node = CustomerJobQBTreeView.SelectedNode.Text


        ' Getting customers and jobs data from Quickbooks

        Dim obj1_QBtoTL_Customer As New QBtoTL_Customer
        Dim customerData As New QBtoTL_Customer.CustomerDataStructureQB

        Dim obj_QBtoTL_JobOrItem As New QBtoTL_JobOrItem
        Dim jobData As New QBtoTL_JobOrItem.JobDataStructureQB


        customerData = obj1_QBtoTL_Customer.GetCustomerQBData()
        jobData = obj_QBtoTL_JobOrItem.GetJobSubJobData()



        For Each customer As QBtoTL_Customer.Customer In customerData.DataArray
            If customer.QB_Name = selected_node Then
                TextBoxKey.Text = customer.QB_ID
                TextBoxName.Text = customer.QB_Name

                Exit Sub
            End If
        Next
        For Each jobs As QBtoTL_JobOrItem.Job_Subjob In jobData.DataArray
            If jobs.QB_Name = selected_node Then
                TextBoxKey.Text = jobs.QB_ID
                TextBoxName.Text = jobs.QB_Name

                Exit Sub
            End If
        Next


    End Sub

    Private Sub CustomerJobTLTreeView_AfterSelect(sender As Object, e As TreeViewEventArgs) Handles CustomerJobTLTreeView.AfterSelect
        Dim selected_node = CustomerJobTLTreeView.SelectedNode.Text
        Dim obj_main As New MAIN


        'Retreiving customers, projects, task from timelive 
        ' Connect to TimeLive Client
        Dim objClientServices As Services.TimeLive.Clients.Clients = MAIN.connect_TL_clients(obj_main.p_token)
        Dim objClientArray() As Object = objClientServices.GetClients()
        'Dim objClient As New Services.TimeLive.Clients.Client

        ' Connect to TimeLive Tasks
        Dim objTaskServices As Services.TimeLive.Tasks.Tasks = MAIN.connect_TL_tasks(obj_main.p_token)
        Dim objTaskArray() As Object = objTaskServices.GetTasks
        'Dim objTask As New Services.TimeLive.Tasks.Task

        'Connect to TimeLive Projects
        Dim objProjectServices As Services.TimeLive.Projects.Projects = MAIN.connect_TL_projects(obj_main.p_token)
        Dim objProjectArray() As Object = objProjectServices.GetProjects

        For Each client As Services.TimeLive.Clients.Client In objClientArray
            If client.ClientName = selected_node Then
                TextBoxTimeLiveID.Text = objClientServices.GetClientIdByName(client.ClientName)
                TextBoxTimeLiveName.Text = client.ClientName
                TextBoxFullNameTL.Text = client.ClientName
                Exit Sub
            End If
        Next

        For Each project As Services.TimeLive.Projects.Project In objProjectArray
            If project.ProjectName = selected_node Then
                TextBoxTimeLiveID.Text = objProjectServices.GetProjectId(project.ProjectName)
                TextBoxTimeLiveName.Text = project.ProjectName
                TextBoxFullNameTL.Text = project.ClientName + ":" + project.ProjectName
                Exit Sub
            End If
        Next
        For Each task As Services.TimeLive.Tasks.Task In objTaskArray
            If task.TaskName = selected_node Then
                TextBoxTimeLiveID.Text = objTaskServices.GetTaskId(task.TaskName)
                TextBoxTimeLiveName.Text = task.TaskName
                TextBoxFullNameTL.Text = task.JobParent + ":" + task.TaskName
                Exit Sub
            End If
        Next


    End Sub


    Private Sub btnAddNode_Click(sender As Object, e As EventArgs) Handles btnAddNodeTL.Click

        Me.Close()

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
        Dim obj_main As New MAIN


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

            'obj_main.generate_treeview(obj_main.p_token)
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
            'obj_main.generate_treeview(obj_main.p_token)
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

            'obj_main.generate_treeview(obj_main.p_token)
        End If

    End Sub

    Private Sub ButtonTreeViewClose_Click(sender As Object, e As EventArgs) Handles ButtonTreeViewClose.Click

        Me.Close()
        MAIN.treeViewFlag = True
    End Sub

    Private Sub btnAddNodeQB_Click(sender As Object, e As EventArgs) Handles btnAddNodeQB.Click

    End Sub
End Class