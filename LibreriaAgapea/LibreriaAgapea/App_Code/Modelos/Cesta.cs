using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LibreriaAgapea.App_Code.Herramientas;
using LibreriaAgapea.App_Code.Modelos;
using LibreriaAgapea.App_Code.Controladores;
using System.IO;

namespace LibreriaAgapea.App_Code.Modelos
{
    public class Cesta
    {     
        public List<Libro> listaLibros { get; set; }
        public DateTime fechaCreacion = DateTime.Now;
        public bool cerrada
        {
            get
            {
                if (DateTime.Now.AddHours(1).CompareTo(fechaCreacion) < 1) { return true; } else return false;
            }
            set
            {
                cerrada = value;
            }
        }

        public Cesta()
        {
            listaLibros = new List<Libro>();
        }

        public string datos()
        {
            string ret = "";
            foreach (Libro libro in listaLibros) ret += libro.titulo + (listaLibros.IndexOf(libro) == listaLibros.Count-1? "":":") ;
            return ret;
        }
    }
}