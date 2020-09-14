using AplicacionLogin.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace AplicacionLogin.Controllers
{
    public class AvionController : Controller
    {
        // GET: Avion
        public ActionResult Avion_pagina1()
        {
            return View();
        }

        public ActionResult Avion_pagina()
        {
            return View();
        }

        public ActionResult Avion_pagina2()
        {
            return View();
        }

        public ActionResult Avion_pagina3(double recorrido, double toneladas, double total)
        {

            ViewBag.recorrido = recorrido;
            ViewBag.toneladas = toneladas;
            ViewBag.total = total;

            return View();
        }

        public ActionResult Avion_pagina4()
        {
            return View();
        }

        public ActionResult Avion_pagina4_2()
        {
            return View();
        }

        public ActionResult BuscarAeropuertos(string inicio, string destino, int recorrido, int pasajeros, int tipoViaje)
        {
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

            return View("Avion_pagina2");
        }

       
    }
}