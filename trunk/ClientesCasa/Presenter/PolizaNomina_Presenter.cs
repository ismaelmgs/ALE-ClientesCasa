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
using System.Data;

namespace ClientesCasa.Presenter
{
    public class PolizaNomina_Presenter : BasePresenter<IViewPolizaNomina>
    {
        private readonly DBPolizaNomina oIClientesCat;

        public PolizaNomina_Presenter(IViewPolizaNomina oView, DBPolizaNomina oGC) : base(oView)
        {
            oIClientesCat = oGC;
            oIView.eNewProcesaArchivo += NewProcesaArchivo_Presenter;
            oIView.eSearchRFC += SearchRFC_Presenter;
            //oIView.eDeleteObj += eDeleteObj_Presenter;
        }

        protected void NewProcesaArchivo_Presenter(object sender, EventArgs e)
        {
            foreach (PolizaNominaNew oP in oIView.ListaPolizas)
            {
                int identity = 0;
                identity = new DBPolizaNomina().DBInsertaRegistrosHeader(oP);

                if (identity > 0)
                {
                    if (new DBPolizaNomina().DBInsertaPolizaNominaDetalle(oP.oLstDetalle, identity))
                    {
                        oIView.iIdFolio = identity;
                        oIView.iSuccess = 1;
                    }
                    else
                        oIView.iSuccess = 0;
                }
                else
                    oIView.iSuccess = 0;
            }
        }

        protected void SearchRFC_Presenter(object sender, EventArgs e)
        {
            oIView.LoadRFC(new DBPolizaNomina().DBGetObtieneExistenciaRFC(oIView.sRFC));
        }

        protected override void ObjSelected_Presenter(object sender, EventArgs e)
        {
            oIView.dtExiste = oIClientesCat.DBGetObtieneSiPeriodoABACOyaExiste;
        }


        protected override void DeleteObj_Presenter(object sender, EventArgs e)
        {
            DataTable dtRes = oIClientesCat.DBSetEliminaNominaABACO(oIView.iIdFolio);


            //oIView.MostrarMensaje(dtRes.Rows[0]["Mensaje"].S(), "Aviso");   
        }
    }
}