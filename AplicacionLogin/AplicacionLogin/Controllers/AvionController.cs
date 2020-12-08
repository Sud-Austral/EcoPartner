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
    public class AvionController : Controller
    {
        // GET: Avion
        public ActionResult Avion_pagina1()
        {
            ViewBag.Title = "Compensación de carbono para Avión";
            return View();
        }

        public ActionResult Avion_pagina()
        {
            ViewBag.Title = "Compensación de carbono para Avión";
            return View();
        }

        public ActionResult Avion_pagina2()
        {
            ViewBag.Title = "Compensación de carbono para Avión";
            return View();
        }

        public ActionResult Avion_pagina3(double recorrido, double toneladas, double total, string nombre, string correo)
        {
            ViewBag.Title = "Compensación de carbono para Avión";
            ViewBag.recorrido = recorrido;
            ViewBag.toneladas = toneladas;
            ViewBag.total = total;
            ViewBag.nombre = nombre;
            ViewBag.correo = correo;

            return View();
        }

        public ActionResult Avion_pagina4(double porsentaje, double total, double ton, string nombre, string correo)
        {
            ViewBag.Title = "Compensación de carbono para Avión";
            CALCULOS ca = new CALCULOS();
            //double totalf = ca.Calculartotalf(porsentaje, total);
            double totalf = ca.Calculartotalf(total, porsentaje);
            ViewBag.toneladas = ton;
            ViewBag.totalf = totalf;
            ViewBag.nombre = nombre;
            ViewBag.correo = correo;
            return View();
        }

        public ActionResult Avion_pagina4_1(double calculo, double ton)
        {
            ViewBag.Title = "Compensación de carbono para Avión";
            ViewBag.total = calculo;
            ViewBag.toneladas = ton;
            Session["toneladas_avion"] = ton;




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

            string returnUrl = "http://localhost:62106/Avion/Retorno_avion";
            string returnFinal = "http://localhost:62106/Avion/Final_avion";
            //  string returnUrl = "https://ecopartnerbank.azurewebsites.net/Avion/Retorno_avion";
            //  string returnFinal = "https://ecopartnerbank.azurewebsites.net/Avion/Final_avion";

            int montotrans = Convert.ToInt32(calculo * 800);
            var initResult = transaction.initTransaction(montotrans, orden, id, returnUrl, returnFinal);


            var tokenWs = initResult.token;
            var formAction = initResult.url;

            ViewBag.Monto = monto / 100;
            ViewBag.Orden = orden;
            ViewBag.token = tokenWs;
            ViewBag.form = formAction;
            return View();
        }

        public ActionResult BuscarAeropuertos(string inicio, string destino, int recorrido, int pasajeros, int tipoViaje, string nombre, string correo)
        {
            ViewBag.Title = "Compensación de carbono para Avión";
            AEROPUERTO p1 = new AEROPUERTO();
            AEROPUERTO p2 = new AEROPUERTO();
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create("https://raw.githubusercontent.com/Sud-Austral/Calculadora/master/BaseDatos/airports.dat");
            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
            var reader = new StreamReader(resp.GetResponseStream());

            List<string> listA = new List<string>();
            List<string> listB = new List<string>();
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var values = line.Split(',');
                string values_r = values[1].Replace("\"", "");

                if (inicio == values_r)
                {
                    p1.LATITUD = Convert.ToDouble(values[6]);
                    p1.LONGITUD = Convert.ToDouble(values[7]);

                }
                if (destino == values_r)
                {
                    p2.LATITUD = Convert.ToDouble(values[6]);
                    p2.LONGITUD = Convert.ToDouble(values[7]);

                }
            }

            CALCULOS ca = new CALCULOS();
            double distancia = ca.CalcularDistancia(p1, p2);
            double carbono1 = ca.CalcularCO2(distancia);
            double carbono2 = ca.CalculosAdicionales(carbono1, recorrido, pasajeros, tipoViaje);
            double total = ca.CalcularValor(carbono2);

            ViewBag.inicio = inicio;
            ViewBag.destino = destino;
            ViewBag.distancia = Math.Round(distancia, 2);
            ViewBag.carbono = carbono2;
            ViewBag.total = total;
            ViewBag.nombre = nombre;
            ViewBag.correo = correo;

            return View("Avion_pagina2");
        }

        public ActionResult About_avion()
        {
            ViewBag.Message = "Your application description page.";

            ViewBag.Title = "Compensación de carbono para Avión";
            return View();


        }

        public ActionResult Contact_avion()
        {
            ViewBag.Message = "Your contact page.";
            ViewBag.Title = "Compensación de carbono para Avión";

            return View();


        }

        public ActionResult Final_avion()
        {
            ViewBag.Title = "Compensación de carbono para Avión";
            ViewBag.Message = "Your contact page.";
            //ViewBag.toneladas = Response.Write Session("toneladas");
            //var ton = Session["toneladas"];
            ViewBag.tonelada = Session["toneladas_avion"];
            //ViewBag.tonelada = ton;


            return View();


        }

        public ActionResult Retorno_avion()
        {
            ViewBag.Title = "Compensación de carbono para Avión";
            var transaction = new Webpay(Configuration.ForTestingWebpayPlusNormal()).NormalTransaction;
            string tokenWs = Request.Form["token_ws"];
            var result = transaction.getTransactionResult(tokenWs);

            var output = result.detailOutput[0];
            int aux = output.responseCode;
            if (output.responseCode == 0)
            {
                ViewBag.redirect = result.urlRedirection;
                ViewBag.Token = tokenWs;
                ViewBag.Response = output.responseCode;
                ViewBag.monto = output.amount;
                ViewBag.avion = output.authorizationCode;

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