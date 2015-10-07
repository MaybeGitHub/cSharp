using System;
using System.Linq;
using System.Web;
using System.IO;
using Libreria_Aggapea.App_Code.Modelos;

namespace Libreria_Aggapea.App_Code.Controladores
{
    public class Ctrl_Ficheros
    { 
        public void guardar(string objeto)
        {
            string path = @"C:\Users\" + Environment.UserName + @"\Documents\Visual Studio 2015\Projects\Libreria Aggapea\Libreria Aggapea\App_Code\Ficheros\Usuarios.txt";
            FileStream fs = new FileStream(path, FileMode.Append, FileAccess.Write, FileShare.Write);
            StreamWriter sw = new StreamWriter(fs);
            sw.Write(objeto);
            sw.Close();
        }

        public string[] leer(string queEnPlural)
        {
            string path = "";
            if (queEnPlural.ToLower().Equals("libros")){
                path = @"C:\Users\" + Environment.UserName + @"\Documents\Visual Studio 2015\Projects\Libreria Aggapea\Libreria Aggapea\App_Code\Ficheros\Libros.txt";
            }

            if (queEnPlural.ToLower().Equals("usuarios")){
                path = @"C:\Users\" + Environment.UserName + @"\Documents\Visual Studio 2015\Projects\Libreria Aggapea\Libreria Aggapea\App_Code\Ficheros\Usuarios.txt";
            }
            return File.ReadAllLines(path);
        }

        public void comprarLibro_ActualizarTxT(string valoresLibro)
        {
            string path = @"C:\Users\" + Environment.UserName + @"\Documents\Visual Studio 2015\Projects\Libreria Aggapea\Libreria Aggapea\App_Code\Ficheros\Libros.txt";
            string[] libros = File.ReadAllLines(path);
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
            File.WriteAllText(path, string.Empty);
            File.WriteAllLines (path, libros);
        }
    }
}