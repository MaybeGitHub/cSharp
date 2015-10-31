using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using LibreriaAgapea.App_Code.Modelos;
using LibreriaAgapea.App_Code.Herramientas;

namespace LibreriaAgapea.App_Code.Controladores
{    
    public class CUsuario
    {
        public List<Usuario> listaUsuarios = new List<Usuario>();

        public CUsuario()
        {
            listaUsuarios.AddRange(File.ReadAllLines(CFichero.rutaUsuarios).Select(linea => new Usuario(linea.Split(':'))));
        }

        public void meterEnCesta(Usuario usuario, Libro libro)
        {
            usuario.cesta.listaLibros.Add(libro);
            CFichero.sobrescribirTxt(CFichero.rutaCestas, usuario.nombre, libro.ISBN10, true);            
        }

        public void sacarDeCesta(Usuario usuario, Libro libro)
        {
            foreach(Libro libroUsuario in usuario.cesta.listaLibros)
            {
                if ( libroUsuario.ISBN10 == libro.ISBN10)
                {
                    usuario.cesta.listaLibros.Remove(libroUsuario);
                    break;
                }
            }
            CFichero.sobrescribirTxt(CFichero.rutaCestas, usuario.nombre, libro.ISBN10, false);
        }
    }
}