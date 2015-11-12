using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using LibreriaAgapea.App_Code.Controladores;
using LibreriaAgapea.App_Code.Modelos;
using System.IO;

namespace LibreriaAgapea.App_Code.Herramientas
{
    [Serializable]
    public class Ayudante
    {
        public void pintarCajaInfoPagina(TextBox cajaMultilinea, HttpContext datos)
        {
            string message = "";
            List<string> valores = (from string key in datos.Request.Params where (key != null) select key).ToList();
            foreach (string key in valores) message += "clave: " + key + " ---- valor:_" + datos.Request[key] + "\n";
            cajaMultilinea.Text = message;
        }

        public Cesta fabricaCesta(Usuario usuario)
        {
            Cesta cesta = new Cesta(usuario);
            string datosCestaUsuario = File.ReadAllLines(CFichero.rutaCestas).Where(linea => linea.Split(':')[1] == usuario.nombre && linea.Split(':')[0] == "1").SingleOrDefault();
            if ( datosCestaUsuario == null )
            {
                StreamWriter sw = new StreamWriter(new FileStream(CFichero.rutaCestas, FileMode.Append));
                sw.WriteLine(cesta.activa + ":" + usuario.nombre);
                sw.Close();
            }
            else {
                for (int i = 2; i < datosCestaUsuario.Split(':').Count(); i++)
                {                   
                    cesta.listaLibros.Add(fabricaLibros(datosCestaUsuario.Split(':')[i], false));
                }
            }
            return cesta;           
        }

        public Libro fabricaLibros(string ISBN, bool ISBN13)
        {
            int posicion = 3;
            if (ISBN13) posicion = 4;
            return File.ReadAllLines(CFichero.rutaLibros).Where(libro => libro.Split(':')[posicion] == ISBN).Select(libro => new Libro(libro.Split(':'))).SingleOrDefault();
        }

        public Usuario fabricaUsuario(string nombre)
        {         
            return File.ReadAllLines(CFichero.rutaUsuarios).Where(usuario => usuario.Split(':')[0] == nombre).Select(libro => new Usuario(libro.Split(':'))).SingleOrDefault();
        }

        public int librosRepetidos(Libro libro, List<Libro> listaLibros)
        {
            int cont = 0;
            foreach ( Libro libroLeido in listaLibros)
            {
                if (libroLeido.titulo == libro.titulo)
                {
                    cont++;
                }
            }
            return cont;
        }

        public string capitalizar(string frase)
        {
            return char.ToUpper(frase[0]) + frase.Substring(1);
        } 

        public void construirPath(Table tabla, string path) {
            tabla.Controls.Clear();
            TableRow fila = new TableRow();
            TableCell columna = null;
            string pagina;
            List<string> rutas = path.Split(':').ToList();           
            for (int i = 0; i < (rutas.Count()*2)-1; i++)
            {                
                if (i % 2 == 0) {
                    columna = new TableCell();
                    if (i == (rutas.Count() * 2) - 2)
                    {
                        Label label = new Label();
                        label.Text = capitalizar(rutas.ElementAt(i / 2));
                        columna.Controls.Add(label);
                    }
                    else
                    {                       
                        HyperLink link = new HyperLink();
                        pagina = capitalizar(rutas.ElementAt(i / 2));
                        link.NavigateUrl = pagina + ".aspx";
                        link.Text = pagina;
                        if ( pagina == "Inicio") link.NavigateUrl = "Centro.aspx";                                         
                        columna.Controls.Add(link);                        
                    }
                    fila.Cells.Add(columna);
                }
                else
                {
                    columna = new TableCell();
                    Label label = new Label();
                    label.Text = " > ";
                    columna.Controls.Add(label);
                    fila.Cells.Add(columna);
                }
            }            
            tabla.Rows.Add(fila);          
        }

        public IEqualityComparer<Libro> comparadorCategorias()
        {
            return new ComparadorCategorias();
        }

        public IEqualityComparer<Libro> comparadorTitulos()
        {
            return new ComparadorTitulos();
        }

        private class ComparadorTitulos : IEqualityComparer<Libro>
        {
            public bool Equals(Libro x, Libro y)
            {
                return x.titulo == y.titulo;
            }

            public int GetHashCode(Libro libro)
            {
                return libro.titulo.GetHashCode();
            }
        }

        private class ComparadorCategorias : IEqualityComparer<Libro>
        {
            public bool Equals(Libro x, Libro y)
            {
                return x.categoria == y.categoria;
            }

            public int GetHashCode(Libro libro)
            {
                return libro.categoria.GetHashCode();
            }
        }
    }
}