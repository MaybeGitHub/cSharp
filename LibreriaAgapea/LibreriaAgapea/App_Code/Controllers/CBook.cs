using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using LibreriaAgapea.App_Code.Models;
using LibreriaAgapea.App_Code.Tools;

namespace LibreriaAgapea.App_Code.Controllers
{
    public class CBook
    {
        private Tool tools = new Tool();
        private CFile cF = new CFile();
        public List<Book> libros = new List<Book>();

        public CBook()
        {
            libros.AddRange(File.ReadAllLines(cF.rutaLibros).Select(linea => new Book(linea.Split(':'))));
        }

        public List<Book> buscarLibros(string busqueda, string tipo)
        {
            if (tipo == "titulo") return libros.Where(libro => libro.titulo.StartsWith(busqueda)).ToList();
            if (tipo == "autor") return libros.Where(libro => libro.autor.StartsWith(busqueda)).ToList();
            if (tipo == "categoria") return libros.Where(libro => libro.categoria.StartsWith(busqueda)).ToList();
            if (tipo == "editorial") return libros.Where(libro => libro.editorial.StartsWith(busqueda)).ToList();
            if (tipo == "isbn") return libros.Where(libro => libro.ISBN10.StartsWith(busqueda) || libro.ISBN13.StartsWith(busqueda) ).ToList();
            return null;
        }
    }
}