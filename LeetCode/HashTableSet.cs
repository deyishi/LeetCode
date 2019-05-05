using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeetCode.DataModel;

namespace LeetCode
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using NUnit.Framework;

    namespace LeetCode
    {
        public class HashTableSet
        {
            [Test]
            public void Test()
            {
                string a = "aaabbbb";

                var r = FrequencySortWithHeap(a);
            }
            public IList<IList<string>> FindDuplicate(string[] paths)
            {
                var result = new List<IList<string>>();
                if (paths == null || paths.Length == 0)
                {
                    return result;
                }

                var files = new Dictionary<string, List<string>>();
                // Parse path by ' '
                // Use file content as key for a list of file path
                // Return keys with list length > 1
                //1.Imagine you are given a real file system, how will you search files? DFS or BFS ?
                //    In general, BFS will use more memory then DFS. However BFS can take advantage of the locality of files in inside directories, and therefore will probably be faster

                //2.If the file content is very large (GB level), how will you modify your solution?
                //    In a real life solution we will not hash the entire file content, since it's not practical. Instead we will first map all the files according to size. Files with different sizes are guaranteed to be different. We will than hash a small part of the files with equal sizes (using MD5 for example). Only if the md5 is the same, we will compare the files byte by byte

                //3.If you can only read the file by 1kb each time, how will you modify your solution?
                //    This won't change the solution. We can create the hash from the 1kb chunks, and then read the entire file if a full byte by byte comparison is required.

                //    What is the time complexity of your modified solution? What is the most time consuming part and memory consuming part of it? How to optimize?
                //    Time complexity is O(n ^ 2 * k) since in worse case we might need to compare every file to all others. k is the file size

                //    How to make sure the duplicated files you find are not false positive?
                //    We will use several filters to compare: File size, Hash and byte by byte comparisons.
                foreach (var path in paths)
                {
                    var pathFiles = path.Split(' ');
                    var dir = pathFiles[0] + '/';
                    for (var i = 1; i < pathFiles.Length; i++)
                    {
                        var index = pathFiles[i].IndexOf('(');
                        var fileContent = pathFiles[i].Substring(index);
                        var fileName = dir + pathFiles[i].Substring(0, index);
                        if (files.ContainsKey(fileContent))
                        {
                            files[fileContent].Add(fileName);
                        }
                        else
                        {
                            files.Add(fileContent, new List<string> { fileName });
                        }
                    }
                }

                foreach (var f in files)
                {
                    if (f.Value.Count > 1)
                    {
                        result.Add(f.Value);
                    }
                }

                return result;
            }

            public IList<string> FindRepeatedDnaSequences(string s)
            {
                var result = new HashSet<string>();
                var set = new HashSet<string>();
                for (var i = 0; i < s.Length - 9; i++)
                {
                    var sub = s.Substring(i, 10);
                    if (!set.Add(sub))
                    {
                        result.Add(sub);
                    }
                }

                return result.ToList();
            }
            public int[,] Multiply(int[,] a, int[,] b)
            {

                var aRows = a.GetLength(0);
                var aCols = a.GetLength(1);
                var bCols = b.GetLength(1);

                var r = new int[aRows, bCols];
                for (int i = 0; i < aRows; i++)
                {
                    for (int k = 0; k < aCols; k++)
                    {
                        if (a[i, k] != 0)
                        {
                            for (int j = 0; j < bCols; j++)
                            {
                                if (b[k, j] != 0) r[i, j] += a[i, k] * b[k, j];
                            }
                        }
                    }
                }

                return r;
            }

            public int MaxSubArrayLen(int[] nums, int k)
            {
                int sum = 0, max = 0;

                // Use a map to track sum at a index.
                Dictionary<int, int> map = new Dictionary<int, int>();


                // Use HashMap stores the sum of all elements before i as key, and i as value. For each i, check not only the current sum but also (currentSum - previousSu,) to see if there is any that equals k, and update max length.
                // [-2,-1,2,1]
                // At i = 3, current sum is 0. current sum 0  - previous sum 3 == K.
                for (var i = 0; i < nums.Length; i++)
                {
                    sum = sum + nums[i];
                    if (sum == k)
                    {
                        max = i + 1;
                    }
                    else if (map.ContainsKey(sum - k))
                    {
                        max = Math.Max(max, i - map[sum - k]);
                    }

                    if (!map.ContainsKey(sum))
                    {
                        map.Add(sum, i);
                    }
                }

                return max;
            }

            public bool IsAnagram(string s, string t)
            {
                int[] map = new int[256];
                foreach (var c in s)
                {
                    map[c]++;
                }

                foreach (var c in t)
                {
                    map[c]--;
                }

                foreach (var x in map)
                {
                    if (x != 0)
                    {
                        return false;
                    }
                }

                return true;
            }

            public string FrequencySort(string s)
            {
                if (string.IsNullOrEmpty(s))
                {
                    return "";
                }


                var map = new Dictionary<char, int>();
                foreach (var c in s)
                {
                    if (!map.ContainsKey(c))
                    {
                        map.Add(c, 0);
                    }

                    map[c]++;
                }

                List<char>[] bucket = new List<char>[s.Length + 1];
                foreach (var key in map.Keys)
                {
                    var v = map[key];
                    if (bucket[v] == null)
                    {
                        bucket[v] = new List<char>();
                    }
                    bucket[v].Add(key);
                }

                var sb = new StringBuilder();
                var index = s.Length;

                while (index > 0)
                {
                    if (bucket[index] != null)
                    {
                        foreach (char c in bucket[index])
                        {
                            for (int i = 0; i < map[c]; i++)
                            {
                                sb.Append(c);
                            }
                        }

                    }

                    index--;
                }

                return sb.ToString();
            }

            public string FrequencySortWithHeap(string s)
            {
                var map =new Dictionary<char,int>();
                foreach (var c in s)
                {
                    if (!map.ContainsKey(c))
                    {
                        map.Add(c,0);
                    }
                    map[c]++;
                }

                var heap = new MaxHeap<CharNode>();
                foreach (var i in map.Keys)
                {
                    heap.Push(new CharNode
                    {
                        Value = i,
                        Frequency = map[i]
                    });
                }

                var sb = new StringBuilder();
                while (heap.Any())
                {
                    var node = heap.Pop();
                    for (var i = 0; i < node.Frequency; i++)
                    {
                        sb.Append(node.Value);
                    }
                }

                return sb.ToString();
            }

            public string GetHint(string secret, string guess)
            {
                if (string.IsNullOrEmpty(secret))
                {
                    return "0A0B";
                }

                int a = 0;
                int b = 0;
                int[] map = new int[10];
                for (int i = 0; i < secret.Length; i++)
                {
                    var s = secret[i];
                    var g = guess[i];
                    if (s == g)
                    {
                        a++;
                    }
                    else
                    {
                        // Number is seen in secret and now is found in guess.
                        if (map[s - '0']++ < 0)
                        {
                            b++;
                        }

                        // Number is seen in guess and now is found in secret.
                        if (map[g - '0']-- > 0)
                        {
                            b++;
                        }
                    }
                }

                return a + "A" + b + "B";
            }
        }

        public class CharNode : IComparable<CharNode>
        {
            public char Value { get; set; }
            public int Frequency { get; set; }

            public int CompareTo(CharNode other)
            {
                var r = Frequency.CompareTo(other.Frequency);
                return r == 0 ? 0 - Value.CompareTo(other.Value) : r;
            }
        }



    }
}
