using Projekt_Programowanie_Obiektowe_71317.Data;
using Projekt_Programowanie_Obiektowe_71317.Exceptions;
using Projekt_Programowanie_Obiektowe_71317.Models;
using Projekt_Programowanie_Obiektowe_71317.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Projekt_Programowanie_Obiektowe_71317.Menu
{
    internal class Drivers
    {
        public Drivers()
        {
            
        }

        public void Menu()
        {
            string projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
            string storagePath = Path.Combine(projectDirectory, "DataStorage");
            var driversRepository = new TextFileRepository<Driver>(Path.Combine(storagePath, "drivers.txt"));

            bool running = true;

            while (running)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("=== SYSTEM ZARZĄDZANIA FLOTĄ ===");
                Console.WriteLine("1. Dodaj Kierowcę");
                Console.WriteLine("2. Wyświetl wszystkich kierowców");
                Console.WriteLine("3. Edytuj Kierowcę");
                Console.WriteLine("4. Usuń Kierowcę");
                Console.WriteLine("0. Wyjście");
                Console.Write("Wybierz opcję: ");
                Console.ResetColor();

                string choice = Console.ReadLine() ?? "";

                try
                {
                    switch (choice)
                    {
                        case "1":
                            Add(driversRepository);
                            break;

                        case "2":
                            Show(driversRepository);
                            break;

                        case "3":
                            Edit(driversRepository);
                            break;

                        case "4":
                            Delete(driversRepository);
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

        private void Add(TextFileRepository<Driver> driversRepository)
        {
            Console.WriteLine("Podaj imię kierowcy: ");
            string name = (Console.ReadLine() ?? "").ToUpper();

            Console.Write("Podaj nazwisko kierowców: ");
            string lastName = (Console.ReadLine() ?? "").ToUpper();

            Console.Write("Podaj PESEL kierowcy: ");
            string pesel = (Console.ReadLine() ?? "").ToUpper();

            Console.Write("Podaj numer prawa jazdy: ");
            string licenseNumber = (Console.ReadLine() ?? "").ToUpper();

            // Tworzymy obiekt (ID zostanie nadane automatycznie w Add)
            var newDriver = new Driver(1, name, lastName, pesel, licenseNumber);

            // WALIDACJA przed zapisem
            DataValidator.ValidateDriver(newDriver);

            // ZAPIS do pliku
            driversRepository.Add(newDriver);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Kierowca dodany pomyślnie!");
            Console.ResetColor();
        }

        private void Show(TextFileRepository<Driver> driversRepository)
        {
            Console.WriteLine("\nLISTA KIEROWCÓW:");
            foreach (var driver in driversRepository.GetAll())
            {
                driver.DisplayInfo();
            }
        }

        private bool Edit(TextFileRepository<Driver> driversRepository)
        {
            Console.Write("Podaj ID kierowcy do edycji: ");
            int idToEdit = int.Parse(Console.ReadLine() ?? "0");

            var driverToEdit = driversRepository.GetById(idToEdit);
            if (driverToEdit == null)
            {
                Console.WriteLine("Nie ma takiego kierowcy!");
                return false;
            }

            Console.WriteLine($"Edytujesz: {driverToEdit.Name}. Podaj nowe imię (lub naciśnij Enter by zostawić): ");
            string newName = Console.ReadLine() ?? "";
            if (!string.IsNullOrWhiteSpace(newName)) driverToEdit.Name = newName;

            Console.WriteLine($"Edytujesz: {driverToEdit.LastName}. Podaj nowe nazwisko (lub naciśnij Enter by zostawić): ");
            string newLastName = Console.ReadLine() ?? "";
            if (!string.IsNullOrWhiteSpace(newLastName)) driverToEdit.LastName = newLastName;

            Console.Write($"Edytujesz: {driverToEdit.Pesel}. Podaj nowy PESEL (lub naciśnij Enter by zostawić): ");
            string newPesel = Console.ReadLine() ?? "";
            if (!string.IsNullOrWhiteSpace(newPesel)) driverToEdit.Pesel = newPesel;

            Console.Write($"Edytujesz: {driverToEdit.LicenseNumber}. Podaj nowy numer prawa jazdy (lub naciśnij Enter by zostawić): ");
            string newLicenseNumber = Console.ReadLine() ?? "";
            if (!string.IsNullOrWhiteSpace(newLicenseNumber)) driverToEdit.LicenseNumber = newLicenseNumber;

            // Tworzymy obiekt (ID zostanie nadane automatycznie w Add)
            var newDriver = new Driver(idToEdit, driverToEdit.Name, driverToEdit.LastName, driverToEdit.Pesel, driverToEdit.LicenseNumber);

            // WALIDACJA przed zapisem
            DataValidator.ValidateDriver(newDriver);

            // ZAPIS do pliku
            driversRepository.Update(newDriver);


            return false;
        }

        private void Delete(TextFileRepository<Driver> driversRepository)
        {
            Console.WriteLine("Podaj ID kierowcy do usunięcia: ");
            int deleteId = int.Parse(Console.ReadLine() ?? "0");
            driversRepository.Delete(deleteId);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Kierowca usunięty pomyślnie!");
            Console.ResetColor();
        }
    }
}
