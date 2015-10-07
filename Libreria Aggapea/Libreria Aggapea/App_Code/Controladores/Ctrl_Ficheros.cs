using System;
using System.Linq;
using System.Web;
using System.IO;
using Libreria_Aggapea.App_Code.Modelos;

namespace Libreria_Aggapea.App_Code.Controladores
{
    public class Ctrl_Ficheros
    { 
        private string pathLibros = @"C:\Users\" + Environment.UserName + @"\Documents\GitHubVisualStudio\cSharp\Libreria Aggapea\Libreria Aggapea\App_Code\Ficheros\Libros.txt";
        private string pathUsuarios = @"C:\Users\" + Environment.UserName + @"\Documents\GitHubVisualStudio\cSharp\Libreria Aggapea\Libreria Aggapea\App_Code\Ficheros\Usuarios.txt";

        public void guardar(string objeto, string queEnPlural)
        {
            FileStream fs = null;
            if (queEnPlural.ToLower().Equals("libros"))
            {
                fs = new FileStream(pathLibros, FileMode.Append, FileAccess.Write, FileShare.Write);
            }

            if (queEnPlural.ToLower().Equals("usuarios"))
            {
                fs = new FileStream(pathUsuarios, FileMode.Append, FileAccess.Write, FileShare.Write);
            }

            StreamWriter sw = new StreamWriter(fs);
            sw.Write(objeto);
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
            return null;            
        }

        public void comprarLibro_ActualizarTxT(string valoresLibro)
        {
            string[] libros = File.ReadAllLines(pathLibros);
            string[] libro = valoresLibro.Split(':');
            for ( int i = 0; i < libros.Length; i++) {
                if (libros[i].Equals(valoresLibro))
                {
                    libro[8] = (int.Parse(libro[8]) + 1).ToString();
                    libro[9] = (int.Parse(libro[9]) - 1).ToString();
                    Libro recreoLibro = new Libro(libro);
                    libros[i] = recreoLibro.datos();
                }
            }
            File.WriteAllText(pathLibros, string.Empty);
            File.WriteAllLines (pathLibros, libros);
        }
    }
}