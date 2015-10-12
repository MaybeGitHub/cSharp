using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Libreria_Aggapea.App_Code.Modelos
{
    public class Cesta
    {
        public Usuario dueño { get; set; }

        public List<Libro> listaLibros { get; set; }

        public Cesta(Usuario dueño)
        {
            this.dueño = dueño;
        }

        public void añadirLibro(Libro libro)
        {
            listaLibros.Add(libro);
        }

        public string datos()
        {
            string ret = "";
            ret += dueño.nombre;
            foreach ( Libro l in listaLibros)
            {
                ret += ":" + l.titulo;
            }
            return ret;
        }

    }
}