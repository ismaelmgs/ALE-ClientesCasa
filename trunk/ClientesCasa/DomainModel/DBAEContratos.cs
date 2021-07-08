using ClientesCasa.Clases;
using ClientesCasa.Objetos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NucleoBase.Core;
using System.Data;

namespace ClientesCasa.DomainModel
{
    public class DBAEContratos : DBBase
    {
        public int DBSaveGenerales(ClienteContrato oContrato)
        {
            try
            {
                object oRes = oDB_SP.EjecutarValor("[ClientesCasa].[spI_CC_InsertaContratoCC]", "@IdCliente", oContrato.iIdCliente,
                                                                                                "@ClaveContrato", oContrato.sClaveContrato,
                                                                                                "@FechaContrato", oContrato.dtFechaContrato,
                                                                                                "@HorasContratadasTotal", oContrato.iHorasContratadas,
                                                                                                "@Matricula", oContrato.sAeronaveMatricula,
                                                                                                "@PorcParticipacion", oContrato.iPorcentajePart,
                                                                                                "@AplicaIntercambio", oContrato.bAplicaIntercambios,
                                                                                                "@FactorIntercambio", oContrato.iFactorIntercambio,
                                                                                                "@TiempoFacturar", oContrato.iTipoCosto,
                                                                                                "@UsuarioCreacion", Utils.GetUser,
                                                                                                "@IP", Utils.GetIPAddress);
                int iRes = 0;
                if (oRes.S().I() > 0)
                {
                    oContrato.iIdContrato = oRes.S().I();
                    iRes = new DBContratos().DBSetInsertaAdicionalesContratoCC(oContrato);
                }
                return iRes > 0 ? 1 : 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int DBActualizaContrato(ClienteContrato oContrato)
        {
            try
            {
                object oRes = oDB_SP.EjecutarValor("[ClientesCasa].[spU_CC_ActualizaContrato]", "@IdContrato", oContrato.sIdContrato,
                                                                                                "@ClaveContrato", oContrato.sClaveContrato,
                                                                                                "@FechaContrato", oContrato.dtFechaContrato,
                                                                                                "@HorasContratadasTotal", oContrato.iHorasContratadas,
                                                                                                "@Matricula", oContrato.sAeronaveMatricula,
                                                                                                "@PorcParticipacion", oContrato.iPorcentajePart,
                                                                                                "@AplicaIntercambio", oContrato.bAplicaIntercambios,
                                                                                                "@FactorIntercambio", oContrato.iFactorIntercambio,
                                                                                                "@TiempoFacturar", oContrato.iTipoCosto,
                                                                                                "@UsuarioModificacion", Utils.GetUser,
                                                                                                "@IP", Utils.GetIPAddress);

                int iRes = 0;

                if (oRes.S().I() > 0)
                {
                    oContrato.iIdContrato = oContrato.sIdContrato.S().I();
                    iRes = new DBContratos().DBSetActualizaAdicionalesContratoCC(oContrato);
                }

                return iRes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int DBSaveTarifa(Contrato_Tarifas objTarifa)
        {
            try
            {
                object oResult;
                oResult = oDB_SP.EjecutarValor("[Principales].[spI_MXJ_InsertaTarifasContrato]", "@IdContrato", objTarifa.iIdContrato,
                                                                                               "@CostoDirectoNalV", objTarifa.dCostoDirNal,
                                                                                                "@CostoDirectoIntV", objTarifa.dCostoDirInt,
                                                                                                "@SeCobraCombustibleV", objTarifa.bCombustible,
                                                                                                "@CalculoPrecioCombuV", objTarifa.iTipoCalculo,
                                                                                                "@ConsumoGalonesHrV", objTarifa.dConsumoGalones,
                                                                                                "@FactorTramosNal", objTarifa.dFactorTramosNal,
                                                                                                "@FactorTramosInt", objTarifa.dFactorTramosInt,
                                                                                                "@CombustibleIntEspV", objTarifa.bPrecioInternacionalEspecial,
                                                                                                "@SeCobraTE", objTarifa.bCobraTiempoEspera,
                                                                                                "@TarifaNacionalTE", objTarifa.dTiempoEsperaFijaNal,
                                                                                                "@TarifaInteracionalTE", objTarifa.dTiempoEsperaFijaInt,
                                                                                                "@PorcentajeTarifaTE", objTarifa.dTiempoEsperaVarNal,
                                                                                                "@PorcentajeInternacionalTE", objTarifa.dTiempoEsperaVarInt,
                                                                                                "@SeCobraPernoctaP", objTarifa.bCobraPernoctas,
                                                                                                "@TarifaNacionalP", objTarifa.dPernoctasFijaNal,
                                                                                                "@TarifaInternacionalP", objTarifa.dPernoctasFijaInt,
                                                                                                "@PorcentajeTarifaP", objTarifa.dPernoctasVarNal,
                                                                                                "@PorcentajeInternacionalP", objTarifa.dPernoctasVarInt,
                                                                                                "@CostoDirectoNalInflacion", 0,
                                                                                                "@CostoDirectoIntInflacion", 0,
                                                                                                "@FijoAnualInflacion", 0,
                                                                                                "@CostoDirectoNalPorcentaje", 0,
                                                                                                "@CostoDirectoIntPorcentaje", 0,
                                                                                                "@FijoAnualPorcentaje", 0,
                                                                                                "@CostoDirectoNalMasPuntos", 0,
                                                                                                "@CostoDirectoIntMasPuntos", 0,
                                                                                                "@FijoAnualMasPuntos", 0,
                                                                                                "@CostoDirectoNalTope", 0,
                                                                                                "@CostoDirectoIntTope", 0,
                                                                                                "@FijoAnualTope", 0,
                                                                                                "@Notas", string.Empty,
                                                                                                "@AplicaIncremento", 3,
                                                                                                "@Status", 1,
                                                                                                "@UsuarioCreacion", Utils.GetUser,
                                                                                                "@IP", string.Empty);

                return oResult.I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Contrato_Tarifas DBGetTarifa(int iIdContrato)
        {
            try
            {
                DataTable dtResult;
                Contrato_Tarifas objGenerales = new Contrato_Tarifas();
                dtResult = oDB_SP.EjecutarDT("[Principales].[spS_MXJ_ConsultaTarifasContrato]", "@IdContrato", iIdContrato);
                foreach (DataRow dr in dtResult.Rows)
                {
                    objGenerales.iIdContrato = iIdContrato;
                    objGenerales.dCostoDirNal = dr.ItemArray[1].S().D();
                    objGenerales.dCostoDirInt = dr.ItemArray[2].S().D();
                    objGenerales.bCombustible = dr.ItemArray[3].S().I() > 0;
                    objGenerales.iTipoCalculo = dr.ItemArray[4].S().I();
                    objGenerales.dConsumoGalones = dr.ItemArray[5].S().D();
                    objGenerales.bPrecioInternacionalEspecial = dr.ItemArray[6].S().I() > 0;

                    objGenerales.bCobraTiempoEspera = dr.ItemArray[7].S().I() > 0;
                    objGenerales.dTiempoEsperaFijaNal = dr.ItemArray[8].S().D();
                    objGenerales.dTiempoEsperaFijaInt = dr.ItemArray[9].S().D();
                    objGenerales.dTiempoEsperaVarNal = dr.ItemArray[10].S().D();
                    objGenerales.dTiempoEsperaVarInt = dr.ItemArray[11].S().D();

                    objGenerales.bCobraPernoctas = dr.ItemArray[12].S().I() > 0;
                    objGenerales.dPernoctasFijaNal = dr.ItemArray[13].S().D();
                    objGenerales.dPernoctasFijaInt = dr.ItemArray[14].S().D();
                    objGenerales.dPernoctasVarNal = dr.ItemArray[15].S().D();
                    objGenerales.dPernoctasVarInt = dr.ItemArray[16].S().D();

                    //objGenerales.iCDNBaseInflacion = dr.ItemArray[17].S().I();
                    //objGenerales.iCDIBaseInflacion = dr.ItemArray[18].S().I();
                    //objGenerales.iFABaseInflacion = dr.ItemArray[19].S().I();

                    //objGenerales.dCDNPorcentaje = dr.ItemArray[20].S().D();
                    //objGenerales.dCDIPorcentaje = dr.ItemArray[21].S().D();
                    //objGenerales.dFAPorcentaje = dr.ItemArray[22].S().D();

                    //objGenerales.dCDNPuntos = dr.ItemArray[23].S().D();
                    //objGenerales.dCDIPuntos = dr.ItemArray[24].S().D();
                    //objGenerales.dFAPuntos = dr.ItemArray[25].S().D();

                    //objGenerales.dCDNTopeMAximo = dr.ItemArray[26].S().D();
                    //objGenerales.dCDITopeMAximo = dr.ItemArray[27].S().D();
                    //objGenerales.dFATopeMAximo = dr.ItemArray[28].S().D();

                    //objGenerales.sNotas = dr.ItemArray[29].S();
                    //objGenerales.iAplicaIncremento = dr.ItemArray[30].S().I();

                    objGenerales.dFactorTramosNal = dr.S("FactorTramosNal").D();
                    objGenerales.dFactorTramosInt = dr.S("FactorTramosInt").D();
                }
                return objGenerales;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable DBGetTarifasContrato(int iIdContrato)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Principales].[spS_MXJ_ConsultaTarifasContrato]", "@IdContrato", iIdContrato);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        public int DBUpdateTarifasContrato(Contrato_Tarifas oTarifas)
        {
            try
            {
                object oResult;
                oResult = oDB_SP.EjecutarValor("[Principales].[spU_MXJ_ActualizaTarifasContrato]", "@IdContrato", oTarifas.iIdContrato,
                                                                                                "@CostoDirectoNalV", oTarifas.dCostoDirNal,
                                                                                                "@CostoDirectoIntV", oTarifas.dCostoDirInt,
                                                                                                "@SeCobraCombustibleV", oTarifas.bCombustible,
                                                                                                "@CalculoPrecioCombuV", oTarifas.iTipoCalculo,
                                                                                                "@ConsumoGalonesHrV", oTarifas.dConsumoGalones,
                                                                                                "@FactorTramosNal", oTarifas.dFactorTramosNal,
                                                                                                "@FactorTramosInt", oTarifas.dFactorTramosInt,
                                                                                                "@CombustibleIntEspV", oTarifas.bPrecioInternacionalEspecial,
                                                                                                "@SeCobraTE", oTarifas.bCobraTiempoEspera,
                                                                                                "@TarifaNacionalTE", oTarifas.dTiempoEsperaFijaNal,
                                                                                                "@TarifaInteracionalTE", oTarifas.dTiempoEsperaFijaInt,
                                                                                                "@PorcentajeTarifaTE", oTarifas.dTiempoEsperaVarNal,
                                                                                                "@PorcentajeInternacionalTE", oTarifas.dTiempoEsperaVarInt,
                                                                                                "@SeCobraPernoctaP", oTarifas.bCobraPernoctas,
                                                                                                "@TarifaNacionalP", oTarifas.dPernoctasFijaNal,
                                                                                                "@TarifaInternacionalP", oTarifas.dPernoctasFijaInt,
                                                                                                "@PorcentajeTarifaP", oTarifas.dPernoctasVarNal,
                                                                                                "@PorcentajeInternacionalP", oTarifas.dPernoctasVarInt,
                                                                                                "@CostoDirectoNalInflacion", 0,
                                                                                                "@CostoDirectoIntInflacion", 0,
                                                                                                "@FijoAnualInflacion", 0,
                                                                                                "@CostoDirectoNalPorcentaje", 0,
                                                                                                "@CostoDirectoIntPorcentaje", 0,
                                                                                                "@FijoAnualPorcentaje", 0,
                                                                                                "@CostoDirectoNalMasPuntos", 0,
                                                                                                "@CostoDirectoIntMasPuntos", 0,
                                                                                                "@FijoAnualMasPuntos", 0,
                                                                                                "@CostoDirectoNalTope", 0,
                                                                                                "@CostoDirectoIntTope", 0,
                                                                                                "@FijoAnualTope", 0,
                                                                                                "@Notas", string.Empty,
                                                                                                "@AplicaIncremento", 3,
                                                                                                "@Status", 1,
                                                                                                "@UsuarioModificacion", Utils.GetUser,
                                                                                                "@IP", string.Empty);

                return oResult.I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int DBSaveCobros(Contrato_CobrosDescuentos objCobros)
        {
            try
            {
                object oResult;
                oResult = oDB_SP.EjecutarValor("[Principales].[spI_MXJ_InsertaCobrosDescuentosContrato]", "@IdContrato", objCobros.iIdContrato,
                                                                                                            "@IdFerryConCargo", objCobros.iFerrysConCargo,
                                                                                                            "@EsperalLibre", objCobros.bAplicaEsperaLibre == true ? 1 : 0,
                                                                                                            "@HorasPorVuelo", objCobros.dHorasVuelo,
                                                                                                            "@FactorHrVuelo", objCobros.dFactorHorasVuelo,
                                                                                                            "@SeDescuentaNalP", objCobros.bPernoctaNal == true ? 1 : 0,
                                                                                                            "@SeDescuentaIntP", objCobros.bPernoctaInt == true ? 1 : 0,
                                                                                                            "@FactorConvHrVueloNalP", objCobros.dPernoctaFactorConversionNal,
                                                                                                            "@FactorConvHrVueloIntP", objCobros.dPernoctaFactorConversionInt,
                                                                                                            "@NumPernoctaLibresAnioP", objCobros.dNumeroPernoctasLibreAnual,
                                                                                                            "@CobroP", objCobros.bPernoctasCobro == true ? 1 : 0,
                                                                                                            "@DescuentoP", objCobros.bPernoctasDescuento == true ? 1 : 0,
                                                                                                            "@SeDescuentaNalTE", objCobros.bTiempoEsperaNal == true ? 1 : 0,
                                                                                                            "@SeDescuentaIntTE", objCobros.bTiempoEsperaInt == true ? 1 : 0,
                                                                                                            "@FactorConvHrVueloNal", objCobros.dTiempoEsperaFactorConversionNal,
                                                                                                            "@FactorConvHrVueloInt", objCobros.dTiempoEsperaFactorConversionInt,
                                                                                                            "@TiempoFacturar", objCobros.iTiempoFatura,
                                                                                                            "@MasMinutos", objCobros.dMinutos,
                                                                                                            "@AplicaTramos", objCobros.bAplicaTramos,
                                                                                                            "@Notas", string.Empty,
                                                                                                            "@Status", 1,
                                                                                                            "@UsuarioCreacion", Utils.GetUser,
                                                                                                            "@IP", string.Empty);

                return oResult.I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Contrato_CobrosDescuentos DBGetCobros(int iIdContrato)
        {
            try
            {
                DataTable dtResult;
                Contrato_CobrosDescuentos objGenerales = new Contrato_CobrosDescuentos();
                dtResult = oDB_SP.EjecutarDT("[Principales].[spS_MXJ_ConsultaCobrosDescuentosContrato]", "@IdContrato", iIdContrato);
                foreach (DataRow dr in dtResult.Rows)
                {
                    objGenerales.iIdContrato = iIdContrato;
                    objGenerales.iFerrysConCargo = dr.ItemArray[1].S().I();
                    objGenerales.bAplicaEsperaLibre = dr.ItemArray[2].S().I() > 0;
                    objGenerales.dHorasVuelo = dr.ItemArray[3].S().D();
                    objGenerales.dFactorHorasVuelo = dr.ItemArray[4].S().D();

                    objGenerales.bPernoctaNal = dr.ItemArray[5].S().I() > 0;
                    objGenerales.bPernoctaInt = dr.ItemArray[6].S().I() > 0;
                    objGenerales.dPernoctaFactorConversionNal = dr.ItemArray[7].S().D();
                    objGenerales.dPernoctaFactorConversionInt = dr.ItemArray[8].S().D();
                    objGenerales.dNumeroPernoctasLibreAnual = dr.ItemArray[9].S().D();
                    objGenerales.bPernoctasCobro = dr.ItemArray[10].B();
                    objGenerales.bPernoctasDescuento = dr.ItemArray[11].B();

                    objGenerales.bTiempoEsperaNal = dr.ItemArray[12].S().I() > 0;
                    objGenerales.bTiempoEsperaInt = dr.ItemArray[13].S().I() > 0;
                    objGenerales.dTiempoEsperaFactorConversionNal = dr.ItemArray[14].S().D();
                    objGenerales.dTiempoEsperaFactorConversionInt = dr.ItemArray[15].S().D();
                    objGenerales.iTiempoFatura = dr.ItemArray[16].S().I();
                    objGenerales.dMinutos = dr.ItemArray[17].S().D();
                    objGenerales.bAplicaTramos = dr.ItemArray[18].B();
                    objGenerales.sNotas = dr.ItemArray[19].S();
                }

                DataTable dtResultServicios;
                int iIDServicio = -1;
                dtResultServicios = oDB_SP.EjecutarDT("[Principales].[spS_MXJ_ConsultaServiciosCargoContrato]", "@IdContrato", iIdContrato);
                foreach (DataRow dr in dtResultServicios.Rows)
                {
                    iIDServicio = 0;
                    iIDServicio = dr.ItemArray[1].S().I();
                    objGenerales.lstIdServiciosConCargo.Add(iIDServicio);
                }


                return objGenerales;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable DBGetCobrosDescuentosContrato(int iIdContrato)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Principales].[spS_MXJ_ConsultaCobrosDescuentosContrato]", "@IdContrato", iIdContrato);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        public int DBUpdateCobros(Contrato_CobrosDescuentos objCobros)
        {
            try
            {
                object oResult;
                oResult = oDB_SP.EjecutarValor("[Principales].[spU_MXJ_ActualizaCobrosDescuentosContrato]", "@IdContrato", objCobros.iIdContrato,
                                                                                                            "@IdFerryConCargo", objCobros.iFerrysConCargo,
                                                                                                            "@EsperalLibre", objCobros.bAplicaEsperaLibre == true ? 1 : 0,
                                                                                                            "@HorasPorVuelo", objCobros.dHorasVuelo,
                                                                                                            "@FactorHrVuelo", objCobros.dFactorHorasVuelo,
                                                                                                            "@SeDescuentaNalP", objCobros.bPernoctaNal == true ? 1 : 0,
                                                                                                            "@SeDescuentaIntP", objCobros.bPernoctaInt == true ? 1 : 0,
                                                                                                            "@FactorConvHrVueloNalP", objCobros.dPernoctaFactorConversionNal,
                                                                                                            "@FactorConvHrVueloIntP", objCobros.dPernoctaFactorConversionInt,
                                                                                                            "@NumPernoctaLibresAnioP", objCobros.dNumeroPernoctasLibreAnual,
                                                                                                            "@CobroP", objCobros.bPernoctasCobro == true ? 1 : 0,
                                                                                                            "@DescuentoP", objCobros.bPernoctasDescuento == true ? 1 : 0,
                                                                                                            "@SeDescuentaNalTE", objCobros.bTiempoEsperaNal == true ? 1 : 0,
                                                                                                            "@SeDescuentaIntTE", objCobros.bTiempoEsperaInt == true ? 1 : 0,
                                                                                                            "@FactorConvHrVueloNal", objCobros.dTiempoEsperaFactorConversionNal,
                                                                                                            "@FactorConvHrVueloInt", objCobros.dTiempoEsperaFactorConversionInt,
                                                                                                            "@TiempoFacturar", objCobros.iTiempoFatura,
                                                                                                            "@MasMinutos", objCobros.dMinutos,
                                                                                                            "@AplicaTramos", objCobros.bAplicaTramos,
                                                                                                            "@Notas", objCobros.sNotas,
                                                                                                            "@Status", 1,
                                                                                                            "@UsuarioModificacion", Utils.GetUser,
                                                                                                            "@IP", string.Empty);

                return oResult.I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int DBSaveIntercambio(Contrato_Intercambios objIntercambio)
        {
            try
            {
                object oResult;
                oResult = oDB_SP.EjecutarValor("[Principales].[spI_MXJ_InsertaIntercambioContrato]", "@IdContrato", objIntercambio.iIdContrato,
                                                                                                    "@IdGrupoModelo", objIntercambio.iIdGrupoModelo,
                                                                                                    "@AplicaFerry", objIntercambio.bAplicaFerry,
                                                                                                    "@Factor", objIntercambio.dFactor,
                                                                                                    "@TarifaNal", objIntercambio.dTarifaNalDlls,
                                                                                                    "@TarifaInt", objIntercambio.dTarifaIntDlls,
                                                                                                    "@Galones", objIntercambio.dGalones,
                                                                                                    "@CostoDirectoNal", objIntercambio.dCDN,
                                                                                                    "@CostoDirectoInt", objIntercambio.dCDI,
                                                                                                    "@Status", 1,
                                                                                                    "@UsuarioCreacion", Utils.GetUser,
                                                                                                    "@IP", string.Empty);

                return oResult.I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable DBGetIntercambios(int iIdContrato)
        {
            try
            {
                return oDB_SP.EjecutarDT("[Principales].[spS_MXJ_ConsultaIntercambioContrato]", "@IdContrato", iIdContrato);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        public int DBUpdateIntercambioContratos(Contrato_Intercambios ointercambios)
        {
            try
            {
                object oResult;
                oResult = oDB_SP.EjecutarValor("[Principales].[spU_MXJ_ActualizaIntercambioContrato]", "@IdIntercambio", ointercambios.iId,
                                                                                                       "@IdContrato", ointercambios.iIdContrato,
                                                                                                       "@IdGrupoModelo", ointercambios.iIdGrupoModelo,
                                                                                                       "@AplicaFerry", ointercambios.bAplicaFerry,
                                                                                                       "@Factor", ointercambios.dFactor,
                                                                                                       "@TarifaNal", ointercambios.dTarifaNalDlls,
                                                                                                       "@TarifaInt", ointercambios.dTarifaIntDlls,
                                                                                                       "@Galones", ointercambios.dGalones,
                                                                                                       "@CostoDirectoNal", ointercambios.dCDN,
                                                                                                        "@CostoDirectoInt", ointercambios.dCDI,
                                                                                                        "@Status", 1,
                                                                                                        "@UsuarioModificacion", Utils.GetUser,
                                                                                                        "@IP", null);

                return oResult.I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int DBDeleteIntercambio(Contrato_Intercambios oIntercambio)
        {
            try
            {
                object oResult;
                oResult = oDB_SP.EjecutarValor("[Principales].[spD_MXJ_EliminaIntercambioContrato]", "@IdIntercambio", oIntercambio.iId,
                                                                                                       "@IdContrato", oIntercambio.iIdContrato);

                return oResult.I();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}