using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    // This class performs the AI checks for a possible winning or losing move
    class AI_Checks
    {
        private string _PlayCell;
        public string PlayCell
        {
            get { return _PlayCell; }
        }

        #region Methods
        /// <summary>
        /// Checks each row on board for a winning or losing row, based on selected team (human / AI)
        /// </summary>
        /// <param name="row"></param>
        /// <param name="board"></param>
        /// <param name="team"></param>
        /// <returns></returns>
        public bool CheckRow(int row, Board board, string team)
        {
            // Variables
            bool isWin = false;
            int countWinCells = 0;

            // Get list of column values in row
            List<string> cellLineValues = board.GetRowValues(row);

            // If the line contains one empty value
            if (cellLineValues.Contains("-"))
            {
                // Check if remaining values match selected team
                for (int index = 0; index < cellLineValues.Count; index++)
                {
                    if (cellLineValues[index] == team)
                    {
                        countWinCells++;
                    }

                    // Assign possible playable cell
                    if (cellLineValues[index] == "-")
                    {
                        _PlayCell = string.Format("{0}{1}", row, index);
                    }
                }

                // Check if win / loss
                if (countWinCells == 2)
                {
                    isWin = true;
                }
            }

            return isWin;
        }

        /// <summary>
        /// Checks each column on board for a winning or losing column, based on selected team (human / AI)
        /// </summary>
        /// <param name="column"></param>
        /// <param name="board"></param>
        /// <param name="team"></param>
        /// <returns></returns>
        public bool CheckColumn(int column, Board board, string team)
        {
            // Variables
            bool isWin = false;
            int countWinCells = 0;

            // Get list of row values in column
            List<string> cellLineValues = board.GetColumnValues(column);

            // If the line contains one empty value
            if (cellLineValues.Contains("-"))
            {
                // Check if remaining values match AI player's team
                for (int index = 0; index < cellLineValues.Count; index++)
                {
                    if (cellLineValues[index] == team)
                    {
                        countWinCells++;
                    }

                    // Assign possible playable cell
                    if (cellLineValues[index] == "-")
                    {
                        _PlayCell = string.Format("{0}{1}", index, column);
                    }
                }

                // Check if win / loss
                if (countWinCells == 2)
                {
                    isWin = true;
                }
            }

            return isWin;
        }

        /// <summary>
        /// Checks each diagonal on board for a winning or losing diagonal, based on selected team (human / AI)
        /// </summary>
        /// <param name="diagonalDirection"></param>
        /// <param name="board"></param>
        /// <param name="team"></param>
        /// <returns></returns>
        public bool CheckDiagonal(DiagonalDirection diagonalDirection, Board board, string team)
        {
            // Variables
            bool isWin = false;
            int countWinCells = 0;
            const int START_ROW_UP = 2;     // For UP diagonal
            const int FINISH_ROW_UP = 0;    // For UP diagonal

            // Get list of diagonal values
            List<string> cellLineValues = board.GetDiagonalValues(diagonalDirection);

            // If the line contains one empty value
            if (cellLineValues.Contains("-"))
            {
                switch (diagonalDirection)
                {
                    case DiagonalDirection.DOWN:
                        // Check if remaining values match selected team
                        for (int index = 0; index < cellLineValues.Count; index++)
                        {
                            if (cellLineValues[index] == team)
                            {
                                countWinCells++;
                            }

                            // Assign possible playable cell
                            if (cellLineValues[index] == "-")
                            {
                                _PlayCell = string.Format("{0}{1}", index, index);
                            }
                        }
                        break;
                    case DiagonalDirection.UP:
                        // Check if remaining values match AI player's team
                        for (int index = 0; index < cellLineValues.Count; index++)
                        {
                            if (cellLineValues[index] == team)
                            {
                                countWinCells++;
                            }

                            // Assign possible playable cell
                            if (cellLineValues[index] == "-")
                            {
                                if (index == 0)
                                {
                                    _PlayCell = string.Format("{0}{1}", START_ROW_UP, index);
                                }
                                else if (index == 1)
                                {
                                    _PlayCell = string.Format("{0}{1}", index, index);
                                }
                                else
                                {
                                    _PlayCell = string.Format("{0}{1}", FINISH_ROW_UP, index);
                                }
                            }
                        }
                        break;
                }

                // Check if win / loss
                if (countWinCells == 2)
                {
                    isWin = true;
                }
            }

            return isWin;
        }
        #endregion
    }
}
