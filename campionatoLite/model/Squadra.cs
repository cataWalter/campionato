using System;
using System.Globalization;

namespace campionatoLite.model
{
    public class Squadra
    {
        public string Nome { get; set; }
        public string Luogo { get; set; }
        public int Punteggio { get; set; }

        public Squadra(string nome, string luogo)
        {
            this.Nome = nome;
            this.Luogo = luogo;
            this.Punteggio = 0;
        }
    }
}