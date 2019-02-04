using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeetCode.Design;
using NUnit.Framework;

namespace LeetCode.DataModel
{
    public class ZigzagIterator
    {
        private readonly IList<int> _v1;
        private readonly IList<int> _v2;
        private int _index;
        private int _p1;
        private int _p2;

        public ZigzagIterator(IList<int> v1, IList<int> v2)
        {
            _v1 = v1;
            _v2 = v2;
            _index = 0;
            _p1 = 0;
            _p2 = 0;

        }

        public bool HasNext()
        {
            return _index < _v1.Count + _v2.Count;
        }

        public int Next()
        {
            var result = -1;
            if (!HasNext())
            {
                return result;
            }

            if (_index % 2 == 0)
            {
                result = _p1 < _v1.Count ? _v1[_p1++] : _v2[_p2++];
            }
            else
            {
                result = _p2 < _v2.Count ? _v2[_p2++] : _v1[_p1++];
            }

            _index++;
            return result;
        }
    }
}
