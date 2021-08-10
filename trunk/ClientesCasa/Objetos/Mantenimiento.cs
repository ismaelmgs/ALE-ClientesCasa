using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace ClientesCasa.Objetos
{
    [Serializable]
    public class MantenimientoGastos
    {
        private Int64 _iIdGasto = 0;
        private int _iNoReferencia = 0;
        private string _sMatricula = string.Empty;
        private string _sCentroCostos = string.Empty;
        private decimal _dImporte = 0;
        private string _sTipoMoneda = string.Empty;
        private string _sDescRubro = string.Empty;
        private string _sContrato = string.Empty;
        private int _iNumeroTrip = 0;
        private int _iMes = 0;
        private int _iAnio = 0;
        private string _sUsuario = string.Empty;
        private int _iPorcentaje = 0;

        
        
        public long iIdGasto { get { return _iIdGasto; } set { _iIdGasto = value; } }
        public int iNoReferencia { get { return _iNoReferencia; } set { _iNoReferencia = value; } }
        public string sMatricula { get { return _sMatricula; } set { _sMatricula = value; } }
        public string sCentroCostos { get { return _sCentroCostos; } set { _sCentroCostos = value; } }
        public decimal dImporte { get { return _dImporte; } set { _dImporte = value; } } 
        public string sTipoMoneda { get { return _sTipoMoneda; } set { _sTipoMoneda = value; } }
        public string sDescRubro { get { return _sDescRubro; } set { _sDescRubro = value; } }
        public string sContrato { get { return _sContrato; } set { _sContrato = value; } }
        public int iNumeroTrip { get { return _iNumeroTrip; } set { _iNumeroTrip = value; } }
        public int iMes { get { return _iMes; } set { _iMes = value; } }
        public int iAnio { get { return _iAnio; } set { _iAnio = value; } }
        public string sUsuario { get { return _sUsuario; } set { _sUsuario = value; } }
        public int iPorcentaje { get { return _iPorcentaje; } set { _iPorcentaje = value; } }
    }

    [Serializable, Bindable(BindableSupport.Yes)]
    public class GastoEstimado
    {
        private Int64 _iIdGasto = 0;
        private string _sNoReferencia = string.Empty;
        private string _sMatricula = string.Empty;
        private string _sCentroCostos = string.Empty;
        private decimal _dImporte = 0;
        private string _sTipoMoneda = string.Empty;
        private string _sDescRubro = string.Empty;
        private string _sContrato = string.Empty;
        private int _iNumeroTrip = 0;
        private int _iMes = 0;
        private int _iAnio = 0;
        private string _sUsuario = string.Empty;
        private int _iIdRubro = 0;
        private int _iNumeroPierna = 0;
        private string _sTipoGasto = string.Empty;
        private string _sAmpliadoGasto = string.Empty;

        private string _sFechaVueloOpe = string.Empty;
        private int _iIdTipoRubro = 0;
        private string _sComentarios = string.Empty;
        private int _iProveedor = 0;


        public long iIdGasto { get { return _iIdGasto; } set { _iIdGasto = value; } }
        public string sNoReferencia { get { return _sNoReferencia; } set { _sNoReferencia = value; } } 
        public string sMatricula { get { return _sMatricula; } set { _sMatricula = value; } } 
        public string sCentroCostos { get { return _sCentroCostos; } set { _sCentroCostos = value; } }
        public decimal dImporte { get { return _dImporte; } set { _dImporte = value; } } 
        public string sTipoMoneda { get { return _sTipoMoneda; } set { _sTipoMoneda = value; } } 
        public string sDescRubro { get { return _sDescRubro; } set { _sDescRubro = value; } }
        public string sContrato { get { return _sContrato; } set { _sContrato = value; } }
        public int iNumeroTrip { get { return _iNumeroTrip; } set { _iNumeroTrip = value; } }
        public int iMes { get { return _iMes; } set { _iMes = value; } } 
        public int iAnio { get { return _iAnio; } set { _iAnio = value; } }
        public string sUsuario { get { return _sUsuario; } set { _sUsuario = value; } }
        public int iIdRubro { get { return _iIdRubro; } set { _iIdRubro = value; } }
        public int iNumeroPierna { get { return _iNumeroPierna; } set { _iNumeroPierna = value; } }
        public string sTipoGasto { get { return _sTipoGasto; } set { _sTipoGasto = value; } }
        public string sAmpliadoGasto { get { return _sAmpliadoGasto; } set { _sAmpliadoGasto = value; } }
        public string sFechaVueloOpe { get { return _sFechaVueloOpe; } set { _sFechaVueloOpe = value; } }
        public int iIdTipoRubro { get { return _iIdTipoRubro; } set { _iIdTipoRubro = value; } }
        public string sComentarios { get { return _sComentarios; } set { _sComentarios = value; } }
        public int iProveedor { get { return _iProveedor; } set { _iProveedor = value; } }
    }

}