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
    public class AutoController : Controller
    {
        // GET: Auto
        public ActionResult auto_pagina1()
        {
            ViewBag.Title = "Compensación de carbono para Autos";
            return View();
        }


        public ActionResult auto_pagina2()
        {
            ViewBag.Title = "Compensación de carbono para Autos";
            return View();
        }


        public ActionResult auto_pagina3(double recorrido, double toneladas, double total, string nombre, string correo)
        {
            ViewBag.Title = "Compensación de carbono para Autos";
            ViewBag.recorrido = recorrido;
            ViewBag.toneladas = toneladas;
            ViewBag.total = total;
            ViewBag.nombre = nombre;
            ViewBag.correo = correo;
            return View();
        }



        public ActionResult auto_pagina4(double porsentaje, double total, double ton, string nombre, string correo)
        {
            ViewBag.Title = "Compensación de carbono para Autos";
            CALCULOS ca = new CALCULOS();
            //double totalf = ca.Calculartotalf(porsentaje, total);
            double totalf = ca.Calculartotalf(total, porsentaje);
            ViewBag.toneladas = ton;
            ViewBag.totalf = totalf;
            ViewBag.nombre = nombre;
            ViewBag.correo = correo;
            return View();
        }
        public ActionResult indexauto()
        {
            ViewBag.Title = "Compensación de carbono para Autos";
            return View();
        }

        public ActionResult auto_pagina4_1(double calculo, double ton)
        {
            ViewBag.Title = "Compensación de carbono para Autos";
            ViewBag.total = calculo;
            ViewBag.toneladas = ton;
            Session["toneladas"] = ton;




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
            var orden = Guid.NewGuid().ToString().Replace("-", string.Empty).Substring(0, 8);
            var id = Guid.NewGuid().ToString().Replace("-", string.Empty).Substring(0, 8);

           //string returnUrl = "http://localhost:62106/Auto/Retorno";
           //string returnFinal = "http://localhost:62106/Auto/Final";
           string returnUrl = "https://ecopartnerbank.azurewebsites.net/Auto/Retorno";
           string returnFinal = "https://ecopartnerbank.azurewebsites.net/Auto/Final";
            int montotrans = Convert.ToInt32(calculo * 800); 
            var initResult = transaction.initTransaction(montotrans, orden, id, returnUrl, returnFinal);


            var tokenWs = initResult.token;
            var formAction = initResult.url;
            ViewBag.Montotrans = montotrans;
            ViewBag.Monto = monto / 100;
            ViewBag.Orden = orden;
            ViewBag.token = tokenWs;
            ViewBag.form = formAction;


            return View();

           


        }


        public ActionResult CalculosAuto(string kilometros, string tipoAuto, string tipoCombustible, string nombre, string correo )
        {
            ViewBag.Title = "Compensación de carbono para Autos";
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

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";


            return View();


        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";


            return View();


        }

        public ActionResult Final()
        {
            ViewBag.Title = "Compensación de carbono para Autos";
            ViewBag.Message = "Your contact page.";
            //ViewBag.toneladas = Response.Write Session("toneladas");
            //var ton = Session["toneladas"];
            ViewBag.tonelada = Session["toneladas"];
            //ViewBag.tonelada = ton;


            return View();


        }

        public ActionResult Index()
        {


            return View();

        }

        public ActionResult prueba()
        {


            return View();


        }

        public ActionResult Retorno()
        {
            ViewBag.Title = "Compensación de carbono para Autos";
            var transaction = new Webpay(Configuration.ForTestingWebpayPlusNormal()).NormalTransaction;
            string tokenWs = Request.Form["token_ws"];
            var result = transaction.getTransactionResult(tokenWs);

            var output = result.detailOutput[0];
            int aux = output.responseCode;
            if(output.responseCode == 0)
            {
                ViewBag.redirect = result.urlRedirection;
                ViewBag.Token = tokenWs;
                ViewBag.Response = output.responseCode;
                ViewBag.monto = output.amount;
                ViewBag.auto = output.authorizationCode;

            }
            
            else
            {
                ViewBag.redirect = result.urlRedirection;
                ViewBag.monto = "La transacción fue rechazada";
                ViewBag.Token = tokenWs;
                return View("Error");
            }
            ViewBag.Message = "Your application description page.";
            
            return View();


        }
        public ActionResult Error()
        {
            return View();
        }
        public ActionResult ErrorT()
        {
            return View();
        }

    }
}