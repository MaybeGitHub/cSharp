using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Libreria_Aggapea.App_Code.Controladores;

namespace Libreria_Aggapea.Vistas
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        private Ctrl_Ficheros ctrl_F = new Ctrl_Ficheros();
        private Ctrl_VistaLibros ctrl_VL = new Ctrl_VistaLibros();

        protected void Page_Load(object sender, EventArgs e)
        {
            //----------------------------------------------------------
            string __message = "", __valor = "";
            foreach (string key in this.Context.Request.Params)
            {
                if (this.Context.Request[key] == null) { __valor = "null"; } else { __valor = this.Context.Request[key].ToString(); };
                __message += "clave: " + key + " ---- valor:_" + __valor + "\n";
            }
            this.TextBox1.Text = __message;
            //----------------------------------------------------------

            welcomeUsuario.Text = "Bienvenido de nuevo, " + this.Request.QueryString["usuario"].ToString();
                  
            generarTabla();
        }

        private void generarTabla()
        {
            double librosRestantes = (double)ctrl_VL.libros.Length;
            int filasNecesarias = (int)Math.Ceiling(librosRestantes / 3);
            TableCell columnActual;
            TableRow rowActual;

            for (int f = 0; f < filasNecesarias; f++)
            {
                rowActual = new TableRow();
                expositor_libros.Rows.Add(rowActual);
                for (int c = 0; c < 3 && librosRestantes > 0; c++)
                {
                    columnActual = new TableCell();
                    columnActual.ControlStyle.BorderColor = System.Drawing.Color.Black;
                    columnActual.ControlStyle.BorderStyle = BorderStyle.Solid;
                    columnActual.Style.Add("padding", "10px");
                    ctrl_VL.construirPanelLibro(columnActual, f, c, comprarLibro);
                    librosRestantes--;
                    rowActual.Cells.Add(columnActual);
                }
            }
        }

        private void comprarLibro(object sender, EventArgs e)
        {
            ctrl_F.comprarLibro_ActualizarTxT(ctrl_VL.mapeoBotones[(Button)sender]);
            Response.Redirect(Request.RawUrl);
        }
    }
}