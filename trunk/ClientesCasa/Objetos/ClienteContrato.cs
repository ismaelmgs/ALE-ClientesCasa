using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace ClientesCasa.Objetos
{
    public class ClienteContrato : BaseObjeto
    {
        //Clientes
        private int _iIdCliente = 0;
        private string _sNumCliente = string.Empty;
        private string _sNombreCliente = string.Empty;
        
        private string _sTipoCliente = "D";
        private string _sRazonSocial = string.Empty;
        private bool _bRFC = false;
        private string _sRFC = string.Empty;
        private string _sTipoContribuyente = string.Empty;
        private int _iActivo = 1;
        private string _sTelefono = string.Empty;
        private string _sFax = string.Empty;
        private string _sCorreoEletronico = string.Empty;
        private int _iIdSector = 0;
        private int _iIdPais = 0;
        private int _iIdEstado = 0;
        private string _sDireccion = string.Empty;
        private string _sCiudad = string.Empty;
        private string _sCP = string.Empty;
        private int _iIdPaisDE = 0;
        private int _iIdEstadoDE = 0;
        private string _sDireccionDE = string.Empty;
        private string _sCiudadDE = string.Empty;
        private string _sCPDE = string.Empty;

        //Datos del contrato
        private int _iIdContrato = 0;
        private string _sIdContrato = string.Empty;
        private string _sClaveContrato = string.Empty;
        private string _sAeronaveSerie = string.Empty;
        private string _sAeronaveMatricula = string.Empty;
        private decimal _iPorcentajePart = 0;
        private int _iHorasContratadas = 0;
        private bool _bAplicaIntercambios = false;
        private int _iFactorIntercambio = 0;
        private DateTime? _dtFechaContrato = new DateTime();
        private int _iEstatusContrato = 0;
        private int _iTipoCosto = 0;
        private DataTable _dtDocumentos = new DataTable();

        //Intercambios
        private int _iIntercambioId = 0;
        private int _iIntercambioGrupoModeloId = 0;
        private decimal _dContratoIntercambiosFactor = 0;
        private decimal _dContratoIntercambiosEspera = 0;
        private decimal _dContratoIntercambiosPernocta = 0;
        private decimal _dContratoIntercambiosValor = 0;
        private int _iContratoIntercambiosGalones = 0;
        private decimal _dContratoIntercambiosTarifaInternacional = 0;
        private decimal _dContratoIntercambiosTarifaNacional = 0;
        private decimal _dContratoIntercambiosFerry = 0;
        private bool _bContratoIntercambiosAplicaFerry = false;

        //Campos de control
        private string _sUsuario = string.Empty;
        private DateTime _dtFechaModif = new DateTime();

        //Contratos
        private DataTable _dtContratos = new DataTable();

        //Adicionales
        private DateTime _FechaFinContrato = new DateTime();
        private decimal _AnticipoContrato = 0;
        private string _MonedaAnticipo = string.Empty;
        private int _TipoServicioConsultoria = 0;
        private int _TipoTarifa = 0;
        private int _detalleTipoTarifa = 0;
        private string _NoPoliza = string.Empty;
        private string _EmpresaAseguradora = string.Empty;
        private DateTime _FechaInicioSeg = new DateTime();
        private DateTime _FechaFinSeg = new DateTime();
        byte[] _vbPolizaSeguro = new byte[1];

        
        public int iIdCliente { get { return _iIdCliente; } set { _iIdCliente = value; } }
        public string sNumCliente { get { return _sNumCliente; } set { _sNumCliente = value; } }
        public string sNombreCliente { get { return _sNombreCliente; } set { _sNombreCliente = value; } }
        public string sTipoCliente { get { return _sTipoCliente; } set { _sTipoCliente = value; } }
        public string sRazonSocial { get { return _sRazonSocial; } set { _sRazonSocial = value; } }
        public bool bRFC { get { return _bRFC; } set { _bRFC = value; } }
        public string sRFC { get { return _sRFC; } set { _sRFC = value; } }
        public string sTipoContribuyente { get { return _sTipoContribuyente; } set { _sTipoContribuyente = value; } }
        public int iActivo { get { return _iActivo; } set { _iActivo = value; } }
        public string sTelefono { get { return _sTelefono; } set { _sTelefono = value; } }
        public string sFax { get { return _sFax; } set { _sFax = value; } }
        public string sCorreoEletronico { get { return _sCorreoEletronico; } set { _sCorreoEletronico = value; } }
        public int iIdSector { get { return _iIdSector; } set { _iIdSector = value; } }
        public int iIdPais { get { return _iIdPais; } set { _iIdPais = value; } }
        public int iIdEstado { get { return _iIdEstado; } set { _iIdEstado = value; } }
        public string sDireccion { get { return _sDireccion; } set { _sDireccion = value; } }
        public string sCiudad { get { return _sCiudad; } set { _sCiudad = value; } }
        public string sCP { get { return _sCP; } set { _sCP = value; } }
        public int iIdPaisDE { get { return _iIdPaisDE; } set { _iIdPaisDE = value; } }
        public int iIdEstadoDE { get { return _iIdEstadoDE; } set { _iIdEstadoDE = value; } }
        public string sDireccionDE { get { return _sDireccionDE; } set { _sDireccionDE = value; } }
        public string sCiudadDE { get { return _sCiudadDE; } set { _sCiudadDE = value; } }
        public string sCPDE { get { return _sCPDE; } set { _sCPDE = value; } }
        public int iIdContrato { get { return _iIdContrato; } set { _iIdContrato = value; } }
        public string sIdContrato { get { return _sIdContrato; } set { _sIdContrato = value; } }
        public string sClaveContrato { get { return _sClaveContrato; } set { _sClaveContrato = value; } }
        public string sAeronaveSerie { get { return _sAeronaveSerie; } set { _sAeronaveSerie = value; } }
        public string sAeronaveMatricula { get { return _sAeronaveMatricula; } set { _sAeronaveMatricula = value; } }
        public decimal iPorcentajePart { get { return _iPorcentajePart; } set { _iPorcentajePart = value; } }
        public int iHorasContratadas { get { return _iHorasContratadas; } set { _iHorasContratadas = value; } }
        public bool bAplicaIntercambios { get { return _bAplicaIntercambios; } set { _bAplicaIntercambios = value; } }
        public int iFactorIntercambio { get { return _iFactorIntercambio; } set { _iFactorIntercambio = value; } }
        public DateTime? dtFechaContrato { get { return _dtFechaContrato; } set { _dtFechaContrato = value; } }
        public int iEstatusContrato { get { return _iEstatusContrato; } set { _iEstatusContrato = value; } }
        public int iTipoCosto { get { return _iTipoCosto; } set { _iTipoCosto = value; } }
        public DataTable dtDocumentos { get { return _dtDocumentos; } set { _dtDocumentos = value; } }
        public int iIntercambioId { get { return _iIntercambioId; } set { _iIntercambioId = value; } }
        public int iIntercambioGrupoModeloId { get { return _iIntercambioGrupoModeloId; } set { _iIntercambioGrupoModeloId = value; } }
        public decimal dContratoIntercambiosFactor { get { return _dContratoIntercambiosFactor; } set { _dContratoIntercambiosFactor = value; } }
        public decimal dContratoIntercambiosEspera { get { return _dContratoIntercambiosEspera; } set { _dContratoIntercambiosEspera = value; } }
        public decimal dContratoIntercambiosPernocta { get { return _dContratoIntercambiosPernocta; } set { _dContratoIntercambiosPernocta = value; } }
        public decimal dContratoIntercambiosValor { get { return _dContratoIntercambiosValor; } set { _dContratoIntercambiosValor = value; } }
        public int iContratoIntercambiosGalones { get { return _iContratoIntercambiosGalones; } set { _iContratoIntercambiosGalones = value; } }
        public decimal dContratoIntercambiosTarifaInternacional { get { return _dContratoIntercambiosTarifaInternacional; } set { _dContratoIntercambiosTarifaInternacional = value; } }
        public decimal dContratoIntercambiosTarifaNacional { get { return _dContratoIntercambiosTarifaNacional; } set { _dContratoIntercambiosTarifaNacional = value; } }
        public decimal dContratoIntercambiosFerry { get { return _dContratoIntercambiosFerry; } set { _dContratoIntercambiosFerry = value; } }
        public bool bContratoIntercambiosAplicaFerry { get { return _bContratoIntercambiosAplicaFerry; } set { _bContratoIntercambiosAplicaFerry = value; } }
        public string sUsuario { get { return _sUsuario; } set { _sUsuario = value; } }
        public DateTime dtFechaModif { get { return _dtFechaModif; } set { _dtFechaModif = value; } }
        public DataTable dtContratos { get { return _dtContratos; } set { _dtContratos = value; } }


        public DateTime dtFechaFinContrato { get { return _FechaFinContrato; } set { _FechaFinContrato = value; } }
        public decimal dAnticipoContrato { get { return _AnticipoContrato; } set { _AnticipoContrato = value; } }
        public string sMonedaAnticipo { get { return _MonedaAnticipo; } set { _MonedaAnticipo = value; } }
        public int iTipoServicioConsultoria { get { return _TipoServicioConsultoria; } set { _TipoServicioConsultoria = value; } }
        public int iTipoTarifa { get { return _TipoTarifa; } set { _TipoTarifa = value; } }
        public int iDetalleTipoTarifa { get { return _detalleTipoTarifa; } set { _detalleTipoTarifa = value; } }
        public string sNoPoliza { get { return _NoPoliza; } set { _NoPoliza = value; } }
        public string sEmpresaAseguradora { get { return _EmpresaAseguradora; } set { _EmpresaAseguradora = value; } }
        public DateTime dtFechaInicioSeg { get { return _FechaInicioSeg; } set { _FechaInicioSeg = value; } }
        public DateTime dtFechaFinSeg { get { return _FechaFinSeg; } set { _FechaFinSeg = value; } }
        public byte[] vbPolizaSeguro { get { return _vbPolizaSeguro; } set { _vbPolizaSeguro = value; } }
    }
}