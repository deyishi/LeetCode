using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeetCode.DataModel;
using NUnit.Framework;

namespace LeetCode.Nine_Chapter
{
    class L6
    {
        public ListNode ReverseBetween(ListNode head, int m, int n)
        {
            if (head == null)
            {
                return null;
            }

            var dummyNode = new ListNode(0) {next = head};

            var prev = dummyNode;
            for (var i = 0; i < m - 1; i++)
            {
                prev = prev.next;
            }

            var mNode = prev.next;

            var nNode = head;
            for (var i = 1; i < n;i++)
            {
                nNode = nNode.next;
            }

            while (mNode != nNode)
            {
                // Use p to find m, p point position doesn't change m pointing position doesn't change, only whats in the position change.
                prev.next = mNode.next;

                // Put m behind n.
                mNode.next = nNode.next;
                nNode.next = mNode;

                // Use p to find m, p point position doesn't change m pointing position doesn't change, only whats in the position change.
                mNode = prev.next;
            }

            return dummyNode.next;

        }

        [Test]
        public void IsPalindrome()
        {
            var t = new[] {1, 2, 2, 1}.ToLinkedList();

            var r = IsPalindrome(t);
        }
        public bool IsPalindrome(ListNode head)
        {
            if (head == null || head.next == null)
            {
                return true;
            }


            var slow = head;
            var fast = head;
            while (fast.next != null && fast.next.next != null)
            {
                slow = slow.next;
                fast = fast.next.next;
            }


            var mid = slow.next;
            ListNode pre = null;
            while (mid != null)
            {
                var next = mid.next;
                mid.next = pre;
                pre = mid;
                mid = next;
            }

            slow.next = pre;

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


        [Test]
        public void ReverseKGroup()
        {
            var t = new[] {1, 2, 3, 1, 2, 3, 4}.ToLinkedList();

            var r = ReverseKGroup(t, 3);
        }
        public ListNode ReverseKGroup(ListNode head, int k)
        {
            if (head == null)
            {
                return null;
            }

            var dummyNode = new ListNode(0) {next = head};

            var prev = dummyNode;
            while (prev != null)
            {
                prev = ReverseKGroupHelper(prev, k);
            }

            return dummyNode.next;
        }

        /// <summary>
        /// prev -> n1 -> n2...nk->nk+1
        /// prev -> nk -> nk-1...n1->nk+1
        /// reverse n1 to nk and return n1
        /// return null if k less than 0 or head is null or nk is null
        /// </summary>
        /// <returns></returns>
        private ListNode ReverseKGroupHelper(ListNode prev, int k)
        {
            if (k <= 0 || prev == null)
            {
                return null;
            }


            var nodeK = prev;
            var node1 = prev.next;
            for (var i = 0; i < k; i++)
            {
                if (nodeK == null)
                {
                    return null;
                }

                nodeK = nodeK.next;
            }

            if (nodeK == null)
            {
                return null;
            }

            var nodeKPlus = nodeK.next;

            ReverseK(prev, prev.next, k);

            node1.next = nodeKPlus;
            prev.next = nodeK;
            return node1;
        }

        private void ReverseK(ListNode prev, ListNode curr, int k)
        {
            for (var i = 0; i < k; i++)
            {
                var next = curr.next;
                curr.next = prev;
                prev = curr;
                curr = next;
            }
        }


        [Test]
        public void ReverseBetweenTwo()
        {
            var t = new[] {1, 2, 3, 4, 5}.ToLinkedList();

            var r = ReverseBetweenTwo(t, 2, 4);
        }
        /// <summary>
        /// prev-> 1 -> ... m-1 -> m -> m+1 -> ... n-1 -> n -> n+1
        /// prev-> 1 -> ... m-1 -> n -> n-1 -> ... m+1 -> m -> n+1
        /// prev, m-1, n+1, reverse between m-1 and n+1
        /// </summary>
        /// <param name="head"></param>
        /// <param name="m"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public ListNode ReverseBetweenTwo(ListNode head, int m, int n)
        {
            if (head == null)
            {
                return null;
            }

            var dummyNode = new ListNode(0) { next = head };

            var mMinus = dummyNode;
            var nNode = dummyNode;

            for (var i = 0; i < m -1; i++)
            {
                mMinus = mMinus.next;
            }

            for (var i = 0; i < n; i++)
            {
                nNode = nNode.next;
            }

            var mNode = mMinus.next;
            var nPlus = nNode.next;

            ListNode prev = null;
            var curr = mNode;
            for (var i = 0; i < n-m +1;i++)
            {
                var next = curr.next;
                curr.next = prev;
                prev = curr;
                curr = next;
            }

            mMinus.next = nNode;
            mNode.next = nPlus;
            return dummyNode.next;
        }

        public RandomListNode CopyRandomList(RandomListNode head)
        {
            if (head == null)
            {
                return null;
            }

            var map = new Dictionary<RandomListNode, RandomListNode>();
            var dummy = new RandomListNode(0);
            var curr = dummy;
            while (head != null)
            {
                RandomListNode newNode;
                if (map.ContainsKey(head))
                {
                    newNode = map[head];
                }
                else
                {
                    newNode = new RandomListNode(head.label);
                    map.Add(head, newNode);
                }
                curr.next = newNode;

                if (head.random != null)
                {
                    if (map.ContainsKey(head.random))
                    {
                        newNode.random = map[head.random];
                    }
                    else
                    {
                        newNode.random = new RandomListNode(head.random.label);
                        map.Add(head.random, newNode.random);
                    }
                }

                curr = curr.next;
                head = head.next;
            }

            return dummy.next;
        }

        public RandomListNode CopyRandomListTwo(RandomListNode head)
        {
            if (head == null)
            {
                return null;
            }

            CopyNext(head);
            CopyRandom(head);
            return SplitList(head);
        }

        private RandomListNode SplitList(RandomListNode head)
        {
            var newHead = head.next;
            while (head != null)
            {
                var temp = head.next;
                head.next = temp.next;
                head = head.next;
                if (temp.next != null)
                {
                    temp.next = temp.next.next;
                }
            }

            return newHead;
        }

        private void CopyRandom(RandomListNode head)
        {
            while (head != null)
            {
                if (head.next.random != null)
                {
                    head.next.random = head.random.next;
                }

                head = head.next.next;
            }
        }

        private void CopyNext(RandomListNode head)
        {
            while (head != null)
            {
                var newNode = new RandomListNode(head.label)
                {
                    next = head.next,
                    random = head.random
                };
                head.next = newNode;
                head = head.next.next;
            }
        }

        [Test]
        public void SortList()
        {
            var l = new[] {7, 5, 3, 2, 1}.ToLinkedList();
            var r = SortList(l);
        }

        public ListNode SortList(ListNode head)
        {
            if (head == null || head.next == null)
            {
                return head;
            }

            ListNode mid = FindMidNode(head);
            ListNode right = SortList(mid.next);
            mid.next = null;
            ListNode left = SortList(head);

            return MergeNode(left, right);
        }

        private ListNode MergeNode(ListNode left, ListNode right)
        {
            var dummy = new ListNode(0);
            var tail = dummy;

            while (left != null && right != null)
            {
                if (left.val < right.val)
                {
                    tail.next = left;
                    left = left.next;
                }
                else
                {
                    tail.next = right;
                    right = right.next;
                }

                tail = tail.next;
            }

            if (left != null)
            {
                tail.next = left;
            }
            else
            {
                tail.next = right;
            }

            return dummy.next;
        }

        private ListNode FindMidNode(ListNode head)
        {
            ListNode slow = head, fast = head;
            while (fast.next != null && fast.next.next != null)
            {
                fast = fast.next.next;
                slow = slow.next;
            }
            return slow;
        }

        public int MinPathSum(int[,] grid)
        {
            if (grid == null || grid.GetLength(0) < 1 || grid.GetLength(1) < 1)
            {
                return 0;
            }

            var r = grid.GetLength(0);
            var c = grid.GetLength(1);
            var path = new int[r, c];

            path[0, 0] = grid[0, 0];
            for (var i = 1; i < r; i++)
            {
                path[0, i] = path[0, i - 1] + grid[0, i];
            }

            for (var i = 1; i < r; i++)
            {
                path[i, 0] = path[i - 1, 0] + grid[i, 0];
            }

            for (var i = 1; i < r; i++) {
                for (var j = 1; j < c; j++)
                {
                    path[i, j] = Math.Min(path[i, j - 1], path[i - 1, j]) + grid[i, j];
                }
            }


            return path[r - 1, c - 1];
        }

        /// <summary>
        /// Handle '.', '/' and '..'.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public string SimplifyPath(string path)
        {
            var stack = new Stack<string>();
            var skip = new HashSet<string> {"..", ".", ""};
            // Split will skip all the /
            foreach (var curr in path.Split('/'))
            {
                //Remove previous path.
                if (curr == ".." && stack.Any())
                {
                    stack.Pop();
                }else if (!skip.Contains(curr))
                {
                    //Push all the valid path.
                    stack.Push(curr);
                }
            }

            var res = "";
            foreach (var dir in stack)
            {
                res = "/" + dir + res;
            }

            return string.IsNullOrEmpty(res) ? "/" : res;
        }
    }


}
