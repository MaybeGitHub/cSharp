using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Libreria_Aggapea.App_Code.Controladores;
using Libreria_Aggapea.Herramientas;

namespace Libreria_Aggapea
{
    public partial class WebForm1 : System.Web.UI.Page
    {

        private Ctrl_VistaUsuarios ctrl_VM = new Ctrl_VistaUsuarios();
        private Tools tool = new Tools();

        protected void Page_Load(Object sender, EventArgs e)
        {
            tool.pintarCajaInfoPagina(TextBox1, Context);
        }

        protected void registrar_boton_Click(object sender, ImageClickEventArgs e)
        {
            if (IsValid)
            {            
                ctrl_VM.meterUsuario(usuario_TxBox.Text, pass_TxBox.Text);
                Response.Redirect("Inicio.aspx?usuario=" + usuario_TxBox.Text);                
            }
        }

        protected void passLong_V_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if ( pass2_TxBox.Text.Length < 8)
            {
                args.IsValid = false;                
            }
        }

        protected void almaCheck_V_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if ( alma_ChkBox.Checked == false)
            {
                args.IsValid = false;
            }
            else
            {
                alma_ChkBox.Checked = false;
            }
        }

        protected void usuarioExiste_FV_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = ctrl_VM.validacionExistenciaUsuario(usuario_TxBox.Text);
        }
    }
}