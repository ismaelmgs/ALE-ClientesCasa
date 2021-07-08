using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClientesCasa.Objetos
{
    [Serializable]
    public class ErroresValidacion : BaseObjeto
    {
        private string _sFila = string.Empty;
        private string _sCampo = string.Empty;
        private string _sValor = string.Empty;
        private string _sStatus = string.Empty;
        private string _sDescripcion = string.Empty;

        public string sFila { get { return _sFila; } set { _sFila = value; } }
        public string sCampo { get { return _sCampo; } set { _sCampo = value; } }
        public string sValor { get { return _sValor; } set { _sValor = value; } }
        public string sStatus { get { return _sStatus; } set { _sStatus = value; } }
        public string sDescripcion { get { return _sDescripcion; } set { _sDescripcion = value; } }
    }
}