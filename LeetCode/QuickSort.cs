using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace LeetCode
{
    public class QuickSort
    {

        [Test] 
        public void Test()
        {
            var nums = new[] {11, 4, 6, 7, 12};
            DoQuickSortTwo(nums, 0, nums.Length - 1);
        }

        public void DoQuickSort(int[] nums, int start, int end)
        {
            if (start < end)
            {
                var p = nums[start];

                var pIndex = end;
                for (var i = end; i > start; i--) {
                    if (nums[i] >= p)
                    {
                        var temp = nums[i];
                        nums[i] = nums[pIndex];
                        nums[pIndex] = temp;
                        pIndex--;
                    }
                }

                Swap(nums, pIndex, start);
                DoQuickSort(nums, start, pIndex - 1);
                DoQuickSort(nums, pIndex + 1, end);
            }
        }

        public void DoQuickSortOne(int[] nums, int start, int end)
        {
            if (start < end)
            {
                var left = 0;
                var right = end;

                var p = nums[(start + end)/ 2];

                while (left <= right)
                {
                    while (left <= right && nums[left] < p)
                    {
                        left++;
                    }

                    while (left <= right && nums[right] > p)
                    {
                        right--;
                    }

                    if (left <= right)
                    {
                        Swap(nums, left, right);
                        left++;
                        right--;
                    }

                }

                DoQuickSortOne(nums, start, right);
                DoQuickSortOne(nums, left, end);
            }
        }

        public void DoQuickSortTwo(int[] nums, int start, int end)
        {
            if (start < end)
            {
                var left = 0;
                var right = end;

                var p = nums[(start + end) / 2];

                while (left < right)
                {
                    while (nums[left] < p)
                    {
                        left++;
                    }

                    while (nums[right] > p)
                    {
                        right--;
                    }

                    Swap(nums, left, right);
                }

                DoQuickSortTwo(nums, start, left-1);
                DoQuickSortTwo(nums, left+1, end);
            }
        }


        public void Swap(int[]nums, int i, int j)
        {
            var temp = nums[i];
            nums[i] = nums[j];
            nums[j] = temp;
        }
    }
}
