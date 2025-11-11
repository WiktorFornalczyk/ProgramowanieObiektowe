using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2
{
    internal class Licz
    {
        private int value;

        public Licz(int value)
        {
            this.value = value;
        }

        public void Dodaj(int x)
        {
            value += x;
        }

        public void Odejmij(int x)
        {
            value -= x;
        }

        public void Wypisz()
        {
            Console.WriteLine($"Wartość: {value}");
        }
    }
}
