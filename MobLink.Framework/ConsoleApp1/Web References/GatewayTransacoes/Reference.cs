﻿//------------------------------------------------------------------------------
// <auto-generated>
//     O código foi gerado por uma ferramenta.
//     Versão de Tempo de Execução:4.0.30319.42000
//
//     As alterações ao arquivo poderão causar comportamento incorreto e serão perdidas se
//     o código for gerado novamente.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// Este código-fonte foi gerado automaticamente por Microsoft.VSDesigner, Versão 4.0.30319.42000.
// 
#pragma warning disable 1591

namespace ConsoleApp1.GatewayTransacoes {
    using System;
    using System.Web.Services;
    using System.Diagnostics;
    using System.Web.Services.Protocols;
    using System.Xml.Serialization;
    using System.ComponentModel;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2046.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="GatewayTransacoesSoap", Namespace="http://linkpatios/")]
    public partial class GatewayTransacoes : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback ConsultaVeiculoOperationCompleted;
        
        private System.Threading.SendOrPostCallback RegistraApreensaoOperationCompleted;
        
        private System.Threading.SendOrPostCallback ConsultaLiberacaoOperationCompleted;
        
        private System.Threading.SendOrPostCallback ConsultaApreensaoOperationCompleted;
        
        private System.Threading.SendOrPostCallback RegistraSaidaPatioOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public GatewayTransacoes() {
            this.Url = global::ConsoleApp1.Properties.Settings.Default.ConsoleApp1_GatewayTransacoes_MobLinkGatewayTransacoes;
            if ((this.IsLocalFileSystemWebService(this.Url) == true)) {
                this.UseDefaultCredentials = true;
                this.useDefaultCredentialsSetExplicitly = false;
            }
            else {
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        public new string Url {
            get {
                return base.Url;
            }
            set {
                if ((((this.IsLocalFileSystemWebService(base.Url) == true) 
                            && (this.useDefaultCredentialsSetExplicitly == false)) 
                            && (this.IsLocalFileSystemWebService(value) == false))) {
                    base.UseDefaultCredentials = false;
                }
                base.Url = value;
            }
        }
        
        public new bool UseDefaultCredentials {
            get {
                return base.UseDefaultCredentials;
            }
            set {
                base.UseDefaultCredentials = value;
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        /// <remarks/>
        public event ConsultaVeiculoCompletedEventHandler ConsultaVeiculoCompleted;
        
        /// <remarks/>
        public event RegistraApreensaoCompletedEventHandler RegistraApreensaoCompleted;
        
        /// <remarks/>
        public event ConsultaLiberacaoCompletedEventHandler ConsultaLiberacaoCompleted;
        
        /// <remarks/>
        public event ConsultaApreensaoCompletedEventHandler ConsultaApreensaoCompleted;
        
        /// <remarks/>
        public event RegistraSaidaPatioCompletedEventHandler RegistraSaidaPatioCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://linkpatios/ConsultaVeiculo", RequestNamespace="http://linkpatios/", ResponseNamespace="http://linkpatios/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string ConsultaVeiculo(int id_grv) {
            object[] results = this.Invoke("ConsultaVeiculo", new object[] {
                        id_grv});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void ConsultaVeiculoAsync(int id_grv) {
            this.ConsultaVeiculoAsync(id_grv, null);
        }
        
        /// <remarks/>
        public void ConsultaVeiculoAsync(int id_grv, object userState) {
            if ((this.ConsultaVeiculoOperationCompleted == null)) {
                this.ConsultaVeiculoOperationCompleted = new System.Threading.SendOrPostCallback(this.OnConsultaVeiculoOperationCompleted);
            }
            this.InvokeAsync("ConsultaVeiculo", new object[] {
                        id_grv}, this.ConsultaVeiculoOperationCompleted, userState);
        }
        
        private void OnConsultaVeiculoOperationCompleted(object arg) {
            if ((this.ConsultaVeiculoCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.ConsultaVeiculoCompleted(this, new ConsultaVeiculoCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://linkpatios/RegistraApreensao", RequestNamespace="http://linkpatios/", ResponseNamespace="http://linkpatios/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string RegistraApreensao(int id_grv) {
            object[] results = this.Invoke("RegistraApreensao", new object[] {
                        id_grv});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void RegistraApreensaoAsync(int id_grv) {
            this.RegistraApreensaoAsync(id_grv, null);
        }
        
        /// <remarks/>
        public void RegistraApreensaoAsync(int id_grv, object userState) {
            if ((this.RegistraApreensaoOperationCompleted == null)) {
                this.RegistraApreensaoOperationCompleted = new System.Threading.SendOrPostCallback(this.OnRegistraApreensaoOperationCompleted);
            }
            this.InvokeAsync("RegistraApreensao", new object[] {
                        id_grv}, this.RegistraApreensaoOperationCompleted, userState);
        }
        
        private void OnRegistraApreensaoOperationCompleted(object arg) {
            if ((this.RegistraApreensaoCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.RegistraApreensaoCompleted(this, new RegistraApreensaoCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://linkpatios/ConsultaLiberacao", RequestNamespace="http://linkpatios/", ResponseNamespace="http://linkpatios/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string ConsultaLiberacao(int id_grv) {
            object[] results = this.Invoke("ConsultaLiberacao", new object[] {
                        id_grv});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void ConsultaLiberacaoAsync(int id_grv) {
            this.ConsultaLiberacaoAsync(id_grv, null);
        }
        
        /// <remarks/>
        public void ConsultaLiberacaoAsync(int id_grv, object userState) {
            if ((this.ConsultaLiberacaoOperationCompleted == null)) {
                this.ConsultaLiberacaoOperationCompleted = new System.Threading.SendOrPostCallback(this.OnConsultaLiberacaoOperationCompleted);
            }
            this.InvokeAsync("ConsultaLiberacao", new object[] {
                        id_grv}, this.ConsultaLiberacaoOperationCompleted, userState);
        }
        
        private void OnConsultaLiberacaoOperationCompleted(object arg) {
            if ((this.ConsultaLiberacaoCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.ConsultaLiberacaoCompleted(this, new ConsultaLiberacaoCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://linkpatios/ConsultaApreensao", RequestNamespace="http://linkpatios/", ResponseNamespace="http://linkpatios/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string ConsultaApreensao(int id_grv) {
            object[] results = this.Invoke("ConsultaApreensao", new object[] {
                        id_grv});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void ConsultaApreensaoAsync(int id_grv) {
            this.ConsultaApreensaoAsync(id_grv, null);
        }
        
        /// <remarks/>
        public void ConsultaApreensaoAsync(int id_grv, object userState) {
            if ((this.ConsultaApreensaoOperationCompleted == null)) {
                this.ConsultaApreensaoOperationCompleted = new System.Threading.SendOrPostCallback(this.OnConsultaApreensaoOperationCompleted);
            }
            this.InvokeAsync("ConsultaApreensao", new object[] {
                        id_grv}, this.ConsultaApreensaoOperationCompleted, userState);
        }
        
        private void OnConsultaApreensaoOperationCompleted(object arg) {
            if ((this.ConsultaApreensaoCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.ConsultaApreensaoCompleted(this, new ConsultaApreensaoCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://linkpatios/RegistraSaidaPatio", RequestNamespace="http://linkpatios/", ResponseNamespace="http://linkpatios/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string RegistraSaidaPatio(int id_grv) {
            object[] results = this.Invoke("RegistraSaidaPatio", new object[] {
                        id_grv});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void RegistraSaidaPatioAsync(int id_grv) {
            this.RegistraSaidaPatioAsync(id_grv, null);
        }
        
        /// <remarks/>
        public void RegistraSaidaPatioAsync(int id_grv, object userState) {
            if ((this.RegistraSaidaPatioOperationCompleted == null)) {
                this.RegistraSaidaPatioOperationCompleted = new System.Threading.SendOrPostCallback(this.OnRegistraSaidaPatioOperationCompleted);
            }
            this.InvokeAsync("RegistraSaidaPatio", new object[] {
                        id_grv}, this.RegistraSaidaPatioOperationCompleted, userState);
        }
        
        private void OnRegistraSaidaPatioOperationCompleted(object arg) {
            if ((this.RegistraSaidaPatioCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.RegistraSaidaPatioCompleted(this, new RegistraSaidaPatioCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        public new void CancelAsync(object userState) {
            base.CancelAsync(userState);
        }
        
        private bool IsLocalFileSystemWebService(string url) {
            if (((url == null) 
                        || (url == string.Empty))) {
                return false;
            }
            System.Uri wsUri = new System.Uri(url);
            if (((wsUri.Port >= 1024) 
                        && (string.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) == 0))) {
                return true;
            }
            return false;
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2046.0")]
    public delegate void ConsultaVeiculoCompletedEventHandler(object sender, ConsultaVeiculoCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2046.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class ConsultaVeiculoCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal ConsultaVeiculoCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2046.0")]
    public delegate void RegistraApreensaoCompletedEventHandler(object sender, RegistraApreensaoCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2046.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class RegistraApreensaoCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal RegistraApreensaoCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2046.0")]
    public delegate void ConsultaLiberacaoCompletedEventHandler(object sender, ConsultaLiberacaoCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2046.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class ConsultaLiberacaoCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal ConsultaLiberacaoCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2046.0")]
    public delegate void ConsultaApreensaoCompletedEventHandler(object sender, ConsultaApreensaoCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2046.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class ConsultaApreensaoCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal ConsultaApreensaoCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2046.0")]
    public delegate void RegistraSaidaPatioCompletedEventHandler(object sender, RegistraSaidaPatioCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.7.2046.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class RegistraSaidaPatioCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal RegistraSaidaPatioCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
}

#pragma warning restore 1591