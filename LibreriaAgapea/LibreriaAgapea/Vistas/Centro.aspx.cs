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
    public partial class Centro : System.Web.UI.Page
    {
        private CLibro cL = new CLibro();
        private CUsuario cU = new CUsuario();
        private Ayudante ayudante = new Ayudante();
        private Usuario usuario = null;
        private int pagina = 0, cantidadLibros, numeroLibrosTabla = 6;

        protected void Page_Load(object sender, EventArgs e)
        {
            text_Buscador.Focus();
            List<Libro> librosQueMeInteresan = cL.listaLibros;

            if (Request.Cookies["usuario"] != null)
            {
                usuario = ayudante.fabricaUsuario(Request.Cookies["usuario"].Value);
            }

            if (IsPostBack)
            {
                foreach (string clave in Request.Params)
                {
                    //TreeView
                    if (clave == "__EVENTARGUMENT" && Request.Params[clave].Split('\\').Count() > 1) librosQueMeInteresan = cL.buscarLibros(Request.Params[clave].Split('\\')[1], "categoria");

                    //Panel Central
                    if (clave.Contains("button_Comprar") && Request.Params[clave] == "Comprar" && usuario != null) cU.meterEnCesta(usuario, ayudante.fabricaLibros(clave.Split('$')[4], false));

                    //Cesta
                    if (clave.Contains("button_Borrar") && Request.Params[clave] == "X") cU.sacarDeCesta(usuario, ayudante.fabricaLibros(clave.Split('$')[4], false), false);

                    //Buscador                    
                    if (clave.Contains("button_Buscador") && Request.Params[clave] == "Buscar" && text_Buscador.Text != "")
                    {
                        string tipoBusqueda = "";
                        for (int i = 0; i < row_Radios.Cells.Count; i++)
                        {
                            RadioButton radio = row_Radios.Cells[i].Controls[0] as RadioButton;
                            if (radio.Checked) { tipoBusqueda = radio.ID; break; }
                        }
                        librosQueMeInteresan = cL.buscarLibros(ayudante.capitalizar(text_Buscador.Text), tipoBusqueda);
                        if (librosQueMeInteresan.Count == 0) librosQueMeInteresan = cL.listaLibros;
                    }

                    //imgButtons
                    if (clave.Contains("imgButton_Libro"))
                    {
                        string codigoLibro = clave.Split('$')[1];
                    }

                    //Paginas
                    if (clave.Contains("button_Pagina"))
                    {
                        pagina = int.Parse(Request.Params[clave]) - 1;                        
                    }
                }
            }
            else
            {
                generarTreeCategorias();
            }

            if (usuario != null) generarCesta(usuario.cesta);

            cantidadLibros = librosQueMeInteresan.Count;
            if ((pagina * numeroLibrosTabla) + numeroLibrosTabla <= librosQueMeInteresan.Count)
            {
                librosQueMeInteresan = librosQueMeInteresan.GetRange(pagina * numeroLibrosTabla, numeroLibrosTabla);
            }
            else
            {
                int librosRestantes = librosQueMeInteresan.Count - (pagina * numeroLibrosTabla);
                librosQueMeInteresan = librosQueMeInteresan.GetRange(librosQueMeInteresan.Count - librosRestantes, librosRestantes);
            }

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
            table_Libros.Controls.Clear();
            TableCell columnActual = null;
            TableRow rowActual = null;
            VLibros vL;

            // Libros 
            foreach (Libro libro in lista)
            {
                if (lista.IndexOf(libro) % 3 == 0)
                {
                    rowActual = new TableRow();                    
                    table_Libros.Rows.Add(rowActual);
                }
                columnActual = new TableCell();
                columnActual.HorizontalAlign = HorizontalAlign.Center;
                vL = LoadControl("~/ControladoresObjetos/VLibros.ascx") as VLibros;                
                vL.createVBook(libro);
                if (usuario == null) vL.mostrarBoton(false);                
                columnActual.Controls.Add(vL);
                rowActual.Cells.Add(columnActual);
            }

            // Botones Paginas
            if (cantidadLibros > numeroLibrosTabla)
            {
                TableRow fila = new TableRow();
                TableCell columna = new TableCell();
                Button boton;
                for (int i = 0; i < (cantidadLibros / numeroLibrosTabla) + 1; i++)
                {
                    boton = new Button();
                    boton.ID = "button_Pagina" + (i + 1).ToString();
                    boton.Text = (i + 1).ToString();
                    columna.Controls.Add(boton);
                }
                fila.Cells.Add(columna);
                table_Paginas.Rows.Add(fila);
            }
        }

        protected void pagar_boton_Click(object sender, EventArgs e)
        {
            if(usuario.cesta.listaLibros.Count != 0) Response.Redirect("Carro.aspx");
        }
    }
}