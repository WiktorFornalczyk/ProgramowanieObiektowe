using System;
using System.Collections.Generic;
using System.Text;

namespace Projekt_Programowanie_Obiektowe_71317.Models
{
    /// <summary>
    /// Klasa bazowa dla wszystkich pojazdów silnikowych w systemie.
    /// Dziedziczy po Asset, co zapewnia jej posiadanie pola Id oraz Nazwy.
    /// </summary>
    public class Vehicle : Asset
    {
        public string RegistrationNumber { get; set; }
        public double Kilometers { get; set; }

        // Konstruktor klasy Vehicle, który inicjalizuje Id, Nazwę, Numer Rejestracyjny oraz Przebieg pojazdu.
        public Vehicle(int Id, string Name, string RegistrationNumber, double Kilometers) : base(Id, Name)
        {
            this.RegistrationNumber = RegistrationNumber;
            this.Kilometers = Kilometers;
        }

        /// <summary>
        /// Nadpisana oraz uzupełniona nowymi danymi metoda wyświetlająca informacje o ciężarówce
        /// </summary>
        public override void DisplayInfo()
        {
            Console.WriteLine($"ID: {Id}, Nazwa: {Name}, Numer Rejestracyjny: {RegistrationNumber}, Przebieg: {Kilometers}km");
        }
    }
}
