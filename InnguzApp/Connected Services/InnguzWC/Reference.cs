﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace InnguzApp.InnguzWC {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="LoginUser", Namespace="http://schemas.datacontract.org/2004/07/InnguzAppWS")]
    [System.SerializableAttribute()]
    public partial class LoginUser : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string claveField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string usuarioField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string clave {
            get {
                return this.claveField;
            }
            set {
                if ((object.ReferenceEquals(this.claveField, value) != true)) {
                    this.claveField = value;
                    this.RaisePropertyChanged("clave");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string usuario {
            get {
                return this.usuarioField;
            }
            set {
                if ((object.ReferenceEquals(this.usuarioField, value) != true)) {
                    this.usuarioField = value;
                    this.RaisePropertyChanged("usuario");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="InnguzWC.Iusuario")]
    public interface Iusuario {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/Iusuario/DoWork", ReplyAction="http://tempuri.org/Iusuario/DoWorkResponse")]
        void DoWork();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/Iusuario/DoWork", ReplyAction="http://tempuri.org/Iusuario/DoWorkResponse")]
        System.Threading.Tasks.Task DoWorkAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/Iusuario/Login", ReplyAction="http://tempuri.org/Iusuario/LoginResponse")]
        InnguzApp.InnguzWC.LoginUser Login(InnguzApp.InnguzWC.LoginUser modelo);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/Iusuario/Login", ReplyAction="http://tempuri.org/Iusuario/LoginResponse")]
        System.Threading.Tasks.Task<InnguzApp.InnguzWC.LoginUser> LoginAsync(InnguzApp.InnguzWC.LoginUser modelo);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IusuarioChannel : InnguzApp.InnguzWC.Iusuario, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class IusuarioClient : System.ServiceModel.ClientBase<InnguzApp.InnguzWC.Iusuario>, InnguzApp.InnguzWC.Iusuario {
        
        public IusuarioClient() {
        }
        
        public IusuarioClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public IusuarioClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public IusuarioClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public IusuarioClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public void DoWork() {
            base.Channel.DoWork();
        }
        
        public System.Threading.Tasks.Task DoWorkAsync() {
            return base.Channel.DoWorkAsync();
        }
        
        public InnguzApp.InnguzWC.LoginUser Login(InnguzApp.InnguzWC.LoginUser modelo) {
            return base.Channel.Login(modelo);
        }
        
        public System.Threading.Tasks.Task<InnguzApp.InnguzWC.LoginUser> LoginAsync(InnguzApp.InnguzWC.LoginUser modelo) {
            return base.Channel.LoginAsync(modelo);
        }
    }
}