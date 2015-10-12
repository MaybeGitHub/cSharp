using Libreria_Aggapea.App_Code.Controladores;
using Libreria_Aggapea.Herramientas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Libreria_Aggapea.Vistas
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        private Ctrl_Ficheros ctrl_F = new Ctrl_Ficheros();
        private Ctrl_VistaLibros ctrl_VL = new Ctrl_VistaLibros();
        private Ctrl_VistaCesta ctrl_VC = new Ctrl_VistaCesta();
        private Tools tool = new Tools();
        private string usuario;

        protected void Page_Load(object sender, EventArgs e)
        {
            tool.pintarCajaInfoPagina(TextBox1, Context);

            if (this.Request.QueryString["usuario"] != null)
            {
                usuario = this.Request.QueryString["usuario"].ToString();
                welcomeUsuario.Text = "Bienvenido de nuevo, " + usuario;
            }
            else
            {
                usuario = "jesus";
                welcomeUsuario.Text = "Bienvenido de nuevo, " + usuario;
            }

            if (!IsPostBack) {
                generarTreeLibros();
                generarTabla();               
            }
            else
            {
                expositor_libros.Controls.Clear();
                foreach ( string key in Request.Params)
                {
                    if ( key == "__EVENTARGUMENT")
                    {
                        if (Request[key].Split('\\').Length > 1) {                            
                            string valor = Request[key].Split('\\')[1];
                            generarTabla(valor);
                        }
                        else
                        {
                            generarTabla();
                        }                        
                    }
                }               
            }

            generarCesta();
        }

        private void generarCesta()
        {
            expositor_cesta.CellSpacing = 0;
            List<string> librosGuardados = new List<string>();
            if ( ctrl_F.encontrar(usuario, "cestas"))
            {
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
                columna.Controls.Add( label );

                string[] librosComprados = null;

                foreach (string cestaBruto in ctrl_F.leer("cestas"))
                {                   
                    if (cestaBruto.Split(':')[0].Equals(usuario))
                    {
                        librosComprados = cestaBruto.Split(':');
                        break;
                    }
                }

                fila = new TableRow();
                expositor_cesta.Rows.Add(fila);

                columna = new TableCell();
                columna.ControlStyle.BorderColor = System.Drawing.Color.DarkSalmon;
                columna.ControlStyle.BorderStyle = BorderStyle.Solid;
                expositor_cesta.Rows[1].Cells.Add(columna);

                for ( int i = 1; i < librosComprados.Length; i++)
                {                   
                    label = new Label();
                    label.Text = librosComprados[i];
                    librosGuardados.Add(librosComprados[i]);
                    label.Style.Add("display", "block");
                    columna.Controls.Add(label);
                }

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
                foreach( string titulo in librosGuardados)
                {
                   foreach ( string libro in ctrl_F.leer("libros"))
                    {
                        if (libro.Split(':').Contains(titulo))
                        {
                            total += double.Parse(libro.Split(':')[7]);
                        }
                    }
                }
                label.Text += total + "€";
                label.Font.Bold = true;
                label.Style.Add("display", "block");
                columna.Controls.Add(label);

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
        }

        private void generarTreeLibros()
        {
            List<string> categoriasYaCreadas = new List<string>();
            TreeNode hoja = new TreeNode("Categorias");
            catalogo_libros.Nodes.Add(hoja);
            catalogo_libros.ExpandDepth = 1;

            foreach (string libroBruto in ctrl_VL.libros) {
                hoja = new TreeNode();
                hoja.Text = libroBruto.Split(':')[6];
                if (!categoriasYaCreadas.Contains(hoja.Text) ){
                    categoriasYaCreadas.Add(hoja.Text);
                    catalogo_libros.Nodes[0].ChildNodes.Add(hoja);
                }
            }
        }

        private void generarTabla()
        {            
            double librosRestantes = (double)ctrl_VL.libros.Length;        
            int filasNecesarias = (int)Math.Ceiling(librosRestantes / 3);
            TableCell columnActual;
            TableRow rowActual;

            for (int f = 0; f < filasNecesarias; f++)
            {
                rowActual = new TableRow();
                expositor_libros.Rows.Add(rowActual);
                for (int c = 0; c < 3 && librosRestantes > 0; c++)
                {
                    columnActual = new TableCell();
                    columnActual.ControlStyle.BorderColor = System.Drawing.Color.Black;
                    columnActual.ControlStyle.BorderStyle = BorderStyle.Solid;
                    columnActual.Style.Add("padding", "10px");
                    ctrl_VL.construirPanelLibro(columnActual, f, c, comprarLibro);                    
                    rowActual.Cells.Add(columnActual);
                    librosRestantes--;
                }
            }
        }

        private void generarTabla(string contenido)
        {
            double librosRestantes = (double)ctrl_VL.leerLibros(contenido).Count;
            int filasNecesarias = (int)Math.Ceiling(librosRestantes / 3);
            TableCell columnActual;
            TableRow rowActual;

            for (int f = 0; f < filasNecesarias; f++)
            {
                rowActual = new TableRow();
                expositor_libros.Rows.Add(rowActual);
                for (int c = 0; c < 3 && librosRestantes > 0; c++)
                {
                    columnActual = new TableCell();
                    columnActual.ControlStyle.BorderColor = System.Drawing.Color.Black;
                    columnActual.ControlStyle.BorderStyle = BorderStyle.Solid;
                    columnActual.Style.Add("padding", "10px");
                    ctrl_VL.construirPanelLibro(columnActual, ctrl_VL.librosEncontrados.ElementAt(c), comprarLibro);
                    rowActual.Cells.Add(columnActual);
                    librosRestantes--;
                }
            }

        }

        private void comprarLibro(object sender, EventArgs e)
        {
            Button botonSeleccionado = (Button)sender;
            string valorBoton = ctrl_VL.mapeoBotones[botonSeleccionado];
            ctrl_VC.añadirLibroAlUsuario(usuario, valorBoton);
            Response.Redirect(Request.RawUrl);
        }
    }
}