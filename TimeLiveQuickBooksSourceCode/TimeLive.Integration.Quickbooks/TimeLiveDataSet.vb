

Partial Public Class TimeLiveDataSet
    Partial Public Class AccountProjectTaskEmployeeDataTable
        Private Sub AccountProjectTaskEmployeeDataTable_AccountProjectTaskEmployeeRowChanging(sender As Object, e As AccountProjectTaskEmployeeRowChangeEvent) Handles Me.AccountProjectTaskEmployeeRowChanging

        End Sub

    End Class

    Partial Public Class AccountExpenseEntryDataTable
        Private Sub AccountExpenseEntryDataTable_AccountExpenseEntryRowChanging(sender As Object, e As AccountExpenseEntryRowChangeEvent) Handles Me.AccountExpenseEntryRowChanging

        End Sub

        Private Sub AccountExpenseEntryDataTable_ColumnChanging(sender As Object, e As DataColumnChangeEventArgs) Handles Me.ColumnChanging
            If (e.Column.ColumnName = Me.AccountEmployeeExpenseSheetIdColumn.ColumnName) Then
                'Add user code here
            End If

        End Sub

    End Class
End Class

Namespace TimeLiveDataSetTableAdapters

    Partial Public Class AccountExpenseEntryTableAdapter
    End Class
End Namespace
