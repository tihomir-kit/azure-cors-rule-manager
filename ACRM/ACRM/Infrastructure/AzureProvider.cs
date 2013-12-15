using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ACRM.Infrastructure
{
    public class AzureProvider
    {
        public AzureProvider()
        {
        }

        public static AzureProvider GetInstance()
        {
            return new AzureProvider();
        }

        public CloudBlobClient GetAzureClient(string accountName, string accountKey)
        {
            var cloudStorageAccount = GetCloudStorageAccount(accountName, accountKey);
            return cloudStorageAccount.CreateCloudBlobClient();
        }

        public bool AzureClientExists(string accountName, string accountKey)
        {
            var cloudStorageAccount = GetCloudStorageAccount(accountName, accountKey);
            return cloudStorageAccount != null;
        }

        public CloudStorageAccount GetCloudStorageAccount(string accountName, string accountKey)
        {
            string storageConnectionString = String.Format("DefaultEndpointsProtocol=https;AccountName={0};AccountKey={1}", accountName, accountKey);

            CloudStorageAccount cloudStorageAccount = CloudStorageAccount.DevelopmentStorageAccount;
            CloudStorageAccount.TryParse(storageConnectionString, out cloudStorageAccount);

            return cloudStorageAccount;
        }
    }
}