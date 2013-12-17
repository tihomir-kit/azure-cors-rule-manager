using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Shared.Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ACRM.Infrastructure
{
    public class AzureProvider
    {
        private string _accountName = null;
        private string _accountKey = null;

        public AzureProvider(string accountName, string accountKey)
        {
            _accountName = accountName;
            _accountKey = accountKey;
        }

        public static AzureProvider GetInstance(string accountName, string accountKey)
        {
            return new AzureProvider(accountName, accountKey);
        }

        public CloudStorageAccount GetCloudStorageAccount()
        {
            string storageConnectionString = String.Format("DefaultEndpointsProtocol=https;AccountName={0};AccountKey={1}", _accountName, _accountKey);

            CloudStorageAccount cloudStorageAccount = CloudStorageAccount.DevelopmentStorageAccount;
            CloudStorageAccount.TryParse(storageConnectionString, out cloudStorageAccount);

            return cloudStorageAccount;
        }

        public CloudBlobClient GetAzureClient()
        {
            var cloudStorageAccount = GetCloudStorageAccount();
            return cloudStorageAccount.CreateCloudBlobClient();
        }

        public bool AzureClientExists()
        {
            var cloudStorageAccount = GetCloudStorageAccount();
            return cloudStorageAccount != null;
        }

        public IList<CorsRule> GetCorsRules()
        {
            var azureClient = GetAzureClient();
            var serviceProperties = azureClient.GetServiceProperties();
            return serviceProperties.Cors.CorsRules;
        }

        public void CreateCorsRule(CorsRule corsRule)
        {
            var azureClient = GetAzureClient();
            var serviceProperties = azureClient.GetServiceProperties();
            serviceProperties.Cors.CorsRules.Add(corsRule);
            azureClient.SetServiceProperties(serviceProperties);
        }

        public void UpdateCorsRule(int id, CorsRule corsRule)
        {
            var azureClient = GetAzureClient();
            var serviceProperties = azureClient.GetServiceProperties();
            serviceProperties.Cors.CorsRules[id] = corsRule;
            azureClient.SetServiceProperties(serviceProperties);
        }

        public void RemoveCorsRule(int id)
        {
            var azureClient = GetAzureClient();
            var serviceProperties = azureClient.GetServiceProperties();
            var corsRuleToRemove = serviceProperties.Cors.CorsRules[id];
            serviceProperties.Cors.CorsRules.Remove(corsRuleToRemove);
            azureClient.SetServiceProperties(serviceProperties);
        }
    }
}