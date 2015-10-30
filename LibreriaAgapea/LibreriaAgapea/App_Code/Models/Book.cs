using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace LibreriaAgapea.App_Code.Models
{
    public class Book
    {
        public string titulo { get; set; }
        public string autor { get; set; }
        public string editorial { get; set; }
        public string ISBN10 { get; set; }
        public string ISBN13 { get; set; }
        public string resumen { get; set; }
        public string categoria { get; set; }
        public double precio { get; set; }

        private string[] atributos = new string[] { "titulo", "autor", "editorial", "ISBN10", "ISBN13", "resumen", "categoria", "precio" };
        private List<string> valores = new List<string>();

        public Book(string[] datosLibro)
        {
            this.titulo = datosLibro[0];
            this.autor = datosLibro[1];
            this.editorial = datosLibro[2];
            this.ISBN10 = datosLibro[3];
            this.ISBN13 = datosLibro[4];
            this.resumen = datosLibro[5];
            this.categoria = datosLibro[6];
            this.precio = double.Parse(datosLibro[7]);

            valores.AddRange(datosLibro);
        }

        public string datos()
        {
            string ret = "";
            for (int i = 0; i < atributos.Length; i++) ret += valores.ToArray()[i] + (i < atributos.Length - 1 ? ":" : "");
            return ret;
        }

        public List<Label> generarLabels()
        {
            List<Label> labels = new List<Label>();
            Label label;

            for (int i = 0; i < atributos.Length; i++)
            {
                label = new Label();
                label.Text = atributos[i].ToUpper() + ": " + valores.ToArray()[i] + (atributos[i].Equals("precio") ? " €" : "");
                label.Style.Add("display", "block");
                labels.Add(label);
            }

            return labels;
        }
    }
}