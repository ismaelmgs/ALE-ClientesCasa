using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using NucleoBase.Core;

namespace ClientesCasa.Clases
{
    public static class Helpers
    {
        public const string sEmpresa = "1";
        public static string sSerie = ConfigurationManager.AppSettings["SeriePolizaNomina"].S();
        public const string sSucursal = "1";
    }
}