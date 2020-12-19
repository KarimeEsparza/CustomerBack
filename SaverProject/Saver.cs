using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;

namespace SaverProject
{
    public static class Saver
    {
    [FunctionName("Saver")]
    public static async Task<IActionResult> Run(
     [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req,
     ILogger log)
    {
      log.LogInformation("HTTP trigger function processed a request.");

      string message = GetDocumentContents(req);
      string azureConnectionString = Environment.GetEnvironmentVariable("AzureWebJobsStorage");

      try
      {
        CloudStorageAccount storageAccount = CloudStorageAccount.Parse(azureConnectionString);
        CloudQueueClient cloudQueueClient = storageAccount.CreateCloudQueueClient();
        CloudQueue cloudQueue = cloudQueueClient.GetQueueReference("customer");
        CloudQueueMessage queueMessage = new CloudQueueMessage(message);
        await cloudQueue.AddMessageAsync(queueMessage);
      }
      catch (Exception ex)
      {
        return new BadRequestObjectResult(string.Concat(ex.Message, string.Empty, ex.StackTrace));
      }

      return new OkObjectResult("Message successfully posted");
    }

    private static string GetDocumentContents(HttpRequest Request)
    {
      string documentContents;
      using (Stream receiveStream = Request.Body)
      {
        using StreamReader readStream = new StreamReader(receiveStream);
        documentContents = readStream.ReadToEnd();
      }
      return documentContents;
    }
  }
}
