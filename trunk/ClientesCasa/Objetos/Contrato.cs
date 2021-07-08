using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClientesCasa.Objetos
{
    [Serializable]
    public class Contrato : BaseObjeto
    {

    }

    [Serializable]
    public class DocumentoContrato : BaseObjeto
    {
        private int _iIdContrato = 0;
        private string _sDescArchivo = string.Empty;
        private string _sNombreArchivo = string.Empty;
        private string _sExtension = string.Empty;
        private byte[] _bArchivo = new byte[1];

        public int iIdContrato { get { return _iIdContrato; } set { _iIdContrato = value; } }
        public string sDescArchivo { get { return _sDescArchivo; } set { _sDescArchivo = value; } }
        public string sNombreArchivo { get { return _sNombreArchivo; } set { _sNombreArchivo = value; } }
        public string sExtension { get { return _sExtension; } set { _sExtension = value; } }
        public byte[] bArchivo { get { return _bArchivo; } set { _bArchivo = value; } }
    }
}