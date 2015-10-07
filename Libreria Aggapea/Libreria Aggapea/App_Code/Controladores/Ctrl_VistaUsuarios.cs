using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Libreria_Aggapea.App_Code.Modelos;

namespace Libreria_Aggapea.App_Code.Controladores
{
    public class Ctrl_VistaUsuarios
    {
        Ctrl_Ficheros ctrl_f = new Ctrl_Ficheros();
        public void meterUsuario(string usuario, string password)
        {
            Usuario user = new Usuario(usuario, password);
            ctrl_f.guardar(user.datos());
        }

        public Boolean validacionExistenciaUsuario(string user)
        {
            string[] listaUsuarios = ctrl_f.leer("usuarios");
            string nombre;
            Boolean esPosible = true;

            foreach( string usuario in listaUsuarios)
            {
                nombre = usuario.Split(':')[0];
                if ( nombre.ToLower().Equals(user.ToLower()) ){
                    esPosible = false;
                }
            }       
            return esPosible;
        }
    }
}