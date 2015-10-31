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
        public void pintarCajaInfoPagina(TextBox cajaMultilinea, HttpContext datos)
        {
            string message = "";
            List<string> valores = (from string key in datos.Request.Params where (key != null) select key).ToList();
            foreach (string key in datos.Request.Params) message += "clave: " + key + " ---- valor:_" + datos.Request[key] + "\n";
            cajaMultilinea.Text = message;
        }

        public Cesta fabricaCesta(Usuario usuario)
        {
            Cesta cesta = new Cesta(usuario);
            string datosCestaUsuario = File.ReadAllLines(CFichero.rutaCestas).Where(linea => linea.Split(':')[0] == usuario.nombre).SingleOrDefault();
            if ( datosCestaUsuario == null )
            {
                StreamWriter sw = new StreamWriter(new FileStream(CFichero.rutaCestas, FileMode.Append, FileAccess.ReadWrite ));
                sw.WriteLine(usuario.datos());
                
            }
            else {
                for (int i = 1; i < datosCestaUsuario.Split(':').Count(); i++)
                {                   
                    cesta.listaLibros.Add(fabricaLibros(datosCestaUsuario.Split(':')[i], false));
                }
            }
            return cesta;           
        }

        public Libro fabricaLibros(string ISBN, bool ISBN13)
        {
            int posicion = 3;
            if (ISBN13) posicion = 4;
            return File.ReadAllLines(CFichero.rutaLibros).Where(libro => libro.Split(':')[posicion] == ISBN).Select(libro => new Libro(libro.Split(':'))).SingleOrDefault();
        }

        public Usuario fabricaUsuario(string nombre)
        {         
            return File.ReadAllLines(CFichero.rutaUsuarios).Where(usuario => usuario.Split(':')[0] == nombre).Select(libro => new Usuario(libro.Split(':'))).SingleOrDefault();
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

        public string capitalizar(string frase)
        {
            return char.ToUpper(frase[0]) + frase.Substring(1);
        }
    }
}