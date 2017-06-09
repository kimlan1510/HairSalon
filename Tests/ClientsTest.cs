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


    public void Dispose()
    {
      Clients.DeleteAll();
      Stylists.DeleteAll();
    }

  }
}
