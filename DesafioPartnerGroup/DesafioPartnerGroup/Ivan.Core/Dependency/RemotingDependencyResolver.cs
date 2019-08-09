using System;
using System.Collections.Generic;
using System.Runtime.Remoting;

namespace Ivan.Dependency
{
    public class RemotingDependencyResolver : IDependencyResolver, IDisposable
    {
        private bool disposed;

        private volatile Dictionary<Type, WellKnownClientTypeEntry> services;

        public void Configure()
        {
            if (this.disposed)
            {
                throw new ObjectDisposedException("RemotingDependencyResolver");
            }
            Dictionary<Type, WellKnownClientTypeEntry> dictionary = new Dictionary<Type, WellKnownClientTypeEntry>();
            WellKnownClientTypeEntry[] registeredWellKnownClientTypes = RemotingConfiguration.GetRegisteredWellKnownClientTypes();
            WellKnownClientTypeEntry[] array = registeredWellKnownClientTypes;
            for (int i = 0; i < array.Length; i++)
            {
                WellKnownClientTypeEntry wellKnownClientTypeEntry = array[i];
                if (wellKnownClientTypeEntry.ObjectType != null && wellKnownClientTypeEntry.ObjectType.IsInterface)
                {
                    dictionary[wellKnownClientTypeEntry.ObjectType] = wellKnownClientTypeEntry;
                }
            }
            this.services = dictionary;
        }

        public bool TryGetImplementationOf(Type type, out object instance)
        {
            if (this.disposed)
            {
                throw new ObjectDisposedException("RemotingDependencyResolver");
            }
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }
            Dictionary<Type, WellKnownClientTypeEntry> dictionary = this.services;
            WellKnownClientTypeEntry wellKnownClientTypeEntry;
            if (dictionary != null && dictionary.TryGetValue(type, out wellKnownClientTypeEntry))
            {
                instance = Activator.GetObject(wellKnownClientTypeEntry.ObjectType, wellKnownClientTypeEntry.ObjectUrl);
                return true;
            }
            instance = null;
            return false;
        }

        public void Dispose()
        {
            this.services = null;
            this.disposed = true;
        }
    }
}
