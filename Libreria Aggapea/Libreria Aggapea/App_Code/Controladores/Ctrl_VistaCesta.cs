using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Libreria_Aggapea.App_Code.Modelos;
using Libreria_Aggapea.Herramientas;
using System.IO;

namespace Libreria_Aggapea.App_Code.Controladores
{
    public class Ctrl_VistaCesta
    {
        private Ctrl_Ficheros ctrl_F = new Ctrl_Ficheros();
        private Tools tools = new Tools();
        public List<Cesta> cestas { get; set; }            
        
        public Ctrl_VistaCesta()
        {
            cestas = File.ReadAllLines(ctrl_F.rutaCestas).Select(linea => new Cesta(linea.Split(':'))).ToList();
        }       

        public void actualizarCesta(Usuario usuario, Libro libroBorrar)
        {
            foreach(Cesta cesta in cestas)
            {
                if ( cesta.dueño.nombre == usuario.nombre )
                {
                    foreach (Libro libro in cesta.listaLibros)
                        if (libro.titulo == libroBorrar.titulo)
                        {
                            cesta.listaLibros.Remove(libro);
                            ctrl_F.actualizarCesta(cesta);
                            break;
                        }
                }
            }           
        }

        public void añadirLibroCestaUsuario(Usuario usuario, Libro libro)
        {
            foreach (Cesta cesta in cestas)
            {
                if (cesta.dueño == usuario)
                {
                    cesta.listaLibros.Add(libro);
                    break;
                }
            }

            ctrl_F.añadirLibroTxTCesta(usuario, libro);
        }

        public void comprobarCesta(Usuario usuario)
        {
            if (cestas.Where(cesta => cesta.dueño == usuario).Select( cesta => cesta ).Count() == 0) {            
                cestas.Add(new Cesta(usuario));
            }
        }
    }
}