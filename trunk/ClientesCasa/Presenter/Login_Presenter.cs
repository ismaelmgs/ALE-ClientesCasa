using ClientesCasa.DomainModel;
using ClientesCasa.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClientesCasa.Presenter
{
    public class Login_Presenter: BasePresenter<IViewLogin>
    {
        private readonly DBRolAccion oIGestCat;

        public Login_Presenter(IViewLogin oView, DBRolAccion oGC)
            : base(oView)
        {
            oIGestCat = oGC;
        }
        protected override void SearchObj_Presenter(object sender, EventArgs e)
        {
            oIView.LoadObjects(oIGestCat.DBSearchObj(oIView.oArrFiltros));
        }
    }
}