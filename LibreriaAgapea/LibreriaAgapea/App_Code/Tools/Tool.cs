using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using LibreriaAgapea.App_Code.Controllers;
using LibreriaAgapea.App_Code.Models;
using System.IO;

namespace LibreriaAgapea.App_Code.Tools
{
    public class Tool
    {
        private CFile ctrl_F = new CFile();
        public Dictionary<Button, Book> mapeoBotonesCompra = new Dictionary<Button, Book>();
        public Dictionary<RadioButton, string> mapeoBotonesRadios = new Dictionary<RadioButton, string>();
        public Dictionary<Button, Book> mapeoBotonesCesta = new Dictionary<Button, Book>();

        public void pintarCajaInfoPagina(TextBox cajaMultilinea, HttpContext datos)
        {
            string message = "";
            List<string> valores = (from string key in datos.Request.Params where (key != null) select key).ToList();
            foreach (string key in datos.Request.Params) message += "clave: " + key + " ---- valor:_" + datos.Request[key] + "\n";
            cajaMultilinea.Text = message;
        }

        public Book fabricaLibros(string titulo)
        {
            return File.ReadAllLines(ctrl_F.rutaLibros).Where(libro => libro.Split(':')[0] == titulo).Select(libro => new Book(libro.Split(':'))).ElementAt(0);
        }

        public void crearPanelLibro(TableCell columnActual, Book libro, Action<object, EventArgs> comprarLibro)
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