using AplicacionLogin.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Transbank.Webpay;

namespace AplicacionLogin.Controllers
{
    public class MotoController : Controller
    {
        // GET: Moto
        public ActionResult moto_pagina1()
        {
            ViewBag.Title = "Compensación de carbono para Moto";
            return View();
        }

        public ActionResult moto_pagina2()
        {
            ViewBag.Title = "Compensación de carbono para Moto";
            return View();
        }

        public ActionResult moto_pagina3(double recorrido, double toneladas, double total, string name, string mail)
        {
            ViewBag.Title = "Compensación de carbono para Moto";
            ViewBag.recorrido = recorrido;
            ViewBag.toneladas = toneladas;
            ViewBag.total = total;
            ViewBag.nombre = name;
            ViewBag.correo = mail;
    
            return View();
        }

        public ActionResult moto_pagina4(double porsentaje, double total, double ton, string name, string mail)
        {
            ViewBag.Title = "Compensación de carbono para Moto";
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
            ViewBag.Title = "Compensación de carbono para Moto";
            ViewBag.total = calculo;
            ViewBag.toneladas = ton;
           
            return View();
        }

        public ActionResult moto_pagina4_2(double calculo, double ton)
        {
            ViewBag.Title = "Compensación de carbono para Moto";
            ViewBag.total = calculo;
            ViewBag.toneladas = ton;
            Session["toneladas_moto"] = ton;




            //var configuration = new Configuration();
            //configuration.Environment = "PRODUCCION";
            //configuration.CommerceCode = "597036025948";
            //Conf.PrivateCertPfxPath = @"D:\home\site\wwwroot\Content\Cert\36025948.pfx";
            //configuration.PrivateCertPfxPath = @"C:\Users\limc_\source\repos\WebApplication6\WebApplication6\Content\Cert\597036025948.pfx";

            //configuration.Password = "1234";
            //configuration.WebpayCertPath = Configuration.GetProductionPublicCertPath();

            //Conf.WebpayCertPath = Configuration.GetProductionPublicCertPath();
            var transaction = new Webpay(Configuration.ForTestingWebpayPlusNormal()).NormalTransaction;

            //var transaction = new Webpay(configuration).NormalTransaction;    //.NormalTransaction;

            //Convert.ToInt16(calculo);


            //decimal valor = Convert.ToDecimal(calculo);
            var monto = Convert.ToInt32(calculo * 100);
            var orden = "1234567";
            var id = "1234456";

            string returnUrl = "http://localhost:62106/Moto/Retorno_moto";
            string returnFinal = "http://localhost:62106/Moto/Final_moto";

            var initResult = transaction.initTransaction(monto, orden, id, returnUrl, returnFinal);


            var tokenWs = initResult.token;
            var formAction = initResult.url;

            ViewBag.Monto = monto / 100;
            ViewBag.Orden = orden;
            ViewBag.token = tokenWs;
            ViewBag.form = formAction;
            return View();
        }

        public ActionResult moto_pagina4_paypal(double total, double ton)
        {
            ViewBag.Title = "Compensación de carbono para Moto";
            ViewBag.total = total;
            ViewBag.toneladas = ton;
            return View();
        }

        public ActionResult CalculosMoto(string kilometros, string cilindrada, string nombre, string correo)
        {
            ViewBag.Title = "Compensación de carbono para Moto";
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

        public ActionResult About_moto()
        {
            ViewBag.Message = "Your application description page.";

            ViewBag.Title = "Compensación de carbono para Moto";
            return View();


        }

        public ActionResult Contact_moto()
        {
            ViewBag.Message = "Your contact page.";
            ViewBag.Title = "Compensación de carbono para Moto";

            return View();


        }

        public ActionResult Final_moto()
        {
            ViewBag.Message = "Your contact page.";
            //ViewBag.toneladas = Response.Write Session("toneladas");
            //var ton = Session["toneladas"];
            ViewBag.tonelada = Session["toneladas_moto"];
            //ViewBag.tonelada = ton;

            ViewBag.Title = "Compensación de carbono para Moto";
            return View();


        }

        public ActionResult Retorno_moto()
        {
            ViewBag.Title = "Compensación de carbono para Moto";
            var transaction = new Webpay(Configuration.ForTestingWebpayPlusNormal()).NormalTransaction;
            string tokenWs = Request.Form["token_ws"];
            var result = transaction.getTransactionResult(tokenWs);

            var output = result.detailOutput[0];
            if (output.responseCode == 0)
            {
                ViewBag.redirect = result.urlRedirection;
                ViewBag.Token = tokenWs;
                ViewBag.Response = output.responseCode;
                ViewBag.monto = output.amount;
                ViewBag.moto = output.authorizationCode;

            }
            ViewBag.Message = "Your application description page.";

            return View();


        }
    }
}