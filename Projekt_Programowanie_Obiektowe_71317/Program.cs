using Projekt_Programowanie_Obiektowe_71317;
using Projekt_Programowanie_Obiektowe_71317.Models;
using System;

internal class Program
{
    private static void Main(string[] args)
    {
        Truck truck = new Truck(1, "Ciężarówka Volvo", "RKR2x234",20000.2, 500, EuroNorm.Euro6a);
        truck.DisplayInfo();

        HeavyTrailer heavyTrailer = new HeavyTrailer(2, "Naczepa Schmitz", 15000.5, "TRK1234", TypeOfTrailer.Kontener, 3, true);
        heavyTrailer.DisplayInfo();

        Cargo cargo = new Cargo(3, "Sadza", "N326", 20.1, false, "Temperatura mniejsza niż 60 stopni");
        cargo.DisplayInfo();
    }
}