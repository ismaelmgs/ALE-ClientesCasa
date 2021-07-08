using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace ClientesCasa.Objetos
{
    [Serializable]
    public class PolizaNomina : BaseObjeto
    {
        //private int _iIdFolio = 0;
        private string _sArchivo = string.Empty;
        private DateTime _dtFechaIni = DateTime.Now;
        private DateTime _dtFechaFin = DateTime.Now;
        private string _sUsuario = string.Empty;
        private List<DetallePolizaNomina> _oLstDetalle = new List<DetallePolizaNomina>();

        //public int iIdFolio { get { return _iIdFolio; } set { _iIdFolio = value; } }
        public string sArchivo { get { return _sArchivo; } set { _sArchivo = value; } }
        public DateTime dtFechaIni { get { return _dtFechaIni; } set { _dtFechaIni = value; } }
        public DateTime dtFechaFin { get { return _dtFechaFin; } set { _dtFechaFin = value; } }
        public string sUsuario { get { return _sUsuario; } set { _sUsuario = value; } }

        public List<DetallePolizaNomina> oLstDetalle { get { return _oLstDetalle; } set { _oLstDetalle = value; } }

    }

    [Serializable]
    public class DetallePolizaNomina
    {
        private int _iIdFolio = 0;
        private string _sEmpresa = string.Empty;
        private string _sPeriodo = string.Empty;
        private string _sFecha = string.Empty;              // NUEVO
        private string _sTipoMovimiento = string.Empty;
        private string _sFactura = string.Empty;
        private string _sTipoNomina = string.Empty;
        private string _sNombre = string.Empty;
        private string _sRFC = string.Empty;
        private decimal _dSalarioMensual = 0;
        private decimal _dVales = 0;
        private decimal _dHorasExtras = 0;
        private decimal _dHorasExtraTLC = 0;                // NUEVO
        private decimal _dHorasExtraADN = 0;                // NUEVO
        private decimal _dHorasExtraPend = 0;               // NUEVO
        private decimal _dIncidenciasNoRep = 0;             // NUEVO
        private decimal _dFaltas = 0;                       // NUEVO
        private decimal _dDevolucionCredInfo = 0;           // NUEVO

        private decimal _dDevolucion = 0;                   // NUEVO
        public decimal dDevolucion
        {
            get { return _dDevolucion; }
            set { _dDevolucion = value; }
        }

        private decimal _dIncidenciasQ3MexJet = 0;
        public decimal dIncidenciasQ3MexJet
        {
            get { return _dIncidenciasQ3MexJet; }
            set { _dIncidenciasQ3MexJet = value; }
        }

        private decimal _dPagoIncapacidad = 0;
        private decimal _dCompensacion = 0;
        private decimal _dCompensacionPorRetro = 0;         // NUEVO

        public decimal dCompensacionPorRetro
        {
            get { return _dCompensacionPorRetro; }
            set { _dCompensacionPorRetro = value; }
        }

        private decimal _dCompensacionFija = 0;             // NUEVO

        public decimal dCompensacionFija
        {
            get { return _dCompensacionFija; }
            set { _dCompensacionFija = value; }
        }

        private decimal _dDevolucionPorFaltas = 0;          // NUEVO
        public decimal dDevolucionPorFaltas
        {
            get { return _dDevolucionPorFaltas; }
            set { _dDevolucionPorFaltas = value; }
        }

        private decimal _dDevolucionPorPrestamo = 0;          // NUEVO
        public decimal dDevolucionPorPrestamo
        {
            get { return _dDevolucionPorPrestamo; }
            set { _dDevolucionPorPrestamo = value; }
        }

        private decimal _dSD = 0;          // NUEVO
        public decimal dSD
        {
            get { return _dSD; }
            set { _dSD = value; }
        }

        private decimal _dRetroactivo = 0;                  // NUEVO

        public decimal dRetroactivo
        {
            get { return _dRetroactivo; }
            set { _dRetroactivo = value; }
        }
        private decimal _dDiasLaborados = 0;             // NUEVO

        public decimal dDiasLaborados
        {
            get { return _dDiasLaborados; }
            set { _dDiasLaborados = value; }
        }
        private decimal _dPagoPropina = 0;
        private decimal _dPropina = 0;                  // NUEVO

        public decimal dPropina
        {
            get { return _dPropina; }
            set { _dPropina = value; }
        }
        private decimal _dHorasDiasFestivos = 0;
        private decimal _dHorasFestivosTLC = 0;             // NUEVO
        public decimal dHorasFestivosTLC
        {
            get { return _dHorasFestivosTLC; }
            set { _dHorasFestivosTLC = value; }
        }

        private decimal _dHorasFestivosADN = 0;             // NUEVO
        public decimal dHorasFestivosADN
        {
            get { return _dHorasFestivosADN; }
            set { _dHorasFestivosADN = value; }
        }

        private decimal _dPrimaDominical = 0;
        private decimal _dPrimaDominicalTLC = 0;            // NUEVO

        public decimal dPrimaDominicalTLC
        {
            get { return _dPrimaDominicalTLC; }
            set { _dPrimaDominicalTLC = value; }
        }
        private decimal _dPrimaDominicalADN = 0;            // NUEVO
        public decimal dPrimaDominicalADN
        {
            get { return _dPrimaDominicalADN; }
            set { _dPrimaDominicalADN = value; }
        }

        private decimal _dPrimaVacacionalTLC = 0;           // NUEVO
        public decimal dPrimaVacacionalTLC
        {
            get { return _dPrimaVacacionalTLC; }
            set { _dPrimaVacacionalTLC = value; }
        }

        private decimal _dPrimaVacacionalADN = 0;           // NUEVO
        public decimal dPrimaVacacionalADN
        {
            get { return _dPrimaVacacionalADN; }
            set { _dPrimaVacacionalADN = value; }
        }

        private decimal _dPrimaVacacional = 0;

        private decimal _dINCXAntiguedad = 0;           // NUEVO
        public decimal dINCXAntiguedad
        {
            get { return _dINCXAntiguedad; }
            set { _dINCXAntiguedad = value; }
        }

        private decimal _dPrimaAntiguedad = 0;              // NOMBRE DIFERENTE
        private decimal _dBono = 0;
        private decimal _dTotalIngresos = 0;
        private decimal _dSalarioDiario = 0;
        private decimal _dSalarioIntegrado = 0;
        private decimal _dSueldo = 0;
        private decimal _dSeptimoDia = 0;
        private decimal _dHorasExtras2 = 0;
        private decimal _dDestajos = 0;
        private decimal _dPremioEficiencia = 0;


        private decimal _dVacaciones = 0;                   // NUEVO

        public decimal dVacaciones
        {
            get { return _dVacaciones; }
            set { _dVacaciones = value; }
        }
        private decimal _dPrimaVacacional2 = 0;             // NUEVO

        public decimal dPrimaVacacional2
        {
            get { return _dPrimaVacacional2; }
            set { _dPrimaVacacional2 = value; }
        }

        private decimal _dAguinaldo2 = 0;                    // NUEVO

        public decimal dAguinaldo2
        {
            get { return _dAguinaldo2; }
            set { _dAguinaldo2 = value; }
        }

        private decimal _dAguinaldo = 0;                    // NUEVO

        public decimal dAguinaldo
        {
            get { return _dAguinaldo; }
            set { _dAguinaldo = value; }
        }
        private decimal _dOtrasPercepciones = 0;
        private decimal _dTotalPercepciones = 0;
        private decimal _dRetInvVida = 0;
        private decimal _dRetCesantia = 0;
        private decimal _dRetEnfMatObrero = 0;
        private decimal _dSeguroViviendaInfonavit = 0;
        private decimal _dSubsEmpleoAcreditado = 0;
        private decimal _dSubsidioEmpleo = 0;
        private decimal _dISRAntesSubsEmpleo = 0;

        private decimal _dISR_Art142 = 0;              // NUEVO
        public decimal dISR_Art142
        {
            get { return _dISR_Art142; }
            set { _dISR_Art142 = value; }
        }

        private decimal _d_IVA = 0;

        public decimal d_IVA
        {
            get { return _d_IVA; }
            set { _d_IVA = value; }
        }

        private decimal _dISR_SP = 0;
        private decimal _dIMSS = 0;
        private decimal _dPrestamoInfonavit = 0;
        private decimal _dAjusteNeto = 0;
        private decimal _dPensionAlimenticia = 0;
        private decimal _dOtrasDeducciones = 0;
        private decimal _dTotalDeducciones = 0;
        private decimal _dNeto = 0;
        private decimal _dPrestamoEmpresa = 0;              // NUEVO

        public decimal dPrestamoEmpresa
        {
            get { return _dPrestamoEmpresa; }
            set { _dPrestamoEmpresa = value; }
        }

        private decimal _dNetoIMSSReal = 0;              // NUEVO
        public decimal dNetoIMSSReal
        {
            get { return _dNetoIMSSReal; }
            set { _dNetoIMSSReal = value; }
        }

        //private decimal _dValesGratificacion = 0;           // NUEVO

        private decimal _dInvalidezVida = 0;
        private decimal _dCesantiaVejez = 0;
        private decimal _dEnfMatPatron = 0;
        private decimal _dFondoRetiroSAR2Porciento = 0;
        private decimal _dImpuestoEstatal3Porciento = 0;
        private decimal _dRiesgoTrabajo = 0;
        private decimal _dIMSSEmpresa = 0;
        private decimal _dInfonavitEmpresa = 0;
        private decimal _dGuarderiaIMSS = 0;
        private decimal _dOtrasObligaciones = 0;
        private decimal _dTotalObligaciones = 0;
        private decimal _dEmpresaPagaAsimilados = 0;
        private decimal _dAsimilados = 0;
        private decimal _dISR = 0;
        private decimal _dTotalPagarAsimiladosSalario = 0;
        private decimal _dPrestamoCompania = 0;
        private decimal _dInteresesPrestamo = 0;
        private decimal _dRecuperacionAsesores = 0;
        private decimal _dValesGratificacion = 0;           // NUEVO

        public decimal dValesGratificacion
        {
            get { return _dValesGratificacion; }
            set { _dValesGratificacion = value; }
        }

        private decimal _dGastosNoComprobados = 0;
        public decimal dGastosNoComprobados
        {
            get { return _dGastosNoComprobados; }
            set { _dGastosNoComprobados = value; }
        }

        private decimal _dDescuentoGMM = 0;                 // NUEVO
        public decimal dDescuentoGMM
        {
            get { return _dDescuentoGMM; }
            set { _dDescuentoGMM = value; }
        }

        private decimal _dCursoIngles = 0;                 // NUEVO
        public decimal dCursoIngles
        {
            get { return _dCursoIngles; }
            set { _dCursoIngles = value; }
        }

        private decimal _dDescuentoPorPagoDeMas = 0;        // NUEVO

        public decimal dDescuentoPorPagoDeMas
        {
            get { return _dDescuentoPorPagoDeMas; }
            set { _dDescuentoPorPagoDeMas = value; }
        }
        private decimal _dDescuentoPorFaltas = 0;           // NUEVO

        public decimal dDescuentoPorFaltas
        {
            get { return _dDescuentoPorFaltas; }
            set { _dDescuentoPorFaltas = value; }
        }

        private decimal _dGastosNoComprobados2 = 0;                 // NUEVO
        public decimal dGastosNoComprobados2
        {
            get { return _dGastosNoComprobados2; }
            set { _dGastosNoComprobados2 = value; }
        }

        private decimal _dDescuentoSportium = 0;            // NUEVO

        public decimal dDescuentoSportium
        {
            get { return _dDescuentoSportium; }
            set { _dDescuentoSportium = value; }
        }
        private decimal _dDescuentoPorMerma = 0;            // NUEVO

        public decimal dDescuentoPorMerma
        {
            get { return _dDescuentoPorMerma; }
            set { _dDescuentoPorMerma = value; }
        }
        //private decimal _dGastosNoComprobados = 0;
        private decimal _dAyudante = 0;                     // NUEVO

        public decimal dAyudante
        {
            get { return _dAyudante; }
            set { _dAyudante = value; }
        }
        private decimal _dDescuentoClienteOtros = 0;        // NUEVO

        public decimal dDescuentoClienteOtros
        {
            get { return _dDescuentoClienteOtros; }
            set { _dDescuentoClienteOtros = value; }
        }
        private decimal _dOtros = 0;
        private decimal _dPensionAlimenticia2 = 0;
        private decimal _dNetoPagarAsimilados = 0;

        private decimal _dTarjetaEmpresarialISR = 0;        // NUEVO
        public decimal dTarjetaEmpresarialISR
        {
            get { return _dTarjetaEmpresarialISR; }
            set { _dTarjetaEmpresarialISR = value; }
        }

        private decimal _dSueldoIMSS = 0;
        private decimal _dAsimiladosSalarios = 0;

        private decimal _dTarjetaEmpresarialISR2 = 0;
        public decimal dTarjetaEmpresarialISR2
        {
            get { return _dTarjetaEmpresarialISR2; }
            set { _dTarjetaEmpresarialISR2 = value; }
        }

        private decimal _dCargaPatronal = 0;
        private decimal _dMARKUP7_5Porciento = 0;
        private decimal _dFactorChequeCertSub = 0;          // NUEVO

        public decimal dFactorChequeCertSub
        {
            get { return _dFactorChequeCertSub; }
            set { _dFactorChequeCertSub = value; }
        }
        private decimal _dCostoEmpleado = 0;
        private decimal _dImporte = 0;
        private decimal _dIVA = 0;
        private decimal _dTotalFactura = 0;


        //private decimal _dSeguroDanoVivienda = 0;
        //private decimal _dRecuperacionPrestamo = 0;
        //private decimal _dNetoIMSSReal = 0;
        //private decimal _dCursoIngles = 0;
        //private decimal _dTarjetaEmpresarial = 0;
        //private decimal _dTarjetaEmpresarial2 = 0;
        //private decimal _dFactor = 0;


        public int iIdFolio { get { return _iIdFolio; } set { _iIdFolio = value; } }
        public string sEmpresa { get { return _sEmpresa; } set { _sEmpresa = value; } }
        public string sPeriodo { get { return _sPeriodo; } set { _sPeriodo = value; } }
        public string sFecha { get { return _sFecha; } set { _sFecha = value; } }
        public string sTipoMovimiento { get { return _sTipoMovimiento; } set { _sTipoMovimiento = value; } }
        public string sFactura { get { return _sFactura; } set { _sFactura = value; } }
        public string sTipoNomina { get { return _sTipoNomina; } set { _sTipoNomina = value; } }
        public string sNombre { get { return _sNombre; } set { _sNombre = value; } }
        public string sRFC { get { return _sRFC; } set { _sRFC = value; } }
        public decimal dSalarioMensual { get { return _dSalarioMensual; } set { _dSalarioMensual = value; } }
        public decimal dVales { get { return _dVales; } set { _dVales = value; } }
        public decimal dHorasExtras { get { return _dHorasExtras; } set { _dHorasExtras = value; } }
        public decimal dHorasExtraTLC
        {
            get { return _dHorasExtraTLC; }
            set { _dHorasExtraTLC = value; }
        }
        public decimal dHorasExtraADN
        {
            get { return _dHorasExtraADN; }
            set { _dHorasExtraADN = value; }
        }
        public decimal dHorasExtraPend
        {
            get { return _dHorasExtraPend; }
            set { _dHorasExtraPend = value; }
        }
        public decimal dIncidenciasNoRep
        {
            get { return _dIncidenciasNoRep; }
            set { _dIncidenciasNoRep = value; }
        }
        public decimal dFaltas
        {
            get { return _dFaltas; }
            set { _dFaltas = value; }
        }
        public decimal dDevolucionCredInfo
        {
            get { return _dDevolucionCredInfo; }
            set { _dDevolucionCredInfo = value; }
        }
        public decimal dPagoIncapacidad { get { return _dPagoIncapacidad; } set { _dPagoIncapacidad = value; } }
        public decimal dCompensacion { get { return _dCompensacion; } set { _dCompensacion = value; } }
        public decimal dHorasDiasFestivos { get { return _dHorasDiasFestivos; } set { _dHorasDiasFestivos = value; } }
        public decimal dPagoPropina { get { return _dPagoPropina; } set { _dPagoPropina = value; } }
        public decimal dPrimaDominical { get { return _dPrimaDominical; } set { _dPrimaDominical = value; } }
        public decimal dPrimaVacacional { get { return _dPrimaVacacional; } set { _dPrimaVacacional = value; } }
        public decimal dPrimaAntiguedad { get { return _dPrimaAntiguedad; } set { _dPrimaAntiguedad = value; } }
        public decimal dBono { get { return _dBono; } set { _dBono = value; } }
        public decimal dTotalIngresos { get { return _dTotalIngresos; } set { _dTotalIngresos = value; } }
        public decimal dSalarioDiario { get { return _dSalarioDiario; } set { _dSalarioDiario = value; } }
        public decimal dSalarioIntegrado { get { return _dSalarioIntegrado; } set { _dSalarioIntegrado = value; } }
        public decimal dSueldo { get { return _dSueldo; } set { _dSueldo = value; } }
        public decimal dSeptimoDia { get { return _dSeptimoDia; } set { _dSeptimoDia = value; } }
        public decimal dHorasExtras2 { get { return _dHorasExtras2; } set { _dHorasExtras2 = value; } }
        public decimal dDestajos { get { return _dDestajos; } set { _dDestajos = value; } }
        public decimal dPremioEficiencia { get { return _dPremioEficiencia; } set { _dPremioEficiencia = value; } }
        public decimal dOtrasPercepciones { get { return _dOtrasPercepciones; } set { _dOtrasPercepciones = value; } }
        public decimal dTotalPercepciones { get { return _dTotalPercepciones; } set { _dTotalPercepciones = value; } }
        public decimal dRetInvVida { get { return _dRetInvVida; } set { _dRetInvVida = value; } }
        public decimal dRetCesantia { get { return _dRetCesantia; } set { _dRetCesantia = value; } }
        public decimal dRetEnfMatObrero { get { return _dRetEnfMatObrero; } set { _dRetEnfMatObrero = value; } }
        public decimal dSeguroViviendaInfonavit { get { return _dSeguroViviendaInfonavit; } set { _dSeguroViviendaInfonavit = value; } }
        public decimal dSubsEmpleoAcreditado { get { return _dSubsEmpleoAcreditado; } set { _dSubsEmpleoAcreditado = value; } }
        public decimal dSubsidioEmpleo { get { return _dSubsidioEmpleo; } set { _dSubsidioEmpleo = value; } }
        public decimal dISRAntesSubsEmpleo { get { return _dISRAntesSubsEmpleo; } set { _dISRAntesSubsEmpleo = value; } }
        public decimal dISR_SP { get { return _dISR_SP; } set { _dISR_SP = value; } }
        public decimal dIMSS { get { return _dIMSS; } set { _dIMSS = value; } }
        public decimal dPrestamoInfonavit { get { return _dPrestamoInfonavit; } set { _dPrestamoInfonavit = value; } }
        public decimal dAjusteNeto { get { return _dAjusteNeto; } set { _dAjusteNeto = value; } }
        public decimal dPensionAlimenticia { get { return _dPensionAlimenticia; } set { _dPensionAlimenticia = value; } }
        public decimal dOtrasDeducciones { get { return _dOtrasDeducciones; } set { _dOtrasDeducciones = value; } }
        public decimal dTotalDeducciones { get { return _dTotalDeducciones; } set { _dTotalDeducciones = value; } }
        public decimal dNeto { get { return _dNeto; } set { _dNeto = value; } }
        public decimal dInvalidezVida { get { return _dInvalidezVida; } set { _dInvalidezVida = value; } }
        public decimal dCesantiaVejez { get { return _dCesantiaVejez; } set { _dCesantiaVejez = value; } }
        public decimal dEnfMatPatron { get { return _dEnfMatPatron; } set { _dEnfMatPatron = value; } }
        public decimal dFondoRetiroSAR2Porciento { get { return _dFondoRetiroSAR2Porciento; } set { _dFondoRetiroSAR2Porciento = value; } }
        public decimal dImpuestoEstatal3Porciento { get { return _dImpuestoEstatal3Porciento; } set { _dImpuestoEstatal3Porciento = value; } }
        public decimal dRiesgoTrabajo { get { return _dRiesgoTrabajo; } set { _dRiesgoTrabajo = value; } }
        public decimal dIMSSEmpresa { get { return _dIMSSEmpresa; } set { _dIMSSEmpresa = value; } }
        public decimal dInfonavitEmpresa { get { return _dInfonavitEmpresa; } set { _dInfonavitEmpresa = value; } }
        public decimal dGuarderiaIMSS { get { return _dGuarderiaIMSS; } set { _dGuarderiaIMSS = value; } }
        public decimal dOtrasObligaciones { get { return _dOtrasObligaciones; } set { _dOtrasObligaciones = value; } }
        public decimal dTotalObligaciones { get { return _dTotalObligaciones; } set { _dTotalObligaciones = value; } }
        public decimal dEmpresaPagaAsimilados { get { return _dEmpresaPagaAsimilados; } set { _dEmpresaPagaAsimilados = value; } }
        public decimal dAsimilados { get { return _dAsimilados; } set { _dAsimilados = value; } }
        public decimal dISR { get { return _dISR; } set { _dISR = value; } }
        public decimal dTotalPagarAsimiladosSalario { get { return _dTotalPagarAsimiladosSalario; } set { _dTotalPagarAsimiladosSalario = value; } }
        public decimal dPrestamoCompania { get { return _dPrestamoCompania; } set { _dPrestamoCompania = value; } }
        public decimal dInteresesPrestamo { get { return _dInteresesPrestamo; } set { _dInteresesPrestamo = value; } }
        public decimal dRecuperacionAsesores { get { return _dRecuperacionAsesores; } set { _dRecuperacionAsesores = value; } }
        //public decimal dGastosNoComprobados { get { return _dGastosNoComprobados; } set { _dGastosNoComprobados = value; } }
        public decimal dOtros { get { return _dOtros; } set { _dOtros = value; } }
        public decimal dPensionAlimenticia2 { get { return _dPensionAlimenticia2; } set { _dPensionAlimenticia2 = value; } }
        public decimal dNetoPagarAsimilados { get { return _dNetoPagarAsimilados; } set { _dNetoPagarAsimilados = value; } }
        public decimal dSueldoIMSS { get { return _dSueldoIMSS; } set { _dSueldoIMSS = value; } }
        public decimal dAsimiladosSalarios { get { return _dAsimiladosSalarios; } set { _dAsimiladosSalarios = value; } }
        public decimal dCargaPatronal { get { return _dCargaPatronal; } set { _dCargaPatronal = value; } }
        public decimal dMARKUP7_5Porciento { get { return _dMARKUP7_5Porciento; } set { _dMARKUP7_5Porciento = value; } }
        public decimal dCostoEmpleado { get { return _dCostoEmpleado; } set { _dCostoEmpleado = value; } }
        public decimal dImporte { get { return _dImporte; } set { _dImporte = value; } }
        public decimal dIVA { get { return _dIVA; } set { _dIVA = value; } }
        public decimal dTotalFactura { get { return _dTotalFactura; } set { _dTotalFactura = value; } }
    }


    public class PolizaNominaNew : BaseObjeto
    {
        //private int _iIdFolio = 0;
        private string _sArchivo = string.Empty;
        private DateTime _dtFechaIni = DateTime.Now;
        private DateTime _dtFechaFin = DateTime.Now;
        private string _sUsuario = string.Empty;
        private List<DetallePolizaNominaNew> _oLstDetalle = new List<DetallePolizaNominaNew>();

        //public int iIdFolio { get { return _iIdFolio; } set { _iIdFolio = value; } }
        public string sArchivo { get { return _sArchivo; } set { _sArchivo = value; } }
        public DateTime dtFechaIni { get { return _dtFechaIni; } set { _dtFechaIni = value; } }
        public DateTime dtFechaFin { get { return _dtFechaFin; } set { _dtFechaFin = value; } }
        public string sUsuario { get { return _sUsuario; } set { _sUsuario = value; } }

        public List<DetallePolizaNominaNew> oLstDetalle { get { return _oLstDetalle; } set { _oLstDetalle = value; } }

    }

    [Serializable]
    public class DetallePolizaNominaNew
    {
        public int _iIdFolio = 0;
        public string _sPeriodo = string.Empty;
        public int IdCounter { set; get; }
        public string  EmpresaFacturante { set; get; }
        public string  RFCEmpresaFacturante { set; get; }
        public string  Periodo { set; get; }
        public string  NombreEmpleado { set; get; }
        public string  RFCEmpleado { set; get; }
        public decimal SalarioMensualBruto { set; get; }
        public decimal Vales { set; get; }
        public decimal HorasExtrasDobles { set; get; }
        public decimal HorasExtrasTriples { set; get; }                // NUEVO
        public decimal PagoPorIncapacidad { set; get; }
        public decimal Compensacion { set; get; }
        public decimal DescuentoPorFaltas { set; get; }           // NUEVO
        public decimal DiasLaborados { set; get; }                // NUEVO
        public decimal Retroactivo { set; get; }                  // NUEVO
        public decimal DiasFestivos { set; get; }
        public decimal SalarioDiarioR { set; get; }
        public decimal PrimaDominical { set; get; }
        public decimal AguinaldoReal { set; get; }                   // NUEVO
        public decimal PrimaVacacional { set; get; }              // NUEVO
        public decimal PrimaAntiguedad { set; get; }              // NOMBRE DIFERENTE
        public decimal Bono { set; get; }
        public decimal TotalIngresos { set; get; }
        public decimal SalarioDiarioFiscal { set; get; }
        public decimal SalarioIntegrado { set; get; }
        public decimal Sueldo { set; get; }
        public decimal SeptimoDia { set; get; }
        public decimal HorasExtras { set; get; }
        public decimal Destajos { set; get; }
        public decimal VacacionesATiempo { set; get; }              // NUEVO
        public decimal PrimaVacacionalReportada { set; get; }       // NUEVO
        public decimal Aguinaldo { set; get; }                      // NUEVO
        public decimal ValesGratificacion { set; get; }
        public decimal MinimoVital { set; get; }                    // NUEVO
        public decimal OtrasPercepciones { set; get; }
        public decimal TotalPercepciones { set; get; }
        public decimal RetiroInvalidezYVida { set; get; }
        public decimal RetencionCesantia { set; get; }
        public decimal RetiroEnfermedadYMaternidad { set; get; }
        public decimal SeguroViviendaInfonavit { set; get; }
        public decimal SubsidioEmpleoAcreditado { set; get; }
        public decimal SubsidioEmpleoSP { set; get; }
        public decimal ISRAntesDeSubsidioEmpleo { set; get; }
        public decimal ISRSP { set; get; }
        public decimal IMSS { set; get; }
        public decimal PrestamoInfonavit { set; get; }              // NUEVO
        public decimal AjusteNeto { set; get; }
        public decimal PensionAlimenticia { set; get; }
        public decimal OtrasDeducciones { set; get; }
        public decimal TotalDeducciones { set; get; }
        public decimal Neto { set; get; }
        public decimal RecuperacionPrestamo { set; get; }
        public decimal SaldoRecuperacionPrestamo { set; get; }
        public decimal PrestamoEmpresa { set; get; }
        public decimal SaldoPrestamoEmpresa { set; get; }
        public decimal InvalidezYVida { set; get; }
        public decimal CesantiaYVejez { set; get; }
        public decimal EnfermedadYMaternidadPatron { set; get; }
        public decimal GastosPensionados { set; get; }
        public decimal FondoRetiroSAR2 { set; get; }
        public decimal ImpuestoEstatal3 { set; get; }
        public decimal RiesgoDeTrabajo { set; get; }
        public decimal IMSSEmpresa { set; get; }
        public decimal InfonavitEmpresa { set; get; }
        public decimal GuarderiaIMSS { set; get; }              // NUEVO
        public decimal OtrasObligaciones { set; get; }
        public decimal TotalObligaciones { set; get; }
        public decimal SueldoIMSS { set; get; }
        public decimal MV { set; get; }
        public decimal CargaPatronal { set; get; }
        public decimal MarkUpPatronal { set; get; }
        public decimal MarkUpMV { set; get; }
        public decimal ISN { set; get; }
        public decimal SUBSIDIOCHEQUECERTIVA { set; get; }
        public decimal CostoEmpleado { set; get; }
        public decimal fill { set; get; }
        public decimal Importe { set; get; }
        public decimal IVA { set; get; }
        public decimal SubTotalFactura { set; get; }
        public decimal Retencion { set; get; }
        public decimal TotalFactura { set; get; }
        public decimal NumeroFactura { set; get; }
    }

    [Serializable]
    public class PolizaNominaSAP
    {
        private string _sEmpresa = string.Empty;
        private int _iId = 0;
        private string _sSucursal = string.Empty;
        private DateTime _dtFecha = DateTime.Now;
        private string _sCardCodeEmpresa = string.Empty;
        private string _sNoReporte = string.Empty;
        private string _sFormaPago = string.Empty;
        private string _sComentarios = string.Empty;
        private string _sSerie = string.Empty;
        private string _sMoneda = string.Empty;
        private decimal _dDescuento = 0;
        private decimal _dTipoCambio = 0;
        private string _sMsg = string.Empty;
        private List<ConceptosSAP> _oLstConceptos = new List<ConceptosSAP>();
        private EstatusPoliza _oEstatus = new EstatusPoliza();

        public string sEmpresa { get { return _sEmpresa; } set { _sEmpresa = value; } }
        public int iId { get { return _iId; } set { _iId = value; } }
        public string sSucursal { get { return _sSucursal; } set { _sSucursal = value; } }
        public DateTime dtFecha { get { return _dtFecha; } set { _dtFecha = value; } }
        public string sCardCodeEmpresa { get { return _sCardCodeEmpresa; } set { _sCardCodeEmpresa = value; } }
        public string sNoReporte { get { return _sNoReporte; } set { _sNoReporte = value; } }
        public string sFormaPago { get { return _sFormaPago; } set { _sFormaPago = value; } }
        public string sComentarios { get { return _sComentarios; } set { _sComentarios = value; } }
        public string sSerie { get { return _sSerie; } set { _sSerie = value; } }
        public string sMoneda { get { return _sMoneda; } set { _sMoneda = value; } }
        public decimal dDescuento { get { return _dDescuento; } set { _dDescuento = value; } }
        public decimal dTipoCambio { get { return _dTipoCambio; } set { _dTipoCambio = value; } }
        public string sMsg { get { return _sMsg; } set { _sMsg = value; } }
        public List<ConceptosSAP> oLstConceptos { get { return _oLstConceptos; } set { _oLstConceptos = value; } }
        public EstatusPoliza oEstatus { get { return _oEstatus; } set { _oEstatus = value; } }
    }

    [Serializable]
    public class ConceptosSAP
    {
        private long _lIdGasto = 0;
        private string _sEmpresa = string.Empty;
        private int _iId = 0;
        private int _iLinea = 0;
        private string _sItem = string.Empty;
        private string _sDescripcionUsuario = string.Empty;
        private int _iCantidad = 0;
        private decimal _dPrecio = 0;
        private decimal _dImpuesto = 0;
        private string _sCodigoImpuesto = string.Empty;
        private decimal _dDescuento = 0;
        private decimal _dTotalLinea = 0;
        private string _sCuenta = string.Empty;
        private string _sAlmacen = string.Empty;
        private string _sProyecto = string.Empty;
        private string _sDimension1 = string.Empty;
        private string _sDimension2 = string.Empty;
        private string _sDimension3 = string.Empty;
        private string _sDimension4 = string.Empty;
        private string _sDimension5 = string.Empty;
        private string _sDescripcionAmpliada = string.Empty;


        public long lIdGasto { get { return _lIdGasto; } set { _lIdGasto = value; } }
        public string sEmpresa { get { return _sEmpresa; } set { _sEmpresa = value; } }
        public int iId { get { return _iId; } set { _iId = value; } }
        public int iLinea { get { return _iLinea; } set { _iLinea = value; } }
        public string sItem { get { return _sItem; } set { _sItem = value; } }
        public string sDescripcionUsuario { get { return _sDescripcionUsuario; } set { _sDescripcionUsuario = value; } }
        public int iCantidad { get { return _iCantidad; } set { _iCantidad = value; } }
        public decimal dPrecio { get { return _dPrecio; } set { _dPrecio = value; } }
        public decimal dImpuesto { get { return _dImpuesto; } set { _dImpuesto = value; } }
        public string sCodigoImpuesto { get { return _sCodigoImpuesto; } set { _sCodigoImpuesto = value; } }
        public decimal dDescuento { get { return _dDescuento; } set { _dDescuento = value; } }
        public decimal dTotalLinea { get { return _dTotalLinea; } set { _dTotalLinea = value; } }
        public string sCuenta { get { return _sCuenta; } set { _sCuenta = value; } }
        public string sAlmacen { get { return _sAlmacen; } set { _sAlmacen = value; } }
        public string sProyecto { get { return _sProyecto; } set { _sProyecto = value; } }
        public string sDimension1 { get { return _sDimension1; } set { _sDimension1 = value; } }
        public string sDimension2 { get { return _sDimension2; } set { _sDimension2 = value; } }
        public string sDimension3 { get { return _sDimension3; } set { _sDimension3 = value; } }
        public string sDimension4 { get { return _sDimension4; } set { _sDimension4 = value; } }
        public string sDimension5 { get { return _sDimension5; } set { _sDimension5 = value; } }
        public string sDescripcionAmpliada { get { return _sDescripcionAmpliada; } set { _sDescripcionAmpliada = value; } }
    }

    [Serializable]
    public class EstatusPoliza
    {
        private int _iEstatus = 0;
        private string _sMensaje = string.Empty;
        private int _iSapDoc = 0;
        private string _sTabla = string.Empty;
        private int _iID = 0;

        public int iEstatus { set { _iEstatus = value; } get { return _iEstatus; } }
        public string sMensaje { set { _sMensaje = value; } get { return _sMensaje; } }
        public int iSapDoc { set { _iSapDoc = value; } get { return _iSapDoc; } }
        public string sTabla { set { _sTabla = value; } get { return _sTabla; } }
        public int iID { set { _iID = value; } get { return _iID; } }
    }

}