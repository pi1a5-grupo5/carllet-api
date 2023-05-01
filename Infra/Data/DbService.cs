using Dapper;
using Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Data;

namespace Infra.Data
{
    public class DbService : IDbService
    {
        private readonly IDbConnection _connection;

        public DbService(IConfiguration configuration)
        {
            _connection = new NpgsqlConnection(configuration.GetConnectionString("poc_v1"));
        }

        public async Task<T> GetAsync<T>(string command, object parameters)
        {
            T result = (await _connection.QueryAsync<T>(command, parameters).ConfigureAwait(false)).FirstOrDefault();

            return result;
        }

        public async Task<List<T>> GetAll<T>(string command, object parameters)
        {
            List<T> result = (await _connection.QueryAsync<T>(command, parameters)).ToList();

            return result;
        }

        public async Task<int> EditData(string command, object parameters)
        {
            int result;

            result = await _connection.ExecuteAsync(command, parameters);

            return result;
        }




    }
}
