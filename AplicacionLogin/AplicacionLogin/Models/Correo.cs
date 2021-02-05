using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace AplicacionLogin.Models
{
    public class Correo
    {
        public static void SendEmailAsync()
        {
            try
            {
                // Credentials
                var credentials = new NetworkCredential("epb@ecopartnersbank.org", "ecopartnersbank");
                // Mail message
                var mail = new MailMessage()
                {
                    From = new MailAddress("lmonsalve22@gmail.com", "Comprobante de aporte final"),
                    Subject = "TITULO DEL CORREO",
                    Body = "<html><head></head><body><img style='width:30%;height:30%'" +
                    "src='https://github.com/Sud-Austral/EcoPartner/raw/master/Mail.png' alt='Logo'>" +
                    "<p><span lang=ES style='font-size:14.0pt;color:#4472C4'>Hola, hemos recibido tu" +
                    "aporte satisfactoriamente </span></p><a href='https://www.ecopartnersbank.org/'>" +
                    "<p>En este link podrás revisar noticias y actualizaciones respecto a las iniciativas que " +
                    "son parte de la Calculadora Ecopartnersbank.</p></a><p>Resumen de tu aporte</p>" +
                    "<table style='width:80%;border: 1px solid black;border-collapse: collapse;vertical-align: top;'>" +
                    "<tr style='text-align: left;vertical-align: top;'><th style='border: 1px solid black;>" +
                    "Nombre </th><th style='border: 1px solid black;'>Toneladas de CO2 </br> generadas</th>" +
                    "<th style='border: 1px solid black;'>Compensación </th></tr><tr style='width:100%;border: 1px solid black;" +
                    "vertical-align: top;'><td style='border: 1px solid black;'>" +
                    "Daniel Araya" +
                    "</td><td style='border: 1px solid black;'>" +
                    "1.5 ton" +
                    "</td><td style='border: 1px solid black;'>" +
                    "$150.000 CLP" +
                    "</td></tr></table><p>Muchas gracias por tu aporte. Estás siendo parte del cambio con " +
                    "un compromiso real.Esperamos que pronto nos visites nuevamente o nos recomiendes con tus cercanos/as." +
                    "En nuestra web siempre podrás ver a qué inicitativas estamos entregando los aportes recibidos a" +
                    "través de este proyecto, junto con noticias asociadas a los avances de la Calculadora " +
                    "EcoPartnersBank. Si necesitas comunicarte con nosotros escribe a epb@ecopartnersbank.org.</p>" +
                    "</body></html>",
                    IsBodyHtml = true
                };

                mail.To.Add(new MailAddress("lmonsalve22@gmail.com"));

                // Smtp client
                var client = new SmtpClient()
                {
                    Port = 25,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Host = "mail.ecopartnersbank.org",
                    EnableSsl = false,
                    Credentials = credentials
                };

                // Send it...         
                client.Send(mail);
            }
            catch (Exception ex)
            {
                // TODO: handle exception
                throw new InvalidOperationException(ex.Message);
            }

            //return Task.Com;
        }

        public static void SendEmailAsync(string correo, string nombre, string monto, string ton)
        {
            try
            {
                // Credentials
                var credentials = new NetworkCredential("epb@ecopartnersbank.org", "ecopartnersbank");
                // Mail message
                var mail = new MailMessage()
                {
                    From = new MailAddress("lmonsalve22@gmail.com", "Comprobante de aporte final"),
                    Subject = "Comprobante de aporte final",
                    Body = "<html><head></head><body><img style='width:30%;height:30%'" +
                    "src='https://github.com/Sud-Austral/EcoPartner/raw/master/Mail.png' alt='Logo'>" +
                    "<p><span lang=ES style='font-size:14.0pt;color:#4472C4'>Hola " +
                    nombre +
                    ", hemos recibido tu" +
                    "aporte satisfactoriamente </span></p><a href='https://www.ecopartnersbank.org/'>" +
                    "<p>En este link podrás revisar noticias y actualizaciones respecto a las iniciativas que " +
                    "son parte de la Calculadora Ecopartnersbank.</p></a><p>Resumen de tu aporte</p>" +
                    "<table style='width:80%;border: 1px solid black;border-collapse: collapse;vertical-align: top;'>" +
                    "<th style='border: 1px solid black;'>Nombre </th>" +
                    //"<th style='border: 1px solid black;>" + "Nombre </th>" +
                    "<th style='border: 1px solid black;'>Toneladas de CO2 </br> generadas</th>" +
                    "<th style='border: 1px solid black;'>Compensación </th>" +
                    "</tr><tr style='width:100%;border: 1px solid black;" +
                    "vertical-align: top;'>" +
                    "<td style='border: 1px solid black;'>" +
                    nombre +
                    "</td><td style='border: 1px solid black;'>" +
                    ton + " ton" +
                    "</td><td style='border: 1px solid black;'>" +
                    "$ " +monto + " CLP" +
                    "</td></tr></table><p>Muchas gracias por tu aporte. Estás siendo parte del cambio con " +
                    "un compromiso real.Esperamos que pronto nos visites nuevamente o nos recomiendes con tus cercanos/as." +
                    "En nuestra web siempre podrás ver a qué inicitativas estamos entregando los aportes recibidos a" +
                    "través de este proyecto, junto con noticias asociadas a los avances de la Calculadora " +
                    "EcoPartnersBank. Si necesitas comunicarte con nosotros escribe a epb@ecopartnersbank.org.</p>" +
                    "</body></html>",
                    IsBodyHtml = true
                };

                mail.To.Add(new MailAddress(correo));

                // Smtp client
                var client = new SmtpClient()
                {
                    Port = 25,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Host = "mail.ecopartnersbank.org",
                    EnableSsl = false,
                    Credentials = credentials
                };

                // Send it...         
                client.Send(mail);
            }
            catch (Exception ex)
            {
                // TODO: handle exception
                throw new InvalidOperationException(ex.Message);
            }

            //return Task.Com;
        }
    }
}