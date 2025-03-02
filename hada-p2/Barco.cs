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
            if (longitud <= 0 || longitud > 9)
                throw new ArgumentOutOfRangeException("La longitud del barco debe estar entre 1 y 9");

            if (orientacion != 'h' && orientacion != 'v')
                throw new ArgumentException("La orientación debe ser 'h' (horizontal) o 'v' (vertical)");

            Nombre = nombre;
            NumDanyos = 0;
            CoordenadasBarco = new Dictionary<Coordenada, string>();

         
            for (int i = 0; i < longitud; i++)
            {
                int fila = coordenadaInicio.Fila;
                int columna = coordenadaInicio.Columna;

                if (orientacion == 'h')
                    columna += i;
                else
                    fila += i;

                Coordenada nuevaCoordenada = new Coordenada(fila, columna);
                CoordenadasBarco[nuevaCoordenada] = Nombre;
            }
        }

        
        public void Disparo(Coordenada c)
        {
            if (CoordenadasBarco.ContainsKey(c) && !CoordenadasBarco[c].EndsWith("_T"))
            {
                CoordenadasBarco[c] += "_T"; 
                NumDanyos++;
                EventoTocado?.Invoke(this, new TocadoArgs(Nombre, c));

                if (Hundido())
                {
                    EventoHundido?.Invoke(this, new HundidoArgs(Nombre));
                }
            }
        }

        
        public bool Hundido()
        {
            foreach (var etiqueta in CoordenadasBarco.Values)
            {
                if (!etiqueta.EndsWith("_T"))
                    return false;
            }
            return true;
        }

        
        public override string ToString()
        {
            string estado = Hundido() ? "Hundido" : "A flote";
            string coordenadas = string.Join(", ", CoordenadasBarco);
            return $"Barco: {Nombre}, Daños: {NumDanyos}, Estado: {estado}, Coordenadas: [{coordenadas}]";
        }
    }

    
    public class TocadoArgs : EventArgs
    {
        public string Nombre { get; }
        public Coordenada CoordenadaImpacto { get; }

        public TocadoArgs(string nombre, Coordenada coordenadaImpacto)
        {
            Nombre = nombre;
            CoordenadaImpacto = coordenadaImpacto;
        }
    }

    public class HundidoArgs : EventArgs
    {
        public string Nombre { get; }

        public HundidoArgs(string nombre)
        {
            Nombre = nombre;
        }
    }
}
