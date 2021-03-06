﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
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

        public bool IsSymmetric(TreeNode root)
        {
            if (root == null)
            {
                return true;
            }
        

            return IsSymmetric(root.left, root.right);
        }

        public bool IsSymmetric(TreeNode left, TreeNode right)
        {
            if (left == null && right==null)
            {
                return true;
            }

            if (left != null && right == null || left == null && right != null || left.val != right.val)
            {
                return false;
            }

            return IsSymmetric(left.left, right.right) && IsSymmetric(left.right, right.left);
        }


        [Test]
        public void RemoveKdigits()
        {
            var num = "9999999999991";
            var k = 8;
            var r = RemoveKdigits(num, k);
        }

        public string RemoveKdigits(string num, int k)
        {
            if (string.IsNullOrEmpty(num) || k == 0)
            {
                return num;
            }

            if (num.Length <= k)
            {
                return "0";
            }

            var newLength = num.Length - k;
            var result = "";

            foreach (var c in num)
            {
                while (k > 0 && result.Length > 0 && result[result.Length-1] > c)
                {
                    result = result.Substring(0, result.Length - 1);
                    k--;
                }

                result += c;
            }


   

            var leadingZeroCount = 0;
            while (leadingZeroCount < newLength && result[leadingZeroCount] == '0')
            {
                leadingZeroCount++;
            }


            return leadingZeroCount == newLength ? "0" : result.Substring(leadingZeroCount, newLength-leadingZeroCount);
        }

        [Test]
        public void Convert()
        {
            var s = "PAYPALISHIRING";
            var n = 3;

            var r = Convert(s, n);
        }
        public string Convert(string s, int numRows)
        {
            if (string.IsNullOrEmpty(s) || numRows <2)
            {
                return s;
            }

            var rows = new Dictionary<int, string>();
            var result = "";
            for (var i = 0; i < numRows; i++)
            {
                rows.Add(i, "");
            }

            var count = 0;
            var zigZagDown = false;
            foreach (var c in s)
            {
                if (count == numRows -1 || count == 0)
                {
                    zigZagDown = !zigZagDown;
                }

                if (zigZagDown)
                {
                    rows[count++] += c;
                }
                else
                {
                    rows[count--] += c;
                }

            }

            for (var i = 0; i < numRows; i++)
            {
                result += rows[i];
            }
            return result;
        }

        [Test]
        public void MyAtoi()
        {
            var c = "42";

            var r = MyAtoi(c);
        }


        /// <summary>
        /// Case: +, -, not valid, over limit
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public int MyAtoi(string str)
        {
            if (string.IsNullOrWhiteSpace(str))
            {
                return 0;
            }


            //Remove white space
            str = str.Trim();

            long result = 0;
            var firstC = str[0];
            var sign = 1;
            var start = 0;
            if (firstC == '-' && firstC != '+' && char.IsDigit(firstC))
            {
                return 0;
            }

            if (firstC == '-')
            {
                sign = -1;
                start++;
            }
            else if (firstC == '+')
            {
                start++;
            }

            for (var i = start; i < str.Length; i++) {
                if (!char.IsDigit(str[i]))
                {
                    return (int) result * sign;
                }

                result = result * 10 + str[i] - '0';

                if (result > int.MaxValue)
                {
                    return sign == -1 ? int.MinValue : int.MaxValue;
                }
            }


            return (int) result * sign;
        }

        [Test]
        public void MaxArea()
        {
            var h = new int[] {1, 8, 6, 2, 5, 4, 8, 3, 7};

            var r = MaxArea(h);
        }

        public int MaxAreaBF(int[] height)
        {
            if (height == null || height.Length < 2 )
            {
                return 0;
            }

            // Get all pairs
            var result = 0;
            for (var i = 0; i < height.Length - 1; i++)
            {
                var start = height[i];

                for (int j = i + 1; j < height.Length; j++)
                {
                    var end = height[j];

                    result = Math.Max(result, Math.Min(start, end) * (j - i));
                }
            }

            return result;
        }

        public int MaxArea(int[] height)
        {
            if (height == null || height.Length < 2)
            {
                return 0;
            }

            // Get all pairs
            var result = 0;
            var left = 0;
            var right = height.Length - 1;
            while (left < right)
            {
                var leftEdge = height[left];
                var rightEdge = height[right];
                var currArea = (right - left) * Math.Min(leftEdge, rightEdge);
                result = Math.Max(result, currArea);
                if (leftEdge > rightEdge)
                {
                    right--;
                }
                else if (leftEdge < rightEdge)
                {
                    left++;
                }
                else
                {
                    right--;
                    left++;
                }
            }

            return result;
        }

        public int ThreeSumClosest(int[] nums, int target)
        {

            Array.Sort(nums);
            var result = 0;
            var diff = int.MaxValue;
            for (var i = 0; i < nums.Length - 2; i++)
            {
                var firstNum = nums[i];
                var left = i + 1;
                var right = nums.Length - 1;
                while (left < right)
                {
                    var currSum = firstNum + nums[left] + nums[right];
                    var currDiff = Math.Abs(currSum - target);
                    if (currDiff < diff)
                    {
                        diff = currDiff;
                        result = currSum;
                    }

                    if (currSum < target)
                    {
                        left++;
                    }
                    else
                    {
                        right++;
                    }
                }

            }

            return result;
        }


        public IList<IList<int>> FourSum(int[] nums, int target)
        {
            var result = new List<IList<int>>();

            if (nums == null || nums.Length < 4)
            {
                return result;
            }
            Array.Sort(nums);
            for (var i = 0; i < nums.Length - 3; i++)
            {
                var firstNum = nums[i];
                if (i > 0 && firstNum == nums[i-1])
                {
                    continue;
                }

                for (var j = i+1; j < nums.Length-2;j++)
                {
                    var secondNum = nums[j];
                    if (j > 0 && secondNum == nums[j - 1])
                    {
                        continue;
                    }

                    var left = j + 1;
                    var right = nums.Length - 1;
                    while (left < right)
                    {
                        var total = firstNum + secondNum + nums[left] + nums[right];
                        if (total == target)
                        {
                            result.Add(new List<int> {firstNum, secondNum, nums[left], nums[right]});
                            left++;
                            right--;
                            while (left < right && nums[left] == nums[left-1])
                            {
                                left++;
                            }

                            while (right > left && nums[right] == nums[right+1])
                            {
                                right--;
                            }
                        }
                        else if (total > target)
                        {
                            right--;
                        }
                        else
                        {
                            left++;
                        }
                    }
                }
            }



            return result;
        }

        [Test]
        public void NextPermutation()
        {
            var n = new[] {1, 2, 3};

            NextPermutation(n);
        }

        public void NextPermutation(int[] nums)
        {
            if (nums == null || nums.Length < 1)
            {
                return;
            }

            var n = nums.Length;
            // VIO = 3
            var vioIndex = n - 1;
            while (vioIndex > 0)
            {
                if (nums[vioIndex - 1] < nums[vioIndex])
                {
                    break;
                }

                vioIndex--;
            }



            if (vioIndex > 0)
            {
                vioIndex--;
                var rightIndex = n - 1;
                while (rightIndex >= 0 && nums[rightIndex] <= nums[vioIndex])
                {
                    rightIndex--;
                }

                var temp = nums[vioIndex];
                nums[vioIndex] = nums[rightIndex];
                nums[rightIndex] = temp;
                vioIndex++;
            }

            var end = n - 1;
            while (end > vioIndex)
            {
                var temp = nums[vioIndex];
                nums[vioIndex] = nums[end];
                nums[end] = temp;
                end--;
                vioIndex++;
            }
        }


       

        public IList<string> LetterCombinations(string digits)
        {
            var result = new List<string>();

            if (string.IsNullOrEmpty(digits))
            {
                return result;
            }

            var dialMap = new Dictionary<char, string[]>
            {
                {'1', new string[0]},
                {'2', new[] {"a", "b", "c"}},
                {'3', new[] {"d", "e", "f"}},
                {'4', new[] {"g", "h", "i"}},
                {'5', new[] {"j", "k", "l"}},
                {'6', new[] {"m", "n", "o"}},
                {'7', new[] {"p", "q", "r", "s"}},
                {'8', new[] {"t", "u", "v"}},
                {'9', new[] {"w", "x", "y", "z"}},
                {'0', new[] {" "}},
            };
            var currString = "";

            LetterCombinationsDFS(digits, currString, dialMap, result);

            return result;
        }

        private void LetterCombinationsDFS(string digits, string currString, Dictionary<char, string[]> dialMap, List<string> result)
        {
            if (digits.Length == currString.Length)
            {
                result.Add(currString);
            }
            else
            {
                foreach (var c in dialMap[digits[currString.Length]])
                {
                    currString += c;
                    LetterCombinationsDFS(digits, currString, dialMap, result);
                    currString = currString.Remove(currString.Length - 1);
                }
            }
        }

        public bool ValidMountainArray(int[] A)
        {
            if (A == null || A.Length < 3)
            {
                return false;
            }

            var n = A.Length;
            var i = 0;
            // Find peak
            while (i < n -1 && A[i] < A[i + 1])
            {
                i++;
            }

            // i == 0 array is in descending
            // i == n - 1 array is ascending 
            if (i == 0 || i == n - 1)
            {
                return false;
            }

            // Star to peak and go down
            while (i + 1 < n && A[i] > A[i + 1])
            {
                i++;
            }

            // check if reached bottom from peak.
            return i == n - 1;
        }

        public int MinDeletionSize(string[] A)
        {
            var cols = A[0].Length;
            var rows = A.Length;
            var unsortedColCount = 0;
            for (var j = 0; j < cols; j++)
            {
                var i = 0;
                while (i + 1 < rows && A[i][j] <= A[i + 1][j])
                {
                    i++;
                }

                if (i != rows - 1)
                {
                    unsortedColCount++;
                }
            }

            return unsortedColCount;
        }

        public int[] DiStringMatch(string S)
        {
            var n = S.Length;
            var low = 0;
            var hight = n;
            var res = new int[n + 1];
            for (var i = 0; i < n; ++i)
            {
                if (S[i] == 'I')
                {
                    res[i] = low++;
                }
                else
                {
                    res[i] = hight--;
                }
            }

            res[n] = low;
            return res;
        }

    }
}

