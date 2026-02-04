using Projekt_Programowanie_Obiektowe_71317.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Channels;

namespace Projekt_Programowanie_Obiektowe_71317.Models
{
    /// <summary>
    /// Klasa bazowa dla wszystkich pojazdów silnikowych w systemie.
    /// Dziedziczy po Asset, co zapewnia jej posiadanie pola Id oraz Nazwy.
    /// </summary>
    public class Vehicle : Asset, ITextSerializable
    {
        public string RegistrationNumber { get; set; }
        public double Kilometers { get; set; }

        public Vehicle() : base()
        {
            RegistrationNumber = string.Empty;
            Kilometers = 0;
        }

        // Konstruktor klasy Vehicle, który inicjalizuje Id, Nazwę, Numer Rejestracyjny oraz Przebieg pojazdu.
        public Vehicle(int Id, string Name, string RegistrationNumber, double Kilometers) : base(Id, Name)
        {
            this.RegistrationNumber = RegistrationNumber;
            this.Kilometers = Kilometers;
        }

        /// <summary>
        /// Nadpisana oraz uzupełniona nowymi danymi metoda Wracająca informacje o ciężarówce
        /// </summary>
        public override string GetInfo()
        {
            return base.GetInfo() + $", Numer Rejestracyjny: {RegistrationNumber}, Przebieg: {Kilometers} kilometrów";
        }
        public override void DisplayInfo()
        {
            Console.WriteLine(GetInfo());     
        }
        public override string ToDataLine()
        {
            return $"{Id};{Name};{RegistrationNumber};{Kilometers}";
        }

        public override void FromDataLine(string line)
        {
            var p = line.Split(';');
            if (p.Length < 5) return;

            Id = int.Parse(p[0]);
            Name = p[1];
            RegistrationNumber = p[2];
            Kilometers = double.Parse(p[3]);
        }
    }
}
