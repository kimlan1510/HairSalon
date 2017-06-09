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


    public void Dispose()
    {
      Stylists.DeleteAll();
    }
  }
}
