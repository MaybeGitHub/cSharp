using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LibreriaAgapea.App_Code.Herramientas;
using LibreriaAgapea.App_Code.Modelos;
using LibreriaAgapea.App_Code.Controladores;


namespace LibreriaAgapea.Vistas
{
    public partial class Login : System.Web.UI.Page
    {

        private Ayudante ayudante = new Ayudante();
        private CUsuario cU = new CUsuario();
        private Usuario usuario;

        protected void Page_Load(object sender, EventArgs e)
        {
            ayudante.pintarCajaInfoPagina(cajaInfo, Context);
            nombre.Focus();
        }

        protected void usuario_CV_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (cU.listaUsuarios.Where(usuario => usuario.nombre == nombre.Text && usuario.contraseña == password.Text).Count() == 1) args.IsValid = true; else args.IsValid = false;
        }

        protected void password_FV_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (password.Text == "") args.IsValid = false;
        }
        protected void datos_FV_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (nombre.Text == "") args.IsValid = false;
        }

        protected void entrar_Click(object sender, EventArgs e)
        {
            if ( IsValid)
            {
                if (Request.Cookies["usuario"] != null)
                {
                    Request.Cookies["usuario"].Value = nombre.Text;
                }
                else
                {
                    HttpCookie miCookie = new HttpCookie("usuario");
                    miCookie.Value = nombre.Text;
                    Response.Cookies.Add(miCookie);
                }
                Response.Redirect("Centro.aspx");
            }
        }
    }
}