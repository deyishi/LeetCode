using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeetCode.DataModel
{
    public class MyHashMap
    {
        private const int Size = 10000;
        private readonly HashMapListNode[] _buckets;

        /** Initialize your data structure here. */
        public MyHashMap()
        {
            _buckets = new HashMapListNode[Size];
        }

        /** value will always be non-negative. */
        public void Put(int key, int value)
        {
            var bucketKey = Hash(key);
            if (_buckets[bucketKey] == null)
            {
                _buckets[bucketKey] = new HashMapListNode(-1, -1);
            }

            HashMapListNode head = FindBucketNode(_buckets[bucketKey], key);
            if (head.Next == null)
            {
                head.Next = new HashMapListNode(key, value);
            }
            else
            {
                head.Next.Value = value;
            }
        }


        /** Returns the value to which the specified key is mapped, or -1 if this map contains no mapping for the key */
        public int Get(int key)
        {
            var bucketKey = Hash(key);
            if (_buckets[bucketKey] == null)
            {
                return -1;
            }

            HashMapListNode head = FindBucketNode(_buckets[bucketKey], key);
            return head.Next?.Value ?? -1;
        }

        /** Removes the mapping of the specified value key if this map contains a mapping for the key */
        public void Remove(int key)
        {
            var bucketKey = Hash(key);
            if (_buckets[bucketKey] == null)
            {
                return;
            }

            HashMapListNode head = FindBucketNode(_buckets[bucketKey], key);
            if (head.Next != null)
            {
                head.Next = head.Next.Next;
            }
        }

        private HashMapListNode FindBucketNode(HashMapListNode head, int key)
        {
            HashMapListNode curr = head;
            while (curr != null && curr.Next != null &&curr.Next.Key != key)
            {
                curr = curr.Next;
            }

            return curr;
        }


        //used to find the bucket
        private int Hash(int key)
        {
            return key.GetHashCode() % Size;
        }
    }

    public class HashMapListNode
    {
        public HashMapListNode Next;
        public int Key;
        public int Value;

        public HashMapListNode(int key, int value)
        {
            Key = key;
            Value = value;
        }
    }
}
