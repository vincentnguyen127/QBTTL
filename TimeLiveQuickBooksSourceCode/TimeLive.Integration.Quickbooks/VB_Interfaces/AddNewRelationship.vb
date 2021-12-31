Public Class AddNewRelationship
    Private obj_ChargingRelationship_2 As New ChargingRelationship_2



    Private Sub TextBoxEmployee_TextChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub TextBoxEmployee_Click(sender As Object, e As EventArgs)


    End Sub

    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click
        Me.DialogResult = DialogResult.OK

    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub ComboBoxEmployee_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBoxEmployee.SelectedIndexChanged

    End Sub

    Private Sub AddNewRelationship_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub TextBoxJob_TextChanged(sender As Object, e As EventArgs) Handles TextBoxJob.TextChanged

    End Sub

    Public Sub TextBoxJob_Click(sender As Object, e As EventArgs) Handles TextBoxJob.Click
        obj_ChargingRelationship_2.generate_jobs_treeview()
        ' RelationshipTreeView.MdiParent = Me
    End Sub

    Private Sub TextBoxItem_TextChanged(sender As Object, e As EventArgs) Handles TextBoxItem.TextChanged

    End Sub


    Private Sub TextBoxItem_Click(sender As Object, e As EventArgs) Handles TextBoxItem.Click
        obj_ChargingRelationship_2.generate_items_treeview()
    End Sub

    Private Sub btnJobs_Click(sender As Object, e As EventArgs) Handles btnJobs.Click
        obj_ChargingRelationship_2.generate_jobs_treeview()
    End Sub

    Private Sub btnItems_Click(sender As Object, e As EventArgs) Handles btnItems.Click
        obj_ChargingRelationship_2.generate_items_treeview()
    End Sub
End Class