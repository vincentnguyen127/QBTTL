Imports Interop.QBFC10
Public Class QBJobOrItem
    Private p_token As String
    Private p_AccountId As String
    Public Overloads Sub Show(ByVal token As String, ByVal AccountId As String)
        If token Is Nothing Then
            MsgBox("Please Login")
        Else
            p_AccountId = AccountId
            p_token = token
            MyBase.Show()
        End If
    End Sub
    Public Sub AddJobInQB(ByVal JobName As String, ByVal Name As String)
        'step1: create QBFC session manager and prepare the request
        Dim sessManager As QBSessionManager
        Dim msgSetRs As IMsgSetResponse
        Try
            JobName = SetLength(JobName)
            sessManager = New QBSessionManagerClass()
            Dim msgSetRq As IMsgSetRequest = sessManager.CreateMsgSetRequest("US", 2, 0)
            msgSetRq.Attributes.OnError = ENRqOnError.roeContinue
            Dim custAdd As ICustomerAdd = msgSetRq.AppendCustomerAddRq
            custAdd.Name.SetValue(JobName)
            custAdd.ParentRef.FullName.SetValue(Name)
            'step2: begin QB session and send the request
            sessManager.OpenConnection("App", "TimeLive Quickbooks")
            sessManager.BeginSession("", ENOpenMode.omDontCare)
            msgSetRs = sessManager.DoRequests(msgSetRq)
            If Not msgSetRs.ResponseList.GetAt(0).StatusMessage.Contains("already in use") Then
                If msgSetRs.ResponseList.GetAt(0).StatusSeverity = "Error" Then
                    Throw New Exception(msgSetRs.ResponseList.GetAt(0).StatusMessage)
                End If
            End If
        Catch ex As Exception
            Throw ex
        Finally
            If Not sessManager Is Nothing Then
                sessManager.EndSession()
                sessManager.CloseConnection()
            End If
        End Try
    End Sub
    Public Sub AddSubJobInQB(ByVal SubJobName As String, ByVal ParentJobName As String)
        'step1: create QBFC session manager and prepare the request
        Dim sessManager As QBSessionManager
        Dim msgSetRs As IMsgSetResponse
        Try
            sessManager = New QBSessionManagerClass()
            Dim msgSetRq As IMsgSetRequest = sessManager.CreateMsgSetRequest("US", 2, 0)
            msgSetRq.Attributes.OnError = ENRqOnError.roeContinue
            Dim custAdd As ICustomerAdd = msgSetRq.AppendCustomerAddRq
            custAdd.Name.SetValue(SubJobName)
            custAdd.ParentRef.FullName.SetValue(ParentJobName)
            'step2: begin QB session and send the request
            sessManager.OpenConnection("App", "TimeLive Quickbooks")
            sessManager.BeginSession("", ENOpenMode.omDontCare)
            msgSetRs = sessManager.DoRequests(msgSetRq)
            If Not msgSetRs.ResponseList.GetAt(0).StatusMessage.Contains("already in use") Then
                If msgSetRs.ResponseList.GetAt(0).StatusSeverity = "Error" Then
                    Throw New Exception(msgSetRs.ResponseList.GetAt(0).StatusMessage)
                End If
            End If
        Catch ex As Exception
            Throw ex
        Finally
            If Not sessManager Is Nothing Then
                sessManager.EndSession()
                sessManager.CloseConnection()
            End If
        End Try
    End Sub
    Public Sub AddItemInQB(ByVal ItemName As String, ByVal ServiceItemAccount As String)
        'step1: create QBFC session manager and prepare the request
        Dim sessManager As QBSessionManager
        Dim msgSetRs As IMsgSetResponse
        Try
            ItemName = SetLength(ItemName)
            sessManager = New QBSessionManagerClass()
            Dim msgSetRq As IMsgSetRequest = sessManager.CreateMsgSetRequest("US", 2, 0)
            msgSetRq.Attributes.OnError = ENRqOnError.roeContinue
            Dim AccountAdd As IAccountAdd = msgSetRq.AppendAccountAddRq
            AccountAdd.Name.SetValue(ServiceItemAccount)
            AccountAdd.AccountType.SetValue(ENAccountType.atExpense)
            Dim ItemAdd As IItemServiceAdd = msgSetRq.AppendItemServiceAddRq
            ItemAdd.Name.SetValue(ItemName)
            ItemAdd.ORSalesPurchase.SalesOrPurchase.AccountRef.FullName.SetValue(ServiceItemAccount)
            'step2: begin QB session and send the request
            sessManager.OpenConnection("App", "TimeLive Quickbooks")
            sessManager.BeginSession("", ENOpenMode.omDontCare)
            msgSetRs = sessManager.DoRequests(msgSetRq)
            If Not msgSetRs.ResponseList.GetAt(0).StatusMessage.Contains("already in use") Then
                If msgSetRs.ResponseList.GetAt(0).StatusSeverity = "Error" Then
                    Throw New Exception(msgSetRs.ResponseList.GetAt(0).StatusMessage)
                End If
            End If
        Catch ex As Exception
            Throw ex
        Finally
            If Not sessManager Is Nothing Then
                sessManager.EndSession()
                sessManager.CloseConnection()
            End If
        End Try
    End Sub
    Public Sub AddJobItemInQB(ByVal SubItemName As String, ByVal ParentItemName As String, ByVal ServiceItemAccount As String)
        'step1: create QBFC session manager and prepare the request
        Dim sessManager As QBSessionManager
        Dim msgSetRs As IMsgSetResponse
        Try
            sessManager = New QBSessionManagerClass()
            Dim msgSetRq As IMsgSetRequest = sessManager.CreateMsgSetRequest("US", 2, 0)
            msgSetRq.Attributes.OnError = ENRqOnError.roeContinue
            Dim AccountAdd As IAccountAdd = msgSetRq.AppendAccountAddRq
            AccountAdd.Name.SetValue(ServiceItemAccount)
            AccountAdd.AccountType.SetValue(ENAccountType.atExpense)
            Dim ItemAdd As IItemServiceAdd = msgSetRq.AppendItemServiceAddRq
            ItemAdd.Name.SetValue(SubItemName)
            If ParentItemName <> "" Then
                ItemAdd.ParentRef.FullName.SetValue(ParentItemName)
            End If
            ItemAdd.ORSalesPurchase.SalesOrPurchase.AccountRef.FullName.SetValue(ServiceItemAccount)
            'step2: begin QB session and send the request
            sessManager.OpenConnection("App", "TimeLive Quickbooks")
            sessManager.BeginSession("", ENOpenMode.omDontCare)
            msgSetRs = sessManager.DoRequests(msgSetRq)
            If Not msgSetRs.ResponseList.GetAt(0).StatusMessage.Contains("already in use") Then
                If msgSetRs.ResponseList.GetAt(0).StatusSeverity = "Error" Then
                    Throw New Exception(msgSetRs.ResponseList.GetAt(0).StatusMessage)
                End If
            End If
        Catch ex As Exception
            Throw ex
        Finally
            If Not sessManager Is Nothing Then
                sessManager.EndSession()
                sessManager.CloseConnection()
            End If
        End Try
    End Sub
    Public Sub AddSubItemInQB(ByVal SubItemName As String, ByVal ParentItemName As String, ByVal ServiceItemAccount As String)
        'step1: create QBFC session manager and prepare the request
        Dim sessManager As QBSessionManager
        Dim msgSetRs As IMsgSetResponse
        Try
            sessManager = New QBSessionManagerClass()
            Dim msgSetRq As IMsgSetRequest = sessManager.CreateMsgSetRequest("US", 2, 0)
            msgSetRq.Attributes.OnError = ENRqOnError.roeContinue
            Dim ItemAdd As IItemServiceAdd = msgSetRq.AppendItemServiceAddRq
            ItemAdd.Name.SetValue(SubItemName)
            ItemAdd.ParentRef.FullName.SetValue(ParentItemName)
            ItemAdd.ORSalesPurchase.SalesOrPurchase.AccountRef.FullName.SetValue(ServiceItemAccount)
            'step2: begin QB session and send the request
            sessManager.OpenConnection("App", "TimeLive Quickbooks")
            sessManager.BeginSession("", ENOpenMode.omDontCare)
            msgSetRs = sessManager.DoRequests(msgSetRq)
            If Not msgSetRs.ResponseList.GetAt(0).StatusMessage.Contains("already in use") Then
                If msgSetRs.ResponseList.GetAt(0).StatusSeverity = "Error" Then
                    Throw New Exception(msgSetRs.ResponseList.GetAt(0).StatusMessage)
                End If
            End If
        Catch ex As Exception
            Throw ex
        Finally
            If Not sessManager Is Nothing Then
                sessManager.EndSession()
                sessManager.CloseConnection()
            End If
        End Try
    End Sub
    Private Sub btnGetAndAddJobOrItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetAndAddJobOrItem.Click
        pgbar.Value = 0
        Try
            If rbJob.Checked = True Then
                AddJobs()
            ElseIf rbItem.Checked = True Then
                AddItems()
            ElseIf rbtJobitems.Checked = True Then
                AddJobsAndItems()
            End If
            MessageBox.Show("Records transferred successfully")
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Public Sub AddJobsAndItems()
        Dim objProjectServices As New Services.TimeLive.Projects.Projects
        Dim authentication As New Services.TimeLive.Projects.SecuredWebServiceHeader
        authentication.AuthenticatedToken = p_token
        objProjectServices.SecuredWebServiceHeaderValue = authentication        
        Dim objProjectArray() As Object
        objProjectArray = objProjectServices.GetProjects()
        Dim objProject As New Services.TimeLive.Projects.Project

        Dim objTaskServices As New Services.TimeLive.Tasks.Tasks
        Dim authentication2 As New Services.TimeLive.Tasks.SecuredWebServiceHeader
        authentication2.AuthenticatedToken = p_token
        objTaskServices.SecuredWebServiceHeaderValue = authentication2
        Dim objTaskArray() As Object
        objTaskArray = objTaskServices.GetTasks()
        Dim objTask As New Services.TimeLive.Tasks.Task

        Dim pblenth As Integer = objProjectArray.Length + objTaskArray.Length - 1
        If pblenth >= 0 Then
            pgbar.Maximum = pblenth
        End If

        ''Step 1 Add Jobs In QuickBooks
        For n As Integer = 0 To objProjectArray.Length - 1
            pgbar.Increment(n)
            objProject = objProjectArray(n)
            With objProject
                AddJobInQB(.ProjectName, .ClientName)
            End With
        Next

        ''Step 2 Add Items In QuickBooks
        For n As Integer = 0 To objTaskArray.Length - 1
            pgbar.Increment(n)
            objTask = objTaskArray(n)
            With objTask
                AddJobItemInQB(.TaskName, .JobItemParent, .Code)
            End With
        Next
    End Sub
    Public Sub AddJobs()
        Dim objProjectServices As New Services.TimeLive.Projects.Projects
        Dim authentication As New Services.TimeLive.Projects.SecuredWebServiceHeader
        authentication.AuthenticatedToken = p_token
        objProjectServices.SecuredWebServiceHeaderValue = authentication
        Dim objProjectArray() As Object
        objProjectArray = objProjectServices.GetProjects()
        Dim objProject As New Services.TimeLive.Projects.Project

        Dim objTaskServices As New Services.TimeLive.Tasks.Tasks
        Dim authentication2 As New Services.TimeLive.Tasks.SecuredWebServiceHeader
        authentication2.AuthenticatedToken = p_token
        objTaskServices.SecuredWebServiceHeaderValue = authentication2
        Dim objTaskArray() As Object
        objTaskArray = objTaskServices.GetTasks()
        Dim objTask As New Services.TimeLive.Tasks.Task

        Dim pblenth As Integer = objProjectArray.Length + objTaskArray.Length - 1
        If pblenth >= 0 Then
            pgbar.Maximum = pblenth
        End If

        ''Step 1 Add Jobs In QuickBooks
        For n As Integer = 0 To objProjectArray.Length - 1
            pgbar.Increment(n)
            objProject = objProjectArray(n)
            With objProject

                AddJobInQB(.ProjectName, .ClientName)
            End With
        Next

        ''Step 2 Add Sub Jobs In QuickBooks
        For n As Integer = 0 To objTaskArray.Length - 1
            pgbar.Increment(n)
            objTask = objTaskArray(n)
            With objTask
                AddSubJobInQB(.TaskName, .JobParent)
            End With
        Next
    End Sub
    Public Sub AddItems()
        Dim objProjectServices As New Services.TimeLive.Projects.Projects
        Dim authentication As New Services.TimeLive.Projects.SecuredWebServiceHeader
        authentication.AuthenticatedToken = p_token
        objProjectServices.SecuredWebServiceHeaderValue = authentication
        Dim objProjectArray() As Object
        objProjectArray = objProjectServices.GetProjects()
        Dim objProject As New Services.TimeLive.Projects.Project

        Dim objTaskServices As New Services.TimeLive.Tasks.Tasks
        Dim authentication2 As New Services.TimeLive.Tasks.SecuredWebServiceHeader
        authentication2.AuthenticatedToken = p_token
        objTaskServices.SecuredWebServiceHeaderValue = authentication2
        Dim objTaskArray() As Object
        objTaskArray = objTaskServices.GetTasks()
        Dim objTask As New Services.TimeLive.Tasks.Task

        Dim pblenth As Integer = objProjectArray.Length + objTaskArray.Length - 1
        If pblenth >= 0 Then
            pgbar.Maximum = pblenth
        End If

        ''Step 1 Add Items In QuickBooks
        For n As Integer = 0 To objProjectArray.Length - 1
            pgbar.Increment(n)
            objProject = objProjectArray(n)
            With objProject
                AddItemInQB(.ProjectName, .ProjectCode)
            End With
        Next

        ''Step 2 Add Sub Items In QuickBooks
        For n As Integer = 0 To objTaskArray.Length - 1
            pgbar.Increment(n)
            objTask = objTaskArray(n)
            With objTask
                AddSubItemInQB(.TaskName, .ItemParent, .Code)
            End With
        Next
    End Sub
    Public Function SetLength(ByVal Name As String) As String
        If Name.Length > 31 Then
            Name = Name.Substring(0, 31)
        End If
        Return Name
    End Function

    Private Sub JobOrItem_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        'MainMenu.Enabled = True
    End Sub
End Class