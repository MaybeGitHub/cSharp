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
        public string contraseña { get; set; }
        public Cesta cesta { get; set; }
        private Ayudante ayudante = new Ayudante();

        public Usuario(string usuario, string contraseña)
        {
            this.nombre = usuario;
            this.contraseña = contraseña;
            cesta = ayudante.fabricaCesta(this);
        }

        public Usuario(string[] datos)
        {
            for (int i = 0; i < datos.Length; i++)
            {
                switch (i)
                {
                    case 0: nombre = datos[i]; break;
                    case 1: contraseña = datos[i]; break;
                }

            }
            cesta = ayudante.fabricaCesta(this);
        }

        public string datos()
        {
            return nombre + ":" + contraseña;
        }
    }
}