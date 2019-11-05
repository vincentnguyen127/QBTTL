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

        Public Function getSelectedExpenseReports() As List(Of ExpenseReport)
            Dim selectedReports As New List(Of ExpenseReport)

            For Each expenseEntry As ExpenseReport In Me.DataArray
                If expenseEntry.RecSelect Then
                    selectedReports.Add(expenseEntry)
                End If
            Next

            Return selectedReports
        End Function

    End Class

    Public Class ExpenseReport
        Public RecSelect As Boolean
        Public EntryID As Integer
        Public EmployeeName As String
        Public ExpenseName As String
        Public ProjectName As String
        Public Description As String
        Public Amount As Double
        Public ExpenseReportDate As Date
        Public ExpenseSheetId As Guid

        Sub New(ByVal EntryID As Integer, ByVal EmployeeName As String, ByVal ExpenseName As String, ByVal Description As String, ByVal ProjectName As String, ByVal Amount As Double, ByVal ExpenseReportDate As Date, ByVal ExpenseSheetId As Guid)
            Me.RecSelect = False
            Me.EntryID = EntryID
            Me.EmployeeName = EmployeeName
            Me.ExpenseName = ExpenseName
            Me.Description = Description
            Me.ProjectName = ProjectName
            Me.Amount = Amount
            Me.ExpenseReportDate = ExpenseReportDate
            Me.ExpenseSheetId = ExpenseSheetId
        End Sub
    End Class

    Public Function GetExpenseReportTLData(ExpenseSheetId As Guid, ByVal EmployeeName As String, ByVal ExpenseSheetDate As Date, MainForm As MAIN, ByVal token As String, UI As Boolean) As ExpenseReportDataStructureQB
        Dim ExpenseReportData As New ExpenseReportDataStructureQB
        Dim expenseReports As TLtoQB_ExpenseReports = New TLtoQB_ExpenseReports
        Dim temp As String = Nothing

        Try
            Dim objExpenseReportDB As New TimeLiveDataSetTableAdapters.AccountExpenseEntryTableAdapter
            Dim expenseObj As Services.TimeLive.ExpenseEntries.ExpenseEntries = MAIN.connect_TL_expense_reports(token)
            Dim expenseReportsBySheetId = expenseObj.GetExpenseEntriesByExpenseSheetIdForMobile(ExpenseSheetId)
            Dim projectTableAdapter As New QB_TL_IDsTableAdapters.Jobs_SubJobsTableAdapter

            'sets status bar. If no, UI skip
            If UI Then
                My.Forms.MAIN.ProgressBar1.Maximum = expenseReportsBySheetId.Length
                My.Forms.MAIN.ProgressBar1.Value = 0
            End If

            For n As Integer = 0 To expenseReportsBySheetId.Length - 1
                Dim objExpenseEntry As Services.TimeLive.ExpenseEntries.ExpenseEntryListForMobile = expenseReportsBySheetId(n)
                With objExpenseEntry
                    Dim projectName As String = projectTableAdapter.GetNamefromTLID(.AccountClientId)
                    If projectName IsNot Nothing Then projectName = projectName.Trim
                    ExpenseReportData.add(New ExpenseReport(.AccountExpenseEntryId, EmployeeName, .AccountExpenseName, .ExpenseEntryDescription, projectName, .ExpenseEntryAmount, ExpenseSheetDate, ExpenseSheetId))
                End With

                If UI Then My.Forms.MAIN.ProgressBar1.Value += 1
            Next

        Catch ex As Exception
            My.Forms.MAIN.History("Error retrieving time entries: " + ex.Message, "N")
        End Try

        My.Forms.MAIN.ProgressBar1.Value = 0
        Return ExpenseReportData
    End Function

    Private Function createExpenseSheetToExpenseEntriesDict(ByVal expenseEntries As ExpenseReport()) As Dictionary(Of Guid, List(Of ExpenseReport))
        Dim expenseSheetToExpenseEntries As New Dictionary(Of Guid, List(Of ExpenseReport))

        For Each expenseEntry As ExpenseReport In expenseEntries
            If Not expenseSheetToExpenseEntries.ContainsKey(expenseEntry.ExpenseSheetId) Then
                expenseSheetToExpenseEntries.Add(expenseEntry.ExpenseSheetId, New List(Of ExpenseReport))
            End If
            expenseSheetToExpenseEntries(expenseEntry.ExpenseSheetId).Add(expenseEntry)
        Next

        Return expenseSheetToExpenseEntries
    End Function

    Private Function performRequest(ByVal msgSetRq As IMsgSetRequest, ByVal errorMsg As String, Optional UI As Boolean = True)
        Dim msgSetRs As IMsgSetResponse = MAIN.SESSMANAGER.DoRequests(msgSetRq)
        Dim response As IResponse = msgSetRs.ResponseList.GetAt(0)
        If response.StatusSeverity = "Error" Then
            If UI Then
                MsgBox(errorMsg + vbCrLf + "Error Message: " + response.StatusMessage, MsgBoxStyle.OkOnly, "Error Processing to Quickbooks")
            Else
                Throw New Exception(response.StatusMessage)
            End If
            Return False
        End If
        Return true
    End Function

    Private Function addAccountNameToQB(ByVal accountName As String, Optional accountNumber As String = Nothing, Optional parentName As String = Nothing)
        Dim msgSetRq As IMsgSetRequest = MAIN.SESSMANAGER.CreateMsgSetRequest("US", 2, 0)
        Dim account As IAccountAdd = msgSetRq.AppendAccountAddRq
        account.Name.SetValue(accountName)
        account.AccountType.SetValue(ENAccountType.atExpense)

        If parentName IsNot Nothing Then
            account.ParentRef.FullName.SetValue(parentName)
            If Not accountExists(parentName) Then
                addAccountNameToQB(parentName)
            End If
        End If

        If accountNumber IsNot Nothing Then
            account.AccountNumber.SetValue(accountNumber)
        End If
        Return performRequest(msgSetRq, "Error inserting account " + accountName + " into quickbooks")
    End Function

    Private Function accountExists(ByVal accountName As String)
        Dim msgSetRq As IMsgSetRequest = MAIN.SESSMANAGER.CreateMsgSetRequest("US", 2, 0)
        msgSetRq.Attributes.OnError = ENRqOnError.roeContinue

        Dim AccountQueryRq As IAccountQuery = msgSetRq.AppendAccountQueryRq
        AccountQueryRq.ORAccountListQuery.FullNameList.Add(accountName)

        Dim accountRetList As IAccountRetList = MAIN.SESSMANAGER.DoRequests(msgSetRq).ResponseList.GetAt(0).Detail
        Dim inQB = Not accountRetList Is Nothing

        Return inQB
    End Function

    Private Sub addCreditExpenseToJournalEntry(ByVal journalEntry As IJournalEntryAdd, ByVal expenseEntry As ExpenseReport)
        Dim defaultAccountName As String = "Capital One Spark_2 x6435"
        Dim defaultAccountNumber As String = "2052"
        With expenseEntry
            If Not accountExists(defaultAccountName) Then
                addAccountNameToQB(defaultAccountName, defaultAccountNumber)
            End If

            Dim journalRow As IORJournalLine = journalEntry.ORJournalLineList.Append()
            If .EmployeeName IsNot Nothing Then journalRow.JournalCreditLine.EntityRef.FullName.SetValue(.EmployeeName)
            journalRow.JournalCreditLine.Amount.SetValue(.Amount)
            journalRow.JournalCreditLine.Memo.SetValue(.ExpenseName + " - " + .Description)
            journalRow.JournalCreditLine.AccountRef.FullName.SetValue(defaultAccountName)
        End With
    End Sub

    Private Sub addDebitToJournalEntry(ByVal journalEntry As IJournalEntryAdd, ByVal totalExpenses As Double, ByVal projectName As String, ByVal memo As String)
        Dim defaultParentAccountName As String = "Direct Costs"
        Dim defaultParentAccountNumber As String = "5000"
        Dim defaultAccountName As String = "Other Direct Costs"
        Dim defaultAccountNumber As String = "5500"

        If Not accountExists(defaultParentAccountName) Then
            addAccountNameToQB(defaultParentAccountName, defaultParentAccountNumber)
        End If

        If Not accountExists(defaultParentAccountName + ":" + defaultAccountName) Then
            addAccountNameToQB(defaultAccountName, defaultAccountNumber, parentName:=defaultParentAccountName)
        End If

        Dim journalRow As IORJournalLine = journalEntry.ORJournalLineList.Append()
        journalRow.JournalDebitLine.Amount.SetValue(totalExpenses)
        journalRow.JournalDebitLine.AccountRef.FullName.SetValue(defaultParentAccountName + ":" + defaultAccountName)
        If projectName IsNot Nothing And projectName <> "" Then journalRow.JournalDebitLine.EntityRef.FullName.SetValue(projectName)
        journalRow.JournalDebitLine.Memo.SetValue(memo)
    End Sub

    Public Function AddExpenseEntriesToQB(ByVal sheet As ExpenseSheet, ByVal expenseEntries As List(Of ExpenseReport), Optional UI As Boolean = True)
        If expenseEntries.Count = 0 Then
            Return 0
        End If

        Dim msgSetRq As IMsgSetRequest = MAIN.SESSMANAGER.CreateMsgSetRequest("US", 2, 0)
        Dim projectToTotalExpenses As New Dictionary(Of String, Double)
        Dim journalEntry As IJournalEntryAdd = msgSetRq.AppendJournalEntryAddRq
        journalEntry.TxnDate.SetValue(sheet.SheetDate)
        journalEntry.Memo.SetValue(sheet.Description)

        Dim numAdded As Integer = 0
        For Each element As ExpenseReport In expenseEntries
            Try
                ' TODO: Check in Table Adapter if the expense entry was already transfered
                addCreditExpenseToJournalEntry(journalEntry, element)
                Dim projectName As String = If(element.ProjectName Is Nothing, "", element.ProjectName)
                If Not projectToTotalExpenses.ContainsKey(projectName) Then
                    projectToTotalExpenses.Add(projectName, 0)
                End If
                projectToTotalExpenses(projectName) += element.Amount
                numAdded += 1
            Catch ex As Exception
                Dim error_msg As String = "Error inserting " + element.EmployeeName + "'s " + element.ExpenseName + " expense on " + element.ExpenseReportDate + " into quickbooks." +
                                          vbCrLf + "Error Message: " + ex.Message
                If UI Then MsgBox(error_msg, MsgBoxStyle.OkOnly, "Error inserting Time Entry into QuickBooks")
                My.Forms.MAIN.History(error_msg, "n")
            End Try
        Next

        For Each projectName As String In projectToTotalExpenses.Keys
            addDebitToJournalEntry(journalEntry, Math.Round(projectToTotalExpenses(projectName), 2), projectName, sheet.Description)
        Next

        Dim errorMsg As String = "Error inserting expenses for " + sheet.EmployeeName + " on " + sheet.EmployeeName + " into quickbooks."
        performRequest(msgSetRq, errorMsg, UI)

        Return numAdded
    End Function

    Public Function TLTransferExpensesToQB(ByRef expenseEntryData As ExpenseReportDataStructureQB, ByVal expenseSheets As ExpenseSheetDataStructure, MainForm As MAIN, UI As Boolean) As Integer
        Dim selectedExpenseEntries As ExpenseReport() = expenseEntryData.getSelectedExpenseReports().ToArray
        If UI Then
            MainForm.ProgressBar1.Maximum = selectedExpenseEntries.Length
            MainForm.ProgressBar1.Value = 0
        End If

        Dim NumRecordsCreatedorUpdated = 0
        Dim expenseSheetToExpenseEntries As Dictionary(Of Guid, List(Of ExpenseReport)) = createExpenseSheetToExpenseEntriesDict(selectedExpenseEntries)

        For Each sheet As ExpenseSheet In expenseSheets.getDataArray()
            If expenseSheetToExpenseEntries.ContainsKey(sheet.SheetId) Then
                Dim expenseEntries As List(Of ExpenseReport) = expenseSheetToExpenseEntries(sheet.SheetId)
                NumRecordsCreatedorUpdated += AddExpenseEntriesToQB(sheet, expenseEntries)

                If UI Then
                    My.Forms.MAIN.ProgressBar1.Value += expenseEntries.Count
                End If
            End If
        Next

        Return NumRecordsCreatedorUpdated
    End Function
End Class
