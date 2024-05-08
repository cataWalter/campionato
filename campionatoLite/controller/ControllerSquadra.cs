using System.Collections.Generic;
using System.Linq;
using campionatoLite.model;

namespace campionatoLite.controller
{
    public class ControllerSquadra : Database
    {
        public List<Squadra> ReadSquadre()
        {
            List<Squadra> squadre = new List<Squadra>();
            foreach (Dictionary<string, object> row in Read("squadre"))
            {
                squadre.Add(new Squadra(row["nome"].ToString(), row["luogo"].ToString())
                );
            }

            squadre = CalcolaClassifica(squadre);
            return squadre.OrderByDescending(s => s.Punteggio).ToList();
        }

        private List<Squadra> CalcolaClassifica(List<Squadra> squadre)
        {
            ControllerPartita x = new ControllerPartita();
            List<Partita> partite = x.ReadPartite();
            foreach (Squadra squadra in squadre)
            {
                squadra.Punteggio = 0;
                foreach (Partita partita in partite)
                {
                    if (partita.SquadraCasa == squadra.Nome)
                    {
                        squadra.Punteggio += partita.PunteggioCasa;
                    }
                    else if (partita.SquadraOspite == squadra.Nome)
                    {
                        squadra.Punteggio += partita.PunteggioOspite;
                    }
                }
            }

            return squadre;
        }
    }
}