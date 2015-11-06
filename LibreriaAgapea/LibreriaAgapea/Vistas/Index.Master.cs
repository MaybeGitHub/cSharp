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
        private Usuario usuario = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Cookies["usuario"] != null )
            {
                usuario = ayudante.fabricaUsuario(Request.Cookies["usuario"].Value);
                label_Welcome.Text = "Bienvenido de nuevo, " + usuario.nombre;
                Button button_Salir = new Button();
                button_Salir.Text = "Log out";
                button_Salir.ID = "button_Salir";
                bienvenido.Controls.Add(button_Salir);                
            }
            else
            {
                label_Welcome.Text = "Bienvenido, nuevo cliente";
                Button button_Registo = new Button();
                button_Registo.Text = "Registro";
                button_Registo.ID = "button_Registro";
                Button button_Login = new Button();
                button_Login.Text = "Iniciar Sesion";
                button_Login.ID = "button_Login";
                bienvenido.Controls.Add(button_Registo);
                bienvenido.Controls.Add(button_Login);

            }           

            List<Libro> librosQueMeInteresan = cL.listaLibros;      
            ayudante.pintarCajaInfoPagina(text_PageInfo, Context);
            text_Buscador.Focus();            

            if (IsPostBack)
            {               
                foreach (string clave in Request.Params) { 
                    
                    //TreeView
                    if (clave == "__EVENTARGUMENT" && Request.Params[clave].Split('\\').Count() > 1) librosQueMeInteresan = cL.buscarLibros(Request.Params[clave].Split('\\')[1], "categoria");

                    //Panel Central
                    if (clave.Contains("button_Comprar") && Request.Params[clave] == "Comprar" && usuario != null) cU.meterEnCesta(usuario, ayudante.fabricaLibros(clave.Split('$')[4], false));

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

                    // Log out
                    if ( clave.Contains("button_Salir") && Request.Params[clave] == "Log out")
                    {
                        HttpCookie miCookie = new HttpCookie("usuario");
                        miCookie.Expires = DateTime.Now.AddDays(-1d);
                        Response.Cookies.Add(miCookie);
                        Response.Redirect("Centro.aspx");
                    }

                    // Redirigir

                    if ( (clave.Contains("button_Registro") || clave.Contains("button_Login")) && Request.Params[clave] != "")
                    {
                        Response.Redirect(clave.Split('_')[1] + ".aspx");
                    }
                }
            }
            else
            {                
                generarTreeCategorias();
                string path = "Inicio";
                HttpCookie miCookie = new HttpCookie("path");
                miCookie.Value = path;
                Response.Cookies.Add(miCookie);
                ayudante.construirPath(table_path, path);                       
            }            

            if ( usuario != null ) generarCesta(usuario.cesta);
            generarTablaCentral(librosQueMeInteresan);            
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

            foreach (Libro libro in cesta.listaLibros.Distinct(ayudante.comparadorTitulos()).OrderBy(libro => libro.titulo))
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
            pagar_button.Click += new EventHandler(pagar_boton_Click);                  
            columna.HorizontalAlign = HorizontalAlign.Center;
            table_Cesta.Rows[3].Cells.Add(columna);            
            columna.Controls.Add(pagar_button);

            int cont = 0;
            foreach (TableRow filaSeleccionada in table_Cesta.Rows)
            {      
                filaSeleccionada.Cells[0].ControlStyle.BorderColor = System.Drawing.Color.FromArgb(Convert.ToInt32("ff9966", 16));
                filaSeleccionada.Cells[0].ControlStyle.BackColor = System.Drawing.Color.FromArgb(Convert.ToInt32("ff9966", 16));
                if (cont == 1) filaSeleccionada.Cells[0].ControlStyle.BackColor = System.Drawing.Color.FromArgb(Convert.ToInt32("ffcc99", 16));
                filaSeleccionada.Cells[0].ControlStyle.BorderStyle = BorderStyle.Solid;
                cont++;
            }
        }

        private void generarTreeCategorias()
        {           
            TreeNode hoja = new TreeNode("Categorias");
            tree_Categorias.Nodes.Add(hoja);
            tree_Categorias.ExpandDepth = 1;
            List<string> listaCategorias = cL.listaLibros.Select(libro => libro.categoria).Distinct().ToList();
            foreach (string categoriaLibro in listaCategorias)
            {
                hoja = new TreeNode();
                hoja.Text = categoriaLibro;                
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
                columnActual.HorizontalAlign = HorizontalAlign.Center;                
                vL = LoadControl("~/ControladoresObjetos/VLibros.ascx") as VLibros;
                vL.getButton().ID = vL.getButton().ID + "$" + libro.ISBN10;
                vL.createVBook(libro);
                if ( usuario == null) vL.mostrarBoton(false);
                columnActual.Width = vL.width;
                columnActual.Height = vL.heigth;
                columnActual.Controls.Add(vL);
                rowActual.Cells.Add(columnActual);
            }
        }

        protected void pagar_boton_Click(object sender, EventArgs e)
        {
            Response.Redirect("Carro.aspx");
        }
    }
}