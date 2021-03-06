﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeetCode.DataModel;
using NUnit.Framework;
using NUnit.Framework.Constraints;

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
                //1 -> Copy 1 - > 2 -> Copy 2
                // Temp = Copy 1
                // Head = 1
                // Head.next = Temp.next 1 -> 2
                // Head = Head.next head = 2
                // Temp.next = Temp.next.next Copy 1 -> Copy 2
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


        [Test]
        public void SetZeros()
        {
            var a = new[,] {{1, 1, 1}, {1, 0, 1}, {1, 1, 1}};
            SetZeroes(a);
        }
        public void SetZeroes(int[,] matrix)
        {
            if (matrix == null || matrix.GetLength(0) < 1 || matrix.GetLength(1) < 1)
            {
                return;
            }

            // Number of rows equal to col length.
            // Number of cols equal to row length.
            var r = matrix.GetLength(0);
            var c = matrix.GetLength(1);

            var firstRowEmpty = false;
            var firstColEmpty = false;

            for (int i = 0; i < r; i++)
            {
                if (matrix[i,0] == 0)
                {
                    firstColEmpty = true;
                    break;
                }
            }

            for (int i = 0; i < c; i++)
            {
                if (matrix[0, i] == 0)
                {
                    firstRowEmpty = true;
                    break;
                }
            }



            for (var i = 1; i < r; i++) {
                for (var j = 1; j < c; j++) {
                    if (matrix[i,j] == 0)
                    {
                        matrix[i, 0] = 0;
                        matrix[0, j] = 0;
                    }
                }
            }

            for (var i = 1; i < r; i++)
            {
                for (var j = 1; j < c; j++)
                {
                    if (matrix[0, j] == 0 || matrix[i, 0] == 0)
                    {
                        matrix[i, j] = 0;
                    }

                }
            }


            if (firstColEmpty)
            {
                for (int i = 0; i < r; i++)
                {
                    matrix[i,0] = 0;
                }
            }


            if (firstRowEmpty)
            {
                for (int i = 0; i < c;i++)
                {
                    matrix[0, i] = 0;
                }
            }
        }

        [Test]
        public void SearchMatrix()
        {
            var m = new[,] {{1}};
            var r = SearchMatrix(m, 1);
        }

        public bool SearchMatrix(int[,] matrix, int target)
        {
            if (matrix == null || matrix.GetLength(0) == 0 || matrix.GetLength(1) == 0)
            {
                return false;
            }

            var rows = matrix.GetLength(0);
            var cols = matrix.GetLength(1);
            for (var i = 0; i < rows; i++)
            {
                // Target is in this row
                if (target <= matrix[i, cols - 1])
                {
                    //Do binary search
                    var left = 0;
                    var right = cols - 1;
                    while (left <= right)
                    {
                        var mid = left + (right - left) / 2;
                        if (matrix[i, mid] == target)
                        {
                            return true;
                        }

                        if (matrix[i, mid] > target)
                        {
                            right = mid - 1;
                        }
                        else
                        {
                            left = mid + 1;
                        }

                    }
                }
            }

            return false;
        }

        [Test]
        public void DeleteDuplicates()
        {
            var l = new int[] {1, 1}.ToLinkedList();

            var r = DeleteDuplicates(l);
        }

        public ListNode DeleteDuplicates(ListNode head)
        {
            if (head == null || head.next == null)
                return head;

            ListNode dummy = new ListNode(0);
            dummy.next = head;
            head = dummy;

            while (head.next != null && head.next.next != null)
            {
                if (head.next.val == head.next.next.val)
                {
                    int val = head.next.val;
                    while (head.next != null && head.next.val == val)
                    {
                        head.next = head.next.next;
                    }
                }
                else
                {
                    head = head.next;
                }
            }

            return dummy.next;
        }


        [Test]
        public void SortedListToBst()
        {
            var list = new[] {1, 2, 3 ,4 ,5 ,6 ,7}.ToLinkedList();
            var r = SortedListToBst(list);
        }

        private ListNode _current;

        private int GetListLength(ListNode head)
        {
            int size = 0;

            while (head != null)
            {
                size++;
                head = head.next;
            }

            return size;
        }

        public TreeNode SortedListToBst(ListNode head)
        {

            _current = head;
            var size = GetListLength(head);

            return SortedListToBstHelper(size);
        }

        public TreeNode SortedListToBstHelper(int size)
        {
            if (size <= 0)
            {
                return null;
            }

            TreeNode left = SortedListToBstHelper(size / 2);
            TreeNode root = new TreeNode(_current.val);
            _current = _current.next;
            TreeNode right = SortedListToBstHelper(size - 1 - size / 2);

            root.left = left;
            root.right = right;

            return root;
        }


        [Test]
        public void BstToDoublyList()
        {
            var t = new[] {2, 1, 3}.ToTree();
            var r = BstToDoublyList(t);
        }
        public DoublyListNode BstToDoublyList(TreeNode root)
        {
            if (root == null)
            {
                return null;
            }

            return DfsInOrder(root);
        }

        private DoublyListNode DfsInOrder(TreeNode root)
        {

            Stack<TreeNode> stack = new Stack<TreeNode>();
            TreeNode curr = root;
            DoublyListNode head = null;
            DoublyListNode prev = null;
            while (curr != null || stack.Any())
            {
                while (curr != null)
                {
                    stack.Push(curr);
                    curr = curr.left;
                }

                curr = stack.Pop();
                // Link curr to prev.
                DoublyListNode node = new DoublyListNode(curr.val) {prev = prev};
                if (head == null)
                {
                    head = node;
                }

                // Link prev to its next.
                if (prev != null)
                {
                    prev.next = node;
                }
                prev = node;

                // Continue dfs in-order
                curr = curr.right;
                
            }

            return head;
        }

        public void Merge(int[] nums1, int m, int[] nums2, int n)
        {
            int mLength = m + n - 1;
            int i = m - 1;
            int j = n - 1;

            while (mLength > 0)
            {
                if (i >= 0 && j >= 0)
                {
                    if (nums1[i] > nums2[j])
                    {
                        nums1[mLength] = nums1[i];
                        i--;
                    }
                    else
                    {
                        nums1[mLength] = nums2[j];
                        j--;
                    }
                }else if (i >= 0)
                {
                    nums1[mLength] = nums1[i];
                    i--;
                }
                else
                {
                    nums1[mLength] = nums2[j];
                    j--;
                }

                mLength--;
            }
        }

        public int[] Intersection(int[] nums1, int[] nums2)
        {
            if (nums1 == null || nums2 == null)
            {
                return null;
            }

            var set = new HashSet<int>();
            Array.Sort(nums1);
            Array.Sort(nums2);
            var i = 0;
            var j = 0;

            while (i < nums1.Length && j < nums2.Length)
            {
                if (nums1[i] > nums2[j])
                {
                    j++;
                }
                else if(nums1[i] < nums2[j])
                {
                    i++;
                }
                else
                {
                    set.Add(nums1[i]);
                    i++;
                    j++;
                }
            }
            return set.ToArray();
        }


        public IList<int> GrayCode(int n)
        {
            var result = new List<int>();
            for (int i = 0; i < Math.Pow(2, 4); ++i)
            {
                result.Add(i >> 1 ^ i);
            }

            return result;
        }

        [Test]
        public void NumDecoding()
        {
            var s = "1001";

            var r = NumDecodings(s);
        }

        //DP calculating current string[currentIndex] combinations count by adding string[currentIndex - 1] + string[currentIndex] based on conditions string[currentIndex] or string[currentIndex - 1] is 0. Whether current index form a valid single digit number or valid double digits number with previous digit.
        public int NumDecodings(string s)
        {
            if (string.IsNullOrEmpty(s) || s[0] == '0')
            {
                return 0;
            }
            int prev = 1;
            int curr = 1;
            for (int i = 1; i < s.Length; i++)
            {

                var temp = 0;
                if (s[i] != '0')
                {
                    temp = curr;
                }
                else if (s[i - 1] == '0')
                {
                    return 0;
                }

                // Can form a valid two digits.
                int twoDigits = (s[i - 1] - '0') * 10 + s[i] - '0';
                if (twoDigits >= 10 && twoDigits <= 26)
                {
                    temp += prev;
                }

                prev = curr;
                curr = temp;
            }
            return curr;
        }

        [Test]
        public void RestoreIpAddresses()
        {
            var s = "1111";

            var r = RestoreIpAddresses(s);
        }
        public IList<string> RestoreIpAddresses(string s)
        {
            var result = new List<string>();
            if (string.IsNullOrEmpty(s) || s.Length <4 || s.Length > 12)
            {
                return result;
            }

            RestoreIpAddressesHelper(result, s, "", 0);
            return result;
        }

        /// <summary>
        /// Backtracking:
        /// return condition when 4 parts of IP address is found and string is used up.
        /// Do 1.1.1.1 check if used up, if not do 1.1.1.two digits.
        /// </summary>
        /// <param name="result"></param>
        /// <param name="s"></param>
        /// <param name="current"></param>
        /// <param name="parts"></param>
        private void RestoreIpAddressesHelper(List<string> result, string s, string current, int parts)
        {
            if (parts == 4 && s.Length == 0)
            {
                result.Add(current.Substring(1));
            }
            else if(parts != 4 && s.Length != 0)
            {
                // Do 1 digits.
                RestoreIpAddressesHelper(result, s.Substring(1), current + "." + s.Substring(0, 1), parts + 1);
                if (s[0] != '0' && s.Length > 1)
                {
                    // Do 2 digits, is s > 1 and not start 0.
                    RestoreIpAddressesHelper(result, s.Substring(2), current + "." + s.Substring(0, 2), parts + 1);

                    // Do 3, if s < 255.
                    if (s.Length > 2 && int.Parse(s.Substring(0,3)) <= 255)
                    {
                        RestoreIpAddressesHelper(result, s.Substring(3), current + "." + s.Substring(0, 3), parts + 1);
                    }
                }
            }
        }

        [Test]
        public void Connect()
        {
            var t = new TreeLinkNode(1)
            {
                left = new TreeLinkNode(2)
                {
                    left =new TreeLinkNode(4),
                    right = new TreeLinkNode(5)
                },
                right = new TreeLinkNode(3)
                {
                    left = new TreeLinkNode(6),
                    right = new TreeLinkNode(7)
                }
            };
            Connect(t);
        }

        public void Connect(TreeLinkNode root)
        {
            if (root == null)
            {
                return;
            }

            if (root.left != null)
            {
                root.left.next = root.right;
            }

            if (root.next != null &&  root.right != null)
            {
                root.right.next = root.next.left;
            }

            Connect(root.left);
            Connect(root.right);
        }


        /// <summary>
        /// Level traversal
        /// </summary>
        /// <param name="root"></param>
        public void ConnectTwo(TreeLinkNode root)
        {
            if (root == null)
            {
                return;
            }

            while (root != null)
            {
                var temp = root;
                if (root.left == null)
                {
                    break;
                }

                while (root != null)
                {
                    root.left.next = root.right;
                    if (root.right != null && root.next != null)
                    {
                        root.right.next = root.next.left;
                    }

                    root = root.next;
                }

                root = temp.left;
            }
        }

        public void ConnectBinaryTree(TreeLinkNode root)
        {
            if (root == null) return;
            if (root.left != null)
            {
                root.left.next = root.right ?? ConnectFindNext(root);
            }

            if (root.right != null)
            {
                root.right.next = ConnectFindNext(root);
            }
            ConnectBinaryTree(root.right);
            ConnectBinaryTree(root.left);
        }

        private TreeLinkNode ConnectFindNext(TreeLinkNode root)
        {
            TreeLinkNode sibling = root.next;
            while (sibling != null)
            {
                if (sibling.left == null && sibling.right == null)
                {
                    sibling = sibling.next;
                }
                else
                {
                    return sibling.left ?? sibling.right;
                }
            }
            return null;
        }

        [Test]
        public void FindKthLargest()
        {

            var t = new[] {1, 9, 5, 7, 6};
            var r = FindKthLargest(t, 4);
        }

        /// <summary>
        /// Partition:
        /// 1. Use end as a place holder.
        /// 2. Select a pivotal, find the pivotal index and insert pivotal. (If index number is less than or equal to p, swap and increase pIndex(is the last number that is large than p). It will skip the large ones, which will get swapped later when number smaller than p is found.) 1, 9, 5, 7, 6, after loop 1, 5, 9(p), 7, final swap.
        /// 3. Index of the Kth largest number is the length  of number - k.
        /// 4. Find K Index == pivotal position.
        /// 5. if k index > pivotal, search pivotal + 1 to end.
        /// </summary>
        /// <param name="nums"></param>
        /// <param name="k"></param>
        /// <returns></returns>
        public int FindKthLargest(int[] nums, int k)
        {
            if (nums == null || nums.Length == 0)
            {
                return 0;
            }

            return QuickSelect(nums, 0, nums.Length - 1, k);
        }

        public int QuickSelect(int[] nums, int start, int end, int k)
        {

            var p = nums[end];

            var pIndex = start;
            for (var i = start; i < end; i++)
            {
                if (nums[i] <= p)
                {
                    Swap(nums, i, pIndex);
                    pIndex++;
                }
            }

            Swap(nums, pIndex, end);

            var kIndex = nums.Length - k;
            if (kIndex == pIndex)
            {
                return nums[pIndex];
            }

            if (kIndex > pIndex)
            {
                return QuickSelect(nums, pIndex + 1, end, k);
            }

            return QuickSelect(nums, start, pIndex - 1, k);
        }


        public void Swap(int[] nums, int i, int j)
        {
            var temp = nums[i];
            nums[i] = nums[j];
            nums[j] = temp;
        }
    }
}
