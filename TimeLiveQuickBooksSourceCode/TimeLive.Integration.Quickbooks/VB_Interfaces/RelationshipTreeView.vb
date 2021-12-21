Public Class RelationshipTreeView

    Dim obj_ChangingRelationship As New ChargingRelationship_2

    Private Sub TreeViewRelationship_AfterSelect(sender As Object, e As TreeViewEventArgs) Handles TreeViewRelationship.AfterSelect

        If Me.LabelRelationshipTreeView.Text = "Jobs" Then
            Dim selectedNode As String = TreeViewRelationship.SelectedNode.Text
            If selectedNode <> TreeViewRelationship.TopNode.Text Then
                obj_ChangingRelationship.FormAddNewRelationshiop.TextBoxJob.Text = selectedNode
            End If

        ElseIf Me.LabelRelationshipTreeView.Text = "Items" Then
                obj_ChangingRelationship.FormAddNewRelationshiop.TextBoxItem.Text = TreeViewRelationship.SelectedNode.Text
            End If

            Me.Close()

    End Sub

End Class