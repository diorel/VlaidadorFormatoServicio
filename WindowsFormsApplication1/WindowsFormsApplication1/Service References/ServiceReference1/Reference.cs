﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WindowsFormsApplication1.ServiceReference1 {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceReference1.IServiceFormatValidator")]
    public interface IServiceFormatValidator {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceFormatValidator/ValidarFormatoINE", ReplyAction="http://tempuri.org/IServiceFormatValidator/ValidarFormatoINEResponse")]
        string ValidarFormatoINE(byte[] ByteArray);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceFormatValidator/ValidarFormatoINE", ReplyAction="http://tempuri.org/IServiceFormatValidator/ValidarFormatoINEResponse")]
        System.Threading.Tasks.Task<string> ValidarFormatoINEAsync(byte[] ByteArray);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IServiceFormatValidatorChannel : WindowsFormsApplication1.ServiceReference1.IServiceFormatValidator, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ServiceFormatValidatorClient : System.ServiceModel.ClientBase<WindowsFormsApplication1.ServiceReference1.IServiceFormatValidator>, WindowsFormsApplication1.ServiceReference1.IServiceFormatValidator {
        
        public ServiceFormatValidatorClient() {
        }
        
        public ServiceFormatValidatorClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ServiceFormatValidatorClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ServiceFormatValidatorClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ServiceFormatValidatorClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string ValidarFormatoINE(byte[] ByteArray) {
            return base.Channel.ValidarFormatoINE(ByteArray);
        }
        
        public System.Threading.Tasks.Task<string> ValidarFormatoINEAsync(byte[] ByteArray) {
            return base.Channel.ValidarFormatoINEAsync(ByteArray);
        }
    }
}
