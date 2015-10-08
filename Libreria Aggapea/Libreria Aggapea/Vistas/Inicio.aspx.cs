using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Libreria_Aggapea.App_Code.Controladores;
using Libreria_Aggapea.Herramientas;

namespace Libreria_Aggapea.Vistas
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        private Ctrl_Ficheros ctrl_F = new Ctrl_Ficheros();
        private Ctrl_VistaLibros ctrl_VL = new Ctrl_VistaLibros();
        private Tools tool = new Tools();

        protected void Page_Load(object sender, EventArgs e)
        {
            tool.pintarCajaInfoPagina(TextBox1, Context);

            if (this.Request.QueryString["usuario"] != null)
            {
                welcomeUsuario.Text = "Bienvenido de nuevo, " + this.Request.QueryString["usuario"].ToString();
            }
            else
            {
                welcomeUsuario.Text = "Bienvenido";
            }

            if (!IsPostBack) {
                generarTreeLibros();
            }
            
            generarTabla();
        }

        private void generarTreeLibros()
        {
            TreeNode hoja = new TreeNode("Categorias");
            catalogo_libros.Nodes.Add(hoja);
            catalogo_libros.ExpandDepth = 1;

            foreach (string libroBruto in ctrl_VL.libros) {
                hoja = new TreeNode();
                hoja.Text = libroBruto.Split(':')[6];
                catalogo_libros.Nodes[0].ChildNodes.Add(hoja);
                foreach ( string libroElegido in ctrl_VL.leerLibros(hoja.Text))
                {
                    hoja.ChildNodes.Add(new TreeNode(libroElegido));
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
            ctrl_F.comprarLibro_ActualizarTxT(ctrl_VL.mapeoBotones[(Button)sender]);
            Response.Redirect(Request.RawUrl);
        }

        protected void catalogo_libros_SelectedNodeChanged(object sender, EventArgs e)
        {
            expositor_libros.Controls.Clear();
            TreeView root = (TreeView) sender;
            generarTabla(root.SelectedNode.Text);
        }
    }
}