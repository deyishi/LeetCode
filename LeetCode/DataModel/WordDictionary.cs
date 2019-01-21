using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode.DataModel
{
    /// <summary>
    /// Trie using back track to check each character of word for '.'.
    /// </summary>
    public class WordDictionary
    {
        private class TrieNode
        {
            public Dictionary<char, TrieNode> Children;
            public bool IsWordEnd;
            public TrieNode()
            {
                Children = new Dictionary<char, TrieNode>();
            }
        }

        private readonly TrieNode _root;
        /** Initialize your data structure here. */
        public WordDictionary()
        {
            _root = new TrieNode();
        }

        /** Adds a word into the data structure. */
        public void AddWord(string word)
        {
            TrieNode current = _root;
            foreach (var c in word.ToCharArray())
            {
                if (!current.Children.ContainsKey(c))
                {
                    var node = new TrieNode();
                    current.Children.Add(c, node);
                }

                current = current.Children[c];
            }

            current.IsWordEnd = true;
        }

        /** Returns if the word is in the data structure. A word could contain the dot character '.' to represent any one letter. */
        public bool Search(string word)
        {
            return SearchHelper(_root, word, 0);
        }

        private bool SearchHelper(TrieNode root, string word, int index)
        {
            if (index == word.Length)
            {
                return root.IsWordEnd;
            }

            if (word[index] != '.')
            {
                if (root.Children.ContainsKey(word[index]))
                {
                    return SearchHelper(root.Children[word[index]], word, index + 1);
                }

                return false;
            }

            foreach (var child in root.Children.Values)
            {
                if (SearchHelper(child, word, index + 1))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
