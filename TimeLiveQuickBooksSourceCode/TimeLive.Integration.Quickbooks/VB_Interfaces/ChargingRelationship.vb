﻿Imports QBFC13Lib
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
        Dim VendorsQBData As New DataTable
        Dim JobsSubJobsQBData As New DataTable
        Dim ItemsSubItemsQBData As New DataTable
        Dim PayrollItemsQBData As New DataTable

        Me.EmployeesTableAdapter.Fill(Me.QB_TL_IDs.Employees)
        Me.Jobs_SubJobsTableAdapter.Fill(Me.QB_TL_IDs.Jobs_SubJobs) ' Maybe do this?
        Me.VendorsTableAdapter.Fill(Me.QB_TL_IDs.Vendors) ' Maybe do this?
        ' Null since TL -> QB relationship sync does not fill items_subitems
        'Me.Items_SubItemsTableAdapter.Fill(Me.QB_TL_IDs.Items_SubItems) ' Should this be added?

        JobsSubJobsQBData = QBJobsSubJobs()
        EmployeesQBData = QBEmployees()
        VendorsQBData = QB1099Vendors()
        PayrollItemsQBData = QBPayrollItems()
        ItemsSubItemsQBData = QBItemsSubItems()

        ' Add all Employees to Employee Filter Box
        For Each employee As DataRow In EmployeesQBData.Rows
            EmployeeFilterBox.Items.Add(employee(0))
        Next

        ' Add all 1099 Vendors to Employee Filter Box and to Employee Data
        For Each vendor As DataRow In VendorsQBData.Rows
            EmployeesQBData.ImportRow(vendor)
            EmployeeFilterBox.Items.Add(vendor(0))
        Next

        ' Add all Jobs/Subjobs to Job Filter Box
        For Each job As DataRow In JobsSubJobsQBData.Rows
            Dim numSubJobs As Integer = Me.Jobs_SubJobsTableAdapter.numSubTaskswithParent(job(0))

            If numSubJobs = 0 Then
                JobFilterBox.Items.Add(job(0).replace(":", MAIN.colonReplacer))
            End If
        Next

        ' Add all Payroll Items to Payroll Filter Box
        For Each payrollItem As DataRow In PayrollItemsQBData.Rows
            PayrollFilterBox.Items.Add(payrollItem(0))
        Next

        ' Add all Items/SubItems to Item Filter Box
        For Each item As DataRow In ItemsSubItemsQBData.Rows
            ItemFilterBox.Items.Add(item(0))
        Next

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

        ' Fill Data Grid with all charging relationships
        Me.ChargingRelationshipsTableAdapter.Fill(Me.QB_TL_IDs.ChargingRelationships)

        ' Remove all relationships from Data Grid where one or more of the entities is inactive
        Dim numRow As Integer = 0
        While numRow < Me.QB_TL_IDs.ChargingRelationships.Count
            Dim row As DataRow = Me.QB_TL_IDs.ChargingRelationships.Rows(numRow)
            Dim remove As Boolean = True

            ' Remove the row if Employee, Subjob, or Payroll attributes are not chosen (are null)
            If IsDBNull(row(1)) Or IsDBNull(row(2)) Then ' Or IsDBNull(row(3)) 'Or IsDBNull(row(4))
                Me.QB_TL_IDs.ChargingRelationships.RemoveChargingRelationshipsRow(row)
                Continue While
            End If

            ' check if the employee/vendor is active
            Dim employeeId As String = row(1).Trim
            ' Compares the employee/vendor in relationship table to active employees/vendors
            Dim emp As DataRow() = EmployeesQBData.Select
            For Each EmployeeRow As DataRow In EmployeesQBData.Select
                Dim employeeId2 As String = EmployeeRow(1).Trim
                If employeeId2 = employeeId Then
                    remove = False ' employee is active
                    Exit For
                End If
            Next

            ' Check if the job is active
            If Not remove Then
                remove = True
                Dim jobID As String = row(2).Trim
                ' Compares the job in relationship table to active jobs
                For Each JobRow As DataRow In JobsSubJobsQBData.Select
                    Dim jobId2 As String = JobRow(1).Trim
                    If jobId2 = jobID Then
                        remove = False ' job is active
                        Exit For
                    End If
                Next
            End If

            ' Check if the payroll item is active
            If Not remove And Not IsDBNull(row(3)) Then
                remove = True
                Dim payrollID As String = row(3).Trim
                ' Compares the payroll item in relationship table to active payroll items
                For Each PayrollRow As DataRow In PayrollItemsQBData.Select
                    Dim payrollId2 As String = PayrollRow(1).Trim
                    If payrollId2 = payrollID Then
                        remove = False ' payroll item is active
                        Exit For
                    End If
                Next
            End If

            ' Check if the item is active, if it exists
            If Not remove And Not IsDBNull(row(4)) Then
                remove = True
                Dim itemID As String = row(4).Trim
                ' Compares the item in relationship table to active items
                For Each ItemRow As DataRow In ItemsSubItemsQBData.Select
                    Dim itemId2 As String = ItemRow(1).Trim
                    If itemId2 = itemID Then
                        remove = False ' item is active
                        Exit For
                    End If
                Next
            End If

            ' Remove relationship if one or more entities are inactive, otherwise increment numRow
            If remove Then
                Me.QB_TL_IDs.ChargingRelationships.RemoveChargingRelationshipsRow(row)
            Else
                numRow += 1
            End If
        End While

        'ChargingRelationshipsTableAdapter.DeleteInvalidRelationships()


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
            'MAIN.QUITQBSESSION()
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
            'MAIN.QUITQBSESSION()
            Throw ex
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles btn_close.Click
        Me.Close()
    End Sub

    ''' <summary>
    ''' Helper Function to get the desired data for the attribute
    ''' </summary>
    ''' <param name="attribute">
    ''' 1 -> Employee
    ''' 2 -> Vendors
    ''' 3 -> Payroll Item
    ''' 4 -> Job/SubJob
    ''' 5 -> Item/SubItem
    ''' </param>
    ''' <returns></returns>
    Protected Function QBData(ByVal attribute As Integer) As DataTable
        Dim attrQBData As New DataTable
        ' Create four typed columns in the DataTable.
        attrQBData.Columns.Add("QB_Name", GetType(String))
        attrQBData.Columns.Add("QB_ID", GetType(String))

        'step1: prepare the request
        Dim msgSetRs As IMsgSetResponse
        Try
            Dim msgSetRq As IMsgSetRequest = MAIN.SESSMANAGER.CreateMsgSetRequest("US", 2, 0)
            msgSetRq.Attributes.OnError = ENRqOnError.roeContinue
            '  Query 
            Select Case attribute
                Case 1
                    msgSetRq.AppendEmployeeQueryRq()
                Case 2
                    msgSetRq.AppendVendorQueryRq()
                Case 3
                    msgSetRq.AppendPayrollItemWageQueryRq()
                Case 4
                    msgSetRq.AppendCustomerQueryRq()
                Case 5
                    msgSetRq.AppendItemServiceQueryRq()
            End Select

            'step2: send the request
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
                Dim retList = resp.Detail

                For i As Integer = 0 To retList.Count - 1
                    ' Do not add Vendor to VendorList if it is not a 1099 Contractor
                    If attribute = 2 Then
                        Dim check1099 As IVendorRet = retList.getAt(i)
                        Dim syncElbVendors = If(My.Settings.SyncElbVendor = "", False, My.Settings.SyncElbVendor)
                        ' Skip them if vendor is not 1099 and we're only syncing 1099 contractors
                        If Not (syncElbVendors Or check1099.IsVendorEligibleFor1099.GetValue) Then
                            Continue For
                        End If
                    End If

                    Dim ret = retList.GetAt(i)

                    If Not ret.IsActive.GetValue Then
                        Continue For
                    End If

                    ' Full Name for Job/SubJob or Item/SubItem
                    Dim name As String = If(attribute = 4 Or attribute = 5, ret.FullName.GetValue.replace(":", MAIN.colonReplacer), ret.Name.GetValue)

                    ' Do not include customers for a job query
                    ' assumes MAIN.colonReplacer Is not a part of the customer's name
                    If (Not attribute = 4) Or name.Contains(MAIN.colonReplacer) Then
                        attrQBData.Rows.Add(name, ret.ListID.GetValue)
                    End If
                Next
            End If
            If msgSetRs.ResponseList.GetAt(0).StatusSeverity = "Error" Then
                My.Forms.MAIN.History(msgSetRs.ResponseList.GetAt(0).StatusMessage, "C")
                Throw New Exception(msgSetRs.ResponseList.GetAt(0).StatusMessage)
            End If
        Catch ex As Exception
            My.Forms.MAIN.History(ex.ToString, "C")
            Throw ex
        End Try
        Return attrQBData
    End Function

    Protected Function QBEmployees() As DataTable
        Dim EmployeesQBData As DataTable = QBData(1)
        Return EmployeesQBData
    End Function

    Protected Function QB1099Vendors() As DataTable
        Dim VendorsQBData As DataTable = QBData(2)
        Return VendorsQBData
    End Function

    Public Function QBPayrollItems() As DataTable
        Dim PayrollItemsQBData As DataTable = QBData(3)
        Return PayrollItemsQBData
    End Function

    Protected Function QBJobsSubJobs() As DataTable
        Dim JobsSubJobsQBData As DataTable = QBData(4)
        Return JobsSubJobsQBData
    End Function

    ' Changed to public since used in TLtoQB_TimeEntry
    Public Function QBItemsSubItems() As DataTable
        Dim ItemsSubItemsQBData As DataTable = QBData(5)
        Return ItemsSubItemsQBData
    End Function

    ' Sort by selected column
    Dim ascend(5) As Integer '0 is ascending, 1 is descending
    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick
        ' If header clicked, sort by that column
        If e.RowIndex = -1 Then
            DataGridView1.Sort(DataGridView1.Columns(e.ColumnIndex), ascend(e.ColumnIndex))
            ascend(e.ColumnIndex) = If(ascend(e.ColumnIndex), 0, 1)
        End If
    End Sub

    ''' <summary>
    ''' Filters the shown relationships based on the selected attribute
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
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

    ' Returns a string for the employee filter based on the selected employee within the checkbox
    ' If an Employee name is selected, query relationships for that employee
    ' Otherwise an empty string is selected, we should do nothing and return an empty string
    Private Function EmployeeFilter()
        Dim name As String = If(EmployeeFilterBox.SelectedItem Is Nothing, "", EmployeeFilterBox.SelectedItem) ' Empty string if no selectedItem
        Dim search As String = ""
        ' Only check if name is not the empty string
        If name.Length Then
            Dim Employee_ID As String = Me.EmployeesTableAdapter.Name_to_ID(name)

            If Employee_ID Is Nothing Then
                Employee_ID = Me.VendorsTableAdapter.Name_to_ID(name)
            End If

            ' Check if Employee DB stores name as "Last, First" instead of "First Last"
            If Employee_ID Is Nothing Then
                Dim space_index = name.IndexOf(" ")
                Dim firstName = name.Substring(0, space_index)
                Dim lastName = name.Substring(space_index + 1)
                name = lastName.Trim + ", " + firstName
                Employee_ID = Me.EmployeesTableAdapter.Name_to_ID(name)
                ' Check if Vendor DB stores name as "Last, First" instead of "First Last"
                If Employee_ID Is Nothing Then
                    Employee_ID = Me.VendorsTableAdapter.Name_to_ID(name)
                End If
            End If

            ' Only update table if Employee ID was found in DB
            If Not Employee_ID Is Nothing Then
                search = "EmployeeQB_ID = '" + Employee_ID.Trim + "'"
            End If
        End If

        Return search
    End Function

    ' Returns a string for the Job/SubJob filter based on the selected Job/SubJob within the checkbox
    ' If an Job/SubJob name is selected, query relationships for that Job/SubJob
    ' Otherwise an empty string is selected, we should do nothing and return an empty string
    Private Function JobFilter()
        Dim name As String = If(JobFilterBox.SelectedItem Is Nothing, "", JobFilterBox.SelectedItem)
        Dim search As String = ""
        ' Only check if name is not the empty string
        If name.Length Then
            Dim Job_ID As String = Me.Jobs_SubJobsTableAdapter.Name_to_ID(name)

            ' Check if DB stores as "job_name"/"subjob_name", not "customer_name:job_name"/"customer_name:job_name:subjob_name"
            If Job_ID Is Nothing Then
                Dim last_colon = name.LastIndexOf(MAIN.colonReplacer) '":"
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

    ' Returns a string for the Payroll Item filter based on the selected Payroll Item within the checkbox
    ' If an Payroll Item name is selected, query relationships for that Payroll Item
    ' Otherwise an empty string is selected, we should do nothing and return an empty string
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

    ' Returns a string for the Item/SubItem filter based on the selected Item/SubItem within the checkbox
    ' If an Item/SubItem name is selected, query relationships for that employee
    ' Otherwise an empty string is selected, we should do nothing and return an empty string
    Private Function ItemFilter()
        Dim name As String = If(ItemFilterBox.SelectedItem Is Nothing, "", ItemFilterBox.SelectedItem)
        Dim search As String = ""
        ' Only check if name is not the empty string
        If name.Length Then
            Dim Item_ID As String = Me.Items_SubItemsTableAdapter.Name_to_ID(name)

            ' Checks if DB stores as "subitem_name", not "service_name:subitem_name"
            If Item_ID Is Nothing Then
                Dim last_colon = name.LastIndexOf(MAIN.colonReplacer) ' ":"
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

    'Private Sub ChargingRelationshipsBindingSource_CurrentChanged(sender As Object, e As EventArgs) Handles MyBasesBindingSource.CurrentChanged

    'End Sub

    Private Sub QBTLIDsBindingSource_CurrentChanged(sender As Object, e As EventArgs) Handles QBTLIDsBindingSource.CurrentChanged

    End Sub

    Private Sub CustomersBindingSource_CurrentChanged(sender As Object, e As EventArgs) Handles CustomersBindingSource.CurrentChanged

    End Sub

    'Private Sub ChargingRelationshipsBindingSource1_CurrentChanged(sender As Object, e As EventArgs) Handles MyBasesBindingSource1.CurrentChanged

    'End Sub

    'Private Sub ChargingRelationshipsBindingSource2_CurrentChanged(sender As Object, e As EventArgs) Handles MyBasesBindingSource2.CurrentChanged

    'End Sub

    Private Sub EmployeeFilterLabel_Click(sender As Object, e As EventArgs) Handles EmployeeFilterLabel.Click

    End Sub

    Private Sub JobFilterLabel_Click(sender As Object, e As EventArgs) Handles JobFilterLabel.Click

    End Sub

    Private Sub PayrollFilterLabel_Click(sender As Object, e As EventArgs) Handles PayrollFilterLabel.Click

    End Sub

    Private Sub ItemFilterLabel_Click(sender As Object, e As EventArgs) Handles ItemFilterLabel.Click

    End Sub

    Private Sub SplitContainer1_SplitterMoved(sender As Object, e As SplitterEventArgs) Handles SplitContainer1.SplitterMoved

    End Sub

    Private Sub SplitContainer2_SplitterMoved(sender As Object, e As SplitterEventArgs) Handles SplitContainer2.SplitterMoved

    End Sub

    Private Sub SplitContainer3_SplitterMoved(sender As Object, e As SplitterEventArgs) Handles SplitContainer3.SplitterMoved

    End Sub

    Private Sub EmployeesBindingSource_CurrentChanged(sender As Object, e As EventArgs) Handles EmployeesBindingSource.CurrentChanged

    End Sub

    Private Sub ItemsSubItemsBindingSource_CurrentChanged(sender As Object, e As EventArgs) Handles ItemsSubItemsBindingSource.CurrentChanged

    End Sub

    Private Sub EmployeesBindingSource1_CurrentChanged(sender As Object, e As EventArgs) Handles EmployeesBindingSource1.CurrentChanged

    End Sub

    Private Sub JobsSubJobsBindingSource_CurrentChanged(sender As Object, e As EventArgs) Handles JobsSubJobsBindingSource.CurrentChanged

    End Sub

    'Private Sub ChargingRelationshipsBindingSource_CurrentChanged(sender As Object, e As EventArgs) Handles MyBasesBindingSource.CurrentChanged

    'End Sub

    'Private Sub ChargingRelationshipsBindingSource1_CurrentChanged(sender As Object, e As EventArgs) Handles MyBasesBindingSource1.CurrentChanged

    'End Sub

    'Private Sub ChargingRelationshipsBindingSource2_CurrentChanged(sender As Object, e As EventArgs) Handles MyBasesBindingSource2.CurrentChanged

    'End Sub
End Class

