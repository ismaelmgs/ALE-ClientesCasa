using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientesCasa.Interfaces
{
    public interface IViewLogin : IBaseView
    {
        object[] oArrFiltros { get; }
        void ObtieneValores();
        void LoadObjects(DataTable dtObjCat);
    }
}
