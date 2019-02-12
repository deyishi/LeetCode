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

            var t = 16 & 15;
            var n = new int[] { 1, 2, 2, 2, 2, 2, 2, 2 };
            var b = 1101;

            t = (6) & -6;
            //var r = ReverseBits(b);
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

        //190. Reverse Bits
        //Check 0
        //shift result to left
        //n & 1 == 1 check last digit, add to result
        //n: 1101 -> 0110 -> 0011 -> 0001 -> 0000
        //r: 0000 -> 0001 -> 0010 -> 0101 -> 1011 
        //shift n to right
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

        //191. Number of 1 Bits
        //    Count current last bit
        //    Shift number to right by 1.
        public int HammingWeight(uint n)
        {
            if (n == 0)
            {
                return 0;
            }
            var res = 0;
            while (n != 0)
            {
                if ((n & 1) == 1)
                {
                    res++;
                }

                n >>= 1;
            }

            return res;
        }

        //201. Bitwise AND of Numbers Range
        //1 1 0
        //1 0 1
        //Keep shifting right m and n by 1 to remove last digit difference(odd and even) until they are the same.
        public int RangeBitwiseAnd(int m, int n)
        {
            if (m == n)
            {
                return n;
            }
            if (n - m == 1)
            {
                return n & m;
            }

            var shift = 0;
            while (m != n)
            {
                m >>= 1;
                n >>= 1;
                shift++;
            }

            return m << shift;
        }

        public bool IsPowerOfTwo(int n)
        {
            // Keep dividing n by 2 or check n & n-1, since power of 2 only has 1 bit.
            while (n % 2 == 0)
            {
                n /= 2;
            }

            return n == 1;
        }

        public bool IsPowerOfFour(int n)
        {
            return n > 0 && (n & (n - 1)) == 0 && (n - 1) % 3 == 0;
        }
        public int[] SingleNumberThree(int[] nums)
        {
            var result = new int[2];
            // Eliminate elements appeared twice with XOR, what left will the two single appearance element XOR.
            var diff = 0;
            foreach (var n in nums)
            {
                diff ^= n;
            }

           //Get its last set bit
           diff &= -diff;

            foreach (var n in nums)
            {
                if ((diff & n) == 0)  // the bit is not set
                {
                    result[0] ^= n;
                }
                else
                {
                    result[1] ^= n;  // the bit is set

                }
            }

            return result;
        }

        public int MaxProduct(string[] words)
        {
            // Convert each word into a 32 bit integer which 0 bit corresponds to 'a', 1 bit corresponds to 'b' and so on.
            // If two words share the same character, then and of their big integer should result != 0.

            var wordIntegers = new int[words.Length];
            // convert each words into bit integer
            for (var i = 0; i < words.Length; i++)
            {

                var word = words[i];
                var num = 0;
                foreach (var c in word)
                {
                    // | or operator, 1 = 01, 2 = 10, 1|2 = 11. 
                    // a = 01
                    // b = 10
                    // ab = 11
                    num |= 1 << (c - 'a');
                }

                wordIntegers[i] = num;
            }

            var max = 0;
            for (var i = 0; i < words.Length; i++)
            {
                for (int j = 0; j < words.Length; j++)
                {
                    if ((wordIntegers[i] & wordIntegers[j]) == 0)
                    {
                        max = Math.Max(words[i].Length * words[j].Length, max);
                    }
                    
                }
            }

            return max;
        }
    }
}
