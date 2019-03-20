Imports QBFC11Lib
Imports System.Configuration
Imports System.Collections.Generic

Public Class ChargingRelationship
    Public Class Job_Subjob
        Private _QBName As String = String.Empty
        Private _QBID As String = String.Empty

        Public Sub New(ByVal QBName As String, ByVal QBID As String)
            Me._QBName = QBName
            Me._QBID = QBID
        End Sub
    End Class

    Private Sub ChargingRelationship_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Dim EmployeesQBData As New DataTable
        Dim JobsSubJobsQBData As New DataTable
        Dim ItemsSubItemsQBData As New DataTable
        Dim PayrollItemsQBData As New DataTable

        Me.EmployeesTableAdapter.Fill(Me.QB_TL_IDs.Employees)
        Me.Jobs_SubJobsTableAdapter.Fill(Me.QB_TL_IDs.Jobs_SubJobs) 'Maybe do this?

        JobsSubJobsQBData = QBJobsSubJobs()
        EmployeesQBData = QBEmployees()
        PayrollItemsQBData = QBPayrollItems()
        ItemsSubItemsQBData = QBItemsSubItems()

        ' Add all Employees to Employee Filter Box
        For Each employee As DataRow In EmployeesQBData.Rows
            EmployeeFilterBox.Items.Add(employee(0))
        Next

        ' Add all Jobs/Subjobs to Job Filter Box
        For Each job As DataRow In JobsSubJobsQBData.Rows
            JobFilterBox.Items.Add(job(0))
        Next

        ' Add all Payroll Items to Payroll Filter Box
        For Each payrollItem As DataRow In PayrollItemsQBData.Rows
            PayrollFilterBox.Items.Add(payrollItem(0))
        Next

        ' Add all Items/SubItems to Item Filter Box
        For Each item As DataRow In ItemsSubItemsQBData.Rows
            ItemFilterBox.Items.Add(item(0))
        Next

        ' Add everything into Data Grid
        With DataGridView1
            Dim ColumnEmployees As New DataGridViewComboBoxColumn

            With ColumnEmployees
                .DataPropertyName = "EmployeeQB_ID"
                .HeaderText = "Employees"
                .Width = 100
                .DataSource = EmployeesQBData
                .DisplayMember = "QB_Name"
                .ValueMember = "QB_ID"
            End With

            .Columns.Remove(DataGridView1.Columns(1))
            .Columns.Insert(1, ColumnEmployees)

            Dim ColumnJ_SJ As New DataGridViewComboBoxColumn

            With ColumnJ_SJ
                .DataPropertyName = "JobSubJobQB_ID"
                .HeaderText = "Jobs/SubJobs"
                .Width = 200
                .DataSource = JobsSubJobsQBData
                .DisplayMember = "QB_Name"
                .ValueMember = "QB_ID"
            End With

            .Columns.Remove(DataGridView1.Columns(2))
            .Columns.Insert(2, ColumnJ_SJ)

            Dim ColumnPayrollItem As New DataGridViewComboBoxColumn

            With ColumnPayrollItem
                .DataPropertyName = "PayrollItemQB_ID"
                .HeaderText = "Payroll Item"
                .Width = 100
                .DataSource = PayrollItemsQBData
                .DisplayMember = "QB_Name"
                .ValueMember = "QB_ID"
            End With

            .Columns.Remove(DataGridView1.Columns(3))
            .Columns.Insert(3, ColumnPayrollItem)

            Dim ColumnI_SI As New DataGridViewComboBoxColumn

            With ColumnI_SI
                .DataPropertyName = "ItemSubItemQB_ID"
                .HeaderText = "Items/SubItems"
                .Width = 200
                .DataSource = ItemsSubItemsQBData
                .DisplayMember = "QB_Name"
                .ValueMember = "QB_ID"
            End With

            .Columns.Remove(DataGridView1.Columns(4))
            .Columns.Insert(4, ColumnI_SI)
        End With

        ' Fill with all charging relationships
        Me.ChargingRelationshipsTableAdapter.Fill(Me.QB_TL_IDs.ChargingRelationships)

        ' Remove relationships for all non-active employees
        Dim numEmployees As Integer = 0
        While numEmployees < Me.QB_TL_IDs.ChargingRelationships.Count
            Dim row As DataRow = Me.QB_TL_IDs.ChargingRelationships.Rows(numEmployees)
            Dim remove As Boolean = True
            ' Remove the row if name was not chosen
            If IsDBNull(row(1)) Then
                Me.QB_TL_IDs.ChargingRelationships.RemoveChargingRelationshipsRow(row)
                Continue While
            End If
            Dim employeeId As String = row(1).Trim

            ' Compares the employee in relationship table to active employees
            For Each EmployeeRow As DataRow In EmployeesQBData.Select
                Dim employeeId2 As String = EmployeeRow(1).Trim
                If employeeId2 = employeeId Then
                    remove = False ' employee is active
                    Exit For
                End If
            Next

            ' Remove employee if they are inactive, otherwise increment numEmployees
            If remove Then
                Me.QB_TL_IDs.ChargingRelationships.RemoveChargingRelationshipsRow(row)
            Else
                numEmployees += 1
            End If
        End While

        DataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells
        DataGridView1.AutoResizeColumns()
        Me.Show()
    End Sub

    Private Sub btn_Save_Click(sender As Object, e As EventArgs) Handles btn_Save.Click
        Try
            Me.ChargingRelationshipsTableAdapter.Update(Me.QB_TL_IDs.ChargingRelationships)
            DataGridView1.Refresh()
        Catch ex As Exception
            My.Forms.MAIN.History(ex.ToString, "C")
            ' Before throwing exception, close the session manager if it is open
            MAIN.QUITQBSESSION()
            Throw ex
        End Try
    End Sub

    Private Sub btn_Delete_Click(sender As Object, e As EventArgs) Handles btn_Delete.Click
        Try
            Me.DataGridView1.Rows.RemoveAt(DataGridView1.CurrentRow.Index)
            Me.ChargingRelationshipsTableAdapter.Update(Me.QB_TL_IDs.ChargingRelationships)
            DataGridView1.Refresh()
        Catch ex As Exception
            My.Forms.MAIN.History(ex.ToString, "C")
            ' Before throwing exception, close the session manager if it is open
            MAIN.QUITQBSESSION()
            Throw ex
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles btn_close.Click
        Me.Close()
    End Sub

    Private Function QBEmployees() As DataTable

        Dim EmployeesQBData As New DataTable

        ' Create four typed columns in the DataTable.
        EmployeesQBData.Columns.Add("QB_Name", GetType(String))
        EmployeesQBData.Columns.Add("QB_ID", GetType(String))

        'step1: create QBFC session manager and prepare the request
        'Dim sessManager As QBSessionManager
        Dim msgSetRs As IMsgSetResponse
        Try
            'sessManager = New QBSessionManagerClass()
            Dim msgSetRq As IMsgSetRequest = MAIN.SESSMANAGER.CreateMsgSetRequest("US", 2, 0)
            msgSetRq.Attributes.OnError = ENRqOnError.roeContinue
            ' Customer Query 
            Dim employeequery As IEmployeeQuery = msgSetRq.AppendEmployeeQueryRq

            'step2: begin QB session and send the request
            'sessManager.OpenConnection("App", "TimeLive Quickbooks")
            'sessManager.BeginSession("", ENOpenMode.omDontCare)
            msgSetRs = MAIN.SESSMANAGER.DoRequests(msgSetRq)
            Dim respList As IResponseList
            respList = msgSetRs.ResponseList
            If respList Is Nothing Then
                Return Nothing
            End If
            ' Should only expect 1 response
            Dim resp As IResponse
            resp = respList.GetAt(0)
            If (resp.StatusCode = 0) Then
                Dim empRetList As IEmployeeRetList
                empRetList = resp.Detail

                Dim empRet As IEmployeeRet
                For i As Integer = 0 To empRetList.Count - 1
                    empRet = empRetList.GetAt(i)
                    With empRet
                        EmployeesQBData.Rows.Add(.Name.GetValue, .ListID.GetValue)
                    End With
                Next
            End If
            If msgSetRs.ResponseList.GetAt(0).StatusSeverity = "Error" Then
                My.Forms.MAIN.History(msgSetRs.ResponseList.GetAt(0).StatusMessage, "C")
                Throw New Exception(msgSetRs.ResponseList.GetAt(0).StatusMessage)
            End If
        Catch ex As Exception
            My.Forms.MAIN.History(ex.ToString, "C")
            ' Before throwing exception, close the session manager if it is open
            MAIN.QUITQBSESSION()
            Throw ex
            'Finally
            '   If Not sessManager Is Nothing Then
            '       sessManager.EndSession()
            '       sessManager.CloseConnection()
            '   End If
        End Try
        Return EmployeesQBData
    End Function

    Private Function QBJobsSubJobs() As DataTable

        Dim JobsSubJobsQBData As New DataTable

        ' Create four typed columns in the DataTable.
        JobsSubJobsQBData.Columns.Add("QB_Name", GetType(String))
        JobsSubJobsQBData.Columns.Add("QB_ID", GetType(String))

        'step1: create QBFC session manager and prepare the request
        'Dim sessManager As QBSessionManager
        Dim msgSetRs As IMsgSetResponse
        Try
            'sessManager = New QBSessionManagerClass()
            Dim msgSetRq As IMsgSetRequest = MAIN.SESSMANAGER.CreateMsgSetRequest("US", 2, 0)
            msgSetRq.Attributes.OnError = ENRqOnError.roeContinue
            ' Customer Query 
            Dim synccust As ICustomerQuery = msgSetRq.AppendCustomerQueryRq

            'step2: begin QB session and send the request
            'sessManager.OpenConnection("App", "TimeLive Quickbooks")
            'sessManager.BeginSession("", ENOpenMode.omDontCare)
            msgSetRs = MAIN.SESSMANAGER.DoRequests(msgSetRq)
            Dim respList As IResponseList
            respList = msgSetRs.ResponseList
            If (respList Is Nothing) Then
                Return Nothing
            End If
            ' Should only expect 1 response
            Dim resp As IResponse
            resp = respList.GetAt(0)
            If (resp.StatusCode = 0) Then
                Dim ptRetList As ICustomerRetList
                ptRetList = resp.Detail

                Dim ptRet As ICustomerRet
                For i As Integer = 0 To ptRetList.Count - 1
                    ptRet = ptRetList.GetAt(i)
                    With ptRet
                        If Not .ParentRef Is Nothing Then
                            JobsSubJobsQBData.Rows.Add(.FullName.GetValue, .ListID.GetValue)
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
            ' Before throwing exception, close the session manager if it is open
            MAIN.QUITQBSESSION()
            Throw ex
            'Finally
            '   If Not sessManager Is Nothing Then
            '       sessManager.EndSession()
            '       sessManager.CloseConnection()
            '   End If
        End Try
        Return JobsSubJobsQBData
    End Function

    ' Changed to public since used in TLtoQB_TimeEntry
    Private Function QBItemsSubItems() As DataTable

        Dim ItemsSubItemsQBData As New DataTable

        ' Create four typed columns in the DataTable.
        ItemsSubItemsQBData.Columns.Add("QB_Name", GetType(String))
        ItemsSubItemsQBData.Columns.Add("QB_ID", GetType(String))

        'step1: create QBFC session manager and prepare the request
        'Dim sessManager As QBSessionManager
        Dim msgSetRs As IMsgSetResponse
        Try
            'sessManager = New QBSessionManagerClass()
            ' Specify spec version (2)
            Dim msgSetRq As IMsgSetRequest = MAIN.SESSMANAGER.CreateMsgSetRequest("US", 2, 0)
            msgSetRq.Attributes.OnError = ENRqOnError.roeContinue
            ' Customer Query 
            Dim itemservicequery As IItemServiceQuery = msgSetRq.AppendItemServiceQueryRq

            'step2: begin QB session and send the request
            'sessManager.OpenConnection("App", "TimeLive Quickbooks")
            'sessManager.BeginSession("", ENOpenMode.omDontCare)
            msgSetRs = MAIN.SESSMANAGER.DoRequests(msgSetRq)
            Dim respList As IResponseList
            respList = msgSetRs.ResponseList
            If (respList Is Nothing) Then
                Return Nothing
            End If
            ' Should only expect 1 response
            Dim resp As IResponse
            resp = respList.GetAt(0)
            If (resp.StatusCode = 0) Then
                Dim ptRetList As IItemServiceRetList
                ptRetList = resp.Detail

                Dim ptRet As IItemServiceRet
                For i As Integer = 0 To ptRetList.Count - 1
                    ptRet = ptRetList.GetAt(i)
                    With ptRet
                        If Not .ParentRef Is Nothing Then
                            ItemsSubItemsQBData.Rows.Add(.FullName.GetValue, .ListID.GetValue) 'Exception: Not Found
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
            ' Before throwing exception, close the session manager if it is open
            MAIN.QUITQBSESSION()
            Throw ex
            'Finally
            '    If Not sessManager Is Nothing Then
            '       sessManager.EndSession()
            '       sessManager.CloseConnection()
            '   End If
        End Try
        Return ItemsSubItemsQBData
    End Function

    Private Function QBPayrollItems() As DataTable

        Dim PayrollItemsQBData As New DataTable

        ' Create four typed columns in the DataTable.
        PayrollItemsQBData.Columns.Add("QB_Name", GetType(String))
        PayrollItemsQBData.Columns.Add("QB_ID", GetType(String))

        'step1: create QBFC session manager and prepare the request
        'Dim sessManager As QBSessionManager
        Dim msgSetRs As IMsgSetResponse
        Try
            'sessManager = New QBSessionManagerClass()
            Dim msgSetRq As IMsgSetRequest = MAIN.SESSMANAGER.CreateMsgSetRequest("US", 2, 0)
            msgSetRq.Attributes.OnError = ENRqOnError.roeContinue
            ' Customer Query 
            Dim PayRollItemWuery As IPayrollItemWageQuery = msgSetRq.AppendPayrollItemWageQueryRq

            'step2: begin QB session and send the request
            'sessManager.OpenConnection("App", "TimeLive Quickbooks")
            'sessManager.BeginSession("", ENOpenMode.omDontCare)
            msgSetRs = MAIN.SESSMANAGER.DoRequests(msgSetRq)
            Dim respList As IResponseList
            respList = msgSetRs.ResponseList
            If (respList Is Nothing) Then
                Return Nothing
            End If
            ' Should only expect 1 response
            Dim resp As IResponse
            resp = respList.GetAt(0)
            If (resp.StatusCode = 0) Then
                Dim ParyRollRetList As IPayrollItemWageRetList
                ParyRollRetList = resp.Detail
                Dim ParyRollRet As IPayrollItemWageRet
                For i As Integer = 0 To ParyRollRetList.Count - 1
                    ParyRollRet = ParyRollRetList.GetAt(i)
                    With ParyRollRet
                        PayrollItemsQBData.Rows.Add(.Name.GetValue, .ListID.GetValue)
                    End With
                Next
            End If
            If msgSetRs.ResponseList.GetAt(0).StatusSeverity = "Error" Then
                My.Forms.MAIN.History(msgSetRs.ResponseList.GetAt(0).StatusMessage, "C")
                Throw New Exception(msgSetRs.ResponseList.GetAt(0).StatusMessage)
            End If
        Catch ex As Exception
            My.Forms.MAIN.History(ex.ToString, "C")
            ' Before throwing exception, close the session manager if it is open
            MAIN.QUITQBSESSION()
            Throw ex
            'Finally
            '    If Not sessManager Is Nothing Then
            '       sessManager.EndSession()
            '       sessManager.CloseConnection()
            '    End If
        End Try
        Return PayrollItemsQBData
    End Function

    ' Sort by selected column
    '                      ID, Employee, Job, Payroll, Item
    Dim ascend(5) As Integer '0 is ascending, 1 is descending
    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        ' If header clicked, sort by that column
        If e.RowIndex = -1 Then
            DataGridView1.Sort(DataGridView1.Columns(e.ColumnIndex), ascend(e.ColumnIndex))
        End If
        ascend(e.ColumnIndex) = If(ascend(e.ColumnIndex), 0, 1)
    End Sub

    ' Filters the shown relationships based on the selected attributes
    Private Sub Filter_SelectedIndexChanged(sender As Object, e As EventArgs) Handles EmployeeFilterBox.SelectedIndexChanged,
                                                                                      JobFilterBox.SelectedIndexChanged,
                                                                                      PayrollFilterBox.SelectedIndexChanged,
                                                                                      ItemFilterBox.SelectedIndexChanged
        Dim EmployeeQuery As String = EmployeeFilter()
        Dim JobQuery As String = JobFilter()
        Dim PayrollQuery As String = PayrollFilter()
        Dim ItemQuery As String = ItemFilter()

        Dim search As String = EmployeeQuery +
                               If(EmployeeQuery.Length And JobQuery.Length, " AND ", "") + JobQuery +
                               If((EmployeeQuery.Length Or JobQuery.Length) And PayrollQuery.Length, " AND ", "") + PayrollQuery +
                               If((EmployeeQuery.Length Or JobQuery.Length Or PayrollQuery.Length) And ItemQuery.Length, " AND ", "") + ItemQuery
        Dim view As DataView = New DataView(Me.QB_TL_IDs.ChargingRelationships, search, "ID Desc", DataViewRowState.CurrentRows)
        DataGridView1.DataSource = view
    End Sub

    Private Function EmployeeFilter()
        Dim name As String = If(EmployeeFilterBox.SelectedItem Is Nothing, "", EmployeeFilterBox.SelectedItem) ' Empty string if no selectedItem
        Dim search As String = ""
        ' Only check if name is not the empty string
        If name.Length Then
            Dim Employee_ID As String = Me.EmployeesTableAdapter.Name_to_ID(name)

            ' Check if DB stores name as "Last, First" instead of "First Last"
            If Employee_ID Is Nothing Then
                Dim space_index = name.IndexOf(" ")
                Dim firstName = name.Substring(0, space_index)
                Dim lastName = name.Substring(space_index + 1)
                name = lastName.Trim + ", " + firstName
                Employee_ID = Me.EmployeesTableAdapter.Name_to_ID(name)
            End If

            ' Only update table if Employee ID was found in DB
            If Not Employee_ID Is Nothing Then
                search = "EmployeeQB_ID = '" + Employee_ID.Trim + "'"
            End If
        End If
        Return search
        'JobFilterBox_SelectedIndexChanged(sender, e)
    End Function

    Private Function JobFilter()
        Dim name As String = If(JobFilterBox.SelectedItem Is Nothing, "", JobFilterBox.SelectedItem)
        Dim search As String = ""
        ' Only check if name is not the empty string
        If name.Length Then
            Dim Job_ID As String = Me.Jobs_SubJobsTableAdapter.Name_to_ID(name)

            ' Check if DB stores as "job_name"/"subjob_name", not "customer_name:job_name"/"customer_name:job_name:subjob_name"
            If Job_ID Is Nothing Then
                Dim last_colon = name.LastIndexOf(":")
                name = name.Substring(last_colon + 1)
                Job_ID = Me.Jobs_SubJobsTableAdapter.Name_to_ID(name)
            End If

            ' Only update table if Employee ID was found in DB
            If Not Job_ID Is Nothing Then
                search = "JobSubJobQB_ID = '" + Job_ID.Trim + "'"
            End If
        End If
        Return search
    End Function

    Private Function PayrollFilter()
        Dim name As String = If(PayrollFilterBox.SelectedItem Is Nothing, "", PayrollFilterBox.SelectedItem)
        Dim search As String = ""
        ' Only check if name is not the empty string
        If name.Length Then
            Dim Payroll_ID As String = Nothing
            Dim PayrollDT As DataTable = QBPayrollItems()
            For Each row As DataRow In PayrollDT.Rows
                If name = row(0).trim Or name = row(1).Trim Then
                    Payroll_ID = row(1).Trim
                    Exit For
                End If
            Next

            ' Only update table if Employee ID was found in DB
            If Not Payroll_ID Is Nothing Then
                search = "PayrollItemQB_ID = '" + Payroll_ID + "'"
            End If
        End If
        Return search
    End Function

    Private Function ItemFilter()
        Dim name As String = If(ItemFilterBox.SelectedItem Is Nothing, "", ItemFilterBox.SelectedItem)
        Dim search As String = ""
        ' Only check if name is not the empty string
        If name.Length Then
            Dim Item_ID As String = Me.Items_SubItemsTableAdapter.Name_to_ID(name)

            ' Check if DB stores as "subitem_name", not "service_name:subitem_name"
            If Item_ID Is Nothing Then
                Dim last_colon = name.LastIndexOf(":")
                name = name.Substring(last_colon + 1)
                Item_ID = Me.Items_SubItemsTableAdapter.Name_to_ID(name)
            End If

            ' Only update table if Employee ID was found in DB
            If Not Item_ID Is Nothing Then
                search = "ItemSubItemQB_ID = '" + Item_ID.Trim + "'"
            End If
        End If
        Return search
    End Function

End Class

