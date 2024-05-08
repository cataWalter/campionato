using System.Data.SQLite;
using System.Collections.Generic;
using System.Linq;

namespace campionatoLite.controller
{
    public abstract class Database
    {
        protected static string ConnectionString;

        protected Database()
        {
            ConnectionString = $"Data Source=campionato.db;Version=3;";
        }

        public void Insert(string tableName, Dictionary<string, object> columnValues)
        {
            ExecuteNonQuery(
                $"INSERT INTO {tableName} ({string.Join(", ", columnValues.Keys)}) VALUES ({string.Join(", ", columnValues.Values.Select(v => $"\"{v}\""))})");
        }

        protected List<Dictionary<string, object>> Read(string tableName)
        {
            List<Dictionary<string, object>> results = new List<Dictionary<string, object>>();
            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                using (SQLiteCommand command = new SQLiteCommand($"SELECT * FROM {tableName}", connection))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Dictionary<string, object> row = new Dictionary<string, object>();
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                string columnName = reader.GetName(i);
                                object columnValue = reader.GetValue(i);
                                row[columnName] = columnValue;
                            }

                            results.Add(row);
                        }
                    }
                }
            }

            return results;
        }
        /*
        private void Update(string tableName, string name, Dictionary<string, object> columnValues)
        {
            ExecuteNonQuery(
                $"UPDATE {tableName} SET {columnValues.Aggregate("", (current, pair) => current + $"{pair.Key} = \"{pair.Value}\", ").TrimEnd(',', ' ')} WHERE nome = \"{name}\"");
        }

        private void Delete(string tableName, int id)
        {
            ExecuteNonQuery($"DELETE FROM {tableName} WHERE Id = {id}");
        }*/


        private void ExecuteNonQuery(string query)
        {
            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}