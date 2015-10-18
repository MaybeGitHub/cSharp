using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Libreria_Aggapea.App_Code.Controladores;
using Libreria_Aggapea.App_Code.Modelos;
using System.IO;

namespace Libreria_Aggapea.Herramientas
{
    public class Tools
    {
        private Ctrl_Ficheros ctrl_F = new Ctrl_Ficheros();
        public Dictionary<Button, Libro> mapeoBotonesCompra = new Dictionary<Button, Libro>();
        public Dictionary<RadioButton, string> mapeoBotonesRadios = new Dictionary<RadioButton, string>();
        public Dictionary<Button, Libro> mapeoBotonesCesta = new Dictionary<Button, Libro>();

        public void pintarCajaInfoPagina( TextBox cajaMultilinea, HttpContext datos )
        {
            string message = "";
            List<string> valores = (from string key in datos.Request.Params where (key != null) select key).ToList();
            foreach (string key in datos.Request.Params) message += "clave: " + key + " ---- valor:_" + datos.Request[key] + "\n";
            cajaMultilinea.Text = message;            
        }

        public Libro fabricaLibros(string titulo)
        {
            return File.ReadAllLines(ctrl_F.rutaLibros).Where(libro => libro.Split(':')[0] == titulo).Select(libro => new Libro(libro.Split(':'))).ElementAt(0);            
        }

        public Table crearPanelCesta(Libro libro, Action<object, EventArgs> borrarLibroCesta)
        {
            Table nuevaTabla = new Table();
            nuevaTabla.Rows.Add(new TableRow());
            TableCell columna = new TableCell();
            Label label = new Label();
            label.Text = libro.titulo;
            label.Style.Add("display", "block");
            columna.Controls.Add(label);
            nuevaTabla.Rows[0].Cells.Add(columna);
            columna = new TableCell();
            Button borrar = new Button();
            borrar.Text = "X";
            borrar.ControlStyle.ForeColor = System.Drawing.Color.Red;
            borrar.Click += new EventHandler( borrarLibroCesta );
            mapeoBotonesCesta.Add(borrar, libro);
            columna.Controls.Add(borrar);
            nuevaTabla.Rows[0].Cells.Add(columna);
            return nuevaTabla;
        }

        public void crearPanelLibro(TableCell columnActual, Libro libro, Action<object, EventArgs> comprarLibro)
        {
            foreach (Label label in libro.generarLabels()) columnActual.Controls.Add(label);
            Button comprar_Button = new Button();
            comprar_Button.Text = "Comprar";
            comprar_Button.UseSubmitBehavior = false;
            comprar_Button.Click += new EventHandler(comprarLibro);
            mapeoBotonesCompra.Add(comprar_Button, libro);
            columnActual.Controls.Add(comprar_Button);
        }
    }
}
