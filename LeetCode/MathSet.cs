using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeetCode.DataModel;
using NUnit.Framework;

namespace LeetCode
{
    public class MathSet
    {
        [Test]
        public void Test()
        {
            var a = new int[,] {{1, 2, 3}, {4, 5, 6}, {7, 8, 9}};
            Rotate(a);
        }

        public string Multiply(string num1, string num2)
        {
            if (string.IsNullOrEmpty(num1) || string.IsNullOrEmpty(num2))
            {
                return null;
            }

            var n1Digits = num1.Length;
            var n2Digits = num2.Length;
            var resultDigits = n1Digits + n2Digits;

            var result = new int[resultDigits];

            for (var i = n1Digits - 1; i >= 0; i--)
            {
                for (var j = n2Digits - 1; j >= 0; j--)
                {
                    var product = (num1[i] - '0') * (num2[j] - '0');

                    var carryDigit = i + j;
                    var currentDigit = i + j + 1;
                    var sum = product + result[currentDigit];
                    result[carryDigit] += sum / 10;
                    result[currentDigit] = sum % 10;
                }
            }

            var sb = new StringBuilder();
            foreach (var p in result)
            {
                if (sb.Length != 0 || p != 0)
                {
                    sb.Append(p);
                }

            }

            return sb.Length == 0 ? "0" : sb.ToString();
        }

        public void Rotate(int[,] matrix)
        {

            // Reverse left to right
            for (var i = 0; i < matrix.GetLength(0); i++)
            {
                for (var j = i; j < matrix.GetLength(1); j++)
                {
                    var temp = matrix[i, j];
                    matrix[i, j] = matrix[j, i];
                    matrix[j, i] = temp;
                }
            }

            for (var i = 0; i < matrix.GetLength(0); i++)
            {
                for (var j = 0; j < matrix.GetLength(1) / 2; j++)
                {
                    var temp = matrix[i, j];
                    matrix[i, j] = matrix[i, matrix.GetLength(1) - 1 - j];
                    matrix[i, matrix.GetLength(1) - 1 - j] = temp;
                }
            }
        }

        [Test]
        public void GroupAnagrams()
        {
            var s = new[] { "eat", "tea", "tan", "ate", "nat", "bat" };

            var r = GroupAnagrams(s);
        }
        public IList<IList<string>> GroupAnagrams(string[] strs)
        {
            var result = new List<IList<string>>();

            if (strs == null || strs.Length < 1)
            {
                return result;
            }

            var map = new Dictionary<string, List<string>>();

            foreach (var s in strs)
            {
                var a = s.ToCharArray();
                Array.Sort(a);
                var sorted = new string(a);
                if (map.ContainsKey(sorted))
                {
                    map[sorted].Add(s);
                }
                else
                {
                    map.Add(sorted, new List<string> { s });
                }
            }

            result.AddRange(map.Values);
            return result;
        }

        [Test]
        public void MyPow()
        {
            var x = 2.00000;
            var n = 4;

            var r = MyPow(x, n);
        }
        public double MyPow(double x, int n)
        {
            if (n == 0)
            {
                return 1;
            }

            if (n == 1)
            {
                return x;
            }

            if (x > 0 && x > double.MaxValue / x)
            {
                return 0;
            }

            if (n % 2 == 0)
            {
                return MyPow(x * x, n / 2);
            }

            return x * MyPow(x * x, n / 2);

        }

        [Test]
        public void Subsets()
        {
            var n = new[] { 1, 2, 3 };
            var r = Subsets(n);
        }

        public IList<IList<int>> Subsets(int[] nums)
        {
            var result = new List<IList<int>>();
            if (nums == null || nums.Length < 1)
            {
                return result;
            }

            SubsetsHelper(nums, result, new List<int>(), 0);
            return result;
        }

        private void SubsetsHelper(int[] nums, List<IList<int>> result, List<int> curr, int i)
        {
            result.Add(new List<int>(curr));
            for (var j = i; j < nums.Length; j++)
            {
                curr.Add(nums[j]);
                SubsetsHelper(nums, result, curr, j + 1);
                curr.RemoveAt(curr.Count - 1);
            }
        }

        [Test]
        public void SubsetsWithDup()
        {
            var n = new int[] { 1, 2, 2 };

            var r = SubsetsWithDup(n);

        }
        public IList<IList<int>> SubsetsWithDup(int[] nums)
        {
            var result = new List<IList<int>>();
            if (nums == null)
            {
                return result;
            }

            Array.Sort(nums);
            SubsetsWithDupHelper(nums, result, new List<int>(), 0);

            return result;
        }

        public void SubsetsWithDupHelper(int[] nums, List<IList<int>> result, List<int> curr, int start)
        {
            result.Add(new List<int>(curr));
            for (var i = start; i < nums.Length; i++)
            {
                if (i > start && nums[i] == nums[i - 1])
                {
                    continue;
                }
                curr.Add(nums[i]);
                SubsetsWithDupHelper(nums, result, curr, i + 1);
                curr.RemoveAt(curr.Count - 1);
            }
        }

        public IList<string> LetterCasePermutation(string s)
        {
            var result = new List<string>();

            if (s == null)
            {
                return result;
            }

            LetterCasePermutationHelper(s, result, "", 0);
            return result;
        }

        private void LetterCasePermutationHelper(string s, List<string> result, string curr, int index)
        {
            if (index == s.Length)
            {
                result.Add(curr);
                return;
            }

            if (Char.IsDigit(curr[index]))
            {
                LetterCasePermutationHelper(s, result, curr + curr[index], index + 1);
            }
            else
            {
                LetterCasePermutationHelper(s, result, curr + Char.ToUpper(curr[index]), index + 1);
                LetterCasePermutationHelper(s, result, curr + Char.ToLower(curr[index]), index + 1);
            }
        }

        public string FractionToDecimal(int numerator, int denominator)
        {
            if (numerator == 0)
            {
                return "0";
            }

            if (denominator == 0)
            {
                return "NaN";
            }

            var res = new StringBuilder();

            //Handle negative
            res.Append((numerator < 0) ^ (denominator < 0) ? "-" : "");

            //Handle overflow
            long num = Math.Abs(numerator);
            long den = Math.Abs(denominator);

            res.Append(num / den);
            num %= den;
            // No fractional  
            if (num == 0)
            {
                return res.ToString();
            }

            res.Append(".");

            var map = new Dictionary<long, int> { { num, res.Length } };

            while (num != 0)
            {
                num *= 10;
                res.Append(num / den);
                num %= den;
                if (map.ContainsKey(num))
                {
                    var index = map[num];
                    res.Insert(index, "(");
                    res.Append(")");
                    return res.ToString();
                }

                map.Add(num, res.Length);
            }

            // No repeating decimal
            return res.ToString();
        }

        //223. Rectangle Area
        //Sum of two rectangles' area minutes overlapped region.
        //    Use long to avoid overflow.
        //    To calculate overlapped, we need to check bottom left corner and top righter corner of the overlapped area.
        //    Height: Max(A, E), Min(C, G)
        //With: Max(B, F) Min(D, H)
        //B---D  F---H
        //|   |  |   |
        //A---C  E---G
        public int ComputeArea(int A, int B, int C, int D, int E, int F, int G, int H)
        {
            long r1 = (C - A) * (D - B);
            long r2 = (G - E) * (H - F);

            long heightStart = Math.Max(A, E);
            long heightEnd = Math.Min(C, G);
            long h = heightEnd - heightStart;
            long widthStart = Math.Max(B, F);
            long widthEnd = Math.Min(D, H);
            long w = widthEnd - widthStart;
            long overlap = 0;
            // overlap
            if (h > 0 && w > 0)
            {
                overlap = h * w;
            }

            return (int)(r1 + r2 - overlap);
        }

        // Divisible by 2, 3, 5
        public bool IsUgly(int num)
        {

            if (num == 1) return true;
            if (num == 0) return false;
            while (num % 2 == 0) num = num / 2;
            while (num % 3 == 0) num = num / 3;
            while (num % 5 == 0) num = num / 5;
            return num == 1;
        }

        public int CountPrimes(int n)
        {
            var notPrime = new bool[n];

            var count = 0;
            for (var i = 2; i < n; i++)
            {
                if (!notPrime[i])
                {
                    count++;
                    //Remove all the numbers within n that are multiple of current number.
                    for (var j = 2; j * i < n; j++)
                    {
                        notPrime[i * j] = true;
                    }
                }
            }

            return count;
        }

        public bool IsStrobogrammatic(string num)
        {
            var i = 0;
            var j = num.Length - 1;
            while (i < num.Length && j >= 0)
            {
                if (!"00 11 88 696".Contains(num[i] + "" + num[j]))
                {
                    return false;
                }

                i++;
                j--;
            }

            return true;
        }

        public IList<string> FindStrobogrammatic(int n)
        {

            return FindStrobogrammaticHelper(n, n);
        }

        private IList<string> FindStrobogrammaticHelper(int n, int m)
        {
            // Even ending
            if (n == 0)
            {
                return new List<string> { "" };
            }

            // Odd ending
            if (n == 1)
            {
                return new List<string> { "0", "1", "8" };
            }

            var list = FindStrobogrammaticHelper(n - 2, m);

            var res = new List<string>();
            foreach (var s in list)
            {
                //When n == m, it is the last round of the DFS, prevent putting 0 as the start and end of the number.
                if (n != m)
                {
                    res.Add("0" + s + "0");
                }

                res.Add("1" + s + "1");
                res.Add("6" + s + "9");
                res.Add("8" + s + "8");
                res.Add("9" + s + "6");
            }

            return res;
        }

        public int NthUglyNumber(int n)
        {
            // DP[n]
            // The next ugly number has to be smallest number among all the existing ugly numbers times (2,3,5) that isn't already in the existing ugly numbers. 
            // DP[0] = 1, DP[1] = Min(2*(1), 3*(1), 5*(1)) = 2, DP[2] = Min(2*(1,2), 3*(1,2), 5*(1,2)) = 3, DP[2] = (2*(1,2,3), 3*(1,2,3), 5*(1,2,3))
            // Use three pointers to track where (2,3,5) are going to be multiplied to avoid duplicates or checking all existing numbers.

            int[] dp = new int[n];
            int p2 = 0, p3 = 0, p5 = 0;

            dp[0] = 1;
            for (var i = 1; i < n; i++)
            {
                dp[i] = Math.Min(dp[p2] * 2, Math.Min(dp[p3] * 3, dp[p5] * 5));

                if (dp[i] == dp[p2] * 2)
                {
                    p2++;
                }

                if (dp[i] == dp[p3] * 3)
                {
                    p3++;
                }

                if (dp[i] == dp[p5] * 5)
                {
                    p5++;
                }
            }

            return dp[n - 1];

        }

        //Same idea as Ugly Number 2.
        //Need an array to track how many times of each prime in primes list has been multiplied to skip duplicate.
        public int NthSuperUglyNumber(int n, int[] primes)
        {
            int[] dp = new int[n];

            dp[0] = 1;

            var tracker = new int[primes.Length];

            for (var i = 1; i < n; i++)
            {
                var min = int.MaxValue;
                for (int j = 0; j < primes.Length; j++)
                {
                    min = Math.Min(min, primes[j] * dp[tracker[j]]);
                }

                dp[i] = min;

                for (int j = 0; j < primes.Length; j++)
                {
                    if (dp[i] == primes[j] * dp[tracker[j]])
                    {
                        tracker[j]++;
                    }
                }

            }

            return dp[n - 1];
        }

        public int BulbSwitch(int n)
        {
            return (int)Math.Sqrt(n);
        }

        public int FlipLights(int n, int m)
        {
            if (m == 0) return 1;
            if (n == 1) return 2;
            if (n == 2 && m == 1) return 3;
            if (n == 2) return 4;
            if (m == 1) return 4;
            if (m == 2) return 7;
            if (m >= 3) return 8;
            return 8;
        }

        public int MaxPoints(Point[] points)
        {
            if (points == null || points.Length == 0)
            {
                return 0;
            }
            if (points.Length < 3)
            {
                return points.Length;
            }
            var max = 0;

            for (int i = 0; i < points.Length; i++)
            {
                for (int j = i + 1; j < points.Length; j++)
                {
                    bool slope = true;
                    long dx = points[i].x - points[j].x;
                    long dy = points[i].y - points[j].y;
                    long commonRatio = 0;
                    // Parallel to x, no slope. 
                    if (dx == 0)
                    {
                        slope = false;
                    }
                    else
                    {
                        commonRatio = dx * points[i].y - dy * points[i].x;
                    }

                    int count = 0;
                    for (var k = 0; k < points.Length; k++)
                    {
                        if (slope)
                        {
                            if (commonRatio == dx * points[k].y - dy * points[k].x)
                            {
                                count++;
                            }
                        }
                        else if (points[k].x == points[i].x)
                        {
                            count++;
                        }
                    }

                    max = Math.Max(count, max);
                }
            }
            return max;

        }
        public int MaxPointsTwo(Point[] points)
        {
            if (points == null)
            {
                return 0;
            }
            if (points.Length < 3)
            {
                return points.Length;
            }
            var max = 0;
            var slopePointsCount = new Dictionary<decimal, int>();

            for (int i = 0; i < points.Length; i++)
            {
                slopePointsCount.Clear();
                int sameX = 1;
                int duplicates = 0;
                for (int j = 0; j < points.Length; j++)
                {
                    if (j != i)
                    {
                        decimal dx = points[j].x - points[i].x;
                        decimal dy = points[j].y - points[i].y;
                        if (dx == 0 && dy == 0)
                        {
                            //Same points
                            duplicates++;
                        }

                        if (dx == 0)
                        {
                            sameX++;
                            continue;
                        }

                        decimal slope = dy / dx;

                        if (slopePointsCount.ContainsKey(slope))
                        {
                            slopePointsCount[slope]++;
                        }
                        else
                        {
                            slopePointsCount.Add(slope, 2);
                        }

                        max = Math.Max(max, slopePointsCount[slope] + duplicates);
                    }
                }

                max = Math.Max(max, sameX);
            }

            return max;

        }

        public int IntegerBreak(int n)
        {
            if (n == 2)
            {
                return 1;
            }

            if (n == 3)
            {
                return 2;

            }

            int product = 1;
            while (n > 4)
            {
                product *= 3;
                n -= 3;
            }

            return n * product;
        }

        public int CountDigitOne(int n)
        {
            long count = 0;
            for (long k = 1; k <= n; k *= 10)
            {
                long r = n / k;
                long m = n % k;
                // sum up the count of ones on every place k
                count += (r + 8) / 10 * k + (r % 10 == 1 ? m + 1 : 0);
            }

            return (int)count;
        }

        public int StrobogrammaticInRange(string low, string high)
        {
            if (low == null || high == null || low.Length > high.Length
                || (low.Length == high.Length && String.Compare(low, high, StringComparison.Ordinal) > 0))
            {
                return 0;
            }

            var pairs = new[,]
            {
                {'0', '0'}, {'1', '1'}, {'6', '9'}, {'8', '8'}, {'9', '6'}
            };

            int count = 0;

            for (int i = low.Length; i <= high.Length; i++)
            {
                count += StrobogrammaticDFS(long.Parse(low), long.Parse(high), new char[i], 0, i - 1, pairs);
            }

            return count;
        }


        private int StrobogrammaticDFS(long low, long high, char[] path, int left, int right, char[,] pairs)
        {
            // Base case when cannot add pairs left > right
            if (left > right)
            {
                //check if in range
                long num = long.Parse(new string(path));

                if (num < low || num > high)
                {
                    return 0;
                }
                return 1;
            }

            var count = 0;

            for (var i = 0; i < pairs.GetLength(0); i++)
            {
                // Form strobogrammatic number by appending the mid two number
                path[left] = pairs[i, 0];
                path[right] = pairs[i, 1];

                // cannot start 0
                if (path.Length != 1 && path[0] == '0')
                {
                    continue;
                }

                // odd digits number cannot have 6 or 9 in the mid. left == right will only happen for odd digits.
                if (left == right && (path[left] == '6' || path[left] == '9'))
                {
                    continue;
                }

                count += StrobogrammaticDFS(low, high, path, left + 1, right - 1, pairs);
            }

            return count;
        }

        public int FindNthDigit(int m)
        {
            long n = m; // convert int to long 
            long start = 1, len = 1, count = 9;

            while (n > len * count)
            {
                n = n - len * count;
                len++;
                count = count * 10;
                start = start * 10;
            }

            start = start + (n - 1) / len;
            String s = start.ToString();
            return s[(int) ((n - 1) % len)] - '0';
        }

        public bool IsReflected(int[,] points)
        {
            int max = int.MinValue;
            int min = int.MaxValue;
            HashSet<string> set = new HashSet<string>();

            for (int i = 0; i <  points.GetLength(0);i++)
            {
                max = Math.Max(max, points[i, 0]);
                min = Math.Min(min, points[i, 0]);

                set.Add(points[i, 0] + "A" + points[i, 1]);
            }

            var sum = max + min;
            for (int i = 0; i < points.GetLength(0); i++)
            {
                var str = sum - points[i, 0] + "A" + points[i, 1];
                if (!set.Contains(str))
                {
                    return false;
                }
            }

            return true;
        }

        public int[] SortTransformedArray(int[] nums, int a, int b, int c)
        {
            var res = new int[nums.Length];
            int l = 0;
            int r = nums.Length - 1;
            int index = a >= 0 ? nums.Length - 1 : 0;
            while (l <= r)
            {
                var lValue = CalQuad(nums[l], a, b, c);
                var rValue = CalQuad(nums[r], a, b, c);
                if (a >= 0)
                {
                    res[index--] = Math.Max(lValue, rValue);
                    if (lValue > rValue)
                    {
                        l++;
                    }
                    else
                    {
                        r--;
                    }
                }
                else
                {
                    res[index++] = Math.Min(lValue, rValue);
                    if (lValue > rValue)
                    {
                        r--;
                    }
                    else
                    {
                        l++;
                    }
                }
            }

            return res;
        }

        public int CalQuad(int x, int a, int b, int c)
        {
            return a * x * x + b * x + c;
        }

        public bool CanMeasureWater(int x, int y, int z)
        {
            //limit brought by the statement that water is finally in one or both buckets
            if (x + y < z) return false;
            //case x or y is zero
            if (x == z || y == z || x + y == z) return true;

            return z % GCD(x, y) == 0;
        }

        public int GCD(int a, int b)
        {
            while (b != 0)
            {
                int temp = b;
                b = a % b;
                a = temp;
            }
            return a;
        }
    }
}
