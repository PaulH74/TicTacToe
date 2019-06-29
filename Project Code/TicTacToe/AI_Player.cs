using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace TicTacToe
{
    class AI_Player : Player
    {
        // Private Attributes
        private Board _CurrentBoardCopy;
        private List<string> _PlayableCells;
        private string _AITeam;
        private string _HumanTeam;
        private AI_Checks _AIChecks;

        // Custom Constructor
        public AI_Player() : base()
        {
            _AIChecks = new AI_Checks();
        }

        #region Methods
        /// <summary>
        /// Stores player 1 and player 2 (AI) teams (X or O) for reference
        /// </summary>
        /// <param name="player1Team"></param>
        /// <param name="player2Team"></param>
        public void StorePlayersTeams(string player1Team, string player2Team)
        {
            _HumanTeam = player1Team;
            _AITeam = player2Team;
        }

        public override string Move(Board board)
        {
            // Create a copy of the current board
            // To be used for game move analysis (possible tree data structure)
            CopyBoard(board);
            //board.ShowBoard();

            // Select playable cell and return value
            return SelectPlayableCell(board);
        }

        /// <summary>
        /// Looks up all empty cells in the current board configuration that are playable
        /// </summary>
        /// <param name="board"></param>
        private void CheckEmptyCells(Board board)
        {
            // Check board for empty cells
            _PlayableCells = board.FindEmptyCells();
        }

        private void CopyBoard(Board board)
        {
            _CurrentBoardCopy = new Board(board);
        }

        /// <summary>
        /// Checks for playable cells (empty cells) on current board and tries to find a winning move, prevent a losing move, or else play a random move
        /// </summary>
        /// <param name="board"></param>
        /// <returns></returns>
        private string SelectPlayableCell(Board board)
        {
            // Variables
            Random randomNum = new Random();
            int num = 0;
            string cellPlayed = "";
            bool winFound = false;
            
            // Check board for empty cells
            CheckEmptyCells(board);

            // Analyse Board for possible winning row, column or diagonal
            if (_PlayableCells.Count != 0)
            {
                // Check possible WINS
                // Check ROWS for possible win
                for (int row = 0; row < board.ROWS; row++)
                {
                    if (_AIChecks.CheckRow(row, board, _AITeam))
                    {
                        cellPlayed = _AIChecks.PlayCell;
                        winFound = true;
                        break;
                    }
                }

                if (!winFound)
                // Check COLUMNS for possible win
                {
                    for (int column = 0; column < board.COLUMNS; column++)
                    {
                        if (_AIChecks.CheckColumn(column, board, _AITeam))
                        {
                            cellPlayed = _AIChecks.PlayCell;
                            winFound = true;
                            break;
                        }
                    }
                }

                if (!winFound)
                // Check DIAGONALS for possible win
                {
                    for (int column = 0; column < board.COLUMNS; column++)
                    {
                        if (_AIChecks.CheckDiagonal(DiagonalDirection.UP, board, _AITeam))
                        {
                            cellPlayed = _AIChecks.PlayCell;
                            winFound = true;
                            break;
                        }
                        if (!winFound)
                        {
                            if (_AIChecks.CheckDiagonal(DiagonalDirection.DOWN, board, _AITeam))
                            {
                                cellPlayed = _AIChecks.PlayCell;
                                winFound = true;
                                break;
                            }
                        }
                    }
                }

                // Check possible losses
                if (!winFound)
                {
                    // Check ROWS for possible loss
                    for (int row = 0; row < board.ROWS; row++)
                    {
                        if (_AIChecks.CheckRow(row, board, _HumanTeam))
                        {
                            cellPlayed = _AIChecks.PlayCell;
                            winFound = true;
                            break;
                        }
                    }
                }

                if (!winFound)
                // Check COLUMNS for possible loss
                {
                    for (int column = 0; column < board.COLUMNS; column++)
                    {
                        if (_AIChecks.CheckColumn(column, board, _HumanTeam))
                        {
                            cellPlayed = _AIChecks.PlayCell;
                            winFound = true;
                            break;
                        }
                    }
                }

                if (!winFound)
                // Check DIAGONALS for possible loss
                {
                    for (int column = 0; column < board.COLUMNS; column++)
                    {
                        if (_AIChecks.CheckDiagonal(DiagonalDirection.UP, board, _HumanTeam))
                        {
                            cellPlayed = _AIChecks.PlayCell;
                            winFound = true;
                            break;
                        }
                        if (!winFound)
                        {
                            if (_AIChecks.CheckDiagonal(DiagonalDirection.DOWN, board, _HumanTeam))
                            {
                                cellPlayed = _AIChecks.PlayCell;
                                winFound = true;
                                break;
                            }
                        }
                    }
                }


                // If no win / loss found, play random cell
                if (!winFound)
                {
                    // Randomly pick an empty cell and play that cell
                    if (_PlayableCells.Count != 0)
                    {
                        num = randomNum.Next(0, _PlayableCells.Count);
                        cellPlayed = _PlayableCells[num];
                    }
                }

            }

            return cellPlayed;
        }
        #endregion
    }
}
