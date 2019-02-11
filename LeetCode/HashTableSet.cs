using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace LeetCode
{
    class WordDistance
    {
        //    244. Shortest Word Distance II
        //Map to track word and its positions to avoid loop through word[] multiple times.
        //In shortest function, get index list for word1 and word2, compare position, if index1<index2, then index1 is before index2, we check next word1's position, otherwise we check next word2 position.
        readonly Dictionary<string, List<int>> _map = new Dictionary<string, List<int>>();
        public WordDistance(string[] words)
        {
            for (int i = 0; i < words.Length; i++)
            {
                var word = words[i];
                if (_map.ContainsKey(word))
                {
                    _map[word].Add(i);
                }
                else
                {
                    _map.Add(word, new List<int> { i });
                }
            }

        }

        public int Shortest(string word1, string word2)
        {
            var w1 = _map[word1];
            var w2 = _map[word2];

            var i = 0;
            var j = 0;
            var result = int.MaxValue;
            while (i < w1.Count && j < w2.Count)
            {
                var a = w1[i];
                var b = w2[j];
                if (a < b)
                {
                    result = Math.Min(result, b - a);
                    i++;
                }
                else
                {
                    result = Math.Min(result, a - b);
                    j++;
                }
            }

            return result;
        }
    }


    class HashTableSet
    {
        [Test]
        public void Test()
        {
            var a = "AAAAAAAAAAAA";

            var r = FindRepeatedDnaSequences(a);
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
                    if (a[i,k] != 0)
                    {
                        for (int j = 0; j < bCols; j++)
                        {
                            if (b[k,j] != 0) r[i,j] += a[i,k] * b[k,j];
                        }
                    }
                }
            }

            return r;
        }


    }

}
