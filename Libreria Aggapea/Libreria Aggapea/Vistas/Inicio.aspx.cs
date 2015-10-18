using Libreria_Aggapea.App_Code.Controladores;
using Libreria_Aggapea.Herramientas;
using System;
using System.Collections;
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
        private ArrayList listaRadios = new ArrayList();

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
                listaRadios.Add(radio);
                radio.GroupName = "busqueda";
                switch (i)
                {
                    case 0: radio.Checked = true; radio.Text = "Autor"; break;
                    case 1: radio.Text = "Titulo"; break;
                }
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

            Cesta cestaUsuario = null;

            foreach (Cesta cesta in ctrl_VC.listaCestas)
            {
                if (cesta.dueño != null && cesta.dueño.nombre == usuario.nombre)
                {
                    cestaUsuario = cesta;
                    break;
                }
            }

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

            foreach (Libro libro in cestaUsuario.listaLibros)
            {
                label = new Label();
                label.Text = (string)libro.titulo;
                label.Style.Add("display", "block");
                columna.Controls.Add(tools.crearPanelCesta(label, borrarLibroCesta));
            }

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
            foreach (Libro libro in cestaUsuario.listaLibros)
            {
                total += libro.precio;
            }
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
            ArrayList categoriasPuestas = new ArrayList();
            TreeNode hoja = new TreeNode("Categorias");
            catalogo_libros.Nodes.Add(hoja);
            catalogo_libros.ExpandDepth = 1;

            foreach (Libro libro in ctrl_VL.listaLibros)
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
                ArrayList librosCategoriaBuscada = ctrl_VL.leerLibros(categoriaSeleccionada);
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
                foreach (Libro libro in ctrl_VL.listaLibros)
                {
                    if (ctrl_VL.listaLibros.IndexOf(libro) % 3 == 0)
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
            Libro libro = (Libro)tools.mapeoBotones[sender];
            ctrl_VC.añadirLibroCestaUsuario(usuario, libro);
            Response.Redirect(Request.RawUrl);
        }

        private void borrarLibroCesta(object sender, EventArgs e)
        {
            ctrl_VC.actualizarCesta(usuario, tools.fabricaLibros((string)tools.mapeoBotones[sender]));
            Response.Redirect(Request.RawUrl);
        }

        protected void busqueda_Btn_Click(object sender, EventArgs e)
        {
            RadioButton radioSeleccionado = null;
            foreach(RadioButton radio in listaRadios)
            {
                if ( radio.Checked)
                {
                    radioSeleccionado = radio;
                    break;
                }
            }
            mostrarResultado_Tx.Text = "";
            string texto = busqueda_Tx.Text;
            string textoCapitalizado = "";
            
            if (texto.Length != 0)
            {
                textoCapitalizado = texto.Replace(texto.ElementAt(0).ToString(), texto.ElementAt(0).ToString().ToUpper());
                ArrayList librosEncontrados = ctrl_VL.buscarLibros(textoCapitalizado, radioSeleccionado.Text.ToLower());
                if (librosEncontrados.Count > 0)
                {
                    mostrarResultado_Tx.Visible = true;
                    foreach (Libro libro in librosEncontrados)
                    {
                        mostrarResultado_Tx.Height = 15 * librosEncontrados.Count;
                        mostrarResultado_Tx.Text += libro.autor + " - " + libro.titulo + " - " + libro.categoria + " - " + libro.precio + "€\n";
                    }
                }
                else
                {
                    mostrarResultado_Tx.Visible = false;
                }
            }
            else
            {
                mostrarResultado_Tx.Visible = false;
            }
            busqueda_Tx.Text = "";        
        }
    }
}