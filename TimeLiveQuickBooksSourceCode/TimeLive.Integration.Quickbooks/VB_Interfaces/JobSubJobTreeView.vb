Imports QBFC13Lib
Public Class JobSubJobTreeView
    Dim obj_main As New MAIN
    Dim job_TLSync As Sync_TLtoQB_JoborItem = New Sync_TLtoQB_JoborItem()
    Dim customer_TLSync As Sync_TLtoQB_Customer = New Sync_TLtoQB_Customer()
    Dim job_qbtotl As New QBtoTL_JobOrItem
    Dim JobData As New QBtoTL_JobOrItem.JobDataStructureQB
    Dim customerData As New QBtoTL_Customer.CustomerDataStructureQB
    Dim customer_qbtotl As QBtoTL_Customer = New QBtoTL_Customer

    Private Sub JobSubJobTreeView_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        customerData = customer_qbtotl.GetCustomerQBData(Nothing, False)
        JobData = job_qbtotl.GetJobSubJobData(MAIN, MAIN.p_token.ToString(), True)
    End Sub

    Private Sub TreeViewJobSubJob_AfterSelect(sender As Object, e As TreeViewEventArgs) Handles TreeViewJobSubJob.AfterSelect
        Dim selected_node = TreeViewJobSubJob.SelectedNode.Text
        Dim obj_main As New MAIN
        TextBoxTimeLiveID.Text = ""
        TextBoxTimeLiveName.Text = ""
        TextBoxFullNameTL.Text = ""

        If Me.RadioButtonBothQBTL.Checked Or Me.RadioButtonTimeLive.Checked Then
            'Retreiving customers, projects, task from timelive 
            ' Connect to TimeLive Client
            Dim objClientServices As Services.TimeLive.Clients.Clients = MAIN.connect_TL_clients(MAIN.p_token.ToString())
            Dim objClientArray() As Object = objClientServices.GetClients()


            ' Connect to TimeLive Tasks
            Dim objTaskServices As Services.TimeLive.Tasks.Tasks = MAIN.connect_TL_tasks(MAIN.p_token.ToString())
            Dim objTaskArray() As Object = objTaskServices.GetTasks
            'Dim objTask As New Services.TimeLive.Tasks.Task

            'Connect to TimeLive Projects
            Dim objProjectServices As Services.TimeLive.Projects.Projects = MAIN.connect_TL_projects(MAIN.p_token.ToString())
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

            If String.IsNullOrEmpty(TextBoxTimeLiveID.Text) Then
                For Each project As Services.TimeLive.Projects.Project In objProjectArray
                    If project.ProjectName = selected_node Then
                        TextBoxTimeLiveID.Text = objProjectServices.GetProjectId(project.ProjectName)
                        TextBoxTimeLiveName.Text = project.ProjectName
                        TextBoxFullNameTL.Text = project.ClientName + ":" + project.ProjectName
                        Exit For
                    End If
                Next
            End If

            If String.IsNullOrEmpty(TextBoxTimeLiveID.Text) Then
                For Each task As Services.TimeLive.Tasks.Task In objTaskArray
                    If task.TaskName = selected_node Then
                        TextBoxTimeLiveID.Text = objTaskServices.GetTaskId(task.TaskName)
                        TextBoxTimeLiveName.Text = task.TaskName
                        TextBoxFullNameTL.Text = task.JobParent + ":" + task.TaskName
                        Exit For
                    End If
                Next
            End If
        Else
            For Each customer As QBtoTL_Customer.Customer In customerData.DataArray
                If customer.QB_Name = selected_node Then
                    TextBoxTimeLiveID.Text = customer.QB_ID
                    TextBoxTimeLiveName.Text = customer.QB_Name
                    TextBoxFullNameTL.Text = customer.QB_Name
                    Exit For
                End If
            Next

            If String.IsNullOrEmpty(TextBoxTimeLiveID.Text) Then
                For Each job As QBtoTL_JobOrItem.Job_Subjob In JobData.DataArray
                    If job.QB_Name = selected_node Then
                        TextBoxTimeLiveID.Text = job.QB_ID
                        TextBoxTimeLiveName.Text = job.QB_Name
                        TextBoxFullNameTL.Text = job.FullName
                        Exit For
                    End If
                Next
            End If

        End If



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


        Dim new_node As String = InputBox("Enter Node Name").Trim()
        'Check if the new node is empty or not
        If String.IsNullOrEmpty(new_node) Then
            MessageBox.Show("New node can not be empty or null")
            Exit Sub
        End If

        If Me.RadioButtonQuickBooks.Checked Then
            'Check if new node is in QuickBooks
            Dim isInQb As Boolean
            For Each job As QBtoTL_JobOrItem.Job_Subjob In JobData.DataArray
                isInQb = If(job.QB_Name = new_node, True, False)
                Exit For
            Next
            If isInQb Then
                MsgBox("The new Node is in QuickBooks, can not create new node")
            Else
                Dim newMsgSetRq = MAIN.SESSMANAGER.CreateMsgSetRequest("US", 2, 0)

                Dim jobAdd As ICustomerAdd = newMsgSetRq.AppendCustomerAddRq
                jobAdd.ParentRef.FullName.SetValue(fullname)
                jobAdd.Name.SetValue(new_node)

                Dim msgSetRs As IMsgSetResponse = MAIN.SESSMANAGER.DoRequests(newMsgSetRq)
                Dim res As IResponse
                res = msgSetRs.ResponseList.GetAt(0)
            End If
            Me.Close()
            Exit Sub
        End If


        'connecting timelive
        Dim objProjectServices As Services.TimeLive.Projects.Projects = MAIN.connect_TL_projects(MAIN.p_token.ToString())
        Dim objTaskServices As Services.TimeLive.Tasks.Tasks = MAIN.connect_TL_tasks(MAIN.p_token.ToString())
        Dim objClientServices As Services.TimeLive.Clients.Clients = MAIN.connect_TL_clients(MAIN.p_token.ToString())

        Dim objServices As New Services.TimeLiveServices
        Dim authentication As New Services.SecuredWebServiceHeader
        authentication.AuthenticatedToken = MAIN.p_token.ToString()
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


        Dim nodes As New List(Of String)

        If Me.RadioButtonTimeLive.Checked Then
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
        ElseIf Me.RadioButtonBothQBTL.Checked Then
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
            job_TLSync.SyncJobsSubJobData(MAIN.p_token.ToString(), obj_main, True, nodes)
        Else


        End If


        Me.Close()
    End Sub
    Private Function AddTimeLiveNode()

    End Function

    Private Sub ButtonTreeViewClose_Click(sender As Object, e As EventArgs) Handles ButtonTreeViewClose.Click
        Me.Close()
    End Sub

    Private Sub RadioButtonQuickBooks_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButtonQuickBooks.Click
        GenerateTreeView("QB")
    End Sub

    Private Sub RadioButtonBothQBTL_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButtonBothQBTL.Click
        GenerateTreeView("QBTL")
    End Sub

    Private Sub RadioButtonTimeLive_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButtonTimeLive.Click
        GenerateTreeView("TL")
    End Sub

    Private Function GenerateTreeView(input As String)
        Me.TreeViewJobSubJob.Nodes.Clear()

        Dim jobDataArray As New List(Of Array)
        'QuickBooks and TimeLive
        If input = "QBTL" Then
            Me.Label1.Text = "Both Quicks and TimeLive"
            For Each row As DataGridViewRow In MAIN.DataGridView1.Rows
                If row.DefaultCellStyle.ForeColor = Color.Blue Then
                    Dim fullName As String = row.Cells("Full Name").Value
                    Dim fullNameArray() As String = Split(fullName, " --> ")
                    jobDataArray.Add(fullNameArray)
                End If
            Next
        ElseIf input = "QB" Then
            Me.Label1.Text = "QuickBooks only"
            For Each row As DataGridViewRow In MAIN.DataGridView1.Rows
                Dim fullName As String = row.Cells("Full Name").Value
                Dim fullNameArray() As String = Split(fullName, " --> ")
                jobDataArray.Add(fullNameArray)
            Next
        Else
            Me.Label1.Text = "TimeLive only"
            For Each row As DataGridViewRow In MAIN.DataGridView2.Rows
                Dim fullName As String = row.Cells("Name").Value
                Dim fullNameArray() As String = Split(fullName, " --> ")
                jobDataArray.Add(fullNameArray)
            Next
        End If

        'add the first node 
        Dim customerNode As New List(Of String)
        For i As Integer = 0 To jobDataArray.Count - 1
            If jobDataArray(i).Length = 2 Then
                Dim customerName = jobDataArray(i)(0)
                If Not customerNode.Contains(customerName) Then
                    customerNode.Add(customerName)
                End If
            End If
        Next

        For Each customer As String In customerNode
            Me.TreeViewJobSubJob.Nodes.Add(customer, customer)
        Next

        For i As Integer = 0 To jobDataArray.Count - 1
            Dim lengthArr As Integer = jobDataArray(i).Length
            If lengthArr = 1 Then
                Continue For
            ElseIf lengthArr = 2 Then
                Me.TreeViewJobSubJob.Nodes(jobDataArray(i)(0)).Nodes.Add(jobDataArray(i)(1), jobDataArray(i)(1))
            ElseIf lengthArr = 3 Then
                Me.TreeViewJobSubJob.Nodes(jobDataArray(i)(0)).Nodes(jobDataArray(i)(1)).Nodes.Add(jobDataArray(i)(2), jobDataArray(i)(2))
            ElseIf lengthArr = 4 Then
                Me.TreeViewJobSubJob.Nodes(jobDataArray(i)(0)).Nodes(jobDataArray(i)(1)).Nodes(jobDataArray(i)(2)).Nodes.Add(jobDataArray(i)(3), jobDataArray(i)(3))
            ElseIf lengthArr = 5 Then
                Me.TreeViewJobSubJob.Nodes(jobDataArray(i)(0)).Nodes(jobDataArray(i)(1)).Nodes(jobDataArray(i)(2)).Nodes(jobDataArray(i)(3)).Nodes.Add(jobDataArray(i)(4), jobDataArray(i)(4))
            Else
                Throw New Exception("Created More than 5 level of a tree")
            End If
        Next
        Me.TreeViewJobSubJob.ExpandAll()
        Me.TreeViewJobSubJob.Update()

    End Function


End Class