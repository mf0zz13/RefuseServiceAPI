using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using Dapper;
using DispatchRecordAPI.Models;
using System.Collections.Generic;

namespace DispatchRecordAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DispatchRecordController : ControllerBase
    {
        private MySqlConnection connection;
        private IConfiguration config;
        private string password;

        public DispatchRecordController(IConfiguration configuration)
        {
            config = configuration;
            //password = config["Database:Password"];
            var password = configuration["Database:Password"];
            var builder = new MySqlConnectionStringBuilder
            {
                Server = "mysqlmssa.mysql.database.azure.com",
                Database = "guntherrefuse",
                UserID = "applogin",
                Password = "T3stP@ssw0rd",
                SslMode = MySqlSslMode.Required,
            };

            connection = new MySqlConnection(builder.ConnectionString);
        }

        [HttpGet]
        [Route("AllDispatchRecords")]
        public async Task<List<DispatchRecord>> GetAllDispatchRecordsAsync()
        {
            try
            {
               await connection.OpenAsync();
                return (await connection.QueryAsync<DispatchRecord>("SELECT * FROM DispatchRecords;")).ToList<DispatchRecord>();
            }
            catch
            {
                return new List<DispatchRecord>();
            }
            finally
            {
                connection.Close();
            }
        }

        [HttpGet]
        [Route("AllDispatchRecordsFromToday")]
        public async Task<List<DispatchRecord>> GetDispatchRecordsFromTodayAsync()
        {
            try
            {
                await connection.OpenAsync();
                return (await connection.QueryAsync<DispatchRecord>($"SELECT * FROM DispatchRecords " +
                                                        $"WHERE DispatchDate = {DateTime.Today.Year}-{DateTime.Today.Month}-{DateTime.Today.Day}"
                                                        )).ToList<DispatchRecord>();
            }
            catch
            {
                return new List<DispatchRecord>();
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
