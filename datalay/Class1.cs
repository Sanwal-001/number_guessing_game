using System;
using MySql.Data.MySqlClient;

namespace datalay
{
    public class GameHistoryDataAccess
    {
        private string connectionString = "Server=localhost;Database=GuessingGameDB;User=root;Password=;";

        public void SaveGameHistory(string playerName, int guessedNumber, int attempts)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "INSERT INTO GameHistory (PlayerName, GuessedNumber, Attempts, GameDate) " +
                                   "VALUES (@PlayerName, @GuessedNumber, @Attempts, @GameDate)";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@PlayerName", playerName);
                        cmd.Parameters.AddWithValue("@GuessedNumber", guessedNumber);
                        cmd.Parameters.AddWithValue("@Attempts", attempts);
                        cmd.Parameters.AddWithValue("@GameDate", DateTime.Now);
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error saving game history: " + ex.Message);
                }
            }
        }
    }

}
