using Projekt_Programowanie_Obiektowe_71317.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Projekt_Programowanie_Obiektowe_71317.Models
{
    /// <summary>
    /// Klasa reprezentująca naczepę specjalistyczną do ciężkich ładunków.
    /// Dziedziczy Trailer a ona po Asset.
    /// </summary>
    public class HeavyTrailer : Trailer, ITextSerializable
    {
        public int NumberOfAxles { get; set; }
        public bool HasOversizePermit { get; set; }

        public HeavyTrailer() : base()
        {
            NumberOfAxles = 0;
            HasOversizePermit = false;
        }

        // Konstruktor klasy Trailer, który inicjalizuje Id, Nazwę, Pojemność ładunkową, Numer rejestracyjny, Typ naczepy, Liczbę osi oraz Czy ma zezwolenie na gabaryty.
        public HeavyTrailer(int Id, string Name, double MaxPayload, string RegistrationNumber, TypeOfTrailer TypeOfTrailer, int NumberOfAxles, bool HasOversizePermit) : base(Id, Name, MaxPayload, RegistrationNumber, TypeOfTrailer)
        {
            this.NumberOfAxles = NumberOfAxles;
            this.HasOversizePermit = HasOversizePermit;
        }

        /// <summary>
        /// Nadpisana oraz uzupełniona nowymi danymi metoda zwracająca informacje o naczepie
        /// </summary>
        public override string GetInfo()
        {
            return base.GetInfo() + $", Liczba osi: {NumberOfAxles}, Możliwość przewożenia gabarytów: {(HasOversizePermit ? "Tak" : "Nie")}";
        }
        public override void DisplayInfo()
        {
            Console.WriteLine(GetInfo());
        }
        public override string ToDataLine()
        {
            return $"{Id};{Name};{MaxPayload};{RegistrationNumber};{TypeOfTrailer};{NumberOfAxles};{HasOversizePermit}";
        }
        public override void FromDataLine(string line)
        {
            var p = line.Split(';');
            if (p.Length < 5) return;

            Id = int.Parse(p[0]);
            Name = p[1];
            MaxPayload = double.Parse(p[2]);
            RegistrationNumber = p[3];
            if (Enum.TryParse(p[4], out TypeOfTrailer TypeOfT))
            {
                TypeOfTrailer = TypeOfT;
            }
            NumberOfAxles = int.Parse(p[5]);
            HasOversizePermit = bool.Parse(p[6]);
        }
    }
}
