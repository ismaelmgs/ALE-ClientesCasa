using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Data;

namespace ClientesCasa.Objetos
{
    [Serializable, Bindable(BindableSupport.Yes)]
    public class UserIdentity : BaseObjeto
    {
        private string _sName = string.Empty;
        private string _sIp = System.Net.Dns.GetHostName();
        private string _sUsuario = string.Empty;
        private string _sRolDescripcion = string.Empty;
        private int _iRol = -1;
        private string _sEstatus = string.Empty;
        private bool _bEncontrado = false;
        private DataTable _dTPermisos;
        private string _sUrlPaginaInicial = string.Empty;
        private string _sCorreoBaseUsuario = string.Empty;



        public string sName { get { return _sName; } set { _sName = value; } }
        public string sIp { get { return _sIp; } set { _sIp = value; } }
        public string sUsuario { get { return _sUsuario; } set { _sUsuario = value; } }
        public string sRolDescripcion { get { return _sRolDescripcion; } set { _sRolDescripcion = value; } }
        public int iRol { get { return _iRol; } set { _iRol = value; } }
        public string sEstatus { get { return _sEstatus; } set { _sEstatus = value; } }
        public bool bEncontrado { get { return _bEncontrado; } set { _bEncontrado = value; } }
        public DataTable dTPermisos { get { return _dTPermisos; } set { _dTPermisos = value; } }
        public string sUrlPaginaInicial { get { return _sUrlPaginaInicial; } set { _sUrlPaginaInicial = value; } }
        public string sCorreoBaseUsuario { get { return _sCorreoBaseUsuario; } set { _sCorreoBaseUsuario = value; } }
    }
}