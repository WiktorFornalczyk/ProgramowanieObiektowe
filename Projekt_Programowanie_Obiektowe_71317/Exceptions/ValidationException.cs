using System;
using System.Collections.Generic;
using System.Text;

namespace Projekt_Programowanie_Obiektowe_71317.Exceptions
{
    /// <summary>
    /// Wyjątek rzucany, gdy dane wejściowe nie spełniają reguł biznesowych systemu.
    /// </summary>
    public class ValidationException(string message) : Exception(message);
}
