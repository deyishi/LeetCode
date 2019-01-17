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

            var stack = new Stack<int>();
            stack.Push(0);
            stack.Push(1);
            var x = stack.Pop();
            var t = new int?[] { 7, 3, 15, null,null, 9,20 }.ToTree();
            var r = new BSTIterator(t);
            var res = r.Next();
            res = r.Next();
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
