using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeetCode.DataModel;
using NUnit.Framework;

namespace LeetCode
{
    public class LinkedListPracticeSet1
    {

        /// <summary>
        /// 21. Merge Two Sorted Lists
        /// </summary>
        /// <param name="l1"></param>
        /// <param name="l2"></param>
        /// <returns></returns>
        public ListNode MergeTwoLists(ListNode l1, ListNode l2)
        {
            // Store result
            var res = new ListNode(0);

            // Current node
            var curr = res;
            while (l1 != null && l2 != null)
            {
                if (l1.val < l2.val)
                {
                    curr.next = new ListNode(l1.val);
                    l1 = l1.next;
                }
                else
                {
                    curr.next = new ListNode(l2.val);
                    l2 = l2.next;
                }

                curr = curr.next;
            }

            if (l1 != null)
            {
                curr.next = l1;
            }
            else
            {
                curr.next = l2;
            }

            return res.next;
        }

        public ListNode DeleteDuplicates(ListNode head)
        {
            var curr = head;
            while (curr != null && curr.next != null)
            {
                if (curr.val == curr.next.val)
                {
                    curr.next = curr.next.next;
                }
                else
                {
                    curr = curr.next;
                }
            }

            return head;
        }

        [Test]
        public void HasCycle()
        {
            var node = new ListNode(1) {next = new ListNode(2)};
            node.next.next = node;
            var r = HasCycle(node);
        }
        public bool HasCycle(ListNode head)
        {
            if (head == null || head.next == null)
            {
                return false;
            }

            var walker = head;
            var runner = head;
            while (runner.next != null && runner.next.next != null)
            {
                walker = walker.next;
                runner = runner.next.next;
                if (walker == runner) return true;
            }
            return false;
        }

    }
}
