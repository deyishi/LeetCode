namespace LeetCode.DataModel
{
    public class NumMatrix
    {
        private readonly int[,] _sum;

        public NumMatrix(int[,] matrix)
        {
            int x = matrix.GetLength(0);
            int y = matrix.GetLength(1);
            _sum = new int[x, y];
            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    if (i == 0 && j == 0) _sum[i, j] = matrix[i, j];
                    else if (i == 0) _sum[i, j] = _sum[i, j - 1] + matrix[i, j];
                    else if (j == 0) _sum[i, j] = _sum[i - 1, j] + matrix[i, j];
                    else _sum[i, j] = _sum[i - 1, j] + _sum[i, j - 1] - _sum[i - 1, j - 1] + matrix[i, j];
                }
            }
        }

        public int SumRegion(int row1, int col1, int row2, int col2)
        {
            return Sum(row2, col2) - Sum(row1 - 1, col2) - Sum(row2, col1 - 1) + Sum(row1 - 1, col1 - 1);
        }

        private int Sum(int row, int col)
        {
            if (row < 0 || col < 0) return 0;
            return _sum[row, col];
        }
    }
}