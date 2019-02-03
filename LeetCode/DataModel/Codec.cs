using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace LeetCode.DataModel
{
    public class Codec
    {

        public string Encode(IList<string> strs)
        {
            var sb = new StringBuilder();
            foreach (var s in strs)
            {
                sb.Append(s.Length).Append('.').Append(s);
            }

            return sb.ToString();
        }

        // Decodes a single string to a list of strings.
        public IList<string> Decode(string s)
        {
            var result = new List<string>();

            int i = 0;
            while (i < s.Length)
            {
                // First slash will always be the coded slash.
                int slash = s.IndexOf('.', i);
                int size = int.Parse(s.Substring(i, slash - i));
                i = slash + size + 1;
                result.Add(s.Substring(slash + 1, i - slash - 1));
            }

            return result;
        }
    }
}
