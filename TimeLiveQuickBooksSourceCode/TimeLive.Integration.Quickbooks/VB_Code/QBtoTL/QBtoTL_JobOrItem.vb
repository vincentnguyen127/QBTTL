'Imports QBFC11Lib
Imports QBFC13Lib

Public Class QBtoTL_JobOrItem

    ' datastructure for jobs and items
    Public Class JobDataStructureQB
        Public NoItems As Integer = 0
        Public DataArray As New List(Of Job_Subjob)
    End Class

    Public Class Job_Subjob
        Public RecSelect As Boolean
        Public QB_Name As String
        Public NewlyAdded As String
        Public QB_ID As String
        Public QBModTime As String
        Public QBCreateTime As String
        Public Email As String
        Public Telephone1 As String
        Public Fax As String
        Public parent As String
        Public FullName As String
        Public subParentInt As Integer
        Public Enabled As Boolean
        Sub New(ByVal NewlyAdded As String, ByVal QB_Name As String, ByVal Email As String, ByVal QB_ID As String, ByVal Telephone1 As String, ByVal Fax As String,
                ByVal QBModTime As String, ByVal QBCreateTime As String, ByVal Is_parent As String, ByVal FullName As String, ByVal subParentInt As Integer, ByVal Enabled As Boolean)
            Me.RecSelect = False
            Me.QB_Name = QB_Name
            Me.NewlyAdded = NewlyAdded
            Me.QB_ID = QB_ID
            Me.QBModTime = QBModTime
            Me.QBCreateTime = QBCreateTime
            Me.Email = Email
            Me.Telephone1 = Telephone1
            Me.Fax = Fax
            Me.parent = Is_parent
            Me.FullName = FullName
            Me.subParentInt = subParentInt
            Me.Enabled = Enabled
        End Sub
    End Class

    Public Class SubJobsOrSubitems
        Public NoItems As Integer = 0
        Public DataArray As New List(Of Job_Item)
    End Class

    Public Class Job_Item
        Public TL_Name As String
        Public TL_ID As String
        Sub New(ByVal TL_Name_in As String, ByVal TL_ID_in As String)
            TL_Name = TL_Name_in
            TL_ID = TL_ID_in
        End Sub
    End Class

    '---------------------------------------Get Job Data ---------------------------------
    Public Function GetJobSubJobData() As JobDataStructureQB
        Dim EmailAddress As String
        Dim Telephone1 As String
        Dim Fax As String
        Dim ModTime As String
        Dim CreateTime As String
        Dim Job_subJobData As New JobDataStructureQB
        Dim NewlyAdd As String

        'step1: prepare the request
        Dim msgSetRs As IMsgSetResponse
        Try
            Dim msgSetRq As IMsgSetRequest = MAIN.SESSMANAGER.CreateMsgSetRequest("US", 2, 0) 'sessManager
            msgSetRq.Attributes.OnError = ENRqOnError.roeContinue
            ' Customer Query 
            Dim synccust As ICustomerQuery = msgSetRq.AppendCustomerQueryRq

            synccust.ORCustomerListQuery.CustomerListFilter.ActiveStatus.SetValue(ENActiveStatus.asActiveOnly)

            'step2: send the request
            msgSetRs = MAIN.SESSMANAGER.DoRequests(msgSetRq) 'sessManager
            Dim respList As IResponseList
            respList = msgSetRs.ResponseList
            If respList Is Nothing Then
                Return Nothing
            End If
            ' Should only expect 1 response
            Dim resp As IResponse
            resp = respList.GetAt(0)
            If (resp.StatusCode = 0) Then
                Dim ptRetList As ICustomerRetList
                ptRetList = resp.Detail

                Dim pblength As Integer = If(ptRetList Is Nothing, 0, ptRetList.Count)


                ' Should only be 1 CustomerRet object returned
                Dim ptRet As ICustomerRet
                For i As Integer = 0 To pblength - 1

                    ptRet = ptRetList.GetAt(i)
                    With ptRet
                        If Not .ParentRef Is Nothing Then
                            Dim PTArray() As String = Split(.FullName.GetValue, ":")
                            If PTArray.Length >= 2 Then
                                Dim name = .Name.GetValue
                                Dim full_name = .FullName.GetValue
                                EmailAddress = If(.Email Is Nothing, "", .Email.GetValue)
                                Telephone1 = If(.Phone Is Nothing, "", .Phone.GetValue)
                                Fax = If(.Fax Is Nothing, "", .Fax.GetValue)
                                CreateTime = If(.TimeCreated Is Nothing, "", .TimeCreated.GetValue.ToString)
                                ModTime = If(.TimeModified Is Nothing, CreateTime, .TimeModified.GetValue.ToString)

                                Dim TL_ID_Count = ISQBID_In_DataTableForJobs(.FullName.GetValue, .ListID.GetValue)

                                NewlyAdd = If(TL_ID_Count, "", "N") ' N if new

                                ' will check which type data should be added 
                                Job_subJobData.NoItems += 1
                                Job_subJobData.DataArray.Add(New Job_Subjob(NewlyAdd, .Name.GetValue, EmailAddress, .ListID.GetValue, Telephone1, Fax, ModTime,
                                                                            CreateTime, PTArray(0).ToString, .FullName.GetValue, 0, .IsActive.GetValue))
                            End If
                        End If
                    End With

                Next
            End If
            If msgSetRs.ResponseList.GetAt(0).StatusSeverity = "Error" Then
                My.Forms.MAIN.History(msgSetRs.ResponseList.GetAt(0).StatusMessage, "C")
                Throw New Exception(msgSetRs.ResponseList.GetAt(0).StatusMessage)
            End If
        Catch ex As Exception
            My.Forms.MAIN.History(ex.ToString, "C")
            'MAIN.QUITQBSESSION()
            Throw ex
        End Try
        Return Job_subJobData
    End Function
    Public Function GetJobSubJobData(MainForm As MAIN, p_token As String, UI As Boolean) As JobDataStructureQB
        Dim EmailAddress As String
        Dim Telephone1 As String
        Dim Fax As String
        Dim ModTime As String
        Dim CreateTime As String
        Dim Job_subJobData As New JobDataStructureQB
        Dim NewlyAdd As String

        'step1: prepare the request
        Dim msgSetRs As IMsgSetResponse
        Try
            Dim msgSetRq As IMsgSetRequest = MAIN.SESSMANAGER.CreateMsgSetRequest("US", 2, 0) 'sessManager
            msgSetRq.Attributes.OnError = ENRqOnError.roeContinue
            ' Customer Query 
            Dim synccust As ICustomerQuery = msgSetRq.AppendCustomerQueryRq

            synccust.ORCustomerListQuery.CustomerListFilter.ActiveStatus.SetValue(ENActiveStatus.asActiveOnly)

            'step2: send the request
            msgSetRs = MAIN.SESSMANAGER.DoRequests(msgSetRq) 'sessManager
            Dim respList As IResponseList
            respList = msgSetRs.ResponseList
            If respList Is Nothing Then
                Return Nothing
            End If
            ' Should only expect 1 response
            Dim resp As IResponse
            resp = respList.GetAt(0)
            If (resp.StatusCode = 0) Then
                Dim ptRetList As ICustomerRetList
                ptRetList = resp.Detail

                Dim pblength As Integer = If(ptRetList Is Nothing, 0, ptRetList.Count)

                If UI Then
                    My.Forms.MAIN.ProgressBar1.Maximum += pblength
                End If

                ' Should only be 1 CustomerRet object returned
                Dim ptRet As ICustomerRet
                For i As Integer = 0 To pblength - 1

                    ptRet = ptRetList.GetAt(i)
                    With ptRet
                        If Not .ParentRef Is Nothing Then
                            Dim PTArray() As String = Split(.FullName.GetValue, ":")
                            If PTArray.Length >= 2 Then
                                Dim name = .Name.GetValue
                                Dim full_name = .FullName.GetValue
                                EmailAddress = If(.Email Is Nothing, "", .Email.GetValue)
                                Telephone1 = If(.Phone Is Nothing, "", .Phone.GetValue)
                                Fax = If(.Fax Is Nothing, "", .Fax.GetValue)
                                CreateTime = If(.TimeCreated Is Nothing, "", .TimeCreated.GetValue.ToString)
                                ModTime = If(.TimeModified Is Nothing, CreateTime, .TimeModified.GetValue.ToString)

                                Dim TL_ID_Count = ISQBID_In_DataTableForJobs(.FullName.GetValue, .ListID.GetValue)

                                NewlyAdd = If(TL_ID_Count, "", "N") ' N if new

                                ' will check which type data should be added 
                                Job_subJobData.NoItems += 1
                                Job_subJobData.DataArray.Add(New Job_Subjob(NewlyAdd, .Name.GetValue, EmailAddress, .ListID.GetValue, Telephone1, Fax, ModTime,
                                                                            CreateTime, PTArray(0).ToString, .FullName.GetValue, 0, .IsActive.GetValue))
                            End If
                        End If
                    End With
                    If UI Then
                        My.Forms.MAIN.ProgressBar1.Value += 1
                    End If
                Next
            End If
            If msgSetRs.ResponseList.GetAt(0).StatusSeverity = "Error" Then
                My.Forms.MAIN.History(msgSetRs.ResponseList.GetAt(0).StatusMessage, "C")
                Throw New Exception(msgSetRs.ResponseList.GetAt(0).StatusMessage)
            End If
        Catch ex As Exception
            My.Forms.MAIN.History(ex.ToString, "C")
            'MAIN.QUITQBSESSION()
            Throw ex
        End Try
        Return Job_subJobData
    End Function

    '---------------------------------------Get Item Data -----------------------------------------------------------------'
    Public Function GetItemSubItemData(MainForm As MAIN, p_token As String, UI As Boolean) As JobDataStructureQB
        Dim ModTime As String
        Dim CreateTime As String
        Dim ItemData As New JobDataStructureQB
        Dim NewlyAdd As String

        'step1: create QBFC session manager and prepare the request
        Dim msgSetRs As IMsgSetResponse
        Try
            Dim msgSetRq As IMsgSetRequest = MAIN.SESSMANAGER.CreateMsgSetRequest("US", 2, 0) 'sessManager
            msgSetRq.Attributes.OnError = ENRqOnError.roeContinue
            Dim synccust As IItemServiceQuery = msgSetRq.AppendItemServiceQueryRq
            synccust.ORListQuery.ListFilter.ActiveStatus.SetValue(ENActiveStatus.asActiveOnly)

            'step2: begin QB session and send the request
            msgSetRs = MAIN.SESSMANAGER.DoRequests(msgSetRq)
            Dim respList As IResponseList
            respList = msgSetRs.ResponseList
            If (respList Is Nothing) Then
                Return Nothing
            End If

            'sets status bar, If no, UI skip
            Dim pblenth As Integer = respList.Count

            If UI Then
                My.Forms.MAIN.ProgressBar1.Maximum += pblenth
            End If

            ' Should only expect 1 response
            Dim resp As IResponse
            resp = respList.GetAt(0)
            If resp.StatusCode = 0 Then
                Dim ptRetList As IItemServiceRetList
                ptRetList = resp.Detail
                ' Should only be 1 CustomerRet object returned

                Dim ptRet As IItemServiceRet

                For i As Integer = 0 To pblenth - 1
                    ' Code below should be altered similarly to above
                    ptRet = ptRetList.GetAt(i)
                    With ptRet
                        CreateTime = If(.TimeCreated Is Nothing, "", .TimeCreated.GetValue.ToString)
                        ModTime = If(.TimeModified Is Nothing, CreateTime, .TimeModified.GetValue.ToString)

                        Dim name As String = If(.ParentRef Is Nothing, .FullName.GetValue, .Name.GetValue)
                        Dim TL_ID_Count = ISQBID_In_DataTableForItems(name, .ListID.GetValue)
                        NewlyAdd = If(TL_ID_Count, "", "N") ' N if new
                        ItemData.NoItems += 1
                        ItemData.DataArray.Add(New Job_Subjob(NewlyAdd, name, "", .ListID.GetValue, "", "", ModTime,
                                                              CreateTime, "", .FullName.GetValue, .Sublevel.GetValue, .IsActive.GetValue))
                    End With
                Next
            End If
            If msgSetRs.ResponseList.GetAt(0).StatusSeverity = "Error" Then
                My.Forms.MAIN.History(msgSetRs.ResponseList.GetAt(0).StatusMessage, "C")
                Throw New Exception(msgSetRs.ResponseList.GetAt(0).StatusMessage)
            End If
        Catch ex As Exception
            My.Forms.MAIN.History(ex.ToString, "C")
        End Try
        Return ItemData
    End Function

    '---------------------------------------Transfer Job Data -----------------------------------------------------------------
    Public Function QBTransferJobstoTL(ByRef objData As QBtoTL_JobOrItem.JobDataStructureQB,
                                   ByVal p_token As String, MainForm As MAIN, UI As Boolean) As Integer

        Dim objProjectServices As Services.TimeLive.Projects.Projects = MAIN.connect_TL_projects(p_token)
        Dim objTaskServices As Services.TimeLive.Tasks.Tasks = MAIN.connect_TL_tasks(p_token)
        Dim objClientServices As Services.TimeLive.Clients.Clients = MAIN.connect_TL_clients(p_token)

        Dim objServices As New Services.TimeLiveServices
        Dim authentication As New Services.SecuredWebServiceHeader
        authentication.AuthenticatedToken = p_token
        objServices.SecuredWebServiceHeaderValue = authentication

        'sets status bar. If no, UI skip
        If UI Then
            Dim pblenth As Integer = objData.DataArray.Count
            'If pblenth >= 0 Then
            My.Forms.MAIN.ProgressBar1.Maximum = pblenth
            My.Forms.MAIN.ProgressBar1.Value = 0
            'End If
        End If

        Dim NoRecordsCreatedorUpdated = 0
        ' open session  for TL
        Dim nProjectTypeId As Integer = objProjectServices.GetProjectTypeId()
        Dim nProjectBillingTypeId As Integer = objProjectServices.GetProjectBillingTypeId()
        Dim nProjectStatusId As Integer = objProjectServices.GetProjectStatusId()
        Dim nTeamLeadId As Integer = objProjectServices.GetTeamLeadId()
        Dim nProjectManagerId As Integer = objProjectServices.GetProjectManagerId()
        Dim nProjectBillingRateTypeId As Integer = objProjectServices.GetProjectBillingRateTypeId()
        'Dim val As Integer = objProjectServices.GetAccountEmployeeId


        For Each element As QBtoTL_JobOrItem.Job_Subjob In objData.DataArray
            'Try
            ' check if the check value is true
            If element.RecSelect Then
                'Dim PTArray() As String = Split(element.p, ":")
                If Not element.parent Is Nothing Then
                    My.Forms.MAIN.History("Processing:  " + element.QB_Name, "n")

                    Dim DT_has_QBID = ISQBID_In_DataTableForJobs(element.QB_Name, element.QB_ID)
                    'if none create
                    'If DT_has_QBID = 0 Then
                    Dim create As Boolean = True
                    If UI And Not CBool(DT_has_QBID) Then
                        'create = MsgBox("New job or subjob found: " + element.FullName.Replace(":", MAIN.colonReplacer) + ". Create?", MsgBoxStyle.YesNo, "Warning!") = MsgBoxResult.Yes
                    End If
                    If create Then
                        ' If QB_ID is in the DB, check if TL ID is too
                        If DT_has_QBID Then
                            Dim TL_ID As String = ISTLID_In_DataTableForJobs(element.QB_ID)
                            If TL_ID Is Nothing Then
                                My.Forms.MAIN.History("Detected empty sync record (No TL ID). Needs to be manually sync or deleted." + element.QB_Name, "i")
                            End If
                            Dim project_or_task_inTL As Boolean =
                                    Array.Exists(objTaskServices.GetTasks, Function(e As Services.TimeLive.Tasks.Task) e.TaskName = element.QB_Name) Or
                                    Array.Exists(objProjectServices.GetProjects, Function(e As Services.TimeLive.Projects.Project) e.ProjectName = element.QB_Name)
                            If project_or_task_inTL Then
                                ' TL already has this value and so does our DB, so just move to next element after updating Progress Bar
                                If UI Then
                                    My.Forms.MAIN.ProgressBar1.Value += 1
                                End If
                                ' TODO: Update TL
                                ' NoRecordsCreatedorUpdated += 1
                                Continue For
                            End If
                        End If

                        Dim PTArray() As String = Split(element.FullName, ":")
                        Dim nClientId As Integer = 0

                        ' Set client ID if the client exists, otherwise print an error message and continue the for loop
                        Dim CustomerAdapter As New QB_TL_IDsTableAdapters.CustomersTableAdapter()
                        Dim TL_Client As String = CustomerAdapter.GetTL_NameFromQB_Name(PTArray(0))
                        TL_Client = If(TL_Client Is Nothing, PTArray(0), TL_Client.Trim)
                        Dim QB_Parent As String = TL_Client + element.FullName.Substring(element.FullName.IndexOf(":"))

                        If Array.Exists(objClientServices.GetClients, Function(clnt As Services.TimeLive.Clients.Client) clnt.ClientName = TL_Client) Then
                            nClientId = objClientServices.GetClientIdByName(TL_Client) ' May want to switch this to PTArray(0)
                        Else
                            My.Forms.MAIN.History("Could not Get Client ID. It is likely that the client associated with this project does not exist. Try adding the client '" +
                                                          PTArray(0) + "' and then try again", "i")
                            If UI Then
                                My.Forms.MAIN.ProgressBar1.Value += 1
                            End If
                            Continue For
                        End If

                        NoRecordsCreatedorUpdated += 1
                        If PTArray.Length = 2 Then ' Job
                            Try
                                objProjectServices.InsertProject(nProjectTypeId, nClientId, 0, 0, nProjectBillingTypeId, element.QB_Name,
                                                                 element.FullName, Now.Date, Now.AddMonths(1).Date, nProjectStatusId, nTeamLeadId,
                                                                 nProjectManagerId, 0, 0, 1, "Months", element.QB_Name, 0, nProjectBillingRateTypeId,
                                                                 False, True, 0, Now.Date, nTeamLeadId, Now.Date, nTeamLeadId, False)

                            Catch ex As System.Web.Services.Protocols.SoapException
                                ' Do Nothing
                            Catch ex As Exception
                                Throw ex
                            End Try

                            My.Forms.MAIN.History("Successfully inserted project " + element.QB_Name, "i")
                            'If Not Array.Exists(objClientServices.GetClients, Function(clnt As Services.TimeLive.Clients.Client) clnt.ClientName = PTArray(0)) Then
                            '   NoRecordsCreatedorUpdated -= 1
                            '   My.Forms.Main.History("Could Not Get Client ID. It Is likely that the client associated With this project does Not exist. Try adding the client " +
                            '   element.parent + " And Then Try again", "i")
                            '   Continue For
                            'End If

                            'Dim JobAdapter As New QB_TL_IDsTableAdapters.Jobs_SubJobsTableAdapter
                            'JobAdapter.Insert(element.QB_ID, objProjectServices.GetProjectId(element.QB_Name), element.QB_Name, element.QB_Name)

                            ' SubJob
                        ElseIf PTArray.Length = 3 Then ' Subjob
                            Dim hasParentProject As Boolean = Array.Exists(objProjectServices.GetProjects,
                                                                Function(proj As Services.TimeLive.Projects.Project) proj.ClientName + ":" + proj.ProjectName = TL_Client + ":" + PTArray(1))
                            If Not hasParentProject Then
                                ' Currently decrement because we do not add the task
                                ' TODO: Add Project then the task, which would mean we would then increment this value (ie we have added 2 instead of 0)
                                Try
                                    objProjectServices.InsertProject(nProjectTypeId, nClientId, 0, 0, nProjectBillingTypeId, PTArray(1), PTArray(0) + ":" + PTArray(1),
                                                                     Now.Date, Now.AddMonths(1).Date, nProjectStatusId, nTeamLeadId, nProjectManagerId,
                                                                     0, 0, 1, "Months", PTArray(1), 0, nProjectBillingRateTypeId,
                                                                     False, True, 0, Now.Date, nTeamLeadId, Now.Date, nTeamLeadId, False)
                                Catch ex As System.Web.Services.Protocols.SoapException
                                    ' Do nothing
                                End Try
                                nProjectTypeId = objProjectServices.GetProjectTypeId() ' Not sure if this is needed, to update project id or something
                                NoRecordsCreatedorUpdated += 1
                                My.Forms.MAIN.History("Successfully inserted QuickBooks project '" + PTArray(0) + ":" + PTArray(1) + "' as '" + TL_Client + ":" + PTArray(1) + "' in TimeLive", "i")
                                'My.Forms.Main.History(ex.ToString, "C")
                                'Continue For
                            End If

                            Dim nProjectId As Integer = objProjectServices.GetProjectId(PTArray(1)) ' Was ProjectName, which was Nothing 

                            Dim nTaskTypeId As Integer = objTaskServices.GetTaskTypeId()
                            Dim nTaskStatusId As Integer = objTaskServices.GetTaskStatusId()
                            Dim nPriorityId As Integer = objTaskServices.GetTaskPriorityId()
                            Dim nProjectMilestoneId As Integer = objProjectServices.GetProjectMilestoneIdByProjectId(nProjectId)
                            Dim nCurrencyId As Integer = objServices.GetCurrencyId()
                            Dim t As New Services.TimeLive.Tasks.Task
                            Dim iPar As String = element.FullName.Substring(element.FullName.IndexOf(":") + 1, element.FullName.Length - element.parent.Length - element.QB_Name.Length - 2)

                            objTaskServices.InsertTask(nProjectId, 0, element.QB_Name, element.QB_Name,
                                                       nTaskTypeId, 1, "Months", 0, False, Now.AddMonths(1).Date, nTaskStatusId, nPriorityId,
                                                       nProjectMilestoneId, False, False, Now.Date, nTeamLeadId, Now.Date, nTeamLeadId, 0,
                                                       0, "Days", True, element.QB_Name, 0, False, nCurrencyId)

                            For Each task As Services.TimeLive.Tasks.Task In objTaskServices.GetTasks
                                Dim v = task
                                If task.TaskName = element.QB_Name Then
                                    My.Forms.MAIN.History("Added this", "i")
                                End If
                            Next

                            My.Forms.MAIN.History("Successfully inserted task " + element.QB_Name, "i")

                        Else ' Subjob of a subjob

                            Dim hasParentTask As Boolean = Array.Exists(objTaskServices.GetTasks,
                                                                Function(proj As Services.TimeLive.Tasks.Task) proj.TaskName = PTArray(PTArray.Length - 2))

                            If Not hasParentTask Then
                                My.Forms.MAIN.History("Subtask " + element.QB_Name + "tried to be added before task " + PTArray(PTArray.Length - 2), "N")
                                Continue For
                            End If

                            Dim nParentTaskId As Integer = objTaskServices.GetParentTaskId(PTArray(PTArray.Length - 2))
                            objTaskServices.UpdateIsParentInTask(nParentTaskId, True)

                            Dim nProjectId As Integer = objProjectServices.GetProjectId(PTArray(1))
                            Dim nTaskTypeId As Integer = objTaskServices.GetTaskTypeId()
                            Dim nTaskStatusId As Integer = objTaskServices.GetTaskStatusId()
                            Dim nPriorityId As Integer = objTaskServices.GetTaskPriorityId()
                            Dim nProjectMilestoneId As Integer = objProjectServices.GetProjectMilestoneIdByProjectId(nProjectId)
                            Dim nCurrencyId As Integer = objServices.GetCurrencyId()

                            objTaskServices.InsertTask(nProjectId, nParentTaskId, element.QB_Name, element.QB_Name,
                                                       nTaskTypeId, 1, "Months", 0, False, Now.AddMonths(1).Date, nTaskStatusId, nPriorityId,
                                                       nProjectMilestoneId, False, False, Now.Date, nTeamLeadId, Now.Date, nTeamLeadId, 0, 0, "Days",
                                                       True, element.QB_Name, 0, False, nCurrencyId)
                            My.Forms.MAIN.History("Successfully inserted task " + element.QB_Name, "i")
                            'Dim JobAdapter As New QB_TL_IDsTableAdapters.Jobs_SubJobsTableAdapter
                            'JobAdapter.Insert(element.QB_ID, objTaskServices.GetTaskId(element.QB_Name), element.QB_Name, element.FullName)
                        End If

                        'Insert record into sync database if Not in it
                        If Not CBool(DT_has_QBID) Then
                            ' QB ID is not in local database
                            Dim project_or_task_inTL As Boolean =
                                Array.Exists(objTaskServices.GetTasks, Function(e As Services.TimeLive.Tasks.Task) e.JobParent + ":" + e.TaskName = element.FullName) Or
                                Array.Exists(objProjectServices.GetProjects, Function(e As Services.TimeLive.Projects.Project) e.ClientName + ":" + e.ProjectName = element.FullName)

                            If project_or_task_inTL Then
                                ' TimeLive has a data entry with the same name, treat as the same and add into DB
                                Dim JobAdapter As New QB_TL_IDsTableAdapters.Jobs_SubJobsTableAdapter
                                ' Currently does not support the same job name with different parent customers / same subjob name with different parent jobs
                                Dim proj_or_task_ID As Integer = If(PTArray.Length = 2, objProjectServices.GetProjectId(element.QB_Name), objTaskServices.GetTaskId(element.QB_Name))
                                Dim QB_ID_fromDB As QB_TL_IDs.Jobs_SubJobsDataTable = JobAdapter.GetCorrespondingQB_ID(element.FullName)
                                If QB_ID_fromDB.Count = 0 Then
                                    ' No record of the data entry in our data table, then add it
                                    JobAdapter.Insert(element.QB_ID, proj_or_task_ID, element.QB_Name, element.FullName)
                                    My.Forms.MAIN.History("Job/Subjob '" + element.QB_Name + "' found in both TimeLive and Quickbooks added to local database", "i")
                                Else
                                    ' Record exists just with an incorrect QB ID, so update it
                                    Dim correctTL_ID As String = QB_ID_fromDB(0)(1)
                                    If correctTL_ID IsNot Nothing Then
                                        JobAdapter.UpdateQBID(element.QB_ID, Trim(correctTL_ID))
                                        My.Forms.MAIN.History("Updated QuickBooks ID of employee '" + element.QB_Name + "' in local database", "i")
                                    End If
                                End If
                            Else
                                Dim errStr As String = If(PTArray.Length = 2, " client in timelive has a project with name ", " project in timelive has a task with name ")
                                My.Forms.MAIN.History("Error creating record In TimeLive: Make sure that no other " + errStr + element.QB_Name, "N")
                            End If
                        End If

                    End If
                    'If TL_ID_Return = 1 Then

                    '    Dim TL_ID As String = ISTLID_In_DataTableForJobs(element.QB_ID)
                    '    If TL_ID Is Nothing Then
                    '        My.Forms.Main.History("Detected empty sync record (No TL ID).Needs To be manually sync Or deleted." + element.QB_Name, "i")

                    '    Else
                    '        NoRecordsCreatedorUpdated = NoRecordsCreatedorUpdated + 1
                    '        My.Forms.Main.History("Updating TL record For: " + element.QB_Name, "i")


                    '        '-----------------------------------------------------------------------------------------------------------------------------------------------------------------
                    '        ' ----------------------------------------------this part is the update-------------------------------------------------------------------------------------------
                    '        '-----------------------------------------------------------------------------------------------------------------------------------------------------------------
                    '    End If
                    'End If

                End If
            End If
            'if no, UI skip
            If UI Then
                My.Forms.MAIN.ProgressBar1.Value += 1
            End If
            'Catch ex As Exception
            'My.Forms.Main.History(ex.ToString, "C")
            'Throw ex
            'End Try
        Next
        'Catch ex As Exception
        'My.Forms.Main.History(ex.ToString, "C")
        'Throw ex
        'End Try

        Return NoRecordsCreatedorUpdated
    End Function


    '---------------------------------------Transfer Item Data -----------------------------------------------------------------'
    Public Function QBTransferItemsToTL(ByRef objData As QBtoTL_JobOrItem.JobDataStructureQB,
                                   ByVal p_token As String, MainForm As MAIN, UI As Boolean) As Integer
        Dim objProjectServices As Services.TimeLive.Projects.Projects = MAIN.connect_TL_projects(p_token)
        Dim objTaskServices As Services.TimeLive.Tasks.Tasks = MAIN.connect_TL_tasks(p_token)
        Dim objClientServices As Services.TimeLive.Clients.Clients = MAIN.connect_TL_clients(p_token)

        Dim objServices As New Services.TimeLiveServices
        Dim authentication As New Services.SecuredWebServiceHeader
        authentication.AuthenticatedToken = p_token
        objServices.SecuredWebServiceHeaderValue = authentication

        'sets status bar. If no, UI skip
        If UI Then
            My.Forms.MAIN.ProgressBar1.Maximum = objData.DataArray.Count
            My.Forms.MAIN.ProgressBar1.Value = 0
        End If

        Dim NoRecordsCreatedorUpdated As Integer = 0
        ' open session  for TL
        'step1: loop thought our data array

        Try
            Dim nProjectTypeId As Integer = objProjectServices.GetProjectTypeId()
            Dim nClientId As Integer
            Try
                nClientId = objClientServices.GetClientId()
            Catch ex As Exception
                'MAIN.QUITQBSESSION()
                Throw New Exception("Client not exist.")
            End Try
            Dim nProjectBillingTypeId As Integer = objProjectServices.GetProjectBillingTypeId()
            Dim nProjectStatusId As Integer = objProjectServices.GetProjectStatusId()
            Dim nTeamLeadId As Integer = objProjectServices.GetTeamLeadId()
            Dim nProjectManagerId As Integer = objProjectServices.GetProjectManagerId()
            Dim nProjectBillingRateTypeId As Integer = objProjectServices.GetProjectBillingRateTypeId()
            Dim ProjectName As String = Nothing

            For Each element As QBtoTL_JobOrItem.Job_Subjob In objData.DataArray
                ' check if the check value is true
                If element.RecSelect Then
                    If Not element.parent Is Nothing Then
                        My.Forms.MAIN.History("Processing:  " + element.QB_Name, "n")

                        Dim TL_ID_Return = ISQBID_In_DataTableForItems(element.FullName, element.QB_ID)

                        'if none create
                        If TL_ID_Return = 0 Then
                            If MsgBox("New item or subitem found: " + element.QB_Name + ". Create?", MsgBoxStyle.YesNo, "Warning!") = MsgBoxResult.Yes Then
                                NoRecordsCreatedorUpdated += 1
                                ' If item
                                If element.subParentInt = 0 Then

                                    objProjectServices.InsertProject(nProjectTypeId, nClientId, 0,
                                0, nProjectBillingTypeId, element.FullName, element.FullName,
                                Now.Date, Now.AddMonths(1).Date, nProjectStatusId, nTeamLeadId,
                                nProjectManagerId, 0, 0, 1, "Months", element.FullName, 0, nProjectBillingRateTypeId,
                                False, True, 0, Now.Date, nTeamLeadId, Now.Date, nTeamLeadId, False)

                                    Dim JobAdapter As New QB_TL_IDsTableAdapters.Items_SubItemsTableAdapter
                                    JobAdapter.Insert(element.QB_ID, objProjectServices.GetProjectId(element.QB_Name), element.QB_Name, element.FullName)
                                    ProjectName = element.FullName ' Should this be outside the if?
                                End If
                                ' If subitem
                                If element.subParentInt <> 0 Then
                                    'NoRecordsCreatedorUpdated += 1
                                    Dim TaskArray() As String = Split(element.FullName, ":")
                                    Dim ParentLevel As Integer = element.subParentInt - 1
                                    Dim ParentTaskName As String = TaskArray(ParentLevel)
                                    ' First check that a Project with Name = ParentTaskName exists before getting ProjectId
                                    'Dim nProjectId As Integer = objProjectServices.GetProjectId(ProjectName) ' Was ProjectName, which was Nothing
                                    Dim nParentTaskId As Integer
                                    If ParentLevel <> 0 Then
                                        nParentTaskId = objTaskServices.GetParentTaskId(ParentTaskName)
                                        objTaskServices.UpdateIsParentInTask(nParentTaskId, True)
                                    Else
                                        nParentTaskId = 0
                                    End If

                                    Dim nProjectId As Integer
                                    ' Check if Parent Project exists, if not then do not try to add the task
                                    Dim hasParentProject = False
                                    Array.ForEach(objProjectServices.GetProjects, Function(proj As Services.TimeLive.Projects.Project) hasParentProject = If(proj.ProjectName = ParentTaskName, True, hasParentProject))

                                    If hasParentProject Then
                                        nProjectId = objProjectServices.GetProjectId(ParentTaskName) ' Was ProjectName, which was Nothing                                
                                    Else
                                        ' Currently decrement because we do not add the task
                                        ' TODO: Add Project then the task, which would mean we would then increment this value (ie add 2 instead of 0)
                                        NoRecordsCreatedorUpdated -= 1
                                        My.Forms.MAIN.History("Could not Get Project ID. It is likely that the project for this task does not exist. Try adding the Project First", "i")
                                        'My.Forms.Main.History(ex.ToString, "C")
                                        Continue For
                                    End If
                                    Dim nProjectMilestoneId As Integer = objProjectServices.GetProjectMilestoneIdByProjectId(nProjectId)
                                    Dim nTaskTypeId As Integer = objTaskServices.GetTaskTypeId()
                                    Dim nTaskStatusId As Integer = objTaskServices.GetTaskStatusId()
                                    Dim nPriorityId As Integer = objTaskServices.GetTaskPriorityId()
                                    Dim nCurrencyId As Integer = objServices.GetCurrencyId()

                                    objTaskServices.InsertTask(nProjectId, nParentTaskId, element.QB_Name, element.FullName,
                                    nTaskTypeId, 1, "Months", 0, 0, Now.AddMonths(1).Date, nTaskStatusId, nPriorityId,
                                    nProjectMilestoneId, False, False, Now.Date, nTeamLeadId, Now.Date, nTeamLeadId, 0, 0, "Days",
                                    True, element.FullName, 0, False, nCurrencyId)

                                    Dim JobAdapter As New QB_TL_IDsTableAdapters.Items_SubItemsTableAdapter
                                    JobAdapter.Insert(element.QB_ID, objTaskServices.GetTaskId(element.FullName), element.QB_Name, element.FullName)
                                End If
                                'If TL_ID_Return = 1 Then

                                '    Dim TL_ID As String = ISQBID_In_DataTableForItems(element.QB_ID)
                                '    If TL_ID Is Nothing Then
                                '        My.Forms.Main.History("Detected empty sync record (No TL ID). Needs to be manually sync or deleted." + element.QB_Name, "i")

                                '    Else
                                '        NoRecordsCreatedorUpdated = NoRecordsCreatedorUpdated + 1
                                '        My.Forms.Main.History("Updating TL record for: " + element.QB_Name, "i")


                                '        '-----------------------------------------------------------------------------------------------------------------------------------------------------------------
                                '        ' ----------------------------------------------this part is the update--------------------------------------------------------------------------------------------. 
                                '        '-----------------------------------------------------------------------------------------------------------------------------------------------------------------
                                '    End If
                                'End If
                            End If
                        End If
                    End If
                End If
                'if no UI, then skip
                If UI Then
                    My.Forms.MAIN.ProgressBar1.Value += 1
                End If
            Next

        Catch ex As Exception
            My.Forms.MAIN.History(ex.ToString, "C")
        End Try

        Return NoRecordsCreatedorUpdated
    End Function

    Public Function SetLength(ByVal str As String) As String
        Return str.Substring(0, Math.Min(50, str.Length)) ' first 50 chars
    End Function

    Public Function ISQBID_In_DataTableForJobs(ByVal myqbName As String, ByVal myqbID As String) As Int16
        Dim jobAdapter As New QB_TL_IDsTableAdapters.Jobs_SubJobsTableAdapter()
        Dim TimeLiveIDs As QB_TL_IDs.Jobs_SubJobsDataTable = jobAdapter.GetCorrespondingTL_ID(myqbID)
        Dim result As Int16 = Math.Min(TimeLiveIDs.Count, 2) ' 0 -> 0; 1 -> 1; more than 1 -> 2
        Dim numResults As String

        Select Case TimeLiveIDs.Count
            Case 0
                numResults = "No records"
            Case 1
                numResults = "One record"
            Case Else
                numResults = "More than one record"
        End Select

        My.Forms.MAIN.History(numResults + " found in local database for: " + myqbName, "i")

        Return result
    End Function

    Public Function ISQBID_In_DataTableForItems(ByVal myqbName As String, ByVal myqbID As String) As Int16
        Dim ItemAdapter As New QB_TL_IDsTableAdapters.Items_SubItemsTableAdapter()
        Dim TimeLiveIDs As QB_TL_IDs.Items_SubItemsDataTable = ItemAdapter.GetCorrespondingTL_ID(myqbID)
        Dim result As Int16 = Math.Min(TimeLiveIDs.Count, 2) ' 0 -> 0; 1 -> 1; more than 1 -> 2
        Dim numResults As String

        Select Case TimeLiveIDs.Count
            Case 0
                numResults = "No records"
            Case 1
                numResults = "One record"
            Case Else
                numResults = "More than one record"
        End Select

        My.Forms.MAIN.History(numResults + " found in local database for: " + myqbName, "i")

        Return result
    End Function

    'Private Function ISTLID_In_DataTableForItem(ByVal myqbID As String) As String
    '    Dim result As String = Nothing
    '    Dim ItemAdapter As New QB_TL_IDsTableAdapters.Items_SubItemsTableAdapter
    '    Dim TimeLiveIDs As QB_TL_IDs.Items_SubItemsDataTable = ItemAdapter.GetCorrespondingTL_ID(myqbID)

    '    If String.IsNullOrEmpty(Trim(TimeLiveIDs(0).TimeLive_ID.ToString())) Then
    '        My.Forms.Main.History("Record has a TLID of Nothing", "I")
    '    Else
    '        My.Forms.Main.History("Record has a TLID of: " + TimeLiveIDs(0).TimeLive_ID.ToString(), "i")
    '        result = TimeLiveIDs(0).TimeLive_ID.ToString()
    '    End If

    '    Return result
    'End Function

    Private Function ISTLID_In_DataTableForJobs(ByVal myqbID As String) As String
        Dim result As String = Nothing
        Dim JobAdapter As New QB_TL_IDsTableAdapters.Jobs_SubJobsTableAdapter
        Dim TimeLiveIDs As QB_TL_IDs.Jobs_SubJobsDataTable = JobAdapter.GetCorrespondingTL_ID(myqbID)

        If TimeLiveIDs Is Nothing Then Return Nothing
        If TimeLiveIDs.Rows.Count = 0 Then Return Nothing


        If String.IsNullOrEmpty(Trim(TimeLiveIDs(0).TimeLive_ID.ToString())) Then
            My.Forms.MAIN.History("Record has a TLID of Nothing", "I")
        Else
            My.Forms.MAIN.History("Record has a TLID of: " + TimeLiveIDs(0).TimeLive_ID.ToString(), "i")
            result = TimeLiveIDs(0).TimeLive_ID.ToString()
        End If

        Return result
        'Return ""
    End Function

End Class
