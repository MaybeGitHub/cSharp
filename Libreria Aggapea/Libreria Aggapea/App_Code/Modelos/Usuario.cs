using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Libreria_Aggapea.App_Code.Modelos
{
    public class Usuario
    {
        public string usuario { get; set; }
        public string contraseña { get; set; }

        public Usuario(string usuario, string contraseña)
        {
            this.usuario = usuario;
            this.contraseña = contraseña;
        }

        public string datos()
        {
            return usuario + ":" + contraseña + "\n";
        }
    }   
}