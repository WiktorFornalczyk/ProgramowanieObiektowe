using System;
using System.Collections.Generic;
using System.Text;

namespace Projekt_Programowanie_Obiektowe_71317.Models
{
    /// <summary>
    /// Klasa reprezentująca kierowcę w systemie.
    /// Dziedziczy po Asset, więc posiada Id oraz Nazwa w tym wypadku nazwa Będzie imieniem.
    /// </summary>
    internal class Driver : Asset
    {
        public string LastName { get; set; }
        public int Pesel { get; set; }
        public string LicenseNumber { get; set; }

        // Konstruktor klasy Driver, który inicjalizuje Id, Imię (Name), Nazwisko, Pesel oraz Numer prawa jazdy.
        public Driver(int Id, string Name, string LastName, int Pesel, string LicenseNumber) : base(Id, Name)
        {
            this.LastName = LastName;
            this.Pesel = Pesel;
            this.LicenseNumber = LicenseNumber;
        }

        // Nadpisana oraz uzupełniona nowymi danymi metoda wyświetlająca informacje o kierowcy
        public override void DisplayInfo()
        {
            Console.WriteLine($"ID: {Id}, Imię: {Name}, Nazwisko: {LastName}, PESEL: {Pesel}, Numer prawa jazdy: {LicenseNumber}");
        }
    }
}
