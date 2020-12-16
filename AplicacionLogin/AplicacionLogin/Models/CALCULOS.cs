using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AplicacionLogin.Models
{
    public class CALCULOS
    {
        //CALCULOS AVIÓN
        public double CalcularDistancia(AEROPUERTO p1, AEROPUERTO p2)
        {
            double salida = 0;
            double rad_per_grad = Math.PI / 180;
            double rkm = 6371;

            double dLatitud_rad = (p2.LATITUD - p1.LATITUD) * rad_per_grad;
            double dLongitud_rad = (p2.LONGITUD - p1.LONGITUD) * rad_per_grad;

            double latitud1_rad = p1.LATITUD * rad_per_grad;
            double latitud2_rad = p2.LATITUD * rad_per_grad;

            double sinDLatitud = Math.Sin(dLatitud_rad / 2);
            double sinDLongitud = Math.Sin(dLongitud_rad / 2);

            double a = sinDLatitud * sinDLatitud +
                       Math.Cos(latitud1_rad) * Math.Cos(latitud2_rad) *
                       sinDLongitud * sinDLongitud;
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            return rkm * c;
        }

        public double CalcularCO2(double distancia)
        {
            return Math.Round(distancia * 0.32 / 1000, 2);
        }

        public double CalculosAdicionales(double CO2, int recorrido, int pasajeros, int tipoViaje)
        {
            //if (tipoViaje == 3) { tipoViaje = 2; }
            //int aux = tipoViaje;
            double total = ((CO2 * recorrido) * pasajeros) * tipoViaje;
            return Math.Round(total, 2);
        }

        public double CalcularValor(double CO2)
        {
            return Math.Round(CO2 * 80, 2);
        }

        public double ValorFinal(AEROPUERTO p1, AEROPUERTO p2)
        {
            return CalcularValor(CalcularCO2(CalcularDistancia(p1, p2)));
        }

        //CALCULOS Moto

        public double CalcularLitrosMoto(string kilometros, string cilindrada)
        {
            double litros = 0;
            switch (cilindrada)
            {
                case "1":
                    litros = Convert.ToDouble(kilometros) / 34.2;
                    break;
                case "2":
                    litros = Convert.ToDouble(kilometros) / 23.2;
                    break;
                case "3":
                    litros = Convert.ToDouble(kilometros) / 18.7;
                    break;
            }
            return litros;

        }

        public double CalcularCO2Moto(double litros)
        {
            double CO2 = 0;
            CO2 = litros * 2.37;

            return Math.Round(CO2 / 1000, 3);
        }

        public double CalcularValorMoto(double CO2)
        {
            return Math.Round(CO2 * 80, 2);
        }


        public double Calculartotalf(double total, double porsentaje)
        {
            /*
            Convert.ToString(porsentaje);
            double porciento = 0;
            if (porsentaje == '1')
            {
                porciento = 0.10;
            }
            */

            //double totalf = porciento * total;
            //return Math.Round(totalf);
            double factor = 0;
            switch (porsentaje)
            {
                case 1:
                    factor = 0.1;
                    break;
                case 2:
                    factor = 0.5;
                    break;
                case 3:
                    factor = 1;
                    break;
                default:
                    break;
            }
            //return Math.Round(1.0);
            return total * factor;
        }

        //CALCULOS AUTO

        public double CalcularLitrosAuto(string kilometros, string tipoAuto)
        {
            double litros = 0;
            switch (tipoAuto)
            {
                case "peque":
                    litros = Convert.ToDouble(kilometros) / 16.1;
                    break;
                case "mediano":
                    litros = Convert.ToDouble(kilometros) / 14;
                    break;
                case "grande":
                    litros = Convert.ToDouble(kilometros) / 10.2;
                    break;
            }
            return litros;

        }

        public double CalcularCO2Auto(double litros, string tipoCombustible)
        {
            double CO2 = 0;
            switch (tipoCombustible)
            {
                case "gasolina":
                    CO2 = litros * 2.37;
                    break;
                case "diesel":
                    CO2 = litros * 2.65;
                    break;

            }
            return Math.Round(CO2 / 1000, 3);
        }

        public double CalcularValorAuto(double CO2)
        {
            return Math.Round(CO2 * 80, 2);
        }

        //CALCULOS CAMIÓN

        public double CalcularCO2Camion(string kilometros, string toneladasCarga)
        {
            double CO2 = 0;
            CO2 = Convert.ToDouble(kilometros) * Convert.ToDouble(toneladasCarga);
            CO2 = CO2 * 118;
            return Math.Round(CO2 / 1000000, 3);
        }

        public double CalcularValorCamion(double CO2)
        {
            return Math.Round(CO2 * 80, 2);
        }



    }
}