using lab2;

Console.ForegroundColor = ConsoleColor.Green;
Console.WriteLine("Zad 1");
Console.ResetColor();

Person person = new Person("Jan", "Kowalski", 30);
person.View();

Person[] people = new Person[3];

people[0] = new Person("Anna", "Nowak", 25);
people[1] = new Person("Piotr", "Zieliński", 40);
people[2] = new Person("Katarzyna", "Wiśniewska", 35);

foreach (var p in people)
{
    p.View();
}

Console.ForegroundColor = ConsoleColor.Green;
Console.WriteLine("Zad 2");
Console.ResetColor();

BankAccount konto = new BankAccount("Jan Kowalski", 1000);
konto.Wplata(500);
konto.Wyplata(200);
Console.WriteLine($"Saldo: {konto.Saldo}");


Console.ForegroundColor = ConsoleColor.Green;
Console.WriteLine("Zad 3");
Console.ResetColor();

int[] ocenyStudenta1 = { 1, 2, 4, 5, 5 };
Student student1 = new Student("Adrian", "Nowak", ocenyStudenta1);
Console.WriteLine($"Średnia ocen: {student1.SredniaOcen}");

Console.ForegroundColor = ConsoleColor.Green;
Console.WriteLine("Zad 4");
Console.ResetColor();

Licz l1 = new Licz(10);
l1.Dodaj(5);
l1.Wypisz();
l1.Odejmij(3);
l1.Wypisz();

Licz l2 = new Licz(24);
l2.Dodaj(7);
l2.Wypisz();
l2.Odejmij(8);
l2.Wypisz();

Console.ForegroundColor = ConsoleColor.Green;
Console.WriteLine("Zad 5");
Console.ResetColor();

int[] liczby = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
Sumator sumator = new Sumator(liczby);
sumator.Wypisz();
Console.WriteLine($"Suma wszystkich liczb: {sumator.Suma()}");
Console.WriteLine($"Suma liczb podzielnych przez 2: {sumator.SumaPodziel2()}");
Console.WriteLine($"Ilość elementów: {sumator.IleElementów()}");
sumator.Index(3, 10);
