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

        public IList<int> SpiralOrder(int[,] matrix)
        {
            //var res = new List<int>();
            //if (matrix.GetLength(0) == 0)
            //{
            //    return res;
            //}

            //var rowStart = 0;
            //var rowEnd = matrix.GetLength(0) -1;
            //var colStart = 0;
            //var colEnd = matrix.GetLength(1) - 1;

            //while (rowStart<=rowEnd || colStart<=colEnd)
            //{
            //    // Go right
            //    for (var i = colStart; i <= colEnd;i++) {
            //        res.Add(matrix[rowStart,i]);
            //    }

            //    rowStart++;

            //    //Go down
            //    for (int j = rowStart; j < rowEnd; j++)
            //    {
            //        res.Add(matrix[j,colEnd]);
            //    }
            //    rowEnd--;
            //    if (rowStart <= rowEnd)
            //    {
            //        for (int j = colEnd; j >= colEnd; j++)
            //        {
            //            res.Add(matrix[j, colEnd]);
            //        }
            //    }

            //}
        }
    }
}
