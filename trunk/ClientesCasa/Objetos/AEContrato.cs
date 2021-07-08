using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace ClientesCasa.Objetos
{
    [Serializable, Bindable(BindableSupport.Yes)]
    public class Contrato_Tarifas
    {
        private int _iIdContrato = -1;
        private decimal _dCostoDirNal = 0m;
        private decimal _dCostoDirInt = 0m;
        private bool _bCombustible = true;
        private int _iTipoCalculo = -1;
        private decimal _dConsumoGalones = 0m;
        private decimal _dFactorTramosNal = 0m;
        private decimal _dFactorTramosInt = 0m;
        private bool _bPrecioInternacionalEspecial = false;

        private bool _bCobraTiempoEspera = true;
        private decimal _dTiempoEsperaFijaNal = 0m;
        private decimal _dTiempoEsperaFijaInt = 0m;
        private decimal _dTiempoEsperaVarNal = 0m;
        private decimal _dTiempoEsperaVarInt = 0m;

        private bool _bCobraPernoctas = true;
        private decimal _dPernoctasFijaNal = 0m;
        private decimal _dPernoctasFijaInt = 0m;
        private decimal _dPernoctasVarNal = 0m;
        private decimal _dPernoctasVarInt = 0m;


        
        public int iIdContrato { get { return _iIdContrato; } set { _iIdContrato = value; } }
        public decimal dCostoDirNal { get { return _dCostoDirNal; } set { _dCostoDirNal = value; } }
        public decimal dCostoDirInt { get { return _dCostoDirInt; } set { _dCostoDirInt = value; } }
        public bool bCombustible { get { return _bCombustible; } set { _bCombustible = value; } }
        public int iTipoCalculo { get { return _iTipoCalculo; } set { _iTipoCalculo = value; } }
        public decimal dConsumoGalones { get { return _dConsumoGalones; } set { _dConsumoGalones = value; } }
        public decimal dFactorTramosNal { get { return _dFactorTramosNal; } set { _dFactorTramosNal = value; } }
        public decimal dFactorTramosInt { get { return _dFactorTramosInt; } set { _dFactorTramosInt = value; } }
        public bool bPrecioInternacionalEspecial { get { return _bPrecioInternacionalEspecial; } set { _bPrecioInternacionalEspecial = value; } }

        public bool bCobraTiempoEspera { get { return _bCobraTiempoEspera; } set { _bCobraTiempoEspera = value; } }
        public decimal dTiempoEsperaFijaNal { get { return _dTiempoEsperaFijaNal; } set { _dTiempoEsperaFijaNal = value; } }
        public decimal dTiempoEsperaFijaInt { get { return _dTiempoEsperaFijaInt; } set { _dTiempoEsperaFijaInt = value; } }
        public decimal dTiempoEsperaVarNal { get { return _dTiempoEsperaVarNal; } set { _dTiempoEsperaVarNal = value; } }
        public decimal dTiempoEsperaVarInt { get { return _dTiempoEsperaVarInt; } set { _dTiempoEsperaVarInt = value; } }

        public bool bCobraPernoctas { get { return _bCobraPernoctas; } set { _bCobraPernoctas = value; } }
        public decimal dPernoctasFijaNal { get { return _dPernoctasFijaNal; } set { _dPernoctasFijaNal = value; } }
        public decimal dPernoctasFijaInt { get { return _dPernoctasFijaInt; } set { _dPernoctasFijaInt = value; } }
        public decimal dPernoctasVarNal { get { return _dPernoctasVarNal; } set { _dPernoctasVarNal = value; } }
        public decimal dPernoctasVarInt { get { return _dPernoctasVarInt; } set { _dPernoctasVarInt = value; } }
    }




    [Serializable, Bindable(BindableSupport.Yes)]
    public class Contrato_CobrosDescuentos
    {
        private int _iIdContrato = -1;
        private int _iFerrysConCargo = 0;
        private bool _bAplicaEsperaLibre = false;
        private decimal _dHorasVuelo = -1;
        private decimal _dFactorHorasVuelo = 0m;
        private bool _bPernoctaNal = false;
        private bool _bPernoctaInt = false;
        private decimal _dPernoctaFactorConversionNal = 0m;
        private decimal _dPernoctaFactorConversionInt = 0m;
        private decimal _dNumeroPernoctasLibreAnual = 0m;
        private bool _bPernoctasDescuento = false;
        private bool _bPernoctasCobro = false;
        private bool _bTiempoEsperaNal = false;
        private bool _bTiempoEsperaInt = false;
        private decimal _dTiempoEsperaFactorConversionNal = 0m;
        private decimal _dTiempoEsperaFactorConversionInt = 0m;
        private int _iTiempoFatura = 0;
        private decimal _dMinutos = 0m;
        private List<int> _lstIdServiciosConCargo = new List<int>();
        private bool _bAplicaTramos = true;


        private string _sNotas = string.Empty;
        public string sNotas { get { return _sNotas; } set { _sNotas = value; } }
        public int iIdContrato { get { return _iIdContrato; } set { _iIdContrato = value; } }
        public int iFerrysConCargo { get { return _iFerrysConCargo; } set { _iFerrysConCargo = value; } }
        public bool bAplicaEsperaLibre { get { return _bAplicaEsperaLibre; } set { _bAplicaEsperaLibre = value; } }
        public decimal dHorasVuelo { get { return _dHorasVuelo; } set { _dHorasVuelo = value; } }
        public decimal dFactorHorasVuelo { get { return _dFactorHorasVuelo; } set { _dFactorHorasVuelo = value; } }
        public bool bPernoctaNal { get { return _bPernoctaNal; } set { _bPernoctaNal = value; } }
        public bool bPernoctaInt { get { return _bPernoctaInt; } set { _bPernoctaInt = value; } }
        public decimal dPernoctaFactorConversionNal { get { return _dPernoctaFactorConversionNal; } set { _dPernoctaFactorConversionNal = value; } }
        public decimal dPernoctaFactorConversionInt { get { return _dPernoctaFactorConversionInt; } set { _dPernoctaFactorConversionInt = value; } }
        public decimal dNumeroPernoctasLibreAnual { get { return _dNumeroPernoctasLibreAnual; } set { _dNumeroPernoctasLibreAnual = value; } }
        public bool bPernoctasDescuento { get { return _bPernoctasDescuento; } set { _bPernoctasDescuento = value; } }
        public bool bPernoctasCobro { get { return _bPernoctasCobro; } set { _bPernoctasCobro = value; } }
        public bool bTiempoEsperaNal { get { return _bTiempoEsperaNal; } set { _bTiempoEsperaNal = value; } }
        public bool bTiempoEsperaInt { get { return _bTiempoEsperaInt; } set { _bTiempoEsperaInt = value; } }
        public decimal dTiempoEsperaFactorConversionNal { get { return _dTiempoEsperaFactorConversionNal; } set { _dTiempoEsperaFactorConversionNal = value; } }
        public decimal dTiempoEsperaFactorConversionInt { get { return _dTiempoEsperaFactorConversionInt; } set { _dTiempoEsperaFactorConversionInt = value; } }
        public int iTiempoFatura { get { return _iTiempoFatura; } set { _iTiempoFatura = value; } }
        public decimal dMinutos { get { return _dMinutos; } set { _dMinutos = value; } }
        public List<int> lstIdServiciosConCargo { get { return _lstIdServiciosConCargo; } set { _lstIdServiciosConCargo = value; } }
        public bool bAplicaTramos { get { return _bAplicaTramos; } set { _bAplicaTramos = value; } }
    }

    public enum TipoIntercambio : int
    {
        Factor = 1,
        Tarifa = 2,
        CostoDirecto = 3
    }

    [Serializable, Bindable(BindableSupport.Yes)]
    public class Contrato_Intercambios
    {
        private int _iId = -1;
        private int _iIdContrato = -1;
        private int _iIdGrupoModelo = -1;
        private bool _bAplicaFerry = false;
        private decimal _dFactor = 0m;
        private decimal _dTarifaNalDlls = 0m;
        private decimal _dTarifaIntDlls = 0m;
        private decimal _dGalones = 0m;
        private decimal _dCDN = 0m;
        private decimal _dCDI = 0m;
        private TipoIntercambio _eTipoIntercambio;

        

        private string _sNotas = string.Empty;
        public string sNotas { get { return _sNotas; } set { _sNotas = value; } }
        public int iId { get { return _iId; } set { _iId = value; } }
        public int iIdContrato { get { return _iIdContrato; } set { _iIdContrato = value; } }
        public int iIdGrupoModelo { get { return _iIdGrupoModelo; } set { _iIdGrupoModelo = value; } }
        public bool bAplicaFerry { get { return _bAplicaFerry; } set { _bAplicaFerry = value; } }
        public decimal dFactor { get { return _dFactor; } set { _dFactor = value; } }
        public decimal dTarifaNalDlls { get { return _dTarifaNalDlls; } set { _dTarifaNalDlls = value; } }
        public decimal dTarifaIntDlls { get { return _dTarifaIntDlls; } set { _dTarifaIntDlls = value; } }
        public decimal dGalones { get { return _dGalones; } set { _dGalones = value; } }
        public decimal dCDN { get { return _dCDN; } set { _dCDN = value; } }
        public decimal dCDI { get { return _dCDI; } set { _dCDI = value; } }
        public TipoIntercambio eTipoIntercambio { get { return _eTipoIntercambio; } set { _eTipoIntercambio = value; } }
    }

}