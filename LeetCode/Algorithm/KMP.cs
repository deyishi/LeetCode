using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace LeetCode.Algorithm
{
    public class KMP
    {
        [Test]
        public void Test()
        {
            var a = "hello";
            var b = "ll";
            var r = KMPSearch(a, b);
        }

        public int KMPSearch(string target, string pattern)
        {
            var result = new List<int>();
            if (target.Length < pattern.Length)
            {
                return -1;
            }

            if (string.IsNullOrEmpty(pattern))
            {
                return 0;
            }

            int m = target.Length;
            int n = pattern.Length;
            int i = 0;
            int j = 0;
            int[] lps = FindLPS(pattern);
            while (i < m)
            {
                if (target[i] == pattern[j])
                {
                    i++;
                    j++;
                }

                if (j == n)
                {
                    result.Add(i - j);
                    j = lps[j - 1];
                }
                else if(i < m && target[i] != pattern[j])
                {
                    if (lps[j] != 0)
                    {
                        j = lps[j - 1];
                    }
                    else
                    {
                        i++;
                    }
                }
            }

            return result.Any() ? result.First() : -1;
        }

        /// <summary>
        /// Find longest common prefix and suffix.
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public int[] FindLPS(string p)
        {
            int[] lps = new int[p.Length];
            lps[0] = 0;
            int len = 0;
            int i = 1;
            int m = p.Length;
            while (i < m)
            {
                if (p[i] == p[len])
                {
                    len++;
                    lps[i] = len;
                    i++;
                }
                else
                {
                    if (len != 0)
                    {
                        len = lps[len - 1];
                    }
                    else
                    {
                        lps[i] = 0;
                        i++;
                    }
                }
            }

            return lps;
        }
    }
}
