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
            switch (tipoBusqueda)
            {
                case "titulo":
                    return listaLibros.Where(libro => libro.titulo.StartsWith(patron)).ToList();
                case "autor":
                    return listaLibros.Where(libro => libro.autor.StartsWith(patron)).ToList();
                case "categoria":
                    return listaLibros.Where(libro => libro.categoria.StartsWith(patron)).ToList();
                case "editorial":
                    return listaLibros.Where(libro => libro.editorial.StartsWith(patron)).ToList();
                case "isbn10":
                    return listaLibros.Where(libro => libro.ISBN10.StartsWith(patron)).ToList();
                case "isbn13":
                    return listaLibros.Where(libro => libro.ISBN13.StartsWith(patron)).ToList();
                default:
                    return null;
            }
        }
    }
}