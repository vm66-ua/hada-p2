using Hada;
using System;
using System.Collections.Generic;
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

        private void cuandoEventoFinPartida(EventArgs evento)
        {
            Console.WriteLine("PARTIDA FINALIZADA!!!");
            finPartida = true;

        }
        private void gameLoop()
        {
            List<Barco> barcos = new List<Barco>;
            barcos.Add(Barco("THOR", 3, 'h', Coordenada(0, 0)));
            barcos.Add(Barco("LOKI", 4, 'v', Coordenada(2, 3)));
            barcos.Add(Barco("MAYA", 5, 'h', Coordenada(6, 3)));
            Tablero t = Tablero(8, barcos);
            char res;

            do
            {
                string c = "";
                do
                {
                    Console.WriteLine("Introduzca una coordenada (formato fila, columna)");
                    Console.ReadLine(c);
                } while (c.Length == 3 && c[0] >= '0' && c[0] <= '8' && c[1] == ',' && c[2] >= '0' && c[2] <= '8');
                t.Disparar(new Coordenada(c[0] - '0', c[2] - '0');


            } while (finPartida || res == 's' || res == 'S');

        }
    }

}
