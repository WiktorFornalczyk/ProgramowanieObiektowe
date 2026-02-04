using Projekt_Programowanie_Obiektowe_71317.Data;
using Projekt_Programowanie_Obiektowe_71317.Exceptions;
using Projekt_Programowanie_Obiektowe_71317.Models;
using Projekt_Programowanie_Obiektowe_71317.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Projekt_Programowanie_Obiektowe_71317.Menu
{
    internal class Trucks
    {
        public Trucks()
        {
        }

        public void Menu()
        {
            string projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
            string storagePath = Path.Combine(projectDirectory, "DataStorage");
            var trucksRepository = new TextFileRepository<Truck>(Path.Combine(storagePath, "trucks.txt"));

            bool running = true;

            while (running)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("=== SYSTEM ZARZĄDZANIA FLOTĄ ===");
                Console.WriteLine("1. Dodaj Ciężarówkę");
                Console.WriteLine("2. Wyświetl wszystkie Ciężarówki");
                Console.WriteLine("3. Edytuj Ciężarówkę");
                Console.WriteLine("4. Usuń Ciężarówkę");
                Console.WriteLine("0. Wyjście");
                Console.Write("Wybierz opcję: ");
                Console.ResetColor();

                string choice = Console.ReadLine() ?? "";

                try
                {
                    switch (choice)
                    {
                        case "1":
                            Add(trucksRepository);
                            break;

                        case "2":
                            Show(trucksRepository);
                            break;

                        case "3":
                            Edit(trucksRepository);
                            break;

                        case "4":
                            Delete(trucksRepository);
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

        private void Add(TextFileRepository<Truck> trucksRepository)
        {
            Console.WriteLine("Podaj nazwę ciężarówki: ");
            string name = (Console.ReadLine() ?? "").ToUpper();

            Console.Write("Podaj numer rejestracyjny: ");
            string regNum = (Console.ReadLine() ?? "").ToUpper();

            Console.Write("Podaj przebieg (Kilometry): ");
            double km = double.Parse(Console.ReadLine() ?? "0");

            Console.Write("Podaj moc silnika (KM): ");
            int enPow = int.Parse(Console.ReadLine() ?? "0");

            Console.WriteLine("Wybierz normę Euro: ");
            Console.WriteLine("1. Euro1");
            Console.WriteLine("2. Euro2");
            Console.WriteLine("3. Euro3");
            Console.WriteLine("4. Euro4");
            Console.WriteLine("5. Euro5");
            Console.WriteLine("6. Euro6");
            Console.WriteLine("7. Euro6a");
            Console.WriteLine("8. Euro6b");
            Console.WriteLine("9. Euro6c");
            Console.WriteLine("10. Euro6d");
            Console.WriteLine("Wybierz (1-10): ");
            string eNT = Console.ReadLine() ?? "";
            if (eNT != "1" && eNT != "2" && eNT != "3" && eNT != "4" && eNT != "5" &&
                eNT != "6" && eNT != "7" && eNT != "8" && eNT != "9" && eNT != "10")
            {
                Console.ForegroundColor = ConsoleColor.Red;
                throw new ValidationException("Nieprawidłowa norma Euro.");
            }
            Console.ResetColor();
            EuroNorm euroNormTru = EuroNorm.Euro1;
            switch (eNT)
            {
                case "1":
                    break;
                case "2":
                    euroNormTru = EuroNorm.Euro2;
                    break;
                case "3":
                    euroNormTru = EuroNorm.Euro3;
                    break;
                case "4":
                    euroNormTru = EuroNorm.Euro4;
                    break;
                case "5":
                    euroNormTru = EuroNorm.Euro5;
                    break;
                case "6":
                    euroNormTru = EuroNorm.Euro6;
                    break;
                case "7":
                    euroNormTru = EuroNorm.Euro6a;
                    break;
                case "8":
                    euroNormTru = EuroNorm.Euro6b;
                    break;
                case "9":
                    euroNormTru = EuroNorm.Euro6c;
                    break;
                case "10":
                    euroNormTru = EuroNorm.Euro6d;
                    break;
            }

            // Tworzymy obiekt (ID zostanie nadane automatycznie w Add)
            var newTruck = new Truck(1, name, regNum, km, enPow, euroNormTru);

            // WALIDACJA przed zapisem
            DataValidator.ValidateTruck(newTruck);

            // ZAPIS do pliku
            trucksRepository.Add(newTruck);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Ciężarówka dodana pomyślnie!");
            Console.ResetColor();
        }

        private void Show(TextFileRepository<Truck> trucksRepository)
        {
            Console.WriteLine("\nLISTA CIĘŻARÓWEK:");
            foreach (var truck in trucksRepository.GetAll())
            {
                truck.DisplayInfo();
            }
        }

        private bool Edit(TextFileRepository<Truck> trucksRepository)
        {
            Console.Write("Podaj ID ciężarówki do edycji: ");
            int idToEdit = int.Parse(Console.ReadLine() ?? "0");

            var truckToEdit = trucksRepository.GetById(idToEdit);
            if (truckToEdit == null)
            {
                Console.WriteLine("Nie ma takiej ciężarówki!");
                return false;
            }

            Console.WriteLine($"Edytujesz: {truckToEdit.Name}. Podaj nową nazwę (lub naciśnij Enter by zostawić): ");
            string newName = Console.ReadLine() ?? "";
            if (!string.IsNullOrWhiteSpace(newName)) truckToEdit.Name = newName;

            Console.WriteLine($"Edytujesz: {truckToEdit.RegistrationNumber}. Podaj nową rejestrację (lub naciśnij Enter by zostawić): ");
            string newRegNum = Console.ReadLine() ?? "";
            if (!string.IsNullOrWhiteSpace(newRegNum)) truckToEdit.RegistrationNumber = newRegNum;

            Console.Write($"Obecny przebieg: {truckToEdit.Kilometers}. Podaj nowy przebieg (lub naciśnij Enter by zostawić): ");
            string newKm = Console.ReadLine() ?? "";

            if (!string.IsNullOrWhiteSpace(newKm))
            {
                if (double.TryParse(newKm, out double outNewKm))
                {
                    truckToEdit.Kilometers = outNewKm;
                }
            }

            Console.Write($"\nObecna moc silnika: {truckToEdit.EnginePower}. Podaj nową moc silnika (lub naciśnij Enter by zostawić): ");
            string newEnginePower = Console.ReadLine() ?? "";

            if (!string.IsNullOrWhiteSpace(newEnginePower))
            {
                if (int.TryParse(newEnginePower, out int outNewEnginePower))
                {
                    truckToEdit.EnginePower = outNewEnginePower;
                }
            }

            Console.Write($"\nObecna norma: {truckToEdit.EuroNorm}. Podaj nową normę (lub naciśnij Enter by zostawić): ");
            Console.WriteLine("Wybierz normę Euro: ");
            Console.WriteLine("1. Euro1");
            Console.WriteLine("2. Euro2");
            Console.WriteLine("3. Euro3");
            Console.WriteLine("4. Euro4");
            Console.WriteLine("5. Euro5");
            Console.WriteLine("6. Euro6");
            Console.WriteLine("7. Euro6a");
            Console.WriteLine("8. Euro6b");
            Console.WriteLine("9. Euro6c");
            Console.WriteLine("10. Euro6d");
            Console.WriteLine("Wybierz (1-10): ");
            string eNT = Console.ReadLine() ?? "";
            if (!string.IsNullOrWhiteSpace(eNT))
            {
                if (eNT != "1" && eNT != "2" && eNT != "3" && eNT != "4" && eNT != "5" &&
                eNT != "6" && eNT != "7" && eNT != "8" && eNT != "9" && eNT != "10")
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    throw new ValidationException("Nieprawidłowa norma Euro.");
                }
                Console.ResetColor();
                EuroNorm newNorm = EuroNorm.Euro1;
                switch (eNT)
                {
                    case "1":
                        break;
                    case "2":
                        newNorm = EuroNorm.Euro2;
                        break;
                    case "3":
                        newNorm = EuroNorm.Euro3;
                        break;
                    case "4":
                        newNorm = EuroNorm.Euro4;
                        break;
                    case "5":
                        newNorm = EuroNorm.Euro5;
                        break;
                    case "6":
                        newNorm = EuroNorm.Euro6;
                        break;
                    case "7":
                        newNorm = EuroNorm.Euro6a;
                        break;
                    case "8":
                        newNorm = EuroNorm.Euro6b;
                        break;
                    case "9":
                        newNorm = EuroNorm.Euro6c;
                        break;
                    case "10":
                        newNorm = EuroNorm.Euro6d;
                        break;
                }
                
                truckToEdit.EuroNorm = newNorm;
            }

            // Tworzymy obiekt (ID zostanie nadane automatycznie w Add)
            var editTruck = new Truck(idToEdit, truckToEdit.Name, truckToEdit.RegistrationNumber, truckToEdit.Kilometers, truckToEdit.EnginePower, truckToEdit.EuroNorm);

            // WALIDACJA przed zapisem
            DataValidator.ValidateTruck(editTruck);

            // ZAPIS do pliku
            trucksRepository.Update(editTruck);

            return false;
        }

        private void Delete(TextFileRepository<Truck> trucksRepository)
        {
            Console.WriteLine("Podaj ID ciężarówki do usunięcia: ");
            int deleteId = int.Parse(Console.ReadLine() ?? "0");
            trucksRepository.Delete(deleteId);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Ciężarówka usunięta pomyślnie!");
            Console.ResetColor();
        }
    }
}
