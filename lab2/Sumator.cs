using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2
{
    internal class Sumator
    {
        private int[] liczby;

        public Sumator(int[] liczby)
        {
            this.liczby = liczby;
        }

        public int Suma()
        {
            int suma = 0;
            foreach (var liczba in liczby)
            {
                suma += liczba;
            }
            return suma;
        }

        public int SumaPodziel2()
        {
            int suma = 0;
            foreach (var liczba in liczby)
            {
                if (liczba % 2 == 0)
                {
                    suma += liczba;
                }
            }
            return suma;
        }

        public int IleElementów()
        {
            return liczby.Length;
        }

        public void Wypisz()
        {
            Console.WriteLine("Liczby:");
            foreach (var liczba in liczby)
            {
                Console.Write(liczba + " ");
            }
            Console.WriteLine();
        }

        public void Index(int lowindex, int highindex)
        {

            Console.WriteLine($"Liczby od indeksu {lowindex} do {highindex}:");
            if (highindex >= liczby.Length)
            {
                highindex = liczby.Length - 1;
            }
            for (int i = lowindex; i <= highindex && i < liczby.Length; i++)
            {
                Console.Write(liczby[i] + " ");
            }
            Console.WriteLine();
        }
    }
}
