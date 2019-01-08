using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode
{
    public class ArrayProblem
    {
        public IList<int> SpiralOrder(int[,] matrix)
        {
            var result = new List<int>();
            if (matrix == null || matrix.GetLength(0) < 1 || matrix.GetLength(1) < 1)
            {
                return result;
            }

            var left = 0;
            // Width
            var right = matrix.GetLength(1);
            var top = 0;
            // Height
            var bot = matrix.GetLength(0);

            while (left < right && top < bot)
            {
                // left to right
                for (var i = left; i < right; i++)
                {
                    result.Add(matrix[top, i]);
                }

                // top to bot
                for (var i = top; i < bot; i++)
                {
                    result.Add(matrix[i, right]);
                }

                // right to left
                for (var i = right; i > left;i--) {
                    result.Add(matrix[bot,i]);
                }

                // bot to top
                for (var i = bot; i > top; i--)
                {
                    result.Add(matrix[i, left]);
                }

                left++;
                right--;
                top++;
                bot--;
            }

            if (left == right)
            {
                for (var i = top; i < bot; i++) {
                    result.Add(matrix[i, left]);
                }
            }else if (top == bot)
            {
                for (var i = left; i < right;i++) {
                    result.Add(matrix[top,i]);
                }
            }

            return result;
        }
    }
}
