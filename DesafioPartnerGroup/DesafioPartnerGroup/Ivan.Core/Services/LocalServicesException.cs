using System;
using System.Runtime.Serialization;

namespace Ivan.Services
{

    [Serializable]
    public class LocalServicesException : Exception
    {

        public LocalServicesException()
        {
        }

        public LocalServicesException(string message) : base(message)
        {
        }

        protected LocalServicesException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

    } // class LocalServicesException

}

