using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClientesCasa.Objetos
{
    public class SolicitudAvion : BaseObjeto
    {
        private int _iIdFlota = 0;
        private int _iIdMatricula = 0;
        private string _sPersona = string.Empty;
        private string _sFechaInicio = string.Empty;
        private string _sFechaFin = string.Empty;
        private int _iAcepta = 0;
        private string _sComentarios = string.Empty;


        public int iIdFlota
        {
            get { return _iIdFlota; }
            set { _iIdFlota = value; }
        }
        public int iIdMatricula
        {
            get { return _iIdMatricula; }
            set { _iIdMatricula = value; }
        }
        public string sPersona
        {
            get { return _sPersona; }
            set { _sPersona = value; }
        }
        public string sFechaInicio
        {
            get { return _sFechaInicio; }
            set { _sFechaInicio = value; }
        }
        public string sFechaFin
        {
            get { return _sFechaFin; }
            set { _sFechaFin = value; }
        }
        public int iAcepta
        {
            get { return _iAcepta; }
            set { _iAcepta = value; }
        }
        public string sComentarios
        {
            get { return _sComentarios; }
            set { _sComentarios = value; }
        }
    }
}