using System;
using System.Collections.Generic;

namespace Ivan.Dependency
{
    public class SimpleDependencyResolver : IDependencyResolver, IDisposable
    {
        private bool disposed;

        private object syncRoot = new object();

        private volatile Dictionary<Type, object> components;

        public void RegisterImplementationOf<T>(T component) where T : class
        {
            if (this.disposed)
            {
                throw new ObjectDisposedException("SimpleDependencyResolver.RegisterImplementationOf disposed");
            }
            if (component == null)
            {
                throw new ArgumentNullException("SimpleDependencyResolver.RegisterImplementationOf component is null");
            }
            this.RegisterImplementationOf(typeof(T), component);
        }

        public void RegisterImplementationOf(Type type, object component)
        {
            if (this.disposed)
            {
                throw new ObjectDisposedException("SimpleDependencyResolver.RegisterImplementationOf disposed");
            }
            if (type == null)
            {
                throw new ArgumentNullException("SimpleDependencyResolver.RegisterImplementationOf the type is null");
            }
            if (component == null)
            {
                throw new ArgumentNullException("SimpleDependencyResolver.RegisterImplementationOf component is null");
            }
            lock (this.syncRoot)
            {
                Dictionary<Type, object> dictionary;
                if (this.components == null)
                {
                    dictionary = new Dictionary<Type, object>();
                }
                else
                {
                    dictionary = new Dictionary<Type, object>(this.components);
                }
                dictionary[type] = component;
                this.components = dictionary;
            }
        }

        public void UnregisterImplementationOf<T>()
        {
            if (this.disposed)
            {
                throw new ObjectDisposedException("SimpleDependencyResolver.UnregisterImplementationOf disposed");
            }
            this.UnregisterImplementationOf(typeof(T));
        }

        public void UnregisterImplementationOf(Type type)
        {
            if (this.disposed)
            {
                throw new ObjectDisposedException("SimpleDependencyResolver.UnregisterImplementationOf disposed");
            }
            if (type == null)
            {
                throw new ArgumentNullException("SimpleDependencyResolver.UnregisterImplementationOf the type is null");
            }
            lock (this.syncRoot)
            {
                if (this.components != null)
                {
                    Dictionary<Type, object> dictionary = new Dictionary<Type, object>(this.components);
                    dictionary.Remove(type);
                    this.components = dictionary;
                }
            }
        }

        public bool TryGetImplementationOf(Type type, out object instance)
        {
            if (this.disposed)
            {
                throw new ObjectDisposedException("SimpleDependencyResolver.TryGetImplementationOf disposed");
            }
            if (type == null)
            {
                throw new ArgumentNullException("SimpleDependencyResolver.TryGetImplementationOf the type is null");
            }
            Dictionary<Type, object> dictionary = this.components;
            if (dictionary != null && dictionary.TryGetValue(type, out instance))
            {
                return true;
            }
            instance = null;
            return false;
        }

        public void Dispose()
        {
            lock (this.syncRoot)
            {
                Dictionary<Type, object> dictionary = this.components;
                this.components = null;
                dictionary.Clear();
            }
            this.disposed = true;
        }
    }
}
