using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LibreriaAgapea.App_Code.Controllers;
using LibreriaAgapea.App_Code.Models;
using LibreriaAgapea.App_Code.Tools;
using LibreriaAgapea.ItemControllers;

namespace LibreriaAgapea.Views
{
    public partial class Center : System.Web.UI.Page
    {
        private Tool tool = new Tool();
        private CBook cB = new CBook();
        private CCart cC = new CCart();
        private string selectedType;
        private User usuario;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["usuario"] != null)
            {
                usuario = (User)Session["usuario"];
            }
            else
            {
                usuario = new User("admin", "12345678");
            }

            string paramentrosEvent = Request.Params["__EVENTARGUMENT"];

            if (IsPostBack)
            {
                if (paramentrosEvent != null && paramentrosEvent.Split('\\').Count() > 1)
                {
                    string[] datosEventArgument = paramentrosEvent.Split('\\');
                    if (datosEventArgument[0].Contains("Categorias"))
                    {
                        selectedType = datosEventArgument[1];
                    }
                }
            }            
            generarTablaCentral();
        }

        private void generarTablaCentral()
        {
            table_Books.Controls.Clear();
            TableCell columnActual = null;
            TableRow rowActual = null;
            VBook vB;
            List<Book> librosMostrar = cB.libros;
            if (selectedType != null)
            {
                librosMostrar = cB.leerLibros(selectedType);
            }

            foreach (Book libro in librosMostrar)
            {
                if (librosMostrar.IndexOf(libro) % 3 == 0)
                {
                    rowActual = new TableRow();
                    table_Books.Rows.Add(rowActual);
                }
                columnActual = new TableCell();
                columnActual.ControlStyle.BorderColor = System.Drawing.Color.RosyBrown;
                columnActual.ControlStyle.BorderWidth = 1;
                vB = LoadControl("~/ItemControllers/VBook.ascx") as VBook;
                vB.getButton().Click += new EventHandler(comprarLibro);
                vB.createVBook(libro);
                tool.mapeoBotonesCompra.Add(vB.getButton(), libro);
                columnActual.Controls.Add(vB);
                rowActual.Cells.Add(columnActual);
            }           
        }

        private void comprarLibro(object sender, EventArgs e)
        {
            Book libro = tool.mapeoBotonesCompra[(Button)sender];
            cC.añadirLibroCestaUsuario(usuario, libro);
            Response.Redirect(Request.RawUrl);
        }
    }
}