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
        private ecopartnerEntities db = new ecopartnerEntities();
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

        public ActionResult moto_pagina2EN()
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

        public ActionResult moto_pagina3EN(double recorrido, double toneladas, double total, string name, string mail)
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

        public ActionResult moto_pagina4EN(double porsentaje, double total, double ton, string name, string mail)
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

        public ActionResult moto_pagina4_2(double calculo, double ton, string nombre, string telefono, string empresa, string pais, string email)
        {
            ViewBag.Title = "Compensación de carbono para Moto";
            ViewBag.total = calculo;
            ViewBag.toneladas = ton;
            Session["toneladas_moto"] = ton;

              //*********************************************************************************
            //                                     Ambiente de producción
            //*********************************************************************************
            var configuration = new Configuration();
            configuration.Environment = "PRODUCCION";
            configuration.CommerceCode = "597036300078";
            configuration.PrivateCertPfxPath = @"D:\home\site\wwwroot\Content\Certificados\597036300078.pfx";

            configuration.Password = "a";
            configuration.WebpayCertPath = Configuration.GetProductionPublicCertPath();
            var transaction = new Webpay(configuration).NormalTransaction;    //.NormalTransaction;
            //*********************************************************************************
            //                                     Ambiente de prueba
            //*********************************************************************************
            //var transaction = new Webpay(Configuration.ForTestingWebpayPlusNormal()).NormalTransaction;

            //Conf.WebpayCertPath = Configuration.GetProductionPublicCertPath();

            //var transaction = new Webpay(Configuration.ForTestingWebpayPlusNormal()).NormalTransaction;


            //var transaction = new Webpay(configuration).NormalTransaction;    //.NormalTransaction;

            //Convert.ToInt16(calculo);


            //decimal valor = Convert.ToDecimal(calculo);
            var monto = Convert.ToInt32(calculo * 100);
            var orden = Guid.NewGuid().ToString().Replace("-", string.Empty).Substring(0, 8);
            var id = Guid.NewGuid().ToString().Replace("-", string.Empty).Substring(0, 8);

            //string returnUrl = "http://localhost:62106/Moto/Retorno_moto";
            //string returnFinal = "http://localhost:62106/Moto/Final_moto";
            string returnUrl = "https://ecopartnerbank.azurewebsites.net/Moto/Retorno_moto";
            string returnFinal = "https://ecopartnerbank.azurewebsites.net/Moto/Final_moto";

            int montotrans = Convert.ToInt32(calculo * 800);
            var initResult = transaction.initTransaction(montotrans, orden, id, returnUrl, returnFinal);

            ViewBag.toneladas = ton;
            ViewBag.nombre = nombre;
            ViewBag.telefono = telefono;
            ViewBag.empresa = empresa;
            ViewBag.pais = pais;
            ViewBag.email = email;
            ViewBag.total = calculo;  //montotrans;
            ViewBag.id = id;

            //*****************************************************************
            //          Insercion en la base de datos
            //*****************************************************************
            //COMPENSACION cOMPENSACION = new COMPENSACION();
            //cOMPENSACION.id = db.COMPENSACION.Count() + 1;
            //cOMPENSACION.nombre = nombre;
            //cOMPENSACION.telefono = telefono;
            //cOMPENSACION.nombreEmpresa = empresa;
            //cOMPENSACION.pais = pais;
            //cOMPENSACION.mail = email;
            //cOMPENSACION.toneladas = ton.ToString();
            //cOMPENSACION.compensacion1 = montotrans.ToString();
            //cOMPENSACION.id_codigo = id;
            //db.COMPENSACION.Add(cOMPENSACION);
            //db.SaveChanges();
            //*****************************************************************
            //          Fin de Insercion en la base de datos
            //*****************************************************************

            var tokenWs = initResult.token;
            var formAction = initResult.url;

            ViewBag.Monto = monto / 100;
            ViewBag.Orden = orden;
            ViewBag.token = tokenWs;
            ViewBag.form = formAction;
            return View();
        }

        public ActionResult moto_pagina4_2EN(double calculo, double ton, string nombre, string telefono, string empresa, string pais, string email)
        {
            ViewBag.Title = "Compensación de carbono para Moto";
            ViewBag.total = calculo;
            ViewBag.toneladas = ton;
            Session["toneladas_moto"] = ton;

            //*********************************************************************************
            //                                     Ambiente de producción
            //*********************************************************************************
            var configuration = new Configuration();
            configuration.Environment = "PRODUCCION";
            configuration.CommerceCode = "597036300078";
            configuration.PrivateCertPfxPath = @"D:\home\site\wwwroot\Content\Certificados\597036300078.pfx";

            configuration.Password = "a";
            configuration.WebpayCertPath = Configuration.GetProductionPublicCertPath();
            var transaction = new Webpay(configuration).NormalTransaction;    
            //.NormalTransaction;
            //*********************************************************************************
            //                                     Ambiente de prueba
            //*********************************************************************************
            //var transaction = new Webpay(Configuration.ForTestingWebpayPlusNormal()).NormalTransaction;

            //Conf.WebpayCertPath = Configuration.GetProductionPublicCertPath();

            //var transaction = new Webpay(Configuration.ForTestingWebpayPlusNormal()).NormalTransaction;


            //var transaction = new Webpay(configuration).NormalTransaction;    //.NormalTransaction;

            //Convert.ToInt16(calculo);


            //decimal valor = Convert.ToDecimal(calculo);
            var monto = Convert.ToInt32(calculo * 100);
            var orden = Guid.NewGuid().ToString().Replace("-", string.Empty).Substring(0, 8);
            var id = Guid.NewGuid().ToString().Replace("-", string.Empty).Substring(0, 8);

            //string returnUrl = "http://localhost:62106/Moto/Retorno_moto";
            //string returnFinal = "http://localhost:62106/Moto/Final_moto";
            string returnUrl = "https://ecopartnerbank.azurewebsites.net/Moto/Retorno_motoEN";
            string returnFinal = "https://ecopartnerbank.azurewebsites.net/Moto/Final_motoEN";

            int montotrans = Convert.ToInt32(calculo * 800);
            var initResult = transaction.initTransaction(montotrans, orden, id, returnUrl, returnFinal);

            ViewBag.toneladas = ton;
            ViewBag.nombre = nombre;
            ViewBag.telefono = telefono;
            ViewBag.empresa = empresa;
            ViewBag.pais = pais;
            ViewBag.email = email;
            ViewBag.total = calculo;  //montotrans;
            ViewBag.id = id;

            //*****************************************************************
            //          Insercion en la base de datos
            //*****************************************************************
            //COMPENSACION cOMPENSACION = new COMPENSACION();
            //cOMPENSACION.id = db.COMPENSACION.Count() + 1;
            //cOMPENSACION.nombre = nombre;
            //cOMPENSACION.telefono = telefono;
            //cOMPENSACION.nombreEmpresa = empresa;
            //cOMPENSACION.pais = pais;
            //cOMPENSACION.mail = email;
            //cOMPENSACION.toneladas = ton.ToString();
            //cOMPENSACION.compensacion1 = montotrans.ToString();
            //cOMPENSACION.id_codigo = id;
            //db.COMPENSACION.Add(cOMPENSACION);
            //db.SaveChanges();
            //*****************************************************************
            //          Fin de Insercion en la base de datos
            //*****************************************************************

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

        public ActionResult CalculosMotoEN(string kilometros, string cilindrada, string nombre, string correo)
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

            return View("moto_pagina2EN");
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

        public ActionResult Final_motoEN()
        {
            ViewBag.Message = "Your contact page.";
            //ViewBag.toneladas = Response.Write Session("toneladas");
            //var ton = Session["toneladas"];
            ViewBag.tonelada = Session["toneladas_moto"];
            //ViewBag.tonelada = ton;

            ViewBag.Title = "Compensación de carbono para Moto";
            return View();


        }

        public void GuardarDatos(string toneladas, string nombre, string telefono, string empresa, string pais, string email, string total, string id)
        {
            Correo.SendEmailAsync(email, nombre, total, toneladas);
            //*****************************************************************
            //          Insercion en la base de datos
            //*****************************************************************
            var validarReplica = db.COMPENSACION.Where(od => od.id_codigo == id).ToList();

            if (validarReplica.Count() == 0)
            {
                COMPENSACION cOMPENSACION = new COMPENSACION();
                cOMPENSACION.id = db.COMPENSACION.Count() + 1;
                cOMPENSACION.nombre = nombre;
                cOMPENSACION.telefono = telefono;
                cOMPENSACION.nombreEmpresa = empresa;
                cOMPENSACION.pais = pais;
                cOMPENSACION.mail = email;
                cOMPENSACION.toneladas = toneladas;
                cOMPENSACION.compensacion1 = total;
                cOMPENSACION.id_codigo = id;
                db.COMPENSACION.Add(cOMPENSACION);
                db.SaveChanges();

            }

            //*****************************************************************
            //          Fin de Insercion en la base de datos
            //*****************************************************************
        }


        public ActionResult Retorno_moto()
        {
            ViewBag.Title = "Compensación de carbono para Moto";

             //*********************************************************************************
            //                                     Ambiente de producción
            //*********************************************************************************
            var configuration = new Configuration();
            configuration.Environment = "PRODUCCION";
            configuration.CommerceCode = "597036300078";
            configuration.PrivateCertPfxPath = @"D:\home\site\wwwroot\Content\Certificados\597036300078.pfx";

            configuration.Password = "a";
            configuration.WebpayCertPath = Configuration.GetProductionPublicCertPath();
            var transaction = new Webpay(configuration).NormalTransaction;    //.NormalTransaction;
            //*********************************************************************************
            //                                     Ambiente de prueba
            //*********************************************************************************
            //var transaction = new Webpay(Configuration.ForTestingWebpayPlusNormal()).NormalTransaction;

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
                ViewBag.moto = output.authorizationCode;

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

        public ActionResult Retorno_motoEN()
        {
            ViewBag.Title = "Compensación de carbono para Moto";

            //*********************************************************************************
            //                                     Ambiente de producción
            //*********************************************************************************
            var configuration = new Configuration();
            configuration.Environment = "PRODUCCION";
            configuration.CommerceCode = "597036300078";
            configuration.PrivateCertPfxPath = @"D:\home\site\wwwroot\Content\Certificados\597036300078.pfx";

            configuration.Password = "a";
            configuration.WebpayCertPath = Configuration.GetProductionPublicCertPath();
            var transaction = new Webpay(configuration).NormalTransaction;    
            //.NormalTransaction;
            //*********************************************************************************
            //                                     Ambiente de prueba
            //*********************************************************************************
            //var transaction = new Webpay(Configuration.ForTestingWebpayPlusNormal()).NormalTransaction;

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
                ViewBag.moto = output.authorizationCode;

            }
            else
            {
                ViewBag.redirect = result.urlRedirection;
                ViewBag.monto = "La transacción fue rechazada";
                ViewBag.Token = tokenWs;
                return View("ErrorEN");
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