using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode.DataModel
{
    public class NumArray
    {
        private readonly int[] _nums;
        public NumArray(int[] nums)
        {
            _nums = nums;
            // Count total at num[i]
            for (int i = 1; i < _nums.Length; i++)
            {
                _nums[i] = _nums[i] + _nums[i - 1];
            }
        }

        public int SumRange(int i, int j)
        {
            if (i == 0)
            {
                return _nums[j];
            }

            return _nums[j] - _nums[i - 1];
        }
    }

    public class NumArrayWithSegmentTree
    {
        private readonly SumSegmentTreeNode _root;
        public NumArrayWithSegmentTree(int[] nums)
        {
            _root = BuildSegmentTree(nums, 0, nums.Length);
        }

        private SumSegmentTreeNode BuildSegmentTree(int[] nums, int start, int end)
        {
            if (start > end)
            {
                return null;
            }

            var treeNode = new SumSegmentTreeNode(start, end);

            if (start == end)
            {
                treeNode.Sum = nums[start];
            }
            else
            {
                var mid = start + (end - start) / 2;
                treeNode.Left = BuildSegmentTree(nums, start, mid);
                treeNode.Right = BuildSegmentTree(nums, mid + 1, end);

                treeNode.Sum = treeNode.Left.Sum + treeNode.Right.Sum;
            }
            return treeNode;

        }

        public void Update(int i, int val)
        {
            UpdateTree(_root, i, val);
        }

        private void UpdateTree(SumSegmentTreeNode tree, int pos, int val)
        {
            // Handle single node tree
            if (tree.Start == tree.End)
            {
                tree.Sum = val;
            }
            else
            {
                var mid = tree.Start + (tree.End - tree.Start) / 2;
                // Recursively update segment tree children.
                if (pos <= mid) 
                {
                    UpdateTree(tree.Left, pos, val);
                }
                else
                {
                    UpdateTree(tree.Right,pos,val);
                }

                tree.Sum = tree.Left.Sum + tree.Right.Sum;
            }
        }

        public int SumRange(int i, int j)
        {
            return FindSumRangeInTree(_root, i, j);
        }

        private int FindSumRangeInTree(SumSegmentTreeNode tree, int start, int end)
        {
            if (tree == null)
            {
                return -1;
            }

            if (tree.Start == start && tree.End == end)
            {
                return tree.Sum;
            }

            var mid = tree.Start + (tree.End - tree.Start) / 2;
            if (end <= mid)
            {
                return FindSumRangeInTree(tree.Left, start, end);
            }

            if(start > mid)
            {
                return FindSumRangeInTree(tree.Right, start, end);
            }

            // The range contains mid.
            return FindSumRangeInTree(tree.Right, mid + 1, end) + FindSumRangeInTree(tree.Left, start, mid);
        }
    }
}
