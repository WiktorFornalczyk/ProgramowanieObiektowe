using Projekt_Programowanie_Obiektowe_71317.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Text;

namespace Projekt_Programowanie_Obiektowe_71317.Services
{
    /// <summary>
    /// Klasa walidująca wprowadzanie dane 
    /// </summary>
    internal class DataValidator
    {
        // Walidacja klasy Asset
        public static void ValidateAsset(Asset asset)
        {
            if (asset.Id < 1)
                throw new ValidationException("Id nie może być ujemne lub równe 0.");

            if (string.IsNullOrWhiteSpace(asset.Name))
                throw new ValidationException("Nazwa nie może być pusta.");
        }

        // Walidacja klasy Vehicle
        public static void ValidateVehicle(Vehicle vehicle)
        {
            ValidateAsset(vehicle);

            if (string.IsNullOrWhiteSpace(vehicle.RegistrationNumber))
                throw new ValidationException("Numer rejestracyjny nie może być pusty.");

            if (vehicle.RegistrationNumber.Length < 5 || vehicle.RegistrationNumber.Length > 10)
                throw new ValidationException("Numer rejestracyjny musi mieć od 5 do 10 znaków.");

            if (vehicle.Kilometers < 0)
                throw new ValidationException("Przebieg nie może być ujemny.");
        }

        // Walidacja klasy Truck
        public static void ValidateTruck(Truck truck)
        {
            ValidateAsset(truck);
            ValidateVehicle(truck);

            if (truck.EnginePower < 200 || truck.EnginePower > 800)
                throw new ValidationException("Moc silnika musi być większa niż 200 i nie większa od 800.");
        }

        // Walidacja klasy Trailer
        public static void ValidateTrailer(Trailer trailer)
        {
            ValidateAsset(trailer);

            if (string.IsNullOrWhiteSpace(trailer.RegistrationNumber))
                throw new ValidationException("Numer rejestracyjny naczepy nie może być pusty.");

            if (trailer.RegistrationNumber.Length < 5 || trailer.RegistrationNumber.Length > 10)
                throw new ValidationException("Numer rejestracyjny musi mieć od 5 do 10 znaków.");

            if (trailer.MaxPayload <= 0)
                throw new ValidationException("Ładowność naczepy musi być większa niż 0.");

            if (trailer.MaxPayload > 40)
                throw new ValidationException("Ładowność naczepy nie może przekraczać 40 ton.");
        }

        // Walidacja klasy HeavyTrailer
        public static void ValidateHeavyTrailer(HeavyTrailer heavyTrailer)
        {
            ValidateAsset(heavyTrailer);
            ValidateTrailer(heavyTrailer);

            if (heavyTrailer.NumberOfAxles < 2 || heavyTrailer.NumberOfAxles > 5)
                throw new ValidationException("Liczba osi naczepy musi być od 2 do 5.");
        }

        // Walidacja klasy Driver
        public static void ValidateDriver(Driver driver)
        {
            ValidateAsset(driver);

            if (string.IsNullOrWhiteSpace(driver.LastName))
                throw new ValidationException("Nazwisko kierowcy nie może być puste.");

            if (driver.Pesel.ToString().Length != 11 || !driver.Pesel.ToString().All(char.IsDigit))
                throw new ValidationException("PESEL musi mieć dokładnie 11 cyfr.");

            if (string.IsNullOrWhiteSpace(driver.LicenseNumber))
                throw new ValidationException("Numer prawa jazdy nie może być pusty.");

            if (driver.LicenseNumber.Length < 5 || driver.LicenseNumber.Length > 15)
                throw new ValidationException("Numer prawa jazdy musi mieć od 5 do 15 znaków.");

            if (!char.IsLetter(driver.LicenseNumber[0]))
                throw new ValidationException("Numer prawa jazdy musi zaczynać się od litery.");

            if (!char.IsDigit(driver.LicenseNumber[^1]))
                throw new ValidationException("Numer prawa jazdy musi kończyć się cyfrą.");

            int age = Age(driver.Pesel.ToString());

            if (age < 18)
                throw new ValidationException("Kierowca musi mieć ukończone 18 lat.");

            if (age > 70)
                throw new ValidationException("Kierowca nie może mieć więcej niż 70 lat.");
        }

        /// <summary>
        /// Klasa obliczająca wiek kierowcy na podstawie numeru PESEL.
        /// </summary>
        /// <param name="pesel"> Pesel kierowcy </param>
        /// <returns> Zwraca wiek kierowcy </returns>
        private static int Age(string pesel)
        {
            // Pobieramy fragmenty stringa i zamieniamy na liczby
            int rok = int.Parse(pesel.Substring(0, 2));
            int miesiac = int.Parse(pesel.Substring(2, 2));
            int dzien = int.Parse(pesel.Substring(4, 2));

            // Określamy wiek na podstawie miesiąca urodzenia
            if (miesiac > 80 && miesiac < 93) { rok += 1800; miesiac -= 80; }
            else if (miesiac > 0 && miesiac < 13) { rok += 1900; }
            else if (miesiac > 20 && miesiac < 33) { rok += 2000; miesiac -= 20; }
            else if (miesiac > 40 && miesiac < 53) { rok += 2100; miesiac -= 40; }
            else if (miesiac > 60 && miesiac < 73) { rok += 2200; miesiac -= 60; }
            else
            {
                throw new ArgumentException("Błędny miesiąc w numerze PESEL.");
            }

            DateTime dataUrodzenia = new DateTime(rok, miesiac, dzien);

            DateTime dzisiaj = DateTime.Today;

            int wiek = dzisiaj.Year - dataUrodzenia.Year;

            // Jeśli dzisiejsza data jest mniejsza niż data urodzenia w tym roku, odejmujemy 1 rok.
            if (dataUrodzenia.Date > dzisiaj.AddYears(-wiek))
            {
                wiek--;
            }

            return wiek;
        }

        // Walidacja klasy Cargo
        public static void ValidateCargo(Cargo cargo)
        {
            ValidateAsset(cargo);

            if (cargo.Weight < 0)
                throw new ValidationException("Waga nie może być ujemna");
        }

        // Walidacja klasy Route
        public static void ValidateRoute(Route route)
        {
            ValidateAsset(route);

            // Sprawdzenie, czy wszystkie obiekty istnieją
            if (route.AssignedDriver == null) throw new ValidationException("Trasa musi mieć przypisanego kierowcę.");
            if (route.AssignedTruck == null) throw new ValidationException("Trasa musi mieć przypisaną ciężarówkę.");
            if (route.AssignedCargo == null) throw new ValidationException("Trasa musi mieć przypisany ładunek.");

            // Sprawdzamy czy naczepa wytrzyma wagę ładunku
            if (route.AssignedTrailer != null)
            {
                if (route.AssignedCargo.Weight > route.AssignedTrailer.MaxPayload)
                {
                    throw new ValidationException(
                        $"Przeładowanie! Ładunek ({route.AssignedCargo.Weight}t) przekracza " +
                        $"możliwości naczepy {route.AssignedTrailer.RegistrationNumber} ({route.AssignedTrailer.MaxPayload}t).");
                }
            }

            // Na każde 5 ton ładunku potrzeba min. 100 KM mocy, sprawdzenie czy ciężarówka spełnia ten warunek
            double requiredPower = (route.AssignedCargo.Weight / 5.0) * 100;
            if (route.AssignedTruck.EnginePower < requiredPower)
            {
                throw new ValidationException(
                    $"Ciężarówka {route.AssignedTruck.RegistrationNumber} ma za małą moc ({route.AssignedTruck.EnginePower}KM) " +
                    $"do tak ciężkiego ładunku. Wymagane ok. {requiredPower}KM.");
            }

            if (route.AssignedTrailer is HeavyTrailer heavyTrailer)
            {
                // Jeśli ładunek jest cięższy niż 30t, wymagamy co najmniej 4 osi
                if (route.AssignedCargo.Weight > 30 && heavyTrailer.NumberOfAxles < 4)
                {
                    throw new ValidationException("Bezpieczeństwo: Ładunek powyżej 30t wymaga naczepy z minimum 4 osiami.");
                }
            }
        }
    }
}
