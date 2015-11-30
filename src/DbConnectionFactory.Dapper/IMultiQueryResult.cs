using System;
using System.Collections.Generic;

namespace DbConnectionFactory
{
    public interface IMultiQueryResult : IDisposable
    {
        IEnumerable<T> Read<T>();
    }
}