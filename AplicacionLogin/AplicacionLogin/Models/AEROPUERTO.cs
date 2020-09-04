using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AplicacionLogin.Models
{
    public class AEROPUERTO
    {
        public int AIRPORT_ID { get; set; }
        public string NOMBRE { get; set; }
        public string CIUDAD { get; set; }
        public string PAIS { get; set; }
        public string IATA { get; set; }
        public string ICAO { get; set; }
        public double LATITUD { get; set; }
        public double LONGITUD { get; set; }
        public float ALTITUD { get; set; }
        public double ZONAHORARIA { get; set; }
        public string DST { get; set; }
        public string TZTIME { get; set; }
        public string TIPO { get; set; }
        public string FUENTE { get; set; }

        public AEROPUERTO(int aIRPORT_ID, string nOMBRE, string cIUDAD, string pAIS, string iATA, string iCAO, double lATITUD, double lONGITUD, float aLTITUD, double zONAHORARIA, string dST, string tZTIME, string tIPO, string fUENTE)
        {
            AIRPORT_ID = aIRPORT_ID;
            NOMBRE = nOMBRE;
            CIUDAD = cIUDAD;
            PAIS = pAIS;
            IATA = iATA;
            ICAO = iCAO;
            LATITUD = lATITUD;
            LONGITUD = lONGITUD;
            ALTITUD = aLTITUD;
            ZONAHORARIA = zONAHORARIA;
            DST = dST;
            TZTIME = tZTIME;
            TIPO = tIPO;
            FUENTE = fUENTE;
        }

        public AEROPUERTO()
        {
        }
    }
}