using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LibreriaAgapea.App_Code.Modelos
{
    public class Usuario
    {
        public string nombre { get; set; }
        public string contraseña { get; set; }
        private Cesta __cesta;
        public Cesta cesta {
            get
            {
                if (__cesta.cerrada)
                {
                    historialCestas.Add(DateTime.Now, cesta);
                    __cesta = new Cesta();
                }
                return __cesta;
            }
              set
            {
                __cesta = value;
            }
        }

        public Dictionary<DateTime, Cesta> historialCestas = new Dictionary<DateTime, Cesta>();

        public Usuario(string usuario, string contraseña)
        {
            this.nombre = usuario;
            this.contraseña = contraseña;
            cesta = new Cesta();
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
            cesta = new Cesta();
        }

        public string datos()
        {
            return nombre + ":" + contraseña;
        }

        public bool sacarLibroDeCesta(Libro libro)
        {
            if (!cesta.cerrada)
            {
                cesta.listaLibros.Remove(libro);
                return true;
            }
            return false;
        }

        public bool meterLibroEnCesta(Libro libro)
        {
            if (!cesta.cerrada)
            {
                cesta.listaLibros.Add(libro);
                return true;
            }
            return false;
        }
    }
}