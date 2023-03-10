using Azure.Data.Tables;
using IoT_Storage.Models;


namespace IoT_Storage.Repository
{
    public class TableStorage
    {
        public static string connectionString = "DefaultEndpointsProtocol=https;AccountName=storageaccountpruthvi;AccountKey=mtE5UVIgVxARUPxKUQVsu6zNdSE4SAmc4JG1Wf9GBCHm426t6wEuENOYhjyVXkjTyK+wIoFkZdA6+AStE3g46A==;EndpointSuffix=core.windows.net";

        public static async Task AddTable(string tableName)
        {
            var data = new TableServiceClient(connectionString);
            var client = data.GetTableClient(tableName);
            await client.CreateIfNotExistsAsync();
        }

        public static async Task<Details> UpdateTable(Details student,string tableName)
        {
            var data = new TableServiceClient(connectionString);
            var client = data.GetTableClient(tableName);
            await client.UpsertEntityAsync(student);
            return student;
        }

        public static async Task<Details> GetTableData(string PartitionKey, string RowKey, string tableName)
        {
            var data = new TableServiceClient(connectionString);
            var client = data.GetTableClient(tableName);
            var tableData = await client.GetEntityAsync<Details>(PartitionKey, RowKey);
            return tableData;
        }

        public static async Task<TableClient> GetTable(string tableName)
        {
            var data = new TableServiceClient(connectionString);
            var client = data.GetTableClient(tableName);
            return client;
        }

        public static async Task DeleteTableData(string PartitionKey, string RowKey, string tableName)
        {
            var data = new TableServiceClient(connectionString);
            var client = data.GetTableClient(tableName);
            await client.DeleteEntityAsync(PartitionKey, RowKey);
            return;
        }

        public static async Task DeleteTable(string tableName)
        {
            var data = new TableServiceClient(connectionString);
            await data.DeleteTableAsync(tableName);
           
        }

    }
}
