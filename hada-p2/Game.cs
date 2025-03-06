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
                barcos.Add(new Barco("THOR", 3, 'h', new Coordenada(0, 0)));
                barcos.Add(new Barco("LOKI", 4, 'v', new Coordenada(2, 3)));
                barcos.Add(new Barco("MAYA", 5, 'h', new Coordenada(6, 3)));
                Tablero t = new Tablero(8, barcos);
                t.EventoFinPartida += cuandoEventoFinPartida;
                char res;
                string c = "";

            bool acaba = false;
                do
                {
                    Console.WriteLine("Introduzca una coordenada (formato fila, columna). Introduzca 'S' para salir.");
                    c = Console.ReadLine();
                    if(c == "S")
                {
                    acaba = true;
                }
                
                
                } while (!acaba && (c.Length != 3 && c[0] >= '0' && c[0] <= '8' && c[1] == ',' && c[2] >= '0' && c[2] <= '8'));
                if(!acaba){
                    t.Disparar(new Coordenada(c[0] - '0', c[2] - '0'));
                t.DibujarTablero();
                }
                else
                {
                t.DibujarTablero();
                    Console.WriteLine("PARTIDA FINALIZADA");
                }


                
            //}

            /*catch
            {
                Console.WriteLine("PARTIDA FINALIZADA");
            }*/
        }
        private void cuandoEventoFinPartida(object obj, EventArgs e)
        {
            finPartida = false;
        }
    }
    
}
