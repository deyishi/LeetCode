using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace LeetCode.Design
{
    public class ConsistentHashing
    {
        [Test]
        public void Test()
        {
            var r = GenerateHash(3);
        }

        public List<int[]> GenerateHash(int n)
        {
            var ret = new List<int[]>();

            var range = new[] {0, 359, 1};
            ret.Add(range);
            for (int i = 2; i <= n; i++)
            {
                int maxRange = int.MinValue;
                int index = -1;

                // Find largest interval.
                for (int j = 0; j < ret.Count; j++)
                {
                    if (maxRange < ret[j][1] - ret[j][0])
                    {
                        maxRange = ret[j][1] - ret[j][0];
                        index = j;
                    }
                }

                // Cut the largest interval in half. Assign the other half to new node.
                int mid = (ret[index][1] + ret[index][0]) / 2;
                int end = ret[index][1];
                ret[index][1] = mid;
                ret.Add(new []{ mid + 1, end, i });
            }

            return ret;
        }
    }
}
