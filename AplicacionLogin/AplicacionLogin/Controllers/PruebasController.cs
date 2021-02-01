using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AplicacionLogin.Controllers
{
    public class PruebasController : Controller
    {
        // GET: Pruebas
        public ActionResult formulario()
        {
            return View();
        }

        public ActionResult boton(double cantidad)
        {
            ViewBag.datoprueba = cantidad;
            return View();
        }
    }
}