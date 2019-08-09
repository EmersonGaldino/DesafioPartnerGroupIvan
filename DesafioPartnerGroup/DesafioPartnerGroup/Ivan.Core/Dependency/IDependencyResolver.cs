using System;

namespace Ivan.Dependency
{
    public interface IDependencyResolver : IDisposable
    {
        bool TryGetImplementationOf(Type type, out object instance);
    }
}
