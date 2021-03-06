﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using LibreriaAgapea.App_Code.Modelos;
using LibreriaAgapea.App_Code.Herramientas;

namespace LibreriaAgapea.App_Code.Controladores
{    
    public class CUsuario
    {
        public List<Usuario> listaUsuarios = new List<Usuario>();

        public CUsuario()
        {
            listaUsuarios.AddRange(File.ReadAllLines(CFichero.rutaUsuarios).Select(linea => new Usuario(linea.Split(':'))));
        }

        public void meterEnCesta(Usuario usuario, Libro libro)
        {
            usuario.cesta.listaLibros.Add(libro);
            CFichero.sobrescribirCestasTxT(CFichero.rutaCestas, usuario.nombre, libro.ISBN10, true, false);            
        }

        public void sacarDeCesta(Usuario usuario, Libro libro, bool varios)
        {
            List<Libro> librosBorrar = new List<Libro>();
            foreach (Libro libroUsuario in usuario.cesta.listaLibros)
            {
                if ( libroUsuario.ISBN10 == libro.ISBN10)
                {
                    librosBorrar.Add(libroUsuario);
                    if ( !varios ) break;
                }
            }

            foreach(Libro libroUsuario in librosBorrar)
            {
                usuario.cesta.listaLibros.Remove(libroUsuario);
                CFichero.sobrescribirCestasTxT(CFichero.rutaCestas, usuario.nombre, libro.ISBN10, false, false);
            }
            
        }
    }
}