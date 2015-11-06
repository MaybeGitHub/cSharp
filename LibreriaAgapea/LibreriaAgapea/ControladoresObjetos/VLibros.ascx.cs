using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LibreriaAgapea.App_Code.Modelos;

namespace LibreriaAgapea.ControladoresObjetos
{
    public partial class VLibros : System.Web.UI.UserControl
    {
        private string __titulo, __autor, __categoria, __ISBN10, __ISBN13, __editorial;
        private double __precio;
        private Image __imagen;

        public Image imagen
        {
            get
            {
                return __imagen;
            }
            set
            {
                __imagen = value;
                imgbutton_Libro.ImageUrl = __imagen.ImageUrl;
            }
        }
        public string titulo
        {
            get
            {
                return __titulo;
            }
            set
            {
                __titulo = value;
                label_Titulo.Text = "Title: " + __titulo;
            }
        }

        public string autor
        {
            get
            {
                return __autor;
            }
            set
            {
                __autor = value;
                label_Autor.Text = "Author: " + __autor;
            }
        }

        public string categoria
        {
            get
            {
                return __categoria;
            }
            set
            {
                __categoria = value;
                label_Categoria.Text = "Type: " + __categoria;
            }
        }

        public string ISBN10
        {
            get
            {
                return __ISBN10;
            }
            set
            {
                __ISBN10 = value;
                label_ISBN0.Text = "ISBN10: " + __ISBN10;
            }
        }

        public string ISBN13
        {
            get
            {
                return __ISBN13;
            }
            set
            {
                __ISBN13 = value;
                label_ISBN1.Text = "ISBN13: " + __ISBN13;
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
                label_Precio.Text = "Price: " + __precio + "€";
            }
        }

        public string editorial
        {
            get
            {
                return __editorial;
            }
            set
            {
                __editorial = value;
                label_Editorial.Text = "Editorial: " + __editorial;
            }
        }

        public void createVBook(Libro book)
        {
            titulo = book.titulo;
            autor = book.autor;
            editorial = book.editorial;
            ISBN10 = book.ISBN10;
            ISBN13 = book.ISBN13;
            precio = book.precio;
            categoria = book.categoria;
        }

        public Button getButton()
        {
            return button_Comprar;
        }

        public void mostrarBoton(bool mostrar)
        {
            if (!mostrar) button_Comprar.Visible = false; else button_Comprar.Visible = true;
        }
    }
}