using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode.DataModel
{
    public class Trie
    {
        private readonly TrieNode _root;
        public Trie()
        {
            _root = new TrieNode();
        }

        /** Inserts a word into the trie. */
        public void Insert(string word)
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

        /** Returns if the word is in the trie. */
        public bool Search(string word)
        {
            TrieNode current = _root;
            foreach (var c in word.ToCharArray())
            {
                if (!current.Children.ContainsKey(c))
                {
                    return false;
                }

                current = current.Children[c];
            }

            return current.IsWordEnd;
        }

        /** Returns if there is any word in the trie that starts with the given prefix. */
        public bool StartsWith(string prefix)
        {
            TrieNode current = _root;
            foreach (var c in prefix.ToCharArray())
            {
                if (!current.Children.ContainsKey(c))
                {
                    return false;
                }

                current = current.Children[c];
            }

            return true;
        }
    }
}
