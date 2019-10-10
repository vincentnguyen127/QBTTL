﻿Imports QBFC13Lib

Public Class Sync_TLtoQB_JoborItem

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
                End If

                If Not MainForm Is Nothing And create Then MainForm.ProgressBar1.Value += 1
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
                        Dim lastColon = objTask.JobParent.LastIndexOf(":")
                        parentName = objTask.JobParent.Substring(0, lastColon)
                        taskName = objTask.JobParent.Substring(lastColon + 1)
                        checkQBItemExists(objTask.JobParent.Substring(0, objTask.JobParent.IndexOf(":")), objTask.TaskName, UI, p_token)
                    Else
                        parentName = objTask.JobParent
                        taskName = objTask.TaskName
                    End If

                    Dim ret = checkQBJobSubJobExist(parentName, taskName, taskID.ToString, UI, p_token, cancel_opt, taskAsItem)

                    If ret = -2 Then ' Cancel was selected
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
    Public Function checkQBItemExists(ByVal customer As String, ByVal item As String, ByVal UI As Boolean, ByVal p_token As String, Optional ByVal cancel_opt As Boolean = False) As Integer
        Try
            If customer Is Nothing Or item Is Nothing Then
                Return 0
            End If
            If customer = "" Or item = "" Then
                Return 0
            End If

            Dim CustomerAdapter As New QB_TL_IDsTableAdapters.CustomersTableAdapter()
            Dim qb_customer As String = CustomerAdapter.GetQB_NameFromTL_Name(customer)
            customer = If(qb_customer Is Nothing, customer, qb_customer.Trim)

            ' Items in QuickBooks must be no more than 31 characters in size
            Dim cappedLenCustomer As String = If(customer.Length > 31, customer.Substring(0, 31).Trim, customer.Trim)
            Dim cappedLenItem As String = If(item.Length > 31, item.Substring(0, 31).Trim, item.Trim)
            Dim numAdded As Integer = 0
            Dim fullItem As String = cappedLenCustomer + ":" + cappedLenItem
            Dim msgSetRq As IMsgSetRequest = MAIN.SESSMANAGER.CreateMsgSetRequest("US", 2, 0)
            msgSetRq.Attributes.OnError = ENRqOnError.roeContinue
            Dim itemQueryRq As IItemQuery = msgSetRq.AppendItemQueryRq
            itemQueryRq.ORListQuery.FullNameList.Add(cappedLenCustomer)
            itemQueryRq.ORListQuery.FullNameList.Add(fullItem)

            Dim msgSetRs As IMsgSetResponse = MAIN.SESSMANAGER.DoRequests(msgSetRq)
            Dim response As IResponse = msgSetRs.ResponseList.GetAt(0)
            Dim itemRetList As IORItemRetList
            itemRetList = response.Detail

            Dim inQB As Boolean = False
            Dim customerIsItemInQB As Boolean = True

            If itemRetList Is Nothing Then
                ' Neither the customer nor item are in the item list
                customerIsItemInQB = False
            ElseIf itemRetList.Count = 2 Then
                ' Both are in QuickBooks
                inQB = True
            End If

            Dim newMsgSetRq As IMsgSetRequest = MAIN.SESSMANAGER.CreateMsgSetRequest("US", 2, 0)
            newMsgSetRq.Attributes.OnError = ENRqOnError.roeContinue

            If Not inQB Then
                Dim create As Boolean = True
                If UI Then
                    Dim MsgBox_result
                    If cancel_opt Then
                        MsgBox_result = MsgBox("New item found in TimeLive: " & vbCrLf & fullItem & vbCrLf & "Create in QuickBooks?", MsgBoxStyle.YesNoCancel, "Warning!")
                        If MsgBox_result = MsgBoxResult.Cancel Then
                            Return -2
                        End If
                    Else
                        MsgBox_result = MsgBox("New item found in TimeLive: " & vbCrLf & fullItem & vbCrLf & "Create in QuickBooks?", MsgBoxStyle.YesNo, "Warning!")
                    End If

                    create = MsgBox_result = MsgBoxResult.Yes
                End If

                If create Then
                    Dim objClientServices As New Services.TimeLive.Clients.Clients
                    Dim authentication As New Services.TimeLive.Clients.SecuredWebServiceHeader
                    authentication.AuthenticatedToken = p_token
                    objClientServices.SecuredWebServiceHeaderValue = authentication

                    Dim client_TL_ID As Integer
                    Try
                        client_TL_ID = objClientServices.GetClientIdByName(customer)
                    Catch ex As Exception
                        My.Forms.MAIN.History("The parent client : '" + customer + "' of Item: '" + item + "' does not exist in TimeLive", "i")

                        Return -1
                    End Try
                    Dim syncCust As Sync_TLtoQB_Customer = New Sync_TLtoQB_Customer()
                    ' Note: Will note transfer over email address since we are just passing in new client instead of the actual client - can change this
                    numAdded += If(syncCust.checkQBCustomerExist(customer, client_TL_ID, New Services.TimeLive.Clients.Client, UI), 0, 1)

                    ' Need to add customer as an item, which the actual item will be below
                    If Not customerIsItemInQB Then
                        Dim customerAdd As IItemServiceAdd = newMsgSetRq.AppendItemServiceAddRq
                        customerAdd.Name.SetValue(cappedLenCustomer)
                        customerAdd.ORSalesPurchase.SalesOrPurchase.AccountRef.FullName.SetValue("<None>")
                    End If

                    Dim itemAdd As IItemServiceAdd = newMsgSetRq.AppendItemServiceAddRq
                    itemAdd.Name.SetValue(cappedLenItem)
                    itemAdd.ParentRef.FullName.SetValue(cappedLenCustomer)
                    itemAdd.ORSalesPurchase.SalesOrPurchase.AccountRef.FullName.SetValue("<None>")

                    'step2: send the request
                    msgSetRs = MAIN.SESSMANAGER.DoRequests(newMsgSetRq)

                    ' Interpret the responses
                    For n As Integer = 0 To msgSetRs.ResponseList.Count - 1
                        Dim res As IResponse = msgSetRs.ResponseList.GetAt(n)
                        If res.StatusSeverity = "Error" Then
                            Throw New Exception(res.StatusMessage)
                        End If
                    Next

                    numAdded += 1

                    My.Forms.MAIN.History("Item in Timelive with name: " + fullItem + " added to QuickBooks", "N")

                ElseIf inQB Then
                    Return 0
                Else ' Use -1 for when something was not created, but is still not in QB
                    Return -1
                End If
            End If
            'Assume only one return
            Dim itemRet As IORItemRet

            If itemRetList IsNot Nothing Then
                itemRet = itemRetList.GetAt(0)

                With itemRet
                    If inQB Then
                        My.Forms.MAIN.History("Found item in QB with name: " + fullItem, "i")
                    End If

                    ' check if its in our database if not then add to it.
                    Dim ItemSubItemAdapter As New QB_TL_IDsTableAdapters.Items_SubItemsTableAdapter()
                    'MsgBox("Hit:" + .ListID.GetValue + "-- " + TL_ID.ToString)

                    ' Add to table adapter
                    'If IsQBID_In_ItemSubItemDataTable(.Name.GetValue.ToString, .ListID.GetValue) = 0 Then
                    '    If IsTLID_In_JobsSubJobsDataTable(TL_ID) = 0 Then
                    '        ' Not in local database
                    '        My.Forms.MAIN.History("Adding Job to local database: '" + TLJobSubJobName, "i")
                    '        ItemSubItemAdapter.Insert(.ListID.GetValue, TL_ID, .Name.GetValue, TLJobSubJobName) 'QBJobSubJobName
                    '    Else
                    '        ' In local database with a different QuickBooks ID
                    '        My.Forms.MAIN.History("Updating job/subjob QuickBooks ID in local database: " + TLJobSubJobName, "i")
                    '        ItemSubItemAdapter.UpdateQBID(.ListID.GetValue, TL_ID)
                    '    End If
                    'Else
                    '    If IsTLID_In_JobsSubJobsDataTable(TL_ID) = 0 Then
                    '        ' In local database with a different TimeLive ID
                    '        My.Forms.MAIN.History("Updating job/subjob TimeLive ID in local database: " + TLJobSubJobName, "i")
                    '        ItemSubItemAdapter.UpdateTLID(TL_ID, .ListID.GetValue)
                    '    End If
                    'End If
                End With
            End If

            Return numAdded

        Catch ex As Exception
            Throw ex
        End Try
    End Function


    Private Sub removeSpacesBetweenColons(ByRef name As String)
        Dim splitArr As String() = name.Split(":")
        Dim nameWithoutSpaces = ""
        For i As Integer = 0 To splitArr.Length - 1
            Dim part As String = splitArr(i).Trim + If(i = splitArr.Length - 1, "", ":")
            nameWithoutSpaces = nameWithoutSpaces + part
        Next

        name = nameWithoutSpaces
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
            Dim numAdded As Integer = 0
            removeSpacesBetweenColons(Parent)
            removeSpacesBetweenColons(TL_Name)
            Dim TLJobSubJobName As String = Parent + ":" + TL_Name
            Dim jobOrTask As String = If(TLJobSubJobName.Split(":").Length = 2, "Job", "Task")

            Dim msgSetRq As IMsgSetRequest = MAIN.SESSMANAGER.CreateMsgSetRequest("US", 2, 0)

            msgSetRq.Attributes.OnError = ENRqOnError.roeContinue
            Dim TaskSubTaskQueryRq As ICustomerQuery = msgSetRq.AppendCustomerQueryRq

            TaskSubTaskQueryRq.ORCustomerListQuery.FullNameList.Add(TLJobSubJobName)

            Dim msgSetRs As IMsgSetResponse = MAIN.SESSMANAGER.DoRequests(msgSetRq)
            Dim response As IResponse = msgSetRs.ResponseList.GetAt(0)
            Dim jobsubjobsRetList As ICustomerRetList
            jobsubjobsRetList = response.Detail

            Dim inQB As Boolean = Not jobsubjobsRetList Is Nothing

            Dim newMsgSetRq As IMsgSetRequest = MAIN.SESSMANAGER.CreateMsgSetRequest("US", 2, 0)
            newMsgSetRq.Attributes.OnError = ENRqOnError.roeContinue

            If TLJobSubJobName.IndexOf(":") > 0 Then ' Check first that customer name is not an empty string
                Dim create As Boolean = True
                If Not inQB Then
                    If UI Then
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
                        ' Case where Project and Task + SubTasks are all stored within Project, semicolon seperated, in TimeLive
                        ' In this case, add project and parent tasks to the parent array.
                        If TL_Name.Contains(":") Then
                            Parent = Parent + ":" + TL_Name.Substring(0, TL_Name.LastIndexOf(":"))
                            TL_Name = TL_Name.Substring(TL_Name.LastIndexOf(":") + 1)
                            taskAsItem = True
                        End If

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

                        End If

                        If ParentArray.Length > 1 Then ' Check if parent of the task is in QB
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
                                    If Not taskAsItem Then
                                        My.Forms.MAIN.History("Verify that '" + parent_name + "' in TimeLive has a code set: " + ex.ToString, "I")
                                    End If
                                End Try

                            Else ' Parent is a task
                                Dim objTaskServices As New Services.TimeLive.Tasks.Tasks
                                Dim authentication As New Services.TimeLive.Tasks.SecuredWebServiceHeader
                                authentication.AuthenticatedToken = p_token
                                objTaskServices.SecuredWebServiceHeaderValue = authentication

                                Try
                                    parent_TL_ID = objTaskServices.GetTaskId(parent_name)
                                Catch ex As System.Web.Services.Protocols.SoapException
                                    If Not taskAsItem Then
                                        My.Forms.MAIN.History("Verify that '" + parent_name + "' in TimeLive has a code set: " + ex.ToString, "I")
                                    End If
                                End Try
                            End If

                            ' Only okay to check for parent if it is either in TimeLive too, or we stored our task within the project field too and it therefore should not be in TimeLive
                            If parent_TL_ID = -1 And Not taskAsItem Then
                                My.Forms.MAIN.History("Parent: '" + Parent + "'of Task: " + TLJobSubJobName + " is not in TimeLive.", "I")
                                Return -1 ' Exit Function
                            Else
                                numAdded += checkQBJobSubJobExist(parents_parent, parent_name, parent_TL_ID, UI, p_token, cancel_opt, taskAsItem)
                                If numAdded = -1 Then
                                    Return -1
                                End If
                            End If
                        End If
                        Dim jobAdd As ICustomerAdd = newMsgSetRq.AppendCustomerAddRq

                        ' TODO: Change client name to corresponding quickbooks ?
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
                    If IsQBID_In_JobSubJobDataTable(.Name.GetValue.ToString, .ListID.GetValue) = 0 Then
                        ' TL_ID will be -1 when we transfer over projects that are not technically in TimeLive
                        ' Example: Project = Project:Task, Task = Item, then Customer:Project is technically not in TimeLive, and will have TL_ID = -1 in DB
                        Dim inTL = IsTLID_In_JobsSubJobsDataTable(TL_ID)

                        If inTL = 0 Then
                            ' Not in local database
                            My.Forms.MAIN.History("Adding Job to local database: '" + TLJobSubJobName, "i")
                            JobSubJobAdapter.Insert(.ListID.GetValue, TL_ID, .Name.GetValue, TLJobSubJobName)
                        ElseIf TL_ID = -1 Or inTL > 1 Then
                            Dim countByIDAndName = JobSubJobAdapter.GetCountFromTLIDAndTLName(TL_ID, TLJobSubJobName)
                            If countByIDAndName = 0 Then
                                JobSubJobAdapter.Insert(.ListID.GetValue, TL_ID, .Name.GetValue, TLJobSubJobName)
                            End If
                        Else ' In DB once, then just update
                            ' In local database with a different QuickBooks ID
                            My.Forms.MAIN.History("Updating job/subjob QuickBooks ID in local database: " + TLJobSubJobName, "i")
                            JobSubJobAdapter.UpdateQBID(.ListID.GetValue, TL_ID)
                        End If
                    Else
                        If IsTLID_In_JobsSubJobsDataTable(TL_ID) = 0 Then
                            ' In local database with a different TimeLive ID
                            My.Forms.MAIN.History("Updating job/subjob TimeLive ID in local database: " + TLJobSubJobName, "i")
                            JobSubJobAdapter.UpdateTLID(TL_ID, .ListID.GetValue)
                        End If
                    End If
                End With
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