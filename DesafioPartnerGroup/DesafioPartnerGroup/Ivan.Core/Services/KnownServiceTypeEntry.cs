using System;

namespace Ivan.Services
{

    public sealed class KnownServiceTypeEntry
    {

        private Type service;
        private Type type;

        public Type Service
        {
            get
            {
                return service;
            }
        }

        public Type Type
        {
            get
            {
                return type;
            }
        }

        public KnownServiceTypeEntry(Type type, Type serviceType)
        {
            if (type == null)
                throw new ArgumentNullException("type");
            if (serviceType == null)
                throw new ArgumentNullException("serviceType");
            this.type = type;
            service = serviceType;
        }

        public KnownServiceTypeEntry(string typeName, string serviceTypeName) : this(Type.GetType(typeName), Type.GetType(serviceTypeName))
        {
        }

    } // class KnownServiceTypeEntry

}

