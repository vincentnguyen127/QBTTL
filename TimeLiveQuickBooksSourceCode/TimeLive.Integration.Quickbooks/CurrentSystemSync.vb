Imports QBFC11Lib
Public Class CurrentSystemSync
    Private p_token As String
    Private p_AccountId As String

    Public Sub PassToken(ByVal token As String, ByVal AccountId As String)
        If token Is Nothing Then
            MsgBox("Please Login")
        Else
            p_AccountId = AccountId
            p_token = token
            SyncCustomerData()
            SyncEmployeeData()
            SyncVendorData()
            SyncJobsSubJobData()
        End If
    End Sub

    '---------------------Sync Customer TL Data to QB---------------------------------------
    ''' <summary>
    ''' Sync the customer data from QB. Print out customers that are in TL but not QB
    ''' </summary>
    Sub SyncCustomerData()
        Dim result As Boolean = False

        My.Forms.MAIN.History("Syncing Clients Data", "n")
        Try
            ' connect to Timelive
            Dim play As New Services.TimeLive.Clients.Clients
            Dim objClientServices As New Services.TimeLive.Clients.Clients
            Dim authentication As New Services.TimeLive.Clients.SecuredWebServiceHeader
            authentication.AuthenticatedToken = p_token
            objClientServices.SecuredWebServiceHeaderValue = authentication
            Dim objClientArray() As Object
            objClientArray = objClientServices.GetClients()
            Dim objClient As New Services.TimeLive.Clients.Client

            For n As Integer = 0 To objClientArray.Length - 1
                objClient = objClientArray(n)
                With objClient
                    'Call subroutine
                    'Dim result As Boolean = checkQBCustomerExist(.ClientName.ToString, objClientServices.GetClientIdByName(.ClientName))
                    result = checkQBCustomerExist(.ClientName.ToString, .ClientId)
                    If result = False Then
                        'Does not exist in QB
                        My.Forms.MAIN.History("Please update or enter client into QB --> Name: " + .ClientName.ToString + " ID: " + .ClientId.ToString + " manually", "I")
                    End If
                End With
            Next

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' Verifies a client exists in QB, adding it to the Data Table if necessary
    ''' </summary>
    ''' <param name="TLClientName"></param>
    ''' <param name="TL_ID"></param>
    ''' <returns>
    ''' False: does not exist in QB
    ''' True: does exist in QB, and we add it to the Data Table if not present
    ''' </returns>
    Public Function checkQBCustomerExist(ByRef TLClientName As String, ByVal TL_ID As Integer) As Boolean

        'Dim sessManager As QBSessionManager
        Dim custRet As ICustomerRet

        Try
            'sessManager = New QBSessionManagerClass()
            Dim msgSetRq As IMsgSetRequest = MAIN.SESSMANAGER.CreateMsgSetRequest("US", 2, 0)

            msgSetRq.Attributes.OnError = ENRqOnError.roeContinue
            Dim CustomerQueryRq As ICustomerQuery = msgSetRq.AppendCustomerQueryRq

            CustomerQueryRq.ORCustomerListQuery.FullNameList.Add(TLClientName)
            'sessManager.OpenConnection("App", "TimeLive Quickbooks")
            'sessManager.BeginSession("", ENOpenMode.omDontCare)
            Dim msgSetRs As IMsgSetResponse = MAIN.SESSMANAGER.DoRequests(msgSetRq)

            Dim response As IResponse = msgSetRs.ResponseList.GetAt(0)
            Dim custRetList As ICustomerRetList
            custRetList = response.Detail

            If custRetList Is Nothing Then
                Return False
            Else

                'Assume only one return
                custRet = custRetList.GetAt(0)

                With custRet
                    My.Forms.MAIN.History("Found name in QB: " + .Name.GetValue.ToString + vbTab + "--> ID: " + .ListID.GetValue.ToString, "i")
                    ' check if its in our database if not then add to it.
                    Dim CustomerAdapter As New QB_TL_IDsTableAdapters.CustomersTableAdapter()

                    If ISQBID_In_CustomerDataTable(.Name.GetValue.ToString, .ListID.GetValue) = 0 Then
                        My.Forms.MAIN.History("Adding customer to sync database: " + TLClientName, "i")
                        CustomerAdapter.Insert(.ListID.GetValue, TL_ID, .Name.GetValue, TLClientName)
                    End If
                End With

                Return True
            End If

        Catch ex As Exception
            MAIN.QUITQBSESSION()
            Throw ex
            'Finally
            '    If Not sessManager Is Nothing Then
            '       sessManager.EndSession()
            '       sessManager.CloseConnection()
            '    End If
        End Try
    End Function

    ''' <summary>
    ''' Check if QB ID is in customer data table
    ''' </summary>
    ''' <param name="myqbName">qb name to be printed</param>
    ''' <param name="myqbID">qb id to be verified</param>
    ''' <returns>
    ''' 0 -> not in data table
    ''' 1 -> one record in data table
    ''' 2 -> more than one record in data table
    ''' </returns>
    Private Function ISQBID_In_CustomerDataTable(ByVal myqbName As String, ByVal myqbID As String) As Int16
        Dim result As Int16 = 0

        Dim CustomerAdapter As New QB_TL_IDsTableAdapters.CustomersTableAdapter()
        Dim TimeLiveIDs As QB_TL_IDs.CustomersDataTable = CustomerAdapter.GetCorrespondingTL_ID(myqbID)

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

    '---------------------Sync Employee TL Data to QB---------------------------------------
    ''' <summary>
    ''' Sync the employee data from QB. Print out employees that are in TL but not QB
    ''' </summary>
    Sub SyncEmployeeData()
        Dim result As Boolean = False

        My.Forms.MAIN.History("Syncing Employees Data", "n")
        Try
            ' connect to Time live
            Dim play As New Services.TimeLive.Employees.Employees
            Dim objClientServices As New Services.TimeLive.Employees.Employees
            Dim authentication As New Services.TimeLive.Employees.SecuredWebServiceHeader
            authentication.AuthenticatedToken = p_token
            objClientServices.SecuredWebServiceHeaderValue = authentication
            Dim objClientArray() As Object
            objClientArray = objClientServices.GetEmployees
            Dim objClient As New Services.TimeLive.Employees.Employee

            ' Print employees within TimeLive that are not in QB
            For n As Integer = 0 To objClientArray.Length - 1
                objClient = objClientArray(n)
                With objClient
                    result = checkQBEmployeeExist(.EmployeeName.ToString, .EmployeeId)

                    If result = False Then
                        'Does not exist in QB
                        My.Forms.MAIN.History("Please update or enter employee in QB --> Name: " + .EmployeeName.ToString + " ID: " + .EmployeeId.ToString + " manually", "I")
                    End If
                End With
            Next
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    ''' <summary>
    ''' Verifies an employee exists in QB, adding it to the Data Table if necessary
    ''' </summary>
    ''' <param name="TLEmployeeName"></param>
    ''' <param name="TL_ID"></param>
    ''' <returns>
    ''' False: does not exist in QB
    ''' True: does exist in QB, and we add it to the Data Table if not present
    ''' </returns>
    Public Function checkQBEmployeeExist(ByRef TLEmployeeName As String, ByVal TL_ID As Integer) As Boolean
        'Dim sessManager As QBSessionManager

        Try
            'sessManager = New QBSessionManagerClass()
            Dim msgSetRq As IMsgSetRequest = MAIN.SESSMANAGER.CreateMsgSetRequest("US", 2, 0)

            msgSetRq.Attributes.OnError = ENRqOnError.roeContinue
            Dim EmployeeQueryRq As IEmployeeQuery = msgSetRq.AppendEmployeeQueryRq

            EmployeeQueryRq.ORListQuery.FullNameList.Add(TLEmployeeName)
            'sessManager.OpenConnection("App", "TimeLive Quickbooks")
            'sessManager.BeginSession("", ENOpenMode.omDontCare)
            Dim msgSetRs As IMsgSetResponse = MAIN.SESSMANAGER.DoRequests(msgSetRq)

            Dim response As IResponse = msgSetRs.ResponseList.GetAt(0)
            Dim empRetList As IEmployeeRetList
            empRetList = response.Detail

            If empRetList Is Nothing Then
                Return False
            Else

                'Assume only one return
                Dim EmployeeRet As IEmployeeRet
                EmployeeRet = empRetList.GetAt(0)

                With EmployeeRet
                    My.Forms.MAIN.History("Found name in QB: " + .Name.GetValue.ToString + " --> ID: " + .ListID.GetValue.ToString, "i")
                    ' check if its in our database if not then add to it.
                    Dim EmployeeAdapter As New QB_TL_IDsTableAdapters.EmployeesTableAdapter()

                    ' Add QB employee to Data Table if not present
                    If ISQBID_In_EmployeeDataTable(.Name.GetValue.ToString, .ListID.GetValue) = 0 Then

                        Dim FirstMiddleLastName() As String = TLEmployeeName.Split(" ")
                        Dim EmployeeName As String = Nothing
                        If FirstMiddleLastName.Length = 3 Then
                            EmployeeName = FirstMiddleLastName(2) + ", " + FirstMiddleLastName(0) + " " + FirstMiddleLastName(1)
                        End If
                        If FirstMiddleLastName.Length = 2 Then
                            EmployeeName = FirstMiddleLastName(1) + ", " + FirstMiddleLastName(0)
                        End If
                        If FirstMiddleLastName.Length = 1 Then
                            EmployeeName = FirstMiddleLastName(0)
                        End If

                        My.Forms.MAIN.History("Adding employee to sync database: " + EmployeeName, "i")
                        EmployeeAdapter.Insert(.ListID.GetValue, TL_ID, .Name.GetValue, EmployeeName)
                    End If
                End With

                Return True
            End If

        Catch ex As Exception
            MAIN.QUITQBSESSION()
            Throw ex
            'Finally
            '    If Not sessManager Is Nothing Then
            '       sessManager.EndSession()
            '       sessManager.CloseConnection()
            '    End If
        End Try
    End Function

    ''' <summary>
    ''' Check if QB ID is in employee data table
    ''' </summary>
    ''' <param name="myqbName"></param>
    ''' <param name="myqbID"></param>
    ''' <returns>
    ''' 0 -> not in data table
    ''' 1 -> one record in data table
    ''' 2 -> more than one record in data table
    ''' </returns>
    Private Function ISQBID_In_EmployeeDataTable(ByVal myqbName As String, ByVal myqbID As String) As Int16
        Dim result As Int16 = 0
        Dim EmployeeAdapter As New QB_TL_IDsTableAdapters.EmployeesTableAdapter
        Dim TimeLiveIDs As QB_TL_IDs.EmployeesDataTable = EmployeeAdapter.GetCorrespondingTL_ID(myqbID)

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

    '---------------------Sync Vendor TL Data to QB---------------------------------------
    'Note need to detect contractors; these need to be linked as vendors
    ''' <summary>
    ''' Sync the vendor data from QB. Print out vendors that are in TL but not QB
    ''' </summary>
    Sub SyncVendorData()
        Dim result As Boolean = True

        My.Forms.MAIN.History("Syncing Vendor Data", "n")
        Try
            ' connect to Time live
            Dim play As New Services.TimeLive.Employees.Employees
            Dim objClientServices As New Services.TimeLive.Employees.Employees
            Dim authentication As New Services.TimeLive.Employees.SecuredWebServiceHeader
            authentication.AuthenticatedToken = p_token
            objClientServices.SecuredWebServiceHeaderValue = authentication
            Dim objClientArray() As Object
            objClientArray = objClientServices.GetEmployees
            Dim objClient As New Services.TimeLive.Employees.Employee


            For n As Integer = 0 To objClientArray.Length - 1
                objClient = objClientArray(n)
                With objClient

                    If .IsVendor = True Then
                        result = checkQBEmployeeExist(.EmployeeName.ToString, .EmployeeId)

                        If result = False Then
                            'Does not exist in QB
                            My.Forms.MAIN.History("Please update or enter vendor into QB --> Name: " + .EmployeeName.ToString + " ID: " + .EmployeeName.ToString + " manually", "I")
                        End If
                    End If
                End With

            Next

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    ''' <summary>
    ''' Verifies a vendor exists in QB, adding it to the Data Table if necessary
    ''' </summary>
    ''' <param name="TLEmployeeName"></param>
    ''' <param name="TL_ID"></param>
    ''' <returns>
    ''' False: does not exist in QB
    ''' True: does exist in QB, and we add it to the Data Table if not present
    ''' </returns>
    Public Function checkQBVendorExist(ByRef TLEmployeeName As String, ByVal TL_ID As Integer) As Boolean
        'Dim sessManager As QBSessionManager

        Try
            'sessManager = New QBSessionManagerClass()
            Dim msgSetRq As IMsgSetRequest = MAIN.SESSMANAGER.CreateMsgSetRequest("US", 2, 0)

            msgSetRq.Attributes.OnError = ENRqOnError.roeContinue
            Dim VendorQueryRq As IVendorQuery = msgSetRq.AppendVendorQueryRq

            VendorQueryRq.ORListQuery.FullNameList.Add(TLEmployeeName)
            'sessManager.OpenConnection("App", "TimeLive Quickbooks")
            'sessManager.BeginSession("", ENOpenMode.omDontCare)
            Dim msgSetRs As IMsgSetResponse = MAIN.SESSMANAGER.DoRequests(msgSetRq)

            Dim response As IResponse = msgSetRs.ResponseList.GetAt(0)
            Dim vendorRetList As IVendorRetList
            vendorRetList = response.Detail

            If vendorRetList Is Nothing Then
                Return False
            Else


                'Assume only one return
                Dim VendorRet As IVendorRet
                VendorRet = vendorRetList.GetAt(0)

                With VendorRet
                    My.Forms.MAIN.History("Found vendor name in QB: " + .Name.GetValue.ToString + " --> ID: " + .ListID.GetValue.ToString, "i")
                    ' check if its in our database if not then add to it.
                    Dim VendorAdapter As New QB_TL_IDsTableAdapters.VendorsTableAdapter()

                    If ISQBID_In_VendorDataTable(.Name.GetValue.ToString, .ListID.GetValue) <= 0 Then
                        My.Forms.MAIN.History("Adding to sync database: " + VendorAdapter.GetCorrespondingTL_ID(TL_ID).ToString, "i")
                        VendorAdapter.Update(.ListID.GetValue, TL_ID, .Name.GetValue, TLEmployeeName)

                    End If
                End With

                Return True
            End If

        Catch ex As Exception
            MAIN.QUITQBSESSION()
            Throw ex
            'Finally
            '    If Not sessManager Is Nothing Then
            '       sessManager.EndSession()
            '       sessManager.CloseConnection()
            '    End If
        End Try


    End Function

    ''' <summary>
    ''' Check if QB ID is in vendor data table
    ''' </summary>
    ''' <param name="myqbName"></param>
    ''' <param name="myqbID"></param>
    ''' <returns>
    ''' 0 -> not in data table
    ''' 1 -> one record in data table
    ''' 2 -> more than one record in data table
    ''' </returns>
    Private Function ISQBID_In_VendorDataTable(ByVal myqbName As String, ByVal myqbID As String) As Int16
        Dim result As Int16 = 0
        Dim VendorAdapter As New QB_TL_IDsTableAdapters.VendorsTableAdapter
        Dim TimeLiveIDs As QB_TL_IDs.VendorsDataTable = VendorAdapter.GetCorrespondingTL_ID(myqbID)

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

    '---------------------Sync Job SubJobs TL Data to QB---------------------------------------
    ''' <summary>
    ''' Sync the Jobs data from QB. Print out Projects and Tasks that are in TL but not QB
    ''' </summary>
    Sub SyncJobsSubJobData2()
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
                    SubJobsOrSubData = GetTasks(objProject.ProjectName.ToString)


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


    Sub SyncJobsSubJobData()
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
                    result = checkQBJobSubJobExist(.JobParent.ToString + ":" + .TaskName.ToString, .TaskID.ToString, .JobParent.ToString + ":" + .TaskName.ToString)
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
    Public Function GetTasks(ParentName As String) As QBtoTL_JobOrItem.SubJobsOrSubitems
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
                    My.Forms.MAIN.History("TL Job  Parent: " + .JobParent.ToString + " TL Job Item Parent: " + .JobItemParent.ToString + " TL Item Parent: " + .ItemParent.ToString + vbTab + "TL Job name: " + .TaskName.ToString + ", TL ID: " + .TaskID.ToString, "i")

                    If .ItemParent = ParentName Then
                        SubJobsOrSubData.NoItems = SubJobsOrSubData.NoItems + 1
                        SubJobsOrSubData.DataArray.Add(New QBtoTL_JobOrItem.Job_Item(.TaskName.ToString, .TaskID.ToString))
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
                        JobSubJobAdapter.Insert(.ListID.GetValue, TL_ID, QBJobSubJobName, TL_Name)
                    End If
                End With

                Return True
            End If

        Catch ex As Exception
            MAIN.QUITQBSESSION()
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

    ''' <summary>
    ''' 
    ''' </summary>
    Public Sub GetTime()
        'Dim sessManager As QBSessionManager

        My.Forms.MAIN.History("Searching in QB for all time: ", "i")

        Try
            'sessManager = New QBSessionManagerClass()
            Dim msgSetRq As IMsgSetRequest = MAIN.SESSMANAGER.CreateMsgSetRequest("US", 2, 0)

            msgSetRq.Attributes.OnError = ENRqOnError.roeContinue
            'Dim TimeQueryRq As ICustomerQuery = msgSetRq.AppendCustomerQueryRq

            Dim TimeQueryRq As ITimeTrackingQuery = msgSetRq.AppendTimeTrackingQueryRq

            Dim FromDate As New DateTime(2018, 1, 1, 0, 0, 0)
            TimeQueryRq.ORTimeTrackingTxnQuery.TimeTrackingTxnFilter.ORDateRangeFilter.ModifiedDateRangeFilter.FromModifiedDate.SetValue(FromDate, True)

            'sessManager.OpenConnection("App", "TimeLive Quickbooks")
            'sessManager.BeginSession("", ENOpenMode.omDontCare)
            Dim msgSetRs As IMsgSetResponse = MAIN.SESSMANAGER.DoRequests(msgSetRq)
            Dim response As IResponse = msgSetRs.ResponseList.GetAt(0)
            Dim TimeRetList As ITimeTrackingRetList

            TimeRetList = response.Detail

            Dim TimeTrackingRet As ITimeTrackingRet
            If TimeRetList Is Nothing Then
                My.Forms.MAIN.History("No time not found", "i")
                Return
            Else
                For i As Integer = 0 To TimeRetList.Count - 1

                    TimeTrackingRet = TimeRetList.GetAt(i)

                    With TimeTrackingRet
                        My.Forms.MAIN.History("Found time name in QB: " + .EntityRef.FullName.GetValue.ToString(), "i")
                    End With
                Next
            End If

        Catch ex As Exception
            MAIN.QUITQBSESSION()
            Throw ex
            'Finally
            '    If Not sessManager Is Nothing Then
            '       sessManager.EndSession()
            '       sessManager.CloseConnection()
            '    End If
        End Try
    End Sub

End Class
