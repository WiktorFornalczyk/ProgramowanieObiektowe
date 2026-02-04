using Projekt_Programowanie_Obiektowe_71317.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Projekt_Programowanie_Obiektowe_71317.Models
{
    /// <summary>
    /// Klasa reprezentująca kierowcę w systemie.
    /// Dziedziczy po Asset, więc posiada Id oraz Nazwa w tym wypadku nazwa Będzie imieniem.
    /// </summary>
    internal class Driver : Asset, ITextSerializable
    {
        public string LastName { get; set; }
        public string Pesel { get; set; }
        public string LicenseNumber { get; set; }

        public Driver() : base()
        {
            LastName = string.Empty;
            Pesel = string.Empty;
            LicenseNumber = string.Empty;
        }

        // Konstruktor klasy Driver, który inicjalizuje Id, Imię (Name), Nazwisko, Pesel oraz Numer prawa jazdy.
        public Driver(int Id, string Name, string LastName, string Pesel, string LicenseNumber) : base(Id, Name)
        {
            this.LastName = LastName;
            this.Pesel = Pesel;
            this.LicenseNumber = LicenseNumber;
        }

        /// <summary>
        /// Nadpisana oraz uzupełniona nowymi danymi metoda zwracająca informacje o kierowcy
        /// </summary>
        public override string GetInfo()
        {
            return $"ID: {Id}, Imię: {Name}, Nazwisko: {LastName}, PESEL: {Pesel}, Numer prawa jazdy: {LicenseNumber}";
        }
        public override void DisplayInfo()
        {
            Console.WriteLine(GetInfo());
        }
        public override string ToDataLine()
        {
            return $"{Id};{Name};{LastName};{Pesel};{LicenseNumber}";
        }
        public override void FromDataLine(string line)
        {
            var p = line.Split(';');
            if (p.Length < 5) return;

            Id = int.Parse(p[0]);
            Name = p[1];
            LastName = p[2];
            Pesel = p[3];
            LicenseNumber = p[4];
        }
    }
}
