using Projekt_Programowanie_Obiektowe_71317;
using Projekt_Programowanie_Obiektowe_71317.Data;
using Projekt_Programowanie_Obiektowe_71317.Exceptions;
using Projekt_Programowanie_Obiektowe_71317.Menu;
using Projekt_Programowanie_Obiektowe_71317.Models;
using Projekt_Programowanie_Obiektowe_71317.Services;
using System;
using System.Diagnostics;

namespace Projekt_Programowanie_Obiektowe_71317.Menu
{
    internal class Cargos
    {
        public Cargos()
        {

        }

        public void Menu()
        {
            string projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
            string storagePath = Path.Combine(projectDirectory, "DataStorage");
            var cargosRepository = new TextFileRepository<Cargo>(Path.Combine(storagePath, "cargos.txt"));

            bool running = true;

            while (running)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("=== SYSTEM ZARZĄDZANIA FLOTĄ ===");
                Console.WriteLine("1. Dodaj Ładunek");
                Console.WriteLine("2. Wyświetl wszystkie ładunki");
                Console.WriteLine("3. Edytuj Ładunek");
                Console.WriteLine("4. Usuń Ładunek");
                Console.WriteLine("0. Wyjście");
                Console.Write("Wybierz opcję: ");
                Console.ResetColor();

                string choice = Console.ReadLine() ?? "";

                try
                {
                    switch (choice)
                    {
                        case "1":
                            Add(cargosRepository);
                            break;

                        case "2":
                            Show(cargosRepository);
                            break;

                        case "3":
                            Edit(cargosRepository);
                            break;

                        case "4":
                            Delete(cargosRepository);
                            break;

                        case "0":
                            running = false;
                            break;

                        default:
                            Console.WriteLine("Nieprawidłowa opcja.");
                            break;
                    }
                }
                catch (ValidationException ex)
                {
                    // Tutaj łapiemy własne błędy 
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"\nBłąd walidacji: {ex.Message}");
                    Console.ResetColor();
                }
                catch (FormatException)
                {
                    // Tutaj łapiemy błędy, gdy ktoś wpisze tekst zamiast liczby
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nBłąd wprowadzono nieprawidłowy format liczbowy!");
                    Console.ResetColor();
                }
                catch (Exception ex)
                {
                    // Tutaj łapiemy niespodziewane błędy
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"\nWystąpił nieoczekiwany błąd: {ex.Message}");
                    Console.ResetColor();
                }

                if (running)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("\nNaciśnij dowolny klawisz, aby kontynuować...");
                    Console.ReadKey();
                    Console.ResetColor();
                }
            }
        }

        private void Add(TextFileRepository<Cargo> cargosRepository)
        {
            Console.WriteLine("Podaj nazwę ładunku: ");
            string name = (Console.ReadLine() ?? "").ToUpper();

            Console.Write("Podaj opis ładunku: ");
            string description = (Console.ReadLine() ?? "").ToUpper();

            Console.Write("Podaj wagę ładunku: ");
            double weight = double.Parse(Console.ReadLine() ?? "0");

            Console.Write("Czy ładunek jest delikatny (true lub false): ");
            bool isFragile = bool.Parse(Console.ReadLine() ?? "false");

            Console.Write("Podaj wymagania co do ładunku: ");
            string stipulation = (Console.ReadLine() ?? "").ToUpper();


            // Tworzymy obiekt (ID zostanie nadane automatycznie w Add)
            var newCargo = new Cargo(1, name, description, weight, isFragile, stipulation);

            // WALIDACJA przed zapisem
            DataValidator.ValidateCargo(newCargo);

            // ZAPIS do pliku
            cargosRepository.Add(newCargo);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Ładunek dodany pomyślnie!");
            Console.ResetColor();
        }

        private void Show(TextFileRepository<Cargo> cargosRepository)
        {
            Console.WriteLine("\nLISTA ŁADUNKÓW:");
            foreach (var cargo in cargosRepository.GetAll())
            {
                cargo.DisplayInfo();
            }
        }

        private bool Edit(TextFileRepository<Cargo> cargosRepository)
        {
            Console.Write("Podaj ID ładunku do edycji: ");
            int idToEdit = int.Parse(Console.ReadLine() ?? "0");

            var cargoToEdit = cargosRepository.GetById(idToEdit);
            if (cargoToEdit == null)
            {
                Console.WriteLine("Nie ma takiego ładunku!");
                return false;
            }

            Console.WriteLine($"Edytujesz: {cargoToEdit.Name}. Podaj nową nazwę (lub naciśnij Enter by zostawić): ");
            string newName = Console.ReadLine() ?? "";
            if (!string.IsNullOrWhiteSpace(newName)) cargoToEdit.Name = newName;

            Console.WriteLine($"Edytujesz: {cargoToEdit.Description}. Podaj nowy opis (lub naciśnij Enter by zostawić): ");
            string newDescription = Console.ReadLine() ?? "";
            if (!string.IsNullOrWhiteSpace(newDescription)) cargoToEdit.Description = newDescription;

            Console.Write($"Obecna waga: {cargoToEdit.Weight}. Podaj nową wagę (lub naciśnij Enter by zostawić): ");
            string newWeight = Console.ReadLine() ?? "";

            if (!string.IsNullOrWhiteSpace(newWeight))
            {
                if (double.TryParse(newWeight, out double outNewWeight))
                {
                    cargoToEdit.Weight = outNewWeight;
                }
            }

            Console.Write($"Edytujesz czy ładunek jest delikatny: {cargoToEdit.IsFragile}. Podaj nową wartość (true lub false) (lub naciśnij Enter by zostawić): ");
            string newIsFragile = Console.ReadLine() ?? "";

            if (!string.IsNullOrWhiteSpace(newIsFragile))
            {
                if (bool.TryParse(newIsFragile, out bool outNewIsFragile))
                {
                    cargoToEdit.IsFragile = outNewIsFragile;
                }
            }

            Console.WriteLine($"Edytujesz: {cargoToEdit.Stipulation}. Podaj nowe wymagania (lub naciśnij Enter by zostawić): ");
            string newStipulation = Console.ReadLine() ?? "";
            if (!string.IsNullOrWhiteSpace(newStipulation)) cargoToEdit.Stipulation = newStipulation;

            // Tworzymy obiekt (ID zostanie nadane automatycznie w Add)
            var newCargo = new Cargo(idToEdit, cargoToEdit.Name, cargoToEdit.Description, cargoToEdit.Weight, cargoToEdit.IsFragile, cargoToEdit.Stipulation);

            // WALIDACJA przed zapisem
            DataValidator.ValidateCargo(newCargo);

            // ZAPIS do pliku
            cargosRepository.Update(newCargo);

            return false;
        }

        private void Delete(TextFileRepository<Cargo> cargosRepository)
        {
            Console.WriteLine("Podaj ID ładunku do usunięcia: ");
            int deleteId = int.Parse(Console.ReadLine() ?? "0");
            cargosRepository.Delete(deleteId);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Ładunek usunięty pomyślnie!");
            Console.ResetColor();
        }
    }
}
