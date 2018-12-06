using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace LeetCode
{
    public class MathProblemSet
    {
        [Test]
        public void Multiply()
        {
            var num1 = "123";
            var num2 = "45";

            var r = Multiply(num1, num2);
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

            for (var i = n1Digits-1; i>= 0; i--) {
                for (var j = n2Digits-1; j >=0;j--)
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

        [Test]
        public void Rotate()
        {
            var m = new[,]
            {
                {
                    1,2,3
                },
                {
                    4,5,6
                },
                {
                    7,8,9
                }
            };

            Rotate(m);
        }


        public void Rotate(int[,] matrix)
        {
            for (var i = 0; i < matrix.GetLength(0); i++)
            {
                for (var j = i; j < matrix.GetLength(1);j++)
                {
                    var temp = matrix[i, j];
                    matrix[i, j] = matrix[j, i];
                    matrix[j, i] = temp;
                }
            }

            for (var i = 0; i < matrix.GetLength(0); i++)
            {
                for (var j = 0; j < matrix.GetLength(1)/2; j++)
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
            var s = new[] {"eat", "tea", "tan", "ate", "nat", "bat"};

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
                    map.Add(sorted, new List<string> {s});
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
            var n = new[] {1, 2, 3};
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
            for (var j = i; j<nums.Length;j++)
            {
                curr.Add(nums[j]);
                SubsetsHelper(nums, result, curr, j + 1);
                curr.RemoveAt(curr.Count - 1);
            }
        }

        [Test]
        public void SubsetsWithDup()
        {
            var n = new int[] {1, 2, 2};

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
    }
}
