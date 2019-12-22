using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace DbConnectionFactory
{
    public sealed class DatabaseConnectionFactory : IDatabaseConnectionFactory
    {
        private readonly string _connectionString;

        public DatabaseConnectionFactory(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        public IDatabaseConnection Create()
        {
            var sqlConnection = new SqlConnection(_connectionString);
            sqlConnection.InfoMessage += OnInfoMessage;
            return new InternalDatabaseConnection(sqlConnection);
        }

        private static void OnInfoMessage(object sender, SqlInfoMessageEventArgs args)
        {
            Trace.TraceWarning(args.Message);
        }

        private sealed class InternalDatabaseConnection : IDatabaseConnection
        {
            public InternalDatabaseConnection(IDbConnection sqlConnection)
            {
                Connection = sqlConnection;
            }

            public void Dispose()
            {
                Connection.Dispose();
            }

            public IDbConnection Connection { get; }
        }
    }
}