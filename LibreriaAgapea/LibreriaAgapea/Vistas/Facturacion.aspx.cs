using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LibreriaAgapea.App_Code.Modelos;
using LibreriaAgapea.App_Code.Herramientas;
using LibreriaAgapea.App_Code.Controladores;

namespace LibreriaAgapea.Vistas
{
    public partial class Facturacion : System.Web.UI.Page
    {
        private Usuario usuario;
        private Ayudante ayudante = new Ayudante();

        protected void Page_Load(object sender, EventArgs e)
        {
            usuario = ayudante.fabricaUsuario(Request.Cookies["usuario"].Value);
            ayudante.construirPath((Table)Master.FindControl("table_Path"), "Inicio:Carro:Facturacion");

            if (IsPostBack)
            {
                foreach(string clave in Request.Params)
                {
                    // Boton Pagar
                    if (clave.Contains("button_Pagar"))
                    {
                        if (CMensajeria.mandarMail(usuario.email))
                        {
                            CFichero.sobrescribirCestasTxT(CFichero.rutaCestas, usuario.nombre, "0", false, true);
                            Response.Redirect("Centro.aspx");
                        }
                    }

                    if (clave.Contains("button_Volver"))
                    {
                        Response.Redirect("Carro.aspx");
                    }
                }
            }

            campo_nombre.Text = usuario.nombre;
            campo_apellido.Text = usuario.apellido;
            campo_direccion.Text = "C/ " + usuario.direccion;
            campo_email.Text = usuario.email;
            list_Cesta.Rows = usuario.cesta.listaLibros.Count;
            foreach(Libro libro in usuario.cesta.listaLibros)
            {
                list_Cesta.Items.Add("Titulo: " + libro.titulo + "    Autor: " + libro.autor + "    Editorial: " + libro.editorial + "    Precio: " + libro.precio + " €"); 
            }
        }
    }
}