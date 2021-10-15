Public Class EmployeeForm
    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click
        Me.DialogResult = DialogResult.OK
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub EmployeeForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class