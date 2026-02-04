using Projekt_Programowanie_Obiektowe_71317.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

// Definicja wyliczenia reprezentującego typy naczep.
public enum TypeOfTrailer
{
    None,
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
    public class Trailer : Asset, ITextSerializable
    {
        public double MaxPayload { get; set; } // Pojemność ładunkowa naczepy w tonach
        public string RegistrationNumber { get; set; }
        public TypeOfTrailer TypeOfTrailer { get; set; }

        public Trailer() : base()
        {
            MaxPayload = 0;
            RegistrationNumber = string.Empty;
            TypeOfTrailer = TypeOfTrailer.None;
        }

        // Konstruktor klasy Trailer, który inicjalizuje Id, Nazwę, Pojemność ładunkową, Numer rejestracyjny oraz Typ naczepy.
        public Trailer(int Id, string Name, double MaxPayload, string RegistrationNumber, TypeOfTrailer TypeOfTrailer) : base(Id, Name)
        {
            this.MaxPayload = MaxPayload;
            this.RegistrationNumber = RegistrationNumber;
            this.TypeOfTrailer = TypeOfTrailer;
        }

        /// <summary>
        /// Nadpisana oraz uzupełniona nowymi danymi metoda zwracająca informacje o naczepie
        /// </summary>
        public override string GetInfo()
        {
            return base.GetInfo() + $", Pojemność ładunkowa: {MaxPayload} ton, Numer rejestracyjny: {RegistrationNumber}, Typ naczepy: {TypeOfTrailer}";
        }
        public override void DisplayInfo()
        {
            Console.WriteLine(GetInfo());
        }
        public override string ToDataLine()
        {
            return $"{Id};{Name};{MaxPayload};{RegistrationNumber};{TypeOfTrailer}";
        }

        public override void FromDataLine(string line)
        {
            var p = line.Split(';');
            if (p.Length < 5) return;

            Id = int.Parse(p[0]);
            Name = p[1];
            MaxPayload = double.Parse(p[2]);
            RegistrationNumber = p[3];
            if (Enum.TryParse(p[4], out TypeOfTrailer TypeOfT))
            {
                TypeOfTrailer = TypeOfT;
            }
        }
    }
}
