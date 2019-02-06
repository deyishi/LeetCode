using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeetCode.DataModel;

namespace LeetCode
{
    public class BreadthFirstSearchSet
    {
        public int NumIslands(char[,] grid)
        {
            if (grid == null || grid.GetLength(0) == 0 || grid.GetLength(1) == 0)
            {
                return 0;
            }

            var count = 0;

            var rows = grid.GetLength(0);
            var columns = grid.GetLength(1);
            var visited = new bool[rows + 1, columns + 1];

            for (var r = 0; r < rows; r++)
            {
                for (var c = 0; c < columns; c++)
                {

                    if (!visited[r, c] && grid[r, c] == '1')
                    {
                        count++;
                        MarkVisited(grid, r, c, visited);
                    }
                }
            }

            return count;
        }

        private static void MarkVisited(char[,] grid, int r, int c, bool[,] visited)
        {
            var rIndex = new[] { 0, 1, -1, 0 };
            var cIndex = new[] { 1, 0, 0, -1 };
            var queue = new Queue<int[,]>();
            queue.Enqueue(new[,] { { r, c } });
            visited[r, c] = true;

            while (queue.Any())
            {
                var curr = queue.Dequeue();
                for (var k = 0; k < 4; k++)
                {
                    var row = curr[0, 0] + rIndex[k];
                    var col = curr[0, 1] + cIndex[k];

                    if (row >= 0 && row < grid.GetLength(0) && col >= 0 && col < grid.GetLength(1) && !visited[row, col])
                    {
                        visited[row, col] = true;
                        if (grid[row, col] == '1')
                        {
                            queue.Enqueue(new[,] { { row, col } });
                        }
                    }
                }
            }
        }


        public const int Zombie = 1;
        public const int People = 0;
        public const int Wall = 2;

        public int ZombieGrid(int[,] grid)
        {
            if (grid == null || grid.GetLength(0) == 0 || grid.GetLength(1) == 0)
            {
                return 0;
            }

            var rows = grid.GetLength(0);
            var columns = grid.GetLength(1);

            var peopleCount = 0;
            var zombiePosition = new Queue<int[,]>();
            for (var r = 0; r < rows; r++)
            {
                for (var c = 0; c < columns; c++)
                {
                    if (grid[r, c] == People)
                    {
                        peopleCount++;
                    }
                    else if (grid[r, c] == Zombie)
                    {
                        zombiePosition.Enqueue(new[,] { { r, c } });
                    }
                }
            }

            if (peopleCount == 0)
            {
                return 0;
            }


            var rIndex = new[] { 0, 1, -1, 0 };
            var cIndex = new[] { 1, 0, 0, -1 };
            var days = 0;
            while (zombiePosition.Any())
            {
                days++;
                var curr = zombiePosition.Dequeue();
                for (var k = 0; k < 4; k++)
                {
                    var row = curr[0, 0] + rIndex[k];
                    var col = curr[0, 1] + cIndex[k];

                    if (row >= 0 && row < grid.GetLength(0) && col >= 0 && col < grid.GetLength(1) && grid[row, col] == People)
                    {
                        grid[row, col] = Zombie;
                        peopleCount--;
                        if (peopleCount == 0)
                        {
                            return days;
                        }

                        zombiePosition.Enqueue(new[,] { { row, col } });
                    }
                }
            }

            return -1;
        }

        public int LadderLength(string beginWord, string endWord, IList<string> wordList)
        {
            if (wordList == null || wordList.Count == 0)
            {
                return 0;
            }

            if (beginWord.Equals(endWord))
            {
                return 1;
            }

            var wordSet = new HashSet<string>(wordList);

            var q = new Queue<string>();
            var visited = new bool[wordList.Count];
            var result = 0;
            q.Enqueue(beginWord);
            while (q.Any())
            {
                result++;
                var size = q.Count;
                for (var i = 0; i < size; i++)
                {
                    var curr = q.Dequeue();
                    for (var j = 0; j < wordList.Count; j++)
                    {
                        var word = wordList[j];
                        if (CanTransform(curr, word) && !visited[j])
                        {
                            if (word == endWord)
                            {
                                result++;
                                return result;
                            }

                            q.Enqueue(word);
                            visited[j] = true;
                        }
                    }
                }

            }

            return 0;
        }

        private bool CanTransform(string beginWord, string word)
        {
            var diffCount = 0;
            for (var i = 0; i < beginWord.Length; i++)
            {

                if (beginWord[i] != word[i])
                {
                    diffCount++;
                    if (diffCount > 1)
                    {
                        return false;
                    }

                }
            }

            return diffCount == 1;
        }

        public int LadderLengthTwo(string beginWord, string endWord, IList<string> wordList)
        {
            if (wordList == null || wordList.Count == 0)
            {
                return 0;
            }

            if (beginWord.Equals(endWord))
            {
                return 1;
            }

            HashSet<string> wordSet = new HashSet<string>(wordList);

            if (!wordSet.Contains(endWord))
            {
                return 0;
            }

            wordSet.Remove(beginWord);
            wordSet.Remove(endWord);

            HashSet<string> begin = new HashSet<string>();
            HashSet<string> end = new HashSet<string>();

            begin.Add(beginWord);
            end.Add(endWord);

            int dist = 2;

            while (begin.Count > 0 && end.Count > 0)
            {
                if (begin.Count > end.Count)
                {
                    HashSet<string> temp = begin;
                    begin = end;
                    end = temp;
                }

                HashSet<string> nextBegin = new HashSet<string>();

                foreach (string s in begin)
                {
                    char[] arr = s.ToCharArray();

                    for (int i = 0; i < arr.Length; i++)
                    {
                        char c = arr[i];

                        for (int j = 0; j < 26; j++)
                        {
                            arr[i] = (char)('a' + j);

                            string newS = new String(arr);

                            if (end.Contains(newS))
                            {
                                return dist;
                            }

                            if (wordSet.Contains(newS))
                            {
                                nextBegin.Add(newS);
                                wordSet.Remove(newS);
                            }
                        }

                        arr[i] = c;
                    }
                }

                if (nextBegin.Count == 0)
                {
                    return 0;
                }

                begin = nextBegin;
                dist++;
            }

            return 0;
        }

        public void Solve(char[,] board)
        {
            if (board == null)
            {
                return;
            }

            var rows = board.GetLength(0);
            var cols = board.GetLength(1);
            var visited = new bool[board.GetLength(0), board.GetLength(1)];

            for (var r = 0; r < rows; r++)
            {
                for (var c = 0; c < cols; c++)
                {
                    if (board[r, c] == 'O' && !visited[r, c])
                    {
                        MarkBoard(board, visited, r, c);
                    }
                }
            }

        }

        private void MarkBoard(char[,] board, bool[,] visited, int r, int c)
        {
            var x = new[] { 1, 0, 0, -1 };
            var y = new[] { 0, 1, -1, 0 };

            var rows = board.GetLength(0);
            var cols = board.GetLength(1);

            var q = new Queue<Coordinate>();
            q.Enqueue(new Coordinate(r, c));
            var list = new List<Coordinate>();
            var closed = true;
            while (q.Any())
            {
                var curr = q.Dequeue();
                list.Add(curr);
                for (var i = 0; i < 4; i++)
                {
                    var ri = curr.X + x[i];
                    var ci = curr.Y + y[i];
                    if (ri >= 0 && ri < rows && ci >= 0 && ci < cols)
                    {
                        if (board[ri, ci] == 'O' && !visited[ri, ci])
                        {
                            visited[ri, ci] = true;
                            q.Enqueue(new Coordinate(ri, ci));
                        }
                    }
                    else
                    {
                        closed = false;
                    }
                }
            }

            if (closed)
            {
                foreach (var cor in list)
                {
                    board[cor.X, cor.Y] = 'X';
                }
            }
        }

        public void WallsAndGates(int[,] rooms)
        {
            if (rooms == null || rooms.GetLength(0) == 0 || rooms.GetLength(1) == 0)
            {
                return;
            }

            // Find all the gates
            // Explore from all the gates and mark the rooms with distance to gates.
            var queue = new Queue<int[,]>();
            var rows = rooms.GetLength(0);
            var cols = rooms.GetLength(1);
            var visited = new bool[rows, cols];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols;j++) {
                    if (rooms[i,j] == 0)
                    {
                        queue.Enqueue(new[,] {{i, j}});
                        visited[i, j] = true;
                    }
                }
            }


            var rIndex = new[] { 0, 1, -1, 0 };
            var cIndex = new[] { 1, 0, 0, -1 };

            // Explore
            while (queue.Any())
            {
                var currentRoom = queue.Dequeue();
                // Go up, down, left and right.
                for (var i = 0; i < 4; i++)
                {
                    var row = currentRoom[0, 0] + rIndex[i];
                    var col = currentRoom[0, 1] + cIndex[i];
                    if (row >= 0 && row < rows && col >= 0 && col < cols && !visited[row, col] && rooms[row,col] == int.MaxValue)
                    {
                        rooms[row, col] = rooms[currentRoom[0, 0], currentRoom[0, 1]] + 1;
                        queue.Enqueue(new int[,]{{ row, col } });
                    }
                }
            }
        }
    }
}
