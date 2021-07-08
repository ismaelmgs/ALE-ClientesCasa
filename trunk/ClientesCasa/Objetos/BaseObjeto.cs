using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace ClientesCasa.Objetos
{
    [Serializable, Bindable(BindableSupport.Yes)]
    public class BaseObjeto
    {
        private ErrorController _oErr = new ErrorController();
        private bool bDisposed = false;

        ~BaseObjeto()
        {
            Dispose(false);
        }

        [Browsable(false)]
        public ErrorController oErr { get { return _oErr; } set { _oErr = value; } }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool bDisposing)
        {
            try
            {
                if (!bDisposed)
                {
                    if (bDisposing)
                    {

                    }
                    bDisposed = true;
                }
            }
            catch
            {

            }
        }
    }
}