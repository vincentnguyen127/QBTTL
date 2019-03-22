Imports QBFC11Lib
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

        Sub New(ByVal NewlyAdded_in As String, ByVal QB_Name_in As String, ByVal Email_in As String, ByVal QB_ID_in As String, ByVal Telephone1_in As String,
                ByVal Fax_in As String, ModTime_in As String, CreateTime_in As String, Optional Enabled_In As Boolean = True)
            RecSelect = False
            NewlyAdded = NewlyAdded_in
            QBModTime = ModTime_in
            QBCreateTime = CreateTime_in
            QB_Name = QB_Name_in
            QB_ID = QB_ID_in
            Email = Email_in
            Telephone1 = Telephone1_in
            Fax = Fax_in
            Enabled = Enabled_In
        End Sub
    End Class

    Public Function GetCustomerQBData(IntegratedUIForm As IntegratedUI, UI As Boolean) As CustomerDataStructureQB
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
            synccust.ORCustomerListQuery.CustomerListFilter.ActiveStatus.SetValue(ENActiveStatus.asAll) 'asActiveOnly)

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
                Dim pblength As Integer = If(custRetList Is Nothing, -1, custRetList.Count)
                If pblength >= 0 Then
                    IntegratedUIForm.ProgressBar1.Maximum = pblength - 1
                End If
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
                    IntegratedUIForm.ProgressBar1.Value = i
                End If
            Next
            'End If
            If msgSetRs.ResponseList.GetAt(0).StatusSeverity = "Error" Then
                My.Forms.MAIN.History(msgSetRs.ResponseList.GetAt(0).StatusMessage, "C")
                Throw New Exception(msgSetRs.ResponseList.GetAt(0).StatusMessage)
            End If

            'Dim objClient As New Services.TimeLive.Clients.Client

            'CustomerData.DataArray.Contains
            'Dim CustomerAdapter As New QB_TL_IDsTableAdapters.CustomersTableAdapter()
            'Dim customers As DataTable = CustomerAdapter.GetCustomers
            'Dim isActive As Boolean
            'For Each customerRow As DataRow In customers.Rows
            'isActive = CustomerData.DataArray.Exists(Function(x As Customer) x.QB_ID = customerRow(1))
            'If Not isActive Then
            'My.Forms.MAIN.History(customerRow.Item(2), "i")
            'End If
            'Next
            ' for each element in Customers
            ' If it is not in CustomerData, then make it disabled in TimeLive
        Catch ex As Exception
            My.Forms.MAIN.History(ex.ToString, "C")
            MAIN.QUITQBSESSION()
            Throw ex
        End Try

        Return CustomerData
    End Function

    Public Function QBTransferCustomerToTL(ByRef objData As QBtoTL_Customer.CustomerDataStructureQB,
                                   ByVal token As String, IntegratedUIForm As IntegratedUI, UI As Boolean) As Integer

        Dim objClientServices As New Services.TimeLive.Clients.Clients
        Dim objClient As New Services.TimeLive.Clients.Client ' Unused
        Dim authentication As New Services.TimeLive.Clients.SecuredWebServiceHeader
        authentication.AuthenticatedToken = token
        objClientServices.SecuredWebServiceHeaderValue = authentication

        'sets status bar. If no, UI skip
        Dim incrementbar As Integer = 0
        If UI Then
            Dim pblength As Integer = objData.NoItems - objData.NoInactive - 1
            If pblength >= 0 Then
                IntegratedUIForm.ProgressBar1.Maximum = pblength
                IntegratedUIForm.ProgressBar1.Value = 0
            End If
        End If

        Dim NoRecordsCreatedorUpdated As Integer = 0

        For Each element As QBtoTL_Customer.Customer In objData.DataArray

            ' check if the check value is true
            If element.Enabled And element.RecSelect Then
                'Check number of QB records that match ID
                My.Forms.MAIN.History("Processing:  " + element.QB_Name, "n")
                Dim TL_ID_Return = ISQBID_In_DataTable(element.QB_Name, element.QB_ID)

                'if none create
                If TL_ID_Return = 0 Then
                    Dim create As Boolean = True
                    ' Do not show Message Box when UI = false, instead just create the new customer
                    If UI Then
                        create = MsgBox("New customer found: " + element.QB_Name + ". Create?", MsgBoxStyle.YesNo, "Warning!") = MsgBoxResult.Yes
                    End If
                    If create Then
                        NoRecordsCreatedorUpdated += 1

                        ' if it does not exist create a new record on both the sync database and on TL
                        My.Forms.MAIN.History("Inserting QB & TL keys into sync database and inserting to TimeLife:  " + element.QB_Name, "i")

                        'Insert record into TimeLife
                        objClientServices.InsertClient(element.QB_Name, SetLength(element.QB_Name),
                        element.Email, "", "", 233, "", "", "", element.Telephone1, "no telephone 2 yet", element.Fax, 0,
                                                       "", element.QB_ID, element.Enabled, False, Now.Date, 0, Now.Date, 0)

                        'Insert record into sync database
                        Dim TLClientID As String = objClientServices.GetClientIdByName(element.QB_Name)
                        If TLClientID IsNot Nothing Then
                            Dim CustomerAdapter As New QB_TL_IDsTableAdapters.CustomersTableAdapter()
                            CustomerAdapter.Insert(element.QB_ID, TLClientID, element.QB_Name, element.QB_Name)
                        Else
                            My.Forms.MAIN.History("Error creating record in TimeLive", "N")
                        End If
                    End If
                End If

                'if it exist check that the TL_ID is not empty ---> 1
                'if not empty, just update
                'if empty, informed the user of a potential error as a record has been created in the sync database without a corresponding TL pointer

                If TL_ID_Return = 1 Then
                    Dim TL_ID As String = ISTLID_In_DataTable(element.QB_ID)
                    If TL_ID Is Nothing Then
                        My.Forms.MAIN.History("Detected empty sync record (No TL ID). Needs to be manually sync or deleted." + element.QB_Name, "i")
                    Else
                        NoRecordsCreatedorUpdated += 1
                        My.Forms.MAIN.History("Updating TL record for: " + element.QB_Name, "i")
                        '-----------------------------------------------------------------------------------------------------------------------------------------------------------------
                        ' --------------------------------------------- this part is the update ------------------------------------------------------------------------------------------
                        '-----------------------------------------------------------------------------------------------------------------------------------------------------------------
                    End If
                End If
            End If

            ' TODO: Make Client in TL enabled/disabled based on element.Enabled field

            'if no UI, skip
            If element.Enabled And UI Then ' Only increment for active customers
                IntegratedUIForm.ProgressBar1.Value = incrementbar
                incrementbar += 1
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

        My.Forms.MAIN.History(numRecords + " found in QB sync table for: " + myqbName, "i")
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

'Update databese record code
'My.Forms.MAIN.History("Inserting TL key into sync database and inserting to TimeLife:  " + element.Name)
''Insert record into Time Life
'objClientServices.InsertClient(element.Name, SetLength(element.Name),
'    element.Email, "", "", 233, "", "", "", element.Telephone1, "no telephone 2 yet", element.Fax, 0, "", element.QB_ID, False,
'    False, Now.Date, 0, Now.Date, 0)

''Insert record into sync database
'Dim CustomerAdapter As New QB_TL_IDsTableAdapters.CustomersTableAdapter()
'CustomerAdapter.Update(element.QB_ID, objClientServices.GetClientIdByName(element.Name))
