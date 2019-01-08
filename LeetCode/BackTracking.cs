using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace LeetCode
{
    public class BackTracking
    {
        [Test]
        public void Combine()
        {
            var n = 4;
            var k = 2;
            var r = Combine(n,k);
        }
        public IList<IList<int>> Combine(int n, int k)
        {
            var result = new List<IList<int>>();
            if (n == 0)
            {
                return result;
            }

            CombineHelper(n, k, 1, new List<int>(), result);

            return result;
        }

        private void CombineHelper(int n, int k, int start, List<int> path, List<IList<int>> result)
        {
            if (path.Count == k)
            {
                result.Add(new List<int>(path));
                return;
            }

            for (var i = start; i <= n; i++) {

                path.Add(i);
                CombineHelper(n, k, i+1, path, result);
                path.RemoveAt(path.Count - 1);
            }
        }

        public bool Exist(char[,] board, string word)
        {
            if (board == null || board.GetLength(0) == 0 || board.GetLength(1) == 0)
            {
                return false;
            }

            if (string.IsNullOrEmpty(word))
            {
                return true;
            }

            var r = board.GetLength(0);
            var c = board.GetLength(1);
            for (int rowIndex = 0; rowIndex < r; rowIndex++)
            {
                for (int colIndex = 0; colIndex < c; colIndex++)
                {
                    if (board[rowIndex,colIndex] == word[0] && WordSearchHelper(board, rowIndex, colIndex, word, 0))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private bool WordSearchHelper(char[,] board, int rowIndex, int colIndex, string word, int i)
        {
            if (i == word.Length)
            {
                return true;
            }

            // Tile that doesn't exist.
            if (rowIndex < 0 || colIndex < 0 || rowIndex >= board.GetLength(0) || colIndex >= board.GetLength(1) || board[rowIndex, colIndex] != word[i])
            {
                return false;
            }

            // Mark current tile visited.
            board[rowIndex, colIndex] = '#';

            //Do recursive search. up, down, left and right.
            var exist = WordSearchHelper(board, rowIndex, colIndex + 1, word, i + 1)
                        || WordSearchHelper(board, rowIndex, colIndex - 1, word, i + 1)
                        || WordSearchHelper(board, rowIndex + 1, colIndex, word, i + 1)
                        || WordSearchHelper(board, rowIndex - 1, colIndex, word, i + 1);

            //Unmark tile.
            board[rowIndex, colIndex] = word[i];

            return exist;
        }
    }
}
