using System;
using System.Collections.Generic;
using System.Text;

// Definicja wyliczenia reprezentującego typy naczep.
public enum TypeOfTrailer
{
    Platforma,
    Plandeka,
    Chłodnia,
    Cysterna,
    Niskopodwoziowa,
    Kontener
}

namespace Projekt_Programowanie_Obiektowe_71317.Models
{
    /// <summary>
    /// Klasa bazowa dla wszystkich naczep.
    /// Dziedziczy po Asset, co zapewnia jej posiadanie pola Id oraz Nazwy.
    /// </summary>
    public class Trailer : Asset
    {
        public double MaxPayload { get; set; } // Pojemność ładunkowa naczepy w tonach
        public string RegistrationNumber { get; set; }
        public TypeOfTrailer TypeOfTrailer { get; set; }

        // Konstruktor klasy Trailer, który inicjalizuje Id, Nazwę, Pojemność ładunkową, Numer rejestracyjny oraz Typ naczepy.
        public Trailer(int Id, string Name, double MaxPayload, string RegistrationNumber, TypeOfTrailer TypeOfTrailer) : base(Id, Name)
        {
            this.MaxPayload = MaxPayload;
            this.RegistrationNumber = RegistrationNumber;
            this.TypeOfTrailer = TypeOfTrailer;
        }

        /// <summary>
        /// Nadpisana oraz uzupełniona nowymi danymi metoda wyświetlająca informacje o naczepie
        /// </summary>
        public override void DisplayInfo()
        {
            Console.WriteLine($"ID: {Id}, Nazwa: {Name}, Pojemność ładunkowa: {MaxPayload} ton, Numer rejestracyjny: {RegistrationNumber}, Typ naczepy: {TypeOfTrailer}");
        }
    }
}
