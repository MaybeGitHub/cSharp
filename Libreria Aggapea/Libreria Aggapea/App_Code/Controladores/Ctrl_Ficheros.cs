using System;
using System.Linq;
using System.Web;
using System.IO;
using Libreria_Aggapea.App_Code.Modelos;
using System.Collections;

namespace Libreria_Aggapea.App_Code.Controladores
{
    public class Ctrl_Ficheros 
    { 
        private string pathLibros = @"C:\Users\" + Environment.UserName + @"\Documents\GitHubVisualStudio\cSharp\Libreria Aggapea\Libreria Aggapea\App_Code\Ficheros\Libros.txt";
        private string pathUsuarios = @"C:\Users\" + Environment.UserName + @"\Documents\GitHubVisualStudio\cSharp\Libreria Aggapea\Libreria Aggapea\App_Code\Ficheros\Usuarios.txt";
        private string pathCestas = @"C:\Users\" + Environment.UserName + @"\Documents\GitHubVisualStudio\cSharp\Libreria Aggapea\Libreria Aggapea\App_Code\Ficheros\Cestas.txt";

        public void añadirNuevo(string datos, string dondeEnPlural)
        {
            FileStream fs = null;
            if (dondeEnPlural.ToLower().Equals("libros"))
            {
                fs = new FileStream(pathLibros, FileMode.Append, FileAccess.Write, FileShare.Write);
            }

            if (dondeEnPlural.ToLower().Equals("usuarios"))
            {
                fs = new FileStream(pathUsuarios, FileMode.Append, FileAccess.Write, FileShare.Write);
            }

            if (dondeEnPlural.ToLower().Equals("cestas"))
            {
                fs = new FileStream(pathCestas, FileMode.Append, FileAccess.Write, FileShare.Write);
            }

            StreamWriter sw = new StreamWriter(fs);
            sw.WriteLine(datos);
            sw.Close();
        }

        public string[] leer(string queEnPlural)
        {
            if (queEnPlural.ToLower().Equals("libros")){
                return File.ReadAllLines(pathLibros);
            }

            if (queEnPlural.ToLower().Equals("usuarios")){
                return File.ReadAllLines(pathUsuarios);
            }

            if (queEnPlural.ToLower().Equals("cestas"))
            {
                return File.ReadAllLines(pathCestas);
            }
            return null;            
        }       

        public void actualizarLibro(Libro libro)
        {
            ArrayList volcado = new ArrayList(leer("libros"));

            for (int i = 0; i < volcado.Count; i++)
            {
                if (((string)volcado[i]).Split(':')[0] == libro.titulo )
                {
                    volcado.RemoveAt(i);
                    volcado.Insert(i, libro.datos());
                }
            }
           
            File.WriteAllText(pathLibros, string.Empty);
            File.WriteAllLines(pathLibros, volcado.Cast<string>());
        }

        public void actualizarCesta(Cesta cesta)
        {
            ArrayList volcado = new ArrayList(leer("cestas"));
            for (int i = 0; i < volcado.Count; i++)
            {
                if (((string)volcado[i]).Split(':')[0] == cesta.dueño.nombre)
                {
                    volcado.RemoveAt(i);
                    volcado.Insert(i, cesta.datos());
                }
            }
            File.WriteAllText(pathCestas, string.Empty);
            File.WriteAllLines(pathCestas, volcado.Cast<string>());
        }

        public void añadirLibroTxTCesta(Usuario usuario, Libro libro)
        {
            string[] cestas = leer("cestas");
            for (int i = 0; i < cestas.Length; i++)
            {
                if (cestas[i].Split(':')[0].Equals(usuario.nombre))
                {
                    cestas[i] += ":" + libro.titulo;
                }
            }
            File.WriteAllText(pathCestas, string.Empty);
            File.WriteAllLines(pathCestas, cestas);
        }
    }
}