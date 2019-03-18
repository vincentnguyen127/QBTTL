Imports Interop.QBFC10
Imports System.Data.SqlClient
Public Class TLClient
    Inherits System.Windows.Forms.Form
    'Create ADO.NET objects.
    Private myConn As SqlConnection
    Private myCmd As SqlCommand
    Private myReader As SqlDataReader
    Private results As String

    Private p_token As String
    Private p_AccountId As String
    Private execute As Boolean = False


    Public Overloads Sub Show(ByVal token As String, ByVal AccountId As String)
        If token Is Nothing Then
            MsgBox("Please Login")
        Else
            p_AccountId = AccountId
            p_token = token
            MyBase.Show()
        End If
    End Sub
    ' Queries QuickBooks for all the customers and displays
    Public Sub GetAllCustomers(ByVal executeOption)
        Dim EmailAddress As String
        Dim Telephone1 As String
        Dim Telephone2 As String
        Dim Fax As String
        'Dim TimeModified As String

        Dim objClientServices As New Services.TimeLive.Clients.Clients
        Dim objClient As New Services.TimeLive.Clients.Client

        'Me.DataGridView1.Rows.Clear()
        Dim authentication As New Services.TimeLive.Clients.SecuredWebServiceHeader
        authentication.AuthenticatedToken = p_token
        objClientServices.SecuredWebServiceHeaderValue = authentication
        'step1: create QBFC session manager and prepare the request
        Dim sessManager As QBSessionManager
        Dim msgSetRs As IMsgSetResponse
        Dim customerfullname As String
        Try
            sessManager = New QBSessionManagerClass()
            Dim msgSetRq As IMsgSetRequest = sessManager.CreateMsgSetRequest("US", 2, 0)
            msgSetRq.Attributes.OnError = ENRqOnError.roeContinue
            Dim synccust As ICustomerQuery = msgSetRq.AppendCustomerQueryRq
            'step2: begin QB session and send the request
            sessManager.OpenConnection("App", "TimeLive Quickbooks")
            sessManager.BeginSession("", ENOpenMode.omDontCare)
            msgSetRs = sessManager.DoRequests(msgSetRq)
            Dim respList As IResponseList
            respList = msgSetRs.ResponseList
            If (respList Is Nothing) Then
                Exit Sub
            End If
            ' Should only expect 1 response
            Dim resp As IResponse
            resp = respList.GetAt(0)
            If (resp.StatusCode = 0) Then
                Dim custRetList As ICustomerRetList
                custRetList = resp.Detail
                'Dim dv1 As New DataView(custRetList)
                'Me.DataGridView1.DataSource = dv1
                'Should only be 1 CustomerRet object returned
                Dim custRet As ICustomerRet
                Dim pblenth As Integer = custRetList.Count - 1
                If pblenth >= 0 Then
                    pgbar.Maximum = pblenth
                End If
                For i As Integer = 0 To custRetList.Count - 1
                    'Dim i = 0
                    pgbar.Increment(i)
                    custRet = custRetList.GetAt(i)

                    With custRet
                        If .ParentRef Is Nothing Then
                            'TimeModified = .TimeModified.GetValue
                            If .Email Is Nothing Then
                                EmailAddress = ""
                            Else
                                EmailAddress = .Email.GetValue
                            End If
                            If .Phone Is Nothing Then
                                Telephone1 = ""
                            Else
                                Telephone1 = .Phone.GetValue
                            End If
                            If .AltPhone Is Nothing Then
                                Telephone2 = ""
                            Else
                                Telephone2 = .AltPhone.GetValue
                            End If
                            If .Fax Is Nothing Then
                                Fax = ""
                            Else
                                Fax = .Fax.GetValue
                            End If


                            '**RL** Need to declare a class structure to safe values. See my email.

                            ' this can fetch the QB ID. runing an array for now store QB ID in Note on timelive
                            ' since we store the customer field as a string. can we store QB ID there does not update 
                            ' Its relying on name i assume.
                            ' this is the object custRet
                            If btnAddClientInTimeLive.Text = "Emulate" Then
                                '   MsgBox(" add ")
                                Me.DataGridView1.Rows.Add(Nothing, custRet, .Name.GetValue, .ListID.GetValue)
                            End If


                            MAIN.StatusWindow.Text += ">>Customer Name is: " + .Name.GetValue + "with ID: " + .ListID.GetValue + vbNewLine
                            localTB1.Text += ">>  Name is:  " + .Name.GetValue + "  with ID: " + .ListID.GetValue + vbNewLine


                            'Here is the real work ------------------------------------------------------------------------
                            'checkbox to transfer have not been implementing yet --done

                            If (executeOption = True) Then

                                If Is_checked_checkBox(.Name.GetValue) = True Then


                                    'If does not exist, at to timelive using webservice.Afteritis created store QB LISTID and TILELIVE ID in TL_QB_Relationship(create record)
                                    If ISQBID_In_DataTable(.ListID.GetValue) = False Then


                                        MsgBox("QBID id is not in the data base Creating:" + .Name.GetValue)


                                        objClientServices.InsertClient(.Name.GetValue, SetLength(.Name.GetValue),
                            EmailAddress, "", "", 233, "", "", "", Telephone1, Telephone2, Fax, 0, "", .ListID.GetValue, False,
                            False, Now.Date, 0, Now.Date, 0)
                                        MsgBox("QBID id is not in the data base Done:" + .Name.GetValue + " ID: " + objClientServices.GetClientIdByName(.Name.GetValue).ToString)

                                        localTB1.Text += "The ID for " & .ListID.GetValue & "Name: " & .Name.GetValue & " Is :" & objClientServices.GetClientIdByName(.Name.GetValue)


                                        Dim CustomerAdapter As New QB_TL_IDsTableAdapters.CustomersTableAdapter()
                                        CustomerAdapter.Insert(.ListID.GetValue, objClientServices.GetClientIdByName(.Name.GetValue), .Name.GetValue)

                                    Else
                                        'MsgBox("Enter 2")
                                        ' CheckifithasaTLID;if it does not at to timelive using webservice. Afterwards, update database record whittTL ID. 
                                        ' MsgBox(objClientServices.GetClientIdByName(.Name.GetValue).ToString)

                                        If ISTLID_In_DataTable(.ListID.GetValue, objClientServices.GetClientIdByName(.Name.GetValue)) = False Then

                                            objClientServices.InsertClientAsync(.Name.GetValue, SetLength(.Name.GetValue),
                           EmailAddress, "", "", 233, "", "", "", Telephone1, Telephone2, Fax, 0, "", .ListID.GetValue, False,
                           False, Now.Date, 0, Now.Date, 0)

                                            Dim CustomerAdapter As New QB_TL_IDsTableAdapters.CustomersTableAdapter()
                                            CustomerAdapter.Insert(.ListID.GetValue, objClientServices.GetClientIdByName(.Name.GetValue), .Name.GetValue)

                                        Else

                                            ' MsgBox("Enter 3")


                                            'If it exists: a. RetrieveTL ID (SELECT) and then you update TL Record b. Updated record directly to the time live databaseQuickbooksID 
                                            'myConn = New SqlConnection("Data Source=TIMELIVEDEV\TIMELIVE;Integrated Security=False;User ID=development@teltrium.com;Password=********;Connect Timeout=15;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False")
                                            'myCmd = myConn.CreateCommand
                                            'myCmd.CommandText = "SELECT * FROM dbo.AccountParty"
                                            'myConn.Open()
                                            'myReader = myCmd.ExecuteReader()


                                            'Concatenate the query result into a string.
                                            'Do While myReader.Read()
                                            '    results = results & myReader.GetString(0) & vbTab & myReader.GetString(1) & vbLf
                                            'Loop
                                            'Display results.
                                            '  MsgBox(results)
                                            '  MsgBox("Update Not implement yet")

                                        End If

                                    End If

                                    ' used to be calling it here
                                    '    objClientServices.InsertClientAsync("Debug", SetLength(.Name.GetValue),
                                    'EmailAddress, "", "", 233, "", "", "", Telephone1, Telephone2, Fax, 0, "", .ListID.GetValue, False,
                                    'False, Now.Date, 0, Now.Date, 0)
                                    ' MsgBox("The ID for " & .ListID.GetValue & " Is : " & objClientServices.GetClientId())

                                    localTB1.Text += "The ID for " & .ListID.GetValue & "Name: " & .Name.GetValue & " Is :" & objClientServices.GetClientIdByName(.Name.GetValue)





                                End If

                            End If

                            ' Dim objClient As New Services.TimeLive.Clients.Client
                            '     objClient.ClientId = 7
                            'objClient.Telephone1 = "New Number"
                            ' objClient.ClientName = "RichL1"
                            'objClient.EmailAddress = "EmailAddress"
                            'objClientServices.AddClient(objClient)

                        End If
                    End With
                Next
            End If
            If msgSetRs.ResponseList.GetAt(0).StatusSeverity = "Error" Then
                Throw New Exception(msgSetRs.ResponseList.GetAt(0).StatusMessage)
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
    Public Function SetLength(ByVal str As String) As String
        If str.Length > 50 Then
            str = str.Substring(0, 50)
        End If
        Return str
    End Function
    Private Function Is_checked_checkBox(ByVal my_var As String)
        Dim ans As Boolean = False
        For Each row As DataGridViewRow In DataGridView1.Rows
            'check which 
            Dim chk As DataGridViewCheckBoxCell = row.Cells("checkbox1")
            Dim name_chk As DataGridViewCell = row.Cells("dataV_Name")
            '  MsgBox(name_chk.Value)
            If chk.Value = True And String.Compare(Trim(name_chk.Value), Trim(my_var)) = 0 Then
                'MsgBox("Comparing : " + name_chk.Value.ToString + "   ===     " + my_var)
                Return True
            End If
        Next



        Return ans
    End Function
    Private Sub btnSyncAndAddClient_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddClientInTimeLive.Click
        pgbar.Value = 0

        If execute = False Then
            localTB1.Text = ""

            Try
                GetAllCustomers(execute)
                localTB1.Text += "Please Check and review customers" + vbNewLine
                MAIN.StatusWindow.Text += "Please Check and review customers" + vbNewLine
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
            execute = True
            btnAddClientInTimeLive.Text = "Execute"
            '  MsgBox("1")
        Else
            ' MsgBox("2")
            Try
                GetAllCustomers(execute)
                localTB1.Text += "Record(s) transferred successfully" + vbNewLine
                MAIN.StatusWindow.Text += "Record(s) transferred successfully" + vbNewLine
                Me.DataGridView1.Rows.Clear()
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
            btnAddClientInTimeLive.Text = "Emulate"
            execute = False

        End If



    End Sub

    Private Function ISQBID_In_DataTable(ByVal myqbID As String)

        Dim CustomerAdapter As New QB_TL_IDsTableAdapters.CustomersTableAdapter()
        Dim result As Boolean = False
        For Each TimeLiveID As QB_TL_IDs.CustomersRow In CustomerAdapter.GetCustomers()
            ' localTB1.Text += ">>Testing IS QB Id In database  " + " TL: " & myqbID & "--- ID: " + TimeLiveID.QuickBooks_ID + vbNewLine
            'MsgBox(TimeLiveID.QuickBooks_ID + ":" + myqbID)
            If String.Compare(Trim(TimeLiveID.QuickBooks_ID), Trim(myqbID)) = 0 Then
                ' MsgBox("True")

                result = True
                Return True
                ' localTB1.Text += "Found!"
                Exit For
            End If
        Next
        'localTB1.Text += "no found "

        Return result
    End Function

    Private Function ISTLID_In_DataTable(ByVal myqbID As String, ByVal myTLID As String)
        Dim result As Boolean = False
        Dim CustomerAdapter As New QB_TL_IDsTableAdapters.CustomersTableAdapter()
        Dim TimeLiveIDs As QB_TL_IDs.CustomersDataTable = CustomerAdapter.GetCorrespondingTL_ID(myqbID)

        For Each TimeLiveID As QB_TL_IDs.CustomersRow In TimeLiveIDs
            Dim v1 = Trim(myTLID)
            Dim v2 = Trim(TimeLiveID.TimeLive_ID)
            ' localTB1.Text += ">>Testing IS QB Id In database  " + " TL ID: " & v1 & "--- Qb ID: " + v2 + vbNewLine

            If String.Compare(v1, v2) = 0 Then
                result = True
                '    localTB1.Text += "Found!111"
                Exit For
            Else
                '   localTB1.Text += "Not !111"
            End If
        Next
        Return result
    End Function
    Private Sub AddClientInTimeLive_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        'MainMenu.Enabled = True

    End Sub

    Private Sub TLClient_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        DataGridView1.Columns("dataV_custRet").Visible = False

        ' becarefuk readying the data. 
        ' need to trim!
        'ISQBID_In_DataTable("QBmyTest1                       ")
        ' MsgBox(ISQBID_In_DataTable("QBmyTest1                       ").ToString)
        '  MsgBox("1")
        'ISQBID_In_DataTable("na")

        ' Code to insert
        'Dim CustomerAdapter As New QB_TL_IDsTableAdapters.CustomersTableAdapter()
        'CustomerAdapter.Insert("QBmyTest1", "TLmyTest1")
        '  MsgBox("Insert data into  QB_TL_IDs ")

        'Get all ID... - Not sure
        'Dim CustomerAdapter As New QB_TL_IDsTableAdapters.CustomersTableAdapter()
        'Dim CustomerIDs As QB_TL_IDs.CustomersDataTable
        'CustomerIDs = CustomerAdapter.GetCustomers()
        'For Each CustomerRow As QB_TL_IDs.CustomersRow In CustomerIDs
        'Main.StatusWindow.Text += ">>  " + " QB ID: " + CustomerRow.QuickBooks_ID + " TL ID: " + CustomerRow.TimeLive_ID + vbNewLine
        'Next

        'Code to get ID
        'Dim CustomerAdapter As New QB_TL_IDsTableAdapters.CustomersTableAdapter()
        'Dim TimeLiveIDs As QB_TL_IDs.CustomersDataTable = CustomerAdapter.GetCorrespondingTL_ID("QBTest1234")

        'For Each TimeLiveID As QB_TL_IDs.CustomersRow In TimeLiveIDs
        'Main.StatusWindow.Text += ">>  " + " TL ID: " + TimeLiveID.TimeLive_ID + vbNewLine
        'Next


        'Update Record
        'Dim CustomerAdapter As New QB_TL_IDsTableAdapters.CustomersTableAdapter
        'CustomerAdapter.Update("Testing", "QBTest1234")

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs)

    End Sub

    Private Sub DataGridView1_CellContentClick_1(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub

    Private Sub GroupBox1_Enter(sender As Object, e As EventArgs) Handles GroupBox1.Enter

    End Sub

    Private Sub sellectAll_Click(sender As Object, e As EventArgs) Handles sellectAll.Click
        Dim lastone As Integer = Convert.ToInt32(DataGridView1.Rows.Count.ToString) - 1
        ' MsgBox(lastone)
        For Each row As DataGridViewRow In DataGridView1.Rows
            'check which 
            row.Cells("checkbox1").Value = True
            Dim check As Integer = row.Cells("checkbox1").RowIndex
            If check = lastone Then

                row.Cells("checkbox1").Value = False

            End If
        Next

    End Sub


End Class