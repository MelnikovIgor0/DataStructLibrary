using System;
using System.Collections.Generic;
using System.Text;

namespace DataStructLibrary
{
    public sealed class DisjointSetUnion<T>
    {
        private Dictionary<T, int> keys;
        private List<int> parents;
        private List<int> height;

        private int GetRoot(int vertex)
        {
            if (parents[vertex] == vertex) return vertex;
            int totalRoot = GetRoot(parents[vertex]);
            parents[vertex] = totalRoot;
            return totalRoot;
        }

        public DisjointSetUnion()
        {
            keys = new Dictionary<T, int>();
            parents = new List<int>();
            height = new List<int>();
        }

        public void AddElement(T element)
        {
            if (keys.ContainsKey(element))
                throw new DisjointSetUnionException("Such an element is already placed in set.");
            if (keys.Count >= (1 << 28)) 
                throw new DisjointSetUnionException("Amount of elements limit reached");
            keys.Add(element, keys.Count);
            parents.Add(parents.Count);
            height.Add(1);
        }

        public void Unite(T first, T second)
        {
            if (!keys.ContainsKey(first) || !keys.ContainsKey(second))
                throw new DisjointSetUnionException("One of keys does not exist");
            int index1 = GetRoot(keys[first]), index2 = GetRoot(keys[second]);
            if (height[index1] < height[index2]) parents[index1] = index2;
            else parents[index2] = index1;
            if (height[index1] == height[index2])
            {
                height[index1]++;
                height[index2]++;
            }
        }

        public bool Check(T first, T second)
        {
            if (!keys.ContainsKey(first) || !keys.ContainsKey(second))
                throw new DisjointSetUnionException("One of keys does not exist");
            return GetRoot(keys[first]) == GetRoot(keys[second]);
        }

        public void Clear()
        {
            keys.Clear();
            parents.Clear();
            height.Clear();
        }

        public int Count { get => parents.Count; }
    }
}
