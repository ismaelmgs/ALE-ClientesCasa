using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientesCasa.Interfaces
{
    public interface IViewGastosSinAsignar : IBaseView
    {
        int iMes { get; set; }
        int iAnio { get; set; }
        DataSet dsGastos { set; get; }

        void LLenaGastos(DataSet ds);

        void LlenaReporte(string sHTML);
    }
}
