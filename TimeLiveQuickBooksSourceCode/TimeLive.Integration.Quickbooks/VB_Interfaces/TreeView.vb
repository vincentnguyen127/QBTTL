Public Class TreeView
    Private Sub CustomerJobTreeView_AfterSelect(sender As Object, e As TreeViewEventArgs) Handles CustomerJobQBTreeView.AfterSelect
        'TextBoxKey.Text = CustomerJobTreeView.SelectedNode.Text
        Dim selected_node = CustomerJobQBTreeView.SelectedNode.Text


        ' Getting customers and jobs data from Quickbooks

        Dim obj1_QBtoTL_Customer As New QBtoTL_Customer
        Dim customerData As New QBtoTL_Customer.CustomerDataStructureQB

        Dim obj_QBtoTL_JobOrItem As New QBtoTL_JobOrItem
        Dim jobData As New QBtoTL_JobOrItem.JobDataStructureQB


        customerData = obj1_QBtoTL_Customer.GetCustomerQBData()
        jobData = obj_QBtoTL_JobOrItem.GetJobSubJobData()



        For Each customer As QBtoTL_Customer.Customer In customerData.DataArray
            If customer.QB_Name = selected_node Then
                TextBoxKey.Text = customer.QB_ID
                TextBoxName.Text = customer.QB_Name
            End If
        Next
        For Each jobs As QBtoTL_JobOrItem.Job_Subjob In jobData.DataArray
            If jobs.QB_Name = selected_node Then
                TextBoxKey.Text = jobs.QB_ID
                TextBoxName.Text = jobs.QB_Name
            End If
        Next


    End Sub

    Private Sub GetCustomerQBData()


    End Sub


End Class