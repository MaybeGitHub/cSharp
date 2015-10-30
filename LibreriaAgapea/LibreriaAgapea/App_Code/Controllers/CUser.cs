using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using LibreriaAgapea.App_Code.Models;
using LibreriaAgapea.App_Code.Tools;

namespace LibreriaAgapea.App_Code.Controllers
{
    public class CUser
    {
        private CFile cF = new CFile();
        public List<User> usuarios = new List<User>();
        private Tool tools = new Tool();

        public CUser()
        {
            usuarios.AddRange(File.ReadAllLines(cF.rutaUsuarios).Select(linea => new User(linea.Split(':'))).ToArray());
        }
    }
}