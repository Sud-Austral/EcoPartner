using AplicacionLogin.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Transbank.Webpay;

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

        public ActionResult auto_pagina3(double recorrido, double toneladas, double total, string nombre, string correo)
        {

            ViewBag.recorrido = recorrido;
            ViewBag.toneladas = toneladas;
            ViewBag.total = total;
            ViewBag.nombre = nombre;
            ViewBag.correo = correo;

            return View();
        }



        public ActionResult auto_pagina4(double porsentaje, double total, double ton, string nombre, string correo)
        {
            CALCULOS ca = new CALCULOS();
            //double totalf = ca.Calculartotalf(porsentaje, total);
            double totalf = ca.Calculartotalf(total, porsentaje);
            ViewBag.toneladas = ton;
            ViewBag.totalf = totalf;
            ViewBag.nombre = nombre;
            ViewBag.correo = correo;

            return View();
        }

        public ActionResult auto_pagina4_1(double calculo, double ton)
        {
            ViewBag.total = calculo;
            ViewBag.toneladas = ton;


            var transaction = new Webpay(Transbank.Webpay.Configuration.ForTestingWebpayPlusNormal()).NormalTransaction;

            var monto = 10;
            var orden = "primera orden";
            var secion = "dies";

            string paguno = "http://localhost:62106/Auto/return";
            string pagdos = "http://localhost:62106/Auto/final";

            var initResult = transaction.initTransaction(monto, orden, secion, paguno, pagdos);

            var tokenWs = initResult.token;
            var formAction = initResult.url;

            ViewBag.Monto = monto;
            ViewBag.BuyOrder = orden;
            ViewBag.Tokenws = tokenWs;
            ViewBag.Formaction = formAction;
            return View();


        }

        public ActionResult CalculosAuto(string kilometros, string tipoAuto, string tipoCombustible, string nombre, string correo )
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
                    ViewBag.nombre = nombre;
                    ViewBag.correo = correo;
                }
            }

           


            return View("auto_pagina2");
        }
    }
}