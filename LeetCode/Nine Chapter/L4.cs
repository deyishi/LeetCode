using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace LeetCode.Nine_Chapter
{
    public class L4
    {
        [Test]
        public void CanFinish()
        {
            var n = 2;
            var g = new [,] { { 1, 0 } };

            var t = new int[2];
            var r = CanFinishOne(n, g);
        }


        public bool CanFinishOne(int numCourses, int[,] prerequisites)
        {
            if (numCourses <= 0)
            {
                return false;
            }

            if (prerequisites == null || prerequisites.GetLength(0) == 0)
            {
                return true;
            }

            var graph = MakeGraph(prerequisites);
            var inDegrees = GetInDegrees(graph);


            var q = new Queue<int>();
            foreach (var i in graph.Keys)
            {
                if (!inDegrees.ContainsKey(i))
                {
                    q.Enqueue(i);
                }
            }

            while (q.Any())
            {
                var node = q.Dequeue();
                foreach (var n in graph[node])
                {
                    inDegrees[n]--;
                    if (inDegrees[n] == 0 && graph.ContainsKey(n))
                    {
                        q.Enqueue(n);
                    }
                }
            }

            return inDegrees.All(x => x.Value == 0);
        }

        private Dictionary<int, int> GetInDegrees(Dictionary<int, List<int>> graph)
        {
            var degrees = new Dictionary<int, int>();
            foreach (var node in graph)
            {
                foreach (var neighbor in node.Value)
                {
                    if (degrees.ContainsKey(neighbor))
                    {
                        degrees[neighbor]++;
                    }
                    else
                    {
                        degrees.Add(neighbor, 1);
                    }

                }
            }

            return degrees;
        }

        private Dictionary<int, List<int>> MakeGraph(int[,] prerequisites)
        {
            var graph = new Dictionary<int, List<int>>();
            for (var i = 0; i < prerequisites.GetLength(0); i++)
            {
                var val = prerequisites[i, 1];
                var neighbor = prerequisites[i, 0];
                if (graph.ContainsKey(val))
                {
                    graph[val].Add(neighbor);
                }
                else
                {
                    graph.Add(val, new List<int> { neighbor });
                }
            }

            return graph;
        }

        public int[] FindOrder(int numCourses, int[,] prerequisites)
        {
            var result = new int[numCourses];
            if (numCourses <= 0 || prerequisites == null)
            {
                return result;
            }

            //Graph and InDegrees
            var graph = new Dictionary<int, List<int>>();
            var inDegrees = new int[numCourses];
            for (var i = 0; i < prerequisites.GetLength(0); i++)
            {
                inDegrees[prerequisites[i, 0]]++;
                if (graph.ContainsKey(prerequisites[i, 1]))
                {
                    graph[prerequisites[i, 1]].Add(prerequisites[i, 0]);
                }
                else
                {
                    graph.Add(prerequisites[i, 1], new List<int> { prerequisites[i, 0] });
                }
            }

            var first = 0;
            var last = 0;

            for (var i = 0; i < numCourses; i++)
            {
                if (inDegrees[i] == 0)
                {
                    result[last++] = i;
                }
            }

            while (first < last)
            {
                if (graph.ContainsKey(result[first]))
                {
                    foreach (var n in graph[result[first]])
                    {
                        inDegrees[n]--;
                        if (inDegrees[n] == 0)
                        {
                            result[last++] = n;
                        }
                    }
                }
                first++;
            }

            if (last != numCourses)
            {
                return new int[0];
            }
            return result;
        }
    }
}
