using ClientesCasa.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClientesCasa.Presenter
{
    public class BasePresenter<TView> where TView : class, IBaseView
    {
        protected TView oIView { get; private set; }

        public BasePresenter(TView _oIView)
        {
            oIView = _oIView;

            oIView.eNewObj += NewObj_Presenter;
            oIView.eObjSelected += ObjSelected_Presenter;
            oIView.eSaveObj += SaveObj_Presenter;
            oIView.eDeleteObj += DeleteObj_Presenter;
            oIView.eSearchObj += SearchObj_Presenter;
        }

        protected virtual void NewObj_Presenter(object sender, EventArgs e) { }

        protected virtual void ObjSelected_Presenter(object sender, EventArgs e) { }

        protected virtual void SaveObj_Presenter(object sender, EventArgs e) { }

        protected virtual void DeleteObj_Presenter(object sender, EventArgs e) { }

        protected virtual void SearchObj_Presenter(object sender, EventArgs e) { }
    }
}