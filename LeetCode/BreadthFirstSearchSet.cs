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
            var m = grid.GetLength(0);
            var n = grid.GetLength(1);
            var directions = new[,]
            {
                {1, 0}, {-1, 0}, {0, 1}, {0, -1}
            };
            var visited = new bool[m, n];
            var count = 0;
            for (int r = 0; r < m; r++)
            {
                for (int c = 0; c < n; c++)
                {
                    if (!visited[r, c] && grid[r, c] == '1')
                    {
                        MarkVisited(grid, r, c, visited, directions);
                        count++;
                    }

                }
            }

            return count;
        }

        private void MarkVisited(char[,] grid, int r, int c, bool[,] visited, int[,] directions)
        {

            visited[r, c] = true;
            for (int i = 0; i < directions.GetLength(0); i++)
            {
                var nr = r + directions[i, 0];
                var nc = c + directions[i, 1];
                if (nr >= 0 && nc >= 0 && nr < grid.GetLength(0) && nc < grid.GetLength(1) && !visited[nr, nc] && grid[nr, nc] == '1')
                {
                    MarkVisited(grid, nr, nc, visited, directions);
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

        public int NumberOfPatterns(int m, int n)
        {
            // Number required to pass two point.
            int[,] skip = new int[10, 10];
            skip[1, 3] = skip[3, 1] = 2;
            skip[7, 1] = skip[1, 7] = 4;
            skip[3, 9] = skip[9, 3] = 6;
            skip[7, 9] = skip[9, 7] = 8;
            skip[1, 9] = skip[2, 8] = skip[3, 7] = skip[6, 4] = skip[9, 1] = skip[8, 2] = skip[7, 3] = skip[4, 6] = 5;


            int result = 0;
            bool[] visited = new bool[10];
            for (int i = m; i <= n; i++)
            {
                // 1,3,7,9 share the same path.
                // 2,4,6,8 share the same path.
                // 5 

                // Do dfs on every points count.
                result += NumberOfPatternsHelper(skip, visited, 1, i - 1) * 4;
                result += NumberOfPatternsHelper(skip, visited, 2, i - 1) * 4;
                result += NumberOfPatternsHelper(skip, visited, 5, i - 1);
            }

            return result;
        }

        private int NumberOfPatternsHelper(int[,] skip, bool[] visited, int curr, int remain)
        {
            if (remain < 0)
            {
                return 0;
            }

            if (remain == 0)
            {
                return 1;
            }

            visited[curr] = true;

            int result = 0;
            for (var i = 1; i <= 9; i++)
            {
                if (!visited[i] && (skip[curr, i] == 0 || visited[skip[curr, i]]))
                {
                    result += NumberOfPatternsHelper(skip, visited, i, remain - 1);
                }
            }

            visited[curr] = false;
            return result;
        }
    }
}
