using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using LeetCode.DataModel;
using NUnit.Framework;

namespace LeetCode.Nine_Chapter
{
    public class TreeProblems
    {
        public IList<TreeNode> GenerateTrees(int n)
        {
            if (n == 0)
            {
                return new List<TreeNode>();
            }


            return GenTrees(1, n);
        }

        public List<TreeNode> GenTrees(int start, int end)
        {

            var list = new List<TreeNode>();

            if (start > end)
            {
                list.Add(null);
                return list;
            }

            if (start == end)
            {
                list.Add(new TreeNode(start));
                return list;
            }

            for (var i = start; i <= end; i++)
            {

                var left = GenTrees(start, i - 1);
                var right = GenTrees(i + 1, end);

                foreach (var lnode in left)
                {
                    foreach (var rnode in right)
                    {

                        list.Add(new TreeNode(i)
                        {
                            left = lnode,
                            right = rnode
                        });
                    }
                }
            }

            return list;
        }

        public IList<IList<int>> LevelOrder(TreeNode root)
        {
            var result = new List<IList<int>>();
            if (root == null)
            {
                return result;
            }

            var queue = new Queue<TreeNode>();
            queue.Enqueue(root);
            while (queue.Any())
            {
                var size = queue.Count;
                var list = new List<int>();
                for (var i = 0; i < size; i++)
                {
                    var node = queue.Dequeue();
                    list.Add(node.val);
                    if (node.left != null)
                    {
                        queue.Enqueue(node.left);
                    }

                    if (node.right != null)
                    {
                        queue.Enqueue(node.right);
                    }
                    result.Add(list);
                }
            }

            return result;
        }

        public IList<int> PreorderTraversal(TreeNode root)
        {
            var result = new List<int>();
            if (root == null)
            {
                return result;
            }
            var stack = new Stack<TreeNode>();
            stack.Push(root);
            while (stack.Any())
            {
                var node = stack.Pop();
                result.Add(node.val);

                if (node.right != null)
                {
                    stack.Push(node.right);
                }

                if (node.left != null)
                {
                    stack.Push(node.left);
                }
            }

            return result;
        }

        public IList<int> InorderTraversal(TreeNode root)
        {
            var result = new List<int>();
            if (root == null)
            {
                return result;
            }
            var stack = new Stack<TreeNode>();
            var curr = root;
            while (true)
            {
                if (curr != null)
                {
                    stack.Push(curr);
                    curr = curr.left;
                }
                else
                {
                    if (!stack.Any())
                    {
                        break;
                    }
                    curr = stack.Pop();
                    result.Add(curr.val);
                    curr = curr.right;
                }
            }

            return result;
        }

        public void TraversalHelper(TreeNode root, List<int> result)
        {
            if (root == null)
            {
                return;
            }

            // Change order to do inorder, preorder, postorder.
            TraversalHelper(root.left, result);
            TraversalHelper(root.right, result);
            result.Add(root.val);
        }

        public bool IsValidBST(TreeNode root)
        {
            if (root == null)
            {
                return true;
            }

            var stack = new Stack<TreeNode>();
            var curr = root;
            TreeNode pre = null;
            while (true)
            {
                if (curr != null)
                {
                    stack.Push(curr);
                    curr = curr.left;
                }
                else
                {
                    if (!stack.Any())
                    {
                        break;
                    }
                    curr = stack.Pop();
                    if (pre != null && curr.val <= pre.val)
                    {
                        return false;
                    }

                    pre = curr;
                    curr = curr.right;
                }
            }

            return true;

        }

        public int KthSmallest(TreeNode root, int k)
        {
            if (root == null)
            {
                return 0;
            }

            var stack = new Stack<TreeNode>();
            var curr = root;
            while (true)
            {
                if (curr != null)
                {
                    stack.Push(curr);
                    curr = curr.left;
                }
                else
                {
                    if (!stack.Any())
                    {
                        break;
                    }

                    var node = stack.Pop();

                    if (k == 0)
                    {
                        return node.val;
                    }

                    k--;
                    curr = node.right;
                }
            }

            return 0;
        }

        [Test]
        public void ZigzagLevelOrder()
        {
            var t = new int[] { 1, 2, 3 }.ToTree();

            var r = ZigzagLevelOrder(t);
        }

        public IList<IList<int>> ZigzagLevelOrder(TreeNode root)
        {
            var result = new List<IList<int>>();
            if (root == null)
            {
                return result;
            }

            var queue = new Queue<TreeNode>();
            queue.Enqueue(root);
            var level = 1;
            while (queue.Any())
            {
                var size = queue.Count;
                var list = new List<int>();
                for (var i = 0; i < size; i++)
                {
                    var node = queue.Dequeue();
                    if (level % 2 != 0)
                    {
                        list.Add(node.val);
                    }
                    else
                    {
                        list.Insert(0, node.val);
                    }

                    if (node.left != null)
                    {
                        queue.Enqueue(node.left);
                    }

                    if (node.right != null)
                    {
                        queue.Enqueue(node.right);
                    }
                }

                level++;
                result.Add(list);
            }

            return result;
        }

        [Test]
        public void PostorderTraversal()
        {
            var t = new[] { 1, 2, 3 }.ToTree();
            var r = PostorderTraversal(t);
        }
        public IList<int> PostorderTraversal(TreeNode root)
        {
            var result = new List<int>();
            if (root == null)
            {
                return result;
            }

            var stack = new Stack<TreeNode>();
            var curr = root;
            TreeNode prev = null;
            while (curr != null || stack.Any())
            {
                if (curr != null)
                {
                    stack.Push(curr);
                    curr = curr.left;
                }
                else
                {
                    var node = stack.Peek();
                    // If is leaf 
                    if (node.right != null && node.right != prev)
                    {
                        curr = node.right;
                    }
                    else
                    {
                        var popped = stack.Pop();
                        result.Add(popped.val);
                        prev = popped;
                    }
                }
            }

            return result;
        }

        public TreeNode BuildTree(int[] preorder, int[] inorder)
        {
            if (preorder == null || preorder.Length < 1 || inorder == null || inorder.Length < 1)
            {
                return null;
            }

            return BuildTreeHelper(0, 0, 0, preorder, inorder);
        }

        private TreeNode BuildTreeHelper(int preStart, int inStart, int inEnd, int[] preorder, int[] inorder)
        {
            if (preStart > preorder.Length || inStart > inEnd)
            {
                return null;
            }

            var rootVal = preorder[preStart];

            var index = inStart;
            while (index <= inEnd)
            {
                if (inorder[index] == rootVal)
                {
                    break;
                }
                index++;
            }

            var root = new TreeNode(rootVal);

            root.left = BuildTreeHelper(preStart + 1, inStart, index - 1, preorder, inorder);
            root.right = BuildTreeHelper(preStart + (index - inStart) + 1, index + 1, inEnd, preorder, inorder);

            return root;
        }

        [Test]
        public void BuildTreeTwo()
        {
            var inorder = new[] { 9, 3, 15, 20, 7 };
            var postorder = new[] { 9, 15, 7, 20, 3 };

            var r = BuildTreeTwo(inorder, postorder);
        }
        public TreeNode BuildTreeTwo(int[] inorder, int[] postorder)
        {
            if (inorder == null || postorder == null || inorder.Length == 0 || inorder.Length != postorder.Length)
            {
                return null;
            }

            return BuildTreeTwoHelper(postorder.Length - 1, 0, inorder.Length - 1, inorder, postorder);
        }

        private TreeNode BuildTreeTwoHelper(int postEnd, int inStart, int inEnd, int[] inorder, int[] postorder)
        {
            if (postEnd < 0 || inStart > inEnd)
            {
                return null;
            }

            var rootVal = postorder[postEnd];
            var index = inEnd;
            while (index >= inStart)
            {
                if (inorder[index] == rootVal)
                {
                    break;
                }

                index--;
            }


            return new TreeNode(rootVal)
            {
                left = BuildTreeTwoHelper(postEnd - (inEnd - index) - 1, inStart, index - 1, inorder, postorder),
                right = BuildTreeTwoHelper(postEnd - 1, index + 1, inEnd, inorder, postorder)

            };
        }

        [Test]
        public void PathSum()
        {
            var t = new[] { 1, 2 }.ToTree();
            var r = PathSum(t, 1);
        }
        public IList<IList<int>> PathSum(TreeNode root, int sum)
        {
            var result = new List<IList<int>>();
            if (root == null)
            {
                return result;
            }

            var stack = new Stack<TreeNode>();
            var curr = root;
            TreeNode prev = null;
            var list = new List<int>();
            while (curr != null || stack.Any())
            {
                if (curr != null)
                {
                    stack.Push(curr);
                    list.Add(curr.val);
                    curr = curr.left;
                }
                else
                {
                    var node = stack.Peek();
                    if (node.right != null && node.right != prev)
                    {
                        curr = node.right;
                    }
                    else
                    {
                        var popped = stack.Pop();
                        if (popped.left == null && popped.right == null && list.Sum() == sum)
                        {
                            result.Add(new List<int>(list));
                        }

                        list.RemoveAt(list.Count - 1);
                        prev = popped;
                    }
                }
            }

            return result;
        }

        [Test]
        public void ValidTree()
        {
            var n = 5;
            var edges = new int[,]
            {
                {0, 1},
                {0, 2},
                {0, 3},
                {1, 4}
            };

            var r = ValidTree(n, edges);
        }

        public bool ValidTree(int n, int[,] edges)
        {
            if (n == 0 || n - 1 != edges.GetLength(0))
            {
                return false;
            }

            //setup graph
            var graph = new Dictionary<int, List<int>>();
            for (var i = 0; i < n; i++)
            {
                graph.Add(i, new List<int>());
            }

            for (var i = 0; i < edges.GetLength(0); i++)
            {

                var u = edges[i, 0];
                var v = edges[i, 1];
                graph[u].Add(v);
                graph[v].Add(u);
            }


            var q = new Queue<int>();
            var visited = new HashSet<int>();
            q.Enqueue(0);
            visited.Add(0);
            while (q.Any())
            {
                var node = q.Dequeue();
                foreach (var neighbor in graph[node])
                {
                    if (visited.Contains(neighbor))
                    {
                        continue;
                    }
                    q.Enqueue(neighbor);
                    visited.Add(neighbor);
                }
            }

            return visited.Count == n;
        }

        public UndirectedGraphNode CloneGraph(UndirectedGraphNode node)
        {
            if (node == null)
            {
                return null;
            }

            // Get all nodes 
            UndirectedGraphNode result = null;
            var q = new Queue<UndirectedGraphNode>();
            q.Enqueue(node);
            var map = new Dictionary<UndirectedGraphNode, UndirectedGraphNode>();
            map.Add(node, new UndirectedGraphNode(node.label));
            while (q.Any())
            {
                var curr = q.Dequeue();
                foreach (var neighbor in curr.neighbors)
                {
                    if (!map.ContainsKey(neighbor))
                    {
                        q.Enqueue(neighbor);
                        map.Add(neighbor, new UndirectedGraphNode(neighbor.label));
                    }
                }
            }

            foreach (var v in map.Keys)
            {
                var newNode = map[v];
                foreach (var n in v.neighbors)
                {
                    var newNeighbor = map[n];
                    newNode.neighbors.Add(newNeighbor);
                }


            }
            return map[node];
        }

        [Test]
        public void TopSort()
        {
            var root = new UndirectedGraphNode(0);
            var one = new UndirectedGraphNode(1);
            var two = new UndirectedGraphNode(2);
            var three = new UndirectedGraphNode(3);
            var four = new UndirectedGraphNode(4);
            var five = new UndirectedGraphNode(5);

            root.neighbors.Add(one);
            root.neighbors.Add(two);
            root.neighbors.Add(three);
            one.neighbors.Add(four);
            two.neighbors.Add(four);
            two.neighbors.Add(five);
            three.neighbors.Add(four);
            three.neighbors.Add(five);

            var list = new List<UndirectedGraphNode>
            {
                root, one, two, three, four, five
            };

            var r = TopSort(list);
        }

        public List<UndirectedGraphNode> TopSort(List<UndirectedGraphNode> graph)
        {
            if (graph == null)
            {
                return null;
            }

            var map = new Dictionary<UndirectedGraphNode, int>();
            foreach (var undirectedGraphNode in graph)
            {
                var neighbors = undirectedGraphNode.neighbors;
                foreach (var n in neighbors)
                {
                    if (map.ContainsKey(n))
                    {
                        map[n]++;
                    }
                    else
                    {
                        map.Add(n, 1);
                    }
                }
            }

            var queue = new Queue<UndirectedGraphNode>();
            var result = new List<UndirectedGraphNode>();
            foreach (var node in graph)
            {
                if (!map.ContainsKey(node))
                {
                    queue.Enqueue(node);
                    result.Add(node);
                }
            }

            while (queue.Any())
            {
                var node = queue.Dequeue();
                foreach (var n in node.neighbors)
                {
                    map[n]--;
                    if (map[n] == 0)
                    {
                        result.Add(n);
                        queue.Enqueue(n);
                    }
                }
            }

            if (result.Count == graph.Count)
            {
                return result;
            }

            return null;
        }

        [Test]
        public void NumIslands()
        {
            var n = new char[,]
            {
                {'1', '1', '1', '1', '0'},
                {'1', '1', '0', '1', '0'},
                {'1', '1', '0', '0', '0'},
                {'0', '0', '0', '0', '0'}
            };

            var t = NumIslands(n);
        }

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
            queue.Enqueue(new[,] {{r, c}});
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
                            queue.Enqueue(new[,] {{row, col}});
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
            var zombiePosition = new Queue<int [,]>();
            for (var r = 0; r < rows; r++)
            {
                for (var c = 0; c < columns; c++)
                {
                    if (grid[r,c] == People)
                    {
                        peopleCount++;
                    }else if (grid[r, c] == Zombie)
                    {
                        zombiePosition.Enqueue(new[,] {{r, c}});
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

                        zombiePosition.Enqueue(new[,] {{row, col}});
                    }
                }
            }

            return -1;
        }

        [Test]
        public void LadderLength()
        {
            var b = "hit";
            var e = "cog";
            var l = new[] { "hot", "dot", "dog", "lot", "log", "cog" };
            var r = LadderLength(b, e, l);
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
            var visited = new bool[wordList.Count ];
            var result = 0;
            q.Enqueue(beginWord);
            while (q.Any())
            {
                result++;
                var size = q.Count;
                for (var i = 0; i < size; i++)
                {
                    var curr = q.Dequeue();
                    for (var j = 0; j < wordList.Count;j++)
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

        [Test]
        public void Solve()
        {
            var g = new[,]
            {
                {'O', 'O', 'O', 'O', 'X', 'X'}, {'O', 'O', 'O', 'O', 'O', 'O'}, {'O', 'X', 'O', 'X', 'O', 'O'},
                {'O', 'X', 'O', 'O', 'X', 'O'}, {'O', 'X', 'O', 'X', 'O', 'O'}, {'O', 'X', 'O', 'O', 'O', 'O'}
            };

            Solve(g);
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
    }
}


