using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace LeetCode.Design
{
    public class TicTacToeTest
    {
        [Test]
        public void Test()
        {
            var a = new[,] { {1, 0, 2}, {2, 1, 1}};

            var t = new TicTacToe(3);
            for (var i = 0; i < a.Length; i++)
            {
                t.Move(a[i, 0], a[i, 1], a[i, 2]);
            }
        }
    }

    public class TicTacToe
    {
        private int[] rows;
        private int[] cols;
        private int diagonal;
        private int antiDiagonal;

        /** Initialize your data structure here. */
        public TicTacToe(int n)
        {
            rows = new int[n];
            cols = new int[n];
        }

        /** Player {player} makes a move at ({row}, {col}).
            @param row The row of the board.
            @param col The column of the board.
            @param player The player, can be either 1 or 2.
            @return The current winning condition, can be either:
                    0: No one wins.
                    1: Player 1 wins.
                    2: Player 2 wins. */
        public int Move(int row, int col, int player)
        {
            int toAdd = player == 1 ? 1 : -1;
            rows[row] += toAdd;
            cols[col] += toAdd;
            if (row == col)
            {
                diagonal += toAdd;
            }

            if (col == (cols.Length - row - 1))
            {
                antiDiagonal += toAdd;
            }

            int size = rows.Length;
            if (Math.Abs(rows[row]) == size ||
                Math.Abs(cols[col]) == size ||
                Math.Abs(diagonal) == size ||
                Math.Abs(antiDiagonal) == size)
            {
                return player;
            }

            return 0;
        }
    }
}
