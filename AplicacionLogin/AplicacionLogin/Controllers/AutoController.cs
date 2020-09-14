using AplicacionLogin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AplicacionLogin.Controllers
{
    public class AutoController : Controller
    {
        // GET: Auto
        public ActionResult auto_pagina1()
        {
            return View();
        }

        public ActionResult auto_pagina2()
        {
            return View();
        }

        public ActionResult auto_pagina3(double recorrido, double toneladas, double total)
        {

            ViewBag.recorrido = recorrido;
            ViewBag.toneladas = toneladas;
            ViewBag.total = total;

            return View();
        }

        

        public ActionResult auto_pagina4(double porsentaje, double total)
        {
            CALCULOS ca = new CALCULOS();
            //double totalf = ca.Calculartotalf(porsentaje, total);
            double totalf = ca.Calculartotalf(total, porsentaje);

            ViewBag.totalf = totalf;

            return View();
        }

        public ActionResult CalculosAuto(string kilometros, string tipoAuto, string tipoCombustible)
        {
            Double num;
            bool isNum = Double.TryParse(kilometros, out num);
            if (isNum)
            {
                if (kilometros != "" && Convert.ToDouble(kilometros) > 0)
                {
                    CALCULOS ca = new CALCULOS();
                    double litros = ca.CalcularLitrosAuto(kilometros, tipoAuto);
                    double carbono = ca.CalcularCO2Auto(litros, tipoCombustible);
                    double total = ca.CalcularValorAuto(carbono);

                    ViewBag.kilometros = kilometros;
                    ViewBag.litros = litros;
                    ViewBag.carbono = carbono;
                    ViewBag.total = total;
                }
            }


            return View("auto_pagina2");
        }
    }
}