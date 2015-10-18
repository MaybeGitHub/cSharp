using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Libreria_Aggapea.Herramientas;
using Libreria_Aggapea.App_Code.Controladores;
using System.IO;

namespace Libreria_Aggapea.App_Code.Modelos
{
    public class Cesta
    {
        private Tools tools = new Tools();
        private Ctrl_Ficheros ctrl_F = new Ctrl_Ficheros();

        public Usuario dueño;
        public List<Libro> listaLibros = new List<Libro>();       

        public Cesta(Usuario dueño)
        {
            this.dueño = dueño;
            listaLibros = new List<Libro>();
        }

        public Cesta(Usuario usuario, List<Libro> libros)
        {
            dueño = usuario;
            listaLibros = libros;            
        }

        public Cesta(string[] datos)
        {
            string[] datosUsuarios = File.ReadAllLines(ctrl_F.rutaUsuarios);
            string[] datosLibros = File.ReadAllLines(ctrl_F.rutaLibros);

            dueño = datosUsuarios.Where(linea => linea.Split(':')[0] == datos[0]).Select(linea => new Usuario(linea.Split(':'))).ElementAt(0);

            for ( int i = 1; i < datos.Length; i++)
                listaLibros.Add(datosLibros.Where(linea => linea.Split(':')[0] == datos[i]).Select(linea => new Libro(linea.Split(':'))).ElementAt(0));
        }

        public void añadirLibro(Libro libro)
        {
            listaLibros.Add(libro);
        }

        public string datos()
        {
            string ret = "";
            foreach(Libro libro in listaLibros ) ret += ":" + libro.titulo;
            return dueño.nombre + ret;
        }

    }
}