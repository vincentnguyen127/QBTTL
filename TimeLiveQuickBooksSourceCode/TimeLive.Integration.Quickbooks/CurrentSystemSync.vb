'Imports QBFC11Lib
Imports QBFC13Lib

Public Class CurrentSystemSync
    Private p_token As String
    Private p_AccountId As String

    ''' <summary>
    ''' Runs a System Sync from TimeLive which adds entities into the database and into Quickbooks if not present
    ''' </summary>
    ''' <param name="token"></param>
    ''' <param name="AccountId"></param>
    Public Sub PassToken(ByVal token As String, ByVal AccountId As String)
        If token Is Nothing Then
            MsgBox("Please Login")
        Else
            p_AccountId = AccountId
            p_token = token

            ' Sync Customers
            Dim syncCustomers As New Sync_TLtoQB_Customer
            If MsgBox("Sync Customers?", MsgBoxStyle.YesNo, "Warning!") = MsgBoxResult.Yes Then
                syncCustomers.SyncCustomerData(p_token)
            End If

            ' Sync Employees
            Dim syncEmployees As New Sync_TLtoQB_Employee
            If MsgBox("Sync Employees?", MsgBoxStyle.YesNo, "Warning!") = MsgBoxResult.Yes Then
                syncEmployees.SyncEmployeeData(p_token)
            End If

            ' Sync Vendors
            Dim syncVendors As New Sync_TLtoQB_Vendor
            If MsgBox("Sync Vendors?", MsgBoxStyle.YesNo, "Warning!") = MsgBoxResult.Yes Then
                syncVendors.SyncVendorData(p_token)
            End If

            ' Sync Jobs/SubJobs
            Dim syncJobs As New Sync_TLtoQB_JoborItem
            If MsgBox("Sync Jobs/SubJobs?", MsgBoxStyle.YesNo, "Warning!") = MsgBoxResult.Yes Then
                syncJobs.SyncJobsSubJobData(p_token)
            End If

            ' Sync Relationships
            Dim syncRelationships As New Sync_TLtoQB_Relationships
            If MsgBox("Sync Relationships?", MsgBoxStyle.YesNo, "Warning!") = MsgBoxResult.Yes Then
                syncRelationships.SyncRelationshipData(p_token)
            End If

            My.Forms.MAIN.History("Sync Complete", "n")
            End If
    End Sub

    ''' <summary>
    ''' Gets Time Entries from QB
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
            'MAIN.QUITQBSESSION()
            Throw ex
            'Finally
            '    If Not sessManager Is Nothing Then
            '       sessManager.EndSession()
            '       sessManager.CloseConnection()
            '    End If
        End Try
    End Sub

End Class
