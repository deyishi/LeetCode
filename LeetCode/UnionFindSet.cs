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

            var r = LongestConsecutive2(a);
        }

        public int LongestConsecutive2(int[] nums)
        {
            var map = new Dictionary<int, int>();
            var result = 0;
            foreach (var n in nums)
            {
                if (!map.ContainsKey(n))
                {

                    var leftBound = map.ContainsKey(n - 1) ? map[n - 1] : 0;
                    var rightBound = map.ContainsKey(n + 1) ? map[n + 1] : 0;

                    var curr = leftBound + rightBound + 1;
                    map.Add(n, curr);

                    result = Math.Max(result, curr);

                    //Update two ends
                    map[n - leftBound] = curr;
                    map[n + rightBound]= curr;
                }
            }

            return result;
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

        public int CountComponents(int n, int[,] edges)
        {
            var r = n;
            var u = new int[n];
            for (int i = 0; i < u.Length; i++)
            {
                u[i] = i;
            }

            for (int i = 0; i < edges.GetLength(0); i++)
            {
                var root1 = Find(u,edges[i, 0]);
                var root2 = Find(u, edges[i, 1]);
                if (root2 != root1)
                {
                    u[root1] = root2;
                    r--;
                }
            }

            return r;
        }

        public int Find(int[] a, int i)
        {
            while (a[i] != i)
            {
                i = a[i];
            }

            return a[i];
        }
    }
}
