using ClientesCasa.DomainModel;
using ClientesCasa.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using NucleoBase.Core;

namespace ClientesCasa.Presenter
{
    public class MttoPDF_Presenter : BasePresenter<IViewMttoPDF>
    {
        private readonly DBMttoPDF oIClientesCat;

        public MttoPDF_Presenter(IViewMttoPDF oView, DBMttoPDF oCI)
            : base(oView)
        {
            oIClientesCat = oCI;

            oIView.eSearchInfoPoliza += SearchInfoPoliza_Presenter;
            oIView.eSearchDetailReferencia += eSearchDetailReferencia_Presenter;
        }

        protected void SearchInfoPoliza_Presenter(object sender, EventArgs e)
        {
            oIView.LoadInfoPoliza(new DBMttoPDF().DBGetInfoPoliza(oIView.sReferencia));
        }

        protected void eSearchDetailReferencia_Presenter(object sender, EventArgs e)
        {
            DataTable dt = oIClientesCat.DBGetDetalleReferencia(oIView.sReferencia);

            if (dt.Rows.Count > 0)
            {
                oIView.iMesRef = dt.Rows[0]["Mes"].S().I();
                oIView.iAnioRef = dt.Rows[0]["Anio"].S().I();
                oIView.sMatriculaRef = dt.Rows[0]["Matricula"].S();
                oIView.sMonedaRef = dt.Rows[0]["TipoMoneda"].S();

                oIView.LoadReferencia(true);
            }
            else
                oIView.LoadReferencia(false);

        }
    }
}