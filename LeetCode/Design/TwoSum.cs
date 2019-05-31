using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode.Design
{

    public class TwoSum
    {
        private readonly Dictionary<int, bool> _duplicateMap;
        /** Initialize your data structure here. */
        public TwoSum()
        {
            _duplicateMap = new Dictionary<int, bool>();
        }

        /** Add the number to an internal data structure.. */
        public void Add(int number)
        {
            _duplicateMap[number] = _duplicateMap.ContainsKey(number);

        }

        /** Find if there exists any pair of numbers which sum is equal to the value. */
        public bool Find(int value)
        {
            foreach (int x in _duplicateMap.Keys)
            {
                int remain = value - x;
                // Remain exist. Check if target = 2 * remain.
                if (_duplicateMap.ContainsKey(remain) && (x != remain || _duplicateMap[remain]))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
