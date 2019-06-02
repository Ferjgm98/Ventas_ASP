using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace InnguzAppWS
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "Iusuario" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface Iusuario
    {
        [OperationContract]
        void DoWork();

        [OperationContract]
        LoginUser Login(LoginUser modelo);

    }

    [DataContract]
    public class LoginUser
    {
        [DataMember]
        public int id_user { get; set; }
        [DataMember]
        public string usuario { get; set; }

        [DataMember]
        public string clave { get; set; }

        [DataMember]
        public string nombre { get; set; }

        [DataMember]
        public string apellido { get; set; }


    }
}
