using System;
using System.Windows.Forms;
using LogicLayer;

namespace Assignment3
{
    public partial class Form1 : Form
    {
        // Declare a class-level instance of GameLogic
        private GameLogic gameLogic;

        public Form1()
        {
            InitializeComponent();
            gameLogic = new GameLogic();  // Initialize GameLogic only once
            gameLogic.StartNewGame();
        }

        private void btnGuess_Click(object sender, EventArgs e)
        {
            int userGuess;

            // Check if the user input is a valid integer
            if (int.TryParse(txtGuess.Text, out userGuess))
            {
                // Call the MakeGuess method from LogicLayer and get the result
                string result = gameLogic.MakeGuess(userGuess);

                // Display the result on the UI
                lblResult.Text = result;
                lblAttempts.Text = $"Attempts {gameLogic.Attempts}";
            }
            else
            {
                MessageBox.Show("Please enter a valid number!");
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            // Reset the game when the reset button is clicked
            gameLogic.StartNewGame(); // Use the existing instance of GameLogic
            lblResult.Text = "";
            lblAttempts.Text = "Attempts: 0";
        }
    }

}
