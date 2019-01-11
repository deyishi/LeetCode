using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace LeetCode
{
    class DynamicProgrammingSet
    {

        [Test]
        public void MinimumTotal()
        {
            var t = new List<IList<int>>()
                {new List<int> {2}, new List<int> {3, 4}, new List<int> {6, 5, 7}, new List<int> {4, 1, 8, 3}};
            var r = MinimumTotal(t);
        }


        /// <summary>
        ///       1
        ///    2     3
        /// Find min from bottom to top.
        /// </summary>
        /// <param name="triangle"></param>
        /// <returns></returns>
        public int MinimumTotal(IList<IList<int>> triangle)
        {
            if (triangle == null || triangle.Count == 0 || triangle[0].Count == 0)
            {
                return 0;
            }

            for (var level = triangle.Count - 2; level >= 0; level--)
            {
                for (var i = 0; i < triangle[level].Count; i++)
                {
                    triangle[level][i] = Math.Min(triangle[level + 1][i], triangle[level + 1][i + 1]) + triangle[level][i];
                }
            }

            return triangle[0][0];
        }
    }
}
