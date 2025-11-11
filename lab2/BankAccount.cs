using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab2
{
    internal class BankAccount
    {
        public string Wlasciciel { get; set; }

        private decimal saldo;

        public decimal Saldo { get { return saldo; } }

        public BankAccount(string wlasciciel, decimal saldo)
        {
            Wlasciciel = wlasciciel;
            this.saldo = saldo;
        }

        public void Wplata(decimal kwota)
        {
            saldo += kwota;
        }

        public void Wyplata(decimal kwota)
        {
            if (kwota > Saldo)
                throw new InvalidOperationException("Niewystarczajace środki na koncie.");
            saldo -= kwota;
        }
    }
}
