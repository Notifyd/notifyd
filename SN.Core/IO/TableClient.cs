﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using Microsoft.WindowsAzure;

namespace Notifyd.Core.IO
{
    public class TableClient
    {
        private CloudStorageAccount _StorageAccount;
        private CloudTableClient _TableClient;
        private CloudTable _Table;
        private string _TableName;

        public TableClient(string tableName)
        {
            _TableName = tableName;

            // Retrieve the storage account from the connection string.
            _StorageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));

            // Create the table client.
            _TableClient = _StorageAccount.CreateCloudTableClient();

            // Create the table if it doesn't exist.
            _Table = _TableClient.GetTableReference(_TableName);

            _Table.CreateIfNotExists();
        }

        public void InsertRow(Models.IEntity obj)
        {
            obj.ModifiedOn = System.DateTime.Now;

            TableOperation operation = TableOperation.Insert(obj);

            _Table.Execute(operation);
        }
    }
}


//    Public Function GetEntry(key As String) As ConfigEntryEntity

      
//        ' Create a retrieve operation that takes a customer entity.
//        Dim retrieveOperation As TableOperation = TableOperation.Retrieve(Of ConfigEntryEntity)("Cogz", "Key91343845")

//        ' Execute the retrieve operation.
//        Dim retrievedResult As TableResult = _Table.Execute(retrieveOperation)

//        ' Print the phone number of the result.
//        If retrievedResult.Result IsNot Nothing Then
//            Return DirectCast(retrievedResult.Result, ConfigEntryEntity)
//        Else
//            Console.WriteLine("The phone number could not be retrieved.")
//            Return Nothing
//        End If
//    End Function

//    Public Sub AddRow(key As String, value As String)


//        ' Create a new customer entity.
//        Dim entry1 As New ConfigEntryEntity("Cogz", key)

//        entry1.Value = value
//        entry1.Owner = Environment.UserName

//        ' Create the TableOperation that inserts the customer entity.
//        Dim insertOperation As TableOperation = TableOperation.Insert(entry1)

//        ' Execute the insert operation.
//        _Table.Execute(insertOperation)
//    End Sub

//    Public Function GetAllEntries() As IEnumerable(Of ConfigEntryEntity)
//        ' Construct the query operation for all customer entities where PartitionKey="Smith".
//        Dim query As TableQuery(Of ConfigEntryEntity) = New TableQuery(Of ConfigEntryEntity)().Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, "Cogz"))

//        ' Print the fields for each customer.
//        Return _Table.ExecuteQuery(query)
       
//    End Function
//End Class