using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LibreriaAgapea.App_Code.Controladores;
using LibreriaAgapea.App_Code.Modelos;
using LibreriaAgapea.App_Code.Herramientas;
using LibreriaAgapea.ControladoresObjetos;

namespace LibreriaAgapea.Vistas
{
    public partial class Index : System.Web.UI.MasterPage
    {
        private CLibro cL = new CLibro();
        private CUsuario cU = new CUsuario();
        private Ayudante ayudante = new Ayudante();
       
        Usuario usuario = new Usuario("admin", "12345678");

        protected void Page_Load(object sender, EventArgs e)
        {
            List<Libro> librosQueMeInteresan = cL.listaLibros;      
            ayudante.pintarCajaInfoPagina(text_PageInfo, Context);
            text_Buscador.Focus();

            if (Session["usuario"] != null)
            {
                usuario = (Usuario)Session["usuario"];
            }
           
            label_Welcome.Text = "Bienvenido de nuevo, " + usuario.nombre;

            if (IsPostBack)
            {
                foreach (string clave in Request.Params) { 

                    if (clave == "__EVENTARGUMENT" && Request.Params[clave].Split('\\').Count() > 1)
                    {
                        //TreeView
                        librosQueMeInteresan = cL.buscarLibros(Request.Params[clave].Split('\\')[1], "categoria");
                    }

                    if (clave.Contains("ctl00$main$ctl00$button_Comprar"))
                    {
                        //Panel Central
                        cU.meterEnCesta(usuario, ayudante.fabricaLibros(clave.Split('$')[4], false));
                    }

                    if (clave.Contains("ctl00$main$ctl00$button_Borrar"))
                    {
                        //Cesta
                        cU.sacarDeCesta(usuario, ayudante.fabricaLibros(clave.Split('$')[4], false));
                    }
                }
            }
            else
            {               
                generarTreeCategorias();                       
            }

            generarTablaCentral(librosQueMeInteresan);
            generarCesta(usuario.cesta);
        }
              

        private void generarCesta(Cesta cesta)
        {           
            table_Cesta.Controls.Clear();

            // Cabecera

            TableRow fila = new TableRow();
            table_Cesta.Rows.Add(fila);

            TableCell columna = new TableCell();
            Label label = new Label();
            label.Text = "Tu cesta";
            label.Style.Add("text-align", "center");
            label.Font.Bold = true;
            label.Style.Add("display", "block");
            columna.Controls.Add(label);
            table_Cesta.Rows[0].Cells.Add(columna);           

            // Libros

            fila = new TableRow();
            table_Cesta.Rows.Add(fila);

            columna = new TableCell();

            foreach (Libro libro in cesta.listaLibros.Distinct(new ComparadorTitulos()))
            {               
                VCestas vC = LoadControl("~/ControladoresObjetos/VCestas.ascx") as VCestas;
                vC.crearVCestas(libro.titulo);
                vC.cantidad = ayudante.librosRepetidos(libro, cesta.listaLibros);
                vC.getButton().ID = vC.getButton().ID + "$" + libro.ISBN10;
                if (!ayudante.mapeoBotones.ContainsKey(vC.getButton().ID)) ayudante.mapeoBotones.Add(vC.getButton().ID, libro);
                columna.Controls.Add(vC);
              
            }
            table_Cesta.Rows[1].Cells.Add(columna);

            // Coste

            fila = new TableRow();
            table_Cesta.Rows.Add(fila);

            columna = new TableCell();
            label = new Label();
            label.Text = "Total : ";
            double total = 0;
            foreach (Libro libro in cesta.listaLibros) total += libro.precio;
            label.Text += total + " €";
            label.Font.Bold = true;
            label.Style.Add("display", "block");
            table_Cesta.Rows[2].Cells.Add(columna);            
            columna.Controls.Add(label);

            // Comprar

            fila = new TableRow();
            table_Cesta.Rows.Add(fila);
            columna = new TableCell();
            Button pagar_button = new Button();
            pagar_button.ID = "button_Pagar";
            pagar_button.Text = "Pagar";                        
            columna.HorizontalAlign = HorizontalAlign.Center;
            table_Cesta.Rows[3].Cells.Add(columna);            
            columna.Controls.Add(pagar_button);

            foreach (TableRow filaSeleccionada in table_Cesta.Rows)
            {
                filaSeleccionada.Cells[0].ControlStyle.BorderColor = System.Drawing.Color.DarkSalmon;
                filaSeleccionada.Cells[0].ControlStyle.BackColor = System.Drawing.Color.LightSalmon;
                filaSeleccionada.Cells[0].ControlStyle.BorderStyle = BorderStyle.Solid;
            }
        }

        private void generarTreeCategorias()
        {           
            TreeNode hoja = new TreeNode("Categorias");
            tree_Categorias.Nodes.Add(hoja);
            tree_Categorias.ExpandDepth = 1;
            List<Libro> librosFiltrados = cL.listaLibros.Distinct(new ComparadorCategorias()).ToList();
            foreach (Libro libro in librosFiltrados)
            {
                hoja = new TreeNode();
                hoja.Text = libro.categoria;                
                tree_Categorias.Nodes[0].ChildNodes.Add(hoja);
            }
        }

        private void generarTablaCentral(List<Libro> lista)
        {
            ((Table)main.FindControl("table_Libros")).Controls.Clear();
            TableCell columnActual = null;
            TableRow rowActual = null;
            VLibros vL;     
            
            foreach (Libro libro in lista)
            {
                if (lista.IndexOf(libro) % 3 == 0)
                {
                    rowActual = new TableRow();
                    ((Table)main.FindControl("table_Libros")).Rows.Add(rowActual);
                }
                columnActual = new TableCell();
                columnActual.ControlStyle.BorderColor = System.Drawing.Color.RosyBrown;
                columnActual.ControlStyle.BorderWidth = 1;
                vL = LoadControl("~/ControladoresObjetos/VLibros.ascx") as VLibros;
                vL.getButton().ID = vL.getButton().ID + "$" + libro.ISBN10;
                if(!ayudante.mapeoBotones.ContainsKey(vL.getButton().ID)) ayudante.mapeoBotones.Add(vL.getButton().ID, libro);
                vL.createVBook(libro);
                columnActual.Controls.Add(vL);
                rowActual.Cells.Add(columnActual);
            }
        }

        public class ComparadorTitulos : IEqualityComparer<Libro>
        {
            public bool Equals(Libro x, Libro y)
            {                
                return x.titulo == y.titulo;
            }

            public int GetHashCode(Libro libro)
            {               
                return libro.titulo.GetHashCode();
            }
        }

        public class ComparadorCategorias : IEqualityComparer<Libro>
        {
            public bool Equals(Libro x, Libro y)
            {
                return x.categoria == y.categoria;
            }

            public int GetHashCode(Libro libro)
            {
                return libro.categoria.GetHashCode();
            }
        }
    }
}