using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LibreriaAgapea.App_Code.Models;

namespace LibreriaAgapea.ItemControllers
{
    public partial class VCarts : System.Web.UI.UserControl
    {
        private string __title;
        private double __count = 0;

        public string title
        {
            get
            {
                return __title;
            }

            set
            {
                __title = value;
                label_Title.Text = __title;
            }
        }

        public double count
        {
            get
            {
                return __count;
            }

            set
            {
                __count = value;
                label_Count.Text = "x " + __count;
            }
        }

        public void createVCarts(string bookTitle)
        {
            title = bookTitle;
        }

        public void addCount()
        {
            count += 1;
        }

        public Button getButton()
        {
            return button_Erase;
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}