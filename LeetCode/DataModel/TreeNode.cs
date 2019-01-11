using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode.DataModel
{
    public class TreeNode
    {
        public int val;
        public TreeNode left;
        public TreeNode right;
        public TreeNode(int x)
        {
            val = x;
        }
    }

    public class TreeLinkNode
    {
        int val;
        public TreeLinkNode left, right, next;
        public TreeLinkNode(int x) { val = x; }
    }

}
