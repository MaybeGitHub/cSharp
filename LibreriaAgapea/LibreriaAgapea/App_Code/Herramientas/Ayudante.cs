using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using LibreriaAgapea.App_Code.Controladores;
using LibreriaAgapea.App_Code.Modelos;
using System.IO;

namespace LibreriaAgapea.App_Code.Herramientas
{
    public class Ayudante
    {
        public Dictionary<string, Libro> mapeoBotones = new Dictionary<string, Libro>();

        public void pintarCajaInfoPagina(TextBox cajaMultilinea, HttpContext datos)
        {
            string message = "";
            List<string> valores = (from string key in datos.Request.Params where (key != null) select key).ToList();
            foreach (string key in datos.Request.Params) message += "clave: " + key + " ---- valor:_" + datos.Request[key] + "\n";
            cajaMultilinea.Text = message;
        }

        public Cesta conseguirLibros(Usuario usuario)
        {
            string cestaActivaUsuario = File.ReadAllLines(CFichero.rutaCestas).Where(linea => linea.Split(':')[0] == usuario.nombre && linea.Split(':')[1] == "true").SingleOrDefault();
            for (int i = 2; i < cestaActivaUsuario.Split(':').Count(); i++)
            {
                if (!usuario.cesta.cerrada)
                {
                    usuario.cesta.listaLibros.Add(fabricaLibros(cestaActivaUsuario.Split(':')[i], false));
                }
            }
            return usuario.cesta;
        }

        public Libro fabricaLibros(string ISBN, bool ISBN13)
        {
            int posicion = 3;
            if (ISBN13) posicion = 4;
            return File.ReadAllLines(CFichero.rutaLibros).Where(libro => libro.Split(':')[posicion] == ISBN).Select(libro => new Libro(libro.Split(':'))).SingleOrDefault();
        }

        public int librosRepetidos(Libro libro, List<Libro> listaLibros)
        {
            int cont = 0;
            foreach ( Libro libroLeido in listaLibros)
            {
                if (libroLeido.titulo == libro.titulo)
                {
                    cont++;
                }
            }
            return cont;
        }
    }
}