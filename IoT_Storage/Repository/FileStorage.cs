using Azure.Storage.Files.Shares;
using Azure.Storage.Files.Shares.Models;


namespace IoT_Storage.Repository
{
    public class FileStorage
    {
        public static string connectionString = "DefaultEndpointsProtocol=https;AccountName=pruthvirajstorage;AccountKey=xYT/zH84pix1VicRRSh1KT+I7K45aP1c/wxbmH3gIwzZ1qBcCmK6BaseVaRy2o5X4fXzHAE8qo6K+ASt7kAaYg==;EndpointSuffix=core.windows.net";

        public static ShareServiceClient serviceClient = null;

        public static async Task CreateFile(string filename)
        {
            try
            {
                serviceClient =  new ShareServiceClient(connectionString);
                var sharedService = serviceClient.GetShareClient(filename);
                await sharedService.CreateIfNotExistsAsync();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }


        public static async Task CreateDirectory(string directoryName,string filename)
        {
            try
            {
                serviceClient = new ShareServiceClient(connectionString);
                var sharedService = serviceClient.GetShareClient(filename);
                ShareDirectoryClient rootDirectory = sharedService.GetRootDirectoryClient();
                ShareDirectoryClient fileDirectory = rootDirectory.GetSubdirectoryClient(directoryName);
                await fileDirectory.CreateIfNotExistsAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public static async Task UploadFile(IFormFile file,string directoryName, string fileSharedName)
        {
            string fileName = file.FileName;
            serviceClient = new ShareServiceClient(connectionString);
            var sharedService = serviceClient.GetShareClient(fileSharedName);
            var directory = sharedService.GetDirectoryClient(directoryName);
            var fileStorage = directory.GetFileClient(fileName);
            await using(var data=file.OpenReadStream())
            {
                await fileStorage.CreateAsync(data.Length);
                await fileStorage.UploadAsync(data);
            }
        }

        public static async Task DeleteDirectory(string directoryName,string fileSharedName)
        {
            serviceClient = new ShareServiceClient(connectionString);
            var sharedService = serviceClient.GetShareClient(fileSharedName);
            var directory = sharedService.GetDirectoryClient(directoryName);
            await directory.DeleteAsync();
        }

        public static async Task DeleteFile(string directoryName, string fileSharedName,string fileName)
        {
            serviceClient = new ShareServiceClient(connectionString);
            var sharedService = serviceClient.GetShareClient(fileSharedName);
            var directory = sharedService.GetDirectoryClient(directoryName);
            var file = directory.GetFileClient(fileName);
            await file.DeleteAsync();
        }

        public static async Task DeleteFileShare(string fileName)
        {
            serviceClient = new ShareServiceClient(connectionString);
            var service= serviceClient.GetShareClient(fileName);
            await service.DeleteIfExistsAsync();
     
            
        }



        public static async Task<List<string>> GetAllFile(string directoryName,string fileSharedName)
        {
            serviceClient = new ShareServiceClient(connectionString);
             var sharedService = serviceClient.GetShareClient(fileSharedName);
            var file = sharedService.GetRootDirectoryClient();
            var Directory = sharedService.GetDirectoryClient(directoryName);
            List<string> name = new List<string>();
            await foreach(ShareFileItem item in Directory.GetFilesAndDirectoriesAsync())
            {
                name.Add(item.Name);
            }
            return name;
            

        }

        public static async Task DownloadFile(string directoryName,string fileShareName,string fileName)
        {
            string path = @"C:\Users\vmadmin\Desktop\IOT_301Project\IoT_Storage_Solution\IoT_Storage_Solution\IoT_Storage\Download\"+fileName;
            serviceClient = new ShareServiceClient(connectionString);
            var sharedService = serviceClient.GetShareClient(fileShareName);
            var directory = sharedService.GetDirectoryClient(directoryName);
            var file = directory.GetFileClient(fileName);
            ShareFileDownloadInfo dwld =  await file.DownloadAsync();
            using(FileStream stream= File.OpenWrite(path))
            {
                await dwld.Content.CopyToAsync(stream);
            }
        }
    }
}
