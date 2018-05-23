using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace AuctionSite.Models
{
    public static class BlobStorageHelper
    {

        private static CloudBlobContainer GetCloudBlobContainer(string containerName)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("dtauctionsite_AzureStorageConnectionString"));
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference(containerName);
            if (container.CreateIfNotExists())
            {
                BlobContainerPermissions p = container.GetPermissions();
                p.PublicAccess = BlobContainerPublicAccessType.Blob;
                container.SetPermissions(p);
            }
            return container;
        }

        /// <summary>
        /// Uploads a stream of data as a block blob to the given container with the given blob name.
        /// </summary>
        /// <param name="containerName"></param>
        /// <param name="blobName"></param>
        /// <param name="data"></param>
        /// <returns>Returns the path to the uploaded blob.</returns>
        public static string UploadBlob(string containerName, string blobName, HttpPostedFileBase file)
        {
            CloudBlobContainer container = GetCloudBlobContainer(containerName);
            CloudBlockBlob blob = container.GetBlockBlobReference(blobName);
            using (var stream = file.InputStream)
            {
                blob.UploadFromStream(stream);
            }

            return $"{CloudConfigurationManager.GetSetting("AzureStorageEndpointURL")}{containerName}/{blobName}";
        }
    }
}