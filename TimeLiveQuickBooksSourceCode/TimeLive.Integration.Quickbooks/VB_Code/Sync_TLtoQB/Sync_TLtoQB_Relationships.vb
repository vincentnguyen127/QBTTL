Imports QBFC13Lib

Public Class Sync_TLtoQB_Relationships
    ''' <summary>
    ''' Sync the employee data from QB. Print out employees that are in TL but not QB
    ''' </summary>
    Sub SyncRelationshipData(ByVal p_token As String, Optional ByVal UI As Boolean = True)
        My.Forms.MAIN.History("Syncing Relationships Data", "n")
        Try
            Dim objTaskServices As Services.TimeLive.Tasks.Tasks = MAIN.connect_TL_tasks(p_token)

            Dim chargingRelationshipAdapter As New QB_TL_IDsTableAdapters.ChargingRelationshipsTableAdapter
            Dim TLTaskRelationshipAdapter As New TimeLiveDataSetTableAdapters.AccountProjectTaskEmployeeTableAdapter
            Dim TLEmployeeAdapter As New QB_TL_IDsTableAdapters.EmployeesTableAdapter
            Dim TLJobAdapter As New QB_TL_IDsTableAdapters.Jobs_SubJobsTableAdapter

            Dim TLTaskRelationships As TimeLiveDataSet.AccountProjectTaskEmployeeDataTable = TLTaskRelationshipAdapter.GetTaskEmployeeData()

            For Each row As DataRow In TLTaskRelationships.Select
                ' Note: NumSubTasks assumes TL_Name in Tasks DB formatted - Customer:Job:SubJob:SubSubJob:...
                ' Note: Checks number subtasks based on first entry that has the corresponding TimeLive ID
                '       If multiple jobs have same TL_ID stored, will get the wrong name and thus search for the wrong sub task
                Dim job_id As String = row(2).ToString
                If job_id IsNot Nothing Then
                    job_id = job_id.Trim
                End If
                Dim employee_id As String = row(3).ToString
                If employee_id IsNot Nothing Then
                    employee_id = employee_id.Trim
                End If

                Dim numSubTasks As Integer
                Try
                    numSubTasks = TLJobAdapter.NumSubTasks(job_id)
                Catch ex As Exception
                    numSubTasks = 1 ' If exception getting num sub tasks, treat as 1 so that we do not add relationship
                End Try

                ' Only add relationships to non-parent projects/tasks
                If numSubTasks = 0 Then
                    ' Returns -1 when user selected Cancel, thus concluding the addition of any other relationships
                    If Add_Relationship(chargingRelationshipAdapter, job_id, employee_id) = -1 Then
                        Exit For
                    End If
                    'End If
                End If
            Next
        Catch ex As Exception
            If UI Then
                MsgBox(ex.Message)
            Else
                Throw ex
            End If
        End Try
    End Sub

    ''' <summary>
    ''' Adds the relationship between an employee and job within the given row into the chargining relationships table
    ''' </summary>
    ''' <param name="chargingRelationshipAdapter"></param>
    ''' <param name="TLProjectID">ID of the project/task in TimeLive</param>
    ''' <param name="TLEmployeeID">ID of the employee in TimeLive</param>
    ''' <returns>
    ''' -1 if cancel, 0 if relationship not added but should have, 
    ''' 1 if relationship was added or already existed
    ''' </returns>
    Function Add_Relationship(ByRef chargingRelationshipAdapter As QB_TL_IDsTableAdapters.ChargingRelationshipsTableAdapter,
                         ByVal TLProjectID As String, ByVal TLEmployeeID As String, Optional itemName As String = Nothing, Optional UI As Boolean = True) As Integer
        If TLEmployeeID Is Nothing Or TLProjectID Is Nothing Then
            Return 0
        End If

        Dim EmployeeAdapter As New QB_TL_IDsTableAdapters.EmployeesTableAdapter
        Dim VendorAdapter As New QB_TL_IDsTableAdapters.VendorsTableAdapter

        Dim QBEmployeeID As String = EmployeeAdapter.GetCorrespondingQB_IDfromTL_ID(TLEmployeeID)

        ' Checks if it was actually a vendor
        If QBEmployeeID Is Nothing Then
            QBEmployeeID = VendorAdapter.GetCorrespondingQB_IDfromTL_ID(TLEmployeeID)
        End If

        Dim Job_SubjobAdapter As New QB_TL_IDsTableAdapters.Jobs_SubJobsTableAdapter
        Dim QBJobSubJobID As String = Job_SubjobAdapter.GetCorrespondingQB_IDfromTL_ID(TLProjectID)

        ' If the relationship in TL is not in the ChargingRelationship table, add it
        If QBEmployeeID IsNot Nothing And QBJobSubJobID IsNot Nothing Then

            QBEmployeeID = QBEmployeeID.Trim
            QBJobSubJobID = QBJobSubJobID.Trim

            Dim JobSubJobName As String = Job_SubjobAdapter.GetNamefromTLID(TLProjectID)

            Dim EmployeeName As String = EmployeeAdapter.GetNamefromID(QBEmployeeID)
            If EmployeeName Is Nothing Then
                EmployeeName = VendorAdapter.GetNamefromID(QBEmployeeID)
            End If

            Dim QBItemID As String = Nothing
            ' Get Item QB ID from QB, if it exists
            If itemName IsNot Nothing Then
                Dim msgSetRq As IMsgSetRequest = MAIN.SESSMANAGER.CreateMsgSetRequest("US", 2, 0)
                msgSetRq.Attributes.OnError = ENRqOnError.roeContinue
                Dim itemQueryRq As IItemQuery = msgSetRq.AppendItemQueryRq
                itemQueryRq.ORListQuery.FullNameList.Add(itemName)

                Dim msgSetRs As IMsgSetResponse = MAIN.SESSMANAGER.DoRequests(msgSetRq)
                Dim response As IResponse = msgSetRs.ResponseList.GetAt(0)
                Dim itemRetList As IORItemRetList
                itemRetList = response.Detail

                If itemRetList IsNot Nothing Then
                    Dim ret As IORItemRet = itemRetList.GetAt(0)
                    QBItemID = ret.ItemServiceRet.ListID.GetValue()
                End If

            End If

            If JobSubJobName IsNot Nothing Then JobSubJobName = JobSubJobName.Trim
            If EmployeeName IsNot Nothing Then EmployeeName = EmployeeName.Trim

            Dim numRel As Integer = chargingRelationshipAdapter.NumEmployeeJobRelationships(QBEmployeeID, QBJobSubJobID)

            ' If there is no current relationship, ask them before adding when UI is present
            If numRel = 0 Then
                Dim msg_resp As MsgBoxResult
                If UI Then
                    msg_resp = MsgBox("Add new relationship from TimeLive between" + vbCrLf + "Employee: " + EmployeeName + vbCrLf + "Task: " +
                                        JobSubJobName + "?", MsgBoxStyle.YesNoCancel, "Warning!")
                    If msg_resp = MsgBoxResult.Cancel Then
                        Return -1
                    End If
                End If

                If Not UI Or msg_resp = MsgBoxResult.Yes Then
                    My.Forms.MAIN.History("Adding Time Relationship between " + EmployeeName + " and " + JobSubJobName + " to local database", "N")
                    Try
                        If QBItemID Is Nothing Then
                            chargingRelationshipAdapter.AddEmployeeJobRelationship(QBEmployeeID, QBJobSubJobID)
                        Else
                            chargingRelationshipAdapter.AddEmployeeJobItemRelationship(QBEmployeeID, QBJobSubJobID, QBItemID)
                        End If
                    Catch ex As Exception
                        My.Forms.MAIN.History("Exception when adding Relationship: " + ex.ToString, "N")
                        Return 0
                    End Try
                    Return 1
                Else
                    Return 0
                End If
            Else
                If QBItemID IsNot Nothing Then
                    chargingRelationshipAdapter.UpdateItem(QBEmployeeID, QBJobSubJobID, QBItemID)
                End If

                My.Forms.MAIN.History(If(numRel > 1, "More than one ", "One ") + " relationship" + " between " + EmployeeName + " And " + JobSubJobName + " already exists", "i")
                Return 1
            End If
        Else
            My.Forms.MAIN.History("QuickBooks Job ID Or QuickBooks Employee ID was invalid", "n")
            Return 0
        End If
    End Function
End Class