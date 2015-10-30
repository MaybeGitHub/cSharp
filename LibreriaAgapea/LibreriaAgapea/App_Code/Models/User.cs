using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibreriaAgapea.App_Code.Models
{
    public class User
    {
        public string nombre { get; set; }
        public string contraseña { get; set; }

        public User(string usuario, string contraseña)
        {
            this.nombre = usuario;
            this.contraseña = contraseña;
        }

        public User(string[] datos)
        {
            for (int i = 0; i < datos.Length; i++)
            {
                switch (i)
                {
                    case 0: nombre = datos[i]; break;
                    case 1: contraseña = datos[i]; break;
                }

            }
        }

        public string datos()
        {
            return nombre + ":" + contraseña + "\n";
        }
    }
}