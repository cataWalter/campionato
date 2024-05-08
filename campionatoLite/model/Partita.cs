using System;
using System.Globalization;

namespace campionatoLite.model
{
    public class Partita
    {
        public string SquadraCasa { get; set; }
        public string SquadraOspite { get; set; }
        public DateTime Data { get; set; }
        public int Giornata { get; set; }
        public int PunteggioCasa { get; set; }
        public int PunteggioOspite { get; set; }


        public Partita(
            string squadraCasa, string squadraOspite, string data, int giornata, int punteggioCasa,
            int punteggioOspite)
        {
            this.SquadraCasa = squadraCasa;
            this.SquadraOspite = squadraOspite;
            this.Data = DateTime.ParseExact(data, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            this.Giornata = giornata;
            this.PunteggioCasa = punteggioCasa;
            this.PunteggioOspite = punteggioOspite;
        }
    }
}