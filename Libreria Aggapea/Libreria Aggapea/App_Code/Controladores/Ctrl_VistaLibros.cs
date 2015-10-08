using Libreria_Aggapea.App_Code.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace Libreria_Aggapea.App_Code.Controladores
{
    public class Ctrl_VistaLibros
    {
        private Ctrl_Ficheros ctrl_F = new Ctrl_Ficheros();

        public string[] libros { get; set; }
        public List<int> librosEncontrados { get; set; }

        public Dictionary<object, string> mapeoBotones = new Dictionary<object, string>();

        public Ctrl_VistaLibros()
        {
            libros = ctrl_F.leer("libros");
        }

        public void construirPanelLibro(TableCell columnActual, int fila, int columna, Action<object, EventArgs> comprarLibro)
        {           
            int libroActual = (3 * fila) + columna;
            string libroSeleccionado = libros[libroActual];
            string[] datosLibro = libroSeleccionado.Split(':');
            Libro libro = new Libro(datosLibro);

            foreach (Label label in libro.generarLabels())
            {
                columnActual.Controls.Add(label);
            }

            Button comprar_Button = new Button();
            comprar_Button.Text = "Comprar";
            comprar_Button.ID = "comprar_boton" + libroActual;
            comprar_Button.Click += new EventHandler( comprarLibro );
            mapeoBotones.Add(comprar_Button, libro.datos());
            columnActual.Controls.Add(comprar_Button);
        }

        public void construirPanelLibro(TableCell columnActual, int libroActual, Action<object, EventArgs> comprarLibro)
        {
            string libroSeleccionado = libros[libroActual];
            string[] datosLibro = libroSeleccionado.Split(':');
            Libro libro = new Libro(datosLibro);

            foreach (Label label in libro.generarLabels())
            {
                columnActual.Controls.Add(label);
            }

            Button comprar_Button = new Button();
            comprar_Button.Text = "Comprar";
            comprar_Button.ID = "comprar_boton" + libroActual;
            comprar_Button.Click += new EventHandler(comprarLibro);
            mapeoBotones.Add(comprar_Button, libro.datos());
            columnActual.Controls.Add(comprar_Button);
        }

        public List<string> leerLibros(string indice)
        {
            List<string> ret = new List<string>();
            librosEncontrados = new List<int>();
            string[] datosLibro;

            for ( int i = 0; i < libros.Length; i++ )
            {
                datosLibro = libros[i].Split(':');
                if ( datosLibro.Contains(indice) )
                {
                    librosEncontrados.Add(i);
                    ret.Add(datosLibro[0]);
                }
            }
            return ret;
        }
    }
}