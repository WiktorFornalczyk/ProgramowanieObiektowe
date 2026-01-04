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

        // Konstruktor chroniony, dostępny tylko dla klas dziedziczących
        protected Asset(int Id, string Name)
        {
            this.Id = Id;
            this.Name = Name;
        }

        /// <summary>
        /// Wirtualna Metoda, którą mogą nadpisać klasy pochodne
        /// </summary>
        public virtual void DisplayInfo()
        {
            Console.WriteLine($"ID: {Id}, Nazwa: {Name}");
        }
    }
}
