using Projekt_Programowanie_Obiektowe_71317.Interfaces;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;

// Definicja wyliczenia reprezentującego normy emisji spalin Euro dla pojazdów, nie można wybrać innej niż te.
public enum EuroNorm
{
    None,
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
    public class Truck : Vehicle, ITextSerializable
    {
        public int EnginePower { get; set; }
        public EuroNorm EuroNorm { get; set; }

        public Truck() : base()
        {
            EnginePower = 0;
            EuroNorm = EuroNorm.None;
        }

        // Konstruktor klasy Truck, który inicjalizuje właściwości klasy bazowej oraz dodatkowe właściwości specyficzne dla ciężarówek.
        public Truck(int Id, string Name, string RegistrationNumber, double Kilometers, int EnginePower, EuroNorm EuroNorm) : base(Id, Name, RegistrationNumber, Kilometers)
        {
            this.EnginePower = EnginePower;
            this.EuroNorm = EuroNorm;
        }

        /// <summary>
        /// Nadpisana oraz uzupełniona nowymi danymi metoda zwracająca informacje o ciężarówce
        /// </summary>
        public override string GetInfo()
        {
            return base.GetInfo() + $", Moc silnika: {EnginePower} KM, Norma spalania: {EuroNorm}";
        }
        public override void DisplayInfo()
        {
            Console.WriteLine(GetInfo());
        }

        public override string ToDataLine()
        {
            return $"{Id};{Name};{RegistrationNumber};{Kilometers};{EnginePower};{EuroNorm}";
        }

        public override void FromDataLine(string line)
        {
            var p = line.Split(';');
            if (p.Length < 5) return;

            Id = int.Parse(p[0]);
            Name = p[1];
            RegistrationNumber = p[2];
            Kilometers = double.Parse(p[3]);
            EnginePower = int.Parse(p[4]);
            if (Enum.TryParse(p[5], out EuroNorm norm))
            {
                EuroNorm = norm;
            }
        }
    }
}
