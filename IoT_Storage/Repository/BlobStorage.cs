using Azure.Storage;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;



namespace IoT_Storage.Repository
{
    public class BlobStorage
    {
       public static string connectionString = "DefaultEndpointsProtocol=https;AccountName=demostorage1211;AccountKey=gUWuFkJ0/bxQEEHVWfoOZpzFoHOCpeUZp04NzsHVrI4ivHdQUuzoChtBatoK1mbqtun4158Gqk9n+ASt3Wmrzw==;EndpointSuffix=core.windows.net";

        public static async Task CreateBlob(string blobName)
        {
            if(string.IsNullOrEmpty(blobName))
            {
                throw new ArgumentNullException("Enter Blob Name: ");
            }
            try
            {
                BlobContainerClient container = new BlobContainerClient(connectionString, blobName);
                await container.CreateAsync();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public static async Task<List<string>> GetBlob(string blobName,string file)
        {
            if (string.IsNullOrEmpty(blobName))
            {
                throw new ArgumentNullException("Enter Blob Name: ");
            }
            try
            {
                BlobContainerClient container = new BlobContainerClient(connectionString, blobName);
                List<string> name = new List<string>();
                await foreach(BlobItem b in container.GetBlobsAsync())
                {
                    name.Add(b.Name);
                }
                return name;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static async Task<BlobProperties> GetBlobContent(string blobName, string file)
        {
            if (string.IsNullOrEmpty(blobName))
            {
                throw new ArgumentNullException("Enter Blob Name: ");
            }
            try
            {
                BlobContainerClient container = new BlobContainerClient(connectionString, blobName);
                BlobClient blob = container.GetBlobClient(file);
                BlobProperties properties = await blob.GetPropertiesAsync();
                return properties;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //public static async Task<BlobProperties> UpdateBlobContent(string blobName, string file)
        //{
        //    if (string.IsNullOrEmpty(blobName))
        //    {
        //        throw new ArgumentNullException("Enter Blob Name: ");
        //    }
        //    try
        //    {
        //        string filename = Path.GetTempFileName();
        //        BlobContainerClient container = new BlobContainerClient(connectionString, blobName);
        //        BlobClient blob = container.GetBlobClient(file);
        //        await blob.UploadAsync(filename);
        //        BlobProperties properties = await blob.GetPropertiesAsync();
        //        return properties;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        public static async Task UpdateBlobContent(string blobName, string file)
        {
            if (string.IsNullOrEmpty(blobName))
            {
                throw new ArgumentNullException("Enter Blob Name: ");
            }
            try
            {
                
                BlobContainerClient container = new BlobContainerClient(connectionString, blobName);
                BlobClient blob = container.GetBlobClient(file);
                var path = @"C:\Users\vmadmin\Desktop\IOT_301Project\IoT_Storage_Solution\IoT_Storage_Solution\IoT_Storage\TestDemo\MedicalDocument.pdf";
               using FileStream uploadFile = File.OpenRead(path);
                await blob.UploadAsync(uploadFile,true);
                uploadFile.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static async Task DeleteBlob(string blobName)
        {
            if(string.IsNullOrEmpty(blobName))
            {
                throw new ArgumentNullException("Enter Blob Name: ");
            }
            try
            {
                BlobContainerClient container = new BlobContainerClient(connectionString, blobName);
                await container.DeleteAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static async Task DeleteBlobContent(string blobName,string filename)
        {
            if (string.IsNullOrEmpty(blobName))
            {
                throw new ArgumentNullException("Enter Blob Name: ");
            }
            try
            {
                BlobContainerClient container = new BlobContainerClient(connectionString, blobName);
                await container.DeleteBlobAsync(filename);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static async Task<BlobProperties> DownloadBlob(string blobName,string file)
        {
            try
            {
                string path = @"C:\Users\vmadmin\Desktop\IOT_301Project\IoT_Storage_Solution\IoT_Storage_Solution\IoT_Storage\Download\" + file;
                BlobContainerClient blobContainer = new BlobContainerClient(connectionString,blobName);
                BlobClient client = blobContainer.GetBlobClient(file);
                await client.DownloadToAsync(path);
                BlobProperties properties = await client.GetPropertiesAsync();
                return properties;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}
