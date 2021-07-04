using System;
using System.Data;
using System.Data.SqlClient;
using Credo.Domain.Interfaces.Services;
using Credo.Domain.Options;
using Dapper;
using Microsoft.Extensions.Configuration;

namespace Credo.Infrastructure.Services
{
    public class DapperService : IDapperService
    {
        private readonly IConfiguration _configuration;
        public DapperService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string GetLogsConnectionString()
        {
            return _configuration.GetConnectionString(nameof(DbConnectionSettings.CredoApiLogs));
        }

        public string GetCredoApiConnectionString()
        {
            return _configuration.GetConnectionString(nameof(DbConnectionSettings.CredoApi));
        }

        public IDbConnection InitializeConnection(string connectionString)
        {
            var conn = new SqlConnection(connectionString);
            DefaultTypeMap.MatchNamesWithUnderscores = true;
            SqlMapper.AddTypeMap(typeof(bool), DbType.Boolean); // There is some problem with dapper mapping boolean to tinyint/int. This one fixes tinyint 
            // In case of dapper connection will open automatically from the pool in case available 
            return conn;
        }
    }
}