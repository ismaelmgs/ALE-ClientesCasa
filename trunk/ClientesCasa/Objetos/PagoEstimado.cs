using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClientesCasa.Objetos
{
    [Serializable]
    public class PagoEstimado : BaseObjeto
    {
        private string _sClaveCliente = string.Empty;
        private string _sClaveContrato = string.Empty;
        private string _sClaveFacturante = string.Empty;
        private string _sFacturante = string.Empty;
        private decimal _dImporteNvo = 0;
        private int _iMes = 0;
        private int _iAnio = 0;
        private string _sDocNum = string.Empty;
        private DateTime _dtDocDate = new DateTime();
        private string _sMoneda = string.Empty;
        private string _sUsuario = string.Empty;

        public string sClaveCliente { set { _sClaveCliente = value; } get { return _sClaveCliente; } }
        public string sClaveContrato { set { _sClaveContrato = value; } get { return _sClaveContrato; } }
        public string sClaveFacturante { set { _sClaveFacturante = value; } get { return _sClaveFacturante; } }
        public string sFacturante { set { _sFacturante = value; } get { return _sFacturante; } }
        public decimal dImporteNvo { set { _dImporteNvo = value; } get { return _dImporteNvo; } }
        public int iMes { set { _iMes = value; } get { return _iMes; } }
        public int iAnio { set { _iAnio = value; } get { return _iAnio; } }
        public string sDocNum { set { _sDocNum = value; } get { return _sDocNum; } }
        public DateTime dtDocDate { set { _dtDocDate = value; } get { return _dtDocDate; } }
        public string sMoneda { set { _sMoneda = value; } get { return _sMoneda; } }
        public string sUsuario { set { _sUsuario = value; } get { return _sUsuario; } }
    }
}