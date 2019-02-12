using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeetCode.DataModel;
using NUnit.Framework;

namespace LeetCode
{
    public class UnionFindSet
    {
        [Test]
        public void Test()
        {
            var a = new int[] {100, 4, 200, 1, 3, 2};

            var r = LongestConsecutive(a);
        }

        public int LongestConsecutive(int[] nums)
        {
            var u = new ArrayUnionFind(nums.Length);
            //number and its index
            var map = new Dictionary<int, int>();
            for (var i = 0; i < nums.Length; i++)
            {

                if (map.ContainsKey(nums[i]))
                {
                    // Duplicate
                    continue;
                }

                map.Add(nums[i], i);

                if (map.ContainsKey(nums[i] - 1))
                {
                    u.Union(i, map[nums[i] - 1]);
                }

                if (map.ContainsKey(nums[i] + 1))
                {
                    u.Union(i, map[nums[i] + 1]);
                }

            }

            return u.MaxUnion();
        }
    }
}
