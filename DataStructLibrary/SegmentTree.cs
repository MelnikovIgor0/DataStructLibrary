using System;
using System.Collections.Generic;
using System.Text;

namespace DataStructLibrary
{
    public sealed class SegmentTree<T>
    {
        private T[] data;
        private T[] buffer;
        private Func<T, T, T> combiner;
        private int size, amountElements;
        private T defaultValue;

        public SegmentTree(T[] value, Func<T, T, T> combiner, T defaultValue)
        {
            if (combiner == null || combiner.GetInvocationList().Length != 1)
                throw new SegmentTreeException("Delegate for getting combination of nodes must have only 1 method.");
            if (value == null)
                throw new ArgumentNullException("Array can not be null.");
            if (value.Length > (1 << 28))
                throw new SegmentTreeException("Segment tree size can not be more than 2^28.");
            amountElements = value.Length;
            this.combiner = (Func<T, T, T>)combiner.Clone();
            this.defaultValue = defaultValue;
            size = 1;
            while (size < value.Length) size <<= 1;
            data = new T[size << 1];
            buffer = (T[])value.Clone();
            Build(1, 1, size);
        }

        private void Build(int vertex, int treeLeft, int treeRight)
        {
            if (treeLeft == treeRight)
            {
                if (treeLeft - 1 < buffer.Length) data[vertex] = buffer[treeLeft - 1];
                else data[vertex] = defaultValue;
            }
            else
            {
                int treeMedium = (treeLeft + treeRight) >> 1;
                Build(vertex << 1, treeLeft, treeMedium);
                Build((vertex << 1) + 1, treeMedium + 1, treeRight);
                data[vertex] = combiner(data[vertex << 1], data[(vertex << 1) + 1]);
            }
        }

        private T GetSegmentValue(int vertex, int treeLeft, int treeRight, int leftBorder, int rightBorder)
        {
            if (treeLeft == leftBorder && treeRight == rightBorder) return data[vertex];
            int treeMedium = (treeLeft + treeRight) >> 1;
            T leftValue = defaultValue, rightValue = defaultValue;
            bool leftCounted = false, rightCounted = false;
            if (leftBorder <= Math.Min(treeMedium, rightBorder))
            {
                leftValue = GetSegmentValue(vertex << 1, treeLeft, treeMedium, 
                    leftBorder, Math.Min(treeMedium, rightBorder));
                leftCounted = true;
            }
            if (Math.Max(leftBorder, treeMedium + 1) <= rightBorder)
            {
                rightValue = GetSegmentValue((vertex << 1) + 1, treeMedium + 1, treeRight,
                    Math.Max(leftBorder, treeMedium + 1), rightBorder);
                rightCounted = true;
            }
            if (leftCounted && rightCounted) return combiner(leftValue, rightValue);
            if (leftCounted) return leftValue;
            return rightValue;
        }

        private void Update(int vertex, int treeLeft, int treeRight, int position, T value, bool inversion)
        {
            if (treeLeft == treeRight)
            {
                if (inversion) data[vertex] = value; 
                else data[vertex] = value;
            }
            else
            {
                int treeMedium = (treeLeft + treeRight) >> 1;
                if (position <= treeMedium) Update(vertex << 1, treeLeft, treeMedium, position, value, inversion);
                else Update((vertex << 1) + 1, treeMedium + 1, treeRight, position, value, inversion);
                data[vertex] = combiner(data[vertex << 1], data[(vertex << 1) + 1]);
            }
        }

        public T Query(int leftBorder, int rightBorder)
        {
            if (leftBorder < 0 || leftBorder >= amountElements || rightBorder < 0 ||
                rightBorder >= amountElements || leftBorder > rightBorder) 
                throw new IndexOutOfRangeException("Borders of query are out of " +
                    "range or left border more than right border.");
            return GetSegmentValue(1, 1, size, leftBorder + 1, rightBorder + 1);
        }

        public T this[int index]
        {
            get
            {
                if (index < 0 || index >= amountElements)
                    throw new IndexOutOfRangeException("Index of tree node is out of range.");
                return data[size + index];
            }
            set
            {
                if (index < 0 || index >= amountElements)
                    throw new IndexOutOfRangeException("Index of tree node is out of range.");
                Update(1, 1, size, index + 1, value, false);
            }
        }

        public T this[int index, bool inversion]
        {
            set
            {
                if (index < 0 || index >= amountElements)
                    throw new IndexOutOfRangeException("Index of tree node is out of range.");
                Update(1, 1, size, index + 1, value, inversion);
            }
        }

        public int Count { get => amountElements; }

        public Func<T, T, T> GetCombiner { get => combiner; }

        public T DefaultValue { get => defaultValue; }
    }
}
