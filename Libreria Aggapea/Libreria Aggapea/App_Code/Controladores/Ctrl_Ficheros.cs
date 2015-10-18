using System;
using System.Linq;
using System.Web;
using System.IO;
using Libreria_Aggapea.App_Code.Modelos;
using System.Collections.Generic;

namespace Libreria_Aggapea.App_Code.Controladores
{
    public class Ctrl_Ficheros 
    { 
        public string rutaLibros = @"C:\Users\" + Environment.UserName + @"\Documents\GitHubVisualStudio\cSharp\Libreria Aggapea\Libreria Aggapea\App_Code\Ficheros\Libros.txt";
        public string rutaUsuarios = @"C:\Users\" + Environment.UserName + @"\Documents\GitHubVisualStudio\cSharp\Libreria Aggapea\Libreria Aggapea\App_Code\Ficheros\Usuarios.txt";
        public string rutaCestas = @"C:\Users\" + Environment.UserName + @"\Documents\GitHubVisualStudio\cSharp\Libreria Aggapea\Libreria Aggapea\App_Code\Ficheros\Cestas.txt";

        public void añadirUsuario(Usuario usuario)
        {
            StreamWriter sw = new StreamWriter( new FileStream(rutaUsuarios, FileMode.Append) );
            sw.Write(usuario.datos());
            sw.Close();
        }

        public void añadirLibro(Libro libro)
        {
            StreamWriter sw = new StreamWriter(new FileStream(rutaLibros, FileMode.Append));
            sw.Write(libro.datos());
            sw.Close();
        }

        public void añadirCesta(Cesta cesta)
        {
            StreamWriter sw = new StreamWriter(new FileStream(rutaCestas, FileMode.Append));
            sw.Write(cesta.datos());
            sw.Close();
        }

        public void actualizarLibro(Libro libro)
        {
            List<string> volcado = new List<string>(File.ReadAllLines(rutaLibros));

            for (int i = 0; i < volcado.Count; i++)
            {
                if ((volcado.ElementAt(i).Split(':')[0] == libro.titulo ))
                {
                    volcado.RemoveAt(i);
                    volcado.Insert(i, libro.datos());
                }
            }
           
            File.WriteAllText(rutaLibros, string.Empty);
            File.WriteAllLines(rutaLibros, volcado.Cast<string>());
        }

        public void actualizarCesta(Cesta cesta)
        {
            List<string> volcado = new List<string>(File.ReadAllLines(rutaCestas));
            for (int i = 0; i < volcado.Count; i++)
            {
                if (((string)volcado[i]).Split(':')[0] == cesta.dueño.nombre)
                {
                    volcado.RemoveAt(i);
                    volcado.Insert(i, cesta.datos());
                }
            }
            File.WriteAllText(rutaCestas, string.Empty);
            File.WriteAllLines(rutaCestas, volcado.Cast<string>());
        }

        public void añadirLibroTxTCesta(Usuario usuario, Libro libro)
        {
            string[] cestas = File.ReadAllLines(rutaCestas);
            for (int i = 0; i < cestas.Length; i++)
            {
                if (cestas[i].Split(':')[0].Equals(usuario.nombre))
                {
                    cestas[i] += ":" + libro.titulo;
                }
            }
            File.WriteAllText(rutaCestas, string.Empty);
            File.WriteAllLines(rutaCestas, cestas);
        }
    }
}