using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace LeetCode
{
    public class RecursionSet
    {

        [Test]
        public void Test()
        {
            var t = "2-1-1";
            var r = DiffWaysToCompute(t);
        }

        public HashSet<string> FindAllArrayCombination(string[] array)
        {
            var result = new HashSet<string>();
            if (array == null || array.Length == 0)
            {
                return result;
            }

            if (array.Length == 1)
            {
                result.Add(array[0]);
                return result;
            }

            Permute(array, 0, array.Length - 1, result);

            return result;
        }

        static void Permute(string[] arry, int i, int n, HashSet<string> result)
        {
            if (i == n)
                result.Add(string.Join("", arry));
            else
            {
                for (var j = i; j <= n; j++)
                {
                    Swap(ref arry[i], ref arry[j]);
                    Permute(arry, i + 1, n, result);
                    Swap(ref arry[i], ref arry[j]); //backtrack
                }
            }
        }

        static void Swap(ref string a, ref string b)
        {
            string tmp;
            tmp = a;
            a = b;
            b = tmp;
        }

        private Dictionary<string, List<int>> dpDictionary = new Dictionary<string, List<int>>();
        public IList<int> DiffWaysToCompute(string input)
        {
            if (dpDictionary.ContainsKey(input))
            {
                return dpDictionary[input];
            }

            var result = new List<int>();

            for (int i = 0; i < input.Length; i++)
            {
                char c = input[i];
                if (c == '-' || c=='+' || c=='*')
                {
                    var left = DiffWaysToCompute(input.Substring(0, i));
                    var right = DiffWaysToCompute(input.Substring(i + 1, input.Length - (i + 1)));

                    foreach (var l in left)
                    {
                        foreach (var r in right)
                        {
                            if (c == '-')
                            {
                                result.Add(l - r);
                            }
                            else if (c == '+')
                            {
                                result.Add(l + r);
                            }
                            else
                            {
                                result.Add(l * r);
                            }
                        }
                    }
                }
            }

            if (result.Count == 0)
            {
                result.Add(int.Parse(input));
            }
            dpDictionary.Add(input,result);

            return result;
        }
    }
}
