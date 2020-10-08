using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Transbank.Webpay;

namespace WebApplication6.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
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
            var monto = 2000;
            var orden = "1234567";
            var id = "1234456";

            string returnUrl = "http://localhost:63928/Home/Retorno";
            string returnFinal = "http://localhost:63928/Home/Final";

            var initResult = transaction.initTransaction(monto, orden, id, returnUrl, returnFinal);
            

            var tokenWs = initResult.token;
            var formAction = initResult.url;

            ViewBag.Monto = monto;
            ViewBag.Orden = orden;
            ViewBag.token = tokenWs;
            ViewBag.form = formAction;

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult prueba()
            {
        return View();
                }

        public ActionResult Retorno()
        {
            var transaction = new Webpay(Configuration.ForTestingWebpayPlusNormal()).NormalTransaction;
            string tokenWs = Request.Form["token_ws"];
            var result = transaction.getTransactionResult(tokenWs);

            var output = result.detailOutput[0];
            if(output.responseCode == 0)
            {
                ViewBag.redirect = result.urlRedirection;
                ViewBag.Token = tokenWs;
                ViewBag.Response = output.responseCode;
                ViewBag.monto = output.amount;
                ViewBag.auto = output.authorizationCode;

            }
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
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}