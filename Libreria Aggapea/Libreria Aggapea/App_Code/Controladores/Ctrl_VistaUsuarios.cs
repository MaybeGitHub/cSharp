using System;
using System.Collections;
using System.Linq;
using System.Web;
using Libreria_Aggapea.App_Code.Modelos;
using Libreria_Aggapea.Herramientas;

namespace Libreria_Aggapea.App_Code.Controladores
{
    public class Ctrl_VistaUsuarios
    {
        private Ctrl_Ficheros ctrl_f = new Ctrl_Ficheros();
        private Tools tools = new Tools();

        public ArrayList listaUsuarios { get; set; }

        public Ctrl_VistaUsuarios()
        {
            listaUsuarios = new ArrayList();
            tools.rellenarList(listaUsuarios, "usuarios");
        }
        public void añadirUsuario(Usuario usuario)
        {
            ctrl_f.añadirNuevo(usuario.datos(), "usuarios");
            listaUsuarios.Add(usuario);
        }
    }
}