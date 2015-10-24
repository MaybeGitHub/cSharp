using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Libreria_Aggapea.App_Code.Modelos;
using Libreria_Aggapea.Herramientas;
using System.IO;

namespace Libreria_Aggapea.App_Code.Controladores
{
    public class Ctrl_VistaUsuarios
    {
        private Ctrl_Ficheros ctrl_F = new Ctrl_Ficheros();
        public List<Usuario> usuarios = new List<Usuario>();
        private Tools tools = new Tools();

        public Ctrl_VistaUsuarios()
        {
            usuarios.AddRange(File.ReadAllLines(ctrl_F.rutaUsuarios).Select(linea => new Usuario(linea.Split(':'))).ToArray());
        }
    }
}