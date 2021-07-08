using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClientesCasa.Objetos
{
    [Serializable]
    public class FacturaASC : BaseObjeto
    {
        //Cabecero
        private string _sNumDoc = string.Empty;
        private string _sNoFactura = string.Empty;
        private string _sCentroCostos = string.Empty;
        private DateTime _dtFechaDoc = DateTime.Now;
        private string _sProveedor = string.Empty;
        private decimal _dImporte = 0;
        private int _iTipoMtto = 0; //  3-Reserva Motores    4-Reserva Interiores
        private int _iTipoMtto2 = 0; //  1-Preventivo    2-Correctivo
        private string _sObservaciones = string.Empty;
        private int _iStatus = 0;
        private string _sUser = string.Empty;
        private string _sFlota = string.Empty;
        private DateTime _dtFechaCont = DateTime.Now;

        private List<ParticionesFacturaASC> _oLstConceptos = new List<ParticionesFacturaASC>();

        public string sNumDoc { get { return _sNumDoc; } set { _sNumDoc = value; } }
        public string sNoFactura { get { return _sNoFactura; } set { _sNoFactura = value; } }
        public string sCentroCostos { get { return _sCentroCostos; } set { _sCentroCostos = value; } }
        public DateTime dtFechaDoc { get { return _dtFechaDoc; } set { _dtFechaDoc = value; } }
        public string sProveedor { get { return _sProveedor; } set { _sProveedor = value; } }
        public decimal dImporte { get { return _dImporte; } set { _dImporte = value; } }
        public int iTipoMtto { get { return _iTipoMtto; } set { _iTipoMtto = value; } }
        public int iTipoMtto2 { get { return _iTipoMtto2; } set { _iTipoMtto2 = value; } }
        public string sObservaciones { get { return _sObservaciones; } set { _sObservaciones = value; } }
        public int iStatus { get { return _iStatus; } set { _iStatus = value; } }
        public string sUser { get { return _sUser; } set { _sUser = value; } }
        public string sFlota { get { return _sFlota; } set { _sFlota = value; } }
        public DateTime dtFechaCont { get { return _dtFechaCont; } set { _dtFechaCont = value; } }

        public List<ParticionesFacturaASC> oLstConceptos { get { return _oLstConceptos; } set { _oLstConceptos = value; } }


    }

    //Particiones
    [Serializable]
    public class ParticionesFacturaASC
    {
        private int _iIDocEntry = 0;
        private int _iDetalle = 0;
        private string _sNumDoc = string.Empty;
        private string _sNumDocD = string.Empty;
        private string _sCentroCostos = string.Empty;
        private decimal _dImporte = 0;
        private string _sObservaciones = string.Empty;
        private int _iStatus = 0;
        private string _sUser = string.Empty;
        private string _sItemCode = string.Empty;
        private int _iTipoMttoD = 0;

        public int iIDocEntry { get { return _iIDocEntry; } set { _iIDocEntry = value; } }
        public int iDetalle { get { return _iDetalle; } set { _iDetalle = value; } }
        public string sNumDoc { get { return _sNumDoc; } set { _sNumDoc = value; } }
        public string sNumDocD { get { return _sNumDocD; } set { _sNumDocD = value; } }
        public string sCentroCostos { get { return _sCentroCostos; } set { _sCentroCostos = value; } }
        public decimal dImporte { get { return _dImporte; } set { _dImporte = value; } }
        public string sObservaciones { get { return _sObservaciones; } set { _sObservaciones = value; } }
        public int iStatus { get { return _iStatus; } set { _iStatus = value; } }
        public string sUser { get { return _sUser; } set { _sUser = value; } }
        public string sItemCode { get { return _sItemCode; } set { _sItemCode = value; } }
        public int iTipoMttoD { get { return _iTipoMttoD; } set { _iTipoMttoD = value; } }
    }
}