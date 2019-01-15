using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace LeetCode
{
    public class BinarySearchPractice
    {

        [Test]
        public void BinarySearch()
        {
            var n = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 12, 12, 12, 12, 12, 15 };
            var t = 15;

            var l = n.Length - 1;
            var m = 0;
            var c = (m - 1 + l) % l;
            var r = BinarySearch(n, t);
            var r2 = BinarySearchFindOccurrenceRange(n, t);
        }
        public int BinarySearch(int[] nums, int target)
        {
            if (nums == null || nums.Length < 1)
            {
                return -1;
            }

            var l = 0;
            var r = nums.Length - 1;
            while (l <= r)
            {
                var m = l + (r - l) / 2;
                if (nums[m] == target)
                {
                    return m;
                }

                if (nums[m] > target)
                {
                    r = m - 1;
                }
                else
                {
                    l = m + 1;
                }
            }

            return -1;
        }

        public int BinarySearchRangeStart(int[] nums, int target, bool rangeStart = true)
        {
            if (nums == null || nums.Length < 1)
            {
                return -1;
            }

            var l = 0;
            var r = nums.Length - 1;
            var result = -1;
            while (l <= r)
            {
                var m = l + (r - l) / 2;
                if (nums[m] == target)
                {
                    result = m;
                    if (rangeStart)
                    {
                        r = m - 1;
                    }
                    else
                    {
                        l = m + 1;
                    }

                }
                else if (nums[m] > target)
                {
                    r = m - 1;
                }
                else
                {
                    l = m + 1;
                }
            }

            return result;
        }

        public int[] BinarySearchFindOccurrenceRange(int[] nums, int target)
        {
            var start = BinarySearchRangeStart(nums, target);
            var end = BinarySearchRangeStart(nums, target, false);
            return new[] {start, end};
        }

        [Test]
        public void FindMin()
        {
            var n = new[] {2, 1};
            var r = FindMin(n);
        }
        public int FindMin(int[] nums)
        {
            if (nums == null || nums.Length < 1)
            {
                return -1;
            }

            var n = nums.Length;
            var l = 0;
            var r = n -1;
            while (l <= r)
            {
                if (nums[l] <= nums[r])
                {
                    return l;
                }

                var m = l + (r - l) / 2;

                var mn = (m + 1) % n;
                var mp = (m - 1 + n) % n;
                if (nums[m] <= nums[mn] && nums[m] <= nums[mp])
                {
                    return m;
                }

                if (nums[m] <= nums[r])
                {
                    r = m - 1;
                }
                else
                {
                    l = m + 1;
                }
            }

            // Array not sorted.
            return -1;
        }

        public int SearchCircularSortedArray(int[] nums, int target)
        {
            if (nums == null || nums.Length < 1)
            {
                return -1;
            }

            var l = 0;
            var r = nums.Length - 1;
            while (l <= r)
            {
                var m = l + (r - l) / 2;

                if (nums[m] == target)
                {
                    return m;
                }

                // check which half is sorted
                if (nums[m] <= nums[r])
                {
                    // Right half is sorted
                    // update range to be sorted half and look for target
                    if (nums[m] <= target && target < nums[r])
                    {
                        l = m + 1;
                    }
                    else
                    {
                        r = m - 1;
                        // update range to the unsorted half
                    }
                }
                else
                {
                    if (nums[l] <= target && target < nums[m])
                    {
                        r = m - 1;
                    }
                    else
                    {
                        l = m + 1;
                    }
                }
            }

            return -1;
        }

        //378. Kth Smallest Element in a Sorted Matrix
        [Test]
        public void METHOD()
        {
            var matrix = new[,]
            {
                {1, 5, 9},
                {10, 11, 13},
                {12, 13, 15}
            };
                var k = 8;
            var r = KthSmallest(matrix, k);
        }
        public int KthSmallest(int[,] matrix, int k)
        {
            var lo = matrix[0, 0];
            var hi = matrix[matrix.GetLength(0) - 1, matrix.GetLength(1) - 1] + 1;
            while (lo < hi)
            {
                var mid = lo + (hi - lo) / 2;
                var count = 0;
                var j = matrix.GetLength(1) - 1;
                for (var i = 0; i < matrix.GetLength(0); i++)
                {
                    while (j >= 0 && matrix[i,j] > mid)
                    {
                        j--;
                    }

                    count += j + 1;
                }

                if (count < k)
                {
                    lo = mid + 1;
                }
                else
                {
                    hi = mid;
                }
            }

            return lo;
        }
    }
}
