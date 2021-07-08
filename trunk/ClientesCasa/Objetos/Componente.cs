using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClientesCasa.Objetos
{
    public class Componente
    {
        private string _sIdComponente = string.Empty;
        private string _sIdDiscrepancia = string.Empty;
        private string _sNombreComponente = string.Empty;
        private string _sDescripcion = string.Empty;
        private string _sNoParteRemovido = string.Empty;
        private string _sNoSerieRemovido = string.Empty;
        private string _sNoParteInstalado = string.Empty;
        private string _sNoSerieInstalado = string.Empty;
        private string _sPocisionComponente = string.Empty;
        private string _sID = string.Empty;
        private string _sRazonServicio = string.Empty;
        private string _sUsuario = string.Empty;
        public string sIdComponente { get { return _sIdComponente; } set { _sIdComponente = value; } }
        public string sIdDiscrepancia { get { return _sIdDiscrepancia; } set { _sIdDiscrepancia = value; } }
        public string sNombreComponente { get { return _sNombreComponente; } set { _sNombreComponente = value; } }
        public string sDescripcion { get { return _sDescripcion; } set { _sDescripcion = value; } }
        public string sNoParteRemovido { get { return _sNoParteRemovido; } set { _sNoParteRemovido = value; } }
        public string sNoSerieRemovido { get { return _sNoSerieRemovido; } set { _sNoSerieRemovido = value; } }
        public string sNoParteInstalado { get { return _sNoParteInstalado; } set { _sNoParteInstalado = value; } }
        public string sNoSerieInstalado { get { return _sNoSerieInstalado; } set { _sNoSerieInstalado = value; } }
        public string sPocisionComponente { get { return _sPocisionComponente; } set { _sPocisionComponente = value; } }
        public string sID { get { return _sID; } set { _sID = value; } }
        public string sRazonServicio { get { return _sRazonServicio; } set { _sRazonServicio = value; } }
        public string sUsuario { get { return _sUsuario; } set { _sUsuario = value; } }

    }
}