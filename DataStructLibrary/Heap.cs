using System;
using System.Collections.Generic;
using System.Text;

namespace DataStructLibrary
{
    public sealed class Heap<T>
    {
        private List<T> data;

        public Func<T, T, bool> comp { get; private set; }

        private T DefaultValue { get; set; }

        public Heap(Func<T, T, bool> comparer, T defaultValue = default)
        {
            if (comparer == null || comparer.GetInvocationList().Length != 1)
                throw new HeapException("Delegate for comparing two values must have only 1 method.");
            comp = comparer;
            DefaultValue = defaultValue;
            data = new List<T>();
            data.Add(DefaultValue);
        }

        private void CiftUp()
        {
            T buffer;
            int newVertex, vertex = data.Count - 1;
            while ((newVertex = vertex >> 1) != 0 && comp(data[vertex], data[newVertex]))
            {
                buffer = data[newVertex];
                data[newVertex] = data[vertex];
                data[vertex] = buffer;
                vertex = newVertex;
            }
        }

        private void CiftDown()
        {
            T buffer;
            int minVertex, vertex = 1;
            while ((minVertex = (vertex << 1)) < data.Count)
            {
                if (minVertex + 1 < data.Count && comp(data[minVertex + 1], data[minVertex])) ++minVertex;
                if (comp(data[vertex], data[minVertex])) return;
                buffer = data[vertex];
                data[vertex] = data[minVertex];
                data[minVertex] = buffer;
                vertex = minVertex;
            }
        }

        public void Push(T value)
        {
            if (data.Count >= (1 << 28))
                throw new HeapException("Amount of elements limit reached");
            data.Add(value);
            CiftUp();
        }

        public void Pop()
        {
            if (data.Count <= 1)
                throw new HeapException("Heap is empty");
            data[1] = data[data.Count - 1];
            data.RemoveAt(data.Count - 1);
            CiftDown();
        }

        public T Top()
        {
            if (data.Count <= 1)
                throw new HeapException("Heap is empty");
            return data[1];
        }

        public bool Empty()
        {
            return data.Count <= 1;
        }

        public void Clear()
        {
            data.Clear();
            data = new List<T>();
            data.Add(DefaultValue);
        }
    }
}
