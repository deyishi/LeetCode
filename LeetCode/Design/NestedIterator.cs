using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeetCode.DataModel;

namespace LeetCode.Design
{
    public class NestedIterator
    {
        private readonly Queue<int> queue;

        public NestedIterator(IList<NestedInteger> nestedList)
        {
            queue = new Queue<int>();
            ConvertListToQueue(nestedList);
        }

        private void ConvertListToQueue(IList<NestedInteger> nestedList)
        {
            foreach (var n in nestedList)
            {
                if (n.IsInteger())
                {
                    queue.Enqueue(n.GetInteger());
                }
                else
                {
                    ConvertListToQueue(n.GetList());
                }
            }
        }

       
        public bool HasNext()
        {
            return queue.Count > 0;
        }

        public int Next()
        {
            if (HasNext())
            {
                return queue.Dequeue();
            }

            return -1;
        }
    }
}
