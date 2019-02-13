using System;
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
        public void Test()
        {

            var t = "ad";
            var r = GenerateAbbreviations(t);
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
        public int DepthSum(IList<NestedInteger> nestedList)
        {
            return DepthSumHelper(nestedList, 1);
        }

        private int DepthSumHelper(IList<NestedInteger> nestedList, int depth)
        {
            var sum = 0;
            foreach (var n in nestedList)
            {
                if (n.IsInteger())
                {
                    sum += n.GetInteger();
                }
                else
                {
                    DepthSumHelper(n.GetList(), depth++);
                }
            }

            return sum;
        }

        public int DepthSumInverse(IList<NestedInteger> nestedList)
        {
            var previousLevel = 0;
            var sum = 0;
            var currList = nestedList;
            while (currList.Any())
            {
                var nextLevel = new List<NestedInteger>();

                foreach (var n in nestedList)
                {
                    if (n.IsInteger())
                    {
                        previousLevel += n.GetInteger();
                    }
                    else
                    {
                        nextLevel.AddRange(n.GetList());
                    }
                }

                // previous level will get added multiple times.
                sum += previousLevel;
                currList = nextLevel;
            }

            return sum;
        }

        public IList<string> GenerateAbbreviations(string word)
        {
            //pos: points to the current character
            //cur: current string formed
            //count: how many letters are abbreviated in the current string

            //At each step:
            //Do not abbreviate
            //Abbreviate the current letters reset abbreviate count

            // Base case pos == word.Length
            // Add abbreviation and put it to the result
            var result = new List<string>();
            GenerateAbbreviationsHelper(result, word, new StringBuilder(), 0, 0);
            return result;
        }

        private void GenerateAbbreviationsHelper(List<string> result, string word, StringBuilder cur, int pos, int count)
        {
            if (pos == word.Length)
            {
                if (count != 0)
                {
                    cur.Append(count);
                }
                result.Add(cur.ToString());
            }
            else
            {
                var unChanged = cur.Length;
                // Do not abbreviate
                GenerateAbbreviationsHelper(result, word, cur, pos + 1, count + 1);
                // Reset cur
                cur.Length = unChanged;

                // Abbreviate at current pos.
                if (count != 0)
                {
                    cur.Append(count);
                }
                cur.Append(word[pos]);

                // Check next abbreviate after abbreviate at current pos.
                GenerateAbbreviationsHelper(result, word, cur, pos + 1, 0);
            }
        }
    }
}
