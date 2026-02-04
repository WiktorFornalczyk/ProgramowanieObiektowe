using Projekt_Programowanie_Obiektowe_71317.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Projekt_Programowanie_Obiektowe_71317.Models
{
    /// <summary>
    /// Klasa reprezentująca ładunek przeznaczony do transportu.
    /// Dziedziczy po Asset, więc posiada Id oraz Nazwę.
    /// </summary>
    internal class Cargo : Asset, ITextSerializable
    {
        public string Description { get; set; }
        public double Weight { get; set; }
        public bool IsFragile { get; set; } // Czy ładunek jest delikatny
        public string Stipulation { get; set; } // Specyfikacja ładunku (np. warunki przechowywania)

        public Cargo() : base() 
        {
            Description = string.Empty;
            Weight = 0.0;
            IsFragile = false;
            Stipulation = string.Empty;
        }

        // Konstruktor klasy Cargo, który inicjalizuje Id, Nazwę, Opis, Wagą, Czy jest delikatny oraz Specyfikację ładunku.
        public Cargo(int Id, string Name, string Description, double Weight, bool IsFragile, string Stipulation) : base(Id, Name)
        {
            this.Description = Description;
            this.Weight = Weight;
            this.IsFragile = IsFragile;
            this.Stipulation = Stipulation;
        }

        /// <summary>
        /// Nadpisana oraz uzupełniona nowymi danymi metoda zwracająca informacje o ładunku
        /// </summary>
        public override string GetInfo()
        {
            return base.GetInfo() + $", Waga: {Weight} ton, Delikatny ładunek: {(IsFragile ? "Tak" : "Nie")}, Opis: {Description}, Zastrzeżenia: {Stipulation}";
        }
        public override void DisplayInfo()
        {
            Console.WriteLine(GetInfo());
        }
        public override string ToDataLine()
        {
            return $"{Id};{Name};{Description};{Weight};{IsFragile};{Stipulation}";
        }
        public override void FromDataLine(string line)
        {
            var p = line.Split(';');
            if (p.Length < 5) return;

            Id = int.Parse(p[0]);
            Name = p[1];
            Description = p[2];
            Weight = double.Parse(p[3]);
            IsFragile = bool.Parse(p[4]);
            Stipulation = p[5];
        }
    }
}
