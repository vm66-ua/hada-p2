using Hada;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hada
{
    class Game
    {
        private bool finPartida;
        public Game()
        {
            finPartida = false;
            gameLoop();
        }

        
        private void gameLoop()
        {
            //try
            //{
                List<Barco> barcos = new List<Barco>();
                barcos.Add(new Barco("THOR", 1, 'h', new Coordenada(0, 0)));
                barcos.Add(new Barco("LOKI", 2, 'v', new Coordenada(1, 2)));
                barcos.Add(new Barco("MAYA", 3, 'h', new Coordenada(3, 1)));
                Tablero t = new Tablero(4, barcos);
                t.EventoFinPartida += cuandoEventoFinPartida;
                string c = "";

            bool acaba = false;
            bool res = false;
            do
            {
                do
                {
                    Console.WriteLine("Introduce la coordenada a la que disparar FILA,COLUMNA ('S' para salir):");
                    c = Console.ReadLine();
                    

                    if (c.Contains(',') && c.Length == 3 && c[0] - '0' >= 0 && c[0] - '0' < t.TamTablero && c[2] - '0' >= 0 && c[2] - '0' < t.TamTablero)
                    {
                        res = true;
                    }
                    else
                    {
                        if (c == "S")
                        {
                            acaba = true;
                        }
                        else
                        {
                            res = false;
                            Console.WriteLine("Formato incrorrecto. Introduzca FILA,COLUMNA (sustituyendo los valores)");
                        }

                    }

                } while (!acaba && !res);
                
                if (!acaba)
                {
                    t.Disparar(new Coordenada(c[0] - '0', c[2] - '0'));
                }
                Console.WriteLine(t.ToString());
            } while (!acaba);
            Console.WriteLine("FIN DE LA PARTIDA");



            }

            //catch
            //{
              //  Console.WriteLine("PARTIDA FINALIZADA");
            //}
        //}
        private void cuandoEventoFinPartida(object obj, EventArgs e)
        {
            finPartida = false;
        }
    }
    
}
