using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using LeetCode.DataModel;
using NUnit.Framework;

namespace LeetCode.Nine_Chapter
{
    public class L2BinarySearch
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

        public int SearchInsert(int[] A, int target)
        {
            if (A == null || A.Length == 0)
            {
                return 0;
            }

            var start = 0;
            var end = A.Length;
            while (start + 1 < end -1)
            {
                var mid = start  + (end - start)/2;
                if (A[mid] == target)
                {
                    return mid;
                }

                if (A[mid] < target)
                {
                    start = mid + 1;
                }
                else
                {
                    end = mid - 1;
                }

            }

            if (A[start]  >= target)
            {
                return start;
            }
            if (A[end] >= target)
            {
                return end;
            }

            return end + 1;

        }

        [Test]
        public void MySqrt()
        {
            var r = MySqrt(8);
        }
        public int MySqrt(int x)
        {
            if (x < 2)
            {
                return x;
            }

            var start = 1;
            var end = x/2;

            while (start <= end)
            {
                var mid = start + (end - start) / 2;

                if (x / mid < mid)
                {
                    end = mid-1;
                }
                else if(x / mid > mid)
                {
                    start = mid + 1;
                }
                else
                {
                    return mid;
                }
            }

            return start - 1;
        }

        public int[] TwoSum(int[] numbers, int target)
        {
            if (numbers == null || numbers.Length < 2)
            {
                return null;
            }

            var start = 0;
            var end = numbers.Length - 1;
            while (start < end)
            {
                var curr = numbers[start] + numbers[end];
                if (curr > target)
                {
                    end--;
                }else if (curr < target)
                {
                    start++;
                }
                else
                {
                    return new[] {start+1, end+1};
                }
            }

            return null;
        }

        [Test]
        public void ClosestValue()
        {
            var b = new[] {4, 2, 5, 1, 3};
            var t = b.ToTree();
            var r = ClosestValue(t, 3.7);
        }

        public int ClosestValue(TreeNode root, double target)
        {
            if (root == null)
            {
                return 0;
            }

            var result = root.val;
            var curr = root;
            while (curr != null)
            {
                result = Math.Abs(curr.val - target) < Math.Abs(result - target) ? curr.val : result;
                curr =  target < curr.val ? curr.left : curr.right;
            }

            return result;
        }
        public int[] IntersectionOne(int[] nums1, int[] nums2)
        {
            if (nums1 == null || nums2 == null)
            {
                return null;
            }
            if (nums1.Length > nums2.Length)
            {
               return IntersectionOne(nums2, nums1);
            }

            var set = new HashSet<int>();

            foreach (var t in nums1)
            {
                if (nums2.Contains(t))
                {
                    set.Add(t);
                }
            }

            return set.ToArray();
        }

        public int[] IntersectionTwo(int[] nums1, int[] nums2)
        {
            if (nums1 == null || nums2 == null)
            {
                return null;
            }

            Array.Sort(nums1);
            Array.Sort(nums2);
            var i = 0;
            var j = 0;
            var result = new HashSet<int>();
            while (i < nums1.Length && j < nums2.Length)
            {
                if (nums1[i] < nums2[j])
                {
                    i++;
                }
                else if (nums1[i] > nums2[j])
                {
                    j++;
                }
                else
                {
                    result.Add(nums1[i]);
                    i++;
                    j++;
                }
            }

            return result.ToArray();
        }

        public bool IsPerfectSquare(int num)
        {
            if (num < 2)
            {
                return true;
            }

            var start = 2;
            var end = num / 2;
            while (start <= end)
            {
                long mid = start + (end - start) / 2;
                if (num/mid == mid)
                {
                    return true;
                }

                if (mid * mid  < num)
                {
                    start = (int) mid + 1;
                }
                else
                {
                    end = (int) mid - 1;
                }
            }

            return false;
        }
    }
}
