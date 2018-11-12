using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Schema;
using LeetCode.DataModel;
using NUnit.Framework;

namespace LeetCode
{
    /// <summary>
    /// Start date 11/11/2018.
    /// End date
    /// </summary>
    public class PracticeSet2
    {
        [Test]
        public void LevelOrderBottom()
        {
            var t = new TreeNode(3)
            {
                left = new TreeNode(9),
                right = new TreeNode(20)
                {
                    left = new TreeNode(15),
                    right = new TreeNode(7)
                }
            };

            LevelOrderBottom(t);
        }


        public IList<IList<int>> LevelOrderBottom(TreeNode root)
        {
            var result = new List<IList<int>>();
            if (root == null)
            {
                return result;
            }

            var q = new Queue<TreeNode>();
            q.Enqueue(root);
            while (q.Any())
            {
                var currLevel = new List<int>();
                var length = q.Count;
                for (int i = 0; i < length; i++)
                {
                    var currNode = q.Dequeue();
                    currLevel.Add(currNode.val);
                    if (currNode.left != null)
                    {
                        q.Enqueue(currNode.left);
                    }

                    if (currNode.right != null)
                    {
                        q.Enqueue(currNode.right);
                    }
                }

                result.Insert(0, currLevel);
            }

            return result;
        }

        [Test]
        public void SortedArrayToBst()
        {
            var a = new int[] {-10, -3, 0, 5, 9};
            var r = SortedArrayToBst(a);
        }

        public TreeNode SortedArrayToBst(int[] nums)
        {
            if (nums == null || nums.Length == 0)
                return null;

            return SortedArrayToBst(nums, 0, nums.Length - 1);
        }

        private TreeNode SortedArrayToBst(int[] nums, int left, int right)
        {
            if (left < right)
            {
                return null;
            }

            var mid = left + (right - left) / 2;

            var root = new TreeNode(nums[mid])
            {
                left = SortedArrayToBst(nums, left, mid),
                right = SortedArrayToBst(nums, mid + 1, right)
            };

            return root;
        }

        [Test]
        public void IsBalanced()
        {
            var t = new TreeNode(1)
            {
                left = new TreeNode(2)
                {
                    left = new TreeNode(3)
                    {
                        left = new TreeNode(4)
                    }
                },
                right = new TreeNode(2)
                {
                    right = new TreeNode(3)
                    {
                        right = new TreeNode(4)
                    }
                }
            };

            var r = IsBalanced(t);

        }

        public bool IsBalanced(TreeNode root)
        {
            if (root == null)
            {
                return true;
            }

            var leftD = IsBalancedDFS(root.left);
            var rightD = IsBalancedDFS(root.right);
            var currBalanced = Math.Abs(leftD - rightD) <= 1;
            var leftBalanced = IsBalanced(root.left);
            var rightBalanced = IsBalanced(root.right);
            var result = currBalanced && leftBalanced && rightBalanced;
            return result;
        }

        public int IsBalancedDFS(TreeNode node)
        {
            if (node == null)
            {
                return 0;
            }

            return 1 + Math.Max(IsBalancedDFS(node.left), IsBalancedDFS(node.right));
        }


        [Test]
        public void MinDepth()
        {
            var t = new TreeNode(1)
            {
                left = new TreeNode(2),
                //right = new TreeNode(20)
                //{
                //    right = new TreeNode(15),
                //    left = new TreeNode(17)
                //}
            };

            var r = MinDepth(t);
        }
        public int MinDepth(TreeNode root)
        {
            if (root == null)
            {
                return 0;
            }

            //Take the shortest
            if (root.right != null && root.left != null)
            {

                return 1 + Math.Min(MinDepth(root.left), MinDepth(root.right));
            }

            //Only one route.
            return 1 + Math.Max(MinDepth(root.left), MinDepth(root.right));
        }


        public bool HasPathSum(TreeNode root, int sum)
        {
            if (root == null)
            {
                return false;
            }

            if (root.left == null && root.right == null)
            {
                return root.val == sum;
            }

            return HasPathSum(root.right, sum - root.val) || HasPathSum(root.left, sum - root.val);
        }


        [Test]
        public void IsPalindrome()
        {
            var s = "a";
            var r = IsPalindrome(s);
        }
        public bool IsPalindrome(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return true;
            }

            var rgx = new Regex("[^a-zA-Z0-9]");
            s = rgx.Replace(s, "").ToLower();

            var left = 0;
            var right = s.Length - 1;
            while (left < right)
            {
                if (s[left] != s[right])
                {
                    return false;
                }

                left++;
                right--;
            }

            return true;
        }

        [Test]
        public void LongestPalindrome()
        {
            var s = "aaa";
            var r = LongestPalindromeDP(s);
        }
        public string LongestPalindrome(string s)
        {
            var result = "";
            if (string.IsNullOrEmpty(s))
            {
                return result;
            }


            for (var i = 0; i < s.Length; i++)
            {
                for (var j = s.Length -1; j >= i; j--) {
                    var left = i;
                    var right = j;
                    var found = true;
                    while (left < right)
                    {
                        if (s[left] != s[right])
                        {
                            found = false;
                        }

                        left++;
                        right--;
                    }

                    if (found && result.Length < (j - i) +1)
                    {
                        result = s.Substring(i, (j - i) + 1);
                    }
                }
            }

            return result;
        }

        public string LongestPalindromeDP(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return "";
            }

            var n = s.Length;
            var table = new bool[n, n];

            // All substrings of length 1 are palindromes 
            var maxLength = 1;
            for (var i = 0; i < n; i++)
            {
                table[i, i] = true;
            }

            // check for sub-string of length 2. 
            var start = 0;
            for (var i = 0; i < n - 1; i++) {
                if (s[i] == s[i + 1])
                {
                    table[i, i+1] = true;
                    start = i;
                    maxLength = 2;
                }
            }
            // Check for lengths greater than 2. k is length 
            // of substring 
            for (var k = 3; k <= n; k++) {
                for (int i = 0; i < n - k + 1; ++i)
                {
                    // Get the ending index of substring from starting index i and length k 
                    var j = i + k - 1;
                    if (table[i + 1,j - 1] && s[i] == s[j])
                    {
                        table[i,j] = true;

                        if (k > maxLength)
                        {
                            start = i;
                            maxLength = k;
                        }
                    }
                }
            }

            return s.Substring(start, maxLength);
        }
    }
}
