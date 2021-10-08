using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientesCasa.Interfaces
{
    public interface IViewCorteMensual : IBaseView
    {
        object[] oArray { get; }
        DataTable dtClientes { set; get; }
        string sClaveContrato { set; get; }
        int iMes { set; get; }
        int iAnio { set; get; }
        

        void LLenaClientes(DataTable dt);
        void LlenaCalculoCorte(DataTable dt);
        
    }
}
