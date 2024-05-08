using System.Collections.Generic;
using campionatoLite.model;

namespace campionatoLite.controller
{
    public class ControllerGiocatore : Database
    {
        public List<Giocatore> ReadGiocatori()
        {
            List<Giocatore> giocatori = new List<Giocatore>();
            foreach (Dictionary<string,object> row in Read("giocatori"))
            {
                Giocatore giocatore = new Giocatore(row["nome"].ToString());
                giocatori.Add(giocatore);
            }

            return giocatori;
        }
    }
}