Imports QBFC13Lib
Imports System.Net.Mail
Imports System.Data.SqlClient

Public Class TLtoQB_ExpenseReports
    Public Class ExpenseReportDataStructureQB
        Public NoItems As Integer = 0
        Public DataArray As New List(Of ExpenseReport)

        Public Sub combine(other As ExpenseReportDataStructureQB)
            Me.NoItems += other.NoItems
            Me.DataArray.AddRange(other.DataArray)
        End Sub

        Public Sub clear()
            Me.NoItems = 0
            Me.DataArray = New List(Of ExpenseReport)
        End Sub
    End Class

    Public Class ExpenseReport
        Public RecSelect As Boolean
        Public CustomerName As String
        Public EmployeeName As String
        Public ExpenseName As String
        Public ProjectName As String
        Public Amount As Double
        Public ExpenseReportDate As Date

        Sub New(ByVal CustomerName As String, ByVal EmployeeName As String, ByVal ExpenseName As String, ByVal ProjectName As String, ByVal Amount As Double, ByVal ExpenseReportDate As Date)
            RecSelect = False
            Me.CustomerName = CustomerName
            Me.EmployeeName = EmployeeName
            Me.ExpenseName = ExpenseName
            Me.ProjectName = ProjectName
            Me.Amount = Amount
            Me.ExpenseReportDate = ExpenseReportDate
        End Sub
    End Class


    Public Function GetExpenseReportTLData(AccountEmployeeId As Integer, dpStartDate As DateTime, dpEndDate As DateTime,
                                       MainForm As MAIN, ByVal token As String, UI As Boolean) As ExpenseReportDataStructureQB
        Dim ExpenseReportData As New ExpenseReportDataStructureQB
        Dim expenseReports As TLtoQB_ExpenseReports = New TLtoQB_ExpenseReports
        Dim temp As String = Nothing

        Try
            Dim objExpenseTrackingServices As New Services.TimeLive.ExpenseEntries.ExpenseEntries
            Dim authentication As New Services.TimeLive.ExpenseEntries.SecuredWebServiceHeader
            authentication.AuthenticatedToken = token
            objExpenseTrackingServices.SecuredWebServiceHeaderValue = authentication
            Dim objExpenseReportsArray() As Object

            objExpenseReportsArray = objExpenseTrackingServices.GetExpenseEntriesByEmployeeIdAndDateRange(AccountEmployeeId, dpStartDate, dpEndDate)
            Dim objExpenseEntry As New Services.TimeLive.ExpenseEntries.ExpenseEntry

            'sets status bar. If no, UI skip
            If UI Then
                My.Forms.MAIN.ProgressBar1.Maximum = objExpenseReportsArray.Length
                My.Forms.MAIN.ProgressBar1.Value = 0
            End If

            'Dim EmployeeName As String = Nothing
            Dim FirstMiddleLastName() As String = Nothing

            For n As Integer = 0 To objExpenseReportsArray.Length - 1
                objExpenseEntry = objExpenseReportsArray(n)

                With objExpenseEntry
                    ExpenseReportData.NoItems += 1
                    Dim customerName As String = .ClientWithProject
                    Dim projectName As String = .ClientWithProject

                    ExpenseReportData.DataArray.Add(New ExpenseReport(customerName, .EmployeeName, .ExpenseName, projectName, .Amount, .ExpenseEntryDate))
                End With

                If UI Then My.Forms.MAIN.ProgressBar1.Value += 1
            Next

        Catch ex As Exception
            My.Forms.MAIN.History("Error retrieving time entries: " + ex.Message, "N")
        End Try

        Return ExpenseReportData
    End Function
End Class
