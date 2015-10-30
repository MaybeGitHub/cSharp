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
    public partial class Index : System.Web.UI.MasterPage
    {
        private CBook cB = new CBook();
        private CCart cC = new CCart();
        private Tool tool = new Tool();
        private User usuario;
        private string categoriaSeleccionada = null;
        private string text_FindValue = "";
        private RadioButton radio;
        private List<RadioButton> listRadios = new List<RadioButton>();

        protected void Page_Load(object sender, EventArgs e)
        {
            tool.pintarCajaInfoPagina(text_PageInfo, Context);
            text_Find.Focus();
            if (Session["usuario"] != null)
            {
                usuario = (User)Session["usuario"];
            }
            else
            {
                usuario = new User("admin", "12345678");
            }
            cC.comprobarCesta(usuario);
            label_Welcome.Text = "Bienvenido de nuevo, " + usuario.nombre;            

            if (IsPostBack)
            {
                string paramentrosEvent = Request.Params["__EVENTARGUMENT"];
                if (paramentrosEvent != null && paramentrosEvent.Split('\\').Count() > 1)
                {
                    string[] datosEventArgument = paramentrosEvent.Split('\\');
                    if (datosEventArgument[0].Contains("Categorias"))
                    {
                        categoriaSeleccionada = datosEventArgument[1];
                    }
                }
            }
            else
            {
                generarTreeCategorias();
            }
            generarRadios();
            generarTablaCentral();
            generarCesta();
        }

        private void generarRadios()
        {
            table_Radios.Controls.Clear();
            table_Radios.Rows.Add(new TableRow());
            TableCell columna;
            RadioButton radio;

            for (int i = 0; i < 5; i++)
            {
                columna = new TableCell();
                radio = new RadioButton();

                radio.GroupName = "busqueda";
                switch (i)
                {
                    case 0: radio.Checked = true; radio.Text = "Autor"; break;
                    case 1: radio.Text = "Titulo"; break;
                    case 2: radio.Text = "Categoria"; break;
                    case 3: radio.Text = "Editorial"; break;    
                    case 4: radio.Text = "ISBN"; break;
                }
                listRadios.Add(radio);                
                columna.Controls.Add(radio);
                table_Radios.Rows[0].Cells.Add(columna);
            }
        }

        private void generarCesta()
        {
            table_Cart.Controls.Clear();
            table_Cart.CellSpacing = 0;

            //Busco Cesta Existente            

            Cart cestaUsuario = cC.cestas.Where(cesta => cesta.dueño != null && cesta.dueño.nombre == usuario.nombre).ElementAt(0);

            // Cabecera

            TableRow fila = new TableRow();
            table_Cart.Rows.Add(fila);

            TableCell columna = new TableCell();
            columna.ControlStyle.BorderColor = System.Drawing.Color.DarkSalmon;
            columna.ControlStyle.BackColor = System.Drawing.Color.LightSalmon;
            columna.ControlStyle.BorderStyle = BorderStyle.Solid;
            table_Cart.Rows[0].Cells.Add(columna);

            Label label = new Label();
            label.Text = "Tu cesta";
            label.Style.Add("text-align", "center");
            label.Font.Bold = true;
            label.Style.Add("display", "block");
            columna.Controls.Add(label);

            // Libros

            fila = new TableRow();
            table_Cart.Rows.Add(fila);

            columna = new TableCell();
            columna.ControlStyle.BorderColor = System.Drawing.Color.DarkSalmon;
            columna.ControlStyle.BorderStyle = BorderStyle.Solid;
            table_Cart.Rows[1].Cells.Add(columna);
            List<string> listBooks = new List<string>();

            foreach (Book libro in cestaUsuario.listaLibros)
            {               
                if ( !listBooks.Contains(libro.titulo) ) {
                    listBooks.Add(libro.titulo);
                    VCarts vC = LoadControl("~/ItemControllers/VCarts.ascx") as VCarts;
                    vC.createVCarts(libro.titulo);
                    foreach(Book book in cestaUsuario.listaLibros)
                    {
                        if ( libro.titulo == book.titulo)
                        {
                            vC.addCount();
                        }
                    }
                    vC.getButton().Click += new EventHandler(borrarLibroCesta);
                    tool.mapeoBotonesCesta.Add(vC.getButton(), libro);
                    columna.Controls.Add(vC);
                }                
            }

            // Coste

            fila = new TableRow();
            table_Cart.Rows.Add(fila);

            columna = new TableCell();
            columna.ControlStyle.BorderColor = System.Drawing.Color.DarkSalmon;
            columna.ControlStyle.BackColor = System.Drawing.Color.LightSalmon;
            columna.ControlStyle.BorderStyle = BorderStyle.Solid;
            table_Cart.Rows[2].Cells.Add(columna);

            label = new Label();
            label.Text = "Total : ";

            double total = 0;
            foreach (Book libro in cestaUsuario.listaLibros) total += libro.precio;            
            label.Text += total + " €";
            label.Font.Bold = true;
            label.Style.Add("display", "block");
            columna.Controls.Add(label);

            // Comprar

            fila = new TableRow();
            table_Cart.Rows.Add(fila);

            columna = new TableCell();
            columna.ControlStyle.BorderColor = System.Drawing.Color.DarkSalmon;
            columna.ControlStyle.BackColor = System.Drawing.Color.LightSalmon;
            columna.ControlStyle.BorderStyle = BorderStyle.Solid;
            columna.HorizontalAlign = HorizontalAlign.Center;
            table_Cart.Rows[3].Cells.Add(columna);

            Button pagar_button = new Button();
            pagar_button.Text = "Pagar";
            columna.Controls.Add(pagar_button);
        }

        private void generarTreeCategorias()
        {
            List<string> categoriasPuestas = new List<string>();
            TreeNode hoja = new TreeNode("Categorias");
            tree_BookType.Nodes.Add(hoja);
            tree_BookType.ExpandDepth = 1;

            foreach (Book libro in cB.libros)
            {
                hoja = new TreeNode();
                hoja.Text = libro.categoria;
                if (!categoriasPuestas.Contains(hoja.Text))
                {
                    categoriasPuestas.Add(hoja.Text);
                    tree_BookType.Nodes[0].ChildNodes.Add(hoja);
                }
            }
        }

        private void generarTablaCentral()
        {
            ((Table)main.FindControl("table_Books")).Controls.Clear();
            TableCell columnActual = null;
            TableRow rowActual = null;
            VBook vB;
            List<Book> librosMostrar = cB.libros;
            if (categoriaSeleccionada != null)
            {
                librosMostrar = cB.buscarLibros(categoriaSeleccionada, "categoria");
            }
            
            if ( text_FindValue != "")
            {
                radio = listRadios.Where(radio => radio.Checked).SingleOrDefault();
                text_FindValue = text_FindValue.Replace(text_FindValue.ElementAt(0).ToString(), text_FindValue.ElementAt(0).ToString().ToUpper());
                librosMostrar = cB.buscarLibros(text_FindValue, radio.Text.ToLower());
            }
            foreach (Book libro in librosMostrar)
            {
                if (librosMostrar.IndexOf(libro) % 3 == 0)
                {
                    rowActual = new TableRow();
                    ((Table)main.FindControl("table_Books")).Rows.Add(rowActual);
                }
                columnActual = new TableCell();
                columnActual.ControlStyle.BorderColor = System.Drawing.Color.RosyBrown;
                columnActual.ControlStyle.BorderWidth = 1;
                vB = LoadControl("~/ItemControllers/VBook.ascx") as VBook;
                vB.getButton().Click += new EventHandler(añadirLibroCesta);
                vB.createVBook(libro);
                tool.mapeoBotonesCompra.Add(vB.getButton(), libro);
                columnActual.Controls.Add(vB);
                rowActual.Cells.Add(columnActual);
            }
        }

        private void añadirLibroCesta(object sender, EventArgs e)
        {
            cC.añadirLibroCesta(usuario, tool.mapeoBotonesCompra[(Button)sender]);
        }

        private void borrarLibroCesta(object sender, EventArgs e)
        {
            cC.borrarLibroCesta(usuario, tool.mapeoBotonesCesta[(Button)sender]);
        }

        protected void busqueda_Btn_Click(object sender, EventArgs e)
        {
            text_FindValue = text_Find.Text;
        }
    }
}