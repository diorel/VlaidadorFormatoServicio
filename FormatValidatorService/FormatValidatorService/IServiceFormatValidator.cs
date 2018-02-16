using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace FormatValidatorService
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IService1" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface IServiceFormatValidator
    {
        //[OperationContract]
        //string VerificarIfe(string Foto);


        [OperationContract]
        Task<string> ValidarFormato(string imageFilePath);


        

    }
 
}
