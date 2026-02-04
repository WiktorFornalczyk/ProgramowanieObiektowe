using Projekt_Programowanie_Obiektowe_71317.Data;
using Projekt_Programowanie_Obiektowe_71317.Exceptions;
using Projekt_Programowanie_Obiektowe_71317.Models;
using Projekt_Programowanie_Obiektowe_71317.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Projekt_Programowanie_Obiektowe_71317.Menu
{
    internal class Routes
    {
        private Driver driver;
        private Cargo cargo;
        private Truck truck;
        private HeavyTrailer trailer;

        public Routes()
        {
            
        }

        public void Menu()
        {
            string projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
            string storagePath = Path.Combine(projectDirectory, "DataStorage");
            var routesRepository = new TextFileRepository<Route>(Path.Combine(storagePath, "routes.txt"));
            var driversRepository = new TextFileRepository<Driver>(Path.Combine(storagePath, "drivers.txt"));
            var trucksRepository = new TextFileRepository<Truck>(Path.Combine(storagePath, "trucks.txt"));
            var trailersRepository = new TextFileRepository<HeavyTrailer>(Path.Combine(storagePath, "trailers.txt"));
            var cargosRepository = new TextFileRepository<Cargo>(Path.Combine(storagePath, "cargos.txt"));

            bool running = true;

            while (running)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("=== SYSTEM ZARZĄDZANIA FLOTĄ ===");
                Console.WriteLine("1. Dodaj Trasę");
                Console.WriteLine("2. Wyświetl wszystkie trasy");
                Console.WriteLine("3. Edytuj Trasę");
                Console.WriteLine("4. Usuń Trasę");
                Console.WriteLine("0. Wyjście");
                Console.Write("Wybierz opcję: ");
                Console.ResetColor();

                string choice = Console.ReadLine() ?? "";

                try
                {
                    switch (choice)
                    {
                        case "1":
                            Add(routesRepository, driversRepository, trucksRepository, trailersRepository, cargosRepository);
                            break;

                        case "2":
                            Show(routesRepository);
                            break;

                        case "3":
                            Edit(routesRepository, driversRepository, trucksRepository, trailersRepository, cargosRepository);
                            break;

                        case "4":
                            Delete(routesRepository);
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

        private void Add(TextFileRepository<Route> routesRepository, TextFileRepository<Driver> driversRepository, TextFileRepository<Truck> trucksRepository, TextFileRepository<HeavyTrailer> trailersRepository, TextFileRepository<Cargo> cargosRepository)
        {
            bool isCompleted = false;

            Console.WriteLine("Podaj miejsce rozpoczęcia: ");
            string startLocation = (Console.ReadLine() ?? "").ToUpper();

            Console.Write("Podaj miejsce docelowe: ");
            string endLocation = (Console.ReadLine() ?? "").ToUpper();

            Console.Write("Podaj długość trasy w km: ");
            double distanceInKm = double.Parse(Console.ReadLine() ?? "0");

            Console.WriteLine("Podaj id Kierowcy do przypisania na trasę: ");
            int idDriver = int.Parse(Console.ReadLine() ?? "0");
            var driverToAdd = driversRepository.GetById(idDriver);
            if (driverToAdd == null)
            {
                Console.WriteLine("Nie ma takiego kierowcy!");
                return;
            }
            driver = driverToAdd;

            Console.WriteLine("Podaj id ciężarówki do przypisania na trasę: ");
            int idTruck = int.Parse(Console.ReadLine() ?? "0");
            var truckToAdd = trucksRepository.GetById(idTruck);
            if (truckToAdd == null)
            {
                Console.WriteLine("Nie ma takiej ciężarówki!");
                return;
            }
            truck = truckToAdd;

            Console.WriteLine("Podaj id naczepy do przypisania na trasę: ");
            int idTrailer = int.Parse(Console.ReadLine() ?? "0");
            var trailerToAdd = trailersRepository.GetById(idTrailer);
            if (trailerToAdd == null)
            {
                Console.WriteLine("Nie ma takiej naczepy!");
                return;
            }
            trailer = trailerToAdd;

            Console.WriteLine("Podaj id ładunku do przypisania na trasę: ");
            int idCargo = int.Parse(Console.ReadLine() ?? "0");
            var cargoToAdd = cargosRepository.GetById(idCargo);
            if (cargoToAdd == null)
            {
                Console.WriteLine("Nie ma takiego ładunku!");
                return;
            }
            cargo = cargoToAdd;

            // Tworzymy obiekt (ID zostanie nadane automatycznie w Add)
            var newRoute = new Route(1, string.Empty, startLocation, endLocation, distanceInKm, isCompleted, driverToAdd, truckToAdd, trailerToAdd, cargoToAdd);

            // WALIDACJA przed zapisem
            DataValidator.ValidateRoute(newRoute);

            // ZAPIS do pliku
            routesRepository.Add(newRoute);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Trasa dodana pomyślnie!");
            Console.ResetColor();
        }

        private void Show(TextFileRepository<Route> routesRepository)
        {
            Console.WriteLine("\nLISTA TRAS:");
            foreach (var trailer in routesRepository.GetAll())
            {
                trailer.DisplayInfo();
            }
        }

        private bool Edit(TextFileRepository<Route> routesRepository, TextFileRepository<Driver> driversRepository, TextFileRepository<Truck> trucksRepository, TextFileRepository<HeavyTrailer> trailersRepository, TextFileRepository<Cargo> cargosRepository)
        {
            Console.Write("Podaj ID trasy do edycji: ");
            int idToEdit = int.Parse(Console.ReadLine() ?? "0");

            var routeToEdit = routesRepository.GetById(idToEdit);
            if (routeToEdit == null)
            {
                Console.WriteLine("Nie ma takiej trasy!");
                return false;
            }

            Console.WriteLine($"Edytujesz: {routeToEdit.StartLocation}. Podaj nowy początek (lub naciśnij Enter by zostawić): ");
            string newStartLocation = Console.ReadLine() ?? "";
            if (!string.IsNullOrWhiteSpace(newStartLocation)) routeToEdit.StartLocation = newStartLocation;

            Console.Write($"Edytujesz: {routeToEdit.EndLocation}. Podaj nowy koniec (lub naciśnij Enter by zostawić): ");
            string newEndLocation = Console.ReadLine() ?? "";
            if (!string.IsNullOrWhiteSpace(newEndLocation)) routeToEdit.EndLocation = newEndLocation;

            Console.Write($"Obecny dystans: {routeToEdit.DistanceInKm}. Podaj nowy dystans (lub naciśnij Enter by zostawić): ");
            string newDistance = Console.ReadLine() ?? "";
            if (!string.IsNullOrWhiteSpace(newDistance))
            {
                if (double.TryParse(newDistance, out double outNewDistance))
                {
                    routeToEdit.DistanceInKm = outNewDistance;
                }
            }

            Console.Write($"Czy ukończona: {routeToEdit.IsCompleted}. Podaj nowy status (true lub false) (lub naciśnij Enter by zostawić): ");
            string newIsCopleted = Console.ReadLine() ?? "";
            if (!string.IsNullOrWhiteSpace(newIsCopleted))
            {
                if (bool.TryParse(newIsCopleted, out bool outNewIsCopleted))
                {
                    routeToEdit.IsCompleted = outNewIsCopleted;
                }
            }

            Console.Write($"Kierowca: {driver}. Podaj id nowego kierowcy (lub naciśnij Enter by zostawić): ");
            string idDriver = Console.ReadLine() ?? "";
            var newDriver = driversRepository.GetById(int.Parse(idDriver));
            if (!string.IsNullOrWhiteSpace(idDriver))
            {
              
                if (newDriver == null)
                {
                    Console.WriteLine("Nie ma takiego kierowcy!");
                    return false;
                }
            }

            Console.Write($"Ciężarówka: {truck}. Podaj id nowej ciężarówki (lub naciśnij Enter by zostawić): ");
            string idTruck = Console.ReadLine() ?? "";
            var newTruck = trucksRepository.GetById(int.Parse(idTruck));
            if (!string.IsNullOrWhiteSpace(idTruck))
            {
                if (newTruck == null)
                {
                    Console.WriteLine("Nie ma takiej ciężarówki!");
                    return false;
                }
            }

            Console.Write($"Naczepa: {trailer}. Podaj id nowej naczepy (lub naciśnij Enter by zostawić): ");
            string idTailer = Console.ReadLine() ?? "";
            var newTrailer = trailersRepository.GetById(int.Parse(idTailer));
            if (!string.IsNullOrWhiteSpace(idTailer))
            {
                if (newTrailer == null)
                {
                    Console.WriteLine("Nie ma takiej naczepy!");
                    return false;
                }
            }

            Console.Write($"Ładunek: {cargo}. Podaj id nowego ładunku (lub naciśnij Enter by zostawić): ");
            string idCargo = Console.ReadLine() ?? "";
            var newCargo = cargosRepository.GetById(int.Parse(idCargo));
            if (!string.IsNullOrWhiteSpace(idCargo))
            {
                if (newCargo == null)
                {
                    Console.WriteLine("Nie ma takiego ładunku!");
                    return false;
                }
            }

            // Tworzymy obiekt (ID zostanie nadane automatycznie w Add)
            var newRoute = new Route(idToEdit, string.Empty, newStartLocation, newEndLocation, double.Parse(newDistance), bool.Parse(newIsCopleted), newDriver, newTruck, newTrailer, newCargo);

            // WALIDACJA przed zapisem
            DataValidator.ValidateRoute(newRoute);

            // ZAPIS do pliku
            routesRepository.Update(newRoute);

            return false;
        }

        private void Delete(TextFileRepository<Route> routesRepository)
        {
            Console.WriteLine("Podaj ID trasy do usunięcia: ");
            int deleteId = int.Parse(Console.ReadLine() ?? "0");
            routesRepository.Delete(deleteId);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Trasa usunięta pomyślnie!");
            Console.ResetColor();
        }
    }
}
