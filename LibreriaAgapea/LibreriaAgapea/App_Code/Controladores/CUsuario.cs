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
        public Ayudante ayudante = new Ayudante();

        public CUsuario()
        {
            listaUsuarios.AddRange(File.ReadAllLines(CFichero.rutaUsuarios).Select(linea => new Usuario(linea.Split(':'))));
            foreach (Usuario usuario in listaUsuarios)
            {
                usuario.cesta = ayudante.conseguirLibros(usuario);
            }
        }

        public void meterEnCesta(Usuario usuario, Libro libro)
        {
            if ( usuario.meterLibroEnCesta(libro) ){
                CFichero.sobrescribirTxt(CFichero.rutaCestas, usuario.nombre, libro.ISBN10, true);
            }
        }

        public void sacarDeCesta(Usuario usuario, Libro libro)
        {
            if ( usuario.sacarLibroDeCesta(libro) )
            {
                CFichero.sobrescribirTxt(CFichero.rutaCestas, usuario.nombre, libro.ISBN10, false);
            }
        }
    }
}