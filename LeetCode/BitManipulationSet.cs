using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace LeetCode
{
    class BitManipulationSet
    {

        /// <summary>
        ///  once: store the number in one
        ///  twice: store the number in two and clear one
        ///  thrice: clear two and one remain same
        ///  Current 	 Incoming 	  Next
        ///ones twos              ones twos
        ///0	 0		 n         n	0	
        ///n     0       n         0    n
        ///0     n       n         0    0
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public int SingleNumber(int[] nums)
        {
            if (nums == null || nums.Length == 0)
            {
                return -1;
            }

            var ones = 0;
            var twos = 0;
            foreach (var n in nums)
            {
                
                ones = ones ^ n & ~twos;
                twos = twos ^ n & ~ones;
            }

            return ones;
        }
    }
}
