using System;
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

        [Test]
        public void ThreeSum()
        {
            var n = new int[] {-1, 0, 1, 2, -1, -4};

            var r = ThreeSum(n);
        }
        public IList<IList<int>> ThreeSum(int[] nums)
        {
            var result = new List<IList<int>>();
            if (nums == null || nums.Length < 3)
            {
                return result;
            }
            Array.Sort(nums);

            for (var i = 0; i < nums.Length; i++) {
                if (nums[i] > 0)
                {
                    return result;
                }

                if (i > 0 && nums[i] == nums[i-1])
                {
                    continue;
                }

                var firstNum = nums[i];
                var left = i + 1;
                var right = nums.Length - 1;
                while (left < right)
                {
                    var secondNum = nums[left];
                    var thirdNum = nums[right];
                    var currSum = firstNum + secondNum + thirdNum;
                    if (currSum == 0)
                    {
                        result.Add(new List<int> {firstNum, secondNum, thirdNum});
                        left++;
                        right--;
                        //skip duplicates
                        while (left < right && nums[left] == nums[left-1])
                        {
                            left++;
                        }
                        while (right > left && nums[right] == nums[right+1])
                        {
                            right--;
                        }
                    }
                    else if (currSum < 0)
                    {
                        left++;
                    }
                    else
                    {
                        right--;
                    }
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

        [Test]
        public void FourSum()
        {
            var n = new[] {0,0,0,0};
            var t = 0;

            var r = FourSum(n, t);
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

        [Test]
        public void FindMedianSortedArrays()
        {
            var a = new[] {1, 2 ,3 ,5 ,6};
            var b = new[] {2, 2, 3};

            var r = FindMedianSortedArrays(a, b);
        }
        public double FindMedianSortedArrays(int[] nums1, int[] nums2)
        {
            if (nums1.Length > nums2.Length)
            {
                return FindMedianSortedArrays(nums2, nums1);
            }

            var x = nums1.Length;
            var y = nums2.Length;

            var low = 0;
            var high = x;
            while (low <= high)
            {
                // No using length - 1 to make it work for both odd and even.
                var partitionX = (low + high) / 2;

                // Added plus one to make it work for both odd and even.
                var partitionY = (x + y + 1) / 2 - partitionX;

                // if partition x has 0 items, use -inf.
                // if partition x = length of the input array, then there is nothing on the right side, set right x to be +inf.
                var maxLeftX = partitionX == 0 ? int.MinValue : nums1[partitionX - 1];
                var minRightX = partitionX == x ? int.MaxValue : nums1[partitionX];
                var maxLeftY = partitionY == 0 ? int.MinValue : nums2[partitionY - 1];
                var minRightY = partitionY == y ? int.MaxValue : nums2[partitionY];

                if (maxLeftX <= minRightY && maxLeftY < minRightX)
                {
                    // Paritioned two arrays at correct place.
                    // If even number, average Max left x left y and Min right x right y.
                    // Max left for odd number.
                    if ((x + y) % 2 == 0)
                    {
                        return ((double)Math.Max(maxLeftX, maxLeftY) + Math.Min(minRightY, minRightX)) / 2;
                    }

                    return (double)Math.Max(maxLeftX, maxLeftY);
                }

                if (maxLeftX > minRightY)
                {
                    // too many items in partion x
                    high = partitionX - 1;
                }
                else
                {
                    // too less items in partion x
                    low = partitionX + 1;
                }
            }

            throw new Exception();
        }

        public int Rob(int[] nums)
        {

            //[1,10,3,1]
            var evenSum = 0;
            var oddSum = 0;

            for (var i = 0; i < nums.Length; i++)
            {
                if (i % 2 == 0)
                {
                    evenSum = Math.Max(evenSum + nums[i], oddSum);
                }
                else
                {
                    oddSum = Math.Max(oddSum + nums[i], evenSum);
                }

                Console.WriteLine("i " + i + " nums[i] " + nums[i] + " Even Sum " + evenSum + " Odd Sum " + oddSum);
            }

            return Math.Max(evenSum, oddSum);
        }

        [Test]
        public void IsMatch()
        {
            var s = "a";
            var p = "a*";
            var currString = "";
            var t = currString[currString.Length];
            var r = IsMatch(s, p);
        }

        public bool IsMatch(string s, string p)
        {
            var dp = new bool[s.Length + 1, p.Length + 1];

           
            dp[0, 0] = true;
            // Check p[1] to p[n] see if match to s[0] due to case such as a*, a*b* or a*b*c*..
            for (var i = 1; i < dp.GetLength(1);i++) {
                if (p[i-1] == '*')
                {
                    dp[0, i] = dp[0, i - 2];
                }
            }

            for (var i = 1; i < dp.GetLength(0); i++)
            {
                for (var j = 1; j < dp.GetLength(1); j++)
                {
                    // pattern and string are 0 based index.
                    if (p[j - 1] == s[i - 1] || p[j - 1] == '.')
                    {
                        dp[i, j] = dp[i - 1, j - 1];
                    }
                    else if (p[j-1] == '*')
                    {
                        dp[i, j] = dp[i, j - 2];
                        if (!dp[i, j])
                        {
                            dp[i, j] = (p[j - 2] == '.' || p[j - 2] == s[i - 1]) && dp[i - 1, j];
                        }
                    }
                    else
                    {
                        dp[i, j] = false;
                    }
                }
            }

            return dp[s.Length, p.Length];
        }

        [Test]
        public void LetterCombinationsDFS()
        {
            var d = "23";
            var r = LetterCombinations(d);
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

        [Test]
        public void ValidMountainArray()
        {
            var n = new[] { 3,5,5};
            var r = ValidMountainArray(n);
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

    }
}

