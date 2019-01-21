using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode.DataModel
{
    public class TrieNode
    {
        public Dictionary<char, TrieNode> Children;
        public bool IsWordEnd;
        public TrieNode()
        {
            Children = new Dictionary<char, TrieNode>();
        }
    }
}
