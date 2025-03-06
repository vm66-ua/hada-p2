using Hada;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hada
{
    class Tablero
    {
        private int _tamTablero;
        public int TamTablero
        {
            get { return _tamTablero; }
            set
            {
                if (value < 4 || value > 9)
                {
                    throw new ArgumentOutOfRangeException($"The size of the board must be between 4 and 9.");
                    // no cumple las necesidades de tamaño por lo que se pone en el tam máximo
                }
                _tamTablero = value;
            }
        }
        private List<Coordenada> coordenadasDisparadas;
        private List<Coordenada> coordenadasTocadas;
        private List<Barco> barcos;
        private List<Barco> barcosEliminados;
        private Dictionary<Coordenada, string> casillasTablero;
        //public event EventHandler<TocadoArgs> EventoTocado;
        //public event EventHandler<HundidoArgs> EventoHundido;
        public event EventHandler<EventArgs> EventoFinPartida;

        public Tablero(int tamTablero, List<Barco> barcos)
        {
            coordenadasDisparadas = new List<Coordenada>();
            coordenadasTocadas = new List<Coordenada>();
            barcosEliminados = new List<Barco>();
            casillasTablero = new Dictionary<Coordenada, string>();
            TamTablero = tamTablero;
            this.barcos = new List<Barco>();
            for (int i = 0; i < barcos.Count; i++)
            {
                this.barcos.Add(barcos[i]);
                barcos[i].EventoTocado += cuandoEventoTocado;
                barcos[i].EventoHundido += cuandoEventoHundido;

            }
            inicializaCasillasTablero();
        }

        private void inicializaCasillasTablero()
        {
            for(int i = 0; i < TamTablero; i++)
            {
                for(int j = 0; j < TamTablero; j++)
                {
                    casillasTablero.Add(new Coordenada(i, j), "AGUA");
                }
            }
            for(int i = 0; i < barcos.Count; i++)
            {
                foreach (object cor in barcos[i].CoordenadasBarco)
                {
                    casillasTablero[(Coordenada)cor] = barcos[i].Nombre;
                }
            }
        }

        public void Disparar(Coordenada c)
        {
            if (c.Fila < 0 || c.Fila > TamTablero || c.Columna < 0 || c.Columna > TamTablero)
            {
                Console.WriteLine($"La coordenada ({c.Fila}, {c.Columna}) está fuera de las dimensiones del tablero.");
            }
            else
            {
                bool ya = false;
                for (int bar = 0; !ya && bar < coordenadasTocadas.Count; bar++)
                {
                    if (Equals(c, coordenadasTocadas[bar]))
                    {
                        ya = true;
                    }
                }

                if (!ya)
                {
                    //bool encontrado = false;
                    for (int bar = 0; bar < barcos.Count; bar++)
                    {
                        /* int tam = barcos[bar].longitud;
                         Posicion p = barcos[bar].posicion;
                         int n = 0;
                         int filas = 0;
                         int columnas = 0;
                         while (n < tam)
                         {

                             if (barcos[bar].fila + filas == c.fila && barcos[bar].columna + columnas == c.columna)
                             {
                                 encontrado = true;
                                 coordenadasTocadas.Add(c);
                                 //llamar al evento tocado !! y que llame a hundido !!

                             }
                             if (barcos[i].orientacion = 'h')
                             {
                                 columnas++;
                             }
                             else
                             {
                                 filas += TamTablero;
                             }

                             tam--;
                         }*/
                        barcos[bar].Disparo(c);
                    }
                }
                //añadir al vector de disparos
                coordenadasDisparadas.Add(c);

            }
        }

        public string DibujarTablero()
        {
            string res = "";
            res += "CASILLAS TABLERO" + Environment.NewLine;
            res += "--------" + Environment.NewLine;
            for (int i = 0; i < TamTablero; i++)
            {
                for (int j = 0; j < TamTablero; j++)
                {
                    res += "[" + casillasTablero[new Coordenada(i, j)] + "]";
                }
                res += Environment.NewLine;
            }
            return res;
        }

        public override string ToString()
        {
            string res = "";

            for (int n = 0; n < barcos.Count; n++)
            {
                res += barcos[n].ToString() + Environment.NewLine;
            }
            res += "Coordenadas disparadas: ";
            for (int n = 0; n < coordenadasDisparadas.Count; n++)
            {
                res += coordenadasDisparadas[n].ToString() + " ";
            }
            res += '\n';
            res += "Coordenadas tocadas: ";
            for (int n = 0; n < coordenadasTocadas.Count; n++)
            {
                res += coordenadasTocadas[n].ToString() + " ";
            }
            res += Environment.NewLine;
            res += DibujarTablero();
            return res;

        }

        private void cuandoEventoTocado(object bar, TocadoArgs e)
        {
            coordenadasTocadas.Add(e.CoordenadaImpacto);
            Console.WriteLine($"TABLERO: Barco [" + e.Nombre + "] tocado en Coordenada: [" + e.CoordenadaImpacto + "]");
            casillasTablero[e.CoordenadaImpacto] = $"{e.Nombre}_T";
        }

        private void cuandoEventoHundido(object bar, HundidoArgs e)
        {
            barcosEliminados.Add((Barco)bar);
            Console.WriteLine($"TABLERO: Barco [" + e.Nombre + "] hundido!!");
            if(barcosEliminados.Count == barcos.Count)
            {
                EventoFinPartida(this, EventArgs.Empty);
            }
        }
    }
}
