using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using LibreriaAgapea.App_Code.Modelos;

namespace LibreriaAgapea.App_Code.Controladores
{
    public class CFichero
    {
        public static string rutaLibros = @"C:/Users/" + Environment.UserName + @"\Documents\GitHubVisualStudio\cSharp\LibreriaAgapea\LibreriaAgapea\App_Code\Ficheros\Libros.txt";
        public static string rutaUsuarios = @"C:/Users/" + Environment.UserName + @"\Documents\GitHubVisualStudio\cSharp\LibreriaAgapea\LibreriaAgapea\App_Code\Ficheros\Usuarios.txt";
        public static string rutaCestas = @"C:/Users/" + Environment.UserName + @"\Documents\GitHubVisualStudio\cSharp\LibreriaAgapea\LibreriaAgapea\App_Code\Ficheros\Cestas.txt";

        public static void añadirUsuario(Usuario usuario)
        {
            StreamWriter sw = new StreamWriter(new FileStream(rutaUsuarios, FileMode.Append));
            sw.WriteLine(usuario.datos());
            sw.Close();
        }

        public static void sobrescribirTxt(string path, string clave, string dato, bool meterDato, bool remplazarDato)
        {
            List<string> lineasFichero = File.ReadAllLines(path).ToList();
            string lineaDeseada = lineasFichero.Where(linea => linea.Split(':')[1] == clave && linea.Split(':')[0] == "1").SingleOrDefault();
            int posicion = lineasFichero.IndexOf(lineaDeseada);
            if (meterDato)
            {
                lineaDeseada += ":" + dato;
            }
            else
            {
                List<string> datosLinea = lineaDeseada.Split(':').ToList();

                if (remplazarDato)
                {
                    datosLinea.RemoveAt(0);
                    datosLinea.Insert(0, dato);
                }
                else
                {
                    foreach (string elemento in datosLinea)
                    {
                        if (elemento == dato)
                        {
                            datosLinea.Remove(elemento);
                            break;
                        }
                    }
                }

                lineaDeseada = "";
                for(int i = 0; i < datosLinea.Count(); i++)
                {
                    lineaDeseada += datosLinea.ElementAt(i) + (i < datosLinea.Count() - 1 ? ":" : "");
                }
            }
            lineasFichero.RemoveAt(posicion);
            lineasFichero.Insert(posicion, lineaDeseada);
            StreamWriter sw = new StreamWriter( File.Create(path) );
            foreach (string linea in lineasFichero)
            {
                sw.WriteLine(linea);
            }
            sw.Close();            
        }
    }
}