using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode.Design
{
    public class MovingAverage
    {
        private int size;
        private double sum;
        private Queue<int> queue;
        /** Initialize your data structure here. */
        public MovingAverage(int size)
        {
            sum = 0;
            this.size = size;
            queue = new Queue<int>();
        }

        public double Next(int val)
        {
            if (queue.Count == size)
            {
                sum -= queue.Dequeue();
            }

            sum += val;
            queue.Enqueue(val);
            return sum / queue.Count;
        }
    }
}
