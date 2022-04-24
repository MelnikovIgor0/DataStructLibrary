using System;
using System.Collections.Generic;
using System.Text;

namespace DataStructLibrary
{
    [Serializable]
    public class SparseTableException : DataStructException
    {
        public SparseTableException() { }

        public SparseTableException(string message) : base("Sparse table exception: " + message) { }

        public SparseTableException(string message, Exception inner) : base("Sparse table exception: " + message,
            inner) { }

        protected SparseTableException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
