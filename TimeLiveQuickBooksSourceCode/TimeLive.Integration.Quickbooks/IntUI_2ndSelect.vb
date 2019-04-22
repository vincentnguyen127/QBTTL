'Imports QBFC11Lib
'Imports QBFC13Lib

Imports System.Net.Mail
Imports System.Data.SqlClient
Imports System.Text
Imports System.Globalization


Public Class IntUI_2ndSelect
    Inherits System.Windows.Forms.Form
    Private p_token As String
    Private p_AccountId As String
    Private Type As Integer
    Private EmployeeRecNo As Double = 0
    Private StartDate As DateTime
    Private EndDate As DateTime
    Private cur_week
    Private SelectAll As Boolean

    Dim selectedEmployeeData As New TLtoQB_TimeEntry.EmployeeDataStructure
    Dim timeentry_tltoqb As TLtoQB_TimeEntry = New TLtoQB_TimeEntry
    Dim TimeEntryData As New TLtoQB_TimeEntry.TimeEntryDataStructureQB



    Public Overloads Sub Show(ByVal token As String, ByVal AccountId As String, ByRef objData As TLtoQB_TimeEntry.EmployeeDataStructure,
                              StartD As DateTime, EndD As DateTime, TypeSelected As Integer)
        Type = TypeSelected
        p_AccountId = AccountId
        p_token = token
        selectedEmployeeData = objData
        StartDate = StartD
        EndDate = EndD
        MyBase.Show()
    End Sub

    Public Sub IntUI_2ndSelect_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Show()

        ' TODO: Test with non-empty DataGridView1
        If DataGridView1.ColumnCount > 1 Then
            DataGridView1.Sort(DataGridView1.Columns(1), System.ComponentModel.ListSortDirection.Ascending)
        End If


        DataGridView1.AutoSize = False
        DataGridView1.AutoSizeRowsMode = False

        '-------------------------201---------------------------------------------
        'for type timeentry
        If Type = 201 Then

            'Load grid(there might be an easire way)
            Dim col2 As New DataGridViewTextBoxColumn
            col2.Name = "Date"
            DataGridView1.Columns.Add(col2)
            Dim col3 As New DataGridViewTextBoxColumn
            col3.Name = "Customer"
            DataGridView1.Columns.Add(col3)
            Dim col4 As New DataGridViewTextBoxColumn
            col4.Name = "Job"
            DataGridView1.Columns.Add(col4)
            Dim col5 As New DataGridViewTextBoxColumn
            col5.Name = "SubJob"
            DataGridView1.Columns.Add(col5)
            Dim col6 As New DataGridViewTextBoxColumn
            col6.Name = "Time"
            DataGridView1.Columns.Add(col6)
            Dim col7 As New DataGridViewTextBoxColumn
            col7.Name = "Class"
            DataGridView1.Columns.Add(col7)
            Dim col8 As New DataGridViewTextBoxColumn
            col8.Name = "Payroll Item"
            DataGridView1.Columns.Add(col8)
            Dim col9 As New DataGridViewTextBoxColumn
            col9.Name = "Item SubItem"
            DataGridView1.Columns.Add(col9)

            'look for first selected employee
            'NOTE - Prior to processing make sure user has selected entries
            For Each element As TLtoQB_TimeEntry.Employee In selectedEmployeeData.DataArray
                With element
                    If element.RecSelect = True Then
                        My.Forms.MAIN.History("Processing: " + element.FullName.ToString(), "n")
                        LoadSelectedTmeEntryItems(element.AccountEmployeeId, element.FullName)
                        'deselect as not to load again
                        element.RecSelect = False
                        Exit For
                    End If
                End With
            Next
        End If

        '-------------------------2---------------------------------------------
        'For Type

        Me.ProgressBar1.Value = 0
        System.Threading.Thread.Sleep(500)
        System.Windows.Forms.Application.DoEvents()

        ' Select all
        SelectAll = True

        For Each row As DataGridViewRow In DataGridView1.Rows
            If row.Cells("Date").Value IsNot Nothing Then
                row.Cells("ckBox").Value = True
            End If
        Next
    End Sub

    Public Sub LoadSelectedTmeEntryItems(AccountEmployeeId As String, EmployeeName As String)
        Dim temp As New TLtoQB_TimeEntry.TimeEntryDataStructureQB
        TimeEntryData = timeentry_tltoqb.GetTimeEntryTLData(AccountEmployeeId, StartDate, EndDate,
                                       Me, p_token, False)
        Dim TotalHour As Integer
        Dim TotalMin As Double
        If TimeEntryData IsNot Nothing Then
            For Each element As TLtoQB_TimeEntry.TimeEntry In TimeEntryData.DataArray
                With element
                    Dim Item_SubItemID As String = Nothing

                    TotalHour = .TotalTime.ToString("%h")
                    'Turn hour to 24 hour time
                    TotalHour = TotalHour Mod 12
                    If Not .TotalTime.ToString.Contains("AM") Then
                        TotalHour += 12
                    End If

                    TotalMin = .TotalTime.ToString("%m")
                    TotalMin = (TotalMin / 60).ToString("00.00")

                    Dim payrollDisp As String = If(.PayrollName Is Nothing, .PayrollItem, .PayrollName)
                    Dim ServiceDisp As String = If(.ServiceName Is Nothing, .ServiceItem, .ServiceName)


                    DataGridView1.Rows.Add(.RecSelect, .TimeEntryDate.ToString("MM/dd/yyyy"), .CustomerName.ToString(),
                                           .ProjectName.ToString(), .TaskWithParent.ToString(),
                                           (TotalHour + TotalMin).ToString, .TimeEntryClass, payrollDisp, ServiceDisp)
                    element.RecSelect = True
                End With
            Next
            Lbel_processing.Text = EmployeeName.ToString()
        End If

        'Select All
        For Each row As DataGridViewRow In DataGridView1.Rows
            If row.Cells("Date").Value IsNot Nothing Then
                row.Cells("ckBox").Value = True
            End If
        Next

        'MsgBox(" at the end " + TimeEntryData.NoItems.ToString)
    End Sub

    Private Sub btnclose_Click(sender As Object, e As EventArgs) Handles bntclose.Click
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub

    Private Sub btnTransfer_Click(sender As Object, e As EventArgs) Handles btnTransfer.Click
        Dim ItemsProcessed As Integer = 0

        If Type = 201 Then
            Reset_Checked_TimeEntry_Value(TimeEntryData)
            Set_Selected_TimeEntry()
            ' need to set selected data

            Dim element As New TLtoQB_TimeEntry.EmployeeDataStructure
            element = selectedEmployeeData
            timeentry_tltoqb.TLTransferTimeToQB(TimeEntryData, p_token, Me, True)

            'wait for one second so user can see progress bar
            System.Threading.Thread.Sleep(500)
            System.Windows.Forms.Application.DoEvents()
            Me.ProgressBar1.Value = 0
            System.Threading.Thread.Sleep(500)
            System.Windows.Forms.Application.DoEvents()

            Dim NextItemFound As Boolean = False
            DataGridView1.DataSource = Nothing
            DataGridView1.Rows.Clear()
            For Each element1 As TLtoQB_TimeEntry.Employee In selectedEmployeeData.DataArray
                With element1
                    If element1.RecSelect = True Then
                        'Dim IntUI_2ndSelect As New IntUI_2ndSelect
                        'IntUI_2ndSelect.Owner = Me.Parent
                        'IntUI_2ndSelect.Show(p_token, p_AccountId, selectedEmployeeData,
                        '                     CDate(StartDate).Date,
                        '                     CDate(EndDate).Date.ToString, 201)

                        My.Forms.MAIN.History("Processing: " + element1.FullName.ToString(), "n")
                        LoadSelectedTmeEntryItems(element1.AccountEmployeeId, element1.FullName)
                        NextItemFound = True
                        'deselect as not to load again
                        element1.RecSelect = False
                        Exit For
                    End If
                End With
            Next

            If NextItemFound = False Then
                MessageBox.Show("Last employee processed")
                Me.Close()
            End If
        End If
    End Sub

    Private Sub Reset_Checked_TimeEntry_Value(ByRef TimeEntryObj As TLtoQB_TimeEntry.TimeEntryDataStructureQB)
        ' reset the check value to zero
        For Each element As TLtoQB_TimeEntry.TimeEntry In TimeEntryObj.DataArray
            element.RecSelect = False
        Next
    End Sub

    Private Sub Set_Selected_TimeEntry()
        For Each row As DataGridViewRow In DataGridView1.Rows
            If row.Cells("Date").Value IsNot Nothing Then
                If row.Cells("ckBox").Value = True Then
                    If TimeEntryData.NoItems > 0 Then
                        TimeEntryData.DataArray(row.Index).RecSelect = True
                        My.Forms.MAIN.History("Selected for processing: " + row.Cells("Date").Value, "n")
                    End If
                End If
            End If
        Next
    End Sub

    Private Sub btnselectall_Click(sender As Object, e As EventArgs) Handles btnselectall.Click
        If SelectAll = False Then
            For Each row As DataGridViewRow In DataGridView1.Rows
                If row.Cells("Name").Value IsNot Nothing Then
                    row.Cells("ckBox").Value = True
                End If
            Next
            SelectAll = True
        Else
            For Each row As DataGridViewRow In DataGridView1.Rows
                If row.Cells("Name").Value IsNot Nothing Then
                    row.Cells("ckBox").Value = False
                End If
            Next
            SelectAll = False
        End If
    End Sub

    Private Sub DataGridView1_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellValueChanged
        Dim TotalTime As Double = 0.0
        Dim strTime As String = Nothing

        If e.ColumnIndex = 0 Then
            For Each row As DataGridViewRow In DataGridView1.Rows
                If (row.Cells("ckBox").Value = True) Then
                    TotalTime = TotalTime + row.Cells("Time").Value.ToString()
                End If
            Next
            Lbel_TotalHours.Text = TotalTime.ToString("F2")
        End If
    End Sub
End Class