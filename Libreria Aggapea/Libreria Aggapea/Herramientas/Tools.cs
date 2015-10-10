using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace Libreria_Aggapea.Herramientas
{
    public class Tools
    {

        public void pintarCajaInfoPagina( TextBox cajaMultilinea, HttpContext datos )
        {
            string message = "", valor = "";
            foreach (string key in datos.Request.Params)
            {
                if (datos.Request[key] == null) { valor = "null"; } else { valor = datos.Request[key].ToString(); };
                message += "clave: " + key + " ---- valor:_" + valor + "\n";
            }
            cajaMultilinea.Text = message;
            
        }
    }
}
