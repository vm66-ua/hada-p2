﻿//JORGE GARCÍA HERNÁNDEZ
//VLADA MUKHAREVA
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

        private List<Barco> inicializar_barcos()
        {
            List<Barco> barcos = new List<Barco>();
            int n = 1;
            int tam, x, y;
            bool coincide;
            Random r = new Random();
            while(n < 4)
            {
                tam = r.Next(1, 5);
                x = r.Next(0, 4);
                y = r.Next(0, 4);
                coincide = false;
                int orientacion = r.Next(0, 2);
                char or;
                if(orientacion == 0)
                {
                    or = 'h';
                }
                else
                {
                    or = 'v';
                }
                Coordenada c;
                Barco b;
                for(int t = 0; t < barcos.Count; t++)
                {
                    
                    for(int coor = 0; coor < tam; coor++)
                    {

                        if(orientacion == 'h')
                        {
                            c = new Coordenada(x, y + coor);

                        }
                        else
                        {
                            c = new Coordenada(x + coor, y);
                        }
                        if (barcos[t].CoordenadasBarco.ContainsKey(c))
                        {
                            coincide = true;
                        }
                    }
                }
                
                if (n == 1)
                {
                    b = new Barco("THOR", tam, or, new Coordenada(x, y));
                }
                else
                {
                    if(n == 2)
                    {
                        b = new Barco("LOKI", tam, or, new Coordenada(x, y));
                    }
                    else
                    {
                        b = new Barco("MAYA", tam, or, new Coordenada(x, y));
                    }
                }
                for (int j = 0; j < b.CoordenadasBarco.Count; j++)
                {
                    int max = 4;
                    for (int i = 0; i < 4; i++)
                    {
                        if (b.CoordenadasBarco.ContainsKey(new Coordenada(i, max)) || b.CoordenadasBarco.ContainsKey(new Coordenada(max, i)))
                        {
                            coincide = true;
                        }
                    }

                }
                if (!coincide)
                    {
                        barcos.Add(b);
                        n++;
                    }
                
            }
            return barcos;
        }
        
        private void gameLoop()
        {
                List<Barco> barcos = new List<Barco>();
                
            barcos = inicializar_barcos();
                
                Tablero t = new Tablero(4, barcos);
                t.EventoFinPartida += cuandoEventoFinPartida;
                string c = "";

            bool acaba = false;
            bool res = false;
            Console.WriteLine(t.ToString());
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
                if (finPartida)
                {
                    acaba = true;
                }
                Console.WriteLine(t.ToString());
            } while (!acaba);
            Console.WriteLine("FIN DE LA PARTIDA");



            }

        private void cuandoEventoFinPartida(object obj, EventArgs e)
        {
            finPartida = true;
        }
    }
    
}
