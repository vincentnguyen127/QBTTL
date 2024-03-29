﻿'------------------------------------------------------------------------------
' <auto-generated>
'     This code was generated by a tool.
'     Runtime Version:4.0.30319.42000
'
'     Changes to this file may cause incorrect behavior and will be lost if
'     the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------

Option Strict Off
Option Explicit On

Imports System
Imports System.ComponentModel
Imports System.Diagnostics
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Xml.Serialization

'
'This source code was auto-generated by Microsoft.VSDesigner, Version 4.0.30319.42000.
'
Namespace Services
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.3056.0"),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code"),  _
     System.Web.Services.WebServiceBindingAttribute(Name:="TimeLiveServicesSoap", [Namespace]:="http://tempuri.org/")>  _
    Partial Public Class TimeLiveServices
        Inherits System.Web.Services.Protocols.SoapHttpClientProtocol
        
        Private securedWebServiceHeaderValueField As SecuredWebServiceHeader
        
        Private AuthenticateUserOperationCompleted As System.Threading.SendOrPostCallback
        
        Private AuthenticateMobileUserOperationCompleted As System.Threading.SendOrPostCallback
        
        Private GetAccountIdForMobileOperationCompleted As System.Threading.SendOrPostCallback
        
        Private GetAccountEmployeeIdForMobileOperationCompleted As System.Threading.SendOrPostCallback
        
        Private GetAccountIdOperationCompleted As System.Threading.SendOrPostCallback
        
        Private GetAccountEmployeeIdOperationCompleted As System.Threading.SendOrPostCallback
        
        Private GetDepartmentIdOperationCompleted As System.Threading.SendOrPostCallback
        
        Private GetLocationIdOperationCompleted As System.Threading.SendOrPostCallback
        
        Private GetWorkTypeIdOperationCompleted As System.Threading.SendOrPostCallback
        
        Private GetCurrencyIdOperationCompleted As System.Threading.SendOrPostCallback
        
        Private useDefaultCredentialsSetExplicitly As Boolean
        
        '''<remarks/>
        Public Sub New()
            MyBase.New
            Me.Url = Global.TimeLive.Quickbooks.Integrator.My.MySettings.Default.TimeLive_Integration_Quickbooks_Services_TimeLiveServices
            If (Me.IsLocalFileSystemWebService(Me.Url) = true) Then
                Me.UseDefaultCredentials = true
                Me.useDefaultCredentialsSetExplicitly = false
            Else
                Me.useDefaultCredentialsSetExplicitly = true
            End If
        End Sub
        
        Public Property SecuredWebServiceHeaderValue() As SecuredWebServiceHeader
            Get
                Return Me.securedWebServiceHeaderValueField
            End Get
            Set
                Me.securedWebServiceHeaderValueField = value
            End Set
        End Property
        
        Public Shadows Property Url() As String
            Get
                Return MyBase.Url
            End Get
            Set
                If (((Me.IsLocalFileSystemWebService(MyBase.Url) = true)  _
                            AndAlso (Me.useDefaultCredentialsSetExplicitly = false))  _
                            AndAlso (Me.IsLocalFileSystemWebService(value) = false)) Then
                    MyBase.UseDefaultCredentials = false
                End If
                MyBase.Url = value
            End Set
        End Property
        
        Public Shadows Property UseDefaultCredentials() As Boolean
            Get
                Return MyBase.UseDefaultCredentials
            End Get
            Set
                MyBase.UseDefaultCredentials = value
                Me.useDefaultCredentialsSetExplicitly = true
            End Set
        End Property
        
        '''<remarks/>
        Public Event AuthenticateUserCompleted As AuthenticateUserCompletedEventHandler
        
        '''<remarks/>
        Public Event AuthenticateMobileUserCompleted As AuthenticateMobileUserCompletedEventHandler
        
        '''<remarks/>
        Public Event GetAccountIdForMobileCompleted As GetAccountIdForMobileCompletedEventHandler
        
        '''<remarks/>
        Public Event GetAccountEmployeeIdForMobileCompleted As GetAccountEmployeeIdForMobileCompletedEventHandler
        
        '''<remarks/>
        Public Event GetAccountIdCompleted As GetAccountIdCompletedEventHandler
        
        '''<remarks/>
        Public Event GetAccountEmployeeIdCompleted As GetAccountEmployeeIdCompletedEventHandler
        
        '''<remarks/>
        Public Event GetDepartmentIdCompleted As GetDepartmentIdCompletedEventHandler
        
        '''<remarks/>
        Public Event GetLocationIdCompleted As GetLocationIdCompletedEventHandler
        
        '''<remarks/>
        Public Event GetWorkTypeIdCompleted As GetWorkTypeIdCompletedEventHandler
        
        '''<remarks/>
        Public Event GetCurrencyIdCompleted As GetCurrencyIdCompletedEventHandler
        
        '''<remarks/>
        <System.Web.Services.Protocols.SoapHeaderAttribute("SecuredWebServiceHeaderValue"),  _
         System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/AuthenticateUser", RequestNamespace:="http://tempuri.org/", ResponseNamespace:="http://tempuri.org/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>  _
        Public Function AuthenticateUser() As String
            Dim results() As Object = Me.Invoke("AuthenticateUser", New Object(-1) {})
            Return CType(results(0),String)
        End Function
        
        '''<remarks/>
        Public Overloads Sub AuthenticateUserAsync()
            Me.AuthenticateUserAsync(Nothing)
        End Sub
        
        '''<remarks/>
        Public Overloads Sub AuthenticateUserAsync(ByVal userState As Object)
            If (Me.AuthenticateUserOperationCompleted Is Nothing) Then
                Me.AuthenticateUserOperationCompleted = AddressOf Me.OnAuthenticateUserOperationCompleted
            End If
            Me.InvokeAsync("AuthenticateUser", New Object(-1) {}, Me.AuthenticateUserOperationCompleted, userState)
        End Sub
        
        Private Sub OnAuthenticateUserOperationCompleted(ByVal arg As Object)
            If (Not (Me.AuthenticateUserCompletedEvent) Is Nothing) Then
                Dim invokeArgs As System.Web.Services.Protocols.InvokeCompletedEventArgs = CType(arg,System.Web.Services.Protocols.InvokeCompletedEventArgs)
                RaiseEvent AuthenticateUserCompleted(Me, New AuthenticateUserCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState))
            End If
        End Sub
        
        '''<remarks/>
        <System.Web.Services.Protocols.SoapHeaderAttribute("SecuredWebServiceHeaderValue"),  _
         System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/AuthenticateMobileUser", RequestNamespace:="http://tempuri.org/", ResponseNamespace:="http://tempuri.org/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>  _
        Public Function AuthenticateMobileUser() As String
            Dim results() As Object = Me.Invoke("AuthenticateMobileUser", New Object(-1) {})
            Return CType(results(0),String)
        End Function
        
        '''<remarks/>
        Public Overloads Sub AuthenticateMobileUserAsync()
            Me.AuthenticateMobileUserAsync(Nothing)
        End Sub
        
        '''<remarks/>
        Public Overloads Sub AuthenticateMobileUserAsync(ByVal userState As Object)
            If (Me.AuthenticateMobileUserOperationCompleted Is Nothing) Then
                Me.AuthenticateMobileUserOperationCompleted = AddressOf Me.OnAuthenticateMobileUserOperationCompleted
            End If
            Me.InvokeAsync("AuthenticateMobileUser", New Object(-1) {}, Me.AuthenticateMobileUserOperationCompleted, userState)
        End Sub
        
        Private Sub OnAuthenticateMobileUserOperationCompleted(ByVal arg As Object)
            If (Not (Me.AuthenticateMobileUserCompletedEvent) Is Nothing) Then
                Dim invokeArgs As System.Web.Services.Protocols.InvokeCompletedEventArgs = CType(arg,System.Web.Services.Protocols.InvokeCompletedEventArgs)
                RaiseEvent AuthenticateMobileUserCompleted(Me, New AuthenticateMobileUserCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState))
            End If
        End Sub
        
        '''<remarks/>
        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/GetAccountIdForMobile", RequestNamespace:="http://tempuri.org/", ResponseNamespace:="http://tempuri.org/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>  _
        Public Function GetAccountIdForMobile(ByVal Username As String, ByVal Password As String) As Integer
            Dim results() As Object = Me.Invoke("GetAccountIdForMobile", New Object() {Username, Password})
            Return CType(results(0),Integer)
        End Function
        
        '''<remarks/>
        Public Overloads Sub GetAccountIdForMobileAsync(ByVal Username As String, ByVal Password As String)
            Me.GetAccountIdForMobileAsync(Username, Password, Nothing)
        End Sub
        
        '''<remarks/>
        Public Overloads Sub GetAccountIdForMobileAsync(ByVal Username As String, ByVal Password As String, ByVal userState As Object)
            If (Me.GetAccountIdForMobileOperationCompleted Is Nothing) Then
                Me.GetAccountIdForMobileOperationCompleted = AddressOf Me.OnGetAccountIdForMobileOperationCompleted
            End If
            Me.InvokeAsync("GetAccountIdForMobile", New Object() {Username, Password}, Me.GetAccountIdForMobileOperationCompleted, userState)
        End Sub
        
        Private Sub OnGetAccountIdForMobileOperationCompleted(ByVal arg As Object)
            If (Not (Me.GetAccountIdForMobileCompletedEvent) Is Nothing) Then
                Dim invokeArgs As System.Web.Services.Protocols.InvokeCompletedEventArgs = CType(arg,System.Web.Services.Protocols.InvokeCompletedEventArgs)
                RaiseEvent GetAccountIdForMobileCompleted(Me, New GetAccountIdForMobileCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState))
            End If
        End Sub
        
        '''<remarks/>
        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/GetAccountEmployeeIdForMobile", RequestNamespace:="http://tempuri.org/", ResponseNamespace:="http://tempuri.org/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>  _
        Public Function GetAccountEmployeeIdForMobile(ByVal Username As String, ByVal Password As String) As Integer
            Dim results() As Object = Me.Invoke("GetAccountEmployeeIdForMobile", New Object() {Username, Password})
            Return CType(results(0),Integer)
        End Function
        
        '''<remarks/>
        Public Overloads Sub GetAccountEmployeeIdForMobileAsync(ByVal Username As String, ByVal Password As String)
            Me.GetAccountEmployeeIdForMobileAsync(Username, Password, Nothing)
        End Sub
        
        '''<remarks/>
        Public Overloads Sub GetAccountEmployeeIdForMobileAsync(ByVal Username As String, ByVal Password As String, ByVal userState As Object)
            If (Me.GetAccountEmployeeIdForMobileOperationCompleted Is Nothing) Then
                Me.GetAccountEmployeeIdForMobileOperationCompleted = AddressOf Me.OnGetAccountEmployeeIdForMobileOperationCompleted
            End If
            Me.InvokeAsync("GetAccountEmployeeIdForMobile", New Object() {Username, Password}, Me.GetAccountEmployeeIdForMobileOperationCompleted, userState)
        End Sub
        
        Private Sub OnGetAccountEmployeeIdForMobileOperationCompleted(ByVal arg As Object)
            If (Not (Me.GetAccountEmployeeIdForMobileCompletedEvent) Is Nothing) Then
                Dim invokeArgs As System.Web.Services.Protocols.InvokeCompletedEventArgs = CType(arg,System.Web.Services.Protocols.InvokeCompletedEventArgs)
                RaiseEvent GetAccountEmployeeIdForMobileCompleted(Me, New GetAccountEmployeeIdForMobileCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState))
            End If
        End Sub
        
        '''<remarks/>
        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/GetAccountId", RequestNamespace:="http://tempuri.org/", ResponseNamespace:="http://tempuri.org/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>  _
        Public Function GetAccountId(ByVal Username As String, ByVal Password As String) As Integer
            Dim results() As Object = Me.Invoke("GetAccountId", New Object() {Username, Password})
            Return CType(results(0),Integer)
        End Function
        
        '''<remarks/>
        Public Overloads Sub GetAccountIdAsync(ByVal Username As String, ByVal Password As String)
            Me.GetAccountIdAsync(Username, Password, Nothing)
        End Sub
        
        '''<remarks/>
        Public Overloads Sub GetAccountIdAsync(ByVal Username As String, ByVal Password As String, ByVal userState As Object)
            If (Me.GetAccountIdOperationCompleted Is Nothing) Then
                Me.GetAccountIdOperationCompleted = AddressOf Me.OnGetAccountIdOperationCompleted
            End If
            Me.InvokeAsync("GetAccountId", New Object() {Username, Password}, Me.GetAccountIdOperationCompleted, userState)
        End Sub
        
        Private Sub OnGetAccountIdOperationCompleted(ByVal arg As Object)
            If (Not (Me.GetAccountIdCompletedEvent) Is Nothing) Then
                Dim invokeArgs As System.Web.Services.Protocols.InvokeCompletedEventArgs = CType(arg,System.Web.Services.Protocols.InvokeCompletedEventArgs)
                RaiseEvent GetAccountIdCompleted(Me, New GetAccountIdCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState))
            End If
        End Sub
        
        '''<remarks/>
        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/GetAccountEmployeeId", RequestNamespace:="http://tempuri.org/", ResponseNamespace:="http://tempuri.org/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>  _
        Public Function GetAccountEmployeeId(ByVal Username As String, ByVal Password As String) As Integer
            Dim results() As Object = Me.Invoke("GetAccountEmployeeId", New Object() {Username, Password})
            Return CType(results(0),Integer)
        End Function
        
        '''<remarks/>
        Public Overloads Sub GetAccountEmployeeIdAsync(ByVal Username As String, ByVal Password As String)
            Me.GetAccountEmployeeIdAsync(Username, Password, Nothing)
        End Sub
        
        '''<remarks/>
        Public Overloads Sub GetAccountEmployeeIdAsync(ByVal Username As String, ByVal Password As String, ByVal userState As Object)
            If (Me.GetAccountEmployeeIdOperationCompleted Is Nothing) Then
                Me.GetAccountEmployeeIdOperationCompleted = AddressOf Me.OnGetAccountEmployeeIdOperationCompleted
            End If
            Me.InvokeAsync("GetAccountEmployeeId", New Object() {Username, Password}, Me.GetAccountEmployeeIdOperationCompleted, userState)
        End Sub
        
        Private Sub OnGetAccountEmployeeIdOperationCompleted(ByVal arg As Object)
            If (Not (Me.GetAccountEmployeeIdCompletedEvent) Is Nothing) Then
                Dim invokeArgs As System.Web.Services.Protocols.InvokeCompletedEventArgs = CType(arg,System.Web.Services.Protocols.InvokeCompletedEventArgs)
                RaiseEvent GetAccountEmployeeIdCompleted(Me, New GetAccountEmployeeIdCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState))
            End If
        End Sub
        
        '''<remarks/>
        <System.Web.Services.Protocols.SoapHeaderAttribute("SecuredWebServiceHeaderValue"),  _
         System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/GetDepartmentId", RequestNamespace:="http://tempuri.org/", ResponseNamespace:="http://tempuri.org/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>  _
        Public Function GetDepartmentId() As Integer
            Dim results() As Object = Me.Invoke("GetDepartmentId", New Object(-1) {})
            Return CType(results(0),Integer)
        End Function
        
        '''<remarks/>
        Public Overloads Sub GetDepartmentIdAsync()
            Me.GetDepartmentIdAsync(Nothing)
        End Sub
        
        '''<remarks/>
        Public Overloads Sub GetDepartmentIdAsync(ByVal userState As Object)
            If (Me.GetDepartmentIdOperationCompleted Is Nothing) Then
                Me.GetDepartmentIdOperationCompleted = AddressOf Me.OnGetDepartmentIdOperationCompleted
            End If
            Me.InvokeAsync("GetDepartmentId", New Object(-1) {}, Me.GetDepartmentIdOperationCompleted, userState)
        End Sub
        
        Private Sub OnGetDepartmentIdOperationCompleted(ByVal arg As Object)
            If (Not (Me.GetDepartmentIdCompletedEvent) Is Nothing) Then
                Dim invokeArgs As System.Web.Services.Protocols.InvokeCompletedEventArgs = CType(arg,System.Web.Services.Protocols.InvokeCompletedEventArgs)
                RaiseEvent GetDepartmentIdCompleted(Me, New GetDepartmentIdCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState))
            End If
        End Sub
        
        '''<remarks/>
        <System.Web.Services.Protocols.SoapHeaderAttribute("SecuredWebServiceHeaderValue"),  _
         System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/GetLocationId", RequestNamespace:="http://tempuri.org/", ResponseNamespace:="http://tempuri.org/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>  _
        Public Function GetLocationId() As Integer
            Dim results() As Object = Me.Invoke("GetLocationId", New Object(-1) {})
            Return CType(results(0),Integer)
        End Function
        
        '''<remarks/>
        Public Overloads Sub GetLocationIdAsync()
            Me.GetLocationIdAsync(Nothing)
        End Sub
        
        '''<remarks/>
        Public Overloads Sub GetLocationIdAsync(ByVal userState As Object)
            If (Me.GetLocationIdOperationCompleted Is Nothing) Then
                Me.GetLocationIdOperationCompleted = AddressOf Me.OnGetLocationIdOperationCompleted
            End If
            Me.InvokeAsync("GetLocationId", New Object(-1) {}, Me.GetLocationIdOperationCompleted, userState)
        End Sub
        
        Private Sub OnGetLocationIdOperationCompleted(ByVal arg As Object)
            If (Not (Me.GetLocationIdCompletedEvent) Is Nothing) Then
                Dim invokeArgs As System.Web.Services.Protocols.InvokeCompletedEventArgs = CType(arg,System.Web.Services.Protocols.InvokeCompletedEventArgs)
                RaiseEvent GetLocationIdCompleted(Me, New GetLocationIdCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState))
            End If
        End Sub
        
        '''<remarks/>
        <System.Web.Services.Protocols.SoapHeaderAttribute("SecuredWebServiceHeaderValue"),  _
         System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/GetWorkTypeId", RequestNamespace:="http://tempuri.org/", ResponseNamespace:="http://tempuri.org/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>  _
        Public Function GetWorkTypeId() As Integer
            Dim results() As Object = Me.Invoke("GetWorkTypeId", New Object(-1) {})
            Return CType(results(0),Integer)
        End Function
        
        '''<remarks/>
        Public Overloads Sub GetWorkTypeIdAsync()
            Me.GetWorkTypeIdAsync(Nothing)
        End Sub
        
        '''<remarks/>
        Public Overloads Sub GetWorkTypeIdAsync(ByVal userState As Object)
            If (Me.GetWorkTypeIdOperationCompleted Is Nothing) Then
                Me.GetWorkTypeIdOperationCompleted = AddressOf Me.OnGetWorkTypeIdOperationCompleted
            End If
            Me.InvokeAsync("GetWorkTypeId", New Object(-1) {}, Me.GetWorkTypeIdOperationCompleted, userState)
        End Sub
        
        Private Sub OnGetWorkTypeIdOperationCompleted(ByVal arg As Object)
            If (Not (Me.GetWorkTypeIdCompletedEvent) Is Nothing) Then
                Dim invokeArgs As System.Web.Services.Protocols.InvokeCompletedEventArgs = CType(arg,System.Web.Services.Protocols.InvokeCompletedEventArgs)
                RaiseEvent GetWorkTypeIdCompleted(Me, New GetWorkTypeIdCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState))
            End If
        End Sub
        
        '''<remarks/>
        <System.Web.Services.Protocols.SoapHeaderAttribute("SecuredWebServiceHeaderValue"),  _
         System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/GetCurrencyId", RequestNamespace:="http://tempuri.org/", ResponseNamespace:="http://tempuri.org/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>  _
        Public Function GetCurrencyId() As Integer
            Dim results() As Object = Me.Invoke("GetCurrencyId", New Object(-1) {})
            Return CType(results(0),Integer)
        End Function
        
        '''<remarks/>
        Public Overloads Sub GetCurrencyIdAsync()
            Me.GetCurrencyIdAsync(Nothing)
        End Sub
        
        '''<remarks/>
        Public Overloads Sub GetCurrencyIdAsync(ByVal userState As Object)
            If (Me.GetCurrencyIdOperationCompleted Is Nothing) Then
                Me.GetCurrencyIdOperationCompleted = AddressOf Me.OnGetCurrencyIdOperationCompleted
            End If
            Me.InvokeAsync("GetCurrencyId", New Object(-1) {}, Me.GetCurrencyIdOperationCompleted, userState)
        End Sub
        
        Private Sub OnGetCurrencyIdOperationCompleted(ByVal arg As Object)
            If (Not (Me.GetCurrencyIdCompletedEvent) Is Nothing) Then
                Dim invokeArgs As System.Web.Services.Protocols.InvokeCompletedEventArgs = CType(arg,System.Web.Services.Protocols.InvokeCompletedEventArgs)
                RaiseEvent GetCurrencyIdCompleted(Me, New GetCurrencyIdCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState))
            End If
        End Sub
        
        '''<remarks/>
        Public Shadows Sub CancelAsync(ByVal userState As Object)
            MyBase.CancelAsync(userState)
        End Sub
        
        Private Function IsLocalFileSystemWebService(ByVal url As String) As Boolean
            If ((url Is Nothing)  _
                        OrElse (url Is String.Empty)) Then
                Return false
            End If
            Dim wsUri As System.Uri = New System.Uri(url)
            If ((wsUri.Port >= 1024)  _
                        AndAlso (String.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) = 0)) Then
                Return true
            End If
            Return false
        End Function
    End Class
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.7.3056.0"),  _
     System.SerializableAttribute(),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code"),  _
     System.Xml.Serialization.XmlTypeAttribute([Namespace]:="http://tempuri.org/"),  _
     System.Xml.Serialization.XmlRootAttribute([Namespace]:="http://tempuri.org/", IsNullable:=false)>  _
    Partial Public Class SecuredWebServiceHeader
        Inherits System.Web.Services.Protocols.SoapHeader
        
        Private usernameField As String
        
        Private passwordField As String
        
        Private authenticatedTokenField As String
        
        Private accountIdField As Integer
        
        Private accountEmployeeIdField As Integer
        
        Private anyAttrField() As System.Xml.XmlAttribute
        
        '''<remarks/>
        Public Property Username() As String
            Get
                Return Me.usernameField
            End Get
            Set
                Me.usernameField = value
            End Set
        End Property
        
        '''<remarks/>
        Public Property Password() As String
            Get
                Return Me.passwordField
            End Get
            Set
                Me.passwordField = value
            End Set
        End Property
        
        '''<remarks/>
        Public Property AuthenticatedToken() As String
            Get
                Return Me.authenticatedTokenField
            End Get
            Set
                Me.authenticatedTokenField = value
            End Set
        End Property
        
        '''<remarks/>
        Public Property AccountId() As Integer
            Get
                Return Me.accountIdField
            End Get
            Set
                Me.accountIdField = value
            End Set
        End Property
        
        '''<remarks/>
        Public Property AccountEmployeeId() As Integer
            Get
                Return Me.accountEmployeeIdField
            End Get
            Set
                Me.accountEmployeeIdField = value
            End Set
        End Property
        
        '''<remarks/>
        <System.Xml.Serialization.XmlAnyAttributeAttribute()>  _
        Public Property AnyAttr() As System.Xml.XmlAttribute()
            Get
                Return Me.anyAttrField
            End Get
            Set
                Me.anyAttrField = value
            End Set
        End Property
    End Class
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.3056.0")>  _
    Public Delegate Sub AuthenticateUserCompletedEventHandler(ByVal sender As Object, ByVal e As AuthenticateUserCompletedEventArgs)
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.3056.0"),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code")>  _
    Partial Public Class AuthenticateUserCompletedEventArgs
        Inherits System.ComponentModel.AsyncCompletedEventArgs
        
        Private results() As Object
        
        Friend Sub New(ByVal results() As Object, ByVal exception As System.Exception, ByVal cancelled As Boolean, ByVal userState As Object)
            MyBase.New(exception, cancelled, userState)
            Me.results = results
        End Sub
        
        '''<remarks/>
        Public ReadOnly Property Result() As String
            Get
                Me.RaiseExceptionIfNecessary
                Return CType(Me.results(0),String)
            End Get
        End Property
    End Class
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.3056.0")>  _
    Public Delegate Sub AuthenticateMobileUserCompletedEventHandler(ByVal sender As Object, ByVal e As AuthenticateMobileUserCompletedEventArgs)
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.3056.0"),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code")>  _
    Partial Public Class AuthenticateMobileUserCompletedEventArgs
        Inherits System.ComponentModel.AsyncCompletedEventArgs
        
        Private results() As Object
        
        Friend Sub New(ByVal results() As Object, ByVal exception As System.Exception, ByVal cancelled As Boolean, ByVal userState As Object)
            MyBase.New(exception, cancelled, userState)
            Me.results = results
        End Sub
        
        '''<remarks/>
        Public ReadOnly Property Result() As String
            Get
                Me.RaiseExceptionIfNecessary
                Return CType(Me.results(0),String)
            End Get
        End Property
    End Class
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.3056.0")>  _
    Public Delegate Sub GetAccountIdForMobileCompletedEventHandler(ByVal sender As Object, ByVal e As GetAccountIdForMobileCompletedEventArgs)
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.3056.0"),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code")>  _
    Partial Public Class GetAccountIdForMobileCompletedEventArgs
        Inherits System.ComponentModel.AsyncCompletedEventArgs
        
        Private results() As Object
        
        Friend Sub New(ByVal results() As Object, ByVal exception As System.Exception, ByVal cancelled As Boolean, ByVal userState As Object)
            MyBase.New(exception, cancelled, userState)
            Me.results = results
        End Sub
        
        '''<remarks/>
        Public ReadOnly Property Result() As Integer
            Get
                Me.RaiseExceptionIfNecessary
                Return CType(Me.results(0),Integer)
            End Get
        End Property
    End Class
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.3056.0")>  _
    Public Delegate Sub GetAccountEmployeeIdForMobileCompletedEventHandler(ByVal sender As Object, ByVal e As GetAccountEmployeeIdForMobileCompletedEventArgs)
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.3056.0"),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code")>  _
    Partial Public Class GetAccountEmployeeIdForMobileCompletedEventArgs
        Inherits System.ComponentModel.AsyncCompletedEventArgs
        
        Private results() As Object
        
        Friend Sub New(ByVal results() As Object, ByVal exception As System.Exception, ByVal cancelled As Boolean, ByVal userState As Object)
            MyBase.New(exception, cancelled, userState)
            Me.results = results
        End Sub
        
        '''<remarks/>
        Public ReadOnly Property Result() As Integer
            Get
                Me.RaiseExceptionIfNecessary
                Return CType(Me.results(0),Integer)
            End Get
        End Property
    End Class
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.3056.0")>  _
    Public Delegate Sub GetAccountIdCompletedEventHandler(ByVal sender As Object, ByVal e As GetAccountIdCompletedEventArgs)
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.3056.0"),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code")>  _
    Partial Public Class GetAccountIdCompletedEventArgs
        Inherits System.ComponentModel.AsyncCompletedEventArgs
        
        Private results() As Object
        
        Friend Sub New(ByVal results() As Object, ByVal exception As System.Exception, ByVal cancelled As Boolean, ByVal userState As Object)
            MyBase.New(exception, cancelled, userState)
            Me.results = results
        End Sub
        
        '''<remarks/>
        Public ReadOnly Property Result() As Integer
            Get
                Me.RaiseExceptionIfNecessary
                Return CType(Me.results(0),Integer)
            End Get
        End Property
    End Class
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.3056.0")>  _
    Public Delegate Sub GetAccountEmployeeIdCompletedEventHandler(ByVal sender As Object, ByVal e As GetAccountEmployeeIdCompletedEventArgs)
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.3056.0"),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code")>  _
    Partial Public Class GetAccountEmployeeIdCompletedEventArgs
        Inherits System.ComponentModel.AsyncCompletedEventArgs
        
        Private results() As Object
        
        Friend Sub New(ByVal results() As Object, ByVal exception As System.Exception, ByVal cancelled As Boolean, ByVal userState As Object)
            MyBase.New(exception, cancelled, userState)
            Me.results = results
        End Sub
        
        '''<remarks/>
        Public ReadOnly Property Result() As Integer
            Get
                Me.RaiseExceptionIfNecessary
                Return CType(Me.results(0),Integer)
            End Get
        End Property
    End Class
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.3056.0")>  _
    Public Delegate Sub GetDepartmentIdCompletedEventHandler(ByVal sender As Object, ByVal e As GetDepartmentIdCompletedEventArgs)
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.3056.0"),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code")>  _
    Partial Public Class GetDepartmentIdCompletedEventArgs
        Inherits System.ComponentModel.AsyncCompletedEventArgs
        
        Private results() As Object
        
        Friend Sub New(ByVal results() As Object, ByVal exception As System.Exception, ByVal cancelled As Boolean, ByVal userState As Object)
            MyBase.New(exception, cancelled, userState)
            Me.results = results
        End Sub
        
        '''<remarks/>
        Public ReadOnly Property Result() As Integer
            Get
                Me.RaiseExceptionIfNecessary
                Return CType(Me.results(0),Integer)
            End Get
        End Property
    End Class
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.3056.0")>  _
    Public Delegate Sub GetLocationIdCompletedEventHandler(ByVal sender As Object, ByVal e As GetLocationIdCompletedEventArgs)
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.3056.0"),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code")>  _
    Partial Public Class GetLocationIdCompletedEventArgs
        Inherits System.ComponentModel.AsyncCompletedEventArgs
        
        Private results() As Object
        
        Friend Sub New(ByVal results() As Object, ByVal exception As System.Exception, ByVal cancelled As Boolean, ByVal userState As Object)
            MyBase.New(exception, cancelled, userState)
            Me.results = results
        End Sub
        
        '''<remarks/>
        Public ReadOnly Property Result() As Integer
            Get
                Me.RaiseExceptionIfNecessary
                Return CType(Me.results(0),Integer)
            End Get
        End Property
    End Class
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.3056.0")>  _
    Public Delegate Sub GetWorkTypeIdCompletedEventHandler(ByVal sender As Object, ByVal e As GetWorkTypeIdCompletedEventArgs)
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.3056.0"),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code")>  _
    Partial Public Class GetWorkTypeIdCompletedEventArgs
        Inherits System.ComponentModel.AsyncCompletedEventArgs
        
        Private results() As Object
        
        Friend Sub New(ByVal results() As Object, ByVal exception As System.Exception, ByVal cancelled As Boolean, ByVal userState As Object)
            MyBase.New(exception, cancelled, userState)
            Me.results = results
        End Sub
        
        '''<remarks/>
        Public ReadOnly Property Result() As Integer
            Get
                Me.RaiseExceptionIfNecessary
                Return CType(Me.results(0),Integer)
            End Get
        End Property
    End Class
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.3056.0")>  _
    Public Delegate Sub GetCurrencyIdCompletedEventHandler(ByVal sender As Object, ByVal e As GetCurrencyIdCompletedEventArgs)
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.3056.0"),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code")>  _
    Partial Public Class GetCurrencyIdCompletedEventArgs
        Inherits System.ComponentModel.AsyncCompletedEventArgs
        
        Private results() As Object
        
        Friend Sub New(ByVal results() As Object, ByVal exception As System.Exception, ByVal cancelled As Boolean, ByVal userState As Object)
            MyBase.New(exception, cancelled, userState)
            Me.results = results
        End Sub
        
        '''<remarks/>
        Public ReadOnly Property Result() As Integer
            Get
                Me.RaiseExceptionIfNecessary
                Return CType(Me.results(0),Integer)
            End Get
        End Property
    End Class
End Namespace
