using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeetCode.DataModel;

namespace LeetCode.Design
{
    public class LRUCache
    {
        private readonly Dictionary<int, Node> _cache;
        private readonly int _capacity;
        private readonly Node _head;
        private readonly Node _tail;

        //146. LRU Cache
        //Use hash table to keep track of the keys and doubly linked list node(key and value).
        //Head.next stores the least used(key, value).
        //Head.tail stores the most recent used(key, value).
        //Get: get current dict[key], if exists, remove it then add it to the end of the list so it becomes the most recent used value.
        //Put: call Get(key), if it exists, it has been updated and return its value.
        //If size reached limit, remove the least used node from hash table by key and then remove from linked list by (head.next). 
        //Create a new node(key, value), insert to hash table then link it to the last of the linked list.
        public LRUCache(int capacity)
        {
            _cache = new Dictionary<int, Node>();
            _capacity = capacity;
            _head = new Node(-1, -1);
            _tail = new Node(-1, -1);
            _head.Next = _tail;
            _tail.Prev = _head;

        }

        public int Get(int key)
        {
            if (!_cache.ContainsKey(key))
            {
                return -1;
            }

            Node current = _cache[key];
            current.Prev.Next = current.Next;
            current.Next.Prev = current.Prev;

            MoveNodeToTail(current);

            return current.Value;

        }

        public void Put(int key, int value)
        {
            if (Get(key) != -1)
            {
                // Refresh this key's node and value.
                _cache[key].Value = value;
                return;
            }

            // Remove least used node
            if (_cache.Count == _capacity)
            {
                _cache.Remove(_head.Next.Key);
                _head.Next = _head.Next.Next;
                _head.Next.Prev = _head;
            }

            Node newNode = new Node(key, value);
            _cache.Add(key, newNode);
            MoveNodeToTail(newNode);
        }

        private void MoveNodeToTail(Node current)
        {
            current.Prev = _tail.Prev;
            _tail.Prev = current;
            current.Prev.Next = current;
            current.Next = _tail;
        }

        public class Node
        {
            public Node Prev;
            public Node Next;
            public int Key;
            public int Value;

            public Node(int key, int value)
            {
                Key = key;
                Value = value;
                Prev = null;
                Next = null;
            }
        }
    }
}
