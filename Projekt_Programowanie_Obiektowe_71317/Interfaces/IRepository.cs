using System;
using System.Collections.Generic;
using System.Text;

namespace Projekt_Programowanie_Obiektowe_71317.Interfaces
{
    /// <summary>
    /// Generyczny interfejs definiujący podstawowe operacje CRUD.
    /// 'T' musi być klasą dziedziczącą po Asset.
    /// </summary>
    /// <typeparam name="T">Typ modelu (np. Truck, Driver, Route)</typeparam>
    public interface IRepository<T> where T : class
    {
        // CREATE: Dodaje nowy obiekt do bazy
        void Add(T entity);

        // READ: Pobiera wszystkie obiekty danego typu
        IEnumerable<T> GetAll();

        // READ: Pobiera jeden konkretny obiekt po jego ID
        T? GetById(int id);

        // UPDATE: Aktualizuje dane istniejącego obiektu
        void Update(T entity);

        // DELETE: Usuwa obiekt z bazy na podstawie ID
        void Delete(int id);
    }
}
