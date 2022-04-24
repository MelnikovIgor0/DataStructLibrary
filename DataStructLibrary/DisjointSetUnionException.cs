using System;
using System.Collections.Generic;
using System.Text;

namespace DataStructLibrary
{
    [Serializable]
    public class DisjointSetUnionException : DataStructException
    {
        public DisjointSetUnionException() { }

        public DisjointSetUnionException(string message) : base("Disjoint set union exception: " + message) { }

        public DisjointSetUnionException(string message, Exception inner) : base("Disjoint set union exception: "
            + message, inner) { }

        protected DisjointSetUnionException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
