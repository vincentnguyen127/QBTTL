Imports Interop.QBFC10
Public Class MainMenu
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
    Private Sub btnQBEmployees_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnQBEmployees.Click
        Dim QBEmployee As New QBEmployee
        QBEmployee.MdiParent = Main
        Me.Enabled = False
        QBEmployee.Show(p_token, p_AccountId)
    End Sub
    Private Sub btnQBCustomers_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnQBCustomers.Click
        Dim QBCustomer As New QBCustomer
        QBCustomer.MdiParent = Main
        Me.Enabled = False
        QBCustomer.Show(p_token, p_AccountId)
    End Sub
    Private Sub btnQBJobOrItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnQBJobOrItem.Click
        Dim QBJobOrItem As New QBJobOrItem
        QBJobOrItem.MdiParent = Main
        Me.Enabled = False
        QBJobOrItem.Show(p_token, p_AccountId)
    End Sub
    Private Sub btnQBTimeTracking_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnQBTimeTracking.Click
        Dim QBTimeTracking As New QBTimeTracking
        QBTimeTracking.MdiParent = Main
        Me.Enabled = False
        QBTimeTracking.Show(p_token, p_AccountId)
    End Sub
    Private Sub btnQBVendors_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnQBVendors.Click
        Dim QBVendor As New QBVendor
        QBVendor.MdiParent = Main
        Me.Enabled = False
        QBVendor.Show(p_token, p_AccountId)
    End Sub
    Private Sub btnQBExpenseTracking_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnQBExpenseTracking.Click
        Dim QBVendorBill As New QBVendorBill
        QBVendorBill.MdiParent = Main
        Me.Enabled = False
        QBVendorBill.Show(p_token, p_AccountId)
    End Sub
    Private Sub btnTLEmployees_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTLEmployees.Click
        Dim TLEmployee As New TLEmployee
        TLEmployee.MdiParent = Main
        Me.Enabled = False
        TLEmployee.Show(p_token, p_AccountId)
    End Sub
    Private Sub btnTLClients_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTLClients.Click
        Dim TLClient As New TLClient
        TLClient.MdiParent = Main
        Me.Enabled = False
        TLClient.Show(p_token, p_AccountId)
    End Sub
    Private Sub btnTLProjectAndTask_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTLProjectAndTask.Click
        Dim TLProjectAndTask As New TLProjectAndTask
        TLProjectAndTask.MdiParent = Main
        Me.Enabled = False
        TLProjectAndTask.Show(p_token, p_AccountId)
    End Sub
    Private Sub btnQBAccounts_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnQBAccounts.Click
        Dim QBAccount As New QBAccount
        QBAccount.MdiParent = Main
        Me.Enabled = False
        QBAccount.Show(p_token, p_AccountId)
    End Sub
    Private Sub MainMenu_FormClosed(sender As System.Object, e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed

        Main.MainMenuStrip.Items(0).Enabled = True
    End Sub
    Private Sub btnTLVendors_Click(sender As System.Object, e As System.EventArgs) Handles btnTLVendors.Click
        Dim TLVendor As New TLVendor
        TLVendor.MdiParent = Main
        Me.Enabled = False
        TLVendor.Show(p_token, p_AccountId)
    End Sub
    Private Sub IntegratedUI_btn_Click(sender As Object, e As EventArgs) Handles IntegratedUI_btn.Click
        Dim IntegratedUI As New IntegratedUI
        IntegratedUI.MdiParent = MAIN
        Me.Enabled = False
        IntegratedUI.Show(p_token, p_AccountId)
    End Sub

    Private Sub MainMenu_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        'IntegratedUI_btn_Click(sender, e)
        '  SetUpTimer(New TimeSpan(16, 00, 00));
        ' autoRun()
    End Sub




End Class