using System;
using Hada;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Hada
{
    public class TocadoArgs : EventArgs
    {
        private string _nombre;
        private Coordenada _coordenadaImpacto;
        public string Nombre => _nombre;

        public Coordenada CoordenadaImpacto => _coordenadaImpacto;

        public TocadoArgs(string nombre, Coordenada coordenadaImpacto)
        {
            _nombre = nombre;
            _coordenadaImpacto = coordenadaImpacto;
        }
    }

    public class HundidoArgs : EventArgs
    {
        public string _nombre;

        public string Nombre => _nombre;
        public HundidoArgs(string nombre)
        {
            _nombre = nombre;
        }
    }
}


