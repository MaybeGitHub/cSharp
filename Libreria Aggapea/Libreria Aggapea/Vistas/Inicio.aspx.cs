using Libreria_Aggapea.App_Code.Controladores;
using Libreria_Aggapea.Herramientas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Libreria_Aggapea.App_Code.Modelos;
using System.Drawing;

namespace Libreria_Aggapea.Vistas
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        private Ctrl_VistaLibros ctrl_VL = new Ctrl_VistaLibros();
        private Ctrl_VistaCesta ctrl_VC = new Ctrl_VistaCesta();
        private Tools tools = new Tools();
        private Usuario usuario;
        private string categoriaSeleccionada = "Categorias";

        protected void Page_Load(object sender, EventArgs e)
        {
            tools.pintarCajaInfoPagina(TextBox1, Context);
            busqueda_Tx.Focus();
            if (Session["usuario"] != null)
            {
                usuario = (Usuario)Session["usuario"];
            }
            else
            {
                usuario = new Usuario("admin", "12345678");
            }

            welcomeUsuario.Text = "Bienvenido de nuevo, " + usuario.nombre;

            string paramentrosEvent = Request.Params["__EVENTARGUMENT"];

            if (IsPostBack)
            {
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
            tablaRadios.Rows.Add(new TableRow());
            TableCell columna;
            RadioButton radio;

            for ( int i = 0; i < 2; i++)
            {
                columna = new TableCell();                
                radio = new RadioButton();
                
                radio.GroupName = "busqueda";
                switch (i)
                {
                    case 0: radio.Checked = true; radio.Text = "Autor"; break;
                    case 1: radio.Text = "Titulo"; break;
                }
                tools.mapeoBotonesRadios.Add(radio, radio.Text.ToLower());
                columna.Controls.Add(radio);
                tablaRadios.Rows[0].Cells.Add(columna);
            }
        }

        private void generarCesta()
        {
            expositor_cesta.Controls.Clear();
            expositor_cesta.CellSpacing = 0;

            //Busco Cesta Existente

            ctrl_VC.comprobarCesta(usuario);

            Cesta cestaUsuario = ctrl_VC.cestas.Where(cesta => cesta.dueño != null && cesta.dueño.nombre == usuario.nombre).ElementAt(0);            

            // Cabecera

            TableRow fila = new TableRow();
            expositor_cesta.Rows.Add(fila);

            TableCell columna = new TableCell();
            columna.ControlStyle.BorderColor = System.Drawing.Color.DarkSalmon;
            columna.ControlStyle.BackColor = System.Drawing.Color.LightSalmon;
            columna.ControlStyle.BorderStyle = BorderStyle.Solid;
            expositor_cesta.Rows[0].Cells.Add(columna);

            Label label = new Label();
            label.Text = "Tu cesta";
            label.Style.Add("text-align", "center");
            label.Font.Bold = true;
            label.Style.Add("display", "block");
            columna.Controls.Add(label);

            // Libros

            fila = new TableRow();
            expositor_cesta.Rows.Add(fila);

            columna = new TableCell();
            columna.ControlStyle.BorderColor = System.Drawing.Color.DarkSalmon;
            columna.ControlStyle.BorderStyle = BorderStyle.Solid;
            expositor_cesta.Rows[1].Cells.Add(columna);

            foreach (Libro libro in cestaUsuario.listaLibros) columna.Controls.Add(tools.crearPanelCesta(libro, borrarLibroCesta));

            // Coste

            fila = new TableRow();
            expositor_cesta.Rows.Add(fila);

            columna = new TableCell();
            columna.ControlStyle.BorderColor = System.Drawing.Color.DarkSalmon;
            columna.ControlStyle.BackColor = System.Drawing.Color.LightSalmon;
            columna.ControlStyle.BorderStyle = BorderStyle.Solid;
            expositor_cesta.Rows[2].Cells.Add(columna);

            label = new Label();
            label.Text = "Total : ";

            double total = 0;
            foreach(Libro libro in cestaUsuario.listaLibros ) total += libro.precio;
            label.Text += total + "€";
            label.Font.Bold = true;
            label.Style.Add("display", "block");
            columna.Controls.Add(label);

            // Comprar

            fila = new TableRow();
            expositor_cesta.Rows.Add(fila);

            columna = new TableCell();
            columna.ControlStyle.BorderColor = System.Drawing.Color.DarkSalmon;
            columna.ControlStyle.BackColor = System.Drawing.Color.LightSalmon;
            columna.ControlStyle.BorderStyle = BorderStyle.Solid;
            columna.HorizontalAlign = HorizontalAlign.Center;
            expositor_cesta.Rows[3].Cells.Add(columna);

            Button pagar_button = new Button();
            pagar_button.Text = "Pagar";
            columna.Controls.Add(pagar_button);
        }

        private void generarTreeCategorias()
        {
            List<string> categoriasPuestas = new List<string>();
            TreeNode hoja = new TreeNode("Categorias");
            catalogo_libros.Nodes.Add(hoja);
            catalogo_libros.ExpandDepth = 1;

            foreach (Libro libro in ctrl_VL.libros)
            {
                hoja = new TreeNode();
                hoja.Text = libro.categoria;
                if (!categoriasPuestas.Contains(hoja.Text))
                {
                    categoriasPuestas.Add(hoja.Text);
                    catalogo_libros.Nodes[0].ChildNodes.Add(hoja);
                }
            }
        }

        private void generarTablaCentral()
        {
            expositor_libros.Controls.Clear();
            TableCell columnActual = null;
            TableRow rowActual = null;

            if (categoriaSeleccionada != "Categorias")
            {
                List<Libro> librosCategoriaBuscada = ctrl_VL.leerLibros(categoriaSeleccionada);
                foreach (Libro libro in librosCategoriaBuscada)
                {
                    if (librosCategoriaBuscada.IndexOf(libro) % 3 == 0)
                    {
                        rowActual = new TableRow();
                        expositor_libros.Rows.Add(rowActual);
                    }
                    columnActual = new TableCell();
                    columnActual.ControlStyle.BorderColor = System.Drawing.Color.Black;
                    columnActual.ControlStyle.BorderStyle = BorderStyle.Solid;
                    columnActual.Style.Add("padding", "10px");
                    tools.crearPanelLibro(columnActual, libro, comprarLibro);
                    rowActual.Cells.Add(columnActual);
                }
            }
            else
            {
                foreach (Libro libro in ctrl_VL.libros)
                {
                    if (ctrl_VL.libros.IndexOf(libro) % 3 == 0)
                    {
                        rowActual = new TableRow();
                        expositor_libros.Rows.Add(rowActual);
                    }
                    columnActual = new TableCell();
                    columnActual.ControlStyle.BorderColor = System.Drawing.Color.Black;
                    columnActual.ControlStyle.BorderStyle = BorderStyle.Solid;
                    columnActual.Style.Add("padding", "10px");
                    tools.crearPanelLibro(columnActual, libro, comprarLibro);
                    rowActual.Cells.Add(columnActual);
                }
            }
        }

        private void comprarLibro(object sender, EventArgs e)
        {
            Libro libro = tools.mapeoBotonesCompra[(Button)sender];
            ctrl_VC.añadirLibroCestaUsuario(usuario, libro);
            Response.Redirect(Request.RawUrl);
        }

        private void borrarLibroCesta(object sender, EventArgs e)
        {
            ctrl_VC.actualizarCesta(usuario, tools.mapeoBotonesCesta[(Button)sender]);
            Response.Redirect(Request.RawUrl);
        }

        protected void busqueda_Btn_Click(object sender, EventArgs e)
        {        
            RadioButton radioSeleccionado = tools.mapeoBotonesRadios.Keys.Where(radio => radio.Checked == true).ElementAt(0);
            mostrarResultado_Tx.Text = "";

            if (busqueda_Tx.Text.Length != 0)
            {
                string texto = busqueda_Tx.Text.Replace(busqueda_Tx.Text.ElementAt(0).ToString(), busqueda_Tx.Text.ElementAt(0).ToString().ToUpper());
                List<Libro> librosEncontrados = ctrl_VL.buscarLibros(texto, tools.mapeoBotonesRadios[radioSeleccionado]);
                if (librosEncontrados.Count > 0)
                {                    
                    mostrarResultado_Tx.Visible = true;
                    mostrarResultado_Tx.Height = 15 * librosEncontrados.Count;
                    foreach (Libro libro in librosEncontrados)
                        mostrarResultado_Tx.Text += libro.autor + " - " + libro.titulo + " - " + libro.categoria + " - " + libro.precio + "€\n";
                }
                else mostrarResultado_Tx.Visible = false;
            }
            else mostrarResultado_Tx.Visible = false;
            busqueda_Tx.Text = "";        
        }
    }
}