using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Libreria_Aggapea.App_Code.Controladores;

namespace Libreria_Aggapea
{
    public partial class WebForm1 : System.Web.UI.Page
    {

        private Ctrl_VistaUsuarios ctrl_VM = new Ctrl_VistaUsuarios();
        protected void Page_Load(Object sender, EventArgs e)
        {
            string __message = "",__valor="";
            foreach (string key in this.Context.Request.Params)
            {
                if (this.Context.Request[key] == null) { __valor = "null";  } else { __valor = this.Context.Request[key].ToString(); };
                __message += "clave: " + key + " ---- valor:_" + __valor + "\n";
            }
            this.TextBox1.Text = __message;
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