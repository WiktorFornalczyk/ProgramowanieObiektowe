using System;
using System.Collections.Generic;
using System.Text;

namespace Projekt_Programowanie_Obiektowe_71317
{
    internal class IdGenerator
    {
        /// <summary>
        ///     Sprawdza z pliku tekstowego maksymalne Id i zwraca następne dostępne Id.
        /// </summary>
        /// <param name="filePath">Ścieżka do pliku</param>
        /// <param name="lines">Pojedyncza zapisana linia w pliku</param>
        /// <param name="maxId">Największe Id w pliku</param>
        /// <returns>Następne możliwe do użycia Id</returns>
        public static int GetNextId(string filePath)
        {
            if (!File.Exists(filePath)) return 1; // Jeśli plik nie istnieje, zwracamy 1 jako pierwsze Id

            var lines = File.ReadAllLines(filePath);

            if (lines.Length == 0) return 1; // Jeśli plik jest pusty, zwracamy 1 jako pierwsze Id

            var maxId = lines
                .Select(line => line.Split(';')[0]) // Id jest pierwszym elementem oddzielonym średnikiem
                .Select(idStr => int.TryParse(idStr, out var id) ? id : 0) // Parsujemy Id, jeśli nie uda się, zwracamy 0
                .Max(); // Znajdujemy maksymalne Id w pliku

            return maxId++; // Zwracamy następne Id
        }
    }
}
