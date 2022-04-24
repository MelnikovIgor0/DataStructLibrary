using System;
using System.Collections.Generic;
using System.Text;

namespace DataStructLibrary
{
    [Serializable]
    public class SegmentTreeException : DataStructException
    {
        public SegmentTreeException() { }
        
        public SegmentTreeException(string message) : base("Segment tree exception: " + message) { }
        
        public SegmentTreeException(string message, Exception inner) : base("Segment tree exception: " + message,
            inner) { }

        protected SegmentTreeException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
