﻿Imports QBFC13Lib
Imports System.Net.Mail
Imports System.Data.SqlClient

Public Class TLtoQB_TimeEntry

    Public Class EmployeeDataStructure
        Public NoItems As Integer = 0
        Public DataArray As New List(Of Employee)
    End Class
    Public Class Employee
        Public RecSelect As Boolean
        Public FullName As String
        Public AccountEmployeeId As String
        Public HoursWorked As Double
        Sub New(ByVal RecSelect_in As Boolean, ByVal FullName_in As String,
                ByVal AccountEmployeeId_in As String, Optional Hours_in As Double = 0)
            RecSelect = RecSelect_in
            FullName = FullName_in
            AccountEmployeeId = AccountEmployeeId_in
            HoursWorked = Hours_in
        End Sub
    End Class

    Public Class TimeEntryDataStructureQB
        Public NoItems As Integer = 0
        Public DataArray As New List(Of TimeEntry)

        Public Sub combine(other As TimeEntryDataStructureQB)
            Me.NoItems += other.NoItems
            Me.DataArray.AddRange(other.DataArray)
        End Sub

        Public Sub clear()
            Me.NoItems = 0
            Me.DataArray = New List(Of TimeEntry)
        End Sub
    End Class
    Public Class TimeEntry
        Public RecSelect As Boolean
        Public CustomerName As String
        Public EmployeeName As String
        Public IsBillable As Boolean
        Public ProjectName As String
        Public TaskWithParent As String
        Public TotalTime As Date
        Public TimeEntryDate As Date
        Public TimeEntryClass As String
        Public PayrollItem_TypeName As Boolean
        Public PayrollItem As String
        Public PayrollName As String
        Public ServiceItem_TypeName As Boolean
        Public ServiceItem As String
        Public ServiceName As String

        Sub New(ByVal CustomerName_in As String, ByVal EmployeeName_in As String, ByVal IsBillable_in As Boolean,
                ByVal ProjectName_in As String,
                ByVal TaskWithParent_in As String, ByVal TotalTime_in As Date, ByVal TimeEntryDate_in As Date,
                ByVal TimeEntryClass_in As String, ByVal PayrollItem_TypeName_in As Boolean,
                ByVal PayrollItem_in As String, ByVal ServiceItem_TypeName_in As Boolean, ByVal ServiceItem_in As String,
                Optional ByVal PayrollName_in As String = Nothing, Optional ByVal ServiceName_in As String = Nothing)

            RecSelect = False
            CustomerName = CustomerName_in
            EmployeeName = EmployeeName_in
            IsBillable = IsBillable_in
            ProjectName = ProjectName_in
            TaskWithParent = TaskWithParent_in
            TotalTime = TotalTime_in
            TimeEntryDate = TimeEntryDate_in
            TimeEntryClass = TimeEntryClass_in
            PayrollItem_TypeName = PayrollItem_TypeName_in
            PayrollItem = PayrollItem_in
            ServiceItem_TypeName = ServiceItem_TypeName_in
            ServiceItem = ServiceItem_in
            If Not PayrollName_in Is Nothing Then
                PayrollName = PayrollName_in.Trim
            End If
            If Not ServiceName_in Is Nothing Then
                ServiceName = ServiceName_in.Trim
            End If
        End Sub
    End Class

    Public Function GetTimeEntryTLData(AccountEmployeeId As Integer, dpStartDate As DateTime, dpEndDate As DateTime,
                                       MainForm As MAIN, ByVal token As String, UI As Boolean) As TimeEntryDataStructureQB
        Dim TimeEntryData As New TimeEntryDataStructureQB
        Try
            Dim objTimeTrackingServices As Services.TimeLive.TimeEntries.TimeEntries = MAIN.connect_TL_time_entries(token)
            Dim objTimeEntryArray() As Object = objTimeTrackingServices.GetTimeEntriesByEmployeeIdAndDateRange(AccountEmployeeId, CDate(dpStartDate).Date, CDate(dpEndDate).Date)
            Dim timeentry_tltoqb As TLtoQB_TimeEntry = New TLtoQB_TimeEntry
            Dim temp As String = Nothing

            Dim objTimeEntry As New Services.TimeLive.TimeEntries.TimeEntry

            'sets status bar. If no, UI skip
            If UI Then
                My.Forms.MAIN.ProgressBar1.Maximum = objTimeEntryArray.Length
                My.Forms.MAIN.ProgressBar1.Value = 0
            End If

            'Dim EmployeeName As String = Nothing
            Dim FirstMiddleLastName() As String = Nothing

            For n As Integer = 0 To objTimeEntryArray.Length - 1
                objTimeEntry = objTimeEntryArray(n)

                With objTimeEntry
                    TimeEntryData.NoItems += 1

                    Dim empId As String = Get_QB_ID_ForTL_EmployeeName(.EmployeeName).Trim() ' EmployeeName)

                    ' Checks if the name is actually stored as "Last, First" instead of "First Last"
                    If empId = Nothing Then
                        Dim EmployeeName As String = Nothing

                        FirstMiddleLastName = .EmployeeName.ToString().Split(" ")
                        If FirstMiddleLastName.Length = 3 Then
                            EmployeeName = FirstMiddleLastName(2) + ", " + FirstMiddleLastName(0) + " " + FirstMiddleLastName(1)
                        End If

                        If FirstMiddleLastName.Length = 2 Then
                            EmployeeName = FirstMiddleLastName(1) + ", " + FirstMiddleLastName(0)
                        End If
                        If FirstMiddleLastName.Length = 1 Then
                            EmployeeName = FirstMiddleLastName(0)
                        End If

                        empId = Get_QB_ID_ForTL_EmployeeName(EmployeeName).Trim()
                    End If

                    Dim fullName As String
                    If .ProjectName.Contains(":") Then
                        fullName = .ClientName.ToString + ":" + .ProjectName.ToString
                    Else
                        fullName = .ClientName.ToString + ":" + .ProjectName.ToString + ":" + .TaskWithParent.ToString
                    End If

                    Sync_TLtoQB_JoborItem.removeSpacesBetweenColonsAndSetLengthOfFields(fullName)
                    Dim jobID As String = Get_QB_ID_ForTL_JobName(fullName, UI)
                    Dim Item_SubItemID As String = Get_QB_ID_ForTL_ItemName(empId, jobID).ToString.Trim

                    Dim Payroll_Item_SubItemID As String = Get_QB_PayrollItemID(empId, jobID).ToString.Trim

                    Dim PayrollItem_TypeName As Boolean = False
                    Dim ServiceItem_TypeName As Boolean = False

                    Dim ItemName As String = get_QB_Name_ForTL_ItemName(Item_SubItemID) 'exception

                    If Not ItemName Is Nothing Then
                        ItemName = ItemName.Trim
                        My.Forms.MAIN.History("Item name: " + ItemName, "i")
                    End If

                    If Item_SubItemID = "" Then
                        Item_SubItemID = "<None>" 'ItemName
                        ServiceItem_TypeName = True
                    End If

                    Dim PayrollName As String = GetPayrollItem(objTimeEntry)

                    If Not PayrollName Is Nothing Then
                        PayrollName = PayrollName.Trim
                        My.Forms.MAIN.History("Payroll Name: " + PayrollName, "i")
                    Else
                        'My.Forms.MainHistory("Payroll ID: " + Payroll_Item_SubItemID, "i")
                    End If

                    If Payroll_Item_SubItemID = "" Then
                        Payroll_Item_SubItemID = PayrollName ' GetPayrollItem(objTimeEntry)
                        PayrollItem_TypeName = True
                    End If

                    Dim ClassName = If(My.Settings.TransferToPayroll = "", "<None>", If(My.Settings.TransferToPayroll, GetClass(objTimeEntry), "<None>"))

                    ' Check if a time entry has yet to be approved
                    'Dim TL_TimeEntries As New TimeLiveDataSetTableAdapters.AccountEmployeeTimeEntryPeriodTableAdapter
                    'Dim TimeEntryApproved As Boolean = TL_TimeEntries.GetTimeApproval(AccountEmployeeId, .TimeEntryDate)
                    TimeEntryData.DataArray.Add(New TimeEntry(.ClientName, .EmployeeName, .IsBillable, .ProjectName, .TaskWithParent, .TotalTime,
                                                          .TimeEntryDate, ClassName, PayrollItem_TypeName, Payroll_Item_SubItemID,
                                                          ServiceItem_TypeName, Item_SubItemID, PayrollName, ItemName))
                End With

                If UI Then
                    My.Forms.MAIN.ProgressBar1.Value += 1
                End If
            Next

        Catch ex As Exception
            My.Forms.MAIN.History("Error retrieving time entries: " + ex.Message, "N")
        End Try

        Return TimeEntryData
    End Function

    Public Function TLTransferTimeToQB(ByRef objData As TLtoQB_TimeEntry.TimeEntryDataStructureQB,
                                   ByVal token As String, MainForm As MAIN, UI As Boolean) As Integer
        'sets status bar. If no, UI skip
        If UI Then
            My.Forms.MAIN.ProgressBar1.Maximum = objData.DataArray.Count
            My.Forms.MAIN.ProgressBar1.Value = 0
        End If

        Dim NoRecordsCreatedorUpdated = 0
        'step1: prepare the request
        Dim msgSetRs As IMsgSetResponse
        Dim msgSetRq As IMsgSetRequest = MAIN.SESSMANAGER.CreateMsgSetRequest("US", 2, 0)
        Try
            msgSetRs = MAIN.SESSMANAGER.DoRequests(msgSetRq)
            For Each element As TLtoQB_TimeEntry.TimeEntry In objData.DataArray
                ' check if the check value is true
                If element.RecSelect Then
                    Try
                        Dim RecordTxnID As String = Nothing
                        With element
                            RecordTxnID = AddTimeEntryInQB(.CustomerName, .EmployeeName, .IsBillable, .ProjectName,
                                                           .TaskWithParent, .TotalTime, .TimeEntryDate, .TimeEntryClass,
                                                           .PayrollItem_TypeName, .PayrollItem, .ServiceItem_TypeName, .ServiceItem, UI)

                            My.Forms.MAIN.History("Inserted time entry for " + .EmployeeName + " on " + .TimeEntryDate + " for task " + .TaskWithParent, "i")
                            NoRecordsCreatedorUpdated += 1
                        End With
                    Catch ex As Exception
                        Dim error_msg As String = "Error inserting time for " + element.EmployeeName + " for the task" + element.TaskWithParent + " on " + element.TimeEntryDate +
                                                  " into quickbooks." + vbCrLf + "Error Message: " + ex.Message
                        If UI Then
                            MsgBox(error_msg, MsgBoxStyle.OkOnly, "Error inserting Time Entry into QuickBooks")
                        End If
                        My.Forms.MAIN.History(error_msg, "n")
                    End Try
                End If
                'if no UI, then skip
                If UI Then
                    My.Forms.MAIN.ProgressBar1.Value += 1
                End If
            Next
        Catch ex As Exception
            'MAIN.QUITQBSESSION()
            Throw ex
        End Try

        Return NoRecordsCreatedorUpdated
    End Function

    ''' <summary>
    ''' 'Checks if QBID is in sync database
    ''' </summary>
    ''' <param name="mytlID"></param>
    ''' <returns></returns>
    Private Function ISTLID_In_DataTable(ByVal mytlID As String) As Int16
        Dim TimeEntrieAdapter As New QB_TL_IDsTableAdapters.TimeEntriesTableAdapter()
        Dim QuickBooksIDs As QB_TL_IDs.TimeEntriesDataTable = TimeEntrieAdapter.GetCorrespondingQB_ID(mytlID)
        Dim result = Math.Min(QuickBooksIDs.Count, 2)
        Dim numResults As String

        Select Case QuickBooksIDs.Count
            Case 0
                numResults = "No record"
            Case 1
                numResults = "One record"
            Case Else
                numResults = "More than one record"
        End Select

        My.Forms.MAIN.History(numResults + " found in local database", "i")
        Return result
    End Function

    Public Function Get_QB_ID_ForTL_EmployeeName(ByVal employeeName As String) As String
        Dim result As String = ""
        My.Forms.MAIN.History("Finding employeeID using Employee Name: " + employeeName, "n")

        Dim EmployeeAdapter As New QB_TL_IDsTableAdapters.EmployeesTableAdapter()
        Dim EmployeeQBID As QB_TL_IDs.EmployeesDataTable = EmployeeAdapter.GetEmployeeByTL_Name(employeeName)

        Dim VendorAdapter As New QB_TL_IDsTableAdapters.VendorsTableAdapter()
        ' No employee found, check if it was actually a vendor
        If EmployeeQBID.Count = 0 Then
            Dim VendorQBID As QB_TL_IDs.VendorsDataTable = VendorAdapter.GetVendorByTL_Name(employeeName)
            My.Forms.MAIN.History("Found " + VendorQBID.Count.ToString + " matching employee/vendor ID" +
                                  If(VendorQBID.Count = 1, "", "s") + " for: " + employeeName, "I")
            result = If(VendorQBID.Count, VendorQBID(0)(0).ToString, "")
        Else
            My.Forms.MAIN.History("Found " + EmployeeQBID.Count.ToString + " matching employee ID" +
                                  If(EmployeeQBID.Count = 1, "", "s") + " for: " + employeeName, "i")
            result = If(EmployeeQBID.Count, EmployeeQBID(0)(0).ToString, "")
        End If

        'My.Forms.MainHistory("QB Employee ID: " + result, "i")
        ' Return string of the ID (Empty string if none found)
        Return result
    End Function

    Public Function get_QB_Name_ForTL_ItemName(ByVal ItemID As String) As String
        Dim ItemAdapter As New QB_TL_IDsTableAdapters.Items_SubItemsTableAdapter
        Dim ItemsQBID As String = ItemAdapter.GetItemNamefromID(ItemID)
        Return ItemsQBID
    End Function

    Public Function Get_QB_ID_ForTL_ItemName(ByVal EmployeeQBID As String, ByVal jobQBID As String) As String
        Dim result As String
        ' check this to get Item Id
        'My.Forms.MainHistory("Finding Item ID using QB employee ID and QB Job ID.", "n")
        'My.Forms.MainHistory("QB Employee ID: " + EmployeeQBID, "i")
        'My.Forms.MainHistory("QB Job ID: " + jobQBID, "i")
        Dim ItemAdapter As New QB_TL_IDsTableAdapters.ChargingRelationshipsTableAdapter
        Dim ItemQBID As QB_TL_IDs.ChargingRelationshipsDataTable = ItemAdapter.GetIItemIDByEmployeeIDAndJob_SubJobID(EmployeeQBID, jobQBID)

        If ItemQBID.Count > 1 Then
            My.Forms.MAIN.History("Found more than one matching items ID: " + ItemQBID.Count.ToString + ". Returning the first one found in the database", "I")
            Return ItemQBID.Rows(0)(4).ToString
        ElseIf ItemQBID.Count = 0 Then
            My.Forms.MAIN.History("Did not find any matching items ID: " + ItemQBID.Count.ToString, "I")
            Return ""
        End If

        result = ItemQBID.Rows(0)(4).ToString
        My.Forms.MAIN.History("QB Item ID: " + result, "i")
        Return result
    End Function

    Public Function Get_QB_ID_ForTL_JobName(ByVal JobName As String, Optional UI As Boolean = False) As String
        Dim result As String

        My.Forms.MAIN.History("Finding Job ID using JobName: " + JobName, "n")
        Dim JobAdapter As New QB_TL_IDsTableAdapters.Jobs_SubJobsTableAdapter()
        Dim JobQBID As QB_TL_IDs.Jobs_SubJobsDataTable = JobAdapter.GetCorrespondingQB_ID(JobName)
        If JobQBID.Count > 1 Then
            My.Forms.MAIN.History("Found " + JobQBID.Count.ToString + " matching JobIDs for the job " + JobName, "I")
            Dim QB_ID As String = JobQBID(0)(0).ToString

            For n As Integer = 1 To JobQBID.Count
                If JobQBID(n)(0).ToString <> QB_ID And UI Then
                    MsgBox("Database has more than one entry for the job " + JobName + " with different QB IDs. Please clean the job database.", MsgBoxStyle.OkOnly, "Conflict in the Job database")
                End If
            Next

            Return QB_ID
        End If
        If JobQBID.Count = 0 Then
            My.Forms.MAIN.History("Did not find any matching JobID: " + JobQBID.Count.ToString, "I")
            Return ""
        End If
        result = JobQBID.Rows(0)(0).ToString

        My.Forms.MAIN.History("Results of Job ID: " + result, "i")
        Return result
    End Function

    Private Function Get_QB_PayrollItemID(ByVal EmployeeQBID As String, ByVal jobQBID As String) As String
        Dim result As String
        My.Forms.MAIN.History("Finding Payroll Item ID using QB employee ID " + EmployeeQBID +
                              " and QB Job ID " + jobQBID + ".", "n")
        Dim payrollItemId As New QB_TL_IDsTableAdapters.ChargingRelationshipsTableAdapter
        Dim PayrollQBID As QB_TL_IDs.ChargingRelationshipsDataTable = payrollItemId.GetPayrollItemIDByEmployeeIDAndJob_SubJobID(EmployeeQBID, jobQBID)
        If PayrollQBID.Count > 1 Then
            My.Forms.MAIN.History("Found more than one matching PayrollItem IDs: " + PayrollQBID.Count.ToString + ". Using the first one found in the database", "I")
            Return PayrollQBID.Rows(0)(3).ToString
        ElseIf PayrollQBID.Count = 0 Then
            My.Forms.MAIN.History("Did not find any matching PayrollItems: " + PayrollQBID.Count.ToString, "I")
            Return ""
        End If

        result = PayrollQBID.Rows(0)(3).ToString
        My.Forms.MAIN.History("Results of PayRoll ID: " + result.ToString, "i")
        Return result
    End Function

    ''' <summary>
    ''' Check if QBID is in sync database and if it has a corresponding TLID
    ''' </summary>
    ''' <param name="mytlID"></param>
    ''' <returns>
    '''
    ''' </returns>
    Private Function ISQBID_In_DataTable(ByVal mytlID As String) As String
        Dim result As String = Nothing
        Dim TimeAdapter As New QB_TL_IDsTableAdapters.TimeEntriesTableAdapter()
        Dim TimeLiveIDs As QB_TL_IDs.TimeEntriesDataTable = TimeAdapter.GetCorrespondingQB_ID(mytlID)

        If String.IsNullOrEmpty(Trim(TimeLiveIDs(0).TimeLive_ID.ToString())) Then
            My.Forms.MAIN.History("Record has a TLID of Nothing", "I")
        Else
            My.Forms.MAIN.History("Record has a TLID of: " + TimeLiveIDs(0).TimeLive_ID.ToString(), "i")
            result = TimeLiveIDs(0).TimeLive_ID.ToString()
        End If

        Return result
    End Function

    'Adds entries to QB based on Job Item Selection
    Public Function AddTimeEntryInQB(ByVal CustomerName As String, ByVal EmployeeName As String, ByVal IsBillable As Boolean,
                                ByVal ProjectName As String, ByVal ServiceItemName As String, ByVal TotalTime As DateTime,
                                ByVal TimeEntryDate As Date, ByVal TimeEntryClass As String, ByVal PayrollItem_TypeName As Boolean,
                                ByVal PayrollItem As String, ByVal ServiceItem_TypeName As Boolean, ByVal ItemID As String, Optional UI As Boolean = False) As String
        ' Default is JobSubJob when no JobHierarchy has yet to be selected
        Dim rbtJobitems_AppSettings As Integer = If(My.Settings.JobHierarchy = "", 1, My.Settings.JobHierarchy)
        Dim RecordTxnID As String = Nothing

        If rbtJobitems_AppSettings = 0 Then
            RecordTxnID = AddTimeEntryInQBJobItem(CustomerName, EmployeeName, IsBillable, ProjectName, ServiceItemName, TotalTime,
                                    TimeEntryDate, TimeEntryClass, PayrollItem)
        ElseIf rbtJobitems_AppSettings = 1 Then
            RecordTxnID = AddTimeEntryInQBJobSubJob(CustomerName, EmployeeName, IsBillable, ProjectName, ServiceItemName, TotalTime,
                                      TimeEntryDate, TimeEntryClass, PayrollItem_TypeName, PayrollItem, ServiceItem_TypeName, ItemID, UI)
        ElseIf rbtJobitems_AppSettings = 2 Then
            RecordTxnID = AddTimeEntryInQBItemSubItem(CustomerName, EmployeeName, IsBillable, ProjectName, ServiceItemName, TotalTime,
                                        TimeEntryDate, TimeEntryClass, PayrollItem_TypeName, PayrollItem, ServiceItem_TypeName, ItemID)
        End If

        Return RecordTxnID
    End Function

    Public Function AddTimeEntryInQBJobItem(ByVal CustomerName As String, ByVal EmployeeName As String, ByVal IsBillable As Boolean,
                                       ByVal ProjectName As String, ByVal ServiceItemName As String, ByVal TotalTime As DateTime,
                                       ByVal TimeEntryDate As Date, ByVal TimeEntryClass As String, ByVal PayrollItem As String) As String
        'step1: prepare the request
        Dim msgSetRs As IMsgSetResponse
        Try
            'ProjectName = SetLength(ProjectName)
            Dim msgSetRq As IMsgSetRequest = MAIN.SESSMANAGER.CreateMsgSetRequest("US", 2, 0)
            msgSetRq.Attributes.OnError = ENRqOnError.roeContinue
            Dim timeAdd As ITimeTrackingAdd = msgSetRq.AppendTimeTrackingAddRq
            timeAdd.CustomerRef.FullName.SetValue(CustomerName & ":" & ProjectName) ' CustomerName & ":" & ProjectName & ":" ServiceItemName
            timeAdd.Duration.SetValue(TotalTime.Hour, TotalTime.Minute, 0, False)
            timeAdd.EntityRef.FullName.SetValue(EmployeeName)
            timeAdd.IsBillable.SetValue(IsBillable)
            timeAdd.ItemServiceRef.FullName.SetValue(ServiceItemName)
            timeAdd.TxnDate.SetValue(TimeEntryDate)
            If Not TimeEntryClass = "<None>" Then
                AddClass(TimeEntryClass)
                timeAdd.ClassRef.FullName.SetValue(TimeEntryClass)
            End If
            If Not PayrollItem = "<None>" Then
                AddPayrollItem(PayrollItem)
                timeAdd.PayrollItemWageRef.FullName.SetValue(PayrollItem)
            End If

            'step2: send the request
            msgSetRs = MAIN.SESSMANAGER.DoRequests(msgSetRq)

            ' Interpret the response
            Dim response As IResponse
            response = msgSetRs.ResponseList.GetAt(0)

            If response.StatusSeverity = "Error" Then
                Throw New Exception(response.StatusMessage)
            End If

            ' The response detail for Add and Mod requests is a 'Ret' object
            ' In our case, it's ICustomerRet
            Dim TimeEntryRet As ITimeTrackingRet
            TimeEntryRet = response.Detail

            ' Make sure a customerRet was returned before trying to obtain

            Return TimeEntryRet.TxnID.GetValue.ToString

        Catch ex As Exception
            'MAIN.QUITQBSESSION()
            Throw ex
        End Try
    End Function

    Public Function AddTimeEntryInQBItemSubItem(ByVal CustomerName As String, ByVal EmployeeName As String, ByVal IsBillable As Boolean,
                                           ByVal ProjectName As String, ByVal ServiceItemName As String, ByVal TotalTime As DateTime,
                                           ByVal TimeEntryDate As Date, ByVal TimeEntryClass As String, ByVal PayrollItem_TypeName As Boolean,
                                         ByVal PayrollItem As String, ByVal ServiceItem_TypeName As Boolean, ByVal ItemID As String) As String
        'step1: prepare the request
        Dim msgSetRs As IMsgSetResponse
        Try
            'ProjectName = SetLength(ProjectName)
            Dim msgSetRq As IMsgSetRequest = MAIN.SESSMANAGER.CreateMsgSetRequest("US", 2, 0)
            msgSetRq.Attributes.OnError = ENRqOnError.roeContinue
            Dim timeAdd As ITimeTrackingAdd = msgSetRq.AppendTimeTrackingAddRq
            timeAdd.CustomerRef.FullName.SetValue(CustomerName & ":" & ProjectName & ":" & ServiceItemName) ' CustomerName
            timeAdd.Duration.SetValue(TotalTime.Hour, TotalTime.Minute, 0, False)
            timeAdd.EntityRef.FullName.SetValue(EmployeeName)
            timeAdd.IsBillable.SetValue(IsBillable)
            timeAdd.ItemServiceRef.FullName.SetValue(ProjectName & ":" & ServiceItemName) ' ProjectName & ":" & ServiceItemName
            timeAdd.TxnDate.SetValue(TimeEntryDate)
            If Not TimeEntryClass = "<None>" Then
                timeAdd.ClassRef.FullName.SetValue(TimeEntryClass)
            End If
            If Not PayrollItem = "<None>" Then
                If PayrollItem_TypeName Then
                    timeAdd.PayrollItemWageRef.FullName.SetValue(PayrollItem)
                Else
                    timeAdd.PayrollItemWageRef.ListID.SetValue(PayrollItem.ToString.Trim)
                End If
            End If

            'step2: send the request
            msgSetRs = MAIN.SESSMANAGER.DoRequests(msgSetRq)

            ' Interpret the response
            Dim response As IResponse
            response = msgSetRs.ResponseList.GetAt(0)

            If response.StatusSeverity = "Error" Then
                Throw New Exception(response.StatusMessage)
            End If

            ' The response detail for Add and Mod requests is a 'Ret' object
            ' In our case, it's ICustomerRet
            Dim TimeEntryRet As ITimeTrackingRet
            TimeEntryRet = response.Detail

            ' Make sure a customerRet was returned before trying to obtain
            Return TimeEntryRet.TxnID.GetValue.ToString

        Catch ex As Exception
            'MAIN.QUITQBSESSION()
            Throw ex
        End Try
    End Function

    Public Function AddTimeEntryInQBJobSubJob(ByVal CustomerName As String, ByVal EmployeeName As String, ByVal IsBillable As Boolean,
                                         ByVal ProjectName As String, ByVal ServiceItemName As String, ByVal TotalTime As DateTime,
                                         ByVal TimeEntryDate As Date, ByVal TimeEntryClass As String, ByVal PayrollItem_TypeName As Boolean,
                                         ByVal PayrollItem As String, ByVal ServiceItem_TypeName As Boolean, ByVal ItemID As String, Optional UI As Boolean = False) As String
        'step1: prepare the request
        Dim msgSetRs As IMsgSetResponse
        Try
            'ProjectName = SetLength(ProjectName)
            Dim msgSetRq As IMsgSetRequest = MAIN.SESSMANAGER.CreateMsgSetRequest("US", 2, 0)
            msgSetRq.Attributes.OnError = ENRqOnError.roeContinue
            Dim timeAdd As ITimeTrackingAdd = msgSetRq.AppendTimeTrackingAddRq

            AddNoneItemInQB("<None>", "<None>")

            ' If Project name contains a semicolon, then it also has the task within it, and ServiceItemName is actually a Service Item
            If ProjectName.Contains(":") Then
                timeAdd.CustomerRef.FullName.SetValue(CustomerName & ":" & ProjectName) ' ProjectName contains the task name as well
                timeAdd.ItemServiceRef.FullName.SetValue(CustomerName & ":" & ProjectName.Split(":")(0) & ":" & ServiceItemName) ' ServiceItemName is the Item
            Else
                timeAdd.CustomerRef.FullName.SetValue(CustomerName & ":" & ProjectName & ":" & ServiceItemName)
                If ServiceItem_TypeName Then
                    timeAdd.ItemServiceRef.FullName.SetValue(ItemID)
                Else
                    timeAdd.ItemServiceRef.ListID.SetValue(ItemID.ToString.Trim)
                End If
            End If

            timeAdd.Duration.SetValue(TotalTime.Hour, TotalTime.Minute, 0, False)
            timeAdd.EntityRef.FullName.SetValue(EmployeeName)
            timeAdd.IsBillable.SetValue(IsBillable)
            timeAdd.TxnDate.SetValue(TimeEntryDate)

            If Not TimeEntryClass = "<None>" Then
                timeAdd.ClassRef.FullName.SetValue(TimeEntryClass)
            End If

            My.Forms.MAIN.History("Payroll Item for time entry: " + PayrollItem, "n") ' TODO: Can delete this when bug is resolved
            If PayrollItem <> "<None>" Then
                If PayrollItem_TypeName Then
                    timeAdd.PayrollItemWageRef.FullName.SetValue(PayrollItem)
                Else
                    timeAdd.PayrollItemWageRef.ListID.SetValue(PayrollItem.ToString.Trim)
                End If
            End If

            'step2: send the request
            msgSetRs = MAIN.SESSMANAGER.DoRequests(msgSetRq)
            ' Interpret the response
            Dim response As IResponse
            response = msgSetRs.ResponseList.GetAt(0)

            If response.StatusSeverity = "Error" Then
                If UI Then
                    MsgBox(response.StatusMessage, MsgBoxStyle.OkOnly, "Error Processing Time Transfer")
                Else
                    Throw New Exception(response.StatusMessage)
                End If
            End If

            ' The response detail for Add and Mod requests is a 'Ret' object
            ' In our case, it's ICustomerRet
            Dim TimeEntryRet As ITimeTrackingRet
            TimeEntryRet = response.Detail

            ' Make sure a customerRet was returned before trying to obtain
            Return TimeEntryRet.TxnID.GetValue.ToString
        Catch ex As Exception
            'MAIN.QUITQBSESSION()
            Throw ex
        End Try
    End Function

    Public Sub AddNoneItemInQB(ByVal ItemName As String, ByVal ServiceItemAccount As String)
        'step1: prepare the request
        Dim msgSetRs As IMsgSetResponse
        Try
            'ItemName = SetLength(ItemName)
            Dim msgSetRq As IMsgSetRequest = MAIN.SESSMANAGER.CreateMsgSetRequest("US", 2, 0)
            msgSetRq.Attributes.OnError = ENRqOnError.roeContinue
            Dim AccountAdd As IAccountAdd = msgSetRq.AppendAccountAddRq
            AccountAdd.Name.SetValue(ServiceItemAccount)
            AccountAdd.AccountType.SetValue(ENAccountType.atExpense)
            Dim ItemAdd As IItemServiceAdd = msgSetRq.AppendItemServiceAddRq
            ItemAdd.Name.SetValue(ItemName)
            ItemAdd.ORSalesPurchase.SalesOrPurchase.AccountRef.FullName.SetValue(ServiceItemAccount)
            'step2: send the request
            msgSetRs = MAIN.SESSMANAGER.DoRequests(msgSetRq)
            'If msgSetRs.ResponseList.GetAt(0).StatusSeverity = "Error" Then
            '    Throw New Exception(msgSetRs.ResponseList.GetAt(0).StatusMessage)
            'End If
        Catch ex As Exception
            'MAIN.QUITQBSESSION()
            Throw ex
        End Try
    End Sub
    Public Function SetLength(ByVal Name As String) As String
        Return Name.Substring(0, Math.Min(Name.Length, 32))
    End Function

    Public Function GetClass(ByVal objTimeEntry As Services.TimeLive.TimeEntries.TimeEntry) As String
        Dim TimeEntryClass As String = "<None>"
        My.Forms.MAIN.History("Class: " + My.Settings.QBClass, "n")

        With objTimeEntry
            Dim QBSelectedClass_AppSettings As Integer = My.Settings.QBClass
            If QBSelectedClass_AppSettings = 1 Then
                Return .CostCenter
            ElseIf QBSelectedClass_AppSettings = 2 Then
                Return .EmployeeDepartment
            ElseIf QBSelectedClass_AppSettings = 3 Then
                Return .EmployeeType
            ElseIf QBSelectedClass_AppSettings = 4 Then
                Return .Milestone
            ElseIf QBSelectedClass_AppSettings = 5 Then
                Return .WorkType
            End If
        End With
        Return TimeEntryClass
    End Function

    Public Function GetPayrollItem(ByVal objTimeEntry As Services.TimeLive.TimeEntries.TimeEntry) As String
        Dim PayrollItem As String = "<None>"

        ' not sure code below works as options are not the payroll type options
        If Not My.Settings.QBPayrollItem = "" Then
            With objTimeEntry
                Dim QBPayrollItem_AppSettings As Integer = My.Settings.QBPayrollItem

                If QBPayrollItem_AppSettings = 1 Then
                    PayrollItem = .CostCenter
                ElseIf QBPayrollItem_AppSettings = 2 Then
                    PayrollItem = .EmployeeDepartment
                ElseIf QBPayrollItem_AppSettings = 3 Then
                    PayrollItem = .EmployeeType
                ElseIf QBPayrollItem_AppSettings = 4 Then
                    PayrollItem = .Milestone
                ElseIf QBPayrollItem_AppSettings = 5 Then
                    PayrollItem = .WorkType
                End If

                'Query QuickBooks to see if it has this payroll item
                If PayrollItem <> "<None>" Then
                    Try
                        Dim msgSetRq As IMsgSetRequest = MAIN.SESSMANAGER.CreateMsgSetRequest("US", 2, 0)
                        msgSetRq.Attributes.OnError = ENRqOnError.roeContinue
                        Dim PayrollQueryRq As IPayrollItemWageQuery = msgSetRq.AppendPayrollItemWageQueryRq
                        PayrollQueryRq.ORListQuery.FullNameList.Add(PayrollItem)
                        Dim msgSetRs As IMsgSetResponse = MAIN.SESSMANAGER.DoRequests(msgSetRq)
                        Dim response As IResponse = msgSetRs.ResponseList.GetAt(0)
                        Dim empRetList As IPayrollItemWageRet = response.Detail

                        If empRetList Is Nothing Then ' When QB does not have that payroll item
                            PayrollItem = "<None>"
                        End If
                    Catch
                        PayrollItem = "<None>"
                    End Try

                End If
            End With
        End If


        Return PayrollItem
    End Function

    'Public Function getItemSubItem()
    'Dim ItemsSubItemsQBData As New DataTable
    '   ItemsSubItemsQBData = ChargingRelationship.QBItemsSubItems()
    '   Return " "
    'End Function


    'Add class to Quickbooks if it does not exist (should be prevented)
    Public Sub AddClass(ByVal ClassName As String)
        'step1: prepare the request
        Dim msgSetRs As IMsgSetResponse
        Try
            Dim msgSetRq As IMsgSetRequest = MAIN.SESSMANAGER.CreateMsgSetRequest("US", 2, 0)
            msgSetRq.Attributes.OnError = ENRqOnError.roeContinue
            Dim ClassAdd As IClassAdd = msgSetRq.AppendClassAddRq
            ClassAdd.Name.SetValue(ClassName)

            'step2: send the request
            msgSetRs = MAIN.SESSMANAGER.DoRequests(msgSetRq)
            If (Not msgSetRs.ResponseList.GetAt(0).StatusMessage.Contains("already in use")) And
               msgSetRs.ResponseList.GetAt(0).StatusSeverity = "Error" Then
                Throw New Exception(msgSetRs.ResponseList.GetAt(0).StatusMessage)
            End If
        Catch ex As Exception
            'MAIN.QUITQBSESSION()
            Throw ex
        End Try
    End Sub

    'Add payroll item to Quickbooks if it does not exist (should be prevented)
    Public Sub AddPayrollItem(ByVal PayrollItem As String)
        'step1: prepare the request
        Dim msgSetRs As IMsgSetResponse
        Try
            Dim msgSetRq As IMsgSetRequest = MAIN.SESSMANAGER.CreateMsgSetRequest("US", 2, 0)
            msgSetRq.Attributes.OnError = ENRqOnError.roeContinue
            Dim PayrollItemAdd As IPayrollItemWageAdd = msgSetRq.AppendPayrollItemWageAddRq
            PayrollItemAdd.Name.SetValue(PayrollItem)
            PayrollItemAdd.ExpenseAccountRef.FullName.SetValue("Payroll Expenses")
            PayrollItemAdd.WageType.SetValue(GetWageType(My.Forms.MAIN.cbWageType.SelectedItem))

            'step2: send the request
            msgSetRs = MAIN.SESSMANAGER.DoRequests(msgSetRq)
            If Not msgSetRs.ResponseList.GetAt(0).StatusMessage.Contains("already in use") Then
                If msgSetRs.ResponseList.GetAt(0).StatusSeverity = "Error" Then
                    Throw New Exception(msgSetRs.ResponseList.GetAt(0).StatusMessage)
                End If
            End If
        Catch ex As Exception
            'MAIN.QUITQBSESSION()
            Throw ex
        End Try
    End Sub

    Public Function GetWageType(ByVal val As String) As ENWageType
        If val = "Bonus" Then
            Return ENWageType.wtBonus
        ElseIf val = "Comission" Then
            Return ENWageType.wtCommission
        ElseIf val = "Hourly" Then
            Return ENWageType.wtHourly
        ElseIf val = "Hourly-Overtime" Then
            Return ENWageType.wtHourlyOvertime
        ElseIf val = "Hourly-Regular" Then
            Return ENWageType.wtHourlyRegular
        ElseIf val = "Hourly-Sick" Then
            Return ENWageType.wtHourlySick
        ElseIf val = "Hourly-Vacation" Then
            Return ENWageType.wtHourlyVacation
        ElseIf val = "Salary" Then
            Return ENWageType.wtSalary
        ElseIf val = "Salary-Regular" Then
            Return ENWageType.wtSalaryRegular
        ElseIf val = "Salary-Sick" Then
            Return ENWageType.wtSalarySick
        ElseIf val = "Salary-Vacation" Then
            Return ENWageType.wtSalaryVacation
        End If
    End Function

End Class
