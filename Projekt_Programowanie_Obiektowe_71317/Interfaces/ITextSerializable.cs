using System;
using System.Collections.Generic;
using System.Text;
using Projekt_Programowanie_Obiektowe_71317.Models;

namespace Projekt_Programowanie_Obiektowe_71317.Interfaces
{
    public interface ITextSerializable
    {
        string ToDataLine();           // Zamiana obiektu na format: Id;Pole1;Pole2
        void FromDataLine(string line); // Rozbicie linii tekstu na pola obiektu
    }
}
