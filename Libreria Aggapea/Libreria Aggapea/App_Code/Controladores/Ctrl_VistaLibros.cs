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
        private Ctrl_Ficheros ctrl_F = new Ctrl_Ficheros();
        private Tools tools = new Tools();
        public ArrayList listaLibros { get; set; }
        public Hashtable mapeoBotones = new Hashtable();

        public Ctrl_VistaLibros()
        {
            listaLibros = new ArrayList();
            tools.rellenarList(listaLibros, "libros");            
        }

        public void construirPanelLibro(TableCell columnActual, int fila, int columna, Action<object, EventArgs> comprarLibro)
        {           
            int libroActual = (3 * fila) + columna;
            Libro libro = (Libro)listaLibros[libroActual];

            foreach (Label label in libro.generarLabels())
            {
                columnActual.Controls.Add(label);
            }

            Button comprar_Button = new Button();
            comprar_Button.Text = "Comprar";
            comprar_Button.ID = "comprar_boton" + libroActual;
            comprar_Button.Click += new EventHandler( comprarLibro );
            mapeoBotones.Add(comprar_Button, libro);
            columnActual.Controls.Add(comprar_Button);
        }

        public void construirPanelLibro(TableCell columnActual, Libro libro, Action<object, EventArgs> comprarLibro)
        {
            foreach (Label label in libro.generarLabels())
            {
                columnActual.Controls.Add(label);
            }

            Button comprar_Button = new Button();
            comprar_Button.Text = "Comprar";
            comprar_Button.Click += new EventHandler(comprarLibro);
            mapeoBotones.Add(comprar_Button, libro);
            columnActual.Controls.Add(comprar_Button);
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
            switch (tipo.ToLower())
            {
                case "titulo":
                    foreach (Libro libro in listaLibros)
                    {
                        if (libro.titulo.StartsWith(busqueda))
                        {
                            libros.Add(libro);
                        }
                    }
                    break;
                case "autor":
                    foreach (Libro libro in listaLibros)
                    {
                        if (libro.autor.StartsWith(busqueda))
                        {
                            libros.Add(libro);
                        }
                    }
                    break;
                case "categoria":
                    foreach (Libro libro in listaLibros)
                    {
                        if (libro.categoria.StartsWith(busqueda))
                        {
                            libros.Add(libro);
                        }
                    }
                    break;
            }

            return libros;               
         }

     }
}
