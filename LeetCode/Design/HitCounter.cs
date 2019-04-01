using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode.Design
{
    public class HitCounter
    {
        private int[] times;
        private int[] hits;
        public HitCounter()
        {
            times = new int[300];
            hits = new int[300];
        }

        /** Record a hit.
            @param timestamp - The current timestamp (in seconds granularity). */
        public void Hit(int timestamp)
        {
            int index = timestamp % 300;
            if (times[index] != timestamp)
            {
                times[index] = timestamp;
                hits[index] = 1;
            }
            else
            {
                hits[index]++;
            }
        }

        /** Return the number of hits in the past 5 minutes.
            @param timestamp - The current timestamp (in seconds granularity). */
        public int GetHits(int timestamp)
        {
            int result = 0;
            for (var i = 0; i < 300; i++) {
                if (timestamp - times[i] < 300)
                {
                    result += hits[i];
                }
            }

            return result;
        }
    }
}
