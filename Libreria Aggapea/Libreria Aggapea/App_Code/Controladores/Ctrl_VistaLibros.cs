using Libreria_Aggapea.App_Code.Modelos;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI.WebControls;
using Libreria_Aggapea.Herramientas;
using System.Linq;
using System.IO;

namespace Libreria_Aggapea.App_Code.Controladores
{
    public class Ctrl_VistaLibros
    {
        private Tools tools = new Tools();
        private Ctrl_Ficheros ctrl_F = new Ctrl_Ficheros();
        public List<Libro> libros = new List<Libro>();

        public Ctrl_VistaLibros()
        {
            libros.AddRange(File.ReadAllLines(ctrl_F.rutaLibros).Select(linea => new Libro(linea.Split(':'))));
        }
        
        public List<Libro> buscarLibros(string busqueda, string tipo)
        {
            if (tipo == "titulo") return libros.Where(libro => libro.titulo.StartsWith(busqueda)).ToList();
            if (tipo == "autor") return libros.Where(libro => libro.autor.StartsWith(busqueda)).ToList();
            return null;
         }

        public List<Libro> leerLibros(string categoria)
        {
            List<Libro> ret = new List<Libro>();
            ret.AddRange(libros.Where(libro => libro.categoria == categoria));
            return ret;
        }
    }
}
