using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Libreria_Aggapea.App_Code.Modelos
{
    public class Cesta
    {
        public Usuario user { get; set; }

        public List<Libro> listaLibros { get; set; }

        public Cesta(Usuario dueño)
        {
            user = dueño;
        }

        public void añadirLibro(Libro libro)
        {
            listaLibros.Add(libro);
        }

        public string datos()
        {
            string ret = "";
            ret += user.usuario;
            foreach ( Libro l in listaLibros)
            {
                ret += ":" + l.nombre;
            }
            return ret;
        }

    }
}