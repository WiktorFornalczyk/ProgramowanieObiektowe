using Projekt_Programowanie_Obiektowe_71317.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Projekt_Programowanie_Obiektowe_71317.Models
{
    /// <summary>
    /// Klasa bazowa dla wszystkich w systemie.
    /// Posiada pola Id oraz Nazwy.
    /// </summary>
    public abstract class Asset
    {
        public int Id { get; set; } // Będzie uzupełniany pierwszym możliwym Id przy pomocy klasy "IdGenerator.cs"
        public string Name { get; set; }

        protected Asset()
        {
            
        }

        // Konstruktor chroniony, dostępny tylko dla klas dziedziczących
        protected Asset(int Id, string Name)
        {
            this.Id = Id;
            this.Name = Name;
        }

        /// <summary>
        /// Wirtualna Metoda, którą mogą nadpisać klasy pochodne
        /// </summary>
        public virtual string GetInfo()
        {
            return $"ID: {Id}, Nazwa: {Name}";
        }

        public virtual void DisplayInfo()
        {
            Console.WriteLine(GetInfo());
        }
        public virtual string ToDataLine()
        {
            return $"{Id};{Name}";
        }
        public virtual void FromDataLine(string line)
        {
            var p = line.Split(';');
            if (p.Length < 5) return;

            Id = int.Parse(p[0]);
            Name = p[1];
        }
    }
}
