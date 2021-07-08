using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClientesCasa.Objetos
{
    [Serializable]
    public class Comprobante : BaseObjeto
    {
        private string _sNumeroReporte = string.Empty;
        private int _iIdGasto = 0;
        private byte[] _bArchivo = new byte[1];
        private string _sNombreArchivo = string.Empty;
        private string _sExtension = string.Empty;

        public string sNumeroReporte { get { return _sNumeroReporte; } set { _sNumeroReporte = value; } }
        public int iIdGasto { get { return _iIdGasto; } set { _iIdGasto = value; } }
        public byte[] bArchivo { get { return _bArchivo; } set { _bArchivo = value; } }
        public string sNombreArchivo { get { return _sNombreArchivo; } set { _sNombreArchivo = value; } }
        public string sExtension { get { return _sExtension; } set { _sExtension = value; } }
    }
}