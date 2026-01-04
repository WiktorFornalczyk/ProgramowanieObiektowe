using System;
using System.Collections.Generic;
using System.Text;

// Definicja wyliczenia reprezentującego normy emisji spalin Euro dla pojazdów, nie można wybrać innej niż te.
public enum EuroNorm
{
    Euro1,
    Euro2,
    Euro3,
    Euro4,
    Euro5,
    Euro6,
    Euro6a,
    Euro6b,
    Euro6c,
    Euro6d
}

namespace Projekt_Programowanie_Obiektowe_71317.Models
{
    /// <summary>
    /// Klasa reprezentująca konkretną ciężarówkę.
    /// Dziedziczy po Vehicle a Vehicle po Asset, co zapewnia jej posiadanie Id, Nazwy, Numeru Rej oraz Przebiegu.
    /// </summary>
    public class Truck : Vehicle
    {
        public int EnginePower { get; set; }
        public EuroNorm EuroNorm { get; set; }

        // Konstruktor klasy Truck, który inicjalizuje właściwości klasy bazowej oraz dodatkowe właściwości specyficzne dla ciężarówek.
        public Truck(int Id, string Name, string RegistrationNumber, double Kilometers, int EnginePower, EuroNorm EuroNorm) : base(Id, Name, RegistrationNumber, Kilometers)
        {
            this.EnginePower = EnginePower;
            this.EuroNorm = EuroNorm;
        }

        /// <summary>
        /// Nadpisana oraz uzupełniona nowymi danymi metoda wyświetlająca informacje o ciężarówce
        /// </summary>
        public override void DisplayInfo()
        {
            Console.WriteLine($"ID: {Id}, Nazwa: {Name}, Numer Rejestracyjny: {RegistrationNumber}, Przebieg: {Kilometers}km, Moc silnika: {EnginePower}, Norma spalania: {EuroNorm}");
        }
    }
}
