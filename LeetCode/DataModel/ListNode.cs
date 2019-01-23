using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode.DataModel
{
    public class ListNode
    {

        public int val;
        public ListNode next;
        public ListNode(int x) { val = x; }

    }

    public class RandomListNode
    {
        public int label;
        public RandomListNode next, random;
        public RandomListNode(int x) { this.label = x; }
    }

    public class DoublyListNode
    {
        int val;
        public DoublyListNode next;
        public DoublyListNode prev;
        public DoublyListNode(int val)
        {
            this.val = val;
            next = prev = null;
        }
    }
}
