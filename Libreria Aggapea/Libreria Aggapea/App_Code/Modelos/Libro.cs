using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Libreria_Aggapea.App_Code.Modelos
{
    public class Libro
    {
        public string nombre { get; set; }
        public string autor { get; set; }
        public string editorial { get; set; }
        public string ISBN10 { get; set; }
        public string ISBN13 { get; set; }
        public string resumen { get; set; }
        public string categoria { get; set; }
        public double precio { get; set; }
        public int vendidos { get; set; }
        public int stock { get; set; }

        private string[] atributos = new string[] { "nombre", "autor", "editorial", "ISBN10", "ISBN13", "resumen", "categoria", "precio", "vendidos", "stock" };
        private List<string> valores = new List<string>();

        public Libro(string[] datosLibro)
        {          
            this.nombre = datosLibro[0];
            this.autor = datosLibro[1]; 
            this.editorial = datosLibro[2]; 
            this.ISBN10 = datosLibro[3]; 
            this.ISBN13 = datosLibro[4]; 
            this.resumen = datosLibro[5]; 
            this.categoria = datosLibro[6]; 
            this.precio = double.Parse(datosLibro[7]); 
            this.vendidos = int.Parse(datosLibro[8]); 
            this.stock = int.Parse(datosLibro[9]);

            valores.AddRange(datosLibro);
        }

        public string datos()
        {
            string ret = "";
            for ( int i = 0; i < atributos.Length; i++ )
            {
                ret += valores.ToArray()[i] + ( i < atributos.Length-1 ? ":": "");
            }
            return ret;
        }

        public List<Label> generarLabels()
        {
            List<Label> labels = new List<Label>();
            Label label;

            for (int i = 0; i < atributos.Length; i++) { 
                label = new Label();
                label.Text = atributos[i].ToUpper() + ": " + valores.ToArray()[i] + (atributos[i].Equals("precio") ? " €": "");
                label.Style.Add("display", "block");
                labels.Add(label);
            }

            return labels;
        }
    }
}