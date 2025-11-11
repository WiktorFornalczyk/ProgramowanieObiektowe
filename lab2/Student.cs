using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2
{
    internal class Student
    {
        private string imie, nazwisko;
        private int[] oceny;

        public double SredniaOcen
        {
            get
            {
                double suma = 0;
                foreach (var ocena in oceny)
                {
                    suma += ocena;
                }
                return (suma / oceny.Length);
            }

        }

        public Student(string imie, string nazwisko, int[] oceny)
        {
            this.imie = imie;
            this.nazwisko = nazwisko;
            this.oceny = oceny;
        }

        public void DodajOcene(int ocena)
        {
            oceny = oceny.Append(ocena).ToArray();
        }
    }
}
