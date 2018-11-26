using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace LeetCode
{
    public class RecursionPractice
    {

        [Test]
        public void FindAllArrayCombination()
        {

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

            Permute(array, 0, array.Length -1, result);

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
    }
}
