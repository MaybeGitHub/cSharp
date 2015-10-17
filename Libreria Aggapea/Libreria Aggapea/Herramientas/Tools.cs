using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using Libreria_Aggapea.App_Code.Controladores;
using Libreria_Aggapea.App_Code.Modelos;

namespace Libreria_Aggapea.Herramientas
{
    public class Tools
    {
        private Ctrl_Ficheros ctrl_F = new Ctrl_Ficheros();

        public void pintarCajaInfoPagina( TextBox cajaMultilinea, HttpContext datos )
        {
            string message = "", valor = "";
            foreach (string key in datos.Request.Params)
            {
                if (datos.Request[key] == null) { valor = "null"; } else { valor = datos.Request[key].ToString(); };
                message += "clave: " + key + " ---- valor:_" + valor + "\n";
            }
            cajaMultilinea.Text = message;            
        }

        public void rellenarList( ArrayList lista, string conQue )
        {
            string[] datosLista = ctrl_F.leer(conQue);
            string nombreUsuario;
            foreach ( string linea in datosLista)
            {
                if (conQue == "libros")
                {
                    lista.Add(new Libro(linea.Split(':')));
                }else if (conQue == "usuarios")
                {
                    lista.Add(new Usuario(linea.Split(':')));
                }else if (conQue == "cestas")
                {
                    nombreUsuario = linea.Split(':')[0];
                    ArrayList soloLibros = new ArrayList();
                    soloLibros.AddRange(linea.Split(':'));
                    soloLibros.RemoveAt(0);
                    lista.Add(new Cesta(fabricaUsuarios(nombreUsuario), fabricaLibros(soloLibros)));
                }
            }                
        }

        public Usuario fabricaUsuarios(string nombre)
        {
            Usuario ret = null;
            foreach (string usuario in ctrl_F.leer("usuarios"))
            {
                if ( usuario.Split(':')[0] == nombre)
                {
                    ret = new Usuario(usuario.Split(':'));
                }
            }
            return ret;
        }

        public Libro fabricaLibros(string titulo)
        {
            Libro ret = null;
            foreach (string libro in ctrl_F.leer("libros"))
            {
                if (libro.Split(':')[0] == titulo)
                {
                    ret = new Libro(libro.Split(':'));
                }
            }
            return ret;
        }

        public ArrayList fabricaLibros(ArrayList libros)
        {
            ArrayList ret = new ArrayList();
            foreach(string titulo in libros )
            {
                ret.Add(fabricaLibros(titulo));
            }
            return ret;
        }
    }
}
