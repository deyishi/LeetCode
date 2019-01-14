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
            var nums = new[] { 3, 2, 3, 1, 2, 4, 5, 5, 6 };
            DoQuickSortOne(nums, 0, nums.Length - 1);
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
                var mid = start + (end - start) / 2;
                Swap(nums, mid, end);

                var p = nums[end];
                while (left < right)
                {
                    if (nums[left++] > p)
                    {
                        Swap(nums, --left, --right);
                    }
                }
                Swap(nums, left, end);

                DoQuickSortOne(nums, start, left - 1);
                DoQuickSortOne(nums, left + 1, end);
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
