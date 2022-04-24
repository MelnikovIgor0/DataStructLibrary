using System;
using System.Collections.Generic;
using System.Text;

namespace DataStructLibrary
{
    [Serializable]
    public class DataStructException : Exception
    {
        public DataStructException() { }

        public DataStructException(string message) : base(message) { }

        public DataStructException(string message, Exception inner) : base(message, inner) { }

        protected DataStructException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
