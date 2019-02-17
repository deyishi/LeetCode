using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace LeetCode
{
    public class BackTrackingSet
    {
        [Test]
        public void Test()
        {
            var s = new char[9][];

            s[0] = new[] { '5', '3', '.', '.', '7', '.', '.', '.', '.' };
            s[1] = new[] { '6', '.', '.', '1', '9', '5', '.', '.', '.' };
            s[2] = new[] { '.', '9', '8', '.', '.', '.', '.', '6', '.' };
            s[3] = new[] { '8', '.', '.', '.', '6', '.', '.', '.', '3' };
            s[4] = new[] { '4', '.', '.', '8', '.', '3', '.', '.', '1' };
            s[5] = new[] { '7', '.', '.', '.', '2', '.', '.', '.', '6' };
            s[6] = new[] { '.', '6', '.', '.', '.', '.', '2', '8', '.' };
            s[7] = new[] { '.', '.', '.', '4', '1', '9', '.', '.', '5' };
            s[8] = new[] { '.', '.', '.', '.', '8', '.', '.', '7', '9' };


            var a = "120122436";
            var r = IsAdditiveNumber(a);
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

        public void SolveSudoku(char[][] board)
        {
            if (board == null || board.Length != 9 || board[0].Length != 9)
            {
                return;
            }

            SolveSudokuHelper(board, 0, 0);
        }

        //Validate board.
        //Recursion Helper that takes board, row and col. Validation function check all rows, cols and 3 by 3 board.
        //Exit when board is all finished or cannot find a solution for the empty spot of given row and col.
        //Start from current row and col search the board for an empty spot.
        //Loop through 1 to 9 for the empty spot:
        //1. Find a valid num to put in that spot
        //2. Make a recursive call using the row and col after that filled spot (This recursive call will check the rest of empty spot with current spot being filled).
        //3. If recursive call return false, reset the current spot and move on to next possible num for this spot.
        //4. Loop finish without finding a valid solution. 
        public bool SolveSudokuHelper(char[][] board, int row, int col)
        {
            while (row < 9 && col < 9)
            {
                if (board[row][col] == '.')
                {
                    break;
                }
                if (col == 8)
                {
                    //Reached end of row
                    col = 0;
                    row++;
                }
                else
                {
                    col++;
                }
            }

            if (row >= 9)
            {
                // This row is finished.
                return true;
            }

            for (var num = 1; num <= 9; num++)
            {
                if (IsValidSudokuChoice(board, row, col, num))
                {
                    board[row][col] = (char)(num + '0');
                    // consider at row end
                    int newRow = row + col / 8;
                    // consider at col end
                    int newCol = (col + 1) % 9;
                    if (SolveSudokuHelper(board, newRow, newCol))
                    {
                        return true;
                    }
                    board[row][col] = '.';
                }
            }

            return false;
        }

        // Do not track row and col scan for empty every recursion
        public bool SolveSudokuHelperTwo(char[][] board)
        {
            for (var row  = 0; row < board.Length; row++) {
                for (var col = 0; col < board[0].Length; col++) {
                    if (board[row][col] == '.')
                    {
                        for (var num = 1; num <= 9; num++)
                        {
                            if (IsValidSudokuChoice(board, row, col, num))
                            {
                                board[row][col] = (char)(num + '0');

                                if (SolveSudokuHelperTwo(board))
                                {
                                    return true;
                                }
                                board[row][col] = '.';
                            }
                        }

                        return false;
                    }
                }
            }

            return true;
        }

        public bool IsValidSudokuChoice(char[][] board, int row, int col, int num)
        {
            // Check col and row
            for (var i = 0; i < 9; i++) {
                if (board[row][i] == num + '0' || board[i][col] == num + '0')
                {
                    return false;
                }
            }
            
            // Check 3 by 3
            // check point 4,4. Start is 3,3.
            int rowOffset = (row / 3) * 3;
            int colOffset = (col / 3) * 3;
            for (var i = 0; i < 3; i++) {
                for (var j = 0; j < 3; j++) {
                    if (board[i + rowOffset][j + colOffset] == num+'0')
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public IList<IList<int>> CombinationSum3(int k, int n)
        {
            var result = new List<IList<int>>();
            if (k == 0 || n == 0)
            {
                return result;
            }

            CombinationSum3Helper(n, k, 1, new List<int>(), result);
            return result;
        }

        public void CombinationSum3Helper(int target, int k, int start, List<int> path, List<IList<int>> result)
        {
            if (target < 0 || path.Count > k)
            {
                return;
            }

            if (path.Count == k && target == 0)
            {
                result.Add(new List<int>(path));
                return;
            }

            for (var i = start; i <= 9; i++)
            {
                if (i > target)
                {
                    return;
                }

                path.Add(i);
                CombinationSum3Helper(target - i, k, i + 1, path, result);
                path.RemoveAt(path.Count - 1);
            }
        }
        public IList<IList<int>> GetFactors(int n)
        {
            var result = new List<IList<int>>();
            var path = new List<int>();
            GetFactorsHelper(result, path, n, 2);
            return result;
        }

        private void GetFactorsHelper(List<IList<int>> result, List<int> path, int n, int start)
        {
            if (n == 1)
            {
                if (path.Count > 0)
                {
                    result.Add(new List<int>(path));
                }
                return;
            }

            for (var i = start; i <= n; i++) {
                if (n % i == 0)
                {
                    path.Add(i);
                    GetFactorsHelper(result, path, n / i, i);
                    path.RemoveAt(path.Count - 1);
                }
            }
        }

        public IList<string> GeneratePalindromes(string s)
        {
            int odd = 0;
            var mid = "";

            var result = new List<string>();
            var list = new List<char>();


            var map = new Dictionary<char, int>();

            // Build character count map and count odds
            foreach (var t in s)
            {
                if (!map.ContainsKey(t))
                {
                    map.Add(t,1);
                }
                else
                {
                    map[t]++;
                }

                odd += map[t] % 2 == 0 ? -1 : 1;
            }

            if (odd > 1)
            {
                return result;
            }

            // Add half count of each character to list
            foreach (var set in map)
            {
                var key = set.Key;
                int val = set.Value;


                // Set mid char for all permutation if s is odd.
                if (val % 2 != 0)
                {
                    mid += key;
                }

                for (int i =0; i < val/2; i++)
                {
                    list.Add(key);
                }
            }


            // Generate all the permutation
            GetPerm(list, mid, new bool[list.Count], new StringBuilder(), result);

            return result;
        }

        private void GetPerm(List<char> list, string mid, bool[] used, StringBuilder path, List<string> result)
        {

            // Use all chars
            if (path.Length == list.Count)
            {
                result.Add(path + mid + new string(path.ToString().Reverse().ToArray()));
                return;
            }

            for (var i = 0; i < list.Count; i++)
            {
                // skip duplicate
                if (i > 0 && list[i] == list[i-1] && !used[i-1])
                {
                    continue;
                }

                if (!used[i])
                {
                    path.Append(list[i]);
                    used[i] = true;
                    GetPerm(list, mid, used, path, result);
                    path.Remove(path.Length - 1, 1);
                    used[i] = false;
                }

            }
        }

        public IList<IList<int>> NumSquares(int n)
        {
            var result = new List<IList<int>>();
            NumSquares(n, new List<int>(), result);
            return result;
        }

        private void NumSquares(int n, List<int> path, List<IList<int>> result)
        {
            if (n == 0)
            {
                result.Add(new List<int>(path));
            }

            for (int i = 1; i <= Math.Sqrt(n); i++)
            {
                var curr = (int)Math.Pow(i, 2);;
                if (n >= Math.Pow(i,2))
                {
                    path.Add(i);
                    NumSquares(n - curr, path, result);
                    path.RemoveAt(path.Count - 1);
                }
            }
        }

        readonly Dictionary<string, bool> _flipGameMem = new Dictionary<string, bool>();
        public bool CanWin(string s)
        {
            if (string.IsNullOrEmpty(s) || s.Length < 2)
            {
                return false;
            }

            if (_flipGameMem.ContainsKey(s))
            {
                return _flipGameMem[s];
            }

            for (var i = 0; i<s.Length-1;i++) {
                if (s[i] == '+' && s[i+1] == '+')
                {
                    var opponentString = s.Substring(0, i) + "--" + s.Substring(i + 2);

                    // If opponent can't win I win.
                    if (!CanWin(opponentString))
                    {
                        _flipGameMem.Add(s, true);
                        return true;
                    }
                }
            }
            _flipGameMem.Add(s, false);
            return false;
        }

        public bool IsAdditiveNumber(string nums)
        {
            // Find first and second number, check if they are valid.
            // The length of second number depends on the length of the first number. Max(i,j) <= n - i - j, the third number must have more digits than i or j, whichever has most digits.
            // Then validate rest of the numbers based on n1 and n2. n3 = n1 + n2, n4 = n2+n3..
            var n = nums.Length;
            for (var i = 1; i <= (n-1)/2;i++) {
                if (nums[0] == '0' && i > 1)
                {
                    return false;
                }
                var n1 = long.Parse(nums.Substring(0, i));
                for (var j = 1; Math.Max(i,j) <= n - i - j; j++) {

                    if (nums[i] == '0' && j > 1)
                    {
                       break;
                    }

                    var n2 = long.Parse(nums.Substring(i, j));

                    if (IsValidAdditiveNumber(n1,n2,i+j,nums))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private bool IsValidAdditiveNumber(long n1, long n2, int start, string nums)
        {
            if (start == nums.Length)
            {
                return true;
            }

            var next = nums.Substring(start);

            var sum = n1 + n2;
            if (!next.StartsWith(sum.ToString()))
            {
                return false;
            }

            return IsValidAdditiveNumber(n2, sum, start + sum.ToString().Length, nums);
        }
    }
}
