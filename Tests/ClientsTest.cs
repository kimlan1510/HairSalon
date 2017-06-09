using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace HairSalon
{
  [Collection("HairSalon")]
  public class ClientsTest : IDisposable
  {
    public ClientsTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb; Initial Catalog=hair_salon_test; Integrated Security=SSPI;";
    }

    [Fact]
    public void Test_DatabaseEmptyAtFirst()
    {
     //Arrange, Act
     int result = Clients.GetAll().Count;

     //Assert
     Assert.Equal(0, result);
    }

    [Fact]
    public void Test_Save_SavesToDatabase()
    {
      //Arrange
      Clients testClients = new Clients("Susan", 1);

      //Act
      testClients.Save();
      List<Clients> result = Clients.GetAll();
      List<Clients> testList = new List<Clients>{testClients};

      //Assert
      Assert.Equal(testList, result);
    }

    [Fact]
    public void Test_Find_FindClientsInDatabase()
    {
      //Arrange
      Clients testClients = new Clients("Susan", 1);
      testClients.Save();

      //Act
      Clients foundClients = Clients.Find(testClients.GetId());

      //Assert
      Assert.Equal(testClients, foundClients);
    }

    [Fact]
    public void Test_Update_UpdatesClientInDatabase()
    {
      //Arrange
      string name = "Emily";
      Clients testClient = new Clients(name, 1);
      testClient.Save();
      string newName = "Susan";
      //Act
      testClient.Update("Susan");
      string result =testClient.GetName();

      //Assert
      Assert.Equal(newName, result);
    }


    public void Dispose()
    {
      Clients.DeleteAll();
      Stylists.DeleteAll();
    }

  }
}
