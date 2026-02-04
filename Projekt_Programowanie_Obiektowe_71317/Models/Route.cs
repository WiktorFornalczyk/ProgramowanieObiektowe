using Projekt_Programowanie_Obiektowe_71317.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;


namespace Projekt_Programowanie_Obiektowe_71317.Models
{
    /// <summary>
    /// Klasa spinająca wszystkie elementy systemu w jedno zlecenie transportowe.
    /// </summary>
    internal class Route : Asset, ITextSerializable
    {
        public string StartLocation { get; set; }
        public string EndLocation { get; set; }
        public double DistanceInKm { get; set; }
        public bool IsCompleted { get; set; }
        public Driver AssignedDriver { get; set; } 
        public Truck AssignedTruck { get; set; } 
        public Trailer AssignedTrailer { get; set; }
        public Cargo AssignedCargo { get; set; }

        public Route() : base()
        {
            StartLocation = string.Empty;
            EndLocation = string.Empty;
            DistanceInKm = 0.0;
            IsCompleted = false;
            AssignedDriver = new Driver();
            AssignedTruck = new Truck();
            AssignedTrailer = new Trailer();
            AssignedCargo = new Cargo();
        }

        /// Konstruktor klasy Route, który inicjalizuje wszystkie właściwości trasy.
        public Route(int Id, string Name, string StartLocation, string EndLocation, double DistanceInKm, bool IsCompleted, Driver Driver, Truck Truck, Trailer Trailer, Cargo Cargo) : base(Id, Name)
        {
            this.StartLocation = StartLocation;
            this.EndLocation = EndLocation;
            this.DistanceInKm = DistanceInKm;
            IsCompleted = false;
            AssignedDriver = Driver;
            AssignedTruck = Truck;
            AssignedTrailer = Trailer;
            AssignedCargo = Cargo;
        }
        /// <summary>
        /// Zwraca szczegółowy raport o trasie.
        /// </summary>
        public override string GetInfo()
        {
            return $"TRASA {Id}: {StartLocation} -> {EndLocation}, Firma: {Name}\n" +
               $"Kierowca: {AssignedDriver.GetInfo()}\n" +
               $"Pojazd: {AssignedTruck.RegistrationNumber}\n" +
               $"Naczepa: {AssignedTrailer.RegistrationNumber}\n" +
               $"Ładunek: {AssignedCargo.Name} {AssignedCargo.Description} ({AssignedCargo.Weight} t)\n" +
               $"Status: {(IsCompleted ? "Zakończona" : "W trakcie")}";
        }
        public override void DisplayInfo()
        {
            Console.WriteLine(GetInfo());
        }
        public override string ToDataLine()
        {
            return $"{Id};{Name};{StartLocation};{EndLocation};{DistanceInKm};{AssignedDriver};{AssignedTruck};{AssignedTrailer};{AssignedCargo};{IsCompleted}";
        }
        public override void FromDataLine(string line)
        {
            var p = line.Split(';');
            if (p.Length < 5) return;

            Id = int.Parse(p[0]);
            Name = p[1];
            StartLocation = p[2];
            EndLocation = p[3];
            DistanceInKm = double.Parse(p[4]);
            AssignedDriver = new Driver();
            AssignedDriver.FromDataLine(p[5]);
            AssignedTruck = new Truck();
            AssignedTruck.FromDataLine(p[6]);
            AssignedTrailer = new Trailer();
            AssignedTrailer.FromDataLine(p[7]);
            AssignedCargo = new Cargo();
            AssignedCargo.FromDataLine(p[8]);
        }
    }
}
