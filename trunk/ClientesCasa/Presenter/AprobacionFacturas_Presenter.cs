using ClientesCasa.Clases;
using ClientesCasa.DomainModel;
using ClientesCasa.Interfaces;
using ClientesCasa.Objetos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NucleoBase.Core;
using System.Configuration;
using System.IO;
using System.Threading;
using System.Globalization;

namespace ClientesCasa.Presenter
{
    public class AprobacionFacturas_Presenter : BasePresenter<IViewAprobacionFacturas>
    {
        private readonly DBAccesoSAP oIClientesCat;

        public AprobacionFacturas_Presenter(IViewAprobacionFacturas oView, DBAccesoSAP oGC)
            : base(oView) 
        {
            oIClientesCat = oGC;
            oIView.eSearchFacturasAprobar += SearchFacturasAprobar_Presenter;
            oIView.eSearchDetalleFac += SearchDetalleFac_Presenter;
            oIView.eUpdateFacturas += UpdateFacturas_Presenter;
            oIView.eSetProcesarAprobacion += SetProcesarAprobacion_Presenter;
            oIView.eGetValidaPDF += eGetValidaPDF_Presenter;
            oIView.eGetValidaXML += eGetValidaXML_Presenter;
            oIView.eSearchFacturasPart += SearchFacturasPart_Presenter;
        }

        protected void SearchFacturasAprobar_Presenter(object sender, EventArgs e)
        {
            oIView.LoadFacturasASC(new DBEnvioFacturas().ObtieneFacturasAprobacionASC(oIView.sNumDoc, oIView.sFechaDesde, oIView.sFechaHasta, oIView.sTipoMtto, oIView.sFlota));
        }

        protected void SearchDetalleFac_Presenter(object sender, EventArgs e)
        {
            oIView.LoadDetalleFacturasASC(new DBEnvioFacturas().ObtieneFacturasDetalleASC(oIView.iDocEntry.S()));
        }

        protected void UpdateFacturas_Presenter(object sender, EventArgs e)
        {
            if (new DBEnvioFacturas().DBActualizaFacturasAprobacion(oIView.iDocEntry, Utils.GetUser))
            {
                oIView.iBanCorrecto = 1;
            }
        }

        protected void SetProcesarAprobacion_Presenter(object sender, EventArgs e) 
        {
            try
            {
                foreach (FacturaAprobacionASC oF in oIView.ListaFacturas)
                {
                    int iIdentity = 0;
                    iIdentity = new DBEnvioFacturas().DBInsertaFacturasHeader(oF, 0);

                    if (iIdentity > 0)
                    {
                        new DBEnvioFacturas().DBSetInsertaFacturaDetalleASC(oF.oLstConceptos, iIdentity);
                        oIView.iBanCorrecto = 1;
                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void eGetValidaPDF_Presenter(object sender, EventArgs e)
        {
            oIView.sCadArchivo = ArmaRuta(2, oIView.sFechaDoc, oIView.sArchivo);
        }

        protected void eGetValidaXML_Presenter(object sender, EventArgs e)
        {
            oIView.sCadArchivo = ArmaRuta(1, oIView.sFechaDoc, oIView.sArchivo);
        }

        private string ArmaRuta(int iOpcion, string sFecha, string sArchivo)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("es-ES");
            string sTipoDoc = string.Empty;
            if (iOpcion == 1)
                sTipoDoc = ".xml";
            else
                sTipoDoc = ".pdf";

            string sCadFinal = string.Empty;
            string sFileName = sArchivo;

            if (sFecha != string.Empty)
            {
                string[] sValores = Convert.ToDateTime(sFecha).ToString("dd/MM/yyyy").Split('/'); //sFecha.Dt().ToShortDateString().ToString("dd-MM-yyyy").Replace("/", "-").Split('-');
                string sRaiz = ConfigurationManager.AppSettings["RutaArchivos"].S();
                string sCardCode = ConfigurationManager.AppSettings["CardCodeASC"].S();

                string sGroupCode = new DBAccesoSAP().DBGetObtieneGruopCodeProveedor(sCardCode).Rows[0][0].S();
                string sCardName = new DBAccesoSAP().DBGetObtieneGruopCodeProveedor(sCardCode).Rows[0][1].S();
                string sFolderNalExt = string.Empty;
                string sFolder2 = string.Empty;

                switch (sGroupCode)
                {
                    // Nacionales
                    case "102":
                        sFolder2 = "nacionales";
                        sFolderNalExt = ConfigurationManager.AppSettings["FolderNacional"].S();
                        break;
                    // Extranjeros
                    case "103":
                        sFolder2 = "extranjeros";
                        sFolderNalExt = ConfigurationManager.AppSettings["FolderExtranjero"].S();
                        break;
                }

                string sAnio = sValores[2].S();
                string sMes = string.Empty;

                if (sValores[1].Length == 1)
                    sMes = "0" + sValores[1].S();
                else
                    sMes = sValores[1].S();

                //sCadFinal = sRaiz + @"\" + sAnio + "-" + sMes + @"\" + sProveedor + @"\" + sCarpetaFac + @"\" + sFileName + sTipoDoc;

                sCadFinal = sRaiz + sFolderNalExt + @"\" + sAnio + @"\Proveedores " + sFolder2 + " " +
                        "TLC" + @"\" + sCardName + @"\" + sMes + @"\" + sFileName + sTipoDoc;

            }

            return sCadFinal;
        }

        protected void SearchFacturasPart_Presenter(object sender, EventArgs e)
        {
            oIView.LoadFacturasPart(new DBEnvioFacturas().ObtieneFacturasParticionesASC(oIView.sNumDoc));
        }

    }
}