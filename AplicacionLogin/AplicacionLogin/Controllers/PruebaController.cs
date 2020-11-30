using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Transbank.Webpay;

namespace AplicacionLogin.Controllers
{
    public class PruebaController : Controller
    {
        // GET: Prueba
        public ActionResult Index()
        {
            var transaction = new Webpay(Configuration.ForTestingWebpayPlusNormal()).NormalTransaction;

            var monto = 50;
            var orden = "1234567";
            var id = "1234456";

            string returnUrl = "http://localhost:62106/Prueba/Retorno";
            string returnFinal = "http://localhost:62106/Prueba/Final";

            var initResult = transaction.initTransaction(monto, orden, id, returnUrl, returnFinal);


            var tokenWs = initResult.token;
            var formAction = initResult.url;

            ViewBag.Monto = monto;
            ViewBag.Orden = orden;
            ViewBag.token = tokenWs;
            ViewBag.form = formAction;
            return View();
        }
        public ActionResult retorno()
        {
            ViewBag.Title = "Compensación de carbono para Autos";
            var transaction = new Webpay(Configuration.ForTestingWebpayPlusNormal()).NormalTransaction;
            string tokenWs = Request.Form["token_ws"];
            var result = transaction.getTransactionResult(tokenWs);

            var output = result.detailOutput[0];
            if (output.responseCode == 0)
            {
                ViewBag.redirect = result.urlRedirection;
                ViewBag.orden = result.buyOrder;
                ViewBag.Token = tokenWs;
                ViewBag.Response = output.responseCode;
                ViewBag.monto = output.amount;
                ViewBag.auto = output.authorizationCode;

            }
            ViewBag.Message = "Your application description page.";
            return View();
        }
        public ActionResult Final()
        {
            return View();
        }

    }
}