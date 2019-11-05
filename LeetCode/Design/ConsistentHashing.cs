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

        public List<int[]> GenerateHash(int serverCount)
        {
            var initialServer = new[] { 0, 359, 1 };

            var servers = new List<int[]> { initialServer };

            for (int serverIndex = 2; serverIndex <= serverCount; serverIndex++)
            {
                var maxCapacityServerIndex = 0;
                for (int i = 1; i < servers.Count; i++)
                {
                    if (servers[i - 1][1] - servers[i - 1][0] < servers[i][1] - servers[i][0])
                    {
                        maxCapacityServerIndex = i;
                    }
                }

                var maxCapacityServerEndRange =
                    (servers[maxCapacityServerIndex][1] - servers[maxCapacityServerIndex][0]) / 2;

                var newServer = new[] { maxCapacityServerEndRange + 1, servers[maxCapacityServerIndex][1], serverIndex };
                servers[maxCapacityServerIndex][1] = maxCapacityServerEndRange;
                servers.Add(newServer);
            }

            return servers;
        }
    }
}
