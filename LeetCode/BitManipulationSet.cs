using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeetCode.DataModel;
using NUnit.Framework;

namespace LeetCode
{
    public class BitManipulationSet
    {
        [Test]
        public void Test()
        {
            var n = new int[] {1, 2, 2 ,2 ,2 ,2, 2,2};
            var b = 1101;
            var r = ReverseBits(b);
        }
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


        public uint ReverseBits(uint n)
        {
            if (n == 0)
            {
                return 0;
            }

            uint res = 0;
            for (int i = 0; i < 32; i++)
            {
                //Move result to left
                res <<= 1;

                //Check n's last digit, add to result.
                if ((n & 1) == 1)
                {
                    res++;
                }

                //Shift n to right, update last digit
                n >>= 1;
            }
            return res;
        }
    }
}
