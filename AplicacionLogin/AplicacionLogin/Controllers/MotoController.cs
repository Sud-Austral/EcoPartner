using AplicacionLogin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AplicacionLogin.Controllers
{
    public class MotoController : Controller
    {
        // GET: Moto
        public ActionResult moto_pagina1()
        {
            return View();
        }

        public ActionResult moto_pagina2()
        {
            return View();
        }

        public ActionResult moto_pagina3()
        {
            return View();
        }

        public ActionResult moto_pagina4()
        {
            return View();
        }

        public ActionResult CalculosMoto(string kilometros, string cilindrada)
        {
            Double num;
            bool isNum = Double.TryParse(kilometros, out num);
            if (isNum)
            {
                if (kilometros != "" && Convert.ToDouble(kilometros) > 0)
                {
                    CALCULOS ca = new CALCULOS();
                    double litros = ca.CalcularLitrosMoto(kilometros, cilindrada);
                    double carbono = ca.CalcularCO2Moto(litros);
                    double total = ca.CalcularValorMoto(carbono);

                    ViewBag.kilometros = kilometros;
                    ViewBag.litros = litros;
                    ViewBag.carbono = carbono;
                    ViewBag.total = total;
                }
            }

            return View("moto_pagina1");
        }
    }
}