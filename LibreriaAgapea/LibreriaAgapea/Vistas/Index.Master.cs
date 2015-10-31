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

            if (Session["usuario"] != null) usuario = (Usuario)Session["usuario"];

            label_Welcome.Text = "Bienvenido de nuevo, " + usuario.nombre;

            if (IsPostBack)
            {
                foreach (string clave in Request.Params) { 
                    
                    //TreeView
                    if (clave == "__EVENTARGUMENT" && Request.Params[clave].Split('\\').Count() > 1) librosQueMeInteresan = cL.buscarLibros(Request.Params[clave].Split('\\')[1], "categoria");
                    
                    //Panel Central
                    if (clave.Contains("button_Comprar") && Request.Params[clave] == "Comprar") cU.meterEnCesta(usuario, ayudante.fabricaLibros(clave.Split('$')[4], false));

                    //Cesta
                    if (clave.Contains("button_Borrar") && Request.Params[clave] == "X") cU.sacarDeCesta(usuario, ayudante.fabricaLibros(clave.Split('$')[3], false));

                    //Buscador                    
                    if (clave.Contains("button_Buscador") && Request.Params[clave] == "Buscar" && text_Buscador.Text != "")
                    {
                        string tipoBusqueda = "";
                        for ( int i = 0; i < row_Radios.Cells.Count; i++)
                        {
                            RadioButton radio = row_Radios.Cells[i].Controls[0] as RadioButton;
                            if (radio.Checked) { tipoBusqueda = radio.ID; break; }
                        }
                        librosQueMeInteresan = cL.buscarLibros(ayudante.capitalizar(text_Buscador.Text), tipoBusqueda);
                        if (librosQueMeInteresan.Count == 0) librosQueMeInteresan = cL.listaLibros;
                    }
                }
            }
            else
            {               
                generarTreeCategorias();                       
            }            

            generarTablaCentral(librosQueMeInteresan);
            generarCesta(usuario.cesta);
            text_Buscador.Text = "";
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

            int cont = 0;
            foreach (TableRow filaSeleccionada in table_Cesta.Rows)
            {                
                filaSeleccionada.Cells[0].ControlStyle.BorderColor = System.Drawing.Color.SaddleBrown;
                filaSeleccionada.Cells[0].ControlStyle.BackColor = System.Drawing.Color.SaddleBrown;
                if (cont == 1) filaSeleccionada.Cells[0].ControlStyle.BackColor = System.Drawing.Color.SandyBrown;
                filaSeleccionada.Cells[0].ControlStyle.BorderStyle = BorderStyle.Solid;
                cont++;
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