using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileUpload
{
    class Program
    {
        static void Main(string[] args)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=XXXX;AccountKey=XXXX==;EndpointSuffix=core.windows.net");
            CloudBlobClient cloudBlobClient = storageAccount.CreateCloudBlobClient();

            // Create a container if does not exist
            CloudBlobContainer cloudBlobContainer = cloudBlobClient.GetContainerReference("hl7data");
            cloudBlobContainer.CreateIfNotExists();

            foreach (string d in Directory.GetDirectories("D:\\novanttest"))
            {
                foreach (string f in Directory.GetFiles(d))
                {
                    var segments = f.Split('_');

                    var cloudBlockBlob = cloudBlobContainer.GetBlockBlobReference(segments[1] + "/" + segments[2] + "/" + segments[3] + "/" + Guid.NewGuid().ToString("N") );
                    cloudBlockBlob.UploadFromFile(f);
                }
            }

        }
    }
}
