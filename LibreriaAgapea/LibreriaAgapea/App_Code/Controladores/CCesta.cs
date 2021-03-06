﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using LibreriaAgapea.App_Code.Modelos;
using LibreriaAgapea.App_Code.Herramientas;

namespace LibreriaAgapea.App_Code.Controladores
{
    public class CCesta
    {
        public List<Cesta> listaCestas { get; set; }

        public CCesta()
        {
            listaCestas.AddRange(File.ReadAllLines(CFichero.rutaCestas).Where(linea => linea.Split(':')[0] != "0").Select(linea => new Cesta(linea.Split(':'))));
        }
    }
}