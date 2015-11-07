using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LibreriaAgapea.App_Code.Modelos;
using LibreriaAgapea.App_Code.Herramientas;

namespace LibreriaAgapea.Vistas
{    
    public partial class Carro : System.Web.UI.Page
    {
        private Ayudante ayudante = new Ayudante();

        protected void Page_Load(object sender, EventArgs e)
        {
            Usuario usuario = ayudante.fabricaUsuario(Request.Cookies["usuario"].Value);
            pruebaLibros.Text = "";

            foreach (Libro libro in usuario.cesta.listaLibros.Distinct(ayudante.comparadorTitulos()))
                pruebaLibros.Text += libro.titulo + " x " + ayudante.librosRepetidos(libro, usuario.cesta.listaLibros) + "\n";

            if(!IsPostBack)
            {
                Request.Cookies["path"].Value += ":Carro";
                        
            }
        }
    }
}