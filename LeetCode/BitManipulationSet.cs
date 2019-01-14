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
        public void SingleNumber()
        {
            var n = new int[] {1, 2, 2 ,2 ,2 ,2, 2,2};
            var r = SingleNumber(n);
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


        [Test]
        public void InsertionSortList()
        {
            var h = new[] {3, 2, 5, 1}.ToLinkedList();
            var r = InsertionSortList(h);
        }

        /// <summary>
        /// Create a new list.
        /// Loop through linked list, find the position for the current node in the new list (loop through new list find a node where val is >= current node val), then insert current node. 
        /// </summary>
        /// <param name="head"></param>
        /// <returns></returns>
        public ListNode InsertionSortList(ListNode head)
        {
            var dummy = new ListNode(0);

            while (head != null)
            {
                var node = dummy;

                // move pointer in new list to add new node if its val is smaller than current dummy[0].
                while (node.next != null && node.next.val < head.val)
                {
                    node = node.next;
                }

                //Save next step
                var temp = head.next;

                //Insert current node into new list. Node is at place where current node should be inserted, so head.next = node.next, then node.next = head. 
                head.next = node.next;
                node.next = head;

                //Move to next step
                head = temp;
            }

            return dummy.next;
        }
    }
}
