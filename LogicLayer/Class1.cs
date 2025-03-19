using System;
using datalay;
using System.Collections.Generic;

namespace LogicLayer
{
    public class GameLogic
    {
        private Random random = new Random(); // Random initialized once at class level
        public int TargetNumber { get; private set; }
        public int Attempts { get; private set; }

        // Instance of the data access layer to save game data
        private GameHistoryDataAccess dataAccess;

        public GameLogic()
        {
            dataAccess = new GameHistoryDataAccess(); // Initialize DataLayer here
        }

        // This method starts a new game by generating a new target number and resetting attempts
        public void StartNewGame()
        {
            TargetNumber = random.Next(1, 101); // Generate a new random number between 1 and 100
            Attempts = 0; // Reset attempts to 0
        }

        // This method handles the player's guess and returns the result
        public string MakeGuess(int guessedNumber)
        {
            Attempts++; // Increment the number of attempts with every guess

            string result = string.Empty;

            if (guessedNumber == TargetNumber)
            {
                result = "Correct!";
                // Save the game history when the guess is correct
                dataAccess.SaveGameHistory("Sanwal", guessedNumber, Attempts);
            }
            else if (guessedNumber < TargetNumber)
            {
                result = "Too low! Try again.";
            }
            else
            {
                result = "Too high! Try again.";
            }

            return result;
        }

        // Method to fetch the game history data
        public List<GameHistory> GetGameHistory()
        {
            return dataAccess.GetGameHistory(); // Call the data access layer to fetch the game history
        }
    }
}
