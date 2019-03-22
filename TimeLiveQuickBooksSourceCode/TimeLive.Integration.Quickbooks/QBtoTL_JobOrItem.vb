Imports QBFC11Lib
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
        Sub New(ByVal NewlyAdded_in As String, ByVal QB_Name_in As String, ByVal Email_in As String, ByVal QB_ID_in As String, ByVal Telephone1_in As String,
                ByVal Fax_in As String, QBModTime_in As String, QBCreateTime_in As String, Is_parent_in As String, FullName_in As String, subParentInt_in As Integer)

            RecSelect = False
            QB_Name = QB_Name_in
            NewlyAdded = NewlyAdded_in
            QB_ID = QB_ID_in
            QBModTime = QBModTime_in
            QBCreateTime = QBCreateTime_in
            Email = Email_in
            Telephone1 = Telephone1_in
            Fax = Fax_in
            parent = Is_parent_in
            FullName = FullName_in
            subParentInt = subParentInt_in
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
    Public Function GetJobSubJobData(IntegratedUIForm As IntegratedUI, p_token As String, UI As Boolean) As JobDataStructureQB
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

                'sets status bar, If no, UI skip
                If UI Then
                    Dim pblenth As Integer = If(ptRetList Is Nothing, -1, ptRetList.Count)
                    If pblenth >= 0 Then
                        IntegratedUIForm.ProgressBar1.Maximum = pblenth - 1
                    End If
                End If

                ' Should only be 1 CustomerRet object returned
                Dim ptRet As ICustomerRet
                For i As Integer = 0 To If (ptRetList Is Nothing, -1, ptRetList.Count - 1)

                    ptRet = ptRetList.GetAt(i)
                    With ptRet
                        If Not .ParentRef Is Nothing Then
                            Dim PTArray() As String = Split(.FullName.GetValue, ":")
                            If PTArray.Length >= 2 Then
                                EmailAddress = If(.Email Is Nothing, "", .Email.GetValue)
                                Telephone1 = If(.Phone Is Nothing, "", .Phone.GetValue)
                                Fax = If(.Fax Is Nothing, "", .Fax.GetValue)
                                CreateTime = If(.TimeCreated Is Nothing, "", .TimeCreated.GetValue.ToString)
                                ModTime = If(.TimeModified Is Nothing, CreateTime, .TimeModified.GetValue.ToString)

                                Dim TL_ID_Count = ISQBID_In_DataTableForJobs(.FullName.GetValue, .ListID.GetValue)

                                NewlyAdd = If(TL_ID_Count, "", "N") ' N if new

                                ' will check which type data should be added 
                                Job_subJobData.NoItems += 1
                                Job_subJobData.DataArray.Add(New Job_Subjob(NewlyAdd, .Name.GetValue, EmailAddress, .ListID.GetValue, Telephone1, Fax, ModTime, CreateTime, PTArray(0).ToString, .FullName.GetValue, 0))
                            End If
                        End If
                    End With
                    If UI Then
                        IntegratedUIForm.ProgressBar1.Value = i
                    End If
                Next
            End If
            If msgSetRs.ResponseList.GetAt(0).StatusSeverity = "Error" Then
                My.Forms.MAIN.History(msgSetRs.ResponseList.GetAt(0).StatusMessage, "C")
                Throw New Exception(msgSetRs.ResponseList.GetAt(0).StatusMessage)
            End If
        Catch ex As Exception
            My.Forms.MAIN.History(ex.ToString, "C")
            MAIN.QUITQBSESSION()
            Throw ex
        End Try
        Return Job_subJobData
    End Function

    '---------------------------------------Get Item Data -----------------------------------------------------------------'
    Public Function GetItemSubItemData(IntegratedUIForm As IntegratedUI, p_token As String, UI As Boolean) As JobDataStructureQB
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
            If UI Then
                Dim pblenth As Integer = respList.Count
                If pblenth >= 0 Then
                    IntegratedUIForm.ProgressBar1.Maximum = pblenth - 1
                End If
            End If

            ' Should only expect 1 response
            Dim resp As IResponse
            resp = respList.GetAt(0)
            If resp.StatusCode = 0 Then
                Dim ptRetList As IItemServiceRetList
                ptRetList = resp.Detail
                ' Should only be 1 CustomerRet object returned

                Dim ptRet As IItemServiceRet

                For i As Integer = 0 To ptRetList.Count - 1
                    ' Code below should be altered similarly to above
                    ptRet = ptRetList.GetAt(i)
                    With ptRet
                        CreateTime = If(.TimeCreated Is Nothing, "", .TimeCreated.GetValue.ToString)
                        ModTime = If(.TimeModified Is Nothing, CreateTime, .TimeModified.GetValue.ToString)

                        Dim name As String = If(.ParentRef Is Nothing, .FullName.GetValue, .Name.GetValue)
                        Dim TL_ID_Count = ISQBID_In_DataTableForItems(name, .ListID.GetValue)
                        NewlyAdd = If(TL_ID_Count, "", "N") ' N if new
                        ItemData.NoItems += 1
                        ItemData.DataArray.Add(New Job_Subjob(NewlyAdd, name, "", .ListID.GetValue, "", "", ModTime, CreateTime, "", .FullName.GetValue, .Sublevel.GetValue))
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
                                   ByVal p_token As String, IntegratedUIForm As IntegratedUI, UI As Boolean) As Integer

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

        'sets status bar. If no, UI skip
        Dim incrementbar As Integer = 0
        If UI Then
            Dim pblenth As Integer = objData.DataArray.Count - 1
            If pblenth >= 0 Then
                IntegratedUIForm.ProgressBar1.Maximum = pblenth
                IntegratedUIForm.ProgressBar1.Value = 0
            End If
        End If

        Dim NoRecordsCreatedorUpdated = 0
        ' open session  for TL
        Try
            Dim nProjectTypeId As Integer = objProjectServices.GetProjectTypeId()
            Dim nProjectBillingTypeId As Integer = objProjectServices.GetProjectBillingTypeId()
            Dim nProjectStatusId As Integer = objProjectServices.GetProjectStatusId()
            Dim nTeamLeadId As Integer = objProjectServices.GetTeamLeadId()
            Dim nProjectManagerId As Integer = objProjectServices.GetProjectManagerId()
            Dim nProjectBillingRateTypeId As Integer = objProjectServices.GetProjectBillingRateTypeId()

            For Each element As QBtoTL_JobOrItem.Job_Subjob In objData.DataArray
                ' check if the check value is true
                If element.RecSelect = True Then
                    'Dim PTArray() As String = Split(element.p, ":")
                    If Not element.parent Is Nothing Then
                        My.Forms.MAIN.History("Processing:  " + element.QB_Name, "n")

                        Dim TL_ID_Return = ISQBID_In_DataTableForJobs(element.QB_Name, element.QB_ID)
                        'if none create
                        If TL_ID_Return = 0 Then
                            If MsgBox("New job or subjob found: " + element.QB_Name + ". Create?", MsgBoxStyle.YesNo, "Warning!") = MsgBoxResult.Yes Then
                                NoRecordsCreatedorUpdated += 1

                                Dim PTArray() As String = Split(element.FullName, ":")
                                If PTArray.Length = 2 Then
                                    Dim nClientId As Integer
                                    Try
                                        nClientId = objClientServices.GetClientIdByName(element.parent)
                                    Catch ex As Exception
                                        MAIN.QUITQBSESSION()
                                        Throw New Exception("Error inserting client name """ + element.parent + """ not exist")
                                    End Try

                                    objProjectServices.InsertProject(nProjectTypeId, nClientId, 0,
                                        0, nProjectBillingTypeId, element.QB_Name, element.QB_Name,
                                        Now.Date, Now.AddMonths(1).Date, nProjectStatusId, nTeamLeadId,
                                        nProjectManagerId, 0, 0, 1, "Months", element.QB_Name, 0, nProjectBillingRateTypeId,
                                        False, True, 0, Now.Date, nTeamLeadId, Now.Date, nTeamLeadId, False)

                                    objProjectServices.GetProjectId(element.QB_Name)

                                    Dim JobAdapter As New QB_TL_IDsTableAdapters.Jobs_SubJobsTableAdapter

                                    JobAdapter.Insert(element.QB_ID, objProjectServices.GetProjectId(element.QB_Name), element.QB_Name, element.QB_Name)

                                End If

                                If PTArray.Length > 2 Then
                                    ' Get job of the subjob
                                    ' Check to make sure that the job exists first before trying to add the subjob
                                    'Dim nProjectId As Integer = objProjectServices.GetProjectId(PTArray(PTArray.Length - 2)) ' was element.parent, which was customer
                                    Dim nParentTaskId As Integer

                                    If PTArray.Length > 3 Then
                                        ' Not sure, but should this instead be PTArray.Length - 3? The hierarchy 1 higher than job?
                                        nParentTaskId = objTaskServices.GetParentTaskId(PTArray(PTArray.Length - 2))
                                        objTaskServices.UpdateIsParentInTask(nParentTaskId, True)
                                    Else
                                        nParentTaskId = 0
                                    End If

                                    Dim nProjectId As Integer
                                    ' Check if Job exists, if not then do not try to add the SubJob
                                    Dim hasParentProject = Array.Exists(objProjectServices.GetProjects,
                                                                        Function(proj As Services.TimeLive.Projects.Project)
                                                                            Return proj.ProjectName = PTArray(PTArray.Length - 2)
                                                                        End Function)

                                    If hasParentProject Then
                                        nProjectId = objProjectServices.GetProjectId(PTArray(PTArray.Length - 2)) ' Was ProjectName, which was Nothing                                
                                    Else
                                        ' Currently decrement because we do not add the task
                                        ' TODO: Add Project then the task, which would mean we would then increment this value (ie add 2 instead of 0)
                                        NoRecordsCreatedorUpdated -= 1
                                        My.Forms.MAIN.History("Could not Get Project ID. It is likely that the project for this task does not exist. Try adding the Project First", "i")
                                        'My.Forms.MAIN.History(ex.ToString, "C")
                                        Continue For
                                    End If

                                    Dim nTaskTypeId As Integer = objTaskServices.GetTaskTypeId()
                                    Dim nTaskStatusId As Integer = objTaskServices.GetTaskStatusId()
                                    Dim nPriorityId As Integer = objTaskServices.GetTaskPriorityId()
                                    Dim nProjectMilestoneId As Integer = objProjectServices.GetProjectMilestoneIdByProjectId(nProjectId)
                                    Dim nCurrencyId As Integer = objServices.GetCurrencyId()

                                    objTaskServices.InsertTask(nProjectId, nParentTaskId, element.QB_Name, element.QB_Name,
                                               nTaskTypeId, 1, "Months", 0,
                                               0, Now.AddMonths(1).Date, nTaskStatusId, nPriorityId,
                                               nProjectMilestoneId, False, False, Now.Date, nTeamLeadId, Now.Date, nTeamLeadId, 0, 0, "Days",
                                               True, element.QB_Name, 0, False, nCurrencyId)

                                    Dim JobAdapter As New QB_TL_IDsTableAdapters.Jobs_SubJobsTableAdapter
                                    JobAdapter.Insert(element.QB_ID, objTaskServices.GetTaskId(element.QB_Name), element.QB_Name, element.FullName)
                                End If
                            End If
                        End If
                        'End If
                        'If TL_ID_Return = 1 Then

                        '    Dim TL_ID As String = ISTLID_In_DataTableForJobs(element.QB_ID)
                        '    If TL_ID Is Nothing Then
                        '        My.Forms.MAIN.History("Detected empty sync record (No TL ID). Needs to be manually sync or deleted." + element.QB_Name, "i")

                        '    Else
                        '        NoRecordsCreatedorUpdated = NoRecordsCreatedorUpdated + 1
                        '        My.Forms.MAIN.History("Updating TL record for: " + element.QB_Name, "i")


                        '        '-----------------------------------------------------------------------------------------------------------------------------------------------------------------
                        '        ' ----------------------------------------------this part is the update--------------------------------------------------------------------------------------------. 
                        '        '-----------------------------------------------------------------------------------------------------------------------------------------------------------------
                        '    End If
                        'End If

                    End If
                End If
                'if no, UI skip
                If UI Then
                    IntegratedUIForm.ProgressBar1.Value = incrementbar
                    incrementbar += 1
                End If
            Next
        Catch ex As Exception
            My.Forms.MAIN.History(ex.ToString, "C")
            'Throw ex
        End Try

        Return NoRecordsCreatedorUpdated
    End Function


    '---------------------------------------Transfer Item Data -----------------------------------------------------------------'
    Public Function QBTransferItemsToTL(ByRef objData As QBtoTL_JobOrItem.JobDataStructureQB,
                                   ByVal p_token As String, IntegratedUIForm As IntegratedUI, UI As Boolean) As Integer
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

        'sets status bar. If no, UI skip
        Dim incrementbar As Integer = 0
        If UI Then
            Dim pblenth As Integer = objData.DataArray.Count - 1
            If pblenth >= 0 Then
                IntegratedUIForm.ProgressBar1.Maximum = pblenth
                IntegratedUIForm.ProgressBar1.Value = 0
            End If
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
                                        'My.Forms.MAIN.History(ex.ToString, "C")
                                        Continue For
                                    End If
                                    Dim nProjectMilestoneId As Integer = objProjectServices.GetProjectMilestoneIdByProjectId(nProjectId)
                                    Dim nTaskTypeId As Integer = objTaskServices.GetTaskTypeId()
                                    Dim nTaskStatusId As Integer = objTaskServices.GetTaskStatusId()
                                    Dim nPriorityId As Integer = objTaskServices.GetTaskPriorityId()
                                    Dim nCurrencyId As Integer = objServices.GetCurrencyId()

                                    objTaskServices.InsertTask(nProjectId, nParentTaskId, element.QB_Name, element.FullName,
                                    nTaskTypeId, 1, "Months", 0,
                                    0, Now.AddMonths(1).Date, nTaskStatusId, nPriorityId,
                                    nProjectMilestoneId, False, False, Now.Date, nTeamLeadId, Now.Date, nTeamLeadId, 0, 0, "Days",
                                    True, element.FullName, 0, False, nCurrencyId)

                                    Dim JobAdapter As New QB_TL_IDsTableAdapters.Items_SubItemsTableAdapter
                                    JobAdapter.Insert(element.QB_ID, objTaskServices.GetTaskId(element.FullName), element.QB_Name, element.QB_Name)
                                End If
                                'If TL_ID_Return = 1 Then

                                '    Dim TL_ID As String = ISQBID_In_DataTableForItems(element.QB_ID)
                                '    If TL_ID Is Nothing Then
                                '        My.Forms.MAIN.History("Detected empty sync record (No TL ID). Needs to be manually sync or deleted." + element.QB_Name, "i")

                                '    Else
                                '        NoRecordsCreatedorUpdated = NoRecordsCreatedorUpdated + 1
                                '        My.Forms.MAIN.History("Updating TL record for: " + element.QB_Name, "i")


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
                    IntegratedUIForm.ProgressBar1.Value = incrementbar
                    incrementbar += 1
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

        My.Forms.MAIN.History(numResults + " found in QB sync table for: " + myqbName, "i")

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

        My.Forms.MAIN.History(numResults + " found in QB sync table for: " + myqbName, "i")

        Return result
    End Function

    'Private Function ISTLID_In_DataTableForItem(ByVal myqbID As String) As String
    '    Dim result As String = Nothing
    '    Dim ItemAdapter As New QB_TL_IDsTableAdapters.Items_SubItemsTableAdapter
    '    Dim TimeLiveIDs As QB_TL_IDs.Items_SubItemsDataTable = ItemAdapter.GetCorrespondingTL_ID(myqbID)

    '    If String.IsNullOrEmpty(Trim(TimeLiveIDs(0).TimeLive_ID.ToString())) Then
    '        My.Forms.MAIN.History("Record has a TLID of Nothing", "I")
    '    Else
    '        My.Forms.MAIN.History("Record has a TLID of: " + TimeLiveIDs(0).TimeLive_ID.ToString(), "i")
    '        result = TimeLiveIDs(0).TimeLive_ID.ToString()
    '    End If

    '    Return result
    'End Function
    'Private Function ISTLID_In_DataTableForJobs(ByVal myqbID As String) As String
    '    Dim result As String = Nothing
    '    Dim JobAdapter As New QB_TL_IDsTableAdapters.Jobs_SubJobsTableAdapter
    '    Dim TimeLiveIDs As QB_TL_IDs.Jobs_SubJobsDataTable = JobAdapter.GetCorrespondingTL_ID(myqbID)

    '    If String.IsNullOrEmpty(Trim(TimeLiveIDs(0).TimeLive_ID.ToString())) Then
    '        My.Forms.MAIN.History("Record has a TLID of Nothing", "I")
    '    Else
    '        My.Forms.MAIN.History("Record has a TLID of: " + TimeLiveIDs(0).TimeLive_ID.ToString(), "i")
    '        result = TimeLiveIDs(0).TimeLive_ID.ToString()
    '    End If

    '    Return result
    'End Function

End Class
