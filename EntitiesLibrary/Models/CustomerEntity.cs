using Microsoft.WindowsAzure.Storage.Table;

namespace EntitiesLibrary
{
  public class CustomerEntity : TableEntity
  {
    public CustomerEntity()
    {
    }

    public CustomerEntity(string partitionKey, string rowKey)
    {
      PartitionKey = partitionKey;
      RowKey = rowKey;
    }

    public string Name { get; set; }

    public string Address { get; set; }

    public string Phone { get; set; }

    public string Email { get; set; }

    public int Age { get; set; }
  }

  public class Customer
  {
    public string Name { get; set; }

    public string Address { get; set; }

    public string Phone { get; set; }

    public string Email { get; set; }

    public int Age { get; set; }
  }
}
