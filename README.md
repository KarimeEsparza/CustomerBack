# CustomerBack
1. The Queue and Table are called customer.
2. To test it locally, the variable in the local.settings.json file should be changed to the desired connection string:
  "Values": {
    //"AzureWebJobsStorage": "UseDevelopmentStorage=true",
    "AzureWebJobsStorage": "<Change this value>",
    "FUNCTIONS_WORKER_RUNTIME": "dotnet",
    "AzureConnectionString": ""
  }
