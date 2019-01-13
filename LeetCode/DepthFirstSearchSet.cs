﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeetCode.DataModel;
using NUnit.Framework;

namespace LeetCode
{
    public class DepthFirstSearchSet
    {


        [Test]
        public void SumNumbers()
        {

            var t = new int[] {1, 2}.ToTree();
            var r = SumNumbers(t);
        }
        /// <summary>
        /// DFS
        ///    1
        ///  2   3
        /// 12 + 13 add when reach leaf 
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        public int SumNumbers(TreeNode root)
        {
            if (root == null)
            {
                return 0;
            }

            int sum = 0;
            var path = "";
            SumNumbersHelper(root, ref sum, path);

            return sum;
        }

        private void SumNumbersHelper(TreeNode root, ref int sum, string path)
        {
            if (root == null)
            {
                return;
            }

            if (root.left == null && root.right==null)
            {
                sum += int.Parse(path + root.val);
                return;
            }

            path += root.val.ToString(); 
            SumNumbersHelper(root.left, ref sum, path);
            SumNumbersHelper(root.right, ref sum, path);
        }
    }
}