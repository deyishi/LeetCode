using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace LeetCode
{
    public class GreedySet
    {
        [Test]
        public void Test()
        {
            int[] r = new int[] {3,3,4 };
            int [] m = new int[] {3,4,4};
            var c = CanCompleteCircuit(r,m);
        }


        public int Candy(int[] ratings)
        {
            int childrenCount = ratings.Length;
            int[] candyPerChild = new int[childrenCount];

            // Each child has at least one candy.
            for (var i = 0; i < childrenCount; i++)
            {
                candyPerChild[i] = 1;
            }

            // Scan from left to right, make sure right higher rate child gets 1 more candy than the left lower rate child.
            for (int i = 1; i < childrenCount; i++)
            {
                if (ratings[i] > ratings[i - 1])
                {
                    candyPerChild[i] = candyPerChild[i - 1] + 1;
                }
            }

            // Scan from right to left, make sure left higher rate child get 1 more candy than the right lower rate child.
            for (int i = childrenCount - 2; i >= 0; i--)
            {
                // Make sure left child doesn't already have more candies than the right child.
                if (ratings[i] > ratings[i + 1] && candyPerChild[i] <= candyPerChild[i + 1])
                {
                    candyPerChild[i] = candyPerChild[i + 1] + 1;
                }
            }

            return candyPerChild.Sum();

        }

        public int CanCompleteCircuit(int[] gas, int[] cost)
        {
            int start = 0;
            int debt = 0;
            int remain = 0;
            for (int i = 0; i < gas.Length; i++)
            {
                remain += gas[i] - cost[i];
                if (remain < 0)
                {
                    // Can reach next station, reset start. Record current total gas
                    start = i + 1;
                    debt += remain;
                    remain = 0;
                }
            }

            return remain + debt >= 0 ? start : -1;
        }
    }
}
