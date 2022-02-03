Imports System.ComponentModel
Imports System.Text.RegularExpressions
Public Class ModifyForm


    Private Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnOK.Click
        If Me.TxtName.Text = "" Then
            MessageBox.Show("Name is required field. Please provide the information and continue.")
        End If

        'Dim regexEmail As Regex = New Regex("^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$")
        'Dim isEmailValid As Boolean = regexEmail.IsMatch(txtEmail.Text.Trim)
        'If Not isEmailValid Then
        '    MessageBox.Show("Invalid Email")

        'End If

        'Dim phoneRegex As New Regex("\d{3}-\d{3}-\d{4}")
        'Dim isPhoneValid As Boolean = phoneRegex.IsMatch(txtTelephone2.Text.Trim)
        'If Not isPhoneValid Then
        '    MessageBox.Show("Invalid Phone" & vbCrLf & "Format ###-###-####")
        'End If

        'Dim faxRegex As New Regex("^\+?[0-9]{6,}$")
        'Dim isFaxValid As Boolean = faxRegex.IsMatch(txtFax.Text.Trim)
        'If Not isFaxValid Then
        '    MessageBox.Show("Fax is Invalid" & vbCrLf & "Number has at least 6 digits")
        'End If

        'If isEmailValid And Not String.IsNullOrEmpty(TxtName.Text) And isPhoneValid And isFaxValid Then
        '    Me.DialogResult = DialogResult.OK
        'End If
        If Not String.IsNullOrEmpty(TxtName.Text) Then
            '  If String.IsNullOrEmpty(txtEmail.Text) Or String.IsNullOrEmpty(txtFax.Text) Or String.IsNullOrEmpty(txtTelephone2.Text) Then
            Me.DialogResult = DialogResult.OK
            '  Else

        End If


        'End If
    End Sub

    Private Sub btnCancel_Click_1(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub TxtName_TextChanged(sender As Object, e As EventArgs) Handles TxtName.TextChanged

        If Me.TxtName.TextLength >= 41 Then
            MessageBox.Show("The name should be less than 41 chars")
        End If
    End Sub

    Private Sub txtEmail_TextChanged(sender As Object, e As EventArgs) Handles txtEmail.TextChanged

        If Me.txtEmail.TextLength > 1023 Then
            MessageBox.Show("The email should be less than 1023 chars")
        End If



    End Sub

    Private Sub txtFax_TextChanged(sender As Object, e As EventArgs) Handles txtFax.TextChanged
        If Me.txtFax.TextLength > 21 Then
            MessageBox.Show("The fax should be less than 21 chars")
        End If
    End Sub


    Private Sub MaskedTextBox1_MaskInputRejected(sender As Object, e As MaskInputRejectedEventArgs) Handles txtTelephone2.MaskInputRejected
        If Me.txtTelephone2.TextLength > 21 Then
            MessageBox.Show("The telephone should be less than 21 chars")
        End If
    End Sub

    Private Sub TxtName_Validating(sender As Object, e As CancelEventArgs) Handles TxtName.Validating
        'If String.IsNullOrEmpty(TxtName.Text.Trim) Then
        '    ErrorProvider1.SetError(TxtName, "Name is required")
        'Else
        '    ErrorProvider1.SetError(TxtName, String.Empty)
        'End If
    End Sub

    Private Sub ModifyForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub GroupBox1_Enter(sender As Object, e As EventArgs) Handles GroupBox1.Enter

    End Sub

    Private Sub Label11_Click(sender As Object, e As EventArgs)

    End Sub
End Class