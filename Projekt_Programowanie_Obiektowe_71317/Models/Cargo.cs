using System;
using System.Collections.Generic;
using System.Text;

namespace Projekt_Programowanie_Obiektowe_71317.Models
{
    /// <summary>
    /// Klasa reprezentująca ładunek przeznaczony do transportu.
    /// Dziedziczy po Asset, więc posiada Id oraz Nazwę.
    /// </summary>
    internal class Cargo : Asset
    {
        public string Description { get; set; }
        public double Weight { get; set; }
        public bool IsFragile { get; set; }
        public string Stipulation { get; set; }

        // Konstruktor klasy Cargo, który inicjalizuje Id, Nazwę, Opis, Wagą, Czy jest delikatny oraz Specyfikację ładunku.
        public Cargo(int Id, string Name, string Description, double Weight, bool IsFragile, string Stipulation) : base(Id, Name)
        {
            this.Description = Description;
            this.Weight = Weight;
            this.IsFragile = IsFragile;
            this.Stipulation = Stipulation;
        }

        // Nadpisana oraz uzupełniona nowymi danymi metoda wyświetlająca informacje o ładunku
        public override void DisplayInfo()
        {
            string isFragile = IsFragile ? "Tak" : "Nie";
            Console.WriteLine($"ID: {Id}, Nazwa: {Name}, Waga: {Weight}, Delikatny ładunek: {isFragile}, Opis: {Description}, Zastrzeżenia: {Stipulation}");
        }
    }
}
