using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Threading.Tasks;
using campionatoLite.model;

namespace campionatoLite.controller
{
    public class ControllerPartita : Database
    {
        public List<Partita> ReadPartite()
        {
            List<Partita> squadre = new List<Partita>();
            foreach (Dictionary<string, object> row in Read("partite"))
            {
                squadre.Add(new Partita(
                    row["squadraCasa"].ToString(),
                    row["squadraOspite"].ToString(),
                    row["data"].ToString(),
                    Convert.ToInt32(row["giornata"]),
                    Convert.ToInt32(row["punteggioCasa"]),
                    Convert.ToInt32(row["punteggioOspite"])
                ));
            }

            return squadre;
        }

        private int TrovaMinimaGiornataNonSimulata()
        {
            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                using (SQLiteCommand command = new SQLiteCommand(
                           "SELECT MIN(giornata) FROM partite WHERE punteggioCasa = 0 AND punteggioOspite = 0;",
                           connection))
                {
                    object result = command.ExecuteScalar();
                    if (result is DBNull) return -1;
                    return Convert.ToInt32(result);
                }
            }
        }

        private int ContaPartiteDaSimulare(int giornataMinima)
        {
            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                using (SQLiteCommand command = new SQLiteCommand(
                           $"SELECT COUNT(*) FROM partite WHERE giornata = {giornataMinima};",
                           connection))
                {
                    return Convert.ToInt32(command.ExecuteScalar());
                }
            }
        }

        public void AttivaSimulazione()
        {
            int giornataMinima = TrovaMinimaGiornataNonSimulata();
            if (giornataMinima != -1)
            {
                int numeroPartiteDaSimulare = ContaPartiteDaSimulare(giornataMinima);
                Task[] tasks = new Task[numeroPartiteDaSimulare];
                Random random = new Random();
                for (int i = 0; i < numeroPartiteDaSimulare; i++)
                {
                    tasks[i] = Task.Run(() => AggiornaPunteggi(giornataMinima, random));
                }

                Task.WaitAll(tasks);
            }
        }

        private void AggiornaPunteggi(int giornata, Random random)
        {
            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                using (SQLiteCommand command = new SQLiteCommand(
                           $"UPDATE partite " +
                           $"SET punteggioCasa = {random.Next(0, 101)}, " +
                           $"punteggioOspite = {random.Next(0, 101)} " +
                           $"WHERE giornata = {giornata} " +
                           $"AND punteggioCasa = 0 " +
                           $"AND punteggioOspite = 0 " +
                           $"LIMIT 1;",
                           connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }

        public void AzzeraPunteggi()
        {
            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                using (SQLiteCommand command = new SQLiteCommand(
                           $"UPDATE partite SET punteggioCasa = 0, punteggioOspite = 0;",
                           connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}