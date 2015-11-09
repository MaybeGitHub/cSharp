using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LibreriaAgapea.App_Code.Modelos;

namespace LibreriaAgapea.ControladoresObjetos
{
    public partial class VCarrito : System.Web.UI.UserControl
    {
        private string __titulo;
        private int __cantidad;
        private double __precio;

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
                label_Cantidad.Text = __cantidad.ToString();
            }
        }

        public double precio
        {
            get
            {
                return __precio;
            }
            set
            {
                __precio = value;
                label_Precio.Text = __precio.ToString() + " €";
            }
        }

        public void crearCarrito(Libro libro)
        {
            titulo = libro.titulo;
            precio = libro.precio;
            imgButton_Borrar.ID = imgButton_Borrar.ID + "$" + libro.ISBN10;
            imgButton_Up.ID = imgButton_Up.ID + "$" + libro.ISBN10;
            imgButton_Down.ID = imgButton_Down.ID + "$" + libro.ISBN10;
        }

        public void ponerCantidad( int cantidad)
        {
            this.cantidad = cantidad;
        }        
    }
}