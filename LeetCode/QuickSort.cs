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

        public void DoQuickSort(int[] nums, int start, int end)
        {
            if (start < end)
            {
                var pivotal = PartitionArray(nums, start, end);
                DoQuickSort(nums, start, pivotal - 1);
                DoQuickSort(nums, pivotal + 1, end);
            }
        }

        private int PartitionArray(int[] nums, int start, int end)
        {
            var pivotal = nums[end];

            var pIndex = start;
            for (var i = start; i < end;i++) {
                if (nums[i] <= pivotal)
                {
                    Swap(nums, pIndex, i);
                    pIndex++;
                }
            }

            Swap(nums, pIndex, end);

            return pIndex;
        }

        public void Swap(int[]nums, int i, int j)
        {
            var temp = nums[i];
            nums[i] = nums[j];
            nums[j] = temp;
        }
    }
}
