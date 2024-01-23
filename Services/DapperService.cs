using Dapper;
using static Dapper.SqlMapper;
using System.Data;
using System.Data.SqlClient;

namespace AppAPI.Services
{
    public abstract class DapperService
    {
        private readonly IConfiguration _configuration;
        private string _connectionString;
        public DapperService(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = configuration.GetSection("ConnStr").Value;
        }
        protected IDbConnection Connection => new SqlConnection(_connectionString);

        protected async Task<List<T>> GetQueryResultAsync<T>(string sQuery, object parameter = null, IDbTransaction transaction = null)
        {
            var command = new CommandDefinition(sQuery, parameter, transaction);
            var result = await Connection.QueryAsync<T>(command);
            return result.ToList();
        }

        protected async Task<T> GetQueryFirstOrDefaultAsync<T>(string sQuery, object parameter = null, IDbTransaction transaction = null)
        {
            var command = new CommandDefinition(sQuery, parameter, transaction);
            var result = await Connection.QueryFirstOrDefaultAsync<T>(command);
            return result;
        }

        protected async Task<int> ExecuteAsync(string sQuery, object parameter = null, IDbTransaction transaction = null)
        {
            var command = new CommandDefinition(sQuery, parameter, transaction);
            var result = await Connection.ExecuteAsync(command);
            return result;
        }

        protected async Task<GridReader> GetMultipleQueryAsync(string sQuery, object parameter = null, IDbTransaction transaction = null)
        {
            var command = new CommandDefinition(sQuery, parameter, transaction);
            var result = await Connection.QueryMultipleAsync(command);
            return result;
        }



    }
}
