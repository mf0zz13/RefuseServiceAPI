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

        public DispatchRecordController(IConfiguration configuration)
        {
            var password = configuration["Database:Password"];
            var builder = new MySqlConnectionStringBuilder
            {
                Server = "mysqlmssa.mysql.database.azure.com",
                Database = "guntherrefuse",
                UserID = "applogin",
                Password = password,
                SslMode = MySqlSslMode.Required,
            };

            connection = new MySqlConnection(builder.ConnectionString);
        }

        [HttpGet]
        [Route("AllDispatchRecords")]
        public List<DispatchRecord> GetAllDispatchRecords()
        {
            try
            {
                connection.Open();
                return connection.Query<DispatchRecord>("SELECT * FROM DispatchRecords;").ToList();

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
        public List<DispatchRecord>? GetDispatchRecordsFromToday()
        {
            try
            {
                connection.Open();
                return connection.Query<DispatchRecord>($"SELECT * FROM DispatchRecords " +
                                                        $"WHERE DispatchDate = {DateTime.Today.Year}-{DateTime.Today.Month}-{DateTime.Today.Day}"
                                                        ).ToList();


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
