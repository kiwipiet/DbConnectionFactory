using System;
using System.Data;

namespace DbConnectionFactory.Core
{
    public interface IDatabaseConnection : IDisposable
    {
        IDbConnection Connection { get; }
    }
}