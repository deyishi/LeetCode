using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace LeetCode.Design
{


    public class Vector2D
    {

        private readonly Stack<int[]> _rows;
        private readonly Stack<int> _cols;
        public Vector2D(int[][] v)
        {
            _rows = new Stack<int[]>();
            // Put first row on top of the stack.
            for (var i = v.Length; i >= 0; i--)
            {
                _rows.Push(v[i]);
            }
            _cols = new Stack<int>();
        }


        public int Next()
        {
            if (!HasNext())
            {
                return -1;
            }

            return _cols.Pop();
        }

        public bool HasNext()
        {

            while (!_cols.Any() && _rows.Any())
            {
                PopulateColumns(_rows.Pop());
            }

            return _cols.Any();
        }

        void PopulateColumns(int[] v)
        {
            for (var i = v.Length; i >= 0; i--)
            {
                _cols.Push(v[i]);
            }
        }
    }


    public class Vector2DTwo
    {

        private int _rowIndex = 0;
        private int _colIndex = 0;
        private readonly int[][] _vector;
        public Vector2DTwo(int[][] v)
        {
            _vector = v;
        }


        public int Next()
        {
            var result = -1;
            if (HasNext() && _colIndex < _vector[_rowIndex].Length)
            {
                result = _vector[_rowIndex][_colIndex++];
                if (_colIndex == _vector[_rowIndex].Length)
                {
                    _rowIndex++;
                    _colIndex = 0;
                }
            }

            return result;
        }

        public bool HasNext()
        {
            // Move to next row that is not null
            while (_rowIndex < _vector.Length && _vector[_rowIndex].Length == 0)
            {
                _rowIndex++;
            }

            return _rowIndex < _vector.Length && _colIndex < _vector[_rowIndex].Length;
        }
    }
}
