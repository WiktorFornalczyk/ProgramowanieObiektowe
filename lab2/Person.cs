using System.Globalization;

namespace lab2
{
    internal class Person
    {
        private string firstName, lastName;
        private int age;

        public Person()
        {
        }

        public Person(string firstName, string lastName, int age)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.age = age;
        }

        public string FirstName
        {
            get { return firstName; }
            set
            {
                if (string.IsNullOrWhiteSpace(value) || value.Length < 2)
                    throw new ArgumentException("First name must be at least 2 characters long and not empty.");
                firstName = value;
            }
        }
        public string LastName
        {
            get { return lastName; }
            set
            {
                if (string.IsNullOrWhiteSpace(value) || value.Length < 2)
                    throw new ArgumentException("First name must be at least 2 characters long and not empty.");
                lastName = value;
            }
        }
        public int Age
        {
            get { return age; }
            set
            {
                if (value < 0) 
                    throw new ArgumentException("Age cannot be negative."); 
                age = value;

            }
        }

        public void View()
        {
            Console.WriteLine($"Imie:{firstName}\tNazwisko:{lastName}\tWiek: {age}");
        }
    }
}
