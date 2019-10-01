Partial Class QB_TL_IDs
    Partial Public Class Items_SubItemsDataTable
    End Class

    Partial Public Class EmployeesDataTable
    End Class

    Partial Public Class VendorsDataTable
    End Class


    Partial Public Class CustomersDataTable
        Private Sub CustomersDataTable_ColumnChanging(sender As Object, e As DataColumnChangeEventArgs) Handles Me.ColumnChanging
            If (e.Column.ColumnName = Me.QuickBooks_IDColumn.ColumnName) Then
                'Add user code here
            End If

        End Sub

        Private Sub CustomersDataTable_CustomersRowChanging(sender As Object, e As CustomersRowChangeEvent) Handles Me.CustomersRowChanging

        End Sub

    End Class

    Friend Function EmployeesTableAdapter() As Object
        Throw New NotImplementedException()
    End Function
End Class

Namespace QB_TL_IDsTableAdapters
    Partial Public Class EmployeesTableAdapter
    End Class

    Partial Public Class Jobs_SubJobsTableAdapter
    End Class

    Partial Public Class ChargingRelationshipsTableAdapter
    End Class


    Partial Public Class Items_SubItemsTableAdapter
    End Class


    Partial Public Class CustomersTableAdapter
    End Class
End Namespace
