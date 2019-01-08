using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace LeetCode
{
    public class BackTracking
    {
        [Test]
        public void Combine()
        {
            var n = 4;
            var k = 2;
            var r = Combine(n,k);
        }
        public IList<IList<int>> Combine(int n, int k)
        {
            var result = new List<IList<int>>();
            if (n == 0)
            {
                return result;
            }

            CombineHelper(n, k, 1, new List<int>(), result);

            return result;
        }

        private void CombineHelper(int n, int k, int start, List<int> path, List<IList<int>> result)
        {
            if (path.Count == k)
            {
                result.Add(new List<int>(path));
                return;
            }

            for (var i = start; i <= n; i++) {

                path.Add(i);
                CombineHelper(n, k, i+1, path, result);
                path.RemoveAt(path.Count - 1);
            }
        }
    }
}
