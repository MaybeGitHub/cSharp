using Libreria_Aggapea.App_Code.Modelos;
using System;
using System.Collections;
using System.Web;
using System.Web.UI.WebControls;
using Libreria_Aggapea.Herramientas;
using System.Linq;

namespace Libreria_Aggapea.App_Code.Controladores
{
    public class Ctrl_VistaLibros
    {
        private Tools tools = new Tools();
        public ArrayList listaLibros { get; set; }

        public Ctrl_VistaLibros()
        {
            listaLibros = new ArrayList();
            tools.rellenarList(listaLibros, "libros");
        }

        public ArrayList leerLibros(string categoria)
        {
            ArrayList ret = new ArrayList();

            foreach ( Libro libro in listaLibros )
            {               
                if ( libro.categoria == categoria )
                {
                    ret.Add(libro);
                }
            }
            return ret;
        }

        public ArrayList buscarLibros(string busqueda, string tipo)
        {
            ArrayList libros = new ArrayList();
            foreach (Libro libro in listaLibros)
            {
                if (tipo.ToLower() == "titulo" && libro.titulo.StartsWith(busqueda))
                {                    
                    libros.Add(libro);
                }

                if (tipo.ToLower() == "autor" && libro.autor.StartsWith(busqueda))
                {
                    libros.Add(libro);
                }
            }                
            return libros;               
         }
     }
}
