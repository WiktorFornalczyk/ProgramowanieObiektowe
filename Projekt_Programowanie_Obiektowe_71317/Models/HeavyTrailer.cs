using System;
using System.Collections.Generic;
using System.Text;

namespace Projekt_Programowanie_Obiektowe_71317.Models
{
    /// <summary>
    /// Klasa reprezentująca naczepę specjalistyczną do ciężkich ładunków.
    /// Dziedziczy Trailer a ona po Asset.
    /// </summary>
    internal class HeavyTrailer : Trailer
    {
        public int NumberOfAxles { get; set; }
        public bool HasOversizePermit { get; set; }

        // Konstruktor klasy Trailer, który inicjalizuje Id, Nazwę, Pojemność ładunkową, Numer rejestracyjny, Typ naczepy, Liczbę osi oraz Czy ma zezwolenie na gabaryty.
        public HeavyTrailer(int Id, string Name, double MaxPayload, string RegistrationNumber, TypeOfTrailer TypeOfTrailer, int NumberOfAxles, bool HasOversizePermit) : base(Id, Name, MaxPayload, RegistrationNumber, TypeOfTrailer)
        {
            this.NumberOfAxles = NumberOfAxles;
            this.HasOversizePermit = HasOversizePermit;
        }

        /// <summary>
        /// Nadpisana oraz uzupełniona nowymi danymi metoda wyświetlająca informacje o naczepie
        /// </summary>
        public override void DisplayInfo()
        {
            string oversizeInfo = HasOversizePermit ? "Tak" : "Nie";
            Console.WriteLine($"ID: {Id}, Nazwa: {Name}, Pojemność ładunkowa: {MaxPayload} ton, Numer rejestracyjny: {RegistrationNumber}, Typ naczepy: {TypeOfTrailer}, Liczba osi: {NumberOfAxles}, Możliwość przewożenia gabarytów: {oversizeInfo}");
        }
    }
}
