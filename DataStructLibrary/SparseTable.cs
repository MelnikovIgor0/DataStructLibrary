using System;
using System.Collections.Generic;
using System.Text;

namespace DataStructLibrary
{
    public sealed class SparseTable<T>
    {
        private T[][] data;
        private Func<T, T, T> comparer;
        private static List<int> logarithms = new List<int>();

        private void UpdateLogarithmsList(int lastNumber)
        {
            if (logarithms.Count == 0) logarithms.Add(-1);
            logarithms[0] = -1;
            while (logarithms.Count <= lastNumber)
                logarithms.Add(logarithms[logarithms.Count >> 1] + 1);
            logarithms[0] = 0;
        }

        private int CountLogarithm(int value)
        {
            int pow = 0, n = 1;
            while (n <= value)
            {
                n <<= 1;
                pow++;
            }
            return pow;
        }

        public SparseTable(T[] value, Func<T, T, T> comparer)
        {
            if ((long)(CountLogarithm(value.Length) + 1) * (long)value.Length > (long)(1 << 28))
                throw new SparseTableException("Array too big to create sparse table.");
            if (comparer == null || comparer.GetInvocationList().Length != 1)
                throw new SparseTableException("Delegate for comparing two values must have only 1 method.");
            UpdateLogarithmsList(value.Length);
            this.comparer = comparer;
            data = new T[logarithms[value.Length] + 1][];
            data[0] = new T[value.Length];
            for (int i = 0; i < value.Length; i++) data[0][i] = value[i];
            for (int i = 1; (1 << i) < value.Length; i++)
            {
                data[i] = new T[value.Length - ((1 << i) - 1)];
                for (int j = 0; j < data[i].Length; j++)
                    data[i][j] = comparer(data[i - 1][j], data[i - 1][j + (1 << (i - 1))]);
            }
        }

        public T Query(int leftBorder, int rightBorder)
        {
            rightBorder++;
            int level = logarithms[rightBorder - leftBorder - 1];
            return comparer(data[level][leftBorder], data[level][rightBorder - (1 << level)]);
        }

        public int Count { get => data[0].Length; }

        public Func<T, T, T> GetComparer { get => comparer; }
    }
}
