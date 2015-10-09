using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Libreria_Aggapea.App_Code.Modelos;

namespace Libreria_Aggapea.App_Code.Controladores
{
    public class Ctrl_VistaCesta
    {
        Ctrl_Ficheros ctrl_F = new Ctrl_Ficheros();
        List<Cesta> listaCestas;
        Cesta cestaElegida;

        public void crearNuevaCesta(string usuario, string datosLibro)
        {
            string[] usuarios = ctrl_F.leer("usuarios");
            string[] datosUsuario;

            foreach ( string user in usuarios )
            {
                datosUsuario = user.Split(':');
                if ( datosUsuario.Contains(usuario) )
                {
                    cestaElegida = new Cesta(new Usuario(datosUsuario));
                    cestaElegida.listaLibros.Add(new Libro(datosLibro.Split(':')));
                    listaCestas.Add( cestaElegida );
                    ctrl_F.guardar(usuario, "cestas");
                }
            }
        }

        public void añadirLibro(string user, string datosLibro)
        {
            Boolean encontrado = false;
            
            foreach (Cesta cesta in listaCestas)
            {
                if (cesta.user.usuario.Equals(user))
                {
                    encontrado = true;
                    cesta.listaLibros.Add(new Libro(datosLibro.Split(':')));
                }
            }

            if (!encontrado)
            {
                crearNuevaCesta(user, datosLibro);
            }
            
            ctrl_F.añadirLibroCesta(user, datosLibro.Split(':')[0]);
        }

    }
}