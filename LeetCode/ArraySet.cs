using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using LeetCode.DataModel;
using NUnit.Framework;

namespace LeetCode
{
    public class ArraySet
    {
        [Test]
        public void Test()
        {
            var n = new[] {1,2 };
            var s = 2 & 1;

            for (var i = 0; i < 4; i ++)
            {
                Debug.WriteLine(i & 1);
            }


            var m = new int[4][];
            m[0] = new[] {0, 1, 0};
            m[1] = new[] {0, 0, 1};
            m[2] = new[] {1, 1, 1};
            m[3] = new[] {0, 0, 0};

            var r = TopKFrequent(n, 2);
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

        public string LargestNumber(int[] nums)
        {
            var n = new List<string>();
            foreach (var num in nums)
            {
                n.Add(num.ToString());
            }

            // Comparator b - a sorting in descending order.
            n.Sort((a, b) => (int) (long.Parse(b + a) - long.Parse(a + b)));
            StringBuilder sb = new StringBuilder();
            foreach (var x in n)
            {
                sb.Append(x);
            }

            //Since a comparator is used in above code, only when all the elements are "0", the first element can be "0".Otherwise, if a string contains one character larger than zero, the first element must be nonzero. Therefore, we only need to consider whether the first string is "0" in a sorted array.
            if (sb[0] == '0')
            {
                return "0";
            }

            //remove leading 0
            return sb.ToString();
        }

        //229. Majority Element II
        //    There will not be more than 2 elements that appear more than n/3. 
        //For example, 20/3 = 6 if candidate A appears 7 times, candidate B appears 7 times, then the other candidate can at most appear 6 times.
        //    Loop through the nums, two counters for two candidates, when we see a candidate increment its counter, otherwise decrease their counter.
        //    If a candidate is not the majority, the algorithm will find another candidate that can pair with the none-majority candidate and drop its count to 0. When count for none-majority candidate become 0, the majority candidate will become the new candidate.
        //    Loop through the numbs and count two candidates appeared times, to check if two candidates are qualify.
        public IList<int> MajorityElement(int[] nums)
        {
            var result = new List<int>();
            if (nums == null || nums.Length == 0)
            {
                return result;
            }

            var mark = nums.Length / 3;

            //if (mark < 1)
            //{
            //    return nums.ToList();
            //}

            int count1 = 0;
            int count2 = 0;
            int candidate1 = 0;
            int candidate2 = 0;
            foreach (var n in nums)
            {
                if (n == candidate1)
                {
                    count1++;
                }else if (n == candidate2)
                {
                    count2++;
                }else if (count1 == 0)
                {
                    candidate1 = n;
                    count1 = 1;
                }else if (count2 == 0)
                {
                    candidate2 = n;
                    count2 = 1;
                }
                else
                {
                    // Cancel out
                    count1--;
                    count2--;
                }
            }


            count1 = 0;
            count2 = 0;
            foreach (var n in nums)
            {
                if (n == candidate1)
                {
                    count1++;
                }else if (n == candidate2)
                {
                    count2++;
                }

            }

            if (count1 > mark)
            {
                result.Add(candidate1);
            }

            if (count2 > mark)
            {
                result.Add(candidate2);
            }

            return result;
        }

        public int[] ProductExceptSelf(int[] nums)
        {
            //238.Product of Array Except Self
            //Product from the left, left[i] = nums[0] * .... * nums[i - 1] * nums[i]
            //Product from the right, right[i] = nums[i] * nums[i + 1] * .... * nums[len(nums)]
            //Calculate 4 for [1, 2, 3, 4, 5, 6, 7]
            //4 = left[3](1 * 2 * 3) * right[3](7 * 6 * 5)
            int n = nums.Length;
            int[] result = new int[n];

            result[0] = 1;
            for (int i = 1; i < n; i++)
            {
                result[i] = result[i - 1] * nums[i - 1];
            }

            int right = 1;
            for (int i = n-1; i >= 0; i--)
            {
                result[i] *= right;
                right *= nums[i];
            }

            return result;
        }

        //Find the positions of two word then record the min distance. 
        public int ShortestDistance(string[] words, string word1, string word2)
        {
            int p1 = -1;
            int p2 = -1;
            int distance = int.MaxValue;

            for (int i = 0; i < words.Length; i++)
            {
                if (words[i] == word1)
                {
                    p1 = i;
                }

                if (words[i] == word2)
                {
                    p2 = i;
                }

                if (p1 != -1 && p2 != -1)
                {
                    distance = Math.Min(distance, Math.Abs(p1 - p2));
                }
            }

            return distance;
        }

        // Loop through words, record p1 for word1, p2 for word2, update distance at each i.
        // If word1 = word2, then update p2 with last p1, then update p1 with i (when we see word1 the second time, we treat it as word2).
        public int ShortestWordDistanceWithDuplicates(string[] words, string word1, string word2)
        {

            int p1 = -1;
            int p2 = -1;
            int distance = int.MaxValue;
            for (var i = 0; i < words.Length; i++)
            {
                if (words[i] == word1)
                {
                    if (word1 == word2)
                    {
                        p2 = p1;
                    }
                    p1 = i;
                }
                else if (words[i] == word2)
                {
                    p2 = i;
                }

                if (p1 != -1 && p2 != -1)
                {
                    distance = Math.Min(distance, Math.Abs(p1 - p2));
                }
            }

            return distance;
        }

        public bool CanAttendMeetings(Interval[] intervals)
        {
            Array.Sort(intervals, (a, b) => a.start - b.start);

            for (int i = 1; i < intervals.Length; i++)
            {
                if (intervals[i].start < intervals[i-1].end)
                {
                    return false;
                }
            }

            return true;
        }

        public int MinMeetingRooms(Interval[] intervals)
        {
            int[] start = new int[intervals.Length];
            int[] end = new int[intervals.Length];


            for (int i = 0; i < intervals.Length; i++)
            {
                start[i] = intervals[i].start;
                end[i] = intervals[i].end;
            }

            Array.Sort(start);
            Array.Sort(end);

            var lastMeetingEndIndex = 0;
            var rooms = 0;
            foreach (var s in start)
            {
               
                if (s < end[lastMeetingEndIndex])
                {
                    // Add a room since current meeting starts before last meeting end.
                    rooms++;
                }
                else
                {
                    //Current meeting starts after last meeting end, put current meeting into the room used by the last meeting. Move meeting end index to point at the next available room's meeting end.
                    lastMeetingEndIndex++;
                }
            }

            return rooms;
        }

        public void WiggleSort(int[] nums)
        {
            for (var i = 1; i < nums.Length; i ++) {
                if ( i % 2 == 1)
                {
                    // Even
                    if (nums[i-1] > nums[i])
                    {
                        WiggleSortSwap(nums, i);
                    }
                }
                else
                {
                    //Odd
                    if (nums[i-1] < nums[i])
                    {
                        WiggleSortSwap(nums, i);
                    }
                }
            }
        }

        private static void WiggleSortSwap(int[] nums, int i)
        {
            var temp = nums[i - 1];
            nums[i - 1] = nums[i];
            nums[i] = temp;
        }

        public int LargestRectangleAreaBruteForce(int[] heights)
        {
            var result = 0;
            for (var i = 0; i < heights.Length; i++)
            {
                var min = int.MaxValue;
                for (int j = i; j < heights.Length; j++)
                {
                    min = Math.Min(heights[j], min);
                    result = Math.Max(min * (j - i + 1), result);
                }
            }

            return result;
        }

        public int LargestRectangleAreaDivideConquer(int[] heights)
        {
            return LargestRectangleAreaDivideConquerHelper(heights, 0, heights.Length - 1);
        }

        private int LargestRectangleAreaDivideConquerHelper(int[] heights, int start, int end)
        {
            if (start > end)
            {
                return 0;
            }

            var minIndex = start;
            for (var i = start + 1; i <= end; i++)
            {
                if (heights[i] < heights[minIndex])
                {
                    minIndex = i;
                }
            }

            return Math.Max(heights[minIndex] * (end - start + 1),
                Math.Max(LargestRectangleAreaDivideConquerHelper(heights, start, minIndex - 1),
                    LargestRectangleAreaDivideConquerHelper(heights, minIndex + 1, end)));
        }

        public int LargestRectangleArea(int[] heights)
        {

            var result = 0;
            var stack = new Stack<int>();
            var i = 0;
            while (i < heights.Length)
            {
                if (!stack.Any() || heights[stack.Peek()] <= heights[i])
                {
                    stack.Push(i++);
                }
                else
                {
                    var h = stack.Pop();
                    var w = stack.Any() ? i - 1 - stack.Peek() : i;
                    result = Math.Max(result, heights[h] * w);
                }
            }

            //Handle a stack didn't get processed due to length 1 array or ascending order array.
            while (!stack.Any())
            {
                var h = stack.Pop();
                result = Math.Max(result, heights[h] * (h + 1));
            }

            return result;
        }

        private int[,] MooreNeighbor =
            {{-1, -1}, {-1, 0}, {-1, 1}, {0, -1}, {0, 1}, {1, -1}, {1, 0}, {1, 1}};
        private int _die = 2;
        private int _live = 3;
        public void GameOfLife(int[][] board)
        {
            // Go through the board, mark cell die, live or keep the same based on the rule.
            for (var i = 0; i < board.Length; i++)
            {
                for (var j = 0; j < board[0].Length; j++)
                {
                    var liveNeighbors = GetLiveNeighbors(board, i, j);
                    if (board[i][j] == 0 && liveNeighbors == 3)
                    {
                        //Any dead cell with exactly three live neighbors becomes a live cell.
                        board[i][j] = _live;
                    }
                    else if (board[i][j] == 1)
                    {
                        if (liveNeighbors == 2 || liveNeighbors == 3)
                        {
                            //Any live cell with two or three live neighbors lives on to the next generation.
                            continue;
                        }

                        if (liveNeighbors < 2 || liveNeighbors > 3)
                        {
                            //Any live cell with  fewer than two live neighbors or more than three live neighbors dies.
                            board[i][j] = _die;
                        }
                    }
                }
            }

            // Update the board
            for (var i = 0; i < board.Length; i++)
            {
                for (var j = 0; j < board[0].Length; j++)
                {
                    if (board[i][j] == _die)
                    {
                        board[i][j] = 0;
                    }

                    if (board[i][j] == _live)
                    {
                        board[i][j] = 1;
                    }
                }
            }
        }

        private int GetLiveNeighbors(int[][] board, int r, int c)
        {
            var rows = board.Length;
            var cols = board[0].Length;
            var count = 0;
            for (var i = 0; i < 8; i++)
            {
                var row = r + MooreNeighbor[i, 0];
                var col = c + MooreNeighbor[i, 1];

                // Check live cell or live cell marked to die.
                if (row >= 0 && col >= 0 && row < rows && col < cols && (board[row][col] == 1 || board[row][col] == _die))
                {
                    count++;
                }
            }

            return count;
        }

        public int FindKthLargest(int[] nums, int k)
        {
            if (nums == null || nums.Length == 0)
            {
                return 0;
            }
            return QuickSelectHelper(nums, 0, nums.Length - 1, k);
        }

        private int QuickSelectHelper(int[] nums, int start, int end, int k)
        {
            var pivotalNum = nums[end];
            var lessThanPIndex = start;

            for (int i = start; i < end; i++)
            {
                if (nums[i] <= pivotalNum)
                {
                    Swap(nums, i, lessThanPIndex);
                    lessThanPIndex++;
                }
            }

            Swap(nums, lessThanPIndex, end);

            if (lessThanPIndex == nums.Length - k)
            {
                return nums[lessThanPIndex];
            }

            if (lessThanPIndex < nums.Length - k)
            {
                return QuickSelectHelper(nums, lessThanPIndex + 1, end, k);
            }

            return QuickSelectHelper(nums, start, lessThanPIndex - 1, k);
        }

        public void Swap(int[] nums, int i, int j)
        {
            var temp = nums[i];
            nums[i] = nums[j];
            nums[j] = temp;
        }

        public void WiggleSortTwo(int[] nums)
        {
            // Sort array
            // m pointer pointing at median, n pointer point at array end
            // Remember m and n needs to subtract 1, since it is 1 based.
            // if i is even put median pointer number, median--
            // else put n pointer number, n--
            Array.Sort(nums);
            var temp = new List<int>(nums);
            var medianIndex = (nums.Length + 1) / 2 - 1;
            var end = nums.Length -1;
            for (var i = 0; i < nums.Length; i++)
            {
                //&1 = 1 odd
                nums[i] = (i & 1) == 1 ? temp[end--] : temp[medianIndex--];
            }
        }

        public IList<int> TopKFrequent(int[] nums, int k)
        {
            Dictionary<int,int> f = new Dictionary<int, int>();
            for (var i = 0; i < nums.Length;i++) {
                if (f.ContainsKey(nums[i]))
                {
                    f[nums[i]]++;
                }
                else
                {
                    f.Add(nums[i], 0);
                }
            }

            List<int>[] n = new List<int>[nums.Length];
            foreach (var key in f.Keys)
            {
                var v = f[key];
                if (n[v] == null)
                {
                    n[v] = new List<int>();
                }
                n[v].Add(key);
            }

            int index = nums.Length-1;
            var result = new List<int>();
            while (index >= 0 && result.Count < k)
            {
                if (n[index] != null)
                {
                    result.AddRange(n[index]);
                }

                index--;
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
