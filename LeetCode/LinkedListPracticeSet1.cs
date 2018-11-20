using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeetCode.DataModel;
using NUnit.Framework;
using NUnit.Framework.Interfaces;

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


        [Test]
        public void GetIntersectionNode()
        {
            var a = new[] {1, 2 };
            var b = new[] {1, 2, 3};
            var c = new[] {4, 5, 6};
            var al = CreateLinkedListFromArray(a);
            var bl = CreateLinkedListFromArray(b);
            var cl = CreateLinkedListFromArray(c);
            al.next.next = cl;
            bl.next.next.next = cl;
            var r = GetIntersectionNodeTwo(al,bl);
        }

        public ListNode GetIntersectionNode(ListNode headA, ListNode headB)
        {
            if (headA == null || headB == null) return null;

            // Make two list the same size by cutting longer one.
            var aLength = FindLinkedListLength(headA);
            var bLength = FindLinkedListLength(headB);

            if (aLength > bLength)
            {
                while (aLength != bLength)
                {
                    headA = headA.next;
                    aLength--;
                }
            }
            else
            {
                while (aLength != bLength)
                {
                    headB = headB.next;
                    bLength--;
                }
            }

            while (headA != headB)
            {
                headA = headA.next;
                headB = headB.next;
            }

            return headA;
        }

        public ListNode GetIntersectionNodeTwo(ListNode headA, ListNode headB)
        {
            if (headA == null || headB == null) return null;
            ListNode a = headA, b = headB;
            while (a != b)
            {

                if (a != null)
                {
                    a = a.next;
                }
                else
                {
                    a = headB;
                }

                if (b != null)
                {
                    b = b.next;
                }
                else
                {
                    b = headA;
                }
            }

            return a;
        }


 

        public ListNode RemoveElements(ListNode head, int val)
        {
            if (head == null)
            {
                return null;
            }

            var res = new ListNode(0){next = head};
            var prevNode = res;
            while (head != null)
            {
                if (head.val != val)
                {
                    prevNode = head;
                }
                else
                {
                    prevNode.next = head.next;
                }

                head = head.next;
            }

            return res.next;
        }

        public ListNode ReverseList(ListNode head)
        {
            if (head == null) return null;
            ListNode pre = null;
            ListNode curr = head;

            while (curr != null)
            {
                var next = curr.next;
                curr.next = pre;
                pre = curr;
                curr = next;
            }

            return pre;
        }
        public bool IsPalindrome(ListNode head)
        {
            if (head == null || head.next == null)
            {
                return true;
            }

            var stack = new Stack<int>();
            stack.Push(head.val);
            var fast = head;
            var slow = head;

            // Find mid
            while (fast.next != null && fast.next.next != null)
            {
                slow = slow.next;
                fast = fast.next.next;
                stack.Push(slow.val);
            }

            // It's odd num remove the mid for checking.
            if (fast.next == null)
            {
                stack.Pop();
            }

            while (slow.next != null)
            {
                slow = slow.next;
                var temp = stack.Pop();
                if (temp != slow.val)
                {
                    return false;
                }
            }

            return true;
        }

        [Test]
        public void LinkedListTest()
        {
            var a = new[] { 1, 2, 3, 3, 2, 1 };
            var al = CreateLinkedListFromArray(a);
            var r = IsPalindromeTwo(al);
        }

        public bool IsPalindromeTwo(ListNode head)
        {
            if (head == null || head.next == null)
            {
                return true;
            }

            var fast = head;
            var slow = head;

            // Find mid
            while (fast.next != null && fast.next.next != null)
            {
                slow = slow.next;
                fast = fast.next.next;
            }

            // Reverse from mid to end
            var curr = slow.next;
            ListNode prev = null;
            while (curr != null)
            {
                var next = curr.next;
                curr.next = prev;
                prev = curr;
                curr = next;
            }

            slow.next = prev;

            while (slow.next != null)
            {
                slow = slow.next;
                if (head.val != slow.val)
                {
                    return false;
                }

                head = head.next;
            }

            return true;
        }

        public static int FindLinkedListLength(ListNode list)
        {
            var i = 0;
            var curr = list;
            while (curr != null)
            {
                i++;
                curr = curr.next;
            }

            return i;
        }

        public static ListNode CreateLinkedListFromArray(int[] a)
        {
            if (a == null || a.Length < 1)
            {
                return null;
            }

            var head = new ListNode(a[0]);
            var curr = head;
           
            for (var i = 1; i < a.Length; i++)
            {
                curr.next = new ListNode(a[i]);
                curr = curr.next;
            }

            return head;
        }

        [Test]
        public void MyLinkedList()
        {
            var linkedList = new MyLinkedList();
              // linked list becomes 1->2->3
            var t = linkedList.GetSize(); // returns 3
        }

        public ListNode MiddleNode(ListNode head)
        {
            if (head == null)
            {
                return null;
            }

            var slow = head;
            var fast = head;
            while (fast.next != null && fast.next.next != null)
            {
                slow = slow.next;
                fast = fast.next.next;
            }

            return fast.next != null ? slow.next : slow;
        }

        [Test]
        public void RemoveNthFromEnd()
        {
            var a = new int[] {1, 2, 3, 4, 5};

            var l = CreateLinkedListFromArray(a);

            var r = RemoveNthFromEndTwo(l, 2);
        }

        public ListNode RemoveNthFromEnd(ListNode head, int n)
        {
            if (head == null)
            {
                return null;
            }

            var size = 0;
            var curr = head;
            while (curr != null)
            {
                curr = curr.next;
                size++;

            }

            if (n == size)
            {
                return head.next;
            }

            // Find the node before the node for delete.
            curr = head;
            for (var i = 0; i < size -n -1;i++)
            {
                curr = curr.next;
            }

            curr.next = curr.next.next;

            return head;
        }


        /// <summary>
        /// O(n)
        /// </summary>
        /// <param name="head"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public ListNode RemoveNthFromEndTwo(ListNode head, int n)
        {
            if (head == null)
            {
                return null;
            }

            // In case we need to remove the first element of list.
            var temp = new ListNode(0) {next = head};
            var fast = temp;
            var slow = temp;

            // Left fast moves n step before slow. Use <= here since we added a head node.
            for (var i = 0; i <= n; i++)
            {
                fast = fast.next;
            }

            // Fast will reach the end n steps before slow. Slow will stop at the n -1 node.
            // One cycle
            while (fast != null)
            {
                slow = slow.next;
                fast = fast.next;
            }

            slow.next = slow.next.next;

            // For case n is the list size, we need to remove first node.
            // Temp: 0 -> 1 -> 2 -> 3 -> 4 -> 5
            // Fast:                              F
            // Slow: S

            return temp.next;

        }

        [Test]
        public void GenerateParenthesis()
        {
            var r = GenerateParenthesis(3);
        }
        public IList<string> GenerateParenthesis(int n)
        {
            var result = new List<string>();

            GenerateParenthesisHelper(result, "", n, 0, 0 );
            return result;
        }

        private void GenerateParenthesisHelper(List<string> result, string curr, int n, int leftPCount, int rightPCount)
        {
            // we have closed n parenthesis
            if (rightPCount == n)
            {
                result.Add(curr);
                return;
            }

            if (leftPCount < n)
            {
                GenerateParenthesisHelper(result, curr + "(", n, leftPCount + 1, rightPCount);
            }

            if (rightPCount < leftPCount)
            {
                GenerateParenthesisHelper(result, curr + ")", n, leftPCount, rightPCount + 1);
            }

        }

    }
}
