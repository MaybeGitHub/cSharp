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
            text_PageInfo.Text = "";
            ayudante.pintarCajaInfoPagina(text_PageInfo, Context);

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

            if (IsPostBack)
            {               
                foreach (string clave in Request.Params) { 
                    
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

            if (Request.Cookies["path"] == null)
            {
                string path = "Inicio";
                HttpCookie miCookie = new HttpCookie("path");
                miCookie.Value = path;
                Response.Cookies.Add(miCookie);
            }

            ayudante.construirPath(table_Path, Request.Cookies["path"].Value);

        }
    }
}