﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace LeetCode
{
    public class BinarySearchSet
    {
        [Test]
        public void Test()
        {
            var m = new int[,] {
                {1,   4,  7, 11, 15},
                {2,   5,  8, 12, 19},
                {3,   6,  9, 16, 22},
                {10, 13, 14, 17, 24},
                {18, 21, 23, 26, 30}
            };

            var n = new[] {3, 1, 3, 4, 2};
            var r = FindDuplicate(n);
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
            return new[] { start, end };
        }

        public int FindMin(int[] nums)
        {
            if (nums == null || nums.Length < 1)
            {
                return -1;
            }

            var l = 0;
            var r = nums.Length - 1;
            while (l < r)
            {
                if (nums[l] <= nums[r] )
                {
                    return nums[l];
                }

                var mid = l + (r - l) / 2;

                if (nums[l] <= nums[mid])
                {
                    l = mid + 1;
                }
                else
                {
                    r = mid;
                }
            }

           
            return nums[l];
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
                    while (j >= 0 && matrix[i, j] > mid)
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

        //4. Median of Two Sorted Arrays
        //low <= high(inclusive)
        //Do binary search on the short array.Find a split point midX in X  (low + high /2) then calculate split point midY(X.Length + Y.Length + 1)/2 (+1 to handle even and odd) in Y.
        //Max of the part left to midX <= Min of the part right to midY(Consider case for one part maybe empty then use int.max or int.min)
        //Max of the part left to midY <= Min of the part right to midX
        //Then we find a result, check for odd and even.
        //If xMax > yMin, then reduce left part by set high = midX -1, else low = midX+1.
        public double FindMedianSortedArrays(int[] nums1, int[] nums2)
        {

            if (nums1.Length > nums2.Length)
            {
                return FindMedianSortedArrays(nums2, nums1);
            }

            int x = nums1.Length;
            int y = nums2.Length;
            int low = 0;
            int high = x;

            // Find split point using the short array to reduce complexity.
            // Inclusive to handle case [1,3], [2], since we split array into empty. 1 is for array with 1 element.
            while (low <= high)
            {
                int midX = (low + (high - low) / 2);
                int midY = (x + y + 1) / 2 - midX;

                // Consider one partition maybe empty.
                var xMax = midX == 0 ? int.MinValue : nums1[midX - 1];
                var yMax = midY == 0 ? int.MinValue : nums2[midY - 1];
                var xMin = midX == x ? int.MaxValue : nums1[midX];
                var yMin = midY == y ? int.MaxValue : nums2[midY];

                if (xMax <= yMin && xMin >= yMax)
                {
                    // Found, check odd or even
                    if ((x + y) % 2 == 0)
                    {
                        return ((double)Math.Max(xMax, yMax) + Math.Min(xMin, yMin)) / 2;
                    }

                    return Math.Max(xMax, yMax);
                }

                if (xMax > yMin)
                {
                    //Left part is too big
                    high = midX - 1;

                }
                else
                {
                    low = midX + 1;
                }
            }

            throw new Exception("Not Found");
        }

        //658. Find K Closest Elements
        //Find range A[i] to A[i + k - 1] as the result, use binary search to find i.
        //    l = 0, r = A.Length - k-1
        //if x - a[mid] (x closer to start range[mid] ) > a[mid + k] - x(x closer to end range), then a[mid + 1] to a[mid + k] is better than a[mid] to a[mid + k - 1].
        public IList<int> FindClosestElements(int[] arr, int k, int x)
        {
            int l = 0;
            int r = arr.Length - 1;

            while (l < r)
            {
                int mid = l + (r - l) / 2;

                // X is closer to mid then mid + k, then result in the left side.
                if (x - arr[mid] < arr[mid + k] - x)
                {
                    r = mid - 1;
                }
                else
                {
                    l = mid;
                }
            }

            var res = new List<int>();
            for (var i = 0; i < k; i++) res.Add(arr[l + i]);
            return res;
        }

        //From top right corner search down and left.
        public bool SearchMatrix(int[,] matrix, int target)
        {

            int r = 0;
            int c = matrix.GetLength(1) - 1;
            while (c >= 0 && r < matrix.GetLength(0))
            {

                if (target < matrix[r, c])
                {
                    c--;
                }
                else if (target > matrix[r, c])
                {
                    r++;
                }
                else
                {
                    return true;
                }
            }

            return false;
        }
        public int FindDuplicate(int[] nums)
        {
            if (nums.Length < 2)
            {
                return -1;
            }

            var left = 0;
            var right = nums.Length;
            while (left < right)
            {
                var mid = left + (right - left) / 2;
                var count = 0;
                foreach (var num in nums)
                {
                    if (num <= mid)
                    {
                        count++;
                    }
                }

                // Duplicate number in the left half.
                if (count <= mid)
                {
                    left = mid + 1;
                }
                else
                {
                    right = mid;
                }
            }

            return right;
        }

        public int LengthOfLIS(int[] nums)
        {
            if (nums == null || nums.Length == 0)
            {
                return 0;
            }

            var dp = new int[nums.Length];
            dp[0] = 1;
            var max = 1;
            for (var i = 1; i < dp.Length; i++)
            {
                int currentMax = 0;
                for (var j = 0; j < i; j++)
                {
                    if (nums[i] > nums[j])
                    {
                        currentMax = Math.Max(currentMax, dp[j]);
                    }

                }

                dp[i] = currentMax + 1;
                max = Math.Max(max, dp[i]);
            }

            return max;
        }

        public int FindMinTwo(int[] nums)
        {
            int l = 0;
            int r = nums.Length - 1;
            while (l < r)
            {

                int mid = l + (r - l) / 2;

                if (nums[mid] < nums[r])
                {
                    // mid to r is sorted. Searching left to mid.
                    r = mid;
                }
                else if (nums[mid] > nums[r])
                {
                    // start to mid is sorted. search mid + 1 to right. Don't need to include mid since it is already large then right.
                    l = mid + 1;
                }
                else
                {
                    // nums[mid] == nums[r]
                    r--; // Remove duplicate at h since we return nums[l].
                }
            }

            return nums[l];

        }

        public int ReversePairs(int[] nums)
        {
            int n = nums.Count();
            var data = new List<int>(nums);
            data.Sort();

            var bit = new BiTree(n);
            int result = 0;
            for (int i = 0; i < n; i++)
            {
                int num = nums[i];
                long t = num * 2L;
                // i is the total scanned elements currently, BIT query returns the count of elements which equal or less than 2 * num,
                // so the subtraction is the count of ones greater than 2 * num.
                result += (i - bit.Query(GetIndex(data, t)));
                bit.Update(GetIndex(data, num), 1);
            }

            return result;
        }

        // Returns the right-most position if exists, otherwise returns floor index
        private int GetIndex(List<int> data, long t)
        {
            int low = 0, mid = 0, high = data.Count - 1;
            if (data[low] > t) return low;
            if (data[high] <= t) return high + 1;

            while (high - low > 1)
            {
                mid = (high - low) / 2 + low;
                if (data[mid] > t) high = mid;
                else low = mid;
            }

            // The BIT index starts from 1
            return low + 1;
        }
    }

    public class BiTree
    {
        private int[] data;
        public BiTree(int n)
        {
            data = new int[n + 1];
        }

        public void Update(int i, int v)
        {
            while (i < data.Count())
            {
                data[i] += v;
                i += GetLowBit(i);
            }
        }

        public int Query(int i)
        {
            int sum = 0;
            while (i > 0)
            {
                sum += data[i];
                i -= GetLowBit(i);
            }

            return sum;
        }

        private int GetLowBit(int x)
        {
            return x & (-x);
        }
    }
}
