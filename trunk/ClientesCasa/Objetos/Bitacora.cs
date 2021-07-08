using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClientesCasa.Objetos
{
    public class Bitacora : BaseObjeto
    {
        //datos de bitacora
        private string _sIdBitacora = string.Empty;
        private string _sFolio = string.Empty;
        private string _sMatricula = string.Empty;
        private string _sSerie = string.Empty;
        private string _PIC = string.Empty;
        private string _SIC = string.Empty;
        //Informacion Principal
        private string _sMotorI = string.Empty;
        private string _sMotorII = string.Empty;
        private string _Planeador = string.Empty;
        private string _APU = string.Empty;
        //Informacion tecnica
        private string _sCMotorI = string.Empty;
        private string _sCMotorII = string.Empty;
        private string _Aterrizajes = string.Empty;
        //Informacion Taller
        private string _sMecanico = string.Empty;
        private bool _bBandera = false;
        //infomacion Usuario
        private string _sUsuario = string.Empty;

        public string sIdBitacora { get { return _sIdBitacora; } set { _sIdBitacora = value; } }
        public string sFolio { get { return _sFolio; } set { _sFolio = value; } }
        public string sMatricula { get { return _sMatricula; } set { _sMatricula = value; } }
        public string sSerie { get { return _sSerie; } set { _sSerie = value; } }
        public string PIC { get { return _PIC; } set { _PIC = value; } }
        public string SIC { get { return _SIC; } set { _SIC = value; } }
        public string sMotorI { get { return _sMotorI; } set { _sMotorI = value; } }
        public string sMotorII { get { return _sMotorII; } set { _sMotorII = value; } }
        public string Planeador { get { return _Planeador; } set { _Planeador = value; } }
        public string APU { get { return _APU; } set { _APU = value; } }
        public string sCMotorI { get { return _sCMotorI; } set { _sCMotorI = value; } }
        public string sCMotorII { get { return _sCMotorII; } set { _sCMotorII = value; } }
        public string sAterrizajes { get { return _Aterrizajes; } set { _Aterrizajes = value; } }
        public string sMecanico { get { return _sMecanico; } set { _sMecanico = value; } }
        public bool bBandera { get { return _bBandera; } set { _bBandera = value; } }
        public string sUsuario { get { return _sUsuario; } set { _sUsuario = value; } }   
    }
}