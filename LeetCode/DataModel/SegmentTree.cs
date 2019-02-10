using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode.DataModel
{
    public class SumSegmentTreeNode
    {
        public int Start { get; set; }
        public int End { get; set; }
        public SumSegmentTreeNode Left { get; set; }
        public SumSegmentTreeNode Right { get; set; }
        public int Sum { get; set; }

        public SumSegmentTreeNode(int start, int end)
        {
            Start = start;
            End = end;
            Left = null;
            Right = null;
            Sum = 0;
        }
    }

}
