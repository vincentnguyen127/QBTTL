Public Class TreeView
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
                Exit Sub
            End If
        Next

        For Each project As Services.TimeLive.Projects.Project In objProjectArray
            If project.ProjectName = selected_node Then
                TextBoxTimeLiveID.Text = objProjectServices.GetProjectId(project.ProjectName)
                TextBoxTimeLiveName.Text = project.ProjectName
                Exit Sub
            End If
        Next
        For Each task As Services.TimeLive.Tasks.Task In objTaskArray
            If task.TaskName = selected_node Then
                TextBoxTimeLiveID.Text = objTaskServices.GetTaskId(task.TaskName)
                TextBoxTimeLiveName.Text = task.TaskName
                Exit Sub
            End If
        Next


    End Sub
End Class