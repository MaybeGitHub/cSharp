using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LibreriaAgapea.App_Code.Modelos;
using LibreriaAgapea.App_Code.Herramientas;
using LibreriaAgapea.ControladoresObjetos;
using LibreriaAgapea.App_Code.Controladores;

namespace LibreriaAgapea.Vistas
{    
    public partial class Carro : System.Web.UI.Page
    {
        private Ayudante ayudante = new Ayudante();
        private CUsuario cU = new CUsuario();
        Usuario usuario = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            usuario = ayudante.fabricaUsuario(Request.Cookies["usuario"].Value);
            ayudante.construirPath((Table)Master.FindControl("table_Path"), "Inicio:Carro");

            if (IsPostBack)
            {
                foreach (string clave in Request.Params)
                {
                    //imgButtons
                    if (clave.Contains("imgButton") && clave.Contains("x"))
                    {
                        string codigoLibro = clave.Split('$')[4].Split('.')[0];
                        switch (clave.Split('$')[3].Split('_')[1])
                        {                            
                            case "Up":
                                cU.meterEnCesta(usuario, ayudante.fabricaLibros(codigoLibro, false));
                                break;
                            case "Down":
                                cU.sacarDeCesta(usuario, ayudante.fabricaLibros(codigoLibro, false), false);
                                break;
                            case "Borrar":
                                cU.sacarDeCesta(usuario, ayudante.fabricaLibros(codigoLibro, false), true);
                                break;
                        }
                    }

                    if (clave.Contains("button_Salir") && Request.Params[clave] == "Log out")
                    {
                        HttpCookie miCookie = new HttpCookie("usuario");
                        miCookie.Expires = DateTime.Now.AddDays(-1d);
                        Response.Cookies.Add(miCookie);
                        Response.Redirect("Centro.aspx");
                    }

                    //Buttons
                    if (clave.Contains("button"))
                    {
                        switch (Request.Params[clave].Contains("Terminar"))
                        {
                            case true:
                                Response.Redirect("Facturacion.aspx");
                                break;
                            case false:
                                Response.Redirect("Centro.aspx");
                                break;
                        }
                    }
                }
            }           

            generarTabla();
        }

        private void generarTabla()
        {
            TableRow fila = null;
            TableCell columna = null;
            double total = 0;

            foreach (Libro libro in usuario.cesta.listaLibros.Distinct(ayudante.comparadorTitulos()))
            {
                fila = new TableRow();                
                columna = new TableCell();
                VCarrito vc = LoadControl("~/ControladoresObjetos/VCarrito.ascx") as VCarrito;
                vc.crearCarrito(libro);
                vc.ponerCantidad(ayudante.librosRepetidos(libro, usuario.cesta.listaLibros));
                columna.Controls.Add(vc);
                columna.ColumnSpan = 3;
                fila.Cells.Add(columna);
                table_Carrito.Rows.Add(fila);
                total += libro.precio * vc.cantidad;
            }

            foreach (TableRow filaElegida in table_Carrito.Rows)
            {
                if (table_Carrito.Rows.GetRowIndex(filaElegida) % 2 == 0)
                {
                    filaElegida.BackColor = System.Drawing.Color.LightYellow;
                }
            }           

            fila = new TableRow();
            fila.BackColor = System.Drawing.Color.FromArgb(Convert.ToInt32("ffcc99", 16));

            columna = new TableCell();
            Button boton = new Button();
            boton.ID = "button_Seguir";
            boton.Text = "Seguir Comprando";
            columna.Controls.Add(boton);
            columna.HorizontalAlign = HorizontalAlign.Center;
            columna.Width = new Unit("32%");
            fila.Cells.Add(columna);

            columna = new TableCell();           
            Label label = new Label();
            label.Text = "Total : " + total + " €";
            label.Font.Size = FontUnit.Large;
            label.Font.Bold = true;
            columna.Controls.Add(label);
            columna.Width = new Unit("34%");
            columna.HorizontalAlign = HorizontalAlign.Center;
            fila.Cells.Add(columna);

            columna = new TableCell();
            boton = new Button();
            boton.ID = "button_Terminar";
            boton.Text = "Terminar Compra";
            columna.Controls.Add(boton);
            columna.HorizontalAlign = HorizontalAlign.Center;
            columna.Width = new Unit("32%");
            fila.Cells.Add(columna);

            table_Carrito.Rows.Add(fila);                          
        }
    }        
}
