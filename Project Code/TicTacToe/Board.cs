using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TicTacToe
{
    class Board
    {
        // Private and Read-Only Attributes
        private string[,] _GRID;
        public string[,] GRID
        {
            get { return _GRID; }
        }

        private const int _ROWS = 3;
        public int ROWS
        {
            get { return _ROWS; }
        }

        private const int _COLUMNS = 3;
        public int COLUMNS
        {
            get { return _COLUMNS; }
        }

        private const string EMPTY_CELL = "-";

        #region Constructors
        // Custom Constructor
        public Board()
        {
            _GRID = new string[_ROWS, _COLUMNS];
            ResetBoard();
        }

        // Copy Constructor
        public Board(Board source)
        {
            _GRID = new string[_ROWS, _COLUMNS];

            // Perform Deep Copy of Board Grid (to avoid accessing same reference)
            CopyBoard(source);
        }
        #endregion

        #region Methods
        /// <summary>
        /// Method to return cell value (string) in grid based on designated row and column values
        /// </summary>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <returns></returns>
        public string GetCellValue(int row, int column)
        {
            return _GRID[row, column];
        }

        /// <summary>
        /// Resets the TIC TAC TOE board
        /// </summary>
        public void ResetBoard()
        {
            // Travers rows + columns and set each element to default
            for (int row = 0; row < _ROWS; row++)
            {
                for (int column = 0; column < _COLUMNS; column++)
                {
                    _GRID[row, column] = EMPTY_CELL;
                }
            }
        }

        /// <summary>
        /// Updates an element in the game board array with an X or an O
        /// </summary>
        /// <param name="gameCharacter"></param>
        /// <param name="row"></param>
        /// <param name="column"></param>
        public void UpdateBoard(string gameCharacter, int row, int column)
        {
            _GRID[row, column] = gameCharacter;
        }

        // To be deleted 
        public void ShowBoard()
        {
            StringBuilder sb = new StringBuilder();

            for (int row = 0; row < _ROWS; row++)
            {
                for (int column = 0; column < _COLUMNS; column++)
                {
                    sb.Append(_GRID[row, column]);
                }
                sb.AppendLine();
            }

            MessageBox.Show(sb.ToString());
        }

        /// <summary>
        /// Performs a deep copy of the source board when the copy constructor is invoked
        /// </summary>
        /// <param name="source"></param>
        private void CopyBoard(Board source)
        {
            // Traverse array and copy values from source board to destination board
            for (int ROW = 0; ROW < _ROWS; ROW++)
            {
                for (int COLUMN = 0; COLUMN < _COLUMNS; COLUMN++)
                {
                    _GRID[ROW, COLUMN] = source._GRID[ROW, COLUMN];
                }
            }
        }

        /// <summary>
        /// For AI Player: locates all empty elements in array that can be a possible move
        /// </summary>
        /// <returns></returns>
        public List<string> FindEmptyCells()
        {
            List<string> emptyCells = new List<string>();

            for (int row = 0; row < _ROWS; row++)
            {
                for (int column = 0; column < _COLUMNS; column++)
                {
                    if (_GRID[row, column] == EMPTY_CELL)
                    {
                        emptyCells.Add(string.Format("{0}{1}", row, column));
                    }
                }
            }

            return emptyCells;
        }

        /// <summary>
        /// Checks the board for a win by checking rows, columns and diagonals
        /// </summary>
        public string CheckBoard(Form1 form)
        {
            string teamWinner = "";
            const string NO_WINNER = "";

            // Check Rows for win
            teamWinner = CheckBoardRows(form);

            // Check Columns for win (if no winning row)
            if (teamWinner == NO_WINNER)
            {
                teamWinner = CheckBoardColumns(form);
            }

            // Check Diagonals for win (if no winning row or column)
            if (teamWinner == NO_WINNER)
            {
                teamWinner = CheckBoardDiagonals(form);
            }

            return teamWinner;
        }

        /// <summary>
        /// Checks each row on the board to see if game won and changes winning buttons on form
        /// </summary>
        /// <returns></returns>
        private string CheckBoardRows(Form1 form)
        {
            string teamWinner = "";
            int winningRow = -1;

            // Traverse rows and check if matching but not empty
            for (int row = 0; row < _ROWS; row++)
            {
                if (_GRID[row, 0] != EMPTY_CELL && _GRID[row, 0] == _GRID[row, 1] && _GRID[row, 1] == _GRID[row, 2])
                {
                    teamWinner = _GRID[row, 0];
                    winningRow = row;
                    break;
                }
            }

            // Change form buttons for winning row
            switch (winningRow)
            {
                case 0:
                    form.SetButtonWinColour(0);
                    form.SetButtonWinColour(1);
                    form.SetButtonWinColour(2);
                    break;
                case 1:
                    form.SetButtonWinColour(3);
                    form.SetButtonWinColour(4);
                    form.SetButtonWinColour(5);
                    break;
                case 2:
                    form.SetButtonWinColour(6);
                    form.SetButtonWinColour(7);
                    form.SetButtonWinColour(8);
                    break;
            }

            return teamWinner;
        }

        /// <summary>
        /// Checks each column on the board to see if game won
        /// </summary>
        /// <returns></returns>
        private string CheckBoardColumns(Form1 form)
        {
            string teamWinner = "";
            int winningColumn = -1;

            // Traverse columns and check if matching but not empty
            for (int column = 0; column < _COLUMNS; column++)
            {
                if (_GRID[0, column] == _GRID[1, column] && _GRID[1, column] == _GRID[2, column] && _GRID[0, column] != EMPTY_CELL)
                {
                    teamWinner = _GRID[0, column];
                    winningColumn = column;
                    break;
                }
            }

            // Change form buttons for winning column
            switch (winningColumn)
            {
                case 0:
                    form.SetButtonWinColour(0);
                    form.SetButtonWinColour(3);
                    form.SetButtonWinColour(6);
                    break;
                case 1:
                    form.SetButtonWinColour(1);
                    form.SetButtonWinColour(4);
                    form.SetButtonWinColour(7);
                    break;
                case 2:
                    form.SetButtonWinColour(2);
                    form.SetButtonWinColour(5);
                    form.SetButtonWinColour(8);
                    break;
            }

            return teamWinner;
        }

        /// <summary>
        /// Checks each diagonal on the board to see if game won
        /// </summary>
        /// <returns></returns>
        private string CheckBoardDiagonals(Form1 form)
        {
            string teamWinner = "";
            string winningDiagonal = "";
            const string DIAGONAL_UP = "UP";        // From left to right
            const string DIAGONAL_DOWN = "DOWN";    // From left to right

            // Check diagonals if matching but not empty
            if (_GRID[0, 0] == _GRID[1, 1] && _GRID[1, 1] == _GRID[2, 2] && _GRID[0, 0] != EMPTY_CELL)
            {
                teamWinner = _GRID[0, 0];
                winningDiagonal = DIAGONAL_DOWN;
            }

            if (_GRID[2, 0] == _GRID[1, 1] && _GRID[1, 1] == _GRID[0, 2] && _GRID[2, 0] != EMPTY_CELL)
            {
                teamWinner = _GRID[2, 0];
                winningDiagonal = DIAGONAL_UP;
            }

            // Change form buttons for winning column
            switch (winningDiagonal)
            {
                case "UP":
                    form.SetButtonWinColour(6);
                    form.SetButtonWinColour(4);
                    form.SetButtonWinColour(2);
                    break;
                case "DOWN":
                    form.SetButtonWinColour(0);
                    form.SetButtonWinColour(4);
                    form.SetButtonWinColour(8);
                    break;
            }

            return teamWinner;
        }

        /// <summary>
        /// Returns a List of column values in a designated row
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public List<string> GetRowValues(int row)
        {
            List<string> rowValues = new List<string>();

            // Traverse row and add column values to list
            for (int column = 0; column < _COLUMNS; column++)
            {
                rowValues.Add(_GRID[row, column]);
            }

            return rowValues;
        }

        /// <summary>
        /// Returns a List of row values in a designated column
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public List<string> GetColumnValues(int column)
        {
            List<string> columnValues = new List<string>();

            // Traverse row and add column values to list
            for (int row = 0; row < _ROWS; row++)
            {
                columnValues.Add(_GRID[row, column]);
            }

            return columnValues;
        }

        /// <summary>
        /// Returns a List of diagonal cell values in a designated diagonal
        /// </summary>
        /// <param name="row"></param>
        /// <returns></returns>
        public List<string> GetDiagonalValues(DiagonalDirection diagonal)
        {
            List<string> diagonalValues = new List<string>();
            int count = 0;
            int row = 0;
            int column = 0;
            const int MAX_COUNT = 3;

            // Traverse diagonal and add values to list
            switch(diagonal)
            {
                case DiagonalDirection.UP:
                    row = 2;
                    do
                    {
                        diagonalValues.Add(_GRID[row, column]);
                        row--;
                        column++;
                        count++;
                    } while (count < MAX_COUNT);
                    break;
                case DiagonalDirection.DOWN:
                    row = 0;
                    do
                    {
                        diagonalValues.Add(_GRID[row, column]);
                        row++;
                        column++;
                        count++;
                    } while (count < MAX_COUNT);
                    break;
            }

            return diagonalValues;
        }
        #endregion
    }
}
