using System;
using System.Collections;
using System.Linq;
using System.Web;

namespace Libreria_Aggapea.App_Code.Modelos
{
    public class Cesta
    {
        public Usuario dueño { get; set; }

        public ArrayList listaLibros { get; set; }

        public Cesta(Usuario dueño)
        {
            this.dueño = dueño;
            listaLibros = new ArrayList();
        }

        public Cesta(Usuario usuario, ArrayList libros)
        {
            dueño = usuario;
            listaLibros = libros;            
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