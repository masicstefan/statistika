using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using Dapper;


namespace APIStatistikaApp.DataAccess.DbAccess
{
    public class SQLDataAccess : ISQLDataAccess
    {
        private readonly IConfiguration _config;
        

        public SQLDataAccess(IConfiguration config)
        {
            _config = config;
        }

        // Load ALL.
        public async Task<IEnumerable<T>> LoadAll<T>(string sqlString, string connectionString = "Default")
        {
            using IDbConnection connection = new SqlConnection(_config.GetConnectionString(connectionString));
            return await connection.QueryAsync<T>(sqlString, commandType: CommandType.Text);
        }

        // Load ONE.
        public async Task<IEnumerable<T>> LoadOne<T, U>(string sqlString, U parameter, string connectionString = "Default")
        {
            using IDbConnection connection = new SqlConnection(_config.GetConnectionString(connectionString));
            return await connection.QueryAsync<T>(sqlString, parameter, commandType: CommandType.Text);
        }

        // Save ONE.
        public async Task SaveOne<T>(string sqlString, T parameter, string connectionString = "Default")
        {
            using IDbConnection connection = new SqlConnection(_config.GetConnectionString(connectionString));
            await connection.ExecuteAsync(sqlString, parameter, commandType: CommandType.Text);
        }
    }
}
