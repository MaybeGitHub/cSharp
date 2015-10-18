using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Libreria_Aggapea.App_Code.Controladores;
using Libreria_Aggapea.Herramientas;
using Libreria_Aggapea.App_Code.Modelos;

namespace Libreria_Aggapea
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        private Ctrl_Ficheros ctrl_F = new Ctrl_Ficheros();       
        private Tools tools = new Tools();
        private Ctrl_VistaUsuarios ctrl_VU = new Ctrl_VistaUsuarios();
        private Usuario usuario;

        protected void Page_Load(Object sender, EventArgs e)
        {
            tools.pintarCajaInfoPagina(TextBox1, Context);
            usuario_TxBox.Focus();
        }

        protected void registrar_boton_Click(object sender, ImageClickEventArgs e)
        {
            if (IsValid)
            {
                usuario = new Usuario(usuario_TxBox.Text, pass_TxBox.Text);
                ctrl_F.añadirUsuario(usuario);
                Session["usuario"] = usuario;
                Server.Transfer("Inicio.aspx");
            }
        }

        protected void passLong_V_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if ( pass2_TxBox.Text.Length < 8) args.IsValid = false;
        }

        protected void almaCheck_V_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if ( alma_ChkBox.Checked == false ) args.IsValid = false;
            alma_ChkBox.Checked = false;
        }

        protected void usuarioExiste_FV_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if ( ctrl_VU.usuarios.Where(usuario => usuario.nombre == usuario_TxBox.Text).Count() != 0 ) args.IsValid = false;            
        }
    }
}