using System;
using System.Collections.Generic;
using System.Linq;
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
            TraversalHelper(root.right,result);
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
            var t= new int[] {1, 2, 3}.ToTree();

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
            var t = new[] {1, 2, 3}.ToTree();
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

                    var node = stack.Peek();
                    if (node.right != null)
                    {
                        curr = node.right;
                    }
                    else
                    {
                        var popped = stack.Pop();
                        result.Add(popped.val);
                        // Both left and right child visited, visit parent.
                        while (stack.Any() && popped == stack.Peek().right)
                        {
                            popped = stack.Pop();
                            result.Add(popped.val);
                        }
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
            while (index < preorder.Length)
            {
                if (inorder[index] == rootVal)
                {
                    break;
                }
                index++;
            }

            var root = new TreeNode(rootVal);

            root.left = BuildTreeHelper(preStart + 1, inStart, index - 1, preorder, inorder);
            root.right = BuildTreeHelper(preStart + (index - preStart) + 1, index + 1, inEnd, preorder, inorder);

            return root;
        }
    }
}
