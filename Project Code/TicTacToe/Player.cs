using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TicTacToe
{
    class Player
    {
        #region Read-only Attributes
        private int _Score;
        public int Score
        {
            get { return _Score; }
        }

        private bool _Turn;
        public bool Turn
        {
            get { return _Turn; }
            set { _Turn = value; }
        }

        private string _Team;       // X or O
        public string Team
        {
            get { return _Team; }
            set { _Team = value; }
        }
        #endregion

        #region Methods
        public void AddToScore()
        {
            _Score++;
        }

        public virtual string Move(Board board)
        {
            return null;
        }
        #endregion
    }
}
