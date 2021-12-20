Public Class RelationshipTreeView

    Dim obj_ChangingRelationship As New ChargingRelationship_2

    Private Sub TreeViewRelationship_AfterSelect(sender As Object, e As TreeViewEventArgs) Handles TreeViewRelationship.AfterSelect

        obj_ChangingRelationship.FormAddNewRelationshiop.TextBoxJob.Text = TreeViewRelationship.SelectedNode.Text

    End Sub

End Class