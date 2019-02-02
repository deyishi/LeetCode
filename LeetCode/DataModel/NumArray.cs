using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode.DataModel
{
    public class NumArray
    {
        private readonly int[] _nums;
        public NumArray(int[] nums)
        {
            _nums = nums;
            // Count total at num[i]
            for (int i = 1; i < _nums.Length; i++)
            {
                _nums[i] = _nums[i] + _nums[i - 1];
            }
        }

        public int SumRange(int i, int j)
        {
            if (i == 0)
            {
                return _nums[j];
            }

            return _nums[j] - _nums[i - 1];
        }
    }
}
