using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode.DataModel
{
    class WordSquare
    {
        [Test]
        public void Test()
        {
            string[] words = new []{"area","lead","wall","lady","ball"};
            findWords(words);
        }

        public List<String> findWords(String[] words)
        {
            List<List<String>> ans = new List<List<string>>();
            if (words == null || words.Length == 0)
            {
                return null;
            }

            Trie trie = new Trie(words);
            int rows = words[0].Length; // how many rolls in the matrix 
            List<String> ansPath = new List<String>();
            foreach (String w in words)
            {
                ansPath.Add(w);
                search(rows, trie, ans, ansPath);
                ansPath.RemoveAt(ansPath.Count - 1);
            }

            return ansPath;

        }

        public void search(int rows, Trie trie, List<List<String>> ans, List<String> ansPath)
        {
            if (ansPath.Count == rows)
            {
                // we got a matrix here
                ans.Add(new List<string>(ansPath));
                return;
            }

            // area  
            // r
            // possible options need to be words start with r 
            // wall
            int prefixLen = ansPath.Count;
            StringBuilder prefixBuilder = new StringBuilder();
            foreach (String s in ansPath)
            {
                prefixBuilder.Append(s[prefixLen]);
            }

            List<String> wordsWithPrefix = trie.findWordsByPrefix(prefixBuilder.ToString());
            foreach (String sw in wordsWithPrefix)
            {
                ansPath.Add(sw);
                search(rows, trie, ans, ansPath);
                ansPath.RemoveAt(ansPath.Count - 1);
            }
        }
        public class TrieNode
        {
            public List<String> words; // words with this prefix
            public TrieNode[] children;

            public TrieNode()
            {
                words = new List<String>();
                children = new TrieNode[26];
            }
        }

        public class Trie
        {

            public TrieNode root;

            public Trie(String[] words)
            {
                root = new TrieNode();
                foreach (String word in words)
                {
                    TrieNode curr = root;
                    foreach (char c in word)
                    {
                        int index = c - 'a';
                        if (curr.children[index] == null)
                        {
                            curr.children[index] = new TrieNode();
                        }
                        curr.children[index].words.Add(word);// so this prefix contains this word
                        curr = curr.children[index];
                    }
                }
            }

            public List<String> findWordsByPrefix(String prefix)
            {
                List<String> ans = new List<String>();
                TrieNode curr = root;
                foreach (char c in prefix)
                {
                    int index = c - 'a';
                    if (curr.children[index] == null)
                    {
                        return ans;
                    }
                    curr = curr.children[index];
                }
                ans.AddRange(curr.words);

                return ans;
            }

        }
    }
}
