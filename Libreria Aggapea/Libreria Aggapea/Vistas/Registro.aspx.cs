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

        private Ctrl_VistaUsuarios ctrl_VU = new Ctrl_VistaUsuarios();
        private Tools tool = new Tools();
        private Usuario usuario;

        protected void Page_Load(Object sender, EventArgs e)
        {
            tool.pintarCajaInfoPagina(TextBox1, Context);
        }

        protected void registrar_boton_Click(object sender, ImageClickEventArgs e)
        {
            if (IsValid)
            {            
                ctrl_VU.añadirUsuario(usuario);
                Session["usuario"] = usuario;
                Server.Transfer("Inicio.aspx");
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
            usuario = new Usuario(usuario_TxBox.Text, pass_TxBox.Text);
            foreach (Usuario usuariosAlmacenados in ctrl_VU.listaUsuarios) {
                if ( usuariosAlmacenados.nombre == usuario.nombre)
                {
                    args.IsValid = false;
                    break;
                }
                else
                {
                    args.IsValid = true;
                }
            }
        }
    }
}