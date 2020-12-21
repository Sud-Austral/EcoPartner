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
    public class CamionController : Controller
    {
        // GET: Camion
        public ActionResult camion_pagina1()
        {
            ViewBag.Title = "Compensación de carbono para Camión";
            return View();
        }

        public ActionResult camion_pagina2()
        {
            ViewBag.Title = "Compensación de carbono para Camión";
            return View();
        }

        public ActionResult camion_pagina3(double recorrido, double toneladas, double total, string nombre, string correo)
        {
            ViewBag.Title = "Compensación de carbono para Camión";
            ViewBag.recorrido = recorrido;
            ViewBag.toneladas = toneladas;
            ViewBag.total = total;
            ViewBag.nombre = nombre;
            ViewBag.correo = correo;

            return View();
        }

        public ActionResult camion_pagina4(double porsentaje, double total, double ton, string nombre, string correo)
        {
            ViewBag.Title = "Compensación de carbono para Camión";
            CALCULOS ca = new CALCULOS();
            //double totalf = ca.Calculartotalf(porsentaje, total);
            double totalf = ca.Calculartotalf(total, porsentaje);

            ViewBag.totalf = totalf;
            ViewBag.toneladas = ton;
            ViewBag.nombre = nombre;
            ViewBag.correo = correo;

            return View();
        }

        public ActionResult camion_pagina4_1(double calculo, double ton, string nombre, string email, string telefono, string empresa, string pais)
        {
            ViewBag.Title = "Compensación de carbono para Camión";
            ViewBag.total = calculo;
       
            ViewBag.toneladas = ton;
            
            Session["nombre"] = nombre;
            Session["telefono"] = telefono;
            Session["empresa"] = empresa;
            Session["pais"] = pais;
            Session["correo"] = email;
            Session["toneladas"] = ton;
            Session["total"] = calculo;
            //Session["toneladas_camion"] = ton;
            var clp = calculo * 800;



            //*********************************************************************************
            //                                     Ambiente de producción
            //*********************************************************************************
            //var configuration = new Configuration();
            //configuration.Environment = "PRODUCCION";
            //configuration.CommerceCode = "597036300078";
            //configuration.PrivateCertPfxPath = @"D:\home\site\wwwroot\Content\Certificados\597036300078.pfx";

            //configuration.Password = "a";
            //configuration.WebpayCertPath = Configuration.GetProductionPublicCertPath();
            //var transaction = new Webpay(configuration).NormalTransaction;    //.NormalTransaction;
            //*********************************************************************************
            //                                     Ambiente de prueba
            //*********************************************************************************
            var transaction = new Webpay(Configuration.ForTestingWebpayPlusNormal()).NormalTransaction;




            //Convert.ToInt16(calculo);


            //decimal valor = Convert.ToDecimal(calculo);
            var monto = Convert.ToInt32(calculo * 100);
            var orden = Guid.NewGuid().ToString().Replace("-", string.Empty).Substring(0, 8);
            var id = Guid.NewGuid().ToString().Replace("-", string.Empty).Substring(0, 8);
            Session["id"] = id;

            string returnUrl = "http://localhost:62106/Camion/Retorno_camion";
            string returnFinal = "http://localhost:62106/Camion/Final_camion";
            //string returnUrl = "https://ecopartnerbank.azurewebsites.net/Camion/Retorno_camion";
            //string returnFinal = "https://ecopartnerbank.azurewebsites.net/Camion/Final_camion";

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

        public ActionResult CalculosCamion(string kilometros, string toneladasCarga, string nombre, string correo)
        {
            ViewBag.Title = "Compensación de carbono para Camión";
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
                    ViewBag.nombre = nombre;
                    ViewBag.correo = correo;
                }
            }

            return View("camion_pagina2");
        }

        public ActionResult About_camion()
        {
            ViewBag.Message = "Your application description page.";

            ViewBag.Title = "Compensación de carbono para Camión";
            return View();


        }

        public ActionResult Contact_camion()
        {
            ViewBag.Message = "Your contact page.";
            ViewBag.Title = "Compensación de carbono para Camión";

            return View();


        }

        public ActionResult Final_camion()
        {
            ViewBag.Title = "Compensación de carbono para Camión";
            ViewBag.Message = "Your contact page.";
            //ViewBag.toneladas = Response.Write Session("toneladas");
            //var ton = Session["toneladas"];
            // ViewBag.tonelada = Session["toneladas_camion"];
            //ViewBag.tonelada = ton;

            ViewBag.id = Session["id"];
            ViewBag.nombre = Session["nombre"];
            ViewBag.telefono = Session["telefono"];
            ViewBag.empresa = Session["empresa"];
            ViewBag.pais = Session["pais"];
            ViewBag.correo = Session["correo"];
            ViewBag.tonelada = Session["toneladas"];
            ViewBag.total = Session["total"];

            return View();
        }

        public ActionResult Retorno_camion()
        {
            ViewBag.Title = "Compensación de carbono para Camión";
            //*********************************************************************************
            //                                     Ambiente de producción
            //*********************************************************************************
            //var configuration = new Configuration();
            //configuration.Environment = "PRODUCCION";
            //configuration.CommerceCode = "597036300078";
            //configuration.PrivateCertPfxPath = @"D:\home\site\wwwroot\Content\Certificados\597036300078.pfx";

            //configuration.Password = "a";
            //configuration.WebpayCertPath = Configuration.GetProductionPublicCertPath();
            //var transaction = new Webpay(configuration).NormalTransaction;    //.NormalTransaction;
            //*********************************************************************************
            //                                     Ambiente de prueba
            //*********************************************************************************
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
                ViewBag.camion = output.authorizationCode;

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