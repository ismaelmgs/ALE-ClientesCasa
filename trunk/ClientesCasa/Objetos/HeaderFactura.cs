using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ClientesCasa.Objetos
{
    public class HeaderFactura
    {
        public DateTime dtFechaDoc { set; get; }
        public string Referencia { set; get; }
        public string Comentarios { set; get; }
    }
}