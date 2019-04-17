Imports QBFC13Lib

Public Class Sync_TLtoQB_Relationships
    ''' <summary>
    ''' Sync the employee data from QB. Print out employees that are in TL but not QB
    ''' </summary>
    Sub SyncRelationshipData(ByVal p_token As String, Optional ByVal UI As Boolean = True)
        Dim create As Boolean = True

        If UI Then
            create = MsgBox("Sync Relationships?", MsgBoxStyle.YesNo, "Warning!") = MsgBoxResult.Yes
        End If
        If create Then
            My.Forms.MAIN.History("Syncing Relationships Data", "n")
            Try
                ' Connect to Time Live
                Dim objTaskServices As New Services.TimeLive.Tasks.Tasks
                Dim authentication As New Services.TimeLive.Tasks.SecuredWebServiceHeader
                authentication.AuthenticatedToken = p_token
                objTaskServices.SecuredWebServiceHeaderValue = authentication

                Dim chargingRelationshipAdapter As New QB_TL_IDsTableAdapters.ChargingRelationshipsTableAdapter

                'Dim TLProjectRelationshipAdapter As New TimeLiveDataSetTableAdapters.AccountProjectEmployeeTableAdapter
                'Dim TLProjectRelationships As TimeLiveDataSet.AccountProjectEmployeeDataTable = TLProjectRelationshipAdapter.GetProjectEmployeeData()

                'For Each row As DataRow In TLProjectRelationships.Select
                '   Add_Relationship(chargingRelationshipAdapter, row)
                'Next

                Dim TLTaskRelationshipAdapter As New TimeLiveDataSetTableAdapters.AccountProjectTaskEmployeeTableAdapter
                Dim TLTaskRelationships As TimeLiveDataSet.AccountProjectTaskEmployeeDataTable = TLTaskRelationshipAdapter.GetTaskEmployeeData()
                Dim objTaskArray() As Object
                objTaskArray = objTaskServices.GetTasks
                Dim objTask As New Services.TimeLive.Tasks.Task

                For Each row As DataRow In TLTaskRelationships.Select
                    ' TODO: Check if task is parent task
                    Add_Relationship(chargingRelationshipAdapter, row)
                Next
            Catch ex As Exception
                If UI Then
                    MsgBox(ex.Message)
                Else
                    Throw ex
                End If
            End Try
        End If
    End Sub

    ''' <summary>
    ''' Adds the relationship between an employee and job within the given row into the chargining relationships table
    ''' </summary>
    ''' <param name="chargingRelationshipAdapter"></param>
    ''' <param name="row">Row with an employee and job/subjob</param>
    Sub Add_Relationship(ByRef chargingRelationshipAdapter As QB_TL_IDsTableAdapters.ChargingRelationshipsTableAdapter, ByVal row As DataRow)
        Dim TLProjectID As String = row(2).ToString ' AccountProjectID
        Dim TLEmployeeID As String = row(3).ToString ' AccountEmployeeID

        Dim EmployeeAdapter As New QB_TL_IDsTableAdapters.EmployeesTableAdapter
        Dim VendorAdapter As New QB_TL_IDsTableAdapters.VendorsTableAdapter

        Dim QBEmployeeID As String = EmployeeAdapter.GetCorrespondingQB_IDfromTL_ID(TLEmployeeID)

        ' Checks if it was actually a vendor
        If QBEmployeeID Is Nothing Then
            QBEmployeeID = VendorAdapter.GetCorrespondingQB_IDfromTL_ID(TLEmployeeID)
        End If

        Dim Job_SubjobAdapter As New QB_TL_IDsTableAdapters.Jobs_SubJobsTableAdapter
        Dim QBJobSubJobID As String = Job_SubjobAdapter.GetCorrespondingQB_IDfromTL_ID(TLProjectID)

        Dim numRel As Integer = chargingRelationshipAdapter.NumEmployeeJobRelationships(QBEmployeeID, QBJobSubJobID)

        ' If the relationship in TL is not in the ChargingRelationship table, add it
        If Not (QBEmployeeID Is Nothing Or QBJobSubJobID Is Nothing) Then

            QBEmployeeID = QBEmployeeID.Trim
            QBJobSubJobID = QBJobSubJobID.Trim

            Dim JobSubJobName As String = Job_SubjobAdapter.GetNamefromID(QBJobSubJobID).Trim
            Dim EmployeeName As String = EmployeeAdapter.GetNamefromID(QBEmployeeID)
            If EmployeeName Is Nothing Then
                EmployeeName = VendorAdapter.GetNamefromID(QBEmployeeID).Trim
            Else
                EmployeeName = EmployeeName.Trim
            End If

            If numRel = 0 Then
                My.Forms.MAIN.History("Adding Time Relationship between " + EmployeeName + " and " + JobSubJobName, "N")
                chargingRelationshipAdapter.AddEmployeeJobRelationship(QBEmployeeID, QBJobSubJobID)
            Else
                My.Forms.MAIN.History(If(numRel > 1, "More than one ", "One ") + " relationship" + " between " + EmployeeName + " and " + JobSubJobName + " already exists", "i")
            End If
        End If
    End Sub
End Class