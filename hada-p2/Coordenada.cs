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
                    throw new ArgumentOutOfRangeException("Fila debe estar entre 0 y 9.");
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
                    throw new ArgumentOutOfRangeException("Columna debe estar entre 0 y 9.");
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
            try
            {
                int filaConvertida = Convert.ToInt32(fila);
                int columnaConvertida= Convert.ToInt32(columna);
                Fila= filaConvertida;
                Columna= columnaConvertida;
            }
            catch (Exception)
            {
                throw new ArgumentException("Los argumentos deben ser números enteros.");
            }

            
        }

        public Coordenada(Coordenada otra)
        {
            Fila= otra.Fila;
            Columna= otra.Columna;
        }

        public override string ToString()
        {
            return "(" + Fila + "," + Columna + ")";
        }

        public override int GetHashCode()
        {
            return Fila.GetHashCode() ^ Columna.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj is Coordenada)
            {
                Coordenada otra = (Coordenada)obj;
                return Fila == otra.Fila && Columna == otra.Columna;
            }

            return false;
        }


    }
}

        

