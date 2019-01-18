using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace LeetCode
{
    public class ArraySet
    {
        [Test]
        public void Test()
        {
            var n = new[] { 0, 1, 2, 4, 5,6, 7 };
            var s = 0;
            var e = 99;
            var r = SummaryRanges(n);
        }


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


        /// <summary>
        /// First range check nums[0]-1 with lower.
        /// Mid ranges loop through from 1 to n, compare [i - 1] + 1 with [i] -1 to check consecutive number (either missing one, missing a chunk or nothing).
        /// Last range check nums[n - 1]+1 with upper.
        /// </summary>
        /// <param name="nums"></param>
        /// <param name="lower"></param>
        /// <param name="upper"></param>
        /// <returns></returns>
        public IList<string> FindMissingRanges(int[] nums, int lower, int upper)
        {
            List<string> result = new List<string>();
            if (nums == null || nums.Length == 0)
            {
                AddRange(lower, upper, result);
                return result;
            }

            AddRange(lower, (long)nums[0] - 1, result);

            for (var i = 1; i < nums.Length; i++) {
                AddRange((long)nums[i-1]+1, (long)nums[i] -1, result);
            }
            AddRange((long)nums[nums.Length -1] + 1, upper, result);

            return result;
        }

        public void AddRange(long start, long end, List<string> r)
        {
            // Not missing anything
            if (start > end)
            {
                return;
            }

            // Missing one
            if (start == end)
            {
                r.Add(start + "");
            }
            else
            {
                // Missing a chunk
                r.Add(start + "->" + end);
            }
        }

        //163. Missing Ranges
        //Create helper function to merge start and end and add to result.
        //    Pointer j to track the start for next range at n[i-1]+1 != n[i], j = i.
        //    Loop through n and use helper function.
        //    Handle last number using j pointer when loop ends.
        public IList<string> SummaryRanges(int[] nums)
        {
            var result = new List<string>();
            if (nums == null || nums.Length == 0)
            {
                return result;
            }

            // Use j to track the number after last merge end.
            var j = 0;
            for (int i = 1; i < nums.Length; i++) {
                if (nums[i-1]+1 == nums[i])
                {
                    continue;
                }
                AddRange(nums[j], nums[i-1], result);
                j = i;
            }

            //Create range from last merge end with last number.
            AddRange(nums[j], nums[nums.Length -1], result);

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
