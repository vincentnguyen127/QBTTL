Imports QBFC13Lib
Imports System.Net.Mail
Imports System.Data.SqlClient

Public Class TLtoQB_ExpenseReports
    Public Class ExpenseSheetDataStructure
        Public DataArray As New List(Of ExpenseSheet)

        Public Sub clear()
            Me.DataArray = New List(Of ExpenseSheet)
        End Sub

        Public Sub add(ByVal expenseSheet As ExpenseSheet)
            Me.DataArray.Add(expenseSheet)
        End Sub

        Public Function NumExpenseReports() As Integer
            Return DataArray.Count
        End Function

        Public Function getDataArray()
            Return DataArray.ToArray
        End Function
    End Class

    Public Class ExpenseSheet
        Public RecSelect As Boolean
        Public SheetId As Guid
        Public EmployeeName As String
        Public SheetDate As Date
        Public Description As String
        Public NumReports As Integer

        Sub New(ByVal SheetId As Guid, ByVal EmployeeName As String, ByVal SheetDate As Date, ByVal Description As String, Optional NumReports As Integer = 0)
            Me.RecSelect = False
            Me.SheetId = SheetId
            Me.EmployeeName = EmployeeName
            Me.SheetDate = SheetDate
            Me.Description = Description
            Me.NumReports = NumReports
        End Sub
    End Class

    Public Class ExpenseReportDataStructureQB
        Public DataArray As New List(Of ExpenseReport)

        Public Sub combine(other As ExpenseReportDataStructureQB)
            Me.DataArray.AddRange(other.DataArray)
        End Sub

        Public Sub clear()
            Me.DataArray = New List(Of ExpenseReport)
        End Sub

        Public Sub add(ByVal expenseReport As ExpenseReport)
            Me.DataArray.Add(expenseReport)
        End Sub

        Public Function NumExpenseReports() As Integer
            Return DataArray.Count
        End Function

        Public Function getDataArray()
            Return DataArray.ToArray
        End Function
    End Class

    Public Class ExpenseReport
        Public RecSelect As Boolean
        Public EmployeeName As String
        Public ExpenseName As String
        Public ProjectName As String
        Public Amount As Double
        Public ExpenseReportDate As Date

        Sub New(ByVal EmployeeName As String, ByVal ExpenseName As String, ByVal ProjectName As String, ByVal Amount As Double, ByVal ExpenseReportDate As Date)
            RecSelect = False
            Me.EmployeeName = EmployeeName
            Me.ExpenseName = ExpenseName
            Me.ProjectName = ProjectName
            Me.Amount = Amount
            Me.ExpenseReportDate = ExpenseReportDate
        End Sub
    End Class

    Public Function GetExpenseReportTLData(ExpenseSheetId As Guid, MainForm As MAIN, ByVal token As String, UI As Boolean) As ExpenseReportDataStructureQB
        Dim ExpenseReportData As New ExpenseReportDataStructureQB
        Dim expenseReports As TLtoQB_ExpenseReports = New TLtoQB_ExpenseReports
        Dim temp As String = Nothing

        Try
            Dim objExpenseReportDB As New TimeLiveDataSetTableAdapters.AccountExpenseEntryTableAdapter
            Dim objExpenseReports As TimeLiveDataSet.AccountExpenseEntryDataTable

            objExpenseReports = objExpenseReportDB.getEntriesOfExpenseSheet(ExpenseSheetId)


            'sets status bar. If no, UI skip
            If UI Then
                My.Forms.MAIN.ProgressBar1.Maximum = objExpenseReports.Count
                My.Forms.MAIN.ProgressBar1.Value = 0
            End If

            For n As Integer = 0 To objExpenseReports.Count - 1
                'objExpenseEntry = objExpenseReports(0)
                'With objExpenseEntry
                '    ExpenseReportData.add(New ExpenseReport(.EmployeeName, .ExpenseName, .ClientWithProject, .Amount, .ExpenseEntryDate))
                'End With

                If UI Then My.Forms.MAIN.ProgressBar1.Value += 1
            Next

        Catch ex As Exception
            My.Forms.MAIN.History("Error retrieving time entries: " + ex.Message, "N")
        End Try

        Return ExpenseReportData
    End Function

    Private Function getExpenseSheetofEntry(expenseEntry As ExpenseReport, employeeTableAdapter As QB_TL_IDsTableAdapters.EmployeesTableAdapter,
                                            vendorTableAdapter As QB_TL_IDsTableAdapters.VendorsTableAdapter,
                                            expenseEntryTableAdapter As TimeLiveDataSetTableAdapters.AccountExpenseEntryTableAdapter,
                                            expenseTableAdapter As TimeLiveDataSetTableAdapters.AccountExpenseTableAdapter) As String
        Dim expenseSheetId As String = ""
        With expenseEntry
            Dim employeeId As String = employeeTableAdapter.GetCorrespondingTL_IDbyTL_Name(.EmployeeName)
            If employeeId Is Nothing Then
                employeeId = vendorTableAdapter.GetCorrespondingTL_IDbyTL_Name(.EmployeeName)
                If employeeId Is Nothing Then
                    My.Forms.MAIN.History("Error retrieving employee ID for " + .EmployeeName, "N")
                End If
            Else
                employeeId = employeeId.Trim
            End If
            Dim expenseId As Integer = expenseTableAdapter.getExpenseIdFromName(.ExpenseName)
            ' TODO: Get this working properly (Might be the date needs to become a datetime)
            expenseSheetId = expenseEntryTableAdapter.getExpenseSheetId(Convert.ToInt32(employeeId), .ExpenseReportDate, expenseId, .Amount)
        End With

        Return expenseSheetId
    End Function

    Private Function createExpenseSheetToExpenseEntriesDict(expenseEntries As ExpenseReport()) As Dictionary(Of String, List(Of ExpenseReport))
        Dim employeeTableAdapter As New QB_TL_IDsTableAdapters.EmployeesTableAdapter
        Dim vendorTableAdapter As New QB_TL_IDsTableAdapters.VendorsTableAdapter
        Dim expenseEntryTableAdapter As New TimeLiveDataSetTableAdapters.AccountExpenseEntryTableAdapter
        Dim expenseTableAdapter As New TimeLiveDataSetTableAdapters.AccountExpenseTableAdapter
        Dim expenseSheetToExpenseEntries As New Dictionary(Of String, List(Of ExpenseReport))

        For Each expenseEntry As ExpenseReport In expenseEntries
            Dim expenseSheetId = getExpenseSheetofEntry(expenseEntry, employeeTableAdapter, vendorTableAdapter, expenseEntryTableAdapter, expenseTableAdapter)
            If expenseSheetId Is Nothing Then
                My.Forms.MAIN.History("Error retrieving Sheet ID for expense " + expenseEntry.ExpenseName + " for" + expenseEntry.EmployeeName, "N")
                expenseSheetId = "Undefined"
            End If

            If Not expenseSheetToExpenseEntries.ContainsKey(expenseSheetId) Then
                expenseSheetToExpenseEntries.Add(expenseSheetId, New List(Of ExpenseReport))
            End If
            expenseSheetToExpenseEntries(expenseSheetId).Add(expenseEntry)
        Next

        Return expenseSheetToExpenseEntries
    End Function

    Public Function AddExpenseEntriesToQB(expenseEntries As List(Of ExpenseReport), Optional UI As Boolean = True)
        'step1: prepare the request
        Dim msgSetRs As IMsgSetResponse
        Dim msgSetRq As IMsgSetRequest = MAIN.SESSMANAGER.CreateMsgSetRequest("US", 2, 0)

        Dim numAdded As Integer = 0
        For Each element As ExpenseReport In expenseEntries
            Try
                numAdded += 1
                ' Add entry to sheet
            Catch ex As Exception
                Dim error_msg As String = "Error inserting " + element.EmployeeName + "'s " + element.ExpenseName + " expense on " + element.ExpenseReportDate + " into quickbooks." +
                                          vbCrLf + "Error Message: " + ex.Message
                If UI Then
                    MsgBox(error_msg, MsgBoxStyle.OkOnly, "Error inserting Time Entry into QuickBooks")
                End If
                My.Forms.MAIN.History(error_msg, "n")
            End Try
        Next

        ' Add sheet to QuickBooks

        Return numAdded
    End Function

    Public Function TLTransferExpensesToQB(ByRef objData As TLtoQB_ExpenseReports.ExpenseReportDataStructureQB,
                                           ByVal token As String, MainForm As MAIN, UI As Boolean) As Integer
        'sets status bar. If no, UI skip
        If UI Then
            My.Forms.MAIN.ProgressBar1.Maximum = objData.NumExpenseReports()
            My.Forms.MAIN.ProgressBar1.Value = 0
        End If

        Dim NumRecordsCreatedorUpdated = 0
        Dim expenseSheetToExpenseEntries As Dictionary(Of String, List(Of ExpenseReport)) = createExpenseSheetToExpenseEntriesDict(objData.getDataArray())

        For Each sheetID As String In expenseSheetToExpenseEntries.Keys
            Dim expenseEntries As List(Of ExpenseReport) = expenseSheetToExpenseEntries(sheetID)
            NumRecordsCreatedorUpdated += AddExpenseEntriesToQB(expenseEntries)

            If UI Then
                My.Forms.MAIN.ProgressBar1.Value += expenseEntries.Count
            End If
        Next

        Return NumRecordsCreatedorUpdated
    End Function
End Class
