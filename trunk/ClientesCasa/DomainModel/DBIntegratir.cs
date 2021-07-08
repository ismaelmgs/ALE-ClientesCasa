using NucleoBase.BaseDeDatos;
using NucleoBase.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClientesCasa.DomainModel
{
    public class DBIntegrator
    {
        public BD_SP oDB_SP = new BD_SP();
        private bool bDisposed = false;

        public DBIntegrator()
        {
            oDB_SP.sConexionSQL = Globales.GetConfigConnection("SqlIntegrator");
        }
    }
}