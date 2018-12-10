using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace LeetCode.Nine_Chapter
{
    public class L2
    {
        [Test]
        public void FindMin()
        {
            var n = new[] {4,1,2,3};
            var r = FindMinTwo(n);
        }
        public int FindMin(int[] nums)
        {
            if (nums == null || nums.Length < 1)
            {
                return -1;
            }

            return FindMinHelper(nums, 0, nums.Length - 1);

        }

        public int FindMinHelper(int[] nums, int start, int end)
        {
            if (nums[start] <= nums[end])
            {
                return nums[start];
            }

            var mid = start + (end - start) / 2;

            return Math.Min(FindMinHelper(nums, start, mid), FindMinHelper(nums, mid + 1, end));

        }

        public int FindMinTwo(int[] nums)
        {
            if (nums == null || nums.Length == 0)
            {
                return -1;
            }

            int start = 0, end = nums.Length - 1;
            int target = nums[end];

            // find the first element <= target
            while (start + 1 < end)
            {
                int mid = start + (end - start) / 2;
                if (nums[mid] <= target)
                {
                    end = mid;
                }
                else
                {
                    start = mid;
                }
            }
            if (nums[start] < target)
            {
                return nums[start];
            }

            return nums[end];
        }

        public int Search(int[] nums, int target)
        {
            if (nums == null || nums.Length < 1)
            {
                return -1;
            }

            int start = 0, end = nums.Length - 1;

            // find the first element <= target
            while (start + 1 < end)
            {
                var mid = start + (end - start) / 2;

                if (nums[mid] == target)
                {
                    return mid;
                }

                if (nums[mid] <= nums[end])
                {
                    if ( target >= nums[mid] && target <= nums[end])
                    {
                        start = mid;
                    }
                    else
                    {
                        end = mid;
                    }
                }
                else
                {
                    if ( target >= nums[start] && target <= nums[mid])
                    {
                        end = mid;
                    }
                    else
                    {
                        start = mid;
                    }
                }
            }

            if (nums[start] == target)
            {
                return start;
            }

            if (nums[end] == target)
            {
                return end;
            }

            return - 1;
        }

        public bool SearchWithDuplications(int[] nums, int target)
        {
            if (nums == null || nums.Length < 1)
            {
                return false;
            }

            int start = 0, end = nums.Length - 1;

            // find the first element <= target
            while (start + 1 < end)
            {
                var mid = start + (end - start) / 2;
                if (nums[mid] == target)
                {
                    return true;
                }

                if ((nums[start] == nums[mid]) && (nums[end] == nums[mid]))
                {
                    start++;
                    end--;
                }
                else if (nums[mid] <= nums[end])
                {
                    if (target >= nums[mid] && target <= nums[end])
                    {
                        start = mid;
                    }
                    else
                    {
                        end = mid;
                    }
                }
                else
                {
                    if (target >= nums[start] && target <= nums[mid])
                    {
                        end = mid;
                    }
                    else
                    {
                        start = mid;
                    }
                }
            }

            if (nums[start] == target)
            {
                return true;
            }

            if (nums[end] == target)
            {
                return true;
            }

            return false;
        }

        [Test]
        public void PeakIndexInMountainArray()
        {
            var n = new[] {24, 69, 100, 99, 79, 78, 67, 36, 26, 19};
            var r = PeakIndexInMountainArray(n);

        }
        public int PeakIndexInMountainArray(int[] a)
        {
            if (a == null || a.Length < 1)
            {
                return -1;
            }

            var start = 0;
            var end = a.Length - 1;

            while (start  + 1< end)
            {
                var mid = start + (end - start) / 2;

                // Decreasing
                if (a[mid] > a[mid + 1])
                {
                    end = mid;
                }
                else
                {
                    // Increasing Order
                    start = mid;
                }

            }

            return Math.Max(a[start], a[end]);

        }

        [Test]
        public void SearchRange()
        {
            var n = new[] {2,2};
            var r = SearchLast(n, 2);
        }
        public int[] SearchRange(int[] nums, int target)
        {
            var result = new[] { -1, -1 };
            if (nums == null || nums.Length < 1)
            {
                return result;
            }

            var start = 0;
            var end = nums.Length - 1;

            while (start < end)
            {
                var mid = start + (end - start) / 2;
                if (nums[mid] < target)
                {
                    start = mid + 1;
                }
                else
                {
                    end = mid;
                }
            }

            if (nums[start] == target)
            {
                result[0] = start;
            }
            else
            {
                return result;
            }

            end = nums.Length - 1;
            while (start < end)
            {
                var mid = start + (end - start) / 2 + 1;
                if (nums[mid] > target)
                {
                    end = mid - 1;
                }
                else
                {
                    start = mid;
                }
            }

            if (nums[start] == target)
            {
                result[1] = start;
            }
            return result;
        }

        public int[] SearchLast(int[] nums, int target)
        {
            var result = new[] { -1, -1 };
            if (nums == null || nums.Length < 1)
            {
                return result;
            }

            var start = 0;
            var end = nums.Length - 1;
            while (start + 1 < end)
            {
                int mid = start + (end - start) / 2;
                if (nums[mid] >= target)
                {
                    end = mid;
                }
                else
                {
                    start = mid;
                }
   
            }

            if (nums[start] == target)
            {
                result[0] = start;
            }else if (nums[end] == target)
            {
                result[0] = end;
            }
            else
            {
                return result;
            }

            start = result[0];
            end = nums.Length - 1;
            while (start + 1 < end)
            {
                var mid = start + (end - start) / 2;
                if (nums[mid] <= target)
                {
                    start = mid;
                }
                else 
                {
                    end = mid;
                }
            }

            if (nums[end] == target)
            {
                result[1] = end;
            }else if (nums[start] == target)
            {
                result[1] = start;
            }

            return result;
        }


        [Test]
        public void FindPeakElement()
        {
            var n = new[] { 1};

            var r = FindPeakElement(n);
        }

        public int FindPeakElement(int[] nums)
        {

            var start = 0;
            var end = nums.Length - 1;
            while (start < end)
            {
                var mid = start + (end - start) / 2;
                if (nums[mid]  < nums[mid+1])
                {
                    start = mid + 1;
                }
                else
                {
                    end = mid;
                }
            }

            return start;
        }

        [Test]
        public void FindClosestElements()
        {
            
        }
        public IList<int> FindClosestElements(int[] arr, int k, int x)
        {
            var start = 0;
            var end = arr.Length - k;
            while (start < end)
            {
                var mid = start + (end - start) / 2;

                if (x > arr[mid])
                {
                    if (x - arr[mid] > arr[mid + k] -x)
                    {
                         start = mid + 1;
                    }
                    else
                    {
                        end = mid;
                    }
                }
                else
                {
                    end = mid;
                }

            }

            var res = new List<int>();
            for (var i = 0; i < k; i++) res.Add(arr[start + i]);
            return res;
        }
    }
}
