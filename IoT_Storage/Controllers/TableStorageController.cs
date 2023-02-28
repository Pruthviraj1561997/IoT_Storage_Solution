using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using IoT_Storage.Repository;
using IoT_Storage.Models;
using System;
using Azure.Data.Tables;

namespace IoT_Storage.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TableStorageController : ControllerBase
    {
        [HttpPost("AddTable")]
        public async Task<string> AddTable(string tableName)
        {
            await TableStorage.AddTable(tableName);
            return null;

        }

        [HttpPost("UpdateTable")]
        public async Task<Details> UpdateTable(Details student,string tableName)
        {
            await TableStorage.UpdateTable(student,tableName);
            return null;

        }


        [HttpGet("GetTableData")]
        public async Task<Details> GetTableData(string RowKey, string PartitionKey, string tableName)
        {
            var data=await TableStorage.GetTableData(RowKey, PartitionKey, tableName);
            return data;

        }

        [HttpGet("GetTable")]
        public async Task<TableClient> GetTable(string tableName)
        {
            var data = await TableStorage.GetTable(tableName);
            return data;

        }


        [HttpDelete("DeleteTableData")]
        public async Task DeleteTableData(string PartitionKey, string RowKey, string tableName)
        {
            await TableStorage.DeleteTableData(PartitionKey, RowKey, tableName);
            return;

        }

        [HttpDelete("DeleteTable")]
        public async Task DeleteTable(string tableName)
        {
            await TableStorage.DeleteTable(tableName);
            return;

        }
    }
}
