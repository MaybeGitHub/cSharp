using System;
using System.Collections;
using System.Linq;
using System.Web;
using Libreria_Aggapea.App_Code.Modelos;
using Libreria_Aggapea.Herramientas;

namespace Libreria_Aggapea.App_Code.Controladores
{
    public class Ctrl_VistaCesta
    {
        private Ctrl_Ficheros ctrl_F { get; set; }
        private Tools tools = new Tools();

        public ArrayList listaCestas { get; set; }

        public Ctrl_VistaCesta()
        {
            listaCestas = new ArrayList();
            ctrl_F = new Ctrl_Ficheros();

            tools.rellenarList(listaCestas, "cestas");
        }

        public void añadirLibroCestaUsuario(Usuario usuario, Libro libro)
        {
            foreach(Cesta cesta in listaCestas)
            {
                if ( cesta.dueño == usuario )
                {
                    cesta.listaLibros.Add(libro);
                    break;
                }
            }
                 
            ctrl_F.añadirLibroTxTCesta(usuario, libro);
            ctrl_F.bajarStockLibroTxT(libro);
        }

        public void comprobarCesta(Usuario usuario)
        {
            bool tengo = false;

            foreach (Cesta cesta in listaCestas)
            {
                if (cesta.dueño != null && cesta.dueño.nombre == usuario.nombre)
                {
                    tengo = true;
                    break;
                }
            }
          

            if (!tengo)
            { 
                listaCestas.Add(new Cesta(usuario));
                ctrl_F.añadirNuevo(usuario.nombre, "cestas");
            }
        }
    }
}