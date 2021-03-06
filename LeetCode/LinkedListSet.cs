﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LeetCode.DataModel;
using NUnit.Framework;

namespace LeetCode
{
    public class LinkedListSet
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

            var res = new ListNode(0) { next = head };
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
            for (var i = 0; i < size - n - 1; i++)
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
            var temp = new ListNode(0) { next = head };
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

        public IList<string> GenerateParenthesis(int n)
        {
            var result = new List<string>();

            GenerateParenthesisHelper(result, "", n, 0, 0);
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


        /// <summary>
        /// Complexity O(knlog(kn)), n is the average length of the lists. K number of list in lists.
        /// </summary>
        /// <param name="lists"></param>
        /// <returns></returns>
        public ListNode MergeKListsBruteForce(ListNode[] lists)
        {
            if (lists == null)
            {
                return null;
            }

            var n = new List<int>();
            
            //O(k*n)
            foreach (var l in lists)
            {
                var curr = l;
                while (curr != null)
                {
                    n.Add(curr.val);
                    curr = curr.next;
                }
            }

            var r = n.OrderBy(x => x).ToArray().ToLinkedList();
            return r;
        }

        public ListNode MergeKLists(ListNode[] lists)
        {
            if (lists == null)
            {
                return null;
            }

            var res = new ListNode(0);
            var curr = res;
            ListNode temp;
            // O(N*K)
            do
            {
                temp = FindMinNode(lists);
                curr.next = temp;
                curr = curr.next;

            } while (temp != null);

            return res.next;
        }

        /// <summary>
        /// Find the min node and remove it from node lists.
        /// O(k)
        /// </summary>
        /// <param name="lists"></param>
        /// <returns></returns>
        public ListNode FindMinNode(ListNode[] lists)
        {
            if (lists == null)
            {
                return null;
            }

            var min = int.MaxValue;
            var index = -1;
            for (var i = 0; i < lists.Length; i++) {
                if (lists[i] != null && lists[i].val < min)
                {
                    min = lists[i].val;
                    index = i;
                }
            }

            ListNode result = null;
            if (index != -1)
            {
                result = lists[index];
                // selected min node from the list 
                lists[index] = lists[index].next;
            }

            return result;
        }

        public ListNode SwapPairs(ListNode head)
        {
            if (head == null)
            {
                return null;
            }

            var result = new ListNode(0) {next = head};
            var curr = result;

            while (curr.next != null && curr.next.next != null)
            {
                var temp = curr.next.next;
                curr.next.next = temp.next;
                temp.next = curr.next;
                curr.next = temp;
                curr = curr.next.next;
            }

            return result.next;

        }

        public ListNode ReverseKGroup(ListNode head, int k)
        {
            if (head == null || head.next == null || k == 0)
            {
                return head;
            }

            // First run
            // 1 2 3 4 5
            // count = 0 curr = 1
            // count = 1 curr = 2
            // count = 2 curr = 3

            // Second run
            // 3 4 5
            // count = 0 curr = 3
            // count = 1 curr = 4
            // count = 2 curr = 5

            var count = 0;
            var curr = head;
            while (curr != null && count < k)
            {
                curr = curr.next;
                count++;
            }

            if (count == k)
            {
                curr = ReverseKGroup(curr, k);
                while (count-- > 0)
                {
                    var temp = head.next;
                    head.next = curr;
                    curr = head;
                    head = temp;
                }

                head = curr;
            }

            return head;
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

        public ListNode OddEvenList(ListNode head)
        {
            if (head == null)
            {
                return null;
            }

            ListNode odd = head;
            ListNode even = head.next;
            ListNode evenHead = even; 

            // Link all the odd together and even together, in the end link end of the odd with even head.
            // While even != null && even.next != null as stop point, since we will end on even.
            while (even != null && even.next != null)
            {
                odd.next = odd.next.next;
                even.next = even.next.next;
                odd = odd.next;
                even = even.next;
            }

            odd.next = evenHead;

            return head;
        }

        public RandomListNode CopyRandomList(RandomListNode head)
        {
            if (head == null)
            {
                return null;
            }

            //In place copy
            CopyNext(head);
            CopyRandomNode(head);
            return ExtractCopiedNode(head);
        }

        private RandomListNode ExtractCopiedNode(RandomListNode head)
        {
            RandomListNode r = head.next;
            while (head != null)
            {
                // 1 -> Copy 1 -> 2 -> Copy 2
                var temp = head.next;
                head.next = temp.next;
                head = head.next;
                if (temp.next != null)
                {
                    temp.next = temp.next.next;
                }
            }

            return r;
        }

        private void CopyRandomNode(RandomListNode head)
        {
            while (head != null)
            {
                if (head.next.random != null)
                {
                    // OriginalNode.random.next is the copy of OriginalNode.random 
                    // Replace Copied node.random with the copy
                    head.next.random = head.random.next;
                }

                head = head.next.next;
            }
        }

        private void CopyNext(RandomListNode head)
        {
            while (head != null)
            {
                var copyNode = new RandomListNode(head.label)
                {
                    next = head.next,
                    random = head.random
                };
                head.next = copyNode;
                head = head.next.next;
            }
        }

        public void ReorderList(ListNode head)
        {
            if (head == null || head.next == null) return;

            //Find mid 
            ListNode preMid = head;
            ListNode end = head;
            while (end.next != null && end.next.next != null)
            {
                preMid = preMid.next;
                end = end.next.next;
            }

            //Reverse the second half
            ListNode curr = preMid.next;
            ListNode prev = null;
            while (curr != null)
            {
                ListNode next = curr.next;
                curr.next = prev;
                prev = curr;
                curr = next;
            }

            preMid.next = prev;

            // Start reorder 
            ListNode p1 = head;
            ListNode p2 = preMid.next;
            while (p1 != preMid)
            {
                preMid.next = p2.next; // Save p2 next
                p2.next = p1.next; // Update p2 next
                p1.next = p2; // Update p1 next
                p1 = p2.next; // Move p1
                p2 = preMid.next; // Move p2.
            }

        }
    }
}
