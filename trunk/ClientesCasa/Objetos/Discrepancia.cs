using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClientesCasa.Objetos
{
    public class Discrepancia
    {
        private string _sIdDiscrepancia = string.Empty;
        private string _sIdBitacora = string.Empty;
        private string _sOrigen = string.Empty;
        private string _sTipoReporte = string.Empty;
        private string _sDescripcion = string.Empty;
        private string _sAccionesCorrectiva = string.Empty;
        private string _sCodigoAta = string.Empty;
        private string _sBase = string.Empty;
        private string _sMecanico = string.Empty;
        private DateTime? _dtFechaDiscrepancia = new DateTime();
        private DateTime? _dtFechaAtencion = new DateTime();
        private string _sReferenciaRep = string.Empty;
        private string _sDiagnostico = string.Empty;
        private string _sId = string.Empty;
        private string _sComponente = string.Empty;
        private string _sUsuario = string.Empty;
        public string sIdDiscrepancia { get { return _sIdDiscrepancia; } set { _sIdDiscrepancia = value; } }
        public string sIdBitacora { get { return _sIdBitacora; } set { _sIdBitacora = value; } }
        public string sOrigen { get { return _sOrigen; } set { _sOrigen = value; } }
        public string sTipoReporte { get { return _sTipoReporte; } set { _sTipoReporte = value; } }
        public string sDescripcion { get { return _sDescripcion; } set { _sDescripcion = value; } }
        public string sAccionesCorrectiva { get { return _sAccionesCorrectiva; } set { _sAccionesCorrectiva = value; } }
        public string sCodigoAta { get { return _sCodigoAta; } set { _sCodigoAta = value; } }
        public string sBase { get { return _sBase; } set { _sBase = value; } }
        public string sMecanico { get { return _sMecanico; } set { _sMecanico = value; } }
        public DateTime? dtFechaDiscrepancia { get { return _dtFechaDiscrepancia; } set { _dtFechaDiscrepancia = value; } }
        public DateTime? dtFechaAtencion { get { return _dtFechaAtencion; } set { _dtFechaAtencion = value; } }
        public string sReferenciaRep { get { return _sReferenciaRep; } set { _sReferenciaRep = value; } }
        public string sDiagnostico { get { return _sDiagnostico; } set { _sDiagnostico = value; } }
        public string sId { get { return _sId; } set { _sId = value; } }
        public string sComponente { get { return _sComponente; } set { _sComponente = value; } }
        public string sUsuario { get { return _sUsuario; } set { _sUsuario = value; } }
    }
}