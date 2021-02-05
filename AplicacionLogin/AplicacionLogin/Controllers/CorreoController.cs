using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AplicacionLogin.Controllers
{
    public class CorreoController : Controller
    {
        // GET: Correo
        public string Index()
        {
            Models.Correo.SendEmailAsync("lmonsalve22@gmail.com", "Luis Monsalve", "1200", "4.6");
            return "Hola";
        }
    }
}