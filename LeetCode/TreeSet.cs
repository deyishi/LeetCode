using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeetCode.DataModel;
using NUnit.Framework;

namespace LeetCode
{
    public class TreeSet
    {
        [Test]
        public void PreorderTraversal()
        {

            var t = new int?[] {5, 1, 5, 5, 5, null, 5}.ToTree();

            var a = new int[] {5, 2, 1, 3, 6};
            var r = VerifyPreorder(a);

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

                count +=1;
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
