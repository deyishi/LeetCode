using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace LeetCode.DataModel
{
    public class ValidWordAbbr
    {

        private Dictionary<string, HashSet<string>> _map = new Dictionary<string, HashSet<string>>();
        public ValidWordAbbr(string[] dictionary)
        {
            foreach (var word in dictionary)
            {
                var abbr = GetWordAbbr(word);
                if (_map.ContainsKey(abbr))
                {
                  _map[abbr].Add(word);
                }
                else
                {
                    _map.Add(abbr, new HashSet<string> {word});
                }


            }
        }

        public bool IsUnique(string word)
        {
            var abbr = GetWordAbbr(word);

            return !_map.ContainsKey(abbr) || _map[abbr].Count == 1 && _map[abbr].Contains(word);
        }

        public string GetWordAbbr(string word)
        {
            if (word == null || word.Length < 3)
            {
                return word;
            }

            return word[0] + (word.Length - 2).ToString() + word[word.Length - 1];
        }
    }
}
