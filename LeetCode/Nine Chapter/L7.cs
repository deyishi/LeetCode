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


        //611. Valid Triangle Number
        //    Sort Array.
        //    Loop through numbers from the end and use two pointers to find the pair that adds up large than current number.
        //    If a pair is found, all the numbers after that pair start add to the pair end is also large than current number(Since we want to count duplicate use end - start).
        public int TriangleNumber(int[] nums)
        {
            var result = 0;
            if (nums == null || nums.Length < 3)
            {
                return result;
            }
            Array.Sort(nums);
            var n = nums.Length;
            for (var i = n-1; i >= 2; i--)
            {
                var l = 0;
                var r = i - 1;
                while (l < r)
                {
                    if (nums[l] + nums[r] > nums[i])
                    {
                        //All the pairs between l and r are large than nums[i].
                        result += r - l;
                        r--;
                    }
                    else
                    {
                        l++;
                    }
                }
            }

            return result;
        }
    }
}
