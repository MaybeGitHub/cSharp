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

        public Usuario(string[] datos)
        {
            for ( int i = 0; i < datos.Length; i++ )
            {
                switch (i)
                {
                    case 0: usuario = datos[i]; break;
                    case 1: contraseña = datos[i]; break;
                }

            }
        }

        public string datos()
        {
            return usuario + ":" + contraseña + "\n";
        }
    }   
}