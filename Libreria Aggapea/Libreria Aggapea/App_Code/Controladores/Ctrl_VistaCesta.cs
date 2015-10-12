using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Libreria_Aggapea.App_Code.Modelos;

namespace Libreria_Aggapea.App_Code.Controladores
{
    public class Ctrl_VistaCesta
    {
        Ctrl_Ficheros ctrl_F { get; set; }

        public Ctrl_VistaCesta()
        {
            ctrl_F = new Ctrl_Ficheros();
        }

        public void añadirLibroAlUsuario(string usuario, string datosLibro)
        {
            Boolean encontrado = false;
            string nombre;

            foreach (string cesta in ctrl_F.leer("cestas") )
            {
                nombre = cesta.Split(':')[0];
                if (nombre.Equals(usuario))
                {
                    encontrado = true;
                }
            }           

            if (!encontrado)
            {
                crearNuevaCesta(usuario);
            }
                 
            ctrl_F.añadirLibroCesta(usuario, datosLibro.Split(':')[0]);
            ctrl_F.actualizarTxTLibros(datosLibro);
        }

        public void crearNuevaCesta(string usuario)
        {
            ctrl_F.guardar(usuario, "cestas");
        }
    }
}