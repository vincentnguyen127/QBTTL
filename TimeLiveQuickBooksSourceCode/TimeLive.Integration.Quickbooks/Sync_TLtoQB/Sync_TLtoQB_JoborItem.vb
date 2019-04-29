Imports QBFC13Lib

Public Class Sync_TLtoQB_JoborItem
    '---------------------Sync Job SubJobs TL Data to QB---------------------------------------
    ''' <summary>
    ''' Sync the Jobs data from QB. Print out Projects and Tasks that are in TL but not QB
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
                result = checkQBJobSubJobExist(objProject.ClientName, objProject.ProjectName, ExpectedQBProjectName, True)
                If result = False Then
                    'Does not exist in QB
                    My.Forms.MAIN.History("Please update or enter project in QB --> Name: " + objProject.ProjectName.ToString + vbTab + "ID: " + objProject.ProjectID.ToString(), "I")
                Else
                    ExpectedQBTaskName = ExpectedQBProjectName
                    My.Forms.MAIN.History("Getting tasks related to project: " & objProject.ProjectName.ToString, "i")
                    SubJobsOrSubData = GetTasks(objProject.ProjectName.ToString, p_token)


                    For Each element As QBtoTL_JobOrItem.Job_Item In SubJobsOrSubData.DataArray
                        result = checkQBJobSubJobExist(ExpectedQBTaskName, element.TL_Name, element.TL_ID, True)
                        If result = False Then
                            'Does not exist in QB
                            My.Forms.MAIN.History("Please update or enter task in QB: " + ExpectedQBTaskName + ":" + element.TL_Name.ToString + " manually", "I")
                        End If
                    Next

                End If
                Exit Sub
            Next

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    ''' <summary>
    ''' Sync the Jobs from QB. Print out Projects and Tasks in TL but not QB
    ''' </summary>
    ''' <param name="p_token"></param>
    Function SyncJobsSubJobData(ByVal p_token As String, Optional IntegratedUIForm As IntegratedUI = Nothing,
                                Optional ByVal UI As Boolean = True, Optional ByVal nameList As List(Of String) = Nothing)
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
            If Not IntegratedUIForm Is Nothing Then IntegratedUIForm.ProgressBar1.Maximum = nameList.Count

            ' Iterate through all projects
            For n As Integer = 0 To objProjectArray.Length - 1
                objProject = objProjectArray(n)
                ' Done this way since field .ProjectID just returned 0
                Dim projectID As Integer = objProjectServices.GetProjectId(objProject.ProjectName)
                Dim full_name As String = objProject.ClientName + ":" + objProject.ProjectName
                Dim create As Boolean = If(nameList Is Nothing, True, nameList.Contains(full_name))

                If create Then
                    numSynced += If(checkQBJobSubJobExist(objProject.ClientName, objProject.ProjectName, projectID.ToString, UI), 0, 1)
                End If

                If Not IntegratedUIForm Is Nothing And create Then IntegratedUIForm.ProgressBar1.Value += 1
            Next

            ' Iterate through all tasks
            For n As Integer = 0 To objTaskArray.Length - 1
                objTask = objTaskArray(n)
                ' Done this way since field .taskID just returned 0
                Dim taskID As Integer = objTaskServices.GetTaskId(objTask.TaskName)
                Dim full_name As String = objTask.JobParent + ":" + objTask.TaskName
                Dim create As Boolean = If(nameList Is Nothing, True, nameList.Contains(full_name))
                If create Then
                    numSynced += If(checkQBJobSubJobExist(objTask.JobParent, objTask.TaskName, taskID.ToString, UI), 0, 1)
                End If

                If Not IntegratedUIForm Is Nothing And create Then IntegratedUIForm.ProgressBar1.Value += 1
            Next
        Catch ex As System.Web.Services.Protocols.SoapException
            If UI Then
                MsgBox("Make sure all Tasks and Jobs have a Code: " + ex.Message)
            Else
                Throw New Exception("Make sure all Tasks and Jobs have a Code: " + ex.Message)
            End If
        Catch ex As Exception
            If UI Then
                MsgBox(ex.Message)
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
            MsgBox(ex.Message)
        End Try
        Return SubJobsOrSubData
    End Function


    ''' <summary>
    ''' Verifies a Job exists in QB, adding it to the Data Table if necessary
    ''' </summary>
    ''' <param name="TL_Name"></param>
    ''' <param name="TL_ID"></param>
    ''' <returns>
    ''' False: does not exist in QB
    ''' True: does exist in QB, and we add it to the Data Table if not present
    ''' </returns>
    Public Function checkQBJobSubJobExist(ByRef Parent As String, ByRef TL_Name As String, ByVal TL_ID As Integer, ByVal UI As Boolean) As Boolean
        'Dim sessManager As QBSessionManager

        ' My.Forms.MAIN.History("Searching in QB for: " + QBJobSubJobName, "i")

        Try
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

            If Not inQB Then
                Dim newMsgSetRq As IMsgSetRequest = MAIN.SESSMANAGER.CreateMsgSetRequest("US", 2, 0)
                newMsgSetRq.Attributes.OnError = ENRqOnError.roeContinue

                Dim create As Boolean = True
                If UI Then
                    create = MsgBox("New " + jobOrTask + " found in TimeLive: " + TLJobSubJobName + ". Create in QuickBooks?", MsgBoxStyle.YesNo, "Warning!") = MsgBoxResult.Yes
                End If

                If create Then
                    ' Add TL Job to QB
                    Dim jobAdd As ICustomerAdd = newMsgSetRq.AppendCustomerAddRq

                    jobAdd.ParentRef.FullName.SetValue(Parent)
                    jobAdd.Name.SetValue(TL_Name)

                    'step2: send the request
                    msgSetRs = MAIN.SESSMANAGER.DoRequests(newMsgSetRq)

                    ' Interpret the response
                    Dim res As IResponse
                    res = msgSetRs.ResponseList.GetAt(0)

                    If res.StatusSeverity = "Error" Then
                        Throw New Exception(res.StatusMessage)
                    End If

                    My.Forms.MAIN.History(jobOrTask + " in Timelive with name: " + TLJobSubJobName + " added to QuickBooks", "N")

                    msgSetRq = MAIN.SESSMANAGER.CreateMsgSetRequest("US", 2, 0)
                    msgSetRq.Attributes.OnError = ENRqOnError.roeContinue

                    TaskSubTaskQueryRq = msgSetRq.AppendCustomerQueryRq
                    TaskSubTaskQueryRq.ORCustomerListQuery.FullNameList.Add(TLJobSubJobName)

                    msgSetRs = MAIN.SESSMANAGER.DoRequests(msgSetRq)
                    response = msgSetRs.ResponseList.GetAt(0)
                    jobsubjobsRetList = response.Detail
                Else
                    Return False
                End If
            End If
            'Assume only one return
            Dim JobSubJobsRet As ICustomerRet
            JobSubJobsRet = jobsubjobsRetList.GetAt(0)

            With JobSubJobsRet
                If inQB Then
                    My.Forms.MAIN.History("Found " + jobOrTask + " in QB with name: " + .Name.GetValue.ToString + " --> ID: " + .ListID.GetValue.ToString, "i")
                End If
                ' check if its in our database if not then add to it.
                Dim JobSubJobAdapter As New QB_TL_IDsTableAdapters.Jobs_SubJobsTableAdapter()
                'MsgBox("Hit:" + .ListID.GetValue + "-- " + TL_ID.ToString)

                ' Add to table adapter
                If ISQBID_In_JobSubJobDataTable(.Name.GetValue.ToString, .ListID.GetValue) = 0 And
                (Not UI Or MsgBox("Job in TL and QB: " + TLJobSubJobName + ". Insert into Table Adapter?", MsgBoxStyle.YesNo, "Warning!") = MsgBoxResult.Yes) Then
                    My.Forms.MAIN.History("Adding " + TLJobSubJobName + " With the TL_ID " + TL_ID.ToString + " to data sync table", "i")
                    JobSubJobAdapter.Insert(.ListID.GetValue, TL_ID, .Name.GetValue, TLJobSubJobName) 'QBJobSubJobName
                End If
            End With

            Return inQB

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
            My.Forms.MAIN.History("One record found in QB sync table for:  " + myqbName, "i")
        End If

        If TimeLiveIDs.Count = 0 Then
            result = 0
            My.Forms.MAIN.History("No records found on QB sync table for:" + myqbName, "i")
        End If

        If TimeLiveIDs.Count > 1 Then
            result = 2
            My.Forms.MAIN.History("More than one record found for:" + myqbName, "I")
        End If

        Return result
    End Function

End Class