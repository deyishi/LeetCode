using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode.Design
{
    public class Logger
    {
        private Dictionary<string, int> _messageQueue;

        public Logger()
        {
            _messageQueue = new Dictionary<string, int>();
        }

        public bool ShouldPrintMessage(int timestamp, string message)
        {
            // Check if its a new message, else increase its time span.
            if (!_messageQueue.ContainsKey(message))
            {
                _messageQueue.Add(message, 0);
            }

            if ( timestamp < _messageQueue[message])
            {
                return false;
            }

            _messageQueue[message] = timestamp + 10;
            return true;
        }
    }
}
