using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TicTacToe
{
    class Game
    {
        // Attributes and Read-Only Properties
        private int[] _GameScores;
        public int[] GameScores
        {
            get { return _GameScores; }
        }
        private int _GameMoveCount = 0;
        private const string EMPTY = "";
        private const int NUMBER_OF_PLAYERS = 2;
        private Player player1;
        private Player player2;
        private Board gameBoard;
        public bool humanPlayersOnly;
           
        // Custom Constructor
        public Game()
        {
            gameBoard = new Board();
            _GameScores = new int[NUMBER_OF_PLAYERS]; // Index 0 - player 1, index 1 - player 2
            ChooseOpponentPlayer();

            // Generate appropriate player objects
            if (humanPlayersOnly)
            {
                player1 = new Player();
                player2 = new Player();
            }
            else
            {
                player1 = new Player();
                player2 = new AI_Player();
            }
            StartGame();
        }

        #region Methods
        /// <summary>
        /// Start a new game
        /// </summary>
        public void StartGame()
        {
            _GameMoveCount = 0;
            ChooseTeam();
            ResetGame();
        }            
        
        /// <summary>
        /// Alternates each player's turn
        /// </summary>
        /// <returns></returns>
        public int PlayerTurn()
        {
            int player = 0;

            if (player1.Turn)
                player = 1;
            else
                player = 2;

            return player;
        }

        /// <summary>
        /// Checks and returns the player with the current turn
        /// </summary>
        /// <returns></returns>
        private Player CurrentTurn()
        {
            Player player;

            // Check player turn
            if (player1.Turn)
            {
                player = player1;
            }
            else
            {
                player = player2;
            }

            return player;
        }

        /// <summary>
        /// Gets and returns AI player's team (AI is always Player 2)
        /// </summary>
        /// <returns></returns>
        public string AITeam()
        {
            return player2.Team;
        }

        /// <summary>
        /// User choice of Human vs Human or Human vs Computer
        /// </summary>
        /// <returns></returns>
        private void ChooseOpponentPlayer()
        {
            const string HEADING = "NUMBER OF PLAYERS...";
 
            // Get User Choice
            DialogResult playerChoice = MessageBox.Show("Do you wish to play against computer?", HEADING, MessageBoxButtons.YesNo);
            if (playerChoice == DialogResult.Yes)
            {
                humanPlayersOnly = false;
            }
            else
            {
                humanPlayersOnly = true;
            }
        }

        /// <summary>
        /// Presents team choice to user
        /// </summary>
        public void ChooseTeam()
        {
            const string HEADING = "NEW GAME...";

            // Get User Choice
            DialogResult start_choice = MessageBox.Show("Player 1 - Do you wish to play as 'X'?", HEADING, MessageBoxButtons.YesNo);
            if (start_choice == DialogResult.Yes)
            {
                player1.Team = Team.X.ToString();
                player2.Team = Team.O.ToString();
                player1.Turn = true;
                player2.Turn = false;
            }
            else
            {
                player1.Team = Team.O.ToString();
                player2.Team = Team.X.ToString();
                player1.Turn = false;
                player2.Turn = true;
            }

            // Display player teams and first turn
            if (player1.Team == Team.X.ToString())
            {
                MessageBox.Show(string.Format("Player 1 is {0}\n\nPlayer 2 is {1}\n\nPlayer 1 (X) goes first", player1.Team, player2.Team), "PLAYER TEAMS");
            }
            else
            {
                MessageBox.Show(string.Format("Player 1 is {0}\n\nPlayer 2 is {1}\n\nPlayer 2 (X) goes first", player1.Team, player2.Team), "PLAYER TEAMS");
            }

            // Store Teams for AI reference (if AI is playing)
            if (player2 is AI_Player)
            {
                (player2 as AI_Player).StorePlayersTeams(player1.Team, player2.Team);
            }
        }

        /// <summary>
        /// Assigns the human player move to the appropriate button / array element, switches player turns, and increments turn count
        /// </summary>
        /// <param name="button"></param>
        /// <returns></returns>
        public string HumanMove(Button button)
        {
            string move = "";
            Player player = CurrentTurn();

            // Get player's team (X or O)
            move = player.Team;

            // Switch player turns
            if (player == player1)
            {
                player1.Turn = false;
                player2.Turn = true;
            }
            else
            {
                player1.Turn = true;
                player2.Turn = false;
            }

            // Apply move and update turn count
            AssignValue(button.Name, move);
            _GameMoveCount++;

            return move;
        }

        /// <summary>
        /// Runs the AI algorithm to play a move and sets the appropriate button (cell) that has been selected by the AI
        /// for the move
        /// </summary>
        /// <returns></returns>
        public int AIMove()
        {
            int buttonNum = 0;
            string move = "";
            string cell = "";
            string buttonName = "";

            // Set player turn
            move = player2.Team;
            player1.Turn = true;
            player2.Turn = false;

            // Find and Select playable cell and set associated Form button
            cell = player2.Move(gameBoard);
            switch (cell)
            {
                case "00":
                    buttonNum = 0;
                    buttonName = "button0";
                    break;
                case "01":
                    buttonNum = 1;
                    buttonName = "button1";
                    break;
                case "02":
                    buttonNum = 2;
                    buttonName = "button2";
                    break;
                case "10":
                    buttonNum = 3;
                    buttonName = "button3";
                    break;
                case "11":
                    buttonNum = 4;
                    buttonName = "button4";
                    break;
                case "12":
                    buttonNum = 5;
                    buttonName = "button5";
                    break;
                case "20":
                    buttonNum = 6;
                    buttonName = "button6";
                    break;
                case "21":
                    buttonNum = 7;
                    buttonName = "button7";
                    break;
                case "22":
                    buttonNum = 8;
                    buttonName = "button8";
                    break;
            }

            // Assign button and record move
            AssignValue(buttonName, move);
            _GameMoveCount++;

            return buttonNum;
        }

        /// <summary>
        /// Updates the game board and game info with the latest move
        /// </summary>
        /// <param name="buttonValue"></param>
        /// <param name="move"></param>
        private void AssignValue(string buttonValue, string move)
        {
            switch (buttonValue)
            {
                case "button0":
                    gameBoard.UpdateBoard(move, 0, 0);      // Update Board (internal array[,])
                    break;
                case "button1":
                    gameBoard.UpdateBoard(move, 0, 1);
                    break;
                case "button2":
                    gameBoard.UpdateBoard(move, 0, 2);
                    break;
                case "button3":
                    gameBoard.UpdateBoard(move, 1, 0);
                    break;
                case "button4":
                    gameBoard.UpdateBoard(move, 1, 1);
                    break;
                case "button5":
                    gameBoard.UpdateBoard(move, 1, 2);
                    break;
                case "button6":
                    gameBoard.UpdateBoard(move, 2, 0);
                    break;
                case "button7":
                    gameBoard.UpdateBoard(move, 2, 1);
                    break;
                case "button8":
                    gameBoard.UpdateBoard(move, 2, 2);
                    break;
            }
        }

        // Method to check if game won and update player scores
        public bool CheckWin(Form1 form)
        {
            string value = "";
            const string GAME_OVER = "GAME OVER";
            bool gameOver = false;

            // Check game board for win
            value = gameBoard.CheckBoard(form);

            // Determine Winning Player
            if (value != "")
            {
                if (player1.Team == value)
                {
                    player1.AddToScore();
                    _GameScores[0] = player1.Score;
                    gameOver = true;
                    MessageBox.Show(string.Format("Player 1 ({0}) is the winner", player1.Team), GAME_OVER);
                }
                else if (player2.Team == value)
                {
                    player2.AddToScore();
                    _GameScores[1] = player2.Score;
                    gameOver = true;
                    MessageBox.Show(string.Format("Player 2 ({0}) is the winner", player2.Team), GAME_OVER);
                }
            }
            else if (_GameMoveCount == 9)
            {
                MessageBox.Show("The game is a DRAW!!!", GAME_OVER);
                gameOver = true;
            }

            return gameOver;
        }

        /// <summary>
        /// Asks player for another game
        /// </summary>
        /// <returns></returns>
        public bool AnotherGame()
        {
            bool playAgain = true;

            const string HEADING = "ANOTHER GAME..?";

            DialogResult playAgain_choice = MessageBox.Show("Do you wish to play again?", HEADING, MessageBoxButtons.YesNo);
            if (playAgain_choice == DialogResult.No)
            {
                playAgain = false;
            }

            return playAgain;
        }

        /// <summary>
        /// Resets the internal game board array
        /// </summary>
        private void ResetGame()
        {
            gameBoard.ResetBoard();
        }
        #endregion
    }
}

