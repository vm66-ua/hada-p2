using System;
using System.Collections.Generic;

namespace Hada
{
    public class Barco
    {
        public Dictionary<Coordenada, string> CoordenadasBarco { get; private set; }
        public string Nombre { get; private set; }
        public int NumDanyos { get; private set; }

        
        public event EventHandler<TocadoArgs> EventoTocado;
        public event EventHandler<HundidoArgs> EventoHundido;

       
        public Barco(string nombre, int longitud, char orientacion, Coordenada coordenadaInicio)
        {
            Nombre = nombre;
            NumDanyos = 0;
            CoordenadasBarco = new Dictionary<Coordenada, string>();

            for (int i = 0; i < longitud; i++)
            {
                int fila, columna;

                if (orientacion == 'h')
                {
                    fila = coordenadaInicio.Fila;
                    columna = coordenadaInicio.Columna + i;
                }
                else
                {
                    fila = coordenadaInicio.Fila + i;
                    columna = coordenadaInicio.Columna;
                }

                CoordenadasBarco.Add(new Coordenada(fila, columna), Nombre);

            }
        }

        
        public void Disparo(Coordenada c)
        {
            int total = CoordenadasBarco.Count;
            Coordenada[] claves = new Coordenada[total];
            string[] valores = new string[total];
            int i = 0;
            int j = 0;

            while(i < total)
            {
                string valor = "";
                Coordenada clave = null;
                int contador = 0;

                for (var elemento = new List<Coordenada>(CoordenadasBarco.Keys); contador < total; contador++)
                {
                    if (contador == i)
                    {
                        clave = elemento[contador];
                        valor = CoordenadasBarco[clave];
                        break;
                    }

                }
                claves[i] = clave;
                valores[i] = valor;
                i++;
            }

            int encontrado = -1;
            for(j=0; j<total; j++) 
            {
                if (claves[j].Equals(c))
                {
                    encontrado = j;
                    break;
                }
             }

            if(encontrado == -1)
            {
                //Console.WriteLine("El disparo en " + c.ToString() + " no impactó en ningún barco. ");
                return;
            }

            if (valores[encontrado] == Nombre)
            {
                CoordenadasBarco[claves[encontrado]] = Nombre + "_T";
                NumDanyos++;

                if (EventoTocado != null)
                {
                    EventoTocado(this, new TocadoArgs(Nombre, c));
                }
                
                if (Hundido())
                {
                    if (EventoHundido != null)
                    {
                        EventoHundido(this, new HundidoArgs(Nombre));
                    }
                }
            }


        }

        
        public bool Hundido()
        {
            int total = CoordenadasBarco.Count;
            Coordenada[] claves = new Coordenada[total];
            string[] valores = new string[total];

            int i = 0;
            int j = 0;

            while(i < total)
            {
                string valor = "";
                Coordenada clave = null;
                int contador = 0;

                for (var elemento = new List<Coordenada>(CoordenadasBarco.Keys); contador < total; contador++)
                {
                    if (contador == i)
                    {
                        clave = elemento[contador];
                        valor = CoordenadasBarco[clave];
                        break;
                    }
                }

                claves[i] = clave;
                valores[i] = valor;
                i++;
            }
              
            for(j=0; j < total; j++)
            {
                if (valores[j] == Nombre)
                {
                    return false;
                }
            }

            return true;

        }

        
        public override string ToString()
        {
            string resultado = "[" + Nombre + "] - DAÑOS: [" + NumDanyos + "] - HUNDIDO: [";
            resultado += Hundido() ? "True]" : "False]";
            resultado += " - COORDENADAS: ";
            
            foreach(var Coor in CoordenadasBarco)
            {
                resultado += $"[{Coor.Key} :{Coor.Value}] "; 
            }
            return resultado;
        }
    }


   
}
