using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode.Design
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
}
