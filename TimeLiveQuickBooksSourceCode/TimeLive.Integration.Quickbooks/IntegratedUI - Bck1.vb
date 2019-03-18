Imports Interop.QBFC10
Imports System.Net.Mail
Imports System.Data.SqlClient
Public Class IntegratedUI
    Inherits System.Windows.Forms.Form
    Private p_token As String
    Private p_AccountId As String
    Private emailBody As String
    Dim empRet As IEmployeeRet



    '' almost the same method as above will try to work on it 
    ''
    'Private Function GetEmployeeQBData()
    '    Dim employee = New EmployeeDataStructureQB
    '    Dim objEmployeeServices As New Services.TimeLive.Employees.Employees
    '    Dim authentication As New Services.TimeLive.Employees.SecuredWebServiceHeader
    '    authentication.AuthenticatedToken = p_token
    '    objEmployeeServices.SecuredWebServiceHeaderValue = authentication

    '    Dim objServices As New Services.TimeLiveServices
    '    Dim authentication2 As New Services.SecuredWebServiceHeader
    '    authentication2.AuthenticatedToken = p_token
    '    objServices.SecuredWebServiceHeaderValue = authentication2

    '    'step1: create QBFC session manager and prepare the request
    '    Dim sessManager As QBSessionManager
    '    Dim empmsgSetRs As IMsgSetResponse

    '    Try
    '        sessManager = New QBSessionManagerClass()
    '        Dim empmsgSetRq As IMsgSetRequest = sessManager.CreateMsgSetRequest("US", 2, 0)
    '        empmsgSetRq.Attributes.OnError = ENRqOnError.roeContinue


    '        Dim syncemployee As IEmployeeQuery = empmsgSetRq.AppendEmployeeQueryRq

    '        'step2: begin QB session and send the request
    '        sessManager.OpenConnection("App", "TimeLive Quickbooks")
    '        sessManager.BeginSession("", ENOpenMode.omDontCare)
    '        empmsgSetRs = sessManager.DoRequests(empmsgSetRq)

    '        Dim emprespList As IResponseList
    '        emprespList = empmsgSetRs.ResponseList

    '        If (emprespList Is Nothing) Then
    '            Return Nothing
    '        End If

    '        ' Should only expect 1 response
    '        Dim empresp As IResponse
    '        empresp = emprespList.GetAt(0)


    '        Dim empRetList As IEmployeeRetList
    '        empRetList = empresp.Detail

    '        Dim nDepartmentId As Integer = objServices.GetDepartmentId()
    '        Dim nRoleId As Integer = objEmployeeServices.GetUserRoleId()
    '        Dim nLocationId As Integer = objServices.GetLocationId()
    '        Dim nEmployeeTypeId As Guid = objEmployeeServices.GetEmployeeTypeId()
    '        Dim nEmployeeStatusId As Integer = objEmployeeServices.GetEmployeeStatusId()
    '        Dim nWorkingDayTypeId As Guid = objEmployeeServices.GetEmployeeWorkingDayTypeId()
    '        Dim nBillingTypeId As Integer = objEmployeeServices.GetEmployeeBillingTypeId()

    '        Dim empRetListCount As Integer
    '        Dim vendorRetListCount As Integer

    '        If empRetList Is Nothing Then
    '            empRetListCount = 0
    '        Else
    '            empRetListCount = empRetList.Count
    '        End If


    '        Dim EmailAddress As String
    '        Dim pblenth As Integer = empRetListCount + vendorRetListCount - 1
    '        If pblenth >= 0 Then
    '            pgbar.Maximum = pblenth
    '        End If

    '        If (empresp.StatusCode = 0) Then
    '            ' Should only be 1 CustomerRet object returned

    '            For i As Integer = 0 To empRetList.Count - 1
    '                pgbar.Increment(i)
    '                empRet = empRetList.GetAt(i)
    '                With empRet
    '                    Try


    '                        'objEmployeeServices.InsertEmployee(EmailAddress,
    '                        'EmailAddress, .FirstName.GetValue, .LastName.GetValue, EmailAddress, "", nDepartmentId, nRoleId, nLocationId,
    '                        '233, nBillingTypeId, Now.Date, -1, 0, 6, 0, 0, nEmployeeTypeId, nEmployeeStatusId,
    '                        '"", .HiredDate.GetValue, Now.Date, nWorkingDayTypeId, System.Guid.Empty, 0, System.Guid.Empty, False, "", "", "", "", "", "", "", "", "", "Mr.", True)



    '                    Catch ex As Exception
    '                        MsgBox(ex.ToString)
    '                    End Try

    '                End With
    '            Next
    '        End If
    '        If empmsgSetRs.ResponseList.GetAt(0).StatusSeverity = "Error" Then
    '            Throw New Exception(empmsgSetRs.ResponseList.GetAt(0).StatusMessage)
    '        End If

    '    Catch ex As Exception
    '        If ex.Message.Contains("Specified email already") Then
    '            Throw New Exception("Specified user already exist.")
    '        Else
    '            Throw ex
    '        End If
    '    Finally
    '        If Not sessManager Is Nothing Then
    '            sessManager.EndSession()
    '            sessManager.CloseConnection()
    '        End If
    '    End Try
    '    Return employee
    'End Function


    Public Class CustomerDataStructureQB
        Public NoCustomer As Integer = 0
        Public DataOption As Integer
        Public dataArray As New List(Of Customer)
        'getter and setter
    End Class
    Public Class Customer
        Public CustomerName As String
        Public Email As String
        Public CustomerQB_ID As String
        Public QBLastModifiedDate As String
        Public Telephone1 As String
        Public Fax As String
        Public TimeMod As String
        ' date modified ?? 
        ' updated date ??  

        Sub New(ByVal CustomerName_in As String, ByVal Email_in As String, ByVal CustomerQB_ID_in As String, ByVal QBLastModifiedDate_in As String, ByVal Telephone1_in As String, ByVal Fax_in As String, TimeMod_in As String)
            CustomerName = CustomerName_in
            Email = Email_in
            CustomerQB_ID = CustomerQB_ID_in
            QBLastModifiedDate = QBLastModifiedDate_in
            Telephone1 = Telephone1_in
            Fax = Fax_in
            TimeMod = TimeMod_in
        End Sub
        Public Property Name() As String
            Get
                Return CustomerName
            End Get
            Set(ByVal value As String)
                CustomerName = value
            End Set
        End Property
        Public Property TimeModified() As String
            Get
                Return TimeMod
            End Get
            Set(ByVal value As String)
                TimeMod = value
            End Set
        End Property

        Public Property QB_ID() As String
            Get
                Return CustomerQB_ID
            End Get
            Set(ByVal value As String)
                CustomerQB_ID = value
            End Set
        End Property

    End Class

    Public Class EmployeeDataStructureQB
        Public NoEmployee As Integer = 0
        ' check if its Employee or job etc.. or dont need it.
        Public DataOption As Integer
        Public dataArray As New List(Of Employee)

        'getter and setter

    End Class
    Public Class Employee
        Public EmployeeName As String
        Public Email As String
        Public EmployeeQB_ID As String

        Public QBLastModifiedDate As String
        Public Telephone1 As String
        Public Fax As String
        Public TimeMod As String
        ' date modified ?? 
        ' updated date ??  

        Sub New(ByVal EmployeeName_in As String, ByVal Email_in As String, ByVal EmployeeQB_ID_in As String, ByVal QBLastModifiedDate_in As String, ByVal Telephone1_in As String, ByVal Fax_in As String, TimeMod_in As String)
            EmployeeName = EmployeeName_in
            Email = Email_in
            EmployeeQB_ID = EmployeeQB_ID_in

            QBLastModifiedDate = QBLastModifiedDate_in
            Telephone1 = Telephone1_in
            Fax = Fax_in
            TimeMod = TimeMod_in
        End Sub
        Public Property Name() As String
            Get
                Return EmployeeName
            End Get
            Set(ByVal value As String)
                EmployeeName = value
            End Set
        End Property
        Public Property TimeModified() As String
            Get
                Return TimeMod
            End Get
            Set(ByVal value As String)
                TimeMod = value
            End Set
        End Property

        Public Property QB_ID() As String
            Get
                Return EmployeeQB_ID
            End Get
            Set(ByVal value As String)
                EmployeeQB_ID = value
            End Set
        End Property

    End Class

    Public Class VendorDataStructureQB
        Public NoVendor As Integer = 0
        ' check if its Vendor or job etc.. or dont need it.
        Public DataOption As Integer
        Public dataArray As New List(Of Vendor)

        'getter and setter

    End Class
    Public Class Vendor
        Public VendorName As String
        Public Email As String
        Public VendorQB_ID As String

        Public QBLastModifiedDate As String
        Public Telephone1 As String
        Public Fax As String
        Public TimeMod As String
        ' date modified ?? 
        ' updated date ??  

        Sub New(ByVal VendorName_in As String, ByVal Email_in As String, ByVal VendorQB_ID_in As String, ByVal QBLastModifiedDate_in As String, ByVal Telephone1_in As String, ByVal Fax_in As String, TimeMod_in As String)
            VendorName = VendorName_in
            Email = Email_in
            VendorQB_ID = VendorQB_ID_in

            QBLastModifiedDate = QBLastModifiedDate_in
            Telephone1 = Telephone1_in
            Fax = Fax_in
            TimeMod = TimeMod_in
        End Sub
        Public Property Name() As String
            Get
                Return VendorName
            End Get
            Set(ByVal value As String)
                VendorName = value
            End Set
        End Property
        Public Property TimeModified() As String
            Get
                Return TimeMod
            End Get
            Set(ByVal value As String)
                TimeMod = value
            End Set
        End Property

        Public Property QB_ID() As String
            Get
                Return VendorQB_ID
            End Get
            Set(ByVal value As String)
                VendorQB_ID = value
            End Set
        End Property

    End Class


    '--------- Hi  
    '---------
    ''---------
    '---------I think we can just make one data structure. and know what parameter what we are passing
    '--------- However, we can make 3 of them. 
    '--------- It serve the same goal.
    '---------
    'Public Class CustomerDataStructureQB
    '    Public NoCustomer As Integer = 0

    '    Public DataOption As Integer   -- dataoption
    '    Public dataArray As New List(Of Customer)

    '    'getter and setter

    'End Class
    ' ---------check if its customer or job etc.. or dont need it or we could do another way.
    '-----------------------------------------------------------------------------------------------
    ' https://stackoverflow.com/questions/19110559/create-an-object-from-another-objects-type.
    ' I will look into this. I think there is a way to do it

    'use 1 , 2 ,3 to seperated it
    'Dim empRet As ICustomerRet
    'Dim syncemployee As ICustomerQuery = empmsgSetRq.AppendCustomerQueryRq
    'Dim empRetList As ICustomerRetList
    ' These 3 varibles that we could play with to do the save thing for getting QB data

    '-----------------------------------------------------------------


    Public Sub IntegratedUI_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Dim customer = GetQBData(1)
        Dim customer = GetCustomerQBData(1)
        ListALL(customer)

    End Sub

    Public Overloads Sub Show(ByVal token As String, ByVal AccountId As String)
        If token Is Nothing Then
            MsgBox("Please Login")
        Else
            p_AccountId = AccountId
            p_token = token
            MyBase.Show()
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Me.Close()
    End Sub

    Private Sub IntegratedUI_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        My.Forms.Login.LastRunDateAndTime = Now.ToString
        My.Settings.LastRunTime = My.Forms.Login.LastRunDateAndTime

        MainMenu.Enabled = True
    End Sub

    Private Sub btnTransfer_Click(sender As Object, e As EventArgs) Handles btnTransfer.Click

        Dim customer = GetCustomerQBData(1)
        ' SendGMail("Updatng", "Body")
        'sendEmail()

        If String.Compare(btnTransfer.Text, "Emulate") = 0 Then
            ListALL(customer)
            btnTransfer.Text = "Execute"
        Else
            ' ListALL(customer)
            QBTransferToTL(customer)
            btnTransfer.Text = "Emulate"
        End If
    End Sub


    Function GetCustomerQBData(ByVal dataOption)

        ' customer, employee, etc... 
        'If (dataOption =) Then
        'End If
        ' do the clients for now  later put it in a case statement
        Dim EmailAddress As String
        Dim Telephone1 As String
        Dim Telephone2 As String
        Dim Fax As String
        Dim My_Time As String
        Dim customerData As New CustomerDataStructureQB
        Dim EmployeeData As New EmployeeDataStructureQB
        Dim VendorData As New VendorDataStructureQB

        ' dont think will need this
        'Dim objClientServices As New Services.TimeLive.Clients.Clients
        'Dim objClient As New Services.TimeLive.Clients.Client
        'Dim authentication As New Services.TimeLive.Clients.SecuredWebServiceHeader
        'authentication.AuthenticatedToken = p_token
        'objClientServices.SecuredWebServiceHeaderValue = authentication

        'step1: create QBFC session manager and prepare the request
        Dim sessManager As QBSessionManager
        Dim msgSetRs As IMsgSetResponse


        Try
            sessManager = New QBSessionManagerClass()
            Dim msgSetRq As IMsgSetRequest = sessManager.CreateMsgSetRequest("US", 2, 0)
            msgSetRq.Attributes.OnError = ENRqOnError.roeContinue

            '-------------------------1---------------------------------------------
            Dim synccust As ICustomerQuery = msgSetRq.AppendCustomerQueryRq

            'step2: begin QB session and send the request
            sessManager.OpenConnection("App", "TimeLive Quickbooks")
            sessManager.BeginSession("", ENOpenMode.omDontCare)
            msgSetRs = sessManager.DoRequests(msgSetRq)


            Dim respList As IResponseList
            respList = msgSetRs.ResponseList

            If (respList Is Nothing) Then
                ' no data
                MsgBox("No data...")
                Return Nothing
            End If
            ' Should only expect 1 response
            Dim resp As IResponse
            resp = respList.GetAt(0)
            If (resp.StatusCode = 0) Then

                '----------------------2------------------------------------------------
                Dim custRetList As ICustomerRetList
                custRetList = resp.Detail

                '------------------------------3-----------------------------------
                Dim custRet As ICustomerRet
                Dim pblenth As Integer = custRetList.Count - 1
                'If pblenth >= 0 Then
                '    pgbar.Maximum = pblenth
                'End If
                For i As Integer = 0 To custRetList.Count - 1
                    'Dim i = 0
                    'pgbar.Increment(i)
                    custRet = custRetList.GetAt(i)

                    With custRet
                        'MsgBox("here")

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

                            If .TimeModified Is Nothing Then
                                My_Time = .TimeCreated.GetValue.ToString

                            Else
                                My_Time = .TimeModified.GetValue.ToString()
                            End If
                            ' MsgBox(time)
                            ' will check which type data should be added 
                            customerData.NoCustomer = customerData.NoCustomer + 1
                            customerData.dataArray.Add(New Customer(.Name.GetValue, EmailAddress, .ListID.GetValue, "", Telephone1, Fax, My_Time))

                        End If
                    End With
                Next
            End If
            If msgSetRs.ResponseList.GetAt(0).StatusSeverity = "Error" Then
                Throw New Exception(msgSetRs.ResponseList.GetAt(0).StatusMessage)
            End If
        Catch ex As Exception
            MsgBox(ex.ToString)

            Throw ex
        Finally
            If Not sessManager Is Nothing Then
                sessManager.EndSession()
                sessManager.CloseConnection()
            End If
        End Try



        ' checking which one is populated 
        ' return the one that is populated

        If (EmployeeData.NoEmployee > 0) Then
            Return EmployeeData
        ElseIf customerData.NoCustomer > 0 Then
            Return customerData
        Else
            Return VendorData
        End If

    End Function
    ' this is the emulate and exucute
    Private Function QBTransferToTL(ByVal objData As CustomerDataStructureQB)

        Dim objClientServices As New Services.TimeLive.Clients.Clients
        Dim objClient As New Services.TimeLive.Clients.Client
        Dim authentication As New Services.TimeLive.Clients.SecuredWebServiceHeader
        authentication.AuthenticatedToken = p_token
        objClientServices.SecuredWebServiceHeaderValue = authentication

        'check if it is automating if it is not do select all of it that modfied

        'sellectAll_Mod()

        For Each element As Customer In objData.dataArray
            ' give it a varible to see if it is automating
            If Is_checked_checkBox(element.CustomerName) = True Then

                If ISQBID_In_DataTable(element.CustomerQB_ID) = False Then

                    localTB1.Text += "Missing QB ID " + vbNewLine
                    localTB1.Text += ">>Inserting to database key table and Inserting to TL:  " + element.CustomerName + vbNewLine

                    My.Forms.StatusWindow.TBStatus.Text += "Missing QB ID " + vbNewLine
                    My.Forms.StatusWindow.TBStatus.Text += ">>Inserting database key table and Inserting to TL:  " + element.CustomerName + vbNewLine
                    ' async or just insert

                    objClientServices.InsertClient(element.Name, SetLength(element.CustomerName),
                    element.Email, "", "", 233, "", "", "", element.Telephone1, "no telephone 2 yet", element.Fax, 0, "", element.CustomerQB_ID, False,
                    False, Now.Date, 0, Now.Date, 0)



                    Dim CustomerAdapter As New QB_TL_IDsTableAdapters.CustomersTableAdapter()

                    ' I think it does not matter
                    ' objClientServices.GetClientIdByName(element.CustomerName) or element.CustomerTL_ID 
                    CustomerAdapter.Insert(element.CustomerQB_ID, objClientServices.GetClientIdByName(element.CustomerName))


                Else
                    ' CheckifithasaTLID;if it does not at to timelive using webservice. Afterwards, update database record whittTL ID. 
                    ' MsgBox(element.CustomerName)
                    If ISTLID_In_DataTable(element.CustomerQB_ID, objClientServices.GetClientIdByName(element.CustomerName)) = False Then


                        localTB1.Text += "Missing TL ID " + vbNewLine
                        localTB1.Text += ">>Updating database key table and Inserting to TL:  " + element.CustomerName + vbNewLine
                        My.Forms.StatusWindow.TBStatus.Text += "Missing TL ID " + vbNewLine
                        My.Forms.StatusWindow.TBStatus.Text += ">>Updating database key table and Inserting to TL:  " + element.CustomerName + vbNewLine

                        objClientServices.InsertClient(element.Name, SetLength(element.CustomerName),
                    element.Email, "", "", 233, "", "", "", element.Telephone1, "no telephone 2 yet", element.Fax, 0, "", element.CustomerQB_ID, False,
                    False, Now.Date, 0, Now.Date, 0)

                        Dim CustomerAdapter As New QB_TL_IDsTableAdapters.CustomersTableAdapter()
                        CustomerAdapter.Update(element.CustomerQB_ID, objClientServices.GetClientIdByName(element.CustomerName))

                    Else
                        localTB1.Text += ">>Updating:  " + element.CustomerName + vbNewLine
                        My.Forms.StatusWindow.TBStatus.Text += ">>Updating:  " + element.CustomerName + vbNewLine

                        '-----------------------------------------------------------------------------------------------------------------------------------------------------------------
                        ' ----------------------------------------------this part is the update--------------------------------------------------------------------------------------------. 
                        '-----------------------------------------------------------------------------------------------------------------------------------------------------------------

                    End If
                End If
            End If
        Next

    End Function

    Public Function SetLength(ByVal str As String) As String
        If str.Length > 50 Then
            str = str.Substring(0, 50)
        End If
        Return str
    End Function

    Public Sub automating()
        ' Call each Function

        Dim customer = GetCustomerQBData(1)
        ListALL(customer)
        QBTransferToTL(customer)
    End Sub

    Private Sub ListALL(ByRef objData As CustomerDataStructureQB)
        localTB1.Text += ">>Reading all the data" + vbNewLine
        localTB1.Text += "Total Customer : " + objData.NoCustomer.ToString + vbNewLine

        For Each element As Customer In objData.dataArray
            My.Forms.StatusWindow.TBStatus.Text += "Name: " + element.CustomerName + vbNewLine
            My.Forms.StatusWindow.TBStatus.Text += "QB_ID: " + element.CustomerQB_ID + vbNewLine
            My.Forms.StatusWindow.TBStatus.Text += "Email: " + element.Email + vbNewLine
            My.Forms.StatusWindow.TBStatus.Text += "Telephone: " + element.Telephone1 + vbNewLine

            localTB1.Text += "Name: " + element.CustomerName + vbNewLine
            localTB1.Text += "QB_ID: " + element.CustomerQB_ID + vbNewLine
            localTB1.Text += "Email: " + element.Email + vbNewLine
            localTB1.Text += "Telephone: " + element.Telephone1 + vbNewLine


        Next
        DataGridView1.DataSource = objData.dataArray
        sellectAll_Mod()
        ' MsgBox("This is the StartWindow" + My.Forms.StatusWindow.TBStatus.Text.ToString)
    End Sub

    Private Function ISTLID_In_DataTable(ByVal myqbID As String, ByVal myTLID As String)
        Dim result As Boolean = False
        Dim CustomerAdapter As New QB_TL_IDsTableAdapters.CustomersTableAdapter()
        Dim TimeLiveIDs As QB_TL_IDs.CustomersDataTable = CustomerAdapter.GetCorrespondingTL_ID(myqbID)
        If myTLID Is Nothing Then
            Return False
        End If
        For Each TimeLiveID As QB_TL_IDs.CustomersRow In TimeLiveIDs
            Dim v1 = Trim(myTLID)
            Dim v2 = Trim(TimeLiveID.TimeLive_ID)
            ' localTB1.Text += ">>Testing IS QB Id In database  " + " TL ID: " & v1 & "--- Qb ID: " + v2 + vbNewLine

            If String.Compare(v1, v2) = 0 Then
                result = True
                Exit For
            Else

            End If
        Next
        Return result
    End Function

    Private Function ISQBID_In_DataTable(ByVal myqbID As String)
        Dim CustomerAdapter As New QB_TL_IDsTableAdapters.CustomersTableAdapter()
        For Each TimeLiveID As QB_TL_IDs.CustomersRow In CustomerAdapter.GetCustomers()
            ' localTB1.Text += ">>Testing IS QB Id In database  " + " TL: " & myqbID & "--- ID: " + TimeLiveID.QuickBooks_ID + vbNewLine
            If String.Compare(Trim(TimeLiveID.QuickBooks_ID), Trim(myqbID)) = 0 Then

                Return True
                Exit For
            End If
        Next
        'localTB1.Text += "no found "
        Return False
    End Function

    Private Function Is_checked_checkBox(ByVal my_var As String)
        Dim ans As Boolean = False
        For Each row As DataGridViewRow In DataGridView1.Rows
            Dim chk As DataGridViewCheckBoxCell = row.Cells("ckBox")
            Dim name_chk As DataGridViewCell = row.Cells(1)

            ' MsgBox(name_chk.Value.ToString + "--" + my_var.ToString)
            Dim st1 = Trim(name_chk.Value.ToString)
            Dim st2 = Trim(my_var.ToString)
            '' localTB1.Text += name_chk.Value.ToString + "--" + my_var.ToString + "Checked: " + chk.Value + "COmpare:  " + String.Compare(st1, st2).ToString + vbNewLine
            If chk.Value = True And String.Compare(st1, st2) = 0 Then
                ' MsgBox("Comparing : " + name_chk.Value.ToString + "   ===     " + my_var)
                Return True
            End If
        Next
        Return False
    End Function

    Private Sub sellectAll_Mod()
        'Make it check if the the time it is running is greater than then time we modify it. 
        'last time we run it 
        Dim lastone As Integer = Convert.ToInt32(DataGridView1.Rows.Count.ToString) - 1
        ' MsgBox(lastone)
        For Each row As DataGridViewRow In DataGridView1.Rows
            'check which 
            If String.Compare(row.Cells(2).Value, My.Forms.Login.LastRunDateAndTime) >= 0 Then
                MsgBox("Runtime:" + My.Forms.Login.LastRunDateAndTime + " Mod or create time:" + row.Cells(2).Value + " is : " + String.Compare(row.Cells(2).Value, My.Forms.Login.LastRunDateAndTime).ToString)
                row.Cells("ckBox").Value = True
                Dim check As Integer = row.Cells("ckBox").RowIndex
            End If
        Next

    End Sub


End Class