Public Class RelationshipTreeView

    Dim obj_ChangingRelationship As New ChargingRelationship_2

    Private Sub TreeViewRelationship_AfterSelect(sender As Object, e As TreeViewEventArgs) Handles TreeViewRelationship.AfterSelect
        Dim selectedNode As String = Replace(TreeViewRelationship.SelectedNode.FullPath, "\", " --> ")
        If Me.LabelRelationshipTreeView.Text = "Jobs" Then
            If selectedNode <> TreeViewRelationship.TopNode.Text Then
                obj_ChangingRelationship.FormAddNewRelationship.TextBoxJob.Text = selectedNode
            End If

        ElseIf Me.LabelRelationshipTreeView.Text = "Items" Then
            obj_ChangingRelationship.FormAddNewRelationship.TextBoxItem.Text = selectedNode
        End If

            Me.Close()

    End Sub

    Private Sub LabelRelationshipTreeView_Click(sender As Object, e As EventArgs) Handles LabelRelationshipTreeView.Click

    End Sub
End Class