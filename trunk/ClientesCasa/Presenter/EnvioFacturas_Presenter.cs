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
    public class EnvioFacturas_Presenter : BasePresenter<IViewEnvioFacturas>
    {
        private readonly DBAccesoSAP oIClientesCat;

        public EnvioFacturas_Presenter(IViewEnvioFacturas oView, DBAccesoSAP oGC)
            : base(oView) 
        {
            oIClientesCat = oGC;

            oIView.eSearchFacturas += SearchFacturas_Presenter;
            oIView.eSearchMatriculasSAP += SearchMatriculasSAP_Presenter;
            oIView.eSearchMatriculasMXJ += SearchMatriculasMXJ_Presenter;
            oIView.eSearchFlotasMXJ += SearchFlotasMXJ_Presenter;
            oIView.eSearchMatriculas += SearchMatriculas_Presenter;
            oIView.eProcesarFacturas += ProcesarFacturas_Presenter;
            oIView.eSearchFacturasPart += SearchFacturasPart_Presenter;
            oIView.eSearchArticulos += SearchArticulos_Presenter;
            oIView.eProcesarArticulos += ProcesarArticulos_Presenter;
            oIView.eGetValidaPDF += eGetValidaPDF_Presenter;
            oIView.eSearchTipoMtto += SearchTipoMtto_Presenter;
        }

        protected void SearchFacturas_Presenter(object sender, EventArgs e)
        {
            //oIView.LoadFacturas(new DBAccesoSAPTaller().DBGetObtieneFacturas(oIView.sNumDoc, oIView.sFechaDesde, oIView.sFechaHasta));
            oIView.LoadFacturas(new DBEnvioFacturas().ObtieneFacturasASC(oIView.sNumDoc, oIView.sFechaDesde, oIView.sFechaHasta, oIView.sTipoMtto, oIView.sEstatus, oIView.sFlota, oIView.sMatricula));
        }

        protected void SearchTipoMtto_Presenter(object sender, EventArgs e)
        {
            oIView.LoadTipoMtto(new DBAccesoSAP().DBGetObtieneTiposMtto());
        }

        protected void SearchMatriculasSAP_Presenter(object sender, EventArgs e)
        {
            oIView.LoadMatriculasSAP(new DBAccesoSAP().DBGetObtieneMatriculasSAP());
        }

        protected void SearchFlotasMXJ_Presenter(object sender, EventArgs e)
        {
            //oIView.LoadFlotas(new DBEnvioFacturas().DBGetObtieneFlotas());
        }

        protected void SearchMatriculasMXJ_Presenter(object sender, EventArgs e)
        {
            oIView.LoadMatriculasMXJ(new DBEnvioFacturas().DBGetObtieneMatriculasMXJ());
        }

        protected void SearchMatriculas_Presenter(object sender, EventArgs e)
        {
            oIView.LoadMatriculas(new DBAccesoSAP().DBGetObtieneMatriculas());
        }

        protected void ProcesarFacturas_Presenter(object sender, EventArgs e)
        {
            foreach (FacturaASC oF in oIView.ListaFacturas)
            {
                if (new DBEnvioFacturas().DBSetActualizaFacturasHeader(oF))
                {
                    new DBEnvioFacturas().DBSetInsertaFacturasDetalle(oF.oLstConceptos);
                    oIView.iBanCorrecto = 1;
                }
            }
        }

        protected void SearchFacturasPart_Presenter(object sender, EventArgs e)
        {
            oIView.LoadFacturasPart(new DBEnvioFacturas().ObtieneFacturasParticionesASC(oIView.sNumDoc));
        }

        protected void SearchArticulos_Presenter(object sender, EventArgs e)
        {
            oIView.LoadListaArticulos(new DBEnvioFacturas().ObtieneArticulos());
        }

        protected void ProcesarArticulos_Presenter(object sender, EventArgs e) 
        {
            if (new DBEnvioFacturas().DBInsertaArticulo(oIView.iIdDetalle, oIView.iDocEntry, oIView.sItemCode, oIView.sArticulo, oIView.sNvoImporte))
            {
                oIView.iBanCorrecto = 1;
            }
        }

        protected void eGetValidaPDF_Presenter(object sender, EventArgs e)
        {
            oIView.sCadArchivo = ArmaRuta(2, oIView.sFechaDoc, oIView.sArchivo);
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

                sCadFinal = sRaiz + sFolderNalExt + @"\" + sAnio + @"\Proveedores " + sFolder2 + " " +
                        "TLC" + @"\" + sCardName + @"\" + sMes + @"\" + sFileName + sTipoDoc;

            }

            return sCadFinal;
        }
    }
}