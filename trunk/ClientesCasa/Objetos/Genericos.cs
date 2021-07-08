using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClientesCasa.Objetos
{
    [Serializable]
    public class ErrorController
    {
        private bool _bExisteError;
        private string _sMsjError = string.Empty;

        public bool bExisteError
        {
            get { return _bExisteError; }
            set { _bExisteError = value; ; }
        }
        public string sMsjError
        {
            get { return _sMsjError; }
            set { _sMsjError = value; ; }
        }
    }

    [Serializable]
    public class ValidationRes
    {
        private string _sMensaje = string.Empty;

        public string sMensaje { get { return _sMensaje; } set { _sMensaje = value; } }
    }
}