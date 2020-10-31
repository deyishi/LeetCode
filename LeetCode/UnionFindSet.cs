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

            int[][] edges = new int[][]{new []{1,2}, new []{1,3}, new []{2,4}};
            var r = FindRedundantConnection(edges);
        }

        public int[] FindRedundantConnection(int[][] edges)
        {
            int[] set = new int[1001];
            int[] rank = new int[1001];
            for (int i = 0; i < set.Length; i++)
            {
                set[i] = i;
            }

            foreach (int[] edge in edges)
            {
                int x = edge[0];
                int y = edge[1];
                if (!Union(set, rank, x, y))
                {
                    return edge;
                }
            }
            return null;
        }

        public bool Union(int[] set, int[] rank, int x, int y)
        {
            int xroot = FindRoot(set, x);
            int yroot = FindRoot(set, y);
            if (xroot == yroot)
            {
                return false; // 2,3 will return false
            }

            if (rank[xroot] == rank[yroot])
            {
                set[yroot] = xroot;
                rank[xroot]++;
            }
            else if (rank[xroot] > rank[yroot])
            {
                set[yroot] = xroot;
            }
            else
            {
                set[xroot] = yroot;
            }
            return true;
        }

        public int FindRoot(int[] set, int x)
        {
            while (set[x] != x)
            {
                x = set[x];
            }
            return x;
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

        [Test]
        public void EarliestAcq()
        {
            var stamps = new FriendStamp[]
            {
                new FriendStamp(20181201, 0, 3),
                new FriendStamp(20190101, 0, 1),
                new FriendStamp(20190104, 3, 4),
                new FriendStamp(20190107, 2, 3),
                new FriendStamp(20190211, 1, 5),
                new FriendStamp(20190224, 2, 4),
                new FriendStamp(20190312, 1, 2),
                new FriendStamp(20190322, 4, 5),
            };

            var n = 6;
            var r = EarliestAcq(stamps, 6);

        }

        public int EarliestAcq(FriendStamp[] stamps, int n)
        {
            var u = new int[n];
            for (int i = 0; i < n; i++)
            {
                u[i] = i;
            }
            var circle = n;
            var sort = stamps.OrderBy(x => x.Time);
            foreach (var stamp in sort)
            {
                var root1 = Find(u, stamp.A);
                var root2 = Find(u, stamp.B);
                if (root1 != root2)
                {
                    u[root1] = root2;
                    circle--;
                }

                if (circle ==1 )
                {
                    return stamp.Time;
                }

            }

            return -1;
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

    public class FriendStamp
    {
        public FriendStamp(int time, int a, int b)
        {
            Time = time;
            A = a;
            B = b;
        }

        public int Time { get; set; }
        public int A { get; set; }
        public int B { get; set; }
    }
}
