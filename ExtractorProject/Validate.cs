﻿

using Microsoft.WindowsAzure.Storage;
using System;

namespace ExtractorProject
{
  class Validate
  {
    public static CloudStorageAccount CreateStorageAccountFromConnectionString(string storageConnectionString)
    {
      CloudStorageAccount storageAccount;
      try
      {
        storageAccount = CloudStorageAccount.Parse(storageConnectionString);
      }
      catch (FormatException)
      {
        Console.WriteLine("Invalid storage account information provided. Please confirm the AccountName and AccountKey are valid in the app.config file - then restart the application.");
        throw;
      }
      catch (ArgumentException)
      {
        Console.WriteLine("Invalid storage account information provided. Please confirm the AccountName and AccountKey are valid in the app.config file - then restart the sample.");
        Console.ReadLine();
        throw;
      }

      return storageAccount;
    }
  }
}
