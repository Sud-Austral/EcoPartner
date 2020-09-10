using AplicacionLogin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AplicacionLogin.Controllers
{
    public class CamionController : Controller
    {
        // GET: Camion
        public ActionResult camion_pagina1()
        {
            return View();
        }

        public ActionResult camion_pagina2()
        {
            return View();
        }

        public ActionResult camion_pagina3(double recorrido, double toneladas, double total)
        {

            ViewBag.recorrido = recorrido;
            ViewBag.toneladas = toneladas;
            ViewBag.total = total;

            return View();
        }

        public ActionResult camion_pagina4()
        {
            return View();
        }

        public ActionResult CalculosCamion(string kilometros, string toneladasCarga)
        {
            Double num;
            bool isNum = Double.TryParse(kilometros, out num);
            bool isNum2 = Double.TryParse(toneladasCarga, out num);
            if (isNum)
            {
                if (kilometros != "" && Convert.ToDouble(kilometros) > 0 && toneladasCarga != "" && Convert.ToDouble(toneladasCarga) > 0)
                {
                    CALCULOS ca = new CALCULOS();
                    double carbono = ca.CalcularCO2Camion(kilometros, toneladasCarga);
                    double total = ca.CalcularValorCamion(carbono);

                    ViewBag.kilometros = kilometros;
                    ViewBag.carga = toneladasCarga;
                    ViewBag.carbono = carbono;
                    ViewBag.total = total;
                }
            }

            return View("camion_pagina2");
        }

    }
}