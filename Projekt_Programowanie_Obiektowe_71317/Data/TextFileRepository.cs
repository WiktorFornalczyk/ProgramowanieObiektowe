using System;
using System.Collections.Generic;
using System.Text;
using Projekt_Programowanie_Obiektowe_71317.Interfaces;
using Projekt_Programowanie_Obiektowe_71317.Models;

namespace Projekt_Programowanie_Obiektowe_71317.Data
{
    internal class TextFileRepository<T>(string filePath) : IRepository<T>
    where T : Asset, ITextSerializable, new()
    {
        private readonly string _path = filePath;

        public IEnumerable<T> GetAll()
        {
            if (!File.Exists(_path)) return Enumerable.Empty<T>();

            return File.ReadAllLines(_path)
                .Select(line =>
                {
                    var item = new T();
                    item.FromDataLine(line);
                    return item;
                }).ToList();
        }

        public T? GetById(int id) => GetAll().FirstOrDefault(x => x.Id == id);

        public void Add(T entity)
        {
            // Automatyczne generowanie ID
            entity.Id = IdGenerator.GetNextId(_path);

            string line = entity.ToDataLine();

            File.AppendAllLines(_path, new[] {line});
        }

        public void Update(T entity)
        {
            var all = GetAll().ToList();
            var index = all.FindIndex(x => x.Id == entity.Id);

            if (index != -1)
            {
                all[index] = entity;
                SaveAll(all);
            }
        }

        public void Delete(int id)
        {
            var filtered = GetAll().Where(x => x.Id != id).ToList();
            SaveAll(filtered);
        }

        // Metoda pomocnicza do nadpisywania całego pliku
        private void SaveAll(IEnumerable<T> items)
        {
            File.WriteAllLines(_path, items.Select(x => x.ToDataLine()));
        }
    }

}
