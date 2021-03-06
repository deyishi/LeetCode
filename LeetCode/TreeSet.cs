﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using LeetCode.DataModel;
using NUnit.Framework;

namespace LeetCode
{
    public class TreeSet
    {
        [Test]
        public void Test()
        {
            var t = new int[] {1, 2, 3, 4, 5}.ToTree();
            var r = FindLeaves(t);
        }


        public IList<int> PreorderTraversal(TreeNode root)
        {
            var result = new List<int>();
            if (root == null)
            {
                return result;
            }

            PreorderTraversalHelper(result, root);
            return result;
        }

        private void PreorderTraversalHelper(List<int> result, TreeNode node)
        {
            if (node == null)
            {
                return;
            }
            result.Add(node.val);
            PreorderTraversalHelper(result, node.left);
            PreorderTraversalHelper(result, node.right);
        }

        /// <summary>
        /// Use dfs update all the left most nodes.
        /// parent.left(root node).right = parent
        /// parent.left(root node).left = parent.right
        /// Cut leaf node converted from parent
        /// parent.left == null
        /// parent.right == null
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        public TreeNode UpsideDownBinaryTree(TreeNode root)
        {
            if (root == null || root.left == null)
            {
                return root;
            }

            TreeNode node = UpsideDownBinaryTree(root.left);
            root.left.right = root;
            root.left.left = root.right;
            root.left = null;            // important
            root.right = null;
            return node;
        }

        //199. Binary Tree Right Side View
        //Check each level from left to right, use size of the list and depth to add to result list.
        public IList<int> RightSideView(TreeNode root)
        {
            var result = new List<int>();
            if (root == null)
            {
                return result;
            }

            RightSideViewHelper(result, root, 0);
            return result;
        }

        public void RightSideViewHelper(List<int> result, TreeNode root, int depth)
        {
            if (root == null)
            {
                return;
            }

            if (depth == result.Count)
            {
                result.Add(root.val);
            }
            RightSideViewHelper(result, root.right, depth + 1);
            RightSideViewHelper(result, root.left, depth + 1);
        }

        public int CountNodes(TreeNode root)
        {

            if (root == null)
            {
                return 0;
            }

            var leftSubTreeHeight = GetLeftTreeHeight(root);
            var rightSubTreeHeight = GetRightTreeHeight(root);

            if (leftSubTreeHeight == rightSubTreeHeight)
            {
                // Full binary tree
                return (int)Math.Pow(2, leftSubTreeHeight) - 1;
            }

            return CountNodes(root.left) + CountNodes(root.right) + 1;
        }

        public int GetLeftTreeHeight(TreeNode node)
        {
            if (node == null)
            {
                return 0;
            }

            int height = 0;
            while (node != null)
            {
                height++;
                node = node.left;
            }

            return height;
        }
        public int GetRightTreeHeight(TreeNode node)
        {
            if (node == null)
            {
                return 0;
            }

            int height = 0;
            while (node != null)
            {
                height++;
                node = node.right;
            }

            return height;
        }

        public int CountUnivalSubtrees(TreeNode root)
        {
            var count = 0;

            CountUnivalSubtreesHelper(root, ref count);
            return count;
        }

        private bool CountUnivalSubtreesHelper(TreeNode root, ref int count)
        {
            if (root == null)
            {
                return true;
            }

            var right = CountUnivalSubtreesHelper(root.right, ref count);
            var left = CountUnivalSubtreesHelper(root.left, ref count);

            if (right && left)
            {
                if (root.right != null && root.val != root.right.val)
                {
                    return false;
                }

                if (root.left != null && root.val != root.left.val)
                {
                    return false;
                }

                count += 1;
                return true;
            }

            return false;
        }

        public bool VerifyPreorder(int[] preorder)
        {
            // Min to keep track left sub tree node, all the right tree nodes should be large than min.
            // Stack to keep track sub tree nodes, when p > min, then we are moving from left to right, remove all the left tree nodes.

            int min = int.MinValue;
            var stack = new Stack<int>();
            foreach (var p in preorder)
            {
                if (p < min)
                {
                    return false;
                }

                while (stack.Any() && p > stack.Peek())
                {
                    min = stack.Pop();
                }

                stack.Push(p);
            }

            return true;

        }
        public TreeNode InorderSuccessor(TreeNode root, TreeNode p)
        {
            // If current node is large than p then we look at current.left else go to right since it will be large than current node. Keep going left and right until we find the smallest node that is large than p.
            TreeNode succ = null;

            while (root != null)
            {
                if (root.val > p.val)
                {
                    succ = root;
                    root = root.left;
                }
                else
                {
                    root = root.right;
                }
            }

            return succ;
        }

        public int LongestConsecutive(TreeNode root)
        {
            if (root == null)
            {
                return 0;
            }

            var result = 0;
            LongestConsecutiveHelper(root, root.val, 0, ref result);
            return result;
        }

        private void LongestConsecutiveHelper(TreeNode root, int target, int curr, ref int result)
        {
            if (root == null)
            {
                return;
            }

            if (target == root.val)
            {
                curr++;
            }
            else
            {
                curr = 1;
            }

            result = Math.Max(result, curr);
            LongestConsecutiveHelper(root.left, root.val + 1, curr, ref result);
            LongestConsecutiveHelper(root.right, root.val + 1, curr, ref result);
        }

        //Find two ends and move toward middle.
        public IList<int> FindMinHeightTrees(int n, int[,] edges)
        {
            var leaves = new List<int>();
            if (n < 2)
            {
                leaves.Add(0);
                return leaves;
            }

            if (n < 3)
            {
                leaves.Add(0);
                leaves.Add(1);
                return leaves;
            }

            // Make graph
            var graph = new Dictionary<int, HashSet<int>>();
            for (int i = 0; i < edges.GetLength(0); i++)
            {
                var key1 = edges[i, 0];
                var key2 = edges[i, 1];
                if (graph.ContainsKey(key1))
                {
                    graph[key1].Add(key2);
                }
                else
                {
                    graph.Add(key1, new HashSet<int> { key2 });
                }

                if (graph.ContainsKey(key2))
                {
                    graph[key2].Add(key1);
                }
                else
                {
                    graph.Add(key2, new HashSet<int> { key1 });
                }
            }

            // Find end
            foreach (var node in graph)
            {
                if (node.Value.Count == 1)
                {
                    leaves.Add(node.Key);
                }
            }

            while (n > 2)
            {
                n -= leaves.Count;
                var newLeaves = new List<int>();
                foreach (var leaf in leaves)
                {
                    foreach (var key in graph[leaf])
                    {
                        graph[key].Remove(leaf);
                        if (graph[key].Count == 1)
                        {
                            newLeaves.Add(key);
                        }
                    }
                    graph.Remove(leaf);
                }

                leaves = newLeaves;
            }


            return leaves;
        }

        // Queue to do level order traversal
        // HdQueue to track each node's horizontal distance
        // Map to track each Hd's node.
        // Min and max to track range of hd in the map to generate result.
        public IList<IList<int>> VerticalOrder(TreeNode root)
        {
            var result = new List<IList<int>>();
            if (root == null)
            {
                return result;
            }

            var queue = new Queue<TreeNode>();
            var hdQueue = new Queue<int>();
            var map = new Dictionary<int, List<int>>();
            queue.Enqueue(root);
            hdQueue.Enqueue(0);

            var min = 0;
            var max = 0;
            while (queue.Count > 0)
            {
                var node = queue.Dequeue();
                var hd = hdQueue.Dequeue();
                if (!map.ContainsKey(hd))
                {
                    map.Add(hd, new List<int>());
                }
                map[hd].Add(node.val);

                if (node.left != null)
                {
                    queue.Enqueue(node.left);
                    hdQueue.Enqueue(hd - 1);
                    min = Math.Min(min, hd - 1);
                }

                if (node.right != null)
                {
                    queue.Enqueue(node.right);
                    hdQueue.Enqueue(hd + 1);
                    max = Math.Max(max, hd + 1);
                }
            }

            for (var i = min; i <= max; i++)
            {
                result.Add(map[i]);
            }

            return result;
        }


        //124. Binary Tree Maximum Path Sum
        //Initiate _maxPath int.min and call MaxGain(root), check gain at each node.
        //MaxGain:
        //  1.Base case node == null, 0 gain
        //  2.Call MaxGain recursively for the node's children to compute max gain from the left and right subtrees: leftGain = Math.Max(0, MaxGain(node.left)), rightGain = Math.Max(0, MaxGain(node.right))
        //  3.Check to continue the old path or to start a new path.Start a new path would be node.val + leftGain + rightGain.Update _maxPath.
        //  4.For the recursion return, one or zero subtrees could add to the current path node.val + Math.Max(leftGain, rightGain).
        private int _pathMax;
        public int MaxPathSum(TreeNode root)
        {
            _pathMax = int.MinValue;
            MaxPathDown(root);
            return _pathMax;
        }

        private int MaxPathDown(TreeNode root)
        {
            if (root == null)
            {
                return 0;
            }

            int left = Math.Max(0, MaxPathDown(root.left));
            int right = Math.Max(0, MaxPathDown(root.right));

            _pathMax = Math.Max(_pathMax, left + right + root.val);

            // return a single child so it can form a path with root.
            return Math.Max(left, right) + root.val;
        }

        public bool IsValidSerialization(string preorder)
        {
            var nodes = preorder.Split(',');
            var diff = 1;

            // All non-null nodes have 2 leaves and 1 root.
            // All null nodes have 0 leaves and 1 root.
            // For each node, we decrease diff by 1, because the node provides a root.
            // If the node is not null, we increase diff by 2, because the node provides two leaves.
            // In the end, diff should be negative (more nodes than needed, 9###), diff shouldn't be positive (not enough nodes, 9#), diff should be 0.

            foreach (var p in nodes)
            {

                // More nodes than needed. 9,#,#,#
                if (--diff < 0)
                {
                    return false;
                }

                // When we see a non-null, we update diff by 2.
                if (p != "#")
                {
                    diff += 2;
                }
            }

            return diff == 0;
        }
        public IList<IList<int>> FindLeaves(TreeNode root)
        {
            var result = new List<IList<int>>();
            FindLeavesHelper(root, result);
            return result;
        }

        public int FindLeavesHelper(TreeNode node, List<IList<int>> result)
        {
            if (node == null)
            {
                return -1;
            }

            int level = 1 + Math.Max(FindLeavesHelper(node.left, result), FindLeavesHelper(node.right, result));
            if (result.Count < level + 1)
            {
                result.Add(new List<int>());
            }

            result[level].Add(node.val);

            return level;

        }


    }

    public class Codec
    {

        // Encodes a tree to a single string.
        public string Serialize(TreeNode root)
        {
            StringBuilder sb = new StringBuilder();
            if (root == null)
            {
                return sb.ToString();
            }

            // Pre order traversal
            Stack<TreeNode> stack = new Stack<TreeNode>();
            stack.Push(root);
            while (stack.Count > 0)
            {
                TreeNode node = stack.Pop();
                sb.Append(node.val);
                sb.Append(",");

                if (node.right != null)
                {
                    stack.Push(node.right);
                }

                if (node.left != null)
                {
                    stack.Push(node.left);
                }
            }

            // Remove last ,
            return sb.ToString().TrimEnd(',');
        }

        // Decodes your encoded data to tree.
        public TreeNode Deserialize(string data)
        {
            if (string.IsNullOrEmpty(data))
            {
                return null;
            }

            var nodes = data.Split(',');

            Queue<int> q = new Queue<int>();
            foreach (var n in nodes)
            {
                q.Enqueue(int.Parse(n));
            }

            return DeserializePreOrderTree(q);
        }

        private TreeNode DeserializePreOrderTree(Queue<int> queue)
        {
            if (queue.Count == 0)
            {
                return null;
            }

            TreeNode root = new TreeNode(queue.Dequeue());
            var leftSubtree = new Queue<int>();
            while (queue.Count > 0 && queue.Peek() < root.val)
            {
                leftSubtree.Enqueue(queue.Dequeue());
            }

            root.left = DeserializePreOrderTree(leftSubtree);
            root.right = DeserializePreOrderTree(queue);
            return root;
        }

        public int LargestBSTSubtree(TreeNode root)
        {
            
            var t= LargestBSTSubtreeHelper(root);
            return t.Size;
        }

        private LargestBSTSubtreeNodeProperty LargestBSTSubtreeHelper(TreeNode root)
        {
            if (root == null)
            {
                return new LargestBSTSubtreeNodeProperty();
            }

           
            var left = LargestBSTSubtreeHelper(root.left);
            var right = LargestBSTSubtreeHelper(root.right);

            var n = new LargestBSTSubtreeNodeProperty();
            if (!left.IsValid || !right.IsValid || root.val <= left.Max || root.val >= right.Min)
            {
                n.IsValid = false;
                n.Size = Math.Max(left.Size, right.Size);
                return n;
            }

            n.IsValid = true;
            n.Size = right.Size + left.Size + 1;

            n.Min = root.left == null ? root.val : left.Min;
            n.Max = root.right == null ? root.val : right.Max;

            return n;
        }
    }

    public class LargestBSTSubtreeNodeProperty
    {
        public bool IsValid { get; set; }
        public int Size { get; set; }
        public int Min { get; set; }
        public int Max { get; set; }

        public LargestBSTSubtreeNodeProperty()
        {
            IsValid = true;
            Size = 0;
            Min = int.MaxValue;
            Max = int.MinValue;
        }

    }

    // 173. Binary Search Tree Iterator
    // Use stack to store left most node and its parents.
    // Next return left most node and push its right child's left most node to stack.
    // If stack is not empty, then it has next.
    public class BSTIterator
    {
        private Stack<TreeNode> stack = new Stack<TreeNode>();
        public BSTIterator(TreeNode root)
        {
            PushAll(root);
        }

        /** @return the next smallest number */
        public int Next()
        {
            TreeNode curr = stack.Pop();
            PushAll(curr.right);
            return curr.val;
        }

        /** @return whether we have a next smallest number */
        public bool HasNext()
        {
            return stack.Any();
        }

        private void PushAll(TreeNode node)
        {
            while (node != null)
            {
                stack.Push(node);
                node = node.left;
            }
        }
    }
}
