using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Npgsql;
using TeacherConnect.Data.Interfaces;
using TeacherConnect.Entities;
using static Dapper.SqlMapper;

namespace TeacherConnect.Data
{
    public class BaseRepository : IRepository
    {

        protected BaseRepository(IConfiguration configuration, string connectionString)
        {
            ConnectionString = configuration.GetConnectionString(connectionString);
        }

        protected string ConnectionString { get; private set; }

        public virtual async Task<T> ExecuteScalarAsync<T>(string sql, object param = null)
        {
            using (IDbConnection conn = new NpgsqlConnection(ConnectionString))
            {
                var data = await conn.ExecuteScalarAsync<T>(sql, param);
                return data;
            }
        }

        public virtual async Task<int> ExecuteAsync(string sql, object param = null, CommandType? commandType = null)
        {
            using (IDbConnection conn = new NpgsqlConnection(ConnectionString))
            {

                return await conn.ExecuteAsync(sql, param, commandType: commandType);
            }
        }

        public virtual async Task<IEnumerable<TReturn>> QueryAsync<TFirst, TSecond, TReturn>(string sql, Func<TFirst, TSecond, TReturn> map, object param = null, string splitOn = "Id", CommandType? commandType = null)
        {
            using (IDbConnection conn = new NpgsqlConnection(ConnectionString))
            {
                return await conn.QueryAsync<TFirst, TSecond, TReturn>(sql, map, param, null, true, splitOn, null, commandType);
            }
        }

        public virtual async Task<List<T>> QueryAsync<T>(string sql, object param = null)
        {
            using (IDbConnection conn = new NpgsqlConnection(ConnectionString))
            {
                return (await conn.QueryAsync<T>(sql, param)).ToList();
            }
        }

        public virtual async Task<T> QueryFirstOrDefaultAsync<T>(string sql, object param = null)
        {
            using (IDbConnection conn = new NpgsqlConnection(ConnectionString))
            {
                return (await conn.QueryFirstOrDefaultAsync<T>(sql, param));
            }
        }

        public virtual async Task<T> QueryFirstAsync<T>(string sql, object param = null)
        {
            using (IDbConnection conn = new NpgsqlConnection(ConnectionString))
            {
                return (await conn.QueryFirstAsync<T>(sql, param));
            }
        }

        public virtual async Task<GridReader> QueryMultipleAsync(string sql, object param = null, CommandType? commandType = null)
        {
            using (IDbConnection conn = new NpgsqlConnection(ConnectionString))
            {
                return (await conn.QueryMultipleAsync(sql, param, null, null, commandType));
            }
        }

        public virtual async Task<List<T>> StoredProcedureAsync<T>(string name, object param = null)
        {
            using (IDbConnection conn = new SqlConnection(ConnectionString))
            {
                return (await conn.QueryAsync<T>(name, param, commandType: CommandType.StoredProcedure)).ToList();
            }
        }
    }
}