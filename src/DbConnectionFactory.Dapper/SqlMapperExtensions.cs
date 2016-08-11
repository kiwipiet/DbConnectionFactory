using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;

namespace DbConnectionFactory
{
    public static class SqlMapperExtensions
    {
        /// <summary>
        ///     Execute parameterized SQL
        /// </summary>
        /// <returns>
        ///     Number of rows affected
        /// </returns>
        public static int Execute(this IDatabaseConnection cnn, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null,
            CommandType? commandType = null)
        {
            return cnn.Connection.Execute(sql, param, transaction, commandTimeout, commandType);
        }

        /// <summary>Execute a command asynchronously using .NET 4.5 Task.</summary>
        public static Task<int> ExecuteAsync(this IDatabaseConnection cnn, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return cnn.Connection.ExecuteAsync(sql, param, transaction, commandTimeout, commandType);
        }

        /// <summary>
        ///     Execute parameterized SQL that selects a single value
        /// </summary>
        /// <returns>
        ///     The first cell selected
        /// </returns>
        public static T ExecuteScalar<T>(this IDatabaseConnection cnn, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null,
            CommandType? commandType = null)
        {
            return cnn.Connection.ExecuteScalar<T>(sql, param, transaction, commandTimeout, commandType);
        }

        /// <summary>
        ///     Executes a query, returning the data typed as per T
        /// </summary>
        /// <remarks>
        ///     the dynamic param may seem a bit odd, but this works around a major usability issue in vs, if it is Object vs
        ///     completion gets annoying. Eg type new [space] get new object
        /// </remarks>
        /// <returns>
        ///     A sequence of data of the supplied type; if a basic type (int, string, etc) is queried then the data from the first
        ///     column in assumed, otherwise an instance is
        ///     created per row, and a direct column-name===member-name mapping is assumed (case insensitive).
        /// </returns>
        public static IEnumerable<T> Query<T>(this IDatabaseConnection cnn, string sql, object param = null, IDbTransaction transaction = null, bool buffered = true,
            int? commandTimeout = null, CommandType? commandType = null)
        {
            return cnn.Connection.Query<T>(sql, param, transaction, buffered, commandTimeout, commandType);
        }

        /// <summary>
        ///     Execute a query asynchronously using .NET 4.5 Task.
        /// </summary>
        public static Task<IEnumerable<T>> QueryAsync<T>(this IDatabaseConnection cnn, string sql, object param = null, IDbTransaction transaction = null,
            int? commandTimeout = null, CommandType? commandType = null)
        {
            return cnn.Connection.QueryAsync<T>(sql, param, transaction, commandTimeout, commandType);
        }

        /// <summary>
        ///     Execute a command that returns multiple result sets, and access each in turn
        /// </summary>
        public static IMultiQueryResult QueryMultiple(this IDatabaseConnection cnn, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null,
            CommandType? commandType = null)
        {
            return new MultiQueryResult(cnn.Connection.QueryMultiple(sql, param, transaction, commandTimeout, commandType));
        }

        /// <summary>
        /// Execute a command that returns multiple result sets, and access each in turn
        /// 
        /// </summary>
        public static async Task<IMultiQueryResult> QueryMultipleAsync(this IDatabaseConnection cnn, string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            var result = await cnn.Connection.QueryMultipleAsync(sql, param, transaction, commandTimeout, commandType).ConfigureAwait(false);
            return new MultiQueryResult(result);
        }
    }
}