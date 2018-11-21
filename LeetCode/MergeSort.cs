using System;
using System.Collections.Generic;
using System.Data.Common;
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
            DoMergeSort(nums, 0, nums.Length - 1);
        }

        private void DoMergeSort(int[] nums, int l, int r)
        {
            if (l < r)
            {
                var mid = (l + r) / 2;
                DoMergeSort(nums, l, mid);
                DoMergeSort(nums, mid + 1, r);

                Merge(nums, l, mid, r);
            }

        }

        private void Merge(int[] nums, int l, int mid, int r)
        {
            var leftSubArrayLength = mid - l + 1;
            var rightSubArrayLength = r - mid;

            var leftSubArray = new int[leftSubArrayLength];
            var rightSubArray = new int[rightSubArrayLength];

            int i;
            int j;
            for (i = 0; i < leftSubArrayLength; i++)
            {
                leftSubArray[i] = nums[l + i];
            }

            for (j=0; j < rightSubArrayLength;j++)
            {
                rightSubArray[j] = nums[mid + 1 + j];
            }

            i = 0;
            j = 0;
            var c = l;
            while (i < leftSubArrayLength && j < rightSubArrayLength)
            {
                if (leftSubArray[i] < rightSubArray[j])
                {
                    nums[c] = leftSubArray[i];
                    i++;
                }
                else
                {
                    nums[c] = rightSubArray[j];
                    j++;
                }

                c++;
            }

            while (i < leftSubArrayLength)
            {
                nums[c] = leftSubArray[i];
                i++;
                c++;
            }

            while (j < rightSubArrayLength)
            {
                nums[c] = rightSubArray[j];
                j++;
                c++;
            }

        }
    }
}
