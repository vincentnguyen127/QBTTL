Public Class WebServiceUtilities    
    Public Shared Function GetTimeLiveServiceURL() As String
        Return "/Services/TimeLiveServices.asmx"
    End Function
    Public Shared Function GetEmployeeServiceURL() As String
        Return "/Services/Employees.asmx"
    End Function
    Public Shared Function GetClientServiceURL() As String
        Return "/Services/Clients.asmx"
    End Function
    Public Shared Function GetProjectServiceURL() As String
        Return "/Services/Projects.asmx"
    End Function
    Public Shared Function GetTaskServiceURL() As String
        Return "/Services/Tasks.asmx"
    End Function
    Public Shared Function GetTimesEntryServiceURL() As String
        Return "/Services/TimeEntries.asmx"
    End Function
    Public Shared Function GetExpenseEntryServiceURL() As String
        Return "/Services/ExpenseEntries.asmx"
    End Function
End Class
