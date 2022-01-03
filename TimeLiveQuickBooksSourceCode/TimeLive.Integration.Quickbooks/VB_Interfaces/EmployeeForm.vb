Public Class EmployeeForm
    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click
        Me.DialogResult = DialogResult.OK
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub EmployeeForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub mTxtHiredDate_MaskInputRejected(sender As Object, e As MaskInputRejectedEventArgs) Handles mTxtHiredDate.MaskInputRejected

    End Sub

    Private Sub lblPassword_Click(sender As Object, e As EventArgs) Handles lblPassword.Click

    End Sub
End Class