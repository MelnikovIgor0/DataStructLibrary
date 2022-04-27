using System;
using System.Collections.Generic;
using System.Text;

namespace DataStructLibrary
{
    [Serializable]
    public class HeapException : Exception
    {
        public HeapException() { }

        public HeapException(string message) : base("Heap exception: " + message) { }

        public HeapException(string message, Exception inner) : base("Heap exception: " + message, inner) { }

        protected HeapException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
