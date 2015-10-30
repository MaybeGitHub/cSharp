using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LibreriaAgapea.App_Code.Tools;
using LibreriaAgapea.App_Code.Models;
using LibreriaAgapea.App_Code.Controllers;
using System.IO;

namespace LibreriaAgapea.App_Code.Models
{
    public class Cart
    {
        private Tool tools = new Tool();
        private CFile cF = new CFile();

        public User dueño;
        public List<Book> listaLibros = new List<Book>();

        public Cart(User dueño)
        {
            this.dueño = dueño;
            listaLibros = new List<Book>();
        }

        public Cart(User usuario, List<Book> libros)
        {
            dueño = usuario;
            listaLibros = libros;
        }

        public Cart(string[] datos)
        {
            string[] datosUsuarios = File.ReadAllLines(cF.rutaUsuarios);
            string[] datosLibros = File.ReadAllLines(cF.rutaLibros);

            dueño = datosUsuarios.Where(linea => linea.Split(':')[0] == datos[0]).Select(linea => new User(linea.Split(':'))).ElementAt(0);

            for (int i = 1; i < datos.Length; i++)
                listaLibros.Add(datosLibros.Where(linea => linea.Split(':')[0] == datos[i]).Select(linea => new Book(linea.Split(':'))).ElementAt(0));
        }

        public void añadirLibro(Book libro)
        {
            listaLibros.Add(libro);
        }

        public string datos()
        {
            string ret = "";
            foreach (Book libro in listaLibros) ret += ":" + libro.titulo;
            return dueño.nombre + ret;
        }
    }
}