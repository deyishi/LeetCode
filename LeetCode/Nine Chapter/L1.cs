using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace LeetCode.Nine_Chapter
{
    public class L1
    {
        [Test]
        public void StrStr()
        {
            var h = "hello";
            var n = "ll";
            var r = StrStr(h, n);
        }

        public int StrStr(string haystack, string needle)
        {
            if (string.IsNullOrEmpty(haystack) || string.IsNullOrEmpty(needle))
            {
                return -1;
            }

            if (needle.Length > haystack.Length)
            {
                return -1;
            }

            for (var i = 0; i < haystack.Length - needle.Length + 1; i++)
            {
                int j;
                for (j = 0; j < needle.Length; j++)
                {
                    if (haystack[i + j] != needle[j])
                    {
                        break;
                    }
                }

                if (j == needle.Length)
                {
                    return i;
                }
            }

            return -1;
        }

        public int LongestPalindrome(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return 0;
            }

            if (s.Length == 1)
            {
                return 1;
            }

            var result = 0;
            var set = new HashSet<char>();
            foreach (var c in s)
            {
                if (!set.Add(c))
                {
                    result++;
                    set.Remove(c);
                }
            }

            return s.Length > result * 2 ? result * 2 + 1 : result * 2;
        }

        public bool IsPalindrome(string s)
        {
            if (string.IsNullOrEmpty(s) || s.Length == 1)
            {
                return true;
            }

            var l = 0;
            var r = s.Length - 1;
            while (l < r)
            {
                while (!char.IsLetterOrDigit(s[l]))
                {
                    l++;
                }

                while (!char.IsLetterOrDigit(s[r]))
                {
                    r--;
                }

                Console.WriteLine(l + " " + r);
                if (char.ToUpper(s[l]) == char.ToUpper(s[r]))
                {
                    l++;
                    r--;
                }
                else
                {
                    return false;
                }
            }

            return true;
        }

        [Test]
        public void LongestPalindromeSub()
        {
            int t = 2147483646;
            var a = new byte[t];
            //for (var i = 0; i < t; i++ )
            //{
            //    a[i] = i;
            //}

            long e = t + 1;
            var n = "abcd";
            LongestPalindromeSub(n);
        }

        public string LongestPalindromeSub(string s)
        {
            if (string.IsNullOrEmpty(s) || s.Length == 1)
            {
                return s;
            }

            // Find P(i,i)
            var maxLength = 1;
            var start = 0;
            var n = s.Length;
            var dp = new bool[n, n];
            for (var i = 0; i < n; i++)
            {
                dp[i, i] = true;
            }

            for (var i = 0; i < n - 1; i++)
            {
                if (s[i] == s[i + 1])
                {
                    dp[i, i + 1] = true;
                    maxLength = 2;
                    start = i;
                }
            }

            for (var i = 3; i <= n; i++)
            {
                // Populate dp for length of 3, dp[0,2] = (S0 == S2 and dp[1,1]), dp[1,3] = (S1 == S3 and dp[2,2])...
                // Populate dp for length of 4, dp[0,3] = (S0 == S3 and dp[1,2]), dp[1,4] = (S1 == S4 and dp[2,3])...
                // Populate dp for length of 5, dp[0,4] = (S0 == S4 and dp[1,3]), dp[1,5] = (S1 == S5 and dp[2,4])...
                for (var j = 0; j < n - i + 1; j++)
                {
                    var end = j + i - 1;
                    if (s[j] == s[end] && dp[j + 1, end - 1])
                    {
                        dp[j, end] = true;
                        if (i > maxLength)
                        {
                            maxLength = i;
                            start = j;
                        }
                    }
                }
            }

            return s.Substring(start, maxLength);
        }

        public int CountSubstrings(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return 0;
            }

            if (s.Length == 1)
            {
                return 1;
            }

            var n = s.Length;
            var dp = new bool[n, n];
            var count = n;
            for (var i = 0; i < n; i++)
            {
                dp[i, i] = true;
            }

            for (var i = 0; i < n - 1; i++)
            {
                if (s[i] == s[i + 1])
                {
                    dp[i, i + 1] = true;
                    count++;
                }
            }

            for (var l = 3; l <= n; l++)
            {
                for (var i = 0; i < n - l + 1; i++)
                {
                    var end = i + l - 1;
                    if (s[i] == s[end] && dp[i + 1, end - 1])
                    {
                        count++;
                        dp[i, end] = true;
                    }
                }
            }

            return count;
        }

        public bool CanPermutePalindrome(string s)
        {
            if (string.IsNullOrEmpty(s) || s.Length == 1)
            {
                return true;
            }

            var set = new HashSet<char>();
            var count = 0;
            foreach (var c in s)
            {
                if (!set.Add(c))
                {
                    count++;
                    set.Remove(c);
                }
            }

            return s.Length - count * 2 <= 1;
        }

        public int LongestPalindromeSubseq(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return 0;
            }

            if (s.Length == 1)
            {
                return 1;
            }

            //Find P(1)
            var maxLength = 1;
            var n = s.Length;

            var dp = new int[n, n];
            for (var i = 0; i < n; i++)
            {
                dp[i, i] = 1;
            }

            for (var i = 0; i < n - 1; i++)
            {
                if (s[i] == s[i + 1])
                {
                    dp[i, i + 1] = 2;
                    maxLength = 2;
                }
                else
                {
                    dp[i, i + 1] = 1;
                }
            }

            for (var l = 3; l <= n; l++)
            {
                for (var i = 0; i < n - l + 1; i++)
                {
                    var end = i + l - 1;
                    if (s[i] == s[end])
                    {
                        dp[i, end] = dp[i + 1, end - 1] + 2;

                        if (dp[i, end] > maxLength)
                        {
                            maxLength = dp[i, end];
                        }
                    }
                    else
                    {
                        dp[i, end] = Math.Max(dp[i, end - 1], dp[i + 1, end]);
                    }
                }
            }

            return maxLength;
        }

        [Test]
        public void MinSubArrayLen()
        {

            var s = 7;
            var nums = new[] {2, 3, 1, 2, 4, 3};
            var r = MinSubArrayLen(s, nums);
        }

        public int MinSubArrayLen(int s, int[] nums)
        {
            if (nums == null || nums.Length == 0)
            {
                return 0;
            }

            var result = int.MaxValue;
            var sum = 0;
            var j = 0;
            for (var i = 0; i < nums.Length; i++)
            {
                while (sum < s && j < nums.Length)
                {
                    sum += nums[j];
                    j++;
                }
                if (sum >= s && j - i < result)
                {
                    result = j - i;
                }
                sum -= nums[i];
            }

            if (result == int.MaxValue)
            {
                return 0;
            }

            return result;
        }



        //public int KthSmallest(int[,] matrix, int k)
        //{
        //    var dx = new[] {0, 1};
        //    var dy = new[] {1, 0};

        //    var n = matrix.GetLength(0);
        //    var m = matrix.GetLength(1);


        //}
    }
}
