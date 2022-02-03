'Imports QBFC11Lib
Imports QBFC13Lib

Imports System.Net.Mail
Imports System.Data.SqlClient

Public Class QBtoTL_Customer

    Public Class CustomerDataStructureQB
        Public NoItems As Integer = 0
        Public DataArray As New List(Of Customer)
        Public NoInactive As Integer = 0

    End Class
    Public Class Customer
        Public RecSelect As Boolean
        Public NewlyAdded As String
        Public QB_Name As String
        Public QB_ID As String
        Public QBModTime As String
        Public QBCreateTime As String
        Public Email As String
        Public Telephone1 As String
        Public Fax As String
        Public Enabled As Boolean
        Public EditSequence As String



        Sub New(ByVal NewlyAdded As String, ByVal QB_Name As String, ByVal Email As String, ByVal QB_ID As String, ByVal Telephone1 As String,
                ByVal Fax As String, ModTime As String, CreateTime As String, Optional Enabled As Boolean = True)
            RecSelect = False
            Me.NewlyAdded = NewlyAdded
            Me.QBModTime = ModTime
            Me.QBCreateTime = CreateTime
            Me.QB_Name = QB_Name
            Me.QB_ID = QB_ID
            Me.Email = Email
            Me.Telephone1 = Telephone1
            Me.Fax = Fax
            Me.Enabled = Enabled

        End Sub
    End Class
    Public Function GetCustomerQBData() As CustomerDataStructureQB
        Dim EmailAddress As String
        Dim Telephone1 As String
        Dim Fax As String
        Dim ModTime As String
        Dim CreateTime As String
        Dim CustomerData As New CustomerDataStructureQB
        Dim NewlyAdd As String

        'step1: prepare the request
        Dim msgSetRs As IMsgSetResponse

        Try
            Dim msgSetRq As IMsgSetRequest = MAIN.SESSMANAGER.CreateMsgSetRequest("US", 2, 0) 'sessManager
            msgSetRq.Attributes.OnError = ENRqOnError.roeContinue

            '-------------------------1---------------------------------------------
            Dim synccust As ICustomerQuery = msgSetRq.AppendCustomerQueryRq
            synccust.ORCustomerListQuery.CustomerListFilter.ActiveStatus.SetValue(ENActiveStatus.asActiveOnly) 'asActiveOnly) or asAll

            'step2: send the request
            msgSetRs = MAIN.SESSMANAGER.DoRequests(msgSetRq) 'sessManager
            Dim respList As IResponseList = msgSetRs.ResponseList
            If (respList Is Nothing Or respList.GetAt(0).Detail Is Nothing) Then
                ' no data
                My.Forms.MAIN.History("No customers found...", "i")
                Return Nothing
            End If
            ' Should only expect 1 response
            Dim resp As IResponse
            resp = respList.GetAt(0)
            'If (resp.StatusCode = 0) Then

            '----------------------2------------------------------------------------
            Dim custRetList As ICustomerRetList
            custRetList = resp.Detail

            '------------------------------3-----------------------------------
            Dim custRet As ICustomerRet
            'sets status bar; If no UI, then skip


            For i As Integer = 0 To If(custRetList Is Nothing, -1, custRetList.Count - 1)
                custRet = custRetList.GetAt(i)
                With custRet
                    If .ParentRef Is Nothing Then
                        EmailAddress = If(.Email Is Nothing, "", .Email.GetValue)
                        Telephone1 = If(.Phone Is Nothing, "", .Phone.GetValue)
                        Fax = If(.Fax Is Nothing, "", .Fax.GetValue)
                        CreateTime = If(.TimeCreated Is Nothing, "", .TimeCreated.GetValue.ToString)
                        ModTime = If(.TimeModified Is Nothing, CreateTime, .TimeModified.GetValue.ToString)

                        ' will check which type data should be added 
                        CustomerData.NoItems += 1
                        If Not .IsActive.GetValue Then
                            CustomerData.NoInactive += 1
                        End If
                        'Check if newlyadded
                        'Dim TL_ID_Count As Int16 = 0 ' Delete
                        Dim TL_ID_Count = ISQBID_In_DataTable(.Name.GetValue, .ListID.GetValue) 'Timeouts

                        NewlyAdd = If(TL_ID_Count, "", "N") ' N if new
                        CustomerData.DataArray.Add(New Customer(NewlyAdd, .Name.GetValue, EmailAddress, .ListID.GetValue, Telephone1, Fax, ModTime, CreateTime, .IsActive.GetValue))
                        ' Debugging purposes only, delete
                        If Not .IsActive.GetValue Then
                            My.Forms.MAIN.History(.Name.GetValue + " is not active", "i")
                        End If
                    End If
                End With

            Next
            'End If
            If msgSetRs.ResponseList.GetAt(0).StatusSeverity = "Error" Then
                My.Forms.MAIN.History(msgSetRs.ResponseList.GetAt(0).StatusMessage, "C")
                Throw New Exception(msgSetRs.ResponseList.GetAt(0).StatusMessage)
            End If

        Catch ex As Exception
            My.Forms.MAIN.History(ex.ToString, "C")
            'MAIN.QUITQBSESSION()
            Throw ex
        End Try

        Return CustomerData
    End Function

    Public Function GetCustomerQBData(MainForm As MAIN, UI As Boolean) As CustomerDataStructureQB
        Dim EmailAddress As String
        Dim Telephone1 As String
        Dim Fax As String
        Dim ModTime As String
        Dim CreateTime As String
        Dim CustomerData As New CustomerDataStructureQB
        Dim NewlyAdd As String
        Dim EditSequence As Object


        'step1: prepare the request
        Dim msgSetRs As IMsgSetResponse

        Try
            Dim msgSetRq As IMsgSetRequest = MAIN.SESSMANAGER.CreateMsgSetRequest("US", 2, 0) 'sessManager
            msgSetRq.Attributes.OnError = ENRqOnError.roeContinue

            '-------------------------1---------------------------------------------
            Dim synccust As ICustomerQuery = msgSetRq.AppendCustomerQueryRq
            synccust.ORCustomerListQuery.CustomerListFilter.ActiveStatus.SetValue(ENActiveStatus.asActiveOnly) 'asActiveOnly) or asAll

            'step2: send the request
            msgSetRs = MAIN.SESSMANAGER.DoRequests(msgSetRq) 'sessManager
            Dim respList As IResponseList = msgSetRs.ResponseList
            If (respList Is Nothing Or respList.GetAt(0).Detail Is Nothing) Then
                ' no data
                My.Forms.MAIN.History("No customers found...", "i")
                Return Nothing
            End If
            ' Should only expect 1 response
            Dim resp As IResponse
            resp = respList.GetAt(0)
            'If (resp.StatusCode = 0) Then

            '----------------------2------------------------------------------------
            Dim custRetList As ICustomerRetList
            custRetList = resp.Detail

            '------------------------------3-----------------------------------
            Dim custRet As ICustomerRet
            'sets status bar; If no UI, then skip
            If UI Then
                My.Forms.MAIN.ProgressBar1.Maximum += If(custRetList Is Nothing, 0, custRetList.Count)
            End If

            For i As Integer = 0 To If(custRetList Is Nothing, -1, custRetList.Count - 1)
                custRet = custRetList.GetAt(i)
                With custRet
                    If .ParentRef Is Nothing Then
                        EmailAddress = If(.Email Is Nothing, "", .Email.GetValue)
                        Telephone1 = If(.Phone Is Nothing, "", .Phone.GetValue)
                        Fax = If(.Fax Is Nothing, "", .Fax.GetValue)
                        CreateTime = If(.TimeCreated Is Nothing, "", .TimeCreated.GetValue.ToString)
                        ModTime = If(.TimeModified Is Nothing, CreateTime, .TimeModified.GetValue.ToString)


                        ' will check which type data should be added 
                        CustomerData.NoItems += 1
                        If Not .IsActive.GetValue Then
                            CustomerData.NoInactive += 1
                        End If
                        'Check if newlyadded
                        'Dim TL_ID_Count As Int16 = 0 ' Delete
                        Dim TL_ID_Count = ISQBID_In_DataTable(.Name.GetValue, .ListID.GetValue) 'Timeouts

                        NewlyAdd = If(TL_ID_Count, "", "N") ' N if new
                        CustomerData.DataArray.Add(New Customer(NewlyAdd, .Name.GetValue, EmailAddress, .ListID.GetValue, Telephone1, Fax, ModTime, CreateTime, .IsActive.GetValue))
                        ' Debugging purposes only, delete
                        If Not .IsActive.GetValue Then
                            My.Forms.MAIN.History(.Name.GetValue + " is not active", "i")
                        End If
                    End If
                End With
                If UI Then
                    My.Forms.MAIN.ProgressBar1.Value = i
                End If
            Next
            'End If
            If msgSetRs.ResponseList.GetAt(0).StatusSeverity = "Error" Then
                My.Forms.MAIN.History(msgSetRs.ResponseList.GetAt(0).StatusMessage, "C")
                Throw New Exception(msgSetRs.ResponseList.GetAt(0).StatusMessage)
            End If

        Catch ex As Exception
            My.Forms.MAIN.History(ex.ToString, "C")
            'MAIN.QUITQBSESSION()
            Throw ex
        End Try

        Return CustomerData
    End Function

    'Modify individual customer in QB
    Public Function ModifyCustomer(customerName As String)
        'send the request to QB to get the selected customer infor 
        Dim msgSetRq As IMsgSetRequest = MAIN.SESSMANAGER.CreateMsgSetRequest("US", 2, 0)

        msgSetRq.Attributes.OnError = ENRqOnError.roeContinue
        Dim CustomerQueryRq As ICustomerQuery = msgSetRq.AppendCustomerQueryRq

        'CustomerQueryRq.ORCustomerListQuery.CustomerListFilter.ActiveStatus.SetValue(ENActiveStatus.asActiveOnly) 'asActiveOnly) or asAll
        CustomerQueryRq.ORCustomerListQuery.FullNameList.Add(customerName)
        Dim msgSetRs As IMsgSetResponse = MAIN.SESSMANAGER.DoRequests(msgSetRq)

        Dim response As IResponse = msgSetRs.ResponseList.GetAt(0)
        Dim custRetList As ICustomerRetList
        custRetList = response.Detail

        Dim custRet As ICustomerRet = custRetList.GetAt(0)

        Dim EmailAddress As String
        Dim Telephone1 As String
        Dim Fax As String
        Dim ModTime As String
        Dim CreateTime As String
        Dim NewlyAdd As String


        With custRet
            EmailAddress = If(.Email Is Nothing, "", .Email.GetValue)
            Telephone1 = If(.Phone Is Nothing, "", .Phone.GetValue)
            Fax = If(.Fax Is Nothing, "", .Fax.GetValue)
            CreateTime = If(.TimeCreated Is Nothing, "", .TimeCreated.GetValue.ToString)
            ModTime = If(.TimeModified Is Nothing, CreateTime, .TimeModified.GetValue.ToString)
        End With

        Dim customer As QBtoTL_Customer.Customer = New QBtoTL_Customer.Customer(NewlyAdd, custRet.Name.GetValue, EmailAddress, custRet.ListID.GetValue, Telephone1, Fax, ModTime, CreateTime, custRet.IsActive.GetValue)
        customer.EditSequence = custRet.EditSequence.GetValue


        MAIN.Get_Customer_Form(customer)


        Try
            Dim newMsgSetRq As IMsgSetRequest = MAIN.SESSMANAGER.CreateMsgSetRequest("US", 2, 0)


            Dim customerMod As ICustomerMod = newMsgSetRq.AppendCustomerModRq
            customerMod.Name.SetValue(customer.QB_Name)
            customerMod.ListID.SetValue(customer.QB_ID)
            customerMod.EditSequence.SetValue(customer.EditSequence)
            customerMod.Email.SetValue(customer.Email)
            customerMod.Phone.SetValue(customer.Telephone1)
            customerMod.Fax.SetValue(customer.Fax)

            msgSetRq = MAIN.SESSMANAGER.DoRequests(newMsgSetRq)
            Dim res As IResponse = msgSetRq.ResponseList.GetAt(0)
        Catch ex As Exception

        End Try

    End Function

    Public Function QBTransferCustomerToTL(ByRef objData As QBtoTL_Customer.CustomerDataStructureQB,
                                   ByVal token As String, MainForm As MAIN, UI As Boolean) As Integer

        Dim objClientServices As Services.TimeLive.Clients.Clients = MAIN.connect_TL_clients(token)

        'sets status bar. If no, UI skip
        Dim incrementbar As Integer = 0
        If UI Then
            Dim pblength As Integer = objData.NoItems - objData.NoInactive
            My.Forms.MAIN.ProgressBar1.Maximum = pblength
            My.Forms.MAIN.ProgressBar1.Value = 0
        End If

        Dim NoRecordsCreatedorUpdated As Integer = 0

        For Each element As Customer In objData.DataArray

            ' check if the check value is true
            If element.Enabled And element.RecSelect Then
                'Check number of QB records that match ID
                My.Forms.MAIN.History("Processing:  " + element.QB_Name, "n")
                ' 0: QuickBooks ID is not in Data Table
                ' 1: QuickBooks ID is in DataTable
                Dim DT_has_QBID = ISQBID_In_DataTable(element.QB_Name, element.QB_ID)

                Dim create As Boolean = True
                ' Do not show Message Box when UI = false, instead just create the new customer
                If UI And Not CBool(DT_has_QBID) Then
                    'create = MsgBox("New customer found: " + element.QB_Name + ". Create?", MsgBoxStyle.YesNo, "Warning!") = MsgBoxResult.Yes
                End If
                If create Then
                    If DT_has_QBID Then
                        Dim TL_ID As String = ISTLID_In_DataTable(element.QB_ID)
                        If TL_ID Is Nothing Then
                            My.Forms.MAIN.History("Detected empty sync record (No TL ID). Needs to be manually sync or deleted." + element.QB_Name, "i")
                        End If
                        Dim customerInTL As Boolean = False
                        ' Check if TL has the TL ID that the DB has
                        Array.ForEach(objClientServices.GetClients,
                                      Sub(e As Services.TimeLive.Clients.Client)
                                          If objClientServices.GetClientIdByName(e.ClientName) = TL_ID Then
                                              customerInTL = True
                                              ' QB and TL have different names, change in DB and alert the user
                                              ' Note: If we change this, then might need to change jobs/subjobs DB
                                              If Not e.ClientName = element.QB_Name Then
                                                  Dim CustomerAdapter As New QB_TL_IDsTableAdapters.CustomersTableAdapter()
                                                  CustomerAdapter.updateCustomerNames(element.QB_Name, e.ClientName, element.QB_ID, Trim(TL_ID))
                                                  My.Forms.MAIN.History("Name Conflict: TL Name: " + e.ClientName + " QB Name: " + element.QB_Name, "N")
                                              End If
                                          End If
                                      End Sub)
                        If customerInTL Then
                            ' TL already has this value and so does our DB, so just move to next element after updating Progress Bar
                            If UI Then
                                incrementbar += 1
                                My.Forms.MAIN.ProgressBar1.Value = incrementbar
                            End If
                            Continue For
                        End If
                    Else
                        ' Not in local Database
                        If Array.Exists(objClientServices.GetClients, Function(e As Services.TimeLive.Clients.Client) e.ClientName = element.QB_Name) Then
                            ' TimeLive has a data entry with the same name, treat as the same and add into DB
                            Dim CustomerAdapter As New QB_TL_IDsTableAdapters.CustomersTableAdapter()
                            Dim QB_ID_fromDB As QB_TL_IDs.CustomersDataTable = CustomerAdapter.GetCorrespondingQB_IDbyQB_Name(element.QB_Name)
                            If QB_ID_fromDB.Count = 0 Then
                                ' No record of the data entry in our data table, then add it
                                CustomerAdapter.Insert(element.QB_ID, objClientServices.GetClientIdByName(element.QB_Name), element.QB_Name, element.QB_Name)
                                My.Forms.MAIN.History("Customer '" + element.QB_Name + "' found in both TimeLive and Quickbooks added to local database", "i")
                            Else
                                ' Record exists just with an incorrect QB ID, so update it
                                Dim correctTL_ID As String = QB_ID_fromDB(0)(1)
                                If correctTL_ID IsNot Nothing Then
                                    CustomerAdapter.UpdateQBID(element.QB_ID, Trim(correctTL_ID))
                                    My.Forms.MAIN.History("Updated QuickBooks ID of Customer '" + element.QB_Name + "' in local database", "i")
                                End If
                            End If
                            Continue For ' Already in TL, so just continue to next element in QB
                        End If
                    End If

                    NoRecordsCreatedorUpdated += 1
                    Dim whereInsert As String = If(DT_has_QBID, "TimeLive: ", "local database and TimeLive: ")
                    ' if it does not exist create a new record on both the sync database and on TL
                    My.Forms.MAIN.History("Inserting customer into " + whereInsert + element.QB_Name, "i")
                    Try
                        'Insert record into TimeLive
                        objClientServices.InsertClient(element.QB_Name, SetLength(element.QB_Name),
                        element.Email, "", "", 233, "", "", "", element.Telephone1, "no telephone 2 yet", element.Fax, 0,
                                                       "", element.QB_ID, element.Enabled, False, Now.Date, 0, Now.Date, 0)
                        My.Forms.MAIN.History("Transfer to TimeLive was successful.", "i")

                        If Not CBool(DT_has_QBID) Then
                            'Insert record into sync database
                            Dim customerInTL As Boolean = Array.Exists(objClientServices.GetClients,
                                                                       Function(e As Services.TimeLive.Clients.Client) e.ClientName = element.QB_Name)
                            If customerInTL Then
                                Dim TLClientID As String = objClientServices.GetClientIdByName(element.QB_Name)
                                My.Forms.MAIN.History("TimeLive Client ID: " + TLClientID, "i")
                                My.Forms.MAIN.History("Inserting new client into sync db.", "i")
                                Dim CustomerAdapter As New QB_TL_IDsTableAdapters.CustomersTableAdapter()
                                CustomerAdapter.Insert(element.QB_ID, TLClientID, element.QB_Name, element.QB_Name)
                            Else
                                My.Forms.MAIN.History("Error creating record in TimeLive", "N")
                            End If
                        End If
                    Catch ex As Exception
                        My.Forms.MAIN.History("Transfer failed." + ex.ToString, "N")
                    End Try
                End If
                'End If

                'if it exist check that the TL_ID is not empty ---> 1
                'if not empty, just update
                'if empty, informed the user of a potential error as a record has been created in the sync database without a corresponding TL pointer

                'If TL_ID_Return = 1 Then
                'Dim TL_ID As String = ISTLID_In_DataTable(element.QB_ID)
                'If TL_ID Is Nothing Then
                'History("Detected empty sync record (No TL ID). Needs to be manually sync or deleted." + element.QB_Name, "i")
                'Else
                '   NoRecordsCreatedorUpdated += 1
                '  History("Updating TL record for: " + element.QB_Name, "i")
                '-----------------------------------------------------------------------------------------------------------------------------------------------------------------
                ' --------------------------------------------- this part is the update ------------------------------------------------------------------------------------------
                '-----------------------------------------------------------------------------------------------------------------------------------------------------------------
                'End If
            End If

            ' TODO: Make Client in TL enabled/disabled based on element.Enabled field

            'if no UI, then skip
            If element.Enabled And UI Then ' Only increment for active customers
                incrementbar += 1
                My.Forms.MAIN.ProgressBar1.Value = incrementbar
            End If
        Next

        Return NoRecordsCreatedorUpdated
    End Function

    ''' <summary>
    ''' 'Checks if QBID is in sync database
    ''' </summary>
    ''' <param name="myqbID"></param>
    ''' <returns></returns>
    Private Function ISQBID_In_DataTable(ByVal myqbName As String, ByVal myqbID As String) As Int16
        'Dim CustomerAdapter As New QB_TL_IDsTableAdapters.CustomersTableAdapter()
        'For Each TimeLiveID As QB_TL_IDs.CustomersRow In CustomerAdapter.GetCustomers()
        '    If String.Compare(Trim(TimeLiveID.QuickBooks_ID), Trim(myqbID)) = 0 Then
        '        Return True
        '        Exit For
        '    End If
        'Next
        Dim CustomerAdapter As New QB_TL_IDsTableAdapters.CustomersTableAdapter()
        Dim TimeLiveIDs As QB_TL_IDs.CustomersDataTable = CustomerAdapter.GetCorrespondingTL_ID(myqbID)
        Dim result As Int16 = Math.Min(TimeLiveIDs.Count, 2)
        Dim numRecords As String

        Select Case TimeLiveIDs.Count
            Case 0
                numRecords = "No records"
            Case 1
                numRecords = "One record"
            Case Else
                numRecords = "More than one record"
        End Select

        My.Forms.MAIN.History(numRecords + " found in local database for: " + myqbName, "i")
        Return result
    End Function

    ''' <summary>
    ''' Check if QBID is in sync database and if it has a corresponding TLID
    ''' </summary>
    ''' <param name="myqbID"></param>
    ''' <returns>
    ''' 0 if not found
    ''' 1 if found
    ''' 2 if more than one is found
    ''' </returns>
    Private Function ISTLID_In_DataTable(ByVal myqbID As String) As String
        Dim result As String = Nothing
        Dim CustomerAdapter As New QB_TL_IDsTableAdapters.CustomersTableAdapter()
        Dim TimeLiveIDs As QB_TL_IDs.CustomersDataTable = CustomerAdapter.GetCorrespondingTL_ID(myqbID)

        If TimeLiveIDs Is Nothing Then Return Nothing
        If TimeLiveIDs.Rows.Count = 0 Then Return Nothing


        If String.IsNullOrEmpty(Trim(TimeLiveIDs(0).TimeLive_ID.ToString())) Then
            My.Forms.MAIN.History("Record has a TLID of Nothing", "I")
        Else
            My.Forms.MAIN.History("Record has a TLID of: " + TimeLiveIDs(0).TimeLive_ID.ToString(), "i")
            result = TimeLiveIDs(0).TimeLive_ID.ToString()
        End If

        Return result
    End Function

    Public Function SetLength(ByVal str As String) As String
        Return str.Substring(0, Math.Min(str.Length, 50)) ' Get first 50 characters, or all if less than 50
    End Function

End Class

'Update database record code
'History("Inserting TL key into sync database and inserting to TimeLife:  " + element.Name)
''Insert record into Time Life
'objClientServices.InsertClient(element.Name, SetLength(element.Name),
'    element.Email, "", "", 233, "", "", "", element.Telephone1, "no telephone 2 yet", element.Fax, 0, "", element.QB_ID, False,
'    False, Now.Date, 0, Now.Date, 0)

''Insert record into sync database
'Dim CustomerAdapter As New QB_TL_IDsTableAdapters.CustomersTableAdapter()
'CustomerAdapter.Update(element.QB_ID, objClientServices.GetClientIdByName(element.Name))
