using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode
{
    public class ArraySet
    {
        public IList<int> SpiralOrder(int[,] matrix)
        {
            var result = new List<int>();
            if (matrix == null || matrix.GetLength(0) < 1 || matrix.GetLength(1) < 1)
            {
                return result;
            }

            var left = 0;
            // Width
            var right = matrix.GetLength(1);
            var top = 0;
            // Height
            var bot = matrix.GetLength(0);

            while (left < right && top < bot)
            {
                // left to right
                for (var i = left; i < right; i++)
                {
                    result.Add(matrix[top, i]);
                }

                // top to bot
                for (var i = top; i < bot; i++)
                {
                    result.Add(matrix[i, right]);
                }

                // right to left
                for (var i = right; i > left;i--) {
                    result.Add(matrix[bot,i]);
                }

                // bot to top
                for (var i = bot; i > top; i--)
                {
                    result.Add(matrix[i, left]);
                }

                left++;
                right--;
                top++;
                bot--;
            }

            if (left == right)
            {
                for (var i = top; i < bot; i++) {
                    result.Add(matrix[i, left]);
                }
            }else if (top == bot)
            {
                for (var i = left; i < right;i++) {
                    result.Add(matrix[top,i]);
                }
            }

            return result;
        }
        /// <summary>
        /// All the intervals before newInterval starts.
        /// Merge all possible intervals overlapping newInterval, (start time is before new interval end time).
        /// </summary>
        /// <param name="intervals"></param>
        /// <param name="newInterval"></param>
        /// <returns></returns>
        public IList<Interval> Insert(IList<Interval> intervals, Interval newInterval)
        {
            var result = new List<Interval>();
            var i = 0;
            while (i < intervals.Count && intervals[i].end < newInterval.start)
            {
                result.Add(intervals[i]);
                i++;
            }
            // i stops at the interval where end is >= newInterval.start, starting of merge point.
            // Loop through all the intervals covered by new interval end.
            while (i < intervals.Count && intervals[i].start <= newInterval.end)
            {
                var current = intervals[i];

                // Save last interval to new interval and make compare.
                newInterval = new Interval
                {
                    start = Math.Min(newInterval.start, current.start),
                    end = Math.Max(newInterval.end, current.end)
                };
                i++;
            }
            result.Add(newInterval);
            //i stops at the interval where start is > new interval end, not covered by intervals.
            while (i<intervals.Count)
            {
                result.Add(intervals[i]);
                i++;
            }

            return result;
        }
    }
}

public class Interval
{

    public int start;
    public int end;
    public Interval() { start = 0; end = 0; }
    public Interval(int s, int e) { start = s; end = e; }
}
