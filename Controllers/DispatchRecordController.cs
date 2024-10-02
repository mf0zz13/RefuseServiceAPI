using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using Dapper;
using DispatchRecordAPI.Models;

namespace DispatchRecordAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DispatchRecordController : ControllerBase
    {
        [HttpGet]
        //[Route("AllDispatchRecords")]
        public List<DispatchRecord> GetAllDispatchRecords()
        {
            var builder = new MySqlConnectionStringBuilder
            {
                Server = "mysqlmssa.mysql.database.azure.com",
                Database = "guntherrefuse",
                UserID = "applogin",
                Password = "T3stP@ssw0rd",
                SslMode = MySqlSslMode.Required,
            };

            var connection = new MySqlConnection(builder.ConnectionString);

            connection.Open();

            return connection.Query<DispatchRecord>("SELECT * FROM DispatchRecords;").ToList();
        }

        //[HttpGet]
        //[Route("AllDispatchRecordsFromToday")]
        //public List<DispatchRecord>? GetDispatchRecordsFromToday()
        //{
        //    return connection.Query<DispatchRecord>($"SELECT * FROM DispatchRecords WHERE DispatchDate = {DateTime.Today.Year}-{DateTime.Today.Month}-{DateTime.Today.Day}").ToList();
        //}
    }
}
