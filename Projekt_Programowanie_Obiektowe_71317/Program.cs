using Projekt_Programowanie_Obiektowe_71317;
using Projekt_Programowanie_Obiektowe_71317.Data;
using Projekt_Programowanie_Obiektowe_71317.Exceptions;
using Projekt_Programowanie_Obiektowe_71317.Menu;
using Projekt_Programowanie_Obiektowe_71317.Models;
using Projekt_Programowanie_Obiektowe_71317.Services;
using System;
using System.Diagnostics;

internal class Program
{
    private static void Main(string[] args)
    {
        bool running = true;

        while (running)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("=== SYSTEM ZARZĄDZANIA FLOTĄ ===");
            Console.WriteLine("Zarządzaj: ");
            Console.WriteLine("1. Ciężarówką");
            Console.WriteLine("2. Kierowcami");
            Console.WriteLine("3. Ładunkiem");
            Console.WriteLine("4. Naczepami");
            Console.WriteLine("5. Trasami");
            Console.WriteLine("0. Wyjście");
            Console.Write("Wybierz opcję: ");
            Console.ResetColor();

            string choice = Console.ReadLine() ?? "";

            switch (choice)
            {
                case "1":
                    Trucks trucks = new Trucks();
                    trucks.Menu();
                    break;
                case "2":
                    Drivers drivers = new Drivers();
                    drivers.Menu();
                    break;
                case "3":
                    Cargos cargos = new Cargos();
                    cargos.Menu();
                    break;
                case "4":
                    Trailers trailers = new Trailers();
                    trailers.Menu();
                    break;
                case "5":
                    Routes routes = new Routes();
                    routes.Menu();
                    break;
                case "0":
                    running = false;
                    break;
                default:
                    Console.WriteLine("Nieprawidłowa opcja.");
                    break;
            }
        }
    }
}