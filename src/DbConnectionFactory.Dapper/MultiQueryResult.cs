using System.Collections.Generic;
using Dapper;

namespace DbConnectionFactory
{
    internal sealed class MultiQueryResult : IMultiQueryResult
    {
        private readonly SqlMapper.GridReader _results;

        public MultiQueryResult(SqlMapper.GridReader results)
        {
            _results = results;
        }

        public IEnumerable<T> Read<T>()
        {
            return _results.Read<T>();
        }

        public void Dispose()
        {
            _results.Dispose();
        }
    }
}