using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace LibreriaAgapea.ControladoresObjetos
{
    public partial class VCestas : System.Web.UI.UserControl
    {
        private string __titulo;
        private int __cantidad = 0;

        public string titulo
        {
            get
            {
                return __titulo;
            }

            set
            {
                __titulo = value;
                label_Titulo.Text = __titulo;
            }
        }

        public int cantidad
        {
            get
            {
                return __cantidad;
            }

            set
            {
                __cantidad = value;
                label_Cantidad.Text = "x " + __cantidad;
            }
        }

        public void crearVCestas(string tituloLibro)
        {
            titulo = tituloLibro;
        }

        public void aumentarContador()
        {
            cantidad += 1;
        }

        public Button getButton()
        {
            return button_Borrar;
        }        
    }
}