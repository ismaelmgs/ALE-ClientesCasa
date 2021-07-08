using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NucleoBase.BaseDeDatos;
using NucleoBase.Core;

namespace ClientesCasa.DomainModel
{
    public class DBBase
    {
        public BD_SP oDB_SP = new BD_SP();
        private bool bDisposed = false;

        public DBBase()
        {
            oDB_SP.sConexionSQL = Globales.GetConfigConnection("SqlDefault");
        }
    }
}