using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using LibreriaAgapea.App_Code.Modelos;
using LibreriaAgapea.App_Code.Herramientas;

namespace LibreriaAgapea.App_Code.Controladores
{
    public class CLibro
    {
        public List<Libro> listaLibros = new List<Libro>();

        public CLibro()
        { 
            listaLibros.AddRange(File.ReadAllLines(CFichero.rutaLibros).Select(linea => new Libro(linea.Split(':'))));
        }

        public List<Libro> buscarLibros(string patron, string tipoBusqueda)
        {
            if (tipoBusqueda == "titulo") return listaLibros.Where(libro => libro.titulo.StartsWith(patron)).ToList();
            if (tipoBusqueda == "autor") return listaLibros.Where(libro => libro.autor.StartsWith(patron)).ToList();
            if (tipoBusqueda == "categoria") return listaLibros.Where(libro => libro.categoria.StartsWith(patron)).ToList();
            if (tipoBusqueda == "editorial") return listaLibros.Where(libro => libro.editorial.StartsWith(patron)).ToList();
            if (tipoBusqueda == "isbn") return listaLibros.Where(libro => libro.ISBN10.StartsWith(patron) || libro.ISBN13.StartsWith(patron) ).ToList();
            return null;
        }
    }
}