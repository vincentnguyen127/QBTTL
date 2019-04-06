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
                result = checkQBJobSubJobExist(ExpectedQBProjectName, objProject.ProjectID, ExpectedQBProjectName)
                If result = False Then
                    'Does not exist in QB
                    My.Forms.MAIN.History("Please update or enter project in QB --> Name: " + objProject.ProjectName.ToString + vbTab + "ID: " + objProject.ProjectID.ToString(), "I")
                Else
                    ExpectedQBTaskName = ExpectedQBProjectName
                    My.Forms.MAIN.History("Getting tasks related to project: " & objProject.ProjectName.ToString, "i")
                    SubJobsOrSubData = GetTasks(objProject.ProjectName.ToString, p_token)


                    For Each element As QBtoTL_JobOrItem.Job_Item In SubJobsOrSubData.DataArray
                        result = checkQBJobSubJobExist(ExpectedQBTaskName + ":" + element.TL_Name, element.TL_ID, ExpectedQBTaskName + ":" + element.TL_Name)
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
    Sub SyncJobsSubJobData(ByVal p_token As String)
        Dim result As Boolean = False
        Dim SubJobsOrSubData As New QBtoTL_JobOrItem.SubJobsOrSubitems

        My.Forms.MAIN.History("Syncing JobSubJob Data", "n")
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
                    ' Done this way since field .taskID just returned 0
                    Dim taskID As Integer = objTaskServices.GetTaskId(.TaskName)
                    If .TaskID = 0 Then
                        My.Forms.MAIN.History(.TaskName + " id: " + taskID.ToString, "i")
                    End If
                    result = checkQBJobSubJobExist(.JobParent.ToString + ":" + .TaskName.ToString, taskID.ToString, .JobParent.ToString + ":" + .TaskName.ToString)

                    If result = False Then
                        'Does not exist in QB
                        My.Forms.MAIN.History("Update or enter task in QB: " + .JobParent.ToString + ":" + .TaskName.ToString + " manually", "I")
                    End If
                End With
            Next


        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

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
    Public Function checkQBJobSubJobExist(ByRef QBJobSubJobName As String, ByVal TL_ID As Integer, ByVal TL_Name As String) As Boolean
        'Dim sessManager As QBSessionManager

        My.Forms.MAIN.History("Serching in QB for: " + QBJobSubJobName, "i")

        Try
            'sessManager = New QBSessionManagerClass()
            Dim msgSetRq As IMsgSetRequest = MAIN.SESSMANAGER.CreateMsgSetRequest("US", 2, 0)

            msgSetRq.Attributes.OnError = ENRqOnError.roeContinue
            Dim TaskSubTaskQueryRq As ICustomerQuery = msgSetRq.AppendCustomerQueryRq

            TaskSubTaskQueryRq.ORCustomerListQuery.FullNameList.Add(QBJobSubJobName)

            'sessManager.OpenConnection("App", "TimeLive Quickbooks")
            'sessManager.BeginSession("", ENOpenMode.omDontCare)
            Dim msgSetRs As IMsgSetResponse = MAIN.SESSMANAGER.DoRequests(msgSetRq)
            Dim response As IResponse = msgSetRs.ResponseList.GetAt(0)
            Dim jobsubjobsRetList As ICustomerRetList
            jobsubjobsRetList = response.Detail

            If jobsubjobsRetList Is Nothing Then
                My.Forms.MAIN.History("Job not found", "i")
                Return False
            Else
                'Assume only one return
                Dim JobSubJobsRet As ICustomerRet
                JobSubJobsRet = jobsubjobsRetList.GetAt(0)

                With JobSubJobsRet
                    My.Forms.MAIN.History("Found job/subjob name in QB: " + .Name.GetValue.ToString + " --> ID: " + .ListID.GetValue.ToString, "i")
                    ' check if its in our database if not then add to it.
                    Dim JobSubJobAdapter As New QB_TL_IDsTableAdapters.Jobs_SubJobsTableAdapter()
                    'MsgBox("Hit:" + .ListID.GetValue + "-- " + TL_ID.ToString)

                    If ISQBID_In_JobSubJobDataTable(.Name.GetValue.ToString, .ListID.GetValue) = 0 Then
                        My.Forms.MAIN.History("Not in database, Adding to Sync Database: " + JobSubJobAdapter.GetCorrespondingTL_ID(TL_ID).ToString + "QB_ID:  " + .ListID.GetValue + " With the TL_ID: " + TL_ID.ToString, "i")
                        JobSubJobAdapter.Insert(.ListID.GetValue, TL_ID, .Name.GetValue, TL_Name) 'QBJobSubJobName
                    End If
                End With

                Return True
            End If

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
            My.Forms.MAIN.History("One record found in QB sync table for: " + myqbName, "i")
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