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

    [Fact]
    public void Test_Update_UpdatesStylistsInDatabase()
    {
      //Arrange
      string name = "Nick";
      Stylists testStylists = new Stylists(name);
      testStylists.Save();
      string newName = "Jacob";

      //Act
      testStylists.Update("Jacob");
      string result = testStylists.GetName();

      //Assert
      Assert.Equal(newName, result);
    }

    [Fact]
    public void Test_Delete_DeleteStylistFromDatabase()
    {
      //Arrange
      string name1 = "Gary";
      Stylists testStylist1 = new Stylists(name1);
      testStylist1.Save();

      string name2 = "Wallace";
      Stylists testStylist2 = new Stylists(name2);
      testStylist2.Save();

      Clients client1 = new Clients("Susan", testStylist1.GetId());
      client1.Save();
      Clients client2 = new Clients("Emma", testStylist2.GetId());
      client2.Save();

      //Act
      testStylist1.Delete();
      List<Stylists> resultStylists = Stylists.GetAll();
      List<Stylists> testStylists = new List<Stylists> {testStylist2};

      List<Clients> resultClients = Clients.GetAll();
      List<Clients> clientsList = new List<Clients> {client2};

      //Assert
      Assert.Equal(testStylists, resultStylists);
      Assert.Equal(clientsList, resultClients);
    }


    public void Dispose()
    {
      Clients.DeleteAll();
      Stylists.DeleteAll();
    }
  }
}
