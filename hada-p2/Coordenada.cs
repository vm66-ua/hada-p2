using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Hada
{
    public class Coordenada
    {
        private int fila;
        private int columna;

        public int Fila
        {
            get { return fila; }
            set
            {
                if (value < 0 || value > 9)
                {
                    throw new ArgumentOutOfRangeException("Fila debe estar entre 0 y 9");
                }

                fila = value;
            }
        }

        public int Columna
        {
            get { return columna; }
            set
            {
                if (value < 0 || value > 9)
                {
                    throw new ArgumentOutOfRangeException("Columna debe estar entre 0 y 9");
                }

                columna = value;
            }
        }

        
        public Coordenada()
        {
            Fila = 0;
            Columna = 0;
        }

        
        public Coordenada(int fila, int columna)
        {
            Fila = fila;
            Columna = columna;
        }

        
        public Coordenada(string fila, string columna)
        {
            if (!int.TryParse(fila, out int f) || !int.TryParse(columna, out int c))
            {
                throw new ArgumentException("Los valores de fila y columna deben ser números enteros");
            }


            Fila = f;
            Columna = c;
        }
    }
}

        

