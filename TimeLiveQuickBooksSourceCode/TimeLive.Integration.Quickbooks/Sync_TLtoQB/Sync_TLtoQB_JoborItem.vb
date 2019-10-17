Imports QBFC13Lib
Imports System.Text.RegularExpressions

Public Class Sync_TLtoQB_JoborItem
    Const MAX_CUSTOMER_LEN As Integer = 41
    Const MAX_ITEM_LEN As Integer = 31

    ''' <summary>
    ''' Checks if the encoding in TimeLive has the Project assigned as the Task name, and task as an item
    ''' i.e. if in TimeLive: Project = project:task(:subtask:subsubtask...), task = item
    ''' </summary>
    ''' <param name="objTask"></param>
    ''' <returns></returns>
    Public Shared Function storedAsTaskItem(objTask As Services.TimeLive.Tasks.Task) As Boolean
        Return (objTask.JobItemParent.Equals("") And objTask.ItemParent.Split(":").Length > 1)
    End Function


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

                ' Done this way since there is a bug in the TimeLive API: the field .ProjectID always returns 0
                Dim projectID As Integer
                Try
                    projectID = objProjectServices.GetProjectId(objProject.ProjectName)
                Catch ex As System.Web.Services.Protocols.SoapException
                    My.Forms.MAIN.History("Make sure that " + objProject.ProjectName + " has a code attribute", "i")
                    Continue For
                End Try

                Dim fullName As String = objProject.ClientName + ":" + objProject.ProjectName
                Dim create As Boolean = If(nameList Is Nothing, True, nameList.Contains(fullName))

                If create Then
                    ' Checks if projectName contains ':', seperate and then add project then task
                    Dim ret = checkQBJobSubJobExist(objProject.ClientName, objProject.ProjectName, projectID.ToString, UI, p_token, cancel_opt)

                    If ret = -2 Then ' Cancel was selected
                        Return numSynced
                    Else
                        numSynced += Math.Max(0, ret)
                    End If

                    If Not MainForm Is Nothing Then MainForm.ProgressBar1.Value += 1
                End If
            Next

            ' Iterate through all tasks
            For n As Integer = 0 To objTaskArray.Length - 1
                objTask = objTaskArray(n)
                ' Done this way since there is a bug in the TimeLive API: the field .taskID always returns 0
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
                    Dim parentName, taskName As String
                    Dim taskAsItem As Boolean = storedAsTaskItem(objTask)

                    If taskAsItem Then
                        Dim lastColon As Integer = objTask.JobParent.LastIndexOf(":")
                        parentName = objTask.JobParent.Substring(0, lastColon)
                        taskName = objTask.JobParent.Substring(lastColon + 1)

                        Dim jobParentColonSeperated As String() = objTask.JobParent.Split(":")
                        Dim customer As String = jobParentColonSeperated(0)
                        Dim job As String = jobParentColonSeperated(1)
                        Dim itemAdded As Integer = checkQBItemExists(customer, job, objTask.TaskName, UI, p_token)

                        If itemAdded = -2 Then ' Cancel was selected
                            Return numSynced
                        Else
                            numSynced += Math.Max(0, itemAdded)
                        End If
                    Else
                        parentName = objTask.JobParent
                        taskName = objTask.TaskName
                    End If

                    Dim jobAdded = checkQBJobSubJobExist(parentName, taskName, taskID.ToString, UI, p_token, cancel_opt, taskAsItem)

                    If jobAdded = -2 Then ' Cancel was selected
                        Return numSynced
                    Else
                        numSynced += Math.Max(0, jobAdded)
                    End If

                    If Not MainForm Is Nothing Then My.Forms.MAIN.ProgressBar1.Value += 1
                End If
            Next
        Catch ex As System.Web.Services.Protocols.SoapException
            If UI Then
                MsgBox("Make sure all Tasks and Jobs have a Code in TimeLive: " + ex.Message, "i")
            Else
                Throw New Exception("Make sure all Tasks and Jobs have a Code in Timelive: " + ex.Message)
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
    ''' Get Tasks
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

                    If .ItemParent = ParentName And Not storedAsTaskItem(objTask) Then
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


    Function askUserToCreateInQB(ByVal UI As Boolean, ByVal cancel_opt As Boolean, ByVal value As String, Optional type As String = "value")
        Dim MsgBox_result As MsgBoxResult = MsgBoxResult.Yes
        If UI Then
            If cancel_opt Then
                MsgBox_result = MsgBox("New " + type + " found in TimeLive: " & vbCrLf & value & vbCrLf & "Create in QuickBooks?", MsgBoxStyle.YesNoCancel, "Warning!")
            Else
                MsgBox_result = MsgBox("New " + type + " found in TimeLive: " & vbCrLf & value & vbCrLf & "Create in QuickBooks?", MsgBoxStyle.YesNo, "Warning!")
            End If
        End If
        Return MsgBox_result
    End Function

    ' Returns number of Items created
    Function createQBItem(ByVal cappedLenCustomer As String, ByVal cappedLenJob As String, ByVal cappedLenItem As String, ByVal jobIsItemInQB As Boolean, ByVal customerIsItemInQB As Boolean) As Boolean
        Dim newMsgSetRq As IMsgSetRequest = MAIN.SESSMANAGER.CreateMsgSetRequest("US", 2, 0)
        newMsgSetRq.Attributes.OnError = ENRqOnError.roeContinue

        Dim fullProject As String = cappedLenCustomer + ":" + cappedLenJob
        Dim fullItem = fullProject + ":" + cappedLenItem

        ' Add customer as an item, which the actual item will be below
        If Not customerIsItemInQB Then
            Dim customerAdd As IItemServiceAdd = newMsgSetRq.AppendItemServiceAddRq
            customerAdd.Name.SetValue(cappedLenCustomer)
            customerAdd.ORSalesPurchase.SalesOrPurchase.AccountRef.FullName.SetValue("<None>")
        End If

        ' Add job as an item, the child of the customer item and the parent of the actual item
        If Not jobIsItemInQB Then
            Dim jobAdd As IItemServiceAdd = newMsgSetRq.AppendItemServiceAddRq
            jobAdd.Name.SetValue(cappedLenJob)
            jobAdd.ParentRef.FullName.SetValue(cappedLenCustomer)
            jobAdd.ORSalesPurchase.SalesOrPurchase.AccountRef.FullName.SetValue("<None>")
        End If

        Dim itemAdd As IItemServiceAdd = newMsgSetRq.AppendItemServiceAddRq
        itemAdd.Name.SetValue(cappedLenItem)
        itemAdd.ParentRef.FullName.SetValue(fullProject)
        itemAdd.ORSalesPurchase.SalesOrPurchase.AccountRef.FullName.SetValue("<None>")

        'step2: send the request
        Dim msgSetRs As IMsgSetResponse = MAIN.SESSMANAGER.DoRequests(newMsgSetRq)
        Dim numItemsAdded As Integer = msgSetRs.ResponseList.Count
        ' Interpret the responses
        For n As Integer = 0 To numItemsAdded - 1
            Dim res As IResponse = msgSetRs.ResponseList.GetAt(n)
            If res.StatusSeverity = "Error" Then
                Throw New Exception(res.StatusMessage)
                Return -1
            End If
        Next

        My.Forms.MAIN.History("Item in Timelive with name: '" + fullItem + "' added to QuickBooks", "N")
        Return numItemsAdded
    End Function

    Function shortenProjectNameToFitInQB(ByVal jobName As String, ByVal maxLen As Integer)
        If jobName.Length <= maxLen Then
            Return jobName
        End If

        Dim expr As String = "_CY[\d]+$"
        Dim calendarYear As Match = Regex.Match(jobName, expr)

        If calendarYear.Success Then
            Dim numStartingElements = maxLen - calendarYear.Length

            Return jobName.Substring(0, numStartingElements) + calendarYear.Value
        Else
            Return jobName.Substring(0, maxLen)
        End If

    End Function

    ''' <summary>
    ''' Verifies a Job exists in QB, adding it to the Data Table if necessary
    ''' </summary>
    ''' <returns>
    ''' Integer: the number of items added into QB
    '''     -2 -> Did not add to QB because cancel was selected
    '''     -1 -> Did not exist in QB, not added to QB
    '''     0 -> Already exists in QB
    '''     1 -> Did not exist in QB, added to QB
    '''     2+ -> Did not exist in QB, and 1 or more of its parents did not exist in QB either, all added to QB
    ''' </returns>
    Public Function checkQBItemExists(ByVal customer As String, ByVal job As String, ByVal item As String, ByVal UI As Boolean, ByVal p_token As String, Optional ByVal cancel_opt As Boolean = False) As Integer
        Try
            If customer Is Nothing Or job Is Nothing Or item Is Nothing Then
                Return 0
            End If
            If customer = "" Or job = "" Or item = "" Then
                Return 0
            End If

            ' Items in QuickBooks must be no more than 31 characters in size
            Dim cappedLenCustomer As String = If(customer.Trim.Length > MAX_ITEM_LEN, customer.Substring(0, MAX_ITEM_LEN).Trim, customer.Trim)
            Dim cappedLenJob As String = shortenProjectNameToFitInQB(job.Trim, MAX_ITEM_LEN)
            Dim cappedLenItem As String = If(item.Trim.Length > MAX_ITEM_LEN, item.Substring(0, MAX_ITEM_LEN).Trim, item.Trim)
            Dim fullProject As String = cappedLenCustomer + ":" + cappedLenJob
            Dim fullItem As String = fullProject + ":" + cappedLenItem

            Dim msgSetRq As IMsgSetRequest = MAIN.SESSMANAGER.CreateMsgSetRequest("US", 2, 0)
            msgSetRq.Attributes.OnError = ENRqOnError.roeContinue
            Dim itemQueryRq As IItemQuery = msgSetRq.AppendItemQueryRq
            itemQueryRq.ORListQuery.FullNameList.Add(cappedLenCustomer)
            itemQueryRq.ORListQuery.FullNameList.Add(fullProject)
            itemQueryRq.ORListQuery.FullNameList.Add(fullItem)

            Dim msgSetRs As IMsgSetResponse = MAIN.SESSMANAGER.DoRequests(msgSetRq)
            Dim itemRetList As IORItemRetList = msgSetRs.ResponseList.GetAt(0).Detail

            Dim retNum As Integer = If(itemRetList Is Nothing, 0, itemRetList.Count)
            Dim customerIsItemInQB As Boolean = retNum >= 1
            Dim jobIsItemInQB As Boolean = retNum >= 2
            Dim itemInQB As Boolean = retNum = 3

            If Not itemInQB Then
                Dim msgBoxResponse = askUserToCreateInQB(UI, cancel_opt, fullItem, "item")
                If msgBoxResponse = MsgBoxResult.Cancel Then
                    Return -2
                ElseIf msgBoxResponse = MsgBoxResult.No Then
                    Return -1
                Else
                    Return createQBItem(cappedLenCustomer, cappedLenJob, cappedLenItem, jobIsItemInQB, customerIsItemInQB)
                End If
            Else
                My.Forms.MAIN.History("Found item in QB with name: " + fullItem, "i")
            End If

            Return 0

        Catch ex As Exception
            Throw ex
        End Try
    End Function


    ' For the spot in our split array with projectIndex, check that it is formatted the way that projects should be
    Private Sub removeSpacesBetweenColonsAndSetLengthOfFields(ByRef name As String, ByVal maxLen As Integer, Optional projectIndex As Integer = -1)
        Dim splitArr As String() = name.Split(":")
        Dim nameWithoutSpaces = ""
        For i As Integer = 0 To splitArr.Length - 1
            Dim field As String = splitArr(i).Trim
            If i = projectIndex Then
                field = shortenProjectNameToFitInQB(field, maxLen)
            Else
                field = If(field.Length > maxLen, field.Substring(0, maxLen), field)
            End If

            If i <> splitArr.Length - 1 Then field = field + ":"
            nameWithoutSpaces = nameWithoutSpaces + field
        Next

        name = nameWithoutSpaces
    End Sub

    Function checkProjectsCustomerIsInQB(ByVal Parent As String, ByVal TLJobSubJobName As String, ByVal p_token As String, ByVal UI As Boolean) As Integer
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
        ' Note: Will not transfer over email address since we are just passing in new client instead of the actual client - can change this
        Return If(syncCust.checkQBCustomerExist(Parent, client_TL_ID, New Services.TimeLive.Clients.Client, UI), 0, 1)
    End Function


    Function checkTasksProjectIsInQB(ByVal Parent As String, ByVal TLJobSubJobName As String, ByVal p_token As String, ByVal UI As Boolean,
                               ByVal ParentArray As String(), ByVal taskAsItem As Boolean, ByVal cancel_opt As Boolean) As Integer
        Dim parent_name As String = ParentArray(ParentArray.Length - 1)
        Dim parents_parent As String = Parent.Substring(0, Math.Max(Parent.LastIndexOf(":"), 0))
        Dim parent_TL_ID As Integer = -1
        Dim parentIsAProject As Boolean = ParentArray.Length = 2

        Try
            If parentIsAProject Then
                Dim objServices As New Services.TimeLive.Projects.Projects
                Dim authentication As New Services.TimeLive.Projects.SecuredWebServiceHeader
                authentication.AuthenticatedToken = p_token
                objServices.SecuredWebServiceHeaderValue = authentication
                parent_TL_ID = objServices.GetProjectId(parent_name)
            Else ' Parent is a task
                Dim objServices As New Services.TimeLive.Tasks.Tasks
                Dim authentication As New Services.TimeLive.Tasks.SecuredWebServiceHeader
                authentication.AuthenticatedToken = p_token
                objServices.SecuredWebServiceHeaderValue = authentication
                parent_TL_ID = objServices.GetTaskId(parent_name)
            End If
        Catch ex As System.Web.Services.Protocols.SoapException
            If Not taskAsItem Then
                My.Forms.MAIN.History("Verify that '" + parent_name + "' in TimeLive has a code set: " + ex.ToString, "I")
            End If
        End Try

        ' Only okay to check for parent if it is either in TimeLive too, or we stored our task within the project field too and it therefore should not be in TimeLive
        If parent_TL_ID = -1 And Not taskAsItem Then
            My.Forms.MAIN.History("Parent: '" + Parent + "'of Task: " + TLJobSubJobName + " is not in TimeLive.", "I")
            Return -1 ' Exit Function
        Else
            Return checkQBJobSubJobExist(parents_parent, parent_name, parent_TL_ID, UI, p_token, cancel_opt, taskAsItem)
        End If
    End Function

    Sub addToTableAdapter(ByVal qb_name As String, ByVal qb_id As String, ByVal tl_name As String, ByVal tl_id As Integer)
        Dim JobSubJobAdapter As New QB_TL_IDsTableAdapters.Jobs_SubJobsTableAdapter()

        If IsQBID_In_JobSubJobDataTable(qb_name, qb_id) = 0 Then
            ' TL_ID will be -1 when we transfer over projects that are not technically in TimeLive
            ' Example: Project = Project:Task, Task = Item, then Customer:Project is technically not in TimeLive, and will have TL_ID = -1 in DB
            Dim inTL = IsTLID_In_JobsSubJobsDataTable(tl_id)

            If inTL = 0 Then
                ' Not in local database
                My.Forms.MAIN.History("Adding Job to local database: '" + tl_name, "i")
                JobSubJobAdapter.Insert(qb_id, tl_id, qb_name, tl_name)
            ElseIf tl_id = -1 Or inTL > 1 Then
                Dim countByIDAndName = JobSubJobAdapter.GetCountFromTLIDAndTLName(tl_id, tl_name)
                If countByIDAndName = 0 Then
                    JobSubJobAdapter.Insert(qb_id, tl_id, qb_name, tl_name)
                End If
            Else ' In DB once, then just update
                My.Forms.MAIN.History("Updating job/subjob QuickBooks ID in local database: " + tl_name, "i")
                JobSubJobAdapter.UpdateQBID(qb_id, tl_id)
            End If
        Else
            If IsTLID_In_JobsSubJobsDataTable(tl_id) = 0 Then
                ' In local database with a different TimeLive ID
                My.Forms.MAIN.History("Updating job/subjob TimeLive ID in local database: " + tl_name, "i")
                JobSubJobAdapter.UpdateTLID(tl_id, qb_id)
            End If
        End If
    End Sub

    ''' <summary>
    ''' Verifies a Job exists in QB, adding it to the Data Table if necessary
    ''' </summary>
    ''' <param name="TL_Name"></param>
    ''' <param name="TL_ID"></param>
    ''' <returns>
    ''' Integer: the number of items added into QB
    '''     -2 -> Did not add to QB because cancel was sellected
    '''     -1 -> Did not exist in QB, not added to QB
    '''     0 -> Already exists in QB
    '''     1 -> Did not exist in QB, added to QB
    '''     2+ -> Did not exist in QB, and 1 or more of its parents did not exist in QB either, all added to QB
    ''' </returns>
    Public Function checkQBJobSubJobExist(ByRef Parent As String, ByRef TL_Name As String, ByVal TL_ID As Integer, ByVal UI As Boolean,
                                          ByVal p_token As String, Optional ByVal cancel_opt As Boolean = False, Optional taskAsItem As Boolean = False) As Integer
        Try
            Dim parentContainsProjectField As Boolean = Parent.Contains(":")
            If parentContainsProjectField Then
                removeSpacesBetweenColonsAndSetLengthOfFields(Parent, MAX_CUSTOMER_LEN, 1)
                removeSpacesBetweenColonsAndSetLengthOfFields(TL_Name, MAX_CUSTOMER_LEN)
            Else
                removeSpacesBetweenColonsAndSetLengthOfFields(TL_Name, MAX_CUSTOMER_LEN, 0)
            End If

            Dim numAdded As Integer = 0
            Dim TLJobSubJobName As String = Parent + ":" + TL_Name
            Dim jobOrTask As String = If(TLJobSubJobName.Split(":").Length = 2, "Job", "Task")

            Dim msgSetRq As IMsgSetRequest = MAIN.SESSMANAGER.CreateMsgSetRequest("US", 2, 0)
            msgSetRq.Attributes.OnError = ENRqOnError.roeContinue
            Dim TaskSubTaskQueryRq As ICustomerQuery = msgSetRq.AppendCustomerQueryRq
            TaskSubTaskQueryRq.ORCustomerListQuery.FullNameList.Add(TLJobSubJobName)
            Dim msgSetRs As IMsgSetResponse = MAIN.SESSMANAGER.DoRequests(msgSetRq)
            Dim jobsubjobsRetList As ICustomerRetList = msgSetRs.ResponseList.GetAt(0).Detail
            Dim inQB As Boolean = Not jobsubjobsRetList Is Nothing
            Dim newMsgSetRq As IMsgSetRequest = MAIN.SESSMANAGER.CreateMsgSetRequest("US", 2, 0)
            newMsgSetRq.Attributes.OnError = ENRqOnError.roeContinue

            If TLJobSubJobName.IndexOf(":") > 0 And Not inQB Then
                Dim MsgBox_result = askUserToCreateInQB(UI, cancel_opt, TLJobSubJobName, jobOrTask)

                If MsgBox_result = MsgBoxResult.Cancel Then
                    Return -2
                ElseIf MsgBox_result = MsgBoxResult.No Then
                    Return -1
                Else
                    ' Case where Project and Task + SubTasks are all stored within Project, semicolon seperated, in TimeLive
                    ' In this case, add project and parent tasks to the parent array.
                    If TL_Name.Contains(":") Then
                        Parent = Parent + ":" + TL_Name.Substring(0, TL_Name.LastIndexOf(":"))
                        TL_Name = TL_Name.Substring(TL_Name.LastIndexOf(":") + 1)
                        taskAsItem = True
                    End If

                    Dim ParentArray() As String = Parent.Split(":")

                    Dim numAddedThisRound As Integer = 0

                    ' Recursively checks that prior fields exist, and adds them if they were not
                    If ParentArray.Length = 1 Then
                        numAddedThisRound = checkProjectsCustomerIsInQB(Parent, TLJobSubJobName, p_token, UI)
                    Else
                        numAddedThisRound = checkTasksProjectIsInQB(Parent, TLJobSubJobName, p_token, UI, ParentArray, taskAsItem, cancel_opt)
                    End If

                    If numAddedThisRound = -1 Then
                        Return -1
                    Else
                        numAdded += numAddedThisRound
                    End If

                    Dim CustomerAdapter As New QB_TL_IDsTableAdapters.CustomersTableAdapter()
                    Dim QB_Customer As String = CustomerAdapter.GetQB_NameFromTL_Name(ParentArray(0))
                    QB_Customer = If(QB_Customer Is Nothing, ParentArray(0), QB_Customer.Trim)
                    Dim QB_Parent As String = If(Parent.Contains(":"), QB_Customer + Parent.Substring(Parent.IndexOf(":")), QB_Customer)

                    Dim jobAdd As ICustomerAdd = newMsgSetRq.AppendCustomerAddRq
                    jobAdd.ParentRef.FullName.SetValue(QB_Parent)
                    jobAdd.Name.SetValue(TL_Name)

                    msgSetRs = MAIN.SESSMANAGER.DoRequests(newMsgSetRq)
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
                    jobsubjobsRetList = msgSetRs.ResponseList.GetAt(0).Detail
                    numAdded += 1

                    My.Forms.MAIN.History(jobOrTask + " in Timelive with name: " + TLJobSubJobName + " added to QuickBooks", "N")
                End If
            End If

            If jobsubjobsRetList IsNot Nothing Then
                Dim JobSubJobsRet As ICustomerRet = jobsubjobsRetList.GetAt(0)
                Dim qb_name As String = JobSubJobsRet.Name.GetValue.ToString
                Dim qb_id As String = JobSubJobsRet.ListID.GetValue.ToString

                If inQB Then
                    My.Forms.MAIN.History("Found " + jobOrTask + " in QB with name: " + qb_name + " --> ID: " + qb_id, "i")
                End If
                addToTableAdapter(qb_name, qb_id, TLJobSubJobName, TL_ID)
            End If

            Return numAdded
        Catch ex As Exception
            Throw ex
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
    Private Function IsQBID_In_JobSubJobDataTable(ByVal myqbName As String, ByVal myqbID As String) As Int16
        Dim JobSubJobsAdapter As New QB_TL_IDsTableAdapters.Jobs_SubJobsTableAdapter
        Dim TimeLiveIDs As QB_TL_IDs.Jobs_SubJobsDataTable = JobSubJobsAdapter.GetCorrespondingTL_ID(myqbID)
        Dim result As Int16 = Math.Min(2, TimeLiveIDs.Count)
        Dim resultString As String = If(result = 2, "More than one record", If(result = 1, "One Record", "No records"))
        My.Forms.MAIN.History(resultString + "found in local database for: " + myqbName, "i")

        Return result
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
    Private Function IsQBID_In_ItemSubItemDataTable(ByVal myqbName As String, ByVal myqbID As String) As Int16
        Dim ItemsSubItemsAdapter As New QB_TL_IDsTableAdapters.Items_SubItemsTableAdapter
        Dim TimeLiveIDs As QB_TL_IDs.Items_SubItemsDataTable = ItemsSubItemsAdapter.GetCorrespondingTL_ID(myqbID)
        Dim result As Int16 = Math.Min(2, TimeLiveIDs.Count)
        Dim resultString As String = If(result = 2, "More than one record", If(result = 1, "One Record", "No records"))
        My.Forms.MAIN.History(resultString + "found in local database for: " + myqbName, "i")

        Return result
    End Function

    ''' <summary>
    ''' Check if TL ID is in employee data table
    ''' </summary>
    ''' <param name="mytlID"></param>
    ''' <returns>
    ''' 0 -> not in data table
    ''' 1 -> one record in data table
    ''' 2 -> more than one record in data table
    ''' </returns>
    Private Function IsTLID_In_JobsSubJobsDataTable(ByVal mytlID As String) As Int16
        Dim JobAdapter As New QB_TL_IDsTableAdapters.Jobs_SubJobsTableAdapter
        Dim quickbooksIDs As QB_TL_IDs.Jobs_SubJobsDataTable = JobAdapter.GetJobsByTLID(mytlID)
        Return Math.Min(2, quickbooksIDs.Count)
    End Function
End Class