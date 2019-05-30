using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeetCode.DataModel;
using NUnit.Framework;
using NUnit.Framework.Constraints;

namespace LeetCode.Nine_Chapter
{
    /// <summary>
    /// Two pointer
    /// </summary>
    class L3TwoPointers
    {

        public int[] WinSum(int[] nums, int k)
        {
            if (nums == null || nums.Length < k || k <= 0)
            {
                return null;
            }

            var result = new int[nums.Length - k + 1];
            // Start point 
            for (var i = 0; i < k; i++)
            {
                result[0] += nums[i];
            }

            for (var i = 1; i < nums.Length; i++)
            {
                result[i] = result[i - 1] - nums[i - 1] + nums[i + k - 1];
            }

            return result;
        }

        public int TwoSumSetLessThan(int[] nums, int target)
        {
            if (nums == null || nums.Length < 2)
            {
                return 0;
            }

            var start = 0;
            var end = nums.Length - 1;
            var count = 0;
            while (start < end)
            {
                var sum = nums[start] + nums[end];


                if (sum <= target)
                {
                    count += end - start;
                    start++;
                }
                else 
                {
                    end--;
                }
            }

            return count;
        }

        public int TwoSumUniquePair(int[] nums, int target)
        {
            if (nums == null || nums.Length < 2)
            {
                return 0;
            }

            Array.Sort(nums);
            var start = 0;
            var end = nums.Length - 1;
            var count = 0;
            while (start < end)
            {
                var sum = nums[start] + nums[end];
                if (sum < target)
                {
                    start++;
                }else if (sum > target)
                {
                    end--;
                }
                else
                {
                    count++;
                    start++;
                    end--;
                    // skip duplicate
                    while (start < end && nums[start] == nums[start-1])
                    {
                        start++;
                    }

                    while (end > start && nums[end] == nums[end+1])
                    {
                        end--;
                    }
                }
            }

            return count;
        }

        [Test]
        public void TwoSumClosest()
        {
            var n = new[] { 1, 3, 5, 7, 9 };
            var r = TwoSumSetLargeThan(n, 8);
        }
        public int TwoSumClosest(int[] nums, int target)
        {
            if (nums == null || nums.Length < 2)
            {
                return 0;
            }

            Array.Sort(nums);
            var start = 0;
            var end = nums.Length - 1;
            var result =  int.MaxValue;
            while (start < end)
            {
                if (nums[start] + nums[end] < target)
                {
                    result = Math.Min(result, Math.Abs(target - nums[start] - nums[end]));
                    start++;
                }
                else
                {
                    result = Math.Min(result, Math.Abs(target - nums[start] - nums[end]));
                    end--;
                }
            }

            return result;
        }

        public int TwoSumSetLargeThan(int[] nums, int target)
        {
            if (nums == null || nums.Length < 2)
            {
                return 0;
            }

            var start = 0;
            var end = nums.Length - 1;
            var count = 0;
            while (start < end)
            {
                var sum = nums[start] + nums[end];


                if (sum <= target)
                {
                    start++;
                }
                else
                {
                    count += end - start;
                    end--;
                }
            }

            return count;
        }

        public char NextGreatestLetter(char[] letters, char target)
        {
            var start = 0;
            var end = letters.Length - 1;
            while (start <= end)
            {
                var mid = start + (end - start) / 2;
                if (letters[mid] < target)
                {
                    start = mid+1;
                }
                else
                {
                    end = mid;
                }
            }



            return letters[start % letters.Length];
        }

        public ListNode MiddleNode(ListNode head)
        {
            if (head == null)
            {
                return null;
            }

            var slow = head;
            var fast = head;
            while (fast?.next != null)
            {
                slow = slow.next;
                fast = fast.next.next;
            }

            return slow;

        }

        public void MoveZeroes(int[] nums)
        {
            var index = 0;
            for (var i = 0; i < nums.Length; i++) {
                if (nums[i] != 0)
                {
                    nums[index++] = nums[i];
                }
            }

            while (index < nums.Length)
            {
                nums[index++] = 0;
            }
        }

        [Test]
        public void METHOD()
        {
            var n = new[] {1, 1, 2};
            var r = RemoveDuplicates(n);
        }

        public int RemoveDuplicates(int[] nums)
        {
            var size = 0;
            for (var i = 0; i < nums.Length;i++) {
                if (nums[i] != nums[size])
                {
                    nums[size++] = nums[i];

                }
            }

            return size + 1;
        }


        public void SortColors(int[] nums)
        {

        }

        public void ColorQuickSort(int[] nums, int start, int end, int startColor, int endColor)
        {
            if (start >= end || startColor == endColor)
            {
                return;
            }

            var i = start;
            var j = end;
            var midColor = startColor + (endColor - startColor) / 2;
            var startColorIndex = start;
            while (i <= end)
            {
                if (nums[i] < midColor)
                {
                    ColorSwap(nums, i++, startColorIndex++);
                }else if (nums[i] > midColor)
                {
                    ColorSwap(nums, startColorIndex, j--);
                }
                else
                {
                    i++;
                }
            }

            ColorQuickSort(nums, start, j - 1, startColor, midColor - 1);
            ColorQuickSort(nums, j, end, midColor + 1, end);
        }

        public void ColorSwap(int[] nums, int i, int j)
        {
            var temp = nums[i];
            nums[i] = nums[j];
            nums[j] = temp;
        }

        public bool IsSubtree(TreeNode s, TreeNode t)
        {

            if (s == null)
            {
                return false;
            }

            return IsSame(s, t) || IsSame(s.left, t) || IsSame(s.right, t);
        }

        public bool IsSame(TreeNode s, TreeNode t)
        {
            if (s == null && t == null)
            {
                return true;
            }

            if (s == null || t == null)
            {
                return false;
            }

            if (s.val != t.val)
            {
                return false;
            }

            return IsSame(s.right, t.right) && IsSame(s.left, t.left);
        }

        public int Min = int.MaxValue;
        public TreeNode MinRoot = null;

        public TreeNode FindSubtree(TreeNode root)
        {
            FindSubtreeHelper(root);
            return MinRoot;
            // write your code here
        }

        public int FindSubtreeHelper(TreeNode root)
        {
            if (root == null)
            {
                return 0;
            }

            var sum = FindSubtreeHelper(root.left) + FindSubtreeHelper(root.right) + root.val;

            if (sum < Min)
            {
                Min = sum;
                MinRoot = root;
            }

            return sum;
        }

        public TreeNode LowestCommonAncestor(TreeNode root, TreeNode p, TreeNode q)
        {
            if (root == null)
            {
                return root;
            }

            while ((root.val - p.val) * (root.val - q.val) > 0)
            {
                root = root.val > p.val ? root.left : root.right;
            }

            return root;
        }
    }
}
