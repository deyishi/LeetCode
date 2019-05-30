using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using LeetCode.DataModel;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace LeetCode
{
    /// <summary>
    /// Start date 11/07/2018.
    /// End date  11/10/2018.
    /// Count 15.
    /// </summary>
    public class PracticeSet1
    {
        public int NumJewelsInStones(string J, string S)
        {
            var jewelsDict = J.ToCharArray();
            var count = 0;
            foreach (var c in S)
            {
                if (jewelsDict.Contains(c))
                {
                    count++;
                }
            }

            return count;
        }

        [Test]
        public void MaxIncreaseKeepingSkyline()
        {
            var a = new int[4][];
            a[0] = new[] { 3, 0, 8, 4 };
            a[1] = new[] { 2, 4, 5, 7 };
            a[2] = new[] { 9, 2, 6, 3 };
            a[3] = new[] { 0, 3, 1, 0 };
            var t = MaxIncreaseKeepingSkyline(a);
        }

        public int MaxIncreaseKeepingSkyline(int[][] grid)
        {
            if (grid?[0] == null)
            {
                return 0;
            }

            var rowMax = new int[grid[0].Length];
            var colMax = new int[grid.Length];

            for (var i = 0; i < grid.Length; i++)
            {
                rowMax[i] = grid[i].Max();
                for (int j = 0; j < grid[i].Length; j++)
                {
                    colMax[i] = Math.Max(colMax[i], grid[j][i]);
                }
            }

            var sum = 0;
            for (var i = 0; i < grid.Length; i++)
            {
                for (int j = 0; j < grid[i].Length; j++)
                {
                    var maxHeight = Math.Min(rowMax[i], colMax[j]);
                    sum += (maxHeight - grid[i][j]);
                }
            }

            return sum;
        }

        [Test]
        public void NumUniqueEmails()
        {
            var t = new[]
                {"test.email+alex@leetcode.com", "test.e.mail+bob.cathy@leetcode.com", "testemail+david@lee.tcode.com"};

            var r = NumUniqueEmails(t);
        }

        public int NumUniqueEmails(string[] emails)
        {
            if (emails?.Length < 2)
            {
                return emails.Length;
            }

            var dict = new HashSet<string>();
            foreach (var email in emails)
            {
                var split = email.Split('@');

                // Get local name
                var local = "";
                foreach (var c in split[0])
                {
                    if (c.Equals('+'))
                    {
                        break;
                    }

                    if (!c.Equals('.'))
                    {
                        local += c;
                    }
                }

                // Get full name
                var name = local + split[1];
                if (!dict.Contains(name))
                {
                    dict.Add(name);
                }

            }

            return dict.Count;
        }


        [Test]
        public void AreAnagram()
        {
            var a = "xabcd";
            var b = "cbadx";

            var r = AreAnagram(a, b);
        }

        public int[] AreAnagram(string s1, string s2)
        {
            var result = new List<int>();

            if (s1.Length != s2.Length)
            {
                return null;
            }

            if (string.Equals(s1, s2))
            {
                for (var i = 0; i < s2.Length; i++)
                {
                    result.Add(i);
                }

                return result.ToArray();
            }

            var map = new Dictionary<char, List<int>>();

            var count = 0;
            foreach (var c in s2)
            {
                if (map.ContainsKey(c))
                {
                    map[c].Add(count);
                }
                else
                {
                    map.Add(c, new List<int> {count});
                }

                count++;

            }


            foreach (var t in s1)
            {
                if (map.ContainsKey(t))
                {
                    result.Add(map[t][0]);
                    map[t].RemoveAt(0);
                    if (!map[t].Any())
                    {
                        map.Remove(t);
                    }
                }
            }

            return map.Any() ? new int[0] : result.ToArray();
        }

        public string ToLowerCase(string str)
        {
            var result = "";
            foreach (var c in str)
            {
                result += char.ToLower(c);
            }

            return result;
        }

        [Test]
        public void Generate()
        {
            var t = Generate(5);
        }

        public IList<IList<int>> Generate(int numRows)
        {
            var result = new List<IList<int>>();
            if (numRows < 1)
            {
                return result;
            }

            var row = new List<int>();

            for (var i = 0; i< numRows;i++)
            {
                row.Insert(0, 1);
                // 1 to row count -1 is the one row above
                // count - 1 since we don't need to change the last digit 
                for (var j  = 1; j < row.Count - 1; j++)
                {
                    row[j] = row[j] + row[j + 1];
                }
               
                result.Add(new List<int>(row));
            }

            return result;
        }



        [Test]
        public void GetRow()
        {
            var t = GetRow(3);
        }

        public IList<int> GetRow(int numRows)
        {
            var result = new List<IList<int>>();
            if (numRows < 1)
            {
                return new List<int> {1};
            }

            var row = new List<int>();

            for (var i = 0; i <= numRows; i++)
            {
                row.Insert(0, 1);
                // 1 to row count -1 is the one row above
                // count - 1 since we don't need to change the last digit 
                for (var j = 1; j < row.Count - 1; j++)
                {
                    row[j] = row[j] + row[j + 1];
                }

                result.Add(new List<int>(row));
            }

            return result[numRows];
        }

        public int MissingNumber(int[] nums)
        {
            if (nums == null || nums.Length < 1)
            {
                return 0;
            }

            for (var i = 0; i < nums.Length; i++) {
                while (nums[i] != i && nums[i] != nums.Length)
                {
                    var swapIndex = Math.Min(nums[i], nums.Length - 1);
                    var temp = nums[swapIndex];
                    nums[swapIndex] = nums[i];
                    nums[i] = temp;
                }
            }

            for (var i = 0; i < nums.Length; i++)
            {
                if (nums[i] != i)
                {
                    return i;
                }
            }

            return nums.Length;
        }

        public int MissingNumberHash(int[] nums)
        {
            if (nums == null || nums.Length < 1)
            {
                return 0;
            }

            var map = new HashSet<int>();
            foreach (var t in nums)
            {
                map.Add(t);
            }

            for (var i = 0; i < nums.Length; i++)
            {
                if (!map.Contains(i))
                {
                    return i;
                }
            }

            return nums.Length;

        }

        [Test]
        public void ThirdMax()
        {
            var t = new int[] {2, 2, 3, 1};
            var r = ThirdMax(t);
        }

        public int ThirdMax(int[] nums)
        {
            if (nums.Length < 3)
            {
                return nums.Max();
            }

            var maxNums = new int?[3];

            foreach (var t in nums)
            {
                if (!maxNums.Contains(t))
                {

                    if (maxNums[2] == null ||t > maxNums[2])
                    {
                        maxNums[0] = maxNums[1];
                        maxNums[1] = maxNums[2];
                        maxNums[2] = t;
                    }else if (maxNums[1] == null || t > maxNums[1])
                    {
                        maxNums[0] = maxNums[1];
                        maxNums[1] = t;
                    }else if (maxNums[0] == null || t > maxNums[0])
                    {
                        maxNums[0] = t;
                    }
                }
            }

            return maxNums[0] ?? maxNums[2].Value;

        }

        public TreeNode Mirror(TreeNode root)
        {
             ReverseTree(root);

            return root;
        }

        public void ReverseTree(TreeNode node)
        {
            if (node == null)
            {
                return;
            }

            var temp = node.right;
            node.right = node.left;
            node.left = temp;

            if (node.left != null)
            {
                ReverseTree(node.left);
            }

            if (node.right != null)
            {
                ReverseTree(node.right);
            }
        }


        [Test]
        public void LengthOfLongestSubstring()
        {
            var s = "bbbbb";
            var r = LengthOfLongestSubstring(s);
        }

        public int LengthOfLongestSubstring(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return 0;
            }

            int[] charMap = new int[256];
            int j = 0;
            int result = int.MinValue;
            for (int i = 0; i < s.Length; i++)
            {

                while (j < s.Length && charMap[s[j]] < 1)
                {
                    charMap[s[j]]++;
                    j++;
                }

                result = Math.Max(result, j - i);
                // Move to next window, clear current window's first char.
                charMap[s[i]]--;

            }

            return result;
        }

        [Test]
        public void ReorderLogFiles()
        {
            var s = new[] {"a1 9 2 3 1", "g1 act car", "zo4 4 7", "ab1 off key dog", "a8 act zoo"};
            var r = ReorderLogFiles(s);

        }

        public string[] ReorderLogFiles(string[] logs)
        {
            if (logs == null || logs.Length < 1)
            {
                return logs;
            }

            var letterLog = new List<string>();
            var numLog = new List<string>();
            foreach (var s in logs) {
                if (IsNumLog(s.Split(' ')[1]))
                {
                    numLog.Add(s);
                }
                else
                {
                    letterLog.Add(s);
                }
            }

            var sort = letterLog.OrderBy(s => s.Substring(s.IndexOf(' ') + 1, s.Length - (s.IndexOf(' ') + 1))).ToList();
            sort.AddRange(numLog);

            return sort.ToArray();
        }

        bool IsNumLog(string str)
        {
            foreach (var c in str)
            {
                if (c < '0' || c > '9')
                    return false;
            }

            return true;
        }

        [Test]
        public void RangeSumBST()
        {
            var t = new TreeNode(10)
            {
                left = new TreeNode(15),
                right = new TreeNode(5)
            };

            var r = RangeSumBST(t, 7,15);
        }

        public int RangeSumBST(TreeNode root, int L, int R)
        {
            var result = 0;
            Helper(root, ref result, L, R);
            return result;
        }

        public void Helper(TreeNode root, ref int sum, int l, int r)
        {
            if (root == null)
            {
                return;
            }

            if (root.val >= l && root.val <= r)
            {
                sum += root.val;
            }
            Helper(root.left, ref sum, l, r);
            Helper(root.right, ref sum, l,r);
        }
      
    }
}
