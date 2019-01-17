using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace LeetCode.Nine_Chapter
{
    class L7
    {

        [Test]
        public void WinSum()
        {
            var n = new[] {1, 2, 7, 8, 5};
            var r = WinSum(n, 3);
        }
        public int[] WinSum(int[] nums, int k)
        {
            // write your code here

            if (nums == null || nums.Length == 0 || k > nums.Length || k == 0)
            {

                return new int[0];

            }

            int[] result = new int[k];
            // Find first k
            int sum = 0;
            for (int i = 0; i < k; ++i)
            {
                sum += nums[i];
            }

            result[0] = sum;
            for (int i = k; i< nums.Length; i++)
            {
                sum = sum - nums[i - k] + nums[i];
                result[i - k + 1] = sum;
            }

            return result;
        }

        [Test]
        public void MoveZeroes()
        {
            var n = new[] {1, 2, 0, 5, 6};
            MoveZeroes(n);
        }

        public void MoveZeroes(int[] nums)
        {
            int rightOfNoneZero = 0;
            int i = 0;
            while (i < nums.Length)
            {
                if (nums[i] != 0)
                {
                    int temp = nums[rightOfNoneZero];
                    nums[rightOfNoneZero] = nums[i];
                    nums[i] = temp;
                    rightOfNoneZero++;
                }
                i++;
            }
        }

        public int RemoveDuplicates(int[] nums)
        {
            if (nums == null || nums.Length == 0)
            {
                return 0;
            }


            var i = 1;
            var noneDuplicateCountInsertPosition = 1; // First number is always none duplicate.
            while (i < nums.Length)
            {
                if (nums[i-1] != nums[i])
                {
                    //Insert none duplicate and update noneDuplicateCountInsertPosition
                    nums[noneDuplicateCountInsertPosition] = nums[i];
                    noneDuplicateCountInsertPosition++;
                }

                i++;
            }

            return noneDuplicateCountInsertPosition;
        }


        public IList<IList<int>> ThreeSum(int[] nums)
        {
            var result = new List<IList<int>>();
            if (nums == null || nums.Length == 0)
            {
                return result;
            }
            Array.Sort(nums);
            var n = nums.Length;
            for (var i = 0; i < n; i++)
            {
                if (nums[i] > 0)
                {
                    return result;
                }

                if (i > 0 && nums[i] == nums[i-1])
                {
                    continue;
                }

                var curr = nums[i];
                var l = i + 1;
                var r = n - 1;
                while (l < r)
                {

                    var sum = nums[l] + nums[r] + curr;
                    if (sum == 0)
                    {
                        result.Add(new List<int> { nums[i], nums[l], nums[r] });
                        l++;
                        r--;
                        while (l < r && nums[l] == nums[l-1])
                        {
                            l++;
                        }

                        while (l < r && nums[r] == nums[r+1])
                        {
                            r--;
                        }
                    }
                    else if (sum < 0)
                    {
                        l++;
                    }
                    else
                    {
                        r--;
                    }
                }
            }

            return result;
        }
    }
}
