using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipBot
{
    internal class Coordination
    {
        private int row;
        private int col;

        public int Row { get => row; set => row = value; }
        public int Col { get => col; set => col = value; }

        public Coordination(int row, int col)
        {
            Row = row;
            Col = col;
        }
    }
}
