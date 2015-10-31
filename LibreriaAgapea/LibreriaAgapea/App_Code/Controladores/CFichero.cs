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
            sw.Write(usuario.datos());
            sw.Close();
        }

        public static void sobrescribirTxt(string path, string datoUnico, string dato, bool meterDato)
        {
            List<string> datos = File.ReadAllLines(path).ToList();
            string lineaSeleccionada = datos.Where(linea => linea.Split(':')[0] == datoUnico).SingleOrDefault();
            int posicion = datos.IndexOf(lineaSeleccionada);
            if (meterDato) {
                lineaSeleccionada += ":" + dato;
            } else {
                List<string> datosLinea = lineaSeleccionada.Split(':').ToList();
                foreach (string elemento in datosLinea ) {
                    if ( elemento == dato )
                    {
                        datosLinea.Remove(elemento);
                        break;
                    }
                }
                lineaSeleccionada = "";
                for(int i = 0; i < datosLinea.Count(); i++)
                {
                    lineaSeleccionada += datosLinea.ElementAt(i) + (i < datosLinea.Count() - 1 ? ":" : "");
                }
            }
            datos.RemoveAt(posicion);
            datos.Insert(posicion, lineaSeleccionada);
            StreamWriter sw = new StreamWriter( File.Create(path) );
            foreach (string linea in datos)
            {
                sw.WriteLine(linea);
            }
            sw.Close();            
        }
    }
}