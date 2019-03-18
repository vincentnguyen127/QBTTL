Imports QBFC11Lib
Imports System.Net.Mail
Imports System.Data.SqlClient


Public Class QBtoTL_Customer
    Public Class CustomerDataStructureQB
        Public NoItems As Integer = 0
        Public DataArray As New List(Of Customer)
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

        Sub New(ByVal NewlyAdded_in As String, ByVal QB_Name_in As String, ByVal Email_in As String, ByVal QB_ID_in As String, ByVal Telephone1_in As String,
                ByVal Fax_in As String, ModTime_in As String, CreateTime_in As String)
            RecSelect = False
            NewlyAdded = NewlyAdded_in
            QBModTime = ModTime_in
            QBCreateTime = CreateTime_in
            QB_Name = QB_Name_in
            QB_ID = QB_ID_in
            Email = Email_in
            Telephone1 = Telephone1_in
            Fax = Fax_in
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

        'step1: create QBFC session manager and prepare the request
        'Dim sessManager As QBSessionManager
        Dim msgSetRs As IMsgSetResponse

        Try
            'sessManager = New QBSessionManagerClass()
            Dim msgSetRq As IMsgSetRequest = MAIN.SESSMANAGER.CreateMsgSetRequest("US", 2, 0) 'sessManager
            msgSetRq.Attributes.OnError = ENRqOnError.roeContinue

            '-------------------------1---------------------------------------------
            Dim synccust As ICustomerQuery = msgSetRq.AppendCustomerQueryRq
            synccust.ORCustomerListQuery.CustomerListFilter.ActiveStatus.SetValue(ENActiveStatus.asActiveOnly)

            'step2: begin QB session and send the request
            'sessManager.OpenConnection("App", "TimeLive Quickbooks") 'sessManager
            'sessManager.BeginSession("", ENOpenMode.omDontCare) 'sessManager
            msgSetRs = MAIN.SESSMANAGER.DoRequests(msgSetRq) 'sessManager


            Dim respList As IResponseList
            respList = msgSetRs.ResponseList

            If (respList Is Nothing) Then
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
                'sets status bar; If no, UI skip
                If UI = True Then
                    Dim pblenth As Integer = custRetList.Count
                    If pblenth >= 0 Then
                        IntegratedUIForm.ProgressBar1.Maximum = pblenth - 1
                    End If
                End If

                For i As Integer = 0 To custRetList.Count - 1
                    custRet = custRetList.GetAt(i)

                    With custRet
                        If .ParentRef Is Nothing Then
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

                            If .Fax Is Nothing Then
                                Fax = ""
                            Else
                                Fax = .Fax.GetValue
                            End If

                            If .TimeModified Is Nothing Then
                                ModTime = .TimeCreated.GetValue.ToString
                            Else
                                ModTime = .TimeModified.GetValue.ToString()
                            End If

                            If .TimeCreated Is Nothing Then
                                CreateTime = .TimeCreated.GetValue.ToString
                            Else
                                CreateTime = .TimeModified.GetValue.ToString()
                            End If

                            ' will check which type data should be added 
                            CustomerData.NoItems = CustomerData.NoItems + 1
                        'Check if newlyadded
                        'Dim TL_ID_Count As Int16 = 0 ' Delete
                        Dim TL_ID_Count = ISQBID_In_DataTable(.Name.GetValue, .ListID.GetValue)

                        If TL_ID_Count <> 0 Then
                            NewlyAdd = ""
                        Else
                                NewlyAdd = "N"
                            End If
                            CustomerData.DataArray.Add(New Customer(NewlyAdd, .Name.GetValue, EmailAddress, .ListID.GetValue, Telephone1, Fax, ModTime, CreateTime))

                        End If
                    End With
                    If UI = True Then
                        IntegratedUIForm.ProgressBar1.Value = i
                    End If
                Next
                'End If
                If msgSetRs.ResponseList.GetAt(0).StatusSeverity = "Error" Then
                My.Forms.MAIN.History(msgSetRs.ResponseList.GetAt(0).StatusMessage, "C")
                Throw New Exception(msgSetRs.ResponseList.GetAt(0).StatusMessage)
            End If
        Catch ex As Exception
            My.Forms.MAIN.History(ex.ToString, "C")
            Throw ex
            'Finally
            '   If Not (sessManager Is Nothing) Then
            '       sessManager.EndSession()
            '       sessManager.CloseConnection()
            '   End If
        End Try

        Return CustomerData
    End Function

    Public Function QBTransferCustomerToTL(ByRef objData As QBtoTL_Customer.CustomerDataStructureQB,
                                   ByVal token As String, IntegratedUIForm As IntegratedUI, UI As Boolean) As Integer

        Dim objClientServices As New Services.TimeLive.Clients.Clients
        Dim objClient As New Services.TimeLive.Clients.Client
        Dim authentication As New Services.TimeLive.Clients.SecuredWebServiceHeader
        authentication.AuthenticatedToken = token
        objClientServices.SecuredWebServiceHeaderValue = authentication


        'sets status bar. If no, UI skip
        Dim incrementbar As Integer = 0
        If UI = True Then
            Dim pblenth As Integer = objData.DataArray.Count - 1
            If pblenth >= 0 Then
                IntegratedUIForm.ProgressBar1.Maximum = pblenth
                IntegratedUIForm.ProgressBar1.Value = 0
            End If
        End If

        Dim NoRecordsCreatedorUpdated As Integer = 0

        For Each element As QBtoTL_Customer.Customer In objData.DataArray

            ' check if the check value is true
            If element.RecSelect = True Then
                'Check number of QB records that match ID
                My.Forms.MAIN.History("Processing:  " + element.QB_Name, "n")

                Dim TL_ID_Return = ISQBID_In_DataTable(element.QB_Name, element.QB_ID)

                'if none create
                If TL_ID_Return = 0 Then

                    If MsgBox("New customer found: " + element.QB_Name + ". Create?", MsgBoxStyle.YesNo, "Warning!") = MsgBoxResult.Yes Then

                        NoRecordsCreatedorUpdated = NoRecordsCreatedorUpdated + 1

                        ' if it does not exist create a new record on both the sync database and on TL
                        My.Forms.MAIN.History("Inserting QB & TL keys into sync database and inserting to TimeLife:  " + element.QB_Name, "i")

                        'Insert record into TimeLife
                        objClientServices.InsertClient(element.QB_Name, SetLength(element.QB_Name),
                        element.Email, "", "", 233, "", "", "", element.Telephone1, "no telephone 2 yet", element.Fax, 0,
                                                       "", element.QB_ID, False, False, Now.Date, 0, Now.Date, 0)

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
                        NoRecordsCreatedorUpdated = NoRecordsCreatedorUpdated + 1
                        My.Forms.MAIN.History("Updating TL record for: " + element.QB_Name, "i")


                        '-----------------------------------------------------------------------------------------------------------------------------------------------------------------
                        ' ----------------------------------------------this part is the update--------------------------------------------------------------------------------------------. 
                        '-----------------------------------------------------------------------------------------------------------------------------------------------------------------
                    End If
                End If
            End If
            'if no, UI skip
            If UI = True Then
                IntegratedUIForm.ProgressBar1.Value = incrementbar
                incrementbar = incrementbar + 1
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
        Dim result As Int16 = 0

        'Dim CustomerAdapter As New QB_TL_IDsTableAdapters.CustomersTableAdapter()
        'For Each TimeLiveID As QB_TL_IDs.CustomersRow In CustomerAdapter.GetCustomers()
        '    If String.Compare(Trim(TimeLiveID.QuickBooks_ID), Trim(myqbID)) = 0 Then
        '        Return True
        '        Exit For
        '    End If
        'Next
        Dim CustomerAdapter As New QB_TL_IDsTableAdapters.CustomersTableAdapter()
        Dim TimeLiveIDs As QB_TL_IDs.CustomersDataTable = CustomerAdapter.GetCorrespondingTL_ID(myqbID)

        If TimeLiveIDs.Count = 1 Then
            result = 1
            My.Forms.MAIN.History("One record found in QB sync table for: " + myqbName, "i")
        End If

        If TimeLiveIDs.Count = 0 Then
            result = 0
            My.Forms.MAIN.History("No records found on QB sync table for:" + myqbName, "i")
        End If

        If TimeLiveIDs.Count > 1 Then
            result = 2
            My.Forms.MAIN.History("More than one record found for:" + myqbName, "I")
        End If

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
        If str.Length > 50 Then
            str = str.Substring(0, 50)
        End If
        Return str
    End Function


End Class

'Upddate databese record code
'My.Forms.MAIN.History("Inserting TL key into sync database and inserting to TimeLife:  " + element.Name)
''Insert record into Time Life
'objClientServices.InsertClient(element.Name, SetLength(element.Name),
'    element.Email, "", "", 233, "", "", "", element.Telephone1, "no telephone 2 yet", element.Fax, 0, "", element.QB_ID, False,
'    False, Now.Date, 0, Now.Date, 0)

''Insert record into sync database
'Dim CustomerAdapter As New QB_TL_IDsTableAdapters.CustomersTableAdapter()
'CustomerAdapter.Update(element.QB_ID, objClientServices.GetClientIdByName(element.Name))
