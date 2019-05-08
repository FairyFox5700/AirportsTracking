using System;

namespace Structures
{
    class KeyValuePair<T> : IComparable<KeyValuePair>
    {
        T key;
        T value_key;

        public KeyValuePair(T key, T value_key)
        {
            this.key = key;
            this.value_key = value_key;
        }

        public int CompareTo(KeyValuePair kvp)
        {
            return this.key.CompareTo(kvp.key);
        }
    }
    
    class  PriorityQueue<T, T>
    {
        AVLTree<KeyValuePair> tree;
        List<T> values;
        int count;

        PriorityQueue()
        {
            tree = new AVLTree<KeyValuePair>();
            values = new List<Queue>();
        }

        public void Add(T key, T value)
        {
            values.Add(value);
            KeyValuePair newkvpair = new KeyValuePair(key, values.Count());
            try
            {
                tree.Add(newkvpair);
            }
            catch(ArgumentException)
            {
                throw new ArgumentException("This key exists now!");
            }
        }
    }
}
