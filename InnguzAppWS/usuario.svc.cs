using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace InnguzAppWS
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "usuario" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione usuario.svc o usuario.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class usuario : Iusuario
    {
        InnguzDBDataContext bd = new InnguzDBDataContext();
        public void DoWork()
        {
        }

        public LoginUser Login(LoginUser modelo)
        {
            LoginUser Login;

            try
            {
                var consulta = (from u in bd.Usuarios where u.Usuario == modelo.usuario && u.Clave == modelo.clave
                                select u).Single();
                Login = new LoginUser()
                         {
                             usuario = consulta.Usuario,
                             clave = consulta.Clave
                         };
                return Login;
            } catch
            {
                return null;
            }
        }
    }
}
