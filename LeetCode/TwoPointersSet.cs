using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace LeetCode
{
    public class TwoPointersSet
    {

        [Test]
        public void LengthOfLongestSubstring()
        {
            var s = "abcabcbb";

            var r = LengthOfLongestSubstring(s);
        }

        /// <summary>
        /// 0.Loop through s.length.
        /// 1.Find a qualified result starting i and j (remember j), j stops at j+1 have duplicate.
        /// 2.Record result (j-i) using Math.Max(result, j-i) for each i.
        /// 3.Increment i and remove s[i] in the map, the result won't count due to Math.Max() during the time while j+1 is still the duplicate.
        /// 4.I is incremented to a point where j+1 is no longer duplicate, then increment j to reach another result. Check result.length at this i.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public int LengthOfLongestSubstring(string s)
        {
            if(string.IsNullOrEmpty(s))
            {
                return 0;
            }

            var result = int.MinValue;
            var map = new int[256];
            var j = 0;
            for (var i = 0; i < s.Length; i++)
            {
                while (j < s.Length && map[s[j]] < 1)
                {
                    map[s[j]]++;
                    j++;
                }

                result = Math.Max(result, j - i);
                map[s[i]]--;
            }

            return result;
        }
    }
}
