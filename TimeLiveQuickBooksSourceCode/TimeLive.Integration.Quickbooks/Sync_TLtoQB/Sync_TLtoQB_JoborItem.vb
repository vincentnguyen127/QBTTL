Imports QBFC13Lib

Public Class Sync_TLtoQB_JoborItem
    '---------------------Sync Job SubJobs TL Data to QB---------------------------------------
    ''' <summary>
    ''' Sync the Jobs data from QB. Print out Projects and Tasks that are in TL but not QB
    ''' Note: Currently not used, using SyncJobsSubJobData
    ''' </summary>
    Sub SyncJobsSubJobData2(ByVal p_token As String)
        Dim result As Boolean = False
        Dim SubJobsOrSubData As New QBtoTL_JobOrItem.SubJobsOrSubitems

        My.Forms.MAIN.History("Syncing JobSubJob Data", "n")
        Try
            ' connect to Time live
            Dim objProjectServices As New Services.TimeLive.Projects.Projects
            Dim authentication As New Services.TimeLive.Projects.SecuredWebServiceHeader
            authentication.AuthenticatedToken = p_token
            objProjectServices.SecuredWebServiceHeaderValue = authentication

            Dim objProjectArray As Object = objProjectServices.GetProjects
            Dim objProject As New Services.TimeLive.Projects.Project
            Dim ExpectedQBProjectName As String = Nothing
            Dim ExpectedQBTaskName As String = Nothing

            For n As Integer = 0 To objProjectArray.Length - 1
                objProject = objProjectArray(n)


                My.Forms.MAIN.History("Processing TL Project: " + objProject.ProjectName.ToString, "i")
                ExpectedQBProjectName = objProject.ClientName + ":" + objProject.ProjectName.ToString
                'result = checkQBJobSubJobExist(ExpectedQBProjectName, objProject.ProjectID, ExpectedQBProjectName)
                result = checkQBJobSubJobExist(objProject.ClientName, objProject.ProjectName, ExpectedQBProjectName, True, p_token) = -1
                If result = False Then
                    'Does not exist in QB
                    My.Forms.MAIN.History("Please update or enter project in QB --> Name: " + objProject.ProjectName.ToString + vbTab + "ID: " + objProject.ProjectID.ToString(), "I")
                Else
                    ExpectedQBTaskName = ExpectedQBProjectName
                    My.Forms.MAIN.History("Getting tasks related to project: " & objProject.ProjectName.ToString, "i")
                    SubJobsOrSubData = GetTasks(objProject.ProjectName.ToString, p_token)


                    For Each element As QBtoTL_JobOrItem.Job_Item In SubJobsOrSubData.DataArray
                        result = checkQBJobSubJobExist(ExpectedQBTaskName, element.TL_Name, element.TL_ID, True, p_token) = -1
                        If result = False Then
                            'Does not exist in QB
                            My.Forms.MAIN.History("Please update or enter task in QB: " + ExpectedQBTaskName + ":" + element.TL_Name.ToString + " manually", "I")
                        End If
                    Next

                End If
                Exit Sub
            Next

        Catch ex As Exception
            MsgBox("Here 1: " + ex.Message)
        End Try

    End Sub

    ''' <summary>
    ''' Sync the Jobs from QB. Print out Projects and Tasks in TL but not QB
    ''' </summary>
    ''' <param name="p_token"></param>
    Function SyncJobsSubJobData(ByVal p_token As String, Optional MainForm As MAIN = Nothing, Optional ByVal UI As Boolean = True,
                                Optional ByVal nameList As List(Of String) = Nothing, Optional ByVal cancel_opt As Boolean = False)
        Dim numSynced As Integer = 0
        My.Forms.MAIN.History("Syncing JobSubJob Data", "n")

        Try
            ' Connect to TimeLive Projects
            Dim objProjectServices As New Services.TimeLive.Projects.Projects
            Dim authenticationProjects As New Services.TimeLive.Projects.SecuredWebServiceHeader
            authenticationProjects.AuthenticatedToken = p_token
            objProjectServices.SecuredWebServiceHeaderValue = authenticationProjects
            Dim objProjectArray() As Object = objProjectServices.GetProjects
            Dim objProject As New Services.TimeLive.Projects.Project

            ' Connect to Timelive Tasks
            Dim objTaskServices As New Services.TimeLive.Tasks.Tasks
            Dim authenticationTasks As New Services.TimeLive.Tasks.SecuredWebServiceHeader
            authenticationTasks.AuthenticatedToken = p_token
            objTaskServices.SecuredWebServiceHeaderValue = authenticationTasks
            ' Note: Will error if 'Code' is null
            Dim objTaskArray() As Object = objTaskServices.GetTasks
            Dim objTask As New Services.TimeLive.Tasks.Task

            ' Only change progress bar if one exists
            If Not MainForm Is Nothing Then
                MainForm.ProgressBar1.Maximum = nameList.Count
                MainForm.ProgressBar1.Value = 0
            End If

            ' Iterate through all projects
            For n As Integer = 0 To objProjectArray.Length - 1
                objProject = objProjectArray(n)

                ' Done this way since field .ProjectID just returned 0
                Dim projectID As Integer
                Try
                    projectID = objProjectServices.GetProjectId(objProject.ProjectName)
                Catch ex As System.Web.Services.Protocols.SoapException
                    My.Forms.MAIN.History("Make sure that " + objProject.ProjectName + " has a code attribute", "i")
                    Continue For
                End Try

                Dim full_name As String = objProject.ClientName + ":" + objProject.ProjectName
                Dim create As Boolean = If(nameList Is Nothing, True, nameList.Contains(full_name))

                If create Then
                    Dim ret = checkQBJobSubJobExist(objProject.ClientName, objProject.ProjectName, projectID.ToString, UI, p_token, cancel_opt)

                    If ret = -2 Then
                        Return numSynced
                    Else
                        numSynced += Math.Max(0, ret)
                    End If
                End If

                If Not MainForm Is Nothing And create Then MainForm.ProgressBar1.Value += 1
            Next

            ' Iterate through all tasks
            For n As Integer = 0 To objTaskArray.Length - 1
                objTask = objTaskArray(n)
                ' Done this way since field .taskID just returned 0
                Dim taskID As Integer
                Try
                    taskID = objTaskServices.GetTaskId(objTask.TaskName)
                Catch ex As System.Web.Services.Protocols.SoapException
                    My.Forms.MAIN.History("Make sure that " + objTask.TaskName + " has a code attribute", "i")
                    Continue For
                End Try
                Dim full_name As String = objTask.JobParent + ":" + objTask.TaskName
                Dim create As Boolean = If(nameList Is Nothing, True, nameList.Contains(full_name))
                If create Then
                    Dim ret = checkQBJobSubJobExist(objTask.JobParent, objTask.TaskName, taskID.ToString, UI, p_token, cancel_opt)

                    If ret = -2 Then
                        Return numSynced
                    Else
                        numSynced += Math.Max(0, ret)
                    End If
                End If

                    If Not MainForm Is Nothing And create Then My.Forms.MAIN.ProgressBar1.Value += 1
            Next
        Catch ex As System.Web.Services.Protocols.SoapException
            If UI Then
                My.Forms.MAIN.History("Make sure all Tasks and Jobs have a Code in TimeLive: " + ex.Message, "i")
            Else
                ' No UI, then do nothing?
                'Throw New Exception("Make sure all Tasks and Jobs have a Code in Timelive: " + ex.Message)
            End If
        Catch ex As Exception
            If UI Then
                MsgBox("Here 2: " + ex.Message)
            Else
                Throw ex
            End If
        End Try

        Return numSynced
    End Function

    ''' <summary>
    ''' Get Tasks, adding them as jobs to QB
    ''' </summary>
    ''' <param name="ParentName"></param>
    ''' <returns></returns>
    Public Function GetTasks(ParentName As String, ByVal p_token As String) As QBtoTL_JobOrItem.SubJobsOrSubitems
        Dim SubJobsOrSubData As New QBtoTL_JobOrItem.SubJobsOrSubitems
        Try
            ' connect to Timelive
            Dim objTaskServices As New Services.TimeLive.Tasks.Tasks
            Dim authentication As New Services.TimeLive.Tasks.SecuredWebServiceHeader
            authentication.AuthenticatedToken = p_token
            objTaskServices.SecuredWebServiceHeaderValue = authentication

            Dim objTaskArray As Object = objTaskServices.GetTasks
            Dim objTask As New Services.TimeLive.Tasks.Task

            For n As Integer = 0 To objTaskArray.Length - 1
                objTask = objTaskArray(n)
                With objTask
                    ' Done this way because field .taskID just returned 0
                    Dim taskID As Integer = objTaskServices.GetTaskId(.TaskName)
                    My.Forms.MAIN.History("TL Job  Parent: " + .JobParent.ToString + " TL Job Item Parent: " + .JobItemParent.ToString + " TL Item Parent: " + .ItemParent.ToString + vbTab + "TL Job name: " + .TaskName.ToString + ", TL ID: " + taskID.ToString, "i")

                    If .ItemParent = ParentName Then
                        SubJobsOrSubData.NoItems += 1
                        SubJobsOrSubData.DataArray.Add(New QBtoTL_JobOrItem.Job_Item(.TaskName.ToString, taskID.ToString))
                    End If
                End With
            Next

        Catch ex As Exception
            MsgBox("Here 3: " + ex.Message)
        End Try
        Return SubJobsOrSubData
    End Function


    ''' <summary>
    ''' Verifies a Job exists in QB, adding it to the Data Table if necessary
    ''' </summary>
    ''' <param name="TL_Name"></param>
    ''' <param name="TL_ID"></param>
    ''' <returns>
    ''' Integer: the number of items added into QB
    '''     -1 -> Did not exist in QB, not added to QB
    '''     0 -> Already exists in QB
    '''     1 -> Did not exist in QB, added to QB
    '''     2+ -> Did not exist in QB, and 1 or more of its parents did not exist in QB either, all added to QB
    ''' </returns>
    Public Function checkQBJobSubJobExist(ByRef Parent As String, ByRef TL_Name As String, ByVal TL_ID As Integer, ByVal UI As Boolean,
                                          ByVal p_token As String, Optional ByVal cancel_opt As Boolean = False) As Integer
        'Dim sessManager As QBSessionManager

        ' My.Forms.Main.History("Searching in QB for: " + QBJobSubJobName, "i")

        Try
            Dim numAdded As Integer = 0
            Dim TLJobSubJobName As String = If(TL_Name.Contains(":"), TL_Name, Parent + ":" + TL_Name)
            Dim jobOrTask As String = If(Parent.Split(":").Length = 1, "Job", "Task")

            'sessManager = New QBSessionManagerClass()
            Dim msgSetRq As IMsgSetRequest = MAIN.SESSMANAGER.CreateMsgSetRequest("US", 2, 0)

            msgSetRq.Attributes.OnError = ENRqOnError.roeContinue
            Dim TaskSubTaskQueryRq As ICustomerQuery = msgSetRq.AppendCustomerQueryRq

            TaskSubTaskQueryRq.ORCustomerListQuery.FullNameList.Add(TLJobSubJobName)

            'sessManager.OpenConnection("App", "TimeLive Quickbooks")
            'sessManager.BeginSession("", ENOpenMode.omDontCare)
            Dim msgSetRs As IMsgSetResponse = MAIN.SESSMANAGER.DoRequests(msgSetRq)
            Dim response As IResponse = msgSetRs.ResponseList.GetAt(0)
            Dim jobsubjobsRetList As ICustomerRetList
            jobsubjobsRetList = response.Detail

            Dim inQB As Boolean = Not jobsubjobsRetList Is Nothing

            Dim newMsgSetRq As IMsgSetRequest = MAIN.SESSMANAGER.CreateMsgSetRequest("US", 2, 0)
            newMsgSetRq.Attributes.OnError = ENRqOnError.roeContinue

            If TLJobSubJobName.IndexOf(":") > 0 Then ' Check first that customer name is not an empty string
                Dim create As Boolean = Not inQB
                If UI And Not inQB Then
                    Dim MsgBox_result
                    If cancel_opt Then
                        MsgBox_result = MsgBox("New " + jobOrTask + " found in TimeLive: " & vbCrLf & TLJobSubJobName & vbCrLf & "Create in QuickBooks?", MsgBoxStyle.YesNoCancel, "Warning!")
                        If MsgBox_result = MsgBoxResult.Cancel Then
                            Return -2
                        End If
                    Else
                        MsgBox_result = MsgBox("New " + jobOrTask + " found in TimeLive: " & vbCrLf & TLJobSubJobName & vbCrLf & "Create in QuickBooks?", MsgBoxStyle.YesNo, "Warning!")
                    End If

                    create = MsgBox_result = MsgBoxResult.Yes
                End If

                If create Then
                    Dim ParentArray() As String = Parent.Split(":")
                    If ParentArray.Length = 1 Then ' Check if customer of the project is in QB
                        Dim objClientServices As New Services.TimeLive.Clients.Clients
                        Dim authentication As New Services.TimeLive.Clients.SecuredWebServiceHeader
                        authentication.AuthenticatedToken = p_token
                        objClientServices.SecuredWebServiceHeaderValue = authentication
                        Dim client_TL_ID As Integer
                        Try
                            client_TL_ID = objClientServices.GetClientIdByName(Parent)
                        Catch ex As Exception
                            My.Forms.MAIN.History("The parent client : '" + Parent + "' of Project: '" + TLJobSubJobName + "' does not exist in TimeLive", "i")

                            Return -1
                        End Try
                        Dim syncCust As Sync_TLtoQB_Customer = New Sync_TLtoQB_Customer()
                        ' Note: Will note transfer over email address since we are just passing in new client instead of the actual client - can change this
                        numAdded += If(syncCust.checkQBCustomerExist(Parent, client_TL_ID, New Services.TimeLive.Clients.Client, UI), 0, 1)

                    ElseIf ParentArray.Length > 1 Then ' Check if parent of the task is in QB
                        Dim parent_name As String = ParentArray(ParentArray.Length - 1)
                        Dim parents_parent As String = Parent.Substring(0, Math.Max(Parent.LastIndexOf(":"), 0))
                        Dim parent_TL_ID As Integer = -1
                        If ParentArray.Length = 2 Then ' Parent is a project
                            Dim objProjectServices As New Services.TimeLive.Projects.Projects
                            Dim authentication As New Services.TimeLive.Projects.SecuredWebServiceHeader
                            authentication.AuthenticatedToken = p_token
                            objProjectServices.SecuredWebServiceHeaderValue = authentication

                            Try
                                parent_TL_ID = objProjectServices.GetProjectId(parent_name)
                            Catch ex As System.Web.Services.Protocols.SoapException
                                My.Forms.MAIN.History("Verify that '" + parent_name + "' in TimeLive has a code set:" + ex.ToString, "i")
                            End Try

                        Else ' Parent is a task
                            Dim objTaskServices As New Services.TimeLive.Tasks.Tasks
                            Dim authentication As New Services.TimeLive.Tasks.SecuredWebServiceHeader
                            authentication.AuthenticatedToken = p_token
                            objTaskServices.SecuredWebServiceHeaderValue = authentication

                            Try
                                parent_TL_ID = objTaskServices.GetTaskId(parent_name)
                            Catch ex As System.Web.Services.Protocols.SoapException
                                My.Forms.MAIN.History("Verify that '" + parent_name + "' in TimeLive has a code set:" + ex.ToString, "I")
                            End Try
                        End If

                        If Not parent_TL_ID = -1 Then
                            numAdded += checkQBJobSubJobExist(parents_parent, parent_name, parent_TL_ID, UI, p_token, cancel_opt)
                            If numAdded = -1 Then
                                Return -1
                            End If
                        Else
                            My.Forms.MAIN.History("Parent: '" + Parent + "'of Task: " + TLJobSubJobName + " is not in TimeLive.", "I")
                            Return -1 ' Exit Function
                        End If
                    End If


                    Dim jobAdd As ICustomerAdd = newMsgSetRq.AppendCustomerAddRq

                    ' TODO: 
                    ' Change client name to corresponding quickbooks 
                    Dim CustomerAdapter As New QB_TL_IDsTableAdapters.CustomersTableAdapter()
                    Dim QB_Customer As String = CustomerAdapter.GetQB_NameFromTL_Name(ParentArray(0))
                    QB_Customer = If(QB_Customer Is Nothing, ParentArray(0), QB_Customer.Trim)

                    Dim QB_Parent As String = If(Parent.Contains(":"), QB_Customer + Parent.Substring(Parent.IndexOf(":")), QB_Customer)

                    jobAdd.ParentRef.FullName.SetValue(QB_Parent)
                    jobAdd.Name.SetValue(TL_Name)

                    'step2: send the request
                    msgSetRs = MAIN.SESSMANAGER.DoRequests(newMsgSetRq)

                    ' Interpret the response
                    Dim res As IResponse
                    res = msgSetRs.ResponseList.GetAt(0)

                    If res.StatusSeverity = "Error" Then
                        Throw New Exception(res.StatusMessage)
                    End If

                    msgSetRq = MAIN.SESSMANAGER.CreateMsgSetRequest("US", 2, 0)
                    msgSetRq.Attributes.OnError = ENRqOnError.roeContinue

                    TaskSubTaskQueryRq = msgSetRq.AppendCustomerQueryRq


                    TaskSubTaskQueryRq.ORCustomerListQuery.FullNameList.Add(TLJobSubJobName)

                    msgSetRs = MAIN.SESSMANAGER.DoRequests(msgSetRq)
                    response = msgSetRs.ResponseList.GetAt(0)
                    jobsubjobsRetList = response.Detail
                    numAdded += 1

                    My.Forms.MAIN.History(jobOrTask + " in Timelive with name: " + TLJobSubJobName + " added to QuickBooks", "N")

                ElseIf inQB Then
                    Return 0
                Else ' Use -1 for when something was not created, but is still not in QB
                    Return -1
                End If

            End If

            'Assume only one return
            Dim JobSubJobsRet As ICustomerRet

            If jobsubjobsRetList IsNot Nothing Then
                JobSubJobsRet = jobsubjobsRetList.GetAt(0)

                With JobSubJobsRet
                    If inQB Then
                        My.Forms.MAIN.History("Found " + jobOrTask + " in QB with name: " + .Name.GetValue.ToString + " --> ID: " + .ListID.GetValue.ToString, "i")
                    End If
                    ' check if its in our database if not then add to it.
                    Dim JobSubJobAdapter As New QB_TL_IDsTableAdapters.Jobs_SubJobsTableAdapter()
                    'MsgBox("Hit:" + .ListID.GetValue + "-- " + TL_ID.ToString)

                    ' Add to table adapter
                    If ISQBID_In_JobSubJobDataTable(.Name.GetValue.ToString, .ListID.GetValue) = 0 Then
                        JobSubJobAdapter.Insert(.ListID.GetValue, TL_ID, .Name.GetValue, TLJobSubJobName) 'QBJobSubJobName
                        My.Forms.MAIN.History("Added '" + TLJobSubJobName + "' With the TL_ID " + TL_ID.ToString + " to local database", "i")
                    End If
                End With
            End If

            Return numAdded

        Catch ex As Exception
            Throw ex
            'Finally
            '    If Not sessManager Is Nothing Then
            '       sessManager.EndSession()
            '       sessManager.CloseConnection()
            '    End If
        End Try
    End Function

    ''' <summary>
    ''' Check if QB ID is in job/subjob data table
    ''' </summary>
    ''' <param name="myqbName"></param>
    ''' <param name="myqbID"></param>
    ''' <returns>
    ''' 0 -> not in data table
    ''' 1 -> one record in data table
    ''' 2 -> more than one record in data table
    ''' </returns>
    Private Function ISQBID_In_JobSubJobDataTable(ByVal myqbName As String, ByVal myqbID As String) As Int16
        Dim result As Int16 = 0

        Dim JobSubJobsAdapter As New QB_TL_IDsTableAdapters.Jobs_SubJobsTableAdapter
        Dim TimeLiveIDs As QB_TL_IDs.Jobs_SubJobsDataTable = JobSubJobsAdapter.GetCorrespondingTL_ID(myqbID)

        If TimeLiveIDs.Count = 1 Then
            result = 1
            My.Forms.MAIN.History("One record found in local database for:  " + myqbName, "i")
        End If

        If TimeLiveIDs.Count = 0 Then
            result = 0
            My.Forms.MAIN.History("No records found in local database for:" + myqbName, "i")
        End If

        If TimeLiveIDs.Count > 1 Then
            result = 2
            My.Forms.MAIN.History("More than one record found in local database for:" + myqbName, "I")
        End If

        Return result
    End Function

End Class