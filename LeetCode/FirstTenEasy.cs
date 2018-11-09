using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace LeetCode
{
    public class FirstTenEasy
    {
        public int NumJewelsInStones(string J, string S)
        {
            var jewelsDict = J.ToCharArray();
            var count = 0;
            foreach (var c in S)
            {
                if (jewelsDict.Contains(c))
                {
                    count++;
                }
            }

            return count;
        }

        [Test]
        public void MaxIncreaseKeepingSkyline()
        {
            var a = new int[4][];
            a[0] = new[] { 3, 0, 8, 4 };
            a[1] = new[] { 2, 4, 5, 7 };
            a[2] = new[] { 9, 2, 6, 3 };
            a[3] = new[] { 0, 3, 1, 0 };
            var t = MaxIncreaseKeepingSkyline(a);
        }

        public int MaxIncreaseKeepingSkyline(int[][] grid)
        {
            if (grid?[0] == null)
            {
                return 0;
            }

            var rowMax = new int[grid[0].Length];
            var colMax = new int[grid.Length];

            for (var i = 0; i < grid.Length; i++)
            {
                rowMax[i] = grid[i].Max();
                for (int j = 0; j < grid[i].Length; j++)
                {
                    colMax[i] = Math.Max(colMax[i], grid[j][i]);
                }
            }

            var sum = 0;
            for (var i = 0; i < grid.Length; i++)
            {
                for (int j = 0; j < grid[i].Length; j++)
                {
                    var maxHeight = Math.Min(rowMax[i], colMax[j]);
                    sum += (maxHeight - grid[i][j]);
                }
            }

            return sum;
        }
    }
}
