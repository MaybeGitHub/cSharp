﻿using System;
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
    public partial class Registro : System.Web.UI.Page
    {
        private Ayudante ayudante = new Ayudante();
        private CUsuario cU = new CUsuario();
        private Usuario usuario;

        protected void Page_Load(Object sender, EventArgs e)
        {
            text_Nombre.Focus();
            ayudante.construirPath((Table)Master.FindControl("table_Path"), "Inicio:Registro");
        }

        protected void registrar_boton_Click(object sender, ImageClickEventArgs e)
        {
            if (IsValid)
            {
                usuario = new Usuario(text_Nombre.Text, text_Apellido.Text, text_Contraseña.Text, text_Direccion.Text, text_Email.Text);
                CFichero.añadirUsuario(usuario);
                if (Request.Cookies["usuario"] != null)
                {
                    Request.Cookies["usuario"].Value = text_Nombre.Text;
                }
                else
                {
                    HttpCookie miCookie = new HttpCookie("usuario");
                    miCookie.Value = text_Nombre.Text;
                    Response.Cookies.Add(miCookie);
                }                
                Response.Redirect("Centro.aspx");
            }
        }

        protected void passLong_V_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (text_Contraseña2.Text.Length < 8) args.IsValid = false;
        }

        protected void almaCheck_V_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (alma_ChkBox.Checked == false) args.IsValid = false;
            alma_ChkBox.Checked = false;
        }

        protected void usuarioExiste_FV_ServerValidate(object source, ServerValidateEventArgs args)
        {
            if (cU.listaUsuarios.Where(usuario => usuario.nombre == text_Nombre.Text).Count() != 0) args.IsValid = false;
        }
    }
}