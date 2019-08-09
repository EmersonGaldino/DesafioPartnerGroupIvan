using System;
using System.Collections.Generic;

namespace Ivan.Dependency
{
    public static class Resolver
    {
        private static object syncRoot = new object();

        private static volatile IList<IDependencyResolver> resolvers;

        public static T GetImplementationOf<T>()
        {
            T result;
            if (!Resolver.TryGetImplementationOf<T>(out result))
            {
                throw new KeyNotFoundException();
            }
            return result;
        }

        public static bool TryGetImplementationOf<T>(out T instance)
        {
            IList<IDependencyResolver> list = Resolver.resolvers;
            if (list != null)
            {
                Type typeFromHandle = typeof(T);
                foreach (IDependencyResolver current in list)
                {
                    object obj;
                    if (current.TryGetImplementationOf(typeFromHandle, out obj))
                    {
                        instance = (T)((object)obj);
                        return true;
                    }
                }
            }
            instance = default(T);
            return false;
        }

        public static void AddResolver(IDependencyResolver resolver)
        {
            lock (Resolver.syncRoot)
            {
                IList<IDependencyResolver> list;
                if (Resolver.resolvers == null)
                {
                    list = new List<IDependencyResolver>();
                }
                else
                {
                    list = new List<IDependencyResolver>(Resolver.resolvers);
                }
                list.Remove(resolver);
                list.Add(resolver);
                Resolver.resolvers = list;
            }
        }

        public static void RemoveResolver(IDependencyResolver resolver)
        {
            lock (Resolver.syncRoot)
            {
                if (Resolver.resolvers != null)
                {
                    IList<IDependencyResolver> list = new List<IDependencyResolver>(Resolver.resolvers);
                    list.Remove(resolver);
                    Resolver.resolvers = list;
                }
            }
        }
    }
}
