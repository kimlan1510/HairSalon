using Xunit;
using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace HairSalon
{
  [Collection("HairSalon")]
  public class StylistTest : IDisposable
  {
    public StylistTest()
    {
      DBConfiguration.ConnectionString = "Data Source=(localdb)\\mssqllocaldb; Initial Catalog=hair_salon_test; Integrated Security=SSPI;";
    }

    [Fact]
    public void Test_StylistEmptyAtFirst()
    {
      //Arrange, Act
      int result = Stylists.GetAll().Count;

      //Assert
      Assert.Equal(0, result);
    }

    [Fact]
    public void Test_Save_SaveStylistToDatabase()
    {
      //Arrange
      Stylists testStylists = new Stylists("Jacob");
      testStylists.Save();

      //Act
      List<Stylists> result = Stylists.GetAll();
      List<Stylists> testList = new List<Stylists>{testStylists};

      //Assert
      Assert.Equal(testList, result);
    }

    [Fact]
    public void Test_Find_FindsStylistsInDatabase()
    {
      //Arrange
      Stylists testStylists = new Stylists("Jacob");
      testStylists.Save();
      //Act
      Stylists foundStylists = Stylists.Find(testStylists.GetId());
      //Assert
      Assert.Equal(testStylists, foundStylists);
    }

    [Fact]
    public void Test_GetClients_RetrieveAllClientsWithinStylists()
    {
      //Arrange
      Stylists testStylists = new Stylists("Jacob");
      testStylists.Save();

      Clients firstClients = new Clients("Susan", testStylists.GetId());
      firstClients.Save();
      Clients secondClients = new Clients("Emma", testStylists.GetId());
      secondClients.Save();

      //Act
      List<Clients> testClientsList = new List<Clients>{firstClients, secondClients};
      List<Clients> resultClientsList = testStylists.GetClients();

      //Assert
      Assert.Equal(testClientsList, resultClientsList);
    }


    public void Dispose()
    {
      Stylists.DeleteAll();
    }
  }
}
