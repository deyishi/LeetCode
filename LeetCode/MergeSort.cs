using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode
{
    /// <summary>
    /// [11,5,22,2]
    /// [11,5,][22,2]
    /// [11],[5],[22],[2]
    /// [5,11][2,22]
    /// [2,5,11,22]
    /// O(n*log(n))
    /// </summary>
    public class MergeSort
    {
        public void Sort(int[] nums)
        {
            var temp = new int[nums.Length];
            DoMergeSort(nums, 0, nums.Length - 1, temp);
        }


        private void DoMergeSort(int[] nums, int l, int r, int[] temp)
        {
            if (l >= r)
            {
                return;
            }


            var mid = l + (r - l) / 2;
            DoMergeSort(nums, l, mid, temp);
            DoMergeSort(nums, mid + 1, r, temp);
            Merge(nums, l, mid, r, temp);

        }

        private void Merge(int[] nums, int l, int mid, int r, int[] temp)
        {
            var leftIndex = l;
            var rightIndex = mid + 1;
            var index = leftIndex;
            while (leftIndex <= mid && rightIndex <= r)
            {
                if (nums[leftIndex] < nums[rightIndex])
                {
                    temp[index++] = nums[leftIndex++];
                }
                else
                {
                    temp[index++] = nums[rightIndex++];
                }
            }

            while (leftIndex <= mid)
            {
                temp[index++] = nums[leftIndex++];
            }


            while (rightIndex <= r)
            {
                temp[index++] = nums[rightIndex++];
            }

            for (var i = l; i <= r; i++)
            {
                nums[i] = temp[i];
            }
        }
    }
}
