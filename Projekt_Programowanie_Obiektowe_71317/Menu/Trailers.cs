using Projekt_Programowanie_Obiektowe_71317.Data;
using Projekt_Programowanie_Obiektowe_71317.Exceptions;
using Projekt_Programowanie_Obiektowe_71317.Models;
using Projekt_Programowanie_Obiektowe_71317.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace Projekt_Programowanie_Obiektowe_71317.Menu
{
    internal class Trailers
    {
        public Trailers()
        {

        }

        public void Menu()
        {
            string projectDirectory = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
            string storagePath = Path.Combine(projectDirectory, "DataStorage");
            var trailersRepository = new TextFileRepository<HeavyTrailer>(Path.Combine(storagePath, "trailers.txt"));

            bool running = true;

            while (running)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("=== SYSTEM ZARZĄDZANIA FLOTĄ ===");
                Console.WriteLine("1. Dodaj Naczepę");
                Console.WriteLine("2. Wyświetl wszystkie naczepy");
                Console.WriteLine("3. Edytuj Naczepę");
                Console.WriteLine("4. Usuń Naczepę");
                Console.WriteLine("0. Wyjście");
                Console.Write("Wybierz opcję: ");
                Console.ResetColor();

                string choice = Console.ReadLine() ?? "";

                try
                {
                    switch (choice)
                    {
                        case "1":
                            Add(trailersRepository);
                            break;

                        case "2":
                            Show(trailersRepository);
                            break;

                        case "3":
                            Edit(trailersRepository);
                            break;

                        case "4":
                            Delete(trailersRepository);
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

        private void Add(TextFileRepository<HeavyTrailer> trailersRepository)
        {
            Console.WriteLine("Podaj nazwę naczepy: ");
            string name = (Console.ReadLine() ?? "").ToUpper();

            Console.Write("Podaj maksymalną ładowność naczepy (tony): ");
            double maxPayload = double.Parse(Console.ReadLine() ?? "0");

            Console.Write("Podaj numer rejestracyjny: ");
            string regNum = (Console.ReadLine() ?? "").ToUpper();

            Console.Write("Podaj ilość osi naczepy: ");
            int numberOfAxles = int.Parse(Console.ReadLine() ?? "0");

            Console.Write("Czy naczepa jest nadgabarytem (true lub false): ");
            bool hasOversizePermit = bool.Parse(Console.ReadLine() ?? "false");

            Console.WriteLine("Wybierz typ naczepy: ");
            Console.WriteLine("1. Platforma");
            Console.WriteLine("2. Plandeka");
            Console.WriteLine("3. Chłodnia");
            Console.WriteLine("4. Cysterna");
            Console.WriteLine("5. Niskopodwoziowa");
            Console.WriteLine("6. Kontener");
            Console.WriteLine("Wybierz (1-6): ");
            string eNT = Console.ReadLine() ?? "";
            if (eNT != "1" && eNT != "2" && eNT != "3" && eNT != "4" && eNT != "5" && eNT != "6")
            {
                Console.ForegroundColor = ConsoleColor.Red;
                throw new ValidationException("Nieprawidłowy typ naczepy.");
            }
            Console.ResetColor();
            TypeOfTrailer typeOfTrailer = TypeOfTrailer.Platforma;
            switch (eNT)
            {
                case "1":
                    break;
                case "2":
                    typeOfTrailer = TypeOfTrailer.Plandeka;
                    break;
                case "3":
                    typeOfTrailer = TypeOfTrailer.Chłodnia;
                    break;
                case "4":
                    typeOfTrailer = TypeOfTrailer.Cysterna;
                    break;
                case "5":
                    typeOfTrailer = TypeOfTrailer.Niskopodwoziowa;
                    break;
                case "6":
                    typeOfTrailer = TypeOfTrailer.Kontener;
                    break;
            }

            // Tworzymy obiekt (ID zostanie nadane automatycznie w Add)
            var newTrailer = new HeavyTrailer(1, name, maxPayload, regNum, typeOfTrailer, numberOfAxles, hasOversizePermit);

            // WALIDACJA przed zapisem
            DataValidator.ValidateHeavyTrailer(newTrailer);

            // ZAPIS do pliku
            trailersRepository.Add(newTrailer);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Naczepa dodana pomyślnie!");
            Console.ResetColor();
        }

        private void Show(TextFileRepository<HeavyTrailer> trailersRepository)
        {
            Console.WriteLine("\nLISTA CIĘŻARÓWEK:");
            foreach (var trailer in trailersRepository.GetAll())
            {
                trailer.DisplayInfo();
            }
        }

        private bool Edit(TextFileRepository<HeavyTrailer> trailersRepository)
        {
            Console.Write("Podaj ID naczepy do edycji: ");
            int idToEdit = int.Parse(Console.ReadLine() ?? "0");

            var trailerToEdit = trailersRepository.GetById(idToEdit);
            if (trailerToEdit == null)
            {
                Console.WriteLine("Nie ma takiej naczepy!");
                return false;
            }

            Console.WriteLine($"Edytujesz: {trailerToEdit.Name}. Podaj nową nazwę (lub naciśnij Enter by zostawić): ");
            string newName = Console.ReadLine() ?? "";
            if (!string.IsNullOrWhiteSpace(newName)) trailerToEdit.Name = newName;

            Console.Write($"Obecny maksymalna ładowność: {trailerToEdit.MaxPayload}. Podaj nową maksymalną ładowność (lub naciśnij Enter by zostawić): ");
            string newMaxPayload = Console.ReadLine() ?? "";

            if (!string.IsNullOrWhiteSpace(newMaxPayload))
            {
                if (double.TryParse(newMaxPayload, out double outNewMaxPayload))
                {
                    trailerToEdit.MaxPayload = outNewMaxPayload;
                }
            }

            Console.WriteLine($"Edytujesz: {trailerToEdit.RegistrationNumber}. Podaj nową rejestrację (lub naciśnij Enter by zostawić): ");
            string newRegNum = Console.ReadLine() ?? "";
            if (!string.IsNullOrWhiteSpace(newRegNum)) trailerToEdit.RegistrationNumber = newRegNum;

            Console.Write($"Obecny ilość osi: {trailerToEdit.NumberOfAxles}. Podaj nową ilość osi (lub naciśnij Enter by zostawić): ");
            string newNumberOfAxles = Console.ReadLine() ?? "";

            if (!string.IsNullOrWhiteSpace(newNumberOfAxles))
            {
                if (int.TryParse(newNumberOfAxles, out int outNewNumberOfAxles))
                {
                    trailerToEdit.NumberOfAxles = outNewNumberOfAxles;
                }
            }

            Console.Write($"Aktualnie ponadgabaryt: {trailerToEdit.HasOversizePermit}. Podaj nową wartość ponadgabarytu (true lub false) (lub naciśnij Enter by zostawić): ");
            string newHasOversizePermit = Console.ReadLine() ?? "";
            
            if (!string.IsNullOrWhiteSpace(newHasOversizePermit))
            {
                if (bool.TryParse(newHasOversizePermit, out bool outNewHasOversizePermit))
                {
                    trailerToEdit.HasOversizePermit = outNewHasOversizePermit;
                }
            }
            
            Console.Write($"Obecny typ: {trailerToEdit.TypeOfTrailer}. Podaj nową normę (lub naciśnij Enter by zostawić): ");
            Console.WriteLine("Wybierz typ naczepy: ");
            Console.WriteLine("1. Platforma");
            Console.WriteLine("2. Plandeka");
            Console.WriteLine("3. Chłodnia");
            Console.WriteLine("4. Cysterna");
            Console.WriteLine("5. Niskopodwoziowa");
            Console.WriteLine("6. Kontener");
            Console.WriteLine("Wybierz (1-6): ");
            string eNT = Console.ReadLine() ?? "";
            if (!string.IsNullOrWhiteSpace(eNT))
            {
                if (eNT != "1" && eNT != "2" && eNT != "3" && eNT != "4" && eNT != "5" && eNT != "6")
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    throw new ValidationException("Nieprawidłowy typ naczepy.");
                }
                Console.ResetColor();
                TypeOfTrailer newTypeOfTrailer = TypeOfTrailer.Platforma;
                switch (eNT)
                {
                    case "1":
                        break;
                    case "2":
                        newTypeOfTrailer = TypeOfTrailer.Plandeka;
                        break;
                    case "3":
                        newTypeOfTrailer = TypeOfTrailer.Chłodnia;
                        break;
                    case "4":
                        newTypeOfTrailer = TypeOfTrailer.Cysterna;
                        break;
                    case "5":
                        newTypeOfTrailer = TypeOfTrailer.Niskopodwoziowa;
                        break;
                    case "6":
                        newTypeOfTrailer = TypeOfTrailer.Kontener;
                        break;
                }
                trailerToEdit.TypeOfTrailer = newTypeOfTrailer;
            }


            // Tworzymy obiekt (ID zostanie nadane automatycznie w Add)
            var newTrailer = new HeavyTrailer(idToEdit, trailerToEdit.Name, trailerToEdit.MaxPayload, trailerToEdit.RegistrationNumber, trailerToEdit.TypeOfTrailer, trailerToEdit.NumberOfAxles, trailerToEdit.HasOversizePermit);

            // WALIDACJA przed zapisem
            DataValidator.ValidateHeavyTrailer(newTrailer);

            // ZAPIS do pliku
            trailersRepository.Update(newTrailer);

            return false;
        }

        private void Delete(TextFileRepository<HeavyTrailer> trailersRepository)
        {
            Console.WriteLine("Podaj ID naczepy do usunięcia: ");
            int deleteId = int.Parse(Console.ReadLine() ?? "0");
            trailersRepository.Delete(deleteId);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Naczepa usunięta pomyślnie!");
            Console.ResetColor();
        }
    }
}
