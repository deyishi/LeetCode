using System;
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

            var r = SearchMatrix(m, 5);
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

            var n = nums.Length;
            var l = 0;
            var r = n - 1;
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
    }
}
