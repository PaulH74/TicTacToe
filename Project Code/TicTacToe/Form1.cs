using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace TicTacToe
{
    public partial class Form1 : Form
    {
        // Attributes
        private Game game;
        private const string EMPTY = "";
        Button playerSelectedButton;

        // Parameter-less Constructor
        public Form1()
        {
            InitializeComponent();
            Show();
            PlayGame();
        }

        private void PlayGame()
        {
            game = new Game();

            // AI Moves first if playing as X
            WhoStartsGame();
        }

        // Human Player's Move
        public void PlayMove()
        {
            // Check empty button has been selected
            if (playerSelectedButton.Text == EMPTY)
            {
                playerSelectedButton.Text = game.HumanMove(playerSelectedButton);
                playerSelectedButton.Update();
                CheckWin();
                WhoStartsGame();
            }
            else
            {
                playerSelectedButton.ForeColor = Color.Red;
                MessageBox.Show("You must select an empty square...try again!", "INVALID MOVE!");
                playerSelectedButton.ForeColor = Color.Black;
            }
        }

        private void PlayAI()
        {
            int AIButton = 0;

            // AI's Turn
            Stopwatch sw = new Stopwatch();
            sw.Start();

            // Run stopwatch to delay move (simulate thinking)
            do
            {
            } while (sw.ElapsedMilliseconds < 500);
            sw.Stop();
            sw.Reset();

            // AI Moves
            AIButton = game.AIMove();
            SetButton(AIButton);
            CheckWin();
            WhoStartsGame();
        }

        /// <summary>
        /// AI Starts game if playing as 'X'
        /// </summary>
        private void WhoStartsGame()
        {
            if (game.PlayerTurn() == 2 && !game.humanPlayersOnly)
            {
                PlayAI();
            }
        }

        /// <summary>
        /// Checks board for a winner, updates scores and resets board for next game
        /// </summary>
        private void CheckWin()
        {
            bool gameOver = false;

            // Check if game is won
            gameOver = game.CheckWin(this);     // Passes in this object (Form1)
            if (gameOver)
            {
                UpdateScores();

                // Check if new game is to be played
                if (game.AnotherGame())
                {
                    PlayNextGame();
                }
                else
                {
                    Application.Exit();
                }
            }
        }

        /// <summary>
        /// Clears the board and starts a new game
        /// </summary>
        private void PlayNextGame()
        {
            ClearBoard();
            game.StartGame();
        }

        /// <summary>
        /// Updates the player scores on the form
        /// </summary>
        private void UpdateScores()
        {
            textBoxPlayer1.Text = game.GameScores[0].ToString();
            textBoxPlayer2.Text = game.GameScores[1].ToString();
        }

        /// <summary>
        /// Event: User-selected Button Click to play move 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_Click(object sender, EventArgs e)
        {
            playerSelectedButton = sender as Button;
            PlayMove();
        }

        /// <summary>
        /// Clears the Form's TIC TAC TOE buttons and resets the text colour to black
        /// </summary>
        private void ClearBoard()
        {
            // Default colour
            Color buttonColour = Color.Black;

            // Default button text
            const string EMPTY_BUTTON = "";

            // Reset Button Text
            button0.Text = EMPTY_BUTTON;
            button1.Text = EMPTY_BUTTON;
            button2.Text = EMPTY_BUTTON;
            button3.Text = EMPTY_BUTTON;
            button4.Text = EMPTY_BUTTON;
            button5.Text = EMPTY_BUTTON;
            button6.Text = EMPTY_BUTTON;
            button7.Text = EMPTY_BUTTON;
            button8.Text = EMPTY_BUTTON;

            // Reset Button Text Colour
            button0.ForeColor = buttonColour;
            button1.ForeColor = buttonColour;
            button2.ForeColor = buttonColour;
            button3.ForeColor = buttonColour;
            button4.ForeColor = buttonColour;
            button5.ForeColor = buttonColour;
            button6.ForeColor = buttonColour;
            button7.ForeColor = buttonColour;
            button8.ForeColor = buttonColour;
        }

        /// <summary>
        /// Changes the button text colour to red to display winning button
        /// </summary>
        /// <param name="num"></param>
        public void SetButtonWinColour(int num)
        {
            // Set Colour
            Color buttonColourChange = Color.Red;

            // Select button and change colour
            switch (num)
            {
                case 0:
                    button0.ForeColor = buttonColourChange;
                    break;
                case 1:
                    button1.ForeColor = buttonColourChange;
                    break;
                case 2:
                    button2.ForeColor = buttonColourChange;
                    break;
                case 3:
                    button3.ForeColor = buttonColourChange;
                    break;
                case 4:
                    button4.ForeColor = buttonColourChange;
                    break;
                case 5:
                    button5.ForeColor = buttonColourChange;
                    break;
                case 6:
                    button6.ForeColor = buttonColourChange;
                    break;
                case 7:
                    button7.ForeColor = buttonColourChange;
                    break;
                case 8:
                    button8.ForeColor = buttonColourChange;
                    break;
            }
        }

        /// <summary>
        /// Sets the AI player's selected button to the appropriate team symbol (X or O)
        /// </summary>
        /// <param name="num"></param>
        private void SetButton(int num)
        {
            string symbol = game.AITeam();

            switch (num)
            {
                case 0:
                    button0.Text = symbol;
                    break;
                case 1:
                    button1.Text = symbol;
                    break;
                case 2:
                    button2.Text = symbol;
                    break;
                case 3:
                    button3.Text = symbol;
                    break;
                case 4:
                    button4.Text = symbol;
                    break;
                case 5:
                    button5.Text = symbol;
                    break;
                case 6:
                    button6.Text = symbol;
                    break;
                case 7:
                    button7.Text = symbol;
                    break;
                case 8:
                    button8.Text = symbol;
                    break;
            }
        }

        private void buttonReset_Click(object sender, EventArgs e)
        {
            ClearBoard();
            game.StartGame();
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}