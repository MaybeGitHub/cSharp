using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using LibreriaAgapea.App_Code.Models;
using LibreriaAgapea.App_Code.Tools;
using LibreriaAgapea.App_Code.Controllers;

namespace LibreriaAgapea.App_Code.Controllers
{
    public class CCart
    {
        private CFile cF = new CFile();
        private Tool tools = new Tool();
        public List<Cart> cestas { get; set; }

        public CCart()
        {
            cestas = File.ReadAllLines(cF.rutaCestas).Select(linea => new Cart(linea.Split(':'))).ToList();
        }

        public void actualizarCesta(User usuario, Book libroBorrar)
        {
            foreach (Cart cesta in cestas)
            {
                if (cesta.dueño.nombre == usuario.nombre)
                {
                    foreach (Book libro in cesta.listaLibros)
                        if (libro.titulo == libroBorrar.titulo)
                        {
                            cesta.listaLibros.Remove(libro);
                            cF.actualizarCesta(cesta);
                            break;
                        }
                }
            }
        }

        public void añadirLibroCestaUsuario(User usuario, Book libro)
        {
            foreach (Cart cesta in cestas)
            {
                if (cesta.dueño == usuario)
                {
                    cesta.listaLibros.Add(libro);
                    break;
                }
            }

            cF.añadirLibroTxTCesta(usuario, libro);
        }

        public void comprobarCesta(User usuario)
        {
            if (cestas.Where(cesta => cesta.dueño == usuario).Select(cesta => cesta).Count() == 0)
            {
                cestas.Add(new Cart(usuario));
            }
        }
    }
}