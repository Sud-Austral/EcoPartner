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

        public ActionResult moto_pagina3(double recorrido, double toneladas, double total, string name, string mail)
        {

            ViewBag.recorrido = recorrido;
            ViewBag.toneladas = toneladas;
            ViewBag.total = total;
            ViewBag.nombre = name;
            ViewBag.correo = mail;
    
            return View();
        }

        public ActionResult moto_pagina4(double porsentaje, double total, double ton, string name, string mail)
        {
            CALCULOS ca = new CALCULOS();
            //double totalf = ca.Calculartotalf(porsentaje, total);
            double totalf = ca.Calculartotalf(total, porsentaje);

            ViewBag.totalf = totalf;
            ViewBag.toneladas = ton;
            ViewBag.nombre = name;
            ViewBag.correo = mail;

            return View();
        }

        public ActionResult moto_pagina4_1(double calculo, double ton)
        {
            ViewBag.total = calculo;
            ViewBag.toneladas = ton;
            return View();
        }

        public ActionResult moto_pagina4_2(double calculo, double ton)
        {
            ViewBag.total = calculo;
            ViewBag.toneladas = ton;
            return View();
        }

        public ActionResult moto_pagina4_paypal(double total, double ton)
        {
            ViewBag.total = total;
            ViewBag.toneladas = ton;
            return View();
        }

        public ActionResult CalculosMoto(string kilometros, string cilindrada, string nombre, string correo)
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
                    ViewBag.nombre = nombre;
                    ViewBag.correo = correo;
                }
            }

            return View("moto_pagina2");
        }
    }
}