Imports Interop.QBFC10
Public Class QBCustomer
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
    Private Sub btnGetAndAddCustomer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetAndAddCustomer.Click
        pgbar.Value = 0
        Try
            Dim play As New Services.TimeLive.Clients.Clients
            Dim objClientServices As New Services.TimeLive.Clients.Clients
            Dim authentication As New Services.TimeLive.Clients.SecuredWebServiceHeader
            authentication.AuthenticatedToken = p_token
            objClientServices.SecuredWebServiceHeaderValue = authentication
            Dim objClientArray() As Object
            objClientArray = objClientServices.GetClients()
            Dim objClient As New Services.TimeLive.Clients.Client
            Dim pblenth As Integer = objClientArray.Length - 1
            If pblenth >= 0 Then
                pgbar.Maximum = pblenth
            End If
            For n As Integer = 0 To objClientArray.Length - 1
                pgbar.Increment(n)
                objClient = objClientArray(n)


                '---------------------------------the real work.--------------------------------------
                With objClient

                    AddCustomerInQB(.ClientName, .EmailAddress, .Notes, .Telephone1, .Telephone2, .Fax)

                End With


            Next
            MessageBox.Show("Record(s) transferred successfully")
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Public Sub AddCustomerInQB(ByVal ClientName As String, ByVal EmailAddress As String, ByVal Notes As String, _
                            ByVal Telephone1 As String, ByVal Telephone2 As String, ByVal Fax As String)

        'step1: create QBFC session manager and prepare the request
        Dim sessManager As QBSessionManager
        Dim msgSetRs As IMsgSetResponse
        Try
            sessManager = New QBSessionManagerClass()
            Dim msgSetRq As IMsgSetRequest = sessManager.CreateMsgSetRequest("US", 2, 0)
            msgSetRq.Attributes.OnError = ENRqOnError.roeContinue
            Dim custAdd As ICustomerAdd = msgSetRq.AppendCustomerAddRq
            custAdd.Name.SetValue(ClientName)
            custAdd.Email.SetValue(EmailAddress)
            'custAdd.Notes.SetValue(Notes)
            'custAdd.Phone.SetValue(Telephone1)
            'custAdd.AltPhone.SetValue(Telephone2)
            'custAdd.Fax.SetValue(Fax)



            'step2: begin QB session and send the request
            sessManager.OpenConnection("App", "TimeLive Quickbooks")
            sessManager.BeginSession("", ENOpenMode.omDontCare)
            msgSetRs = sessManager.DoRequests(msgSetRq)
            If Not msgSetRs.ResponseList.GetAt(0).StatusMessage.Contains("already in use") Then
                If msgSetRs.ResponseList.GetAt(0).StatusSeverity = "Error" Then
                    Throw New Exception(msgSetRs.ResponseList.GetAt(0).StatusMessage)
                End If
            End If


        Catch ex As Exception
            Throw ex
        Finally
            If Not sessManager Is Nothing Then
                sessManager.EndSession()
                sessManager.CloseConnection()
            End If
        End Try
    End Sub

    Private Sub Customer_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        'MainMenu.Enabled = True
    End Sub

    Private Sub QBCustomer_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ' Code to insert
        'Dim CustomerAdapter As New QB_TL_IDsTableAdapters.CustomersTableAdapter()
        'CustomerAdapter.Insert("QBmyTest1", "TLmyTest1")
        MsgBox("Insert data into  QB_TL_IDs ")

        'Get all ID... - Not sure
        'Dim CustomerAdapter As New QB_TL_IDsTableAdapters.CustomersTableAdapter()
        'Dim CustomerIDs As QB_TL_IDs.CustomersDataTable
        'CustomerIDs = CustomerAdapter.GetCustomers()
        'For Each CustomerRow As QB_TL_IDs.CustomersRow In CustomerIDs
        'My.Forms.StatusWindow.TBStatus.Text += ">>  " + " QB ID: " + CustomerRow.QuickBooks_ID + " TL ID: " + CustomerRow.TimeLive_ID + vbNewLine
        'Next

        'Code to get ID
        'Dim CustomerAdapter As New QB_TL_IDsTableAdapters.CustomersTableAdapter()
        'Dim TimeLiveIDs As QB_TL_IDs.CustomersDataTable = CustomerAdapter.GetCorrespondingTL_ID("QBTest1234")

        'For Each TimeLiveID As QB_TL_IDs.CustomersRow In TimeLiveIDs
        'My.Forms.StatusWindow.TBStatus.Text += ">>  " + " TL ID: " + TimeLiveID.TimeLive_ID + vbNewLine
        'Next


        'Update Record
        'Dim CustomerAdapter As New QB_TL_IDsTableAdapters.CustomersTableAdapter
        'CustomerAdapter.Update("Testing", "QBTest1234")

    End Sub
End Class
