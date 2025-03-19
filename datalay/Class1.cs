using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

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

        // Method to fetch game history
        public List<GameHistory> GetGameHistory()
        {
            List<GameHistory> gameHistoryList = new List<GameHistory>();

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    string query = "SELECT PlayerName, GuessedNumber, Attempts, GameDate FROM GameHistory";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                GameHistory history = new GameHistory
                                {
                                    PlayerName = reader.GetString("PlayerName"),
                                    GuessedNumber = reader.GetInt32("GuessedNumber"),
                                    Attempts = reader.GetInt32("Attempts"),
                                    GameDate = reader.GetDateTime("GameDate")
                                };
                                gameHistoryList.Add(history);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("Error fetching game history: " + ex.Message);
                }
            }

            return gameHistoryList;
        }
    }

    // Class representing the GameHistory data model
    public class GameHistory
    {
        public string PlayerName { get; set; }
        public int GuessedNumber { get; set; }
        public int Attempts { get; set; }
        public DateTime GameDate { get; set; }
    }
}
