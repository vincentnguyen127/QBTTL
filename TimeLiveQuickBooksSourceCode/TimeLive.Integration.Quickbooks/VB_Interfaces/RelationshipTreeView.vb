Imports QBFC13Lib

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

    End Sub

    Private Sub LabelRelationshipTreeView_Click(sender As Object, e As EventArgs) Handles LabelRelationshipTreeView.Click

    End Sub

    'Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click
    '    Me.Close()
    'End Sub
    Private Function btnOK_Click() Handles btnOK.Click
        Me.Close()
    End Function

    Private Sub btnAdd_Click(sender As Object, e As EventArgs) Handles btnAdd.Click


        Dim account As String
        Dim itemName As String
        Dim msgSetRs As IMsgSetResponse
        Dim msgSetRq As IMsgSetRequest = MAIN.SESSMANAGER.CreateMsgSetRequest("US", 2, 0)

        Dim newForm As NewItemForm = New NewItemForm()

        'Getting list of account for the combobox 

        msgSetRq.Attributes.OnError = ENRqOnError.roeContinue
        msgSetRq.AppendAccountQueryRq()
        msgSetRs = MAIN.SESSMANAGER.DoRequests(msgSetRq)
        Dim respList As IResponseList = msgSetRs.ResponseList
        Dim resp As IResponse
        resp = respList.GetAt(0)
        If (resp.StatusCode = 0) Then
            Dim retList = resp.Detail
            For i As Integer = 0 To retList.Count - 1
                Dim ret = retList.GetAt(i)
                Dim name As String = ret.Name.GetValue
                newForm.ComboBoxAccount.Items.Add(name)
            Next
        End If

        If TreeViewRelationship.SelectedNode Is Nothing Then
            MessageBox.Show("Please select a node first")
            Exit Sub
        End If

        'Get the inputs from the form

        If DialogResult.OK = newForm.ShowDialog() Then
            account = newForm.ComboBoxAccount.Text
            itemName = newForm.TextBoxName.Text.Trim()

            Try


                Dim node As String = TreeViewRelationship.SelectedNode.FullPath

                Dim selectedNode As String = Replace(node, "\", ":")


                Dim newMsgSetRq As IMsgSetRequest = MAIN.SESSMANAGER.CreateMsgSetRequest("US", 2, 0)
                Dim itemAdd As IItemServiceAdd = newMsgSetRq.AppendItemServiceAddRq
                itemAdd.Name.SetValue(itemName)
                itemAdd.ParentRef.FullName.SetValue(selectedNode)
                itemAdd.ORSalesPurchase.SalesOrPurchase.AccountRef.FullName.SetValue(account)
                msgSetRs = MAIN.SESSMANAGER.DoRequests(newMsgSetRq)
                Dim res As IResponse = msgSetRs.ResponseList.GetAt(0)

            Catch ex As Exception

            End Try

        End If

        Me.Close()

    End Sub

    Private Sub RelationshipTreeView_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub
End Class