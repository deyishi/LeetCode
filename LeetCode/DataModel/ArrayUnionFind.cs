using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode.DataModel
{
    public class ArrayUnionFind
    {
        private readonly int?[] _array;
        public ArrayUnionFind(int n)
        {
            _array = new int?[n];
            
        }

        public int Find(int x)
        {
            if (_array[x] == null)
            {
                return x;
            }

            return Find(_array[x].Value);
        }

        public void Union(int x, int y)
        {
            var xRoot = Find(x);
            var yRoot = Find(y);

            _array[xRoot] = yRoot;
        }

        public int MaxUnion()
        {
            var count = new int [_array.Length];
            var max = 0;
            for (var i = 0; i < _array.Length; i++) {
                var x = Find(i);
                count[x]++;
                max = Math.Max(max, count[x]);
            }

            return max;
        }
    }

}
