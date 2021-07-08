using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClientesCasa.Objetos
{
    public class Rubro : BaseObjeto
    {

    }

    [Serializable]
    public class RubrosMatricula : BaseObjeto
    {
        private int _iIdContrato = 0;
        private int _iIdAeronave = 0;
        private int _iIdRubro = 0;
        private int _iParticipacion = 0;

        public int iIdContrato { get { return _iIdContrato; } set { _iIdContrato = value; } }
        public int iIdAeronave { get { return _iIdAeronave; } set { _iIdAeronave = value; } }
        public int iIdRubro { get { return _iIdRubro; } set { _iIdRubro = value; } }
        public int iParticipacion { get { return _iParticipacion; } set { _iParticipacion = value; } }

    }
}