using System;
using System.Data;

namespace DbConnectionFactory
{
    public interface IDatabaseConnection : IDisposable
    {
        IDbConnection Connection { get; }
    }
}