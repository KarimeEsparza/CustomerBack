using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using EntitiesLibrary;
using Newtonsoft.Json;
using Microsoft.WindowsAzure.Storage.Table;
using Microsoft.WindowsAzure.Storage;

namespace ExtractorProject
{
  public static class Extractor
  {
    [FunctionName("Extractor")]
    public static void Run([QueueTrigger("customer", Connection = "AzureWebJobsStorage")] string myQueueItem, ILogger log)
    {
      log.LogInformation($"C# Queue trigger function processed: {myQueueItem}");
      Customer customer = JsonConvert.DeserializeObject<Customer>(myQueueItem);
      CustomerEntity custEntity = new CustomerEntity("Customers", Guid.NewGuid().ToString())
      {
        Name = customer.Name,
        Address = customer.Address,
        Phone = customer.Phone,
        Email = customer.Email,
        Age = customer.Age
      };

      InsertRecord(custEntity, log);
    }

    private static void InsertRecord(CustomerEntity customerEntity, ILogger log)
    {
      string azureConnectionString = Environment.GetEnvironmentVariable("AzureWebJobsStorage");
      CloudTable table = AuthTable(azureConnectionString);
      if (table != null)
      {
        TableOperation insert = TableOperation.Insert(customerEntity);
        table.ExecuteAsync(insert);
      }
      else
        log.LogInformation("Table is null. Check if the table 'customer' exist in this context.");
    }

    private static CloudTable AuthTable(string connectionString)
    {
      try
      {
        CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connectionString);
        CloudTableClient client = storageAccount.CreateCloudTableClient();
        CloudTable table = client.GetTableReference("customer");

        return table;
      }
      catch
      {
        return null;
      }
    }
  }
}
