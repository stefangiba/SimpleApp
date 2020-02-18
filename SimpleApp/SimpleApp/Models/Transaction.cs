using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleApp.Models {
    class Transaction {
        public string Id { get; set; }
        public string Comerciant { get; set; }
        public string Stare { get; set; }
        public string Data { get; set; }
        public string Cifre { get; set; }
        public string Suma { get; set; }
        public string Categorie { get; set; }
        public string Culoare { get; set; }

        public Transaction(string Id, string Comerciant, string Stare, string Data, string Cifre, string Suma, string Categorie, string Culoare)
        {
            this.Id = Id;
            this.Comerciant = Comerciant;
            this.Stare = Stare;
            this.Data = Data;
            this.Cifre = Cifre;
            this.Suma = Suma;
            this.Categorie = Categorie;
            this.Culoare = Culoare;
        }


    }
}
