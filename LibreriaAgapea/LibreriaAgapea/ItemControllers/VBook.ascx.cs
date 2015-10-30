using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LibreriaAgapea.App_Code.Models;

namespace LibreriaAgapea.ItemControllers
{
    public partial class VBook : System.Web.UI.UserControl
    {
        private string __title, __author, __type, __ISBN10, __ISBN13, __editorial;
        private double __price;
        private Image __image;

        public Image image
        {
            get
            {
                return __image;
            }
            set
            {
                __image = value;
                imgbutton_Book.ImageUrl = __image.ImageUrl;
            }
        }
        public string title
        {
            get
            {
                return __title;
            }
            set
            {
                __title = value;
                label_Title.Text = "Title: " + __title;
            }
        }

        public string author
        {
            get
            {
                return __author;
            }
            set
            {
                __author = value;
                label_Author.Text = "Author: " + __author;
            }
        }

        public string type
        {
            get
            {
                return __type;
            }
            set
            {
                __type = value;
                label_Type.Text = "Type: " + __type;
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

        public double price
        {
            get
            {
                return __price;
            }
            set
            {
                __price = value;
                label_Price.Text = "Price: " + __price + "€";
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

        public void createVBook(Book book)
        {
            title = book.titulo;
            author = book.autor;
            editorial = book.editorial;
            ISBN10 = book.ISBN10;
            ISBN13 = book.ISBN13;
            price = book.precio;
            type = book.categoria;
        }

        public Button getButton()
        {
            return button_Buy;
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}