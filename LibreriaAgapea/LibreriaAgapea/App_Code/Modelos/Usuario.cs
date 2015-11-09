using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LibreriaAgapea.App_Code.Herramientas;

namespace LibreriaAgapea.App_Code.Modelos
{
    public class Usuario
    {
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string contraseña { get; set; }
        public string direccion { get; set; }
        public string email { get; set; }
        public Cesta cesta { get; set; }
        private Ayudante ayudante = new Ayudante();

        public Usuario(string nombre, string apellidos, string contraseña, string direccion, string email)
        {
            this.nombre = nombre;
            this.apellido = apellidos;
            this.contraseña = contraseña;
            this.direccion = direccion;
            this.email = email;
            cesta = ayudante.fabricaCesta(this);
        }

        public Usuario(string[] datos)
        {
            for (int i = 0; i < datos.Length; i++)
            {
                switch (i)
                {
                    case 0:
                        nombre = datos[i];
                        break;
                    case 1:
                        apellido = datos[i];
                        break;
                    case 2:
                        contraseña = datos[i];
                        break;
                    case 3:
                        direccion = datos[i];
                        break;
                    case 4:
                        email = datos[i];
                        break;                    
                }

            }
            cesta = ayudante.fabricaCesta(this);
        }

        public string datos()
        {
            return nombre + ":" + apellido + ":" + contraseña + ":" + direccion + ":" + email;
        }
    }
}