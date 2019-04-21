using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode.Daily
{
    public class April21
    {
        public bool IsValidSudoku(char[][] board)
        {
            if (board == null || board.Length != 9 || board[0].Length != 9)
            {
                return false;
            }

            var set = new HashSet<string>();
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {

                    int currentCellNum = board[i][j];

                    // Skip .
                    if (currentCellNum == '.')
                    {
                        continue;
                    }


                    // Found duplicate in current row
                    if (!set.Add(currentCellNum + " in row " + i))
                    {
                        return false;
                    }

                    // Found duplicate in current col
                    if (!set.Add(currentCellNum + " in col " + j))
                    {
                        return false;
                    }

                    // Found duplicate in 3 by 3 grid.
                    // Example: 0,1,3 col and row index belong to 0 0 grid.
                    if (!set.Add(currentCellNum + " in grid " + i / 3 + j / 3))
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}
