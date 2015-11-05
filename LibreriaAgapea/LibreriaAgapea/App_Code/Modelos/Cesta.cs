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
        public Usuario usuario { get; set; }
        private Ayudante ayudante = new Ayudante();      

        public Cesta(Usuario usuario)
        {
            this.usuario = usuario;
            listaLibros = new List<Libro>();
        }

        public Cesta(string[] datosFichero)
        {
            usuario = ayudante.fabricaUsuario(datosFichero[0]);
            for(int i = 1; i < datosFichero.Count(); i++)
            {
                listaLibros.Add(ayudante.fabricaLibros(datosFichero[i], false));
            }
        }

        public string datos()
        {
            string ret = "";
            foreach (Libro libro in listaLibros) ret += libro.ISBN10 + (listaLibros.IndexOf(libro) < listaLibros.Count-1? ":":"") ;
            return ret;
        }
    }
}