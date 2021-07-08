using ClientesCasa.Objetos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientesCasa.Interfaces
{
    public interface IViewConsultaCargaPolizas : IBaseView
    {
        string sFechaIni { get; set; }
        string sFechaFin { get; set; }
        long lgIdFolio { get; set; }

        void LoadPolizas(DataTable dt);
        void LoadPolizaXFolio(DataTable dt);

        event EventHandler eSearchXFolio;
    }
}
