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

        public DispatchRecordController()
        {
            var builder = new MySqlConnectionStringBuilder
            {
                Server = "mysqlmssa.mysql.database.azure.com",
                Database = "guntherrefuse",
                UserID = "applogin",
                Password = "T3stiesP@ssw0rd",
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
                                                        $"WHERE DispatchDate = '{DateTime.Today.Year}-{DateTime.Today.Month}-{DateTime.Today.Day}'"
                                                        )).ToList<DispatchRecord>();
            }
            catch
            {
                return new List<DispatchRecord>();
            }
            finally
            {
                await connection.CloseAsync();
            }
        }

        [HttpGet]
        [Route("AllTrucks")]
        public async Task<List<Truck>> GetTrucksAsync()
        {
            try
            {
                await connection.OpenAsync();
                return (await connection.QueryAsync<Truck>("SELECT * FROM Trucks")).ToList();
            }
            catch
            {
                return new List<Truck>();
            }
            finally
            {
                await connection.CloseAsync();
            }
        }

        [HttpGet]
        [Route("AllEmployees")]
        public async Task<List<Employee>> GetEmployeesAsync()
        {
            try
            {
                await connection.OpenAsync();
                return (await connection.QueryAsync<Employee>("SELECT * FROM Employees")).ToList();
            }
            catch
            {
                return new List<Employee>();
            }
            finally
            {
                await connection.CloseAsync();
            }
        }

        [HttpPost]
        [Route("AddDispatchRecord")]
        public async Task AddDispatchRecord([FromBody] DispatchRecord record)
        {  try
            {
                await connection.OpenAsync();
                await connection.ExecuteAsync("INSERT INTO dispatchrecords (DispatchDate, ServiceArea, Route, TruckNumber, Driver, HelperOne, HelperTwo, RefuseType) " +
                    $"VALUES ('{record.DispatchDate.Year}-{record.DispatchDate.Month}-{record.DispatchDate.Day}', '{record.ServiceArea}', '{record.Route}', '{record.TruckNumber}', '{record.Driver}', '{record.HelperOne}', '{record.HelperTwo}', '{record.RefuseType}');");
            }
            catch
            {
                throw new NotImplementedException();
            }
            finally
            {
                await connection.CloseAsync();
            }
        }
    }
}