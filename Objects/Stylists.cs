using System.Collections.Generic;
using System.Data.SqlClient;
using System;

namespace HairSalon
{
  public class Stylists
  {
    private int _id;
    private string _name;

    public Stylists(string name, int id = 0)
    {
      _name = name;
      _id = id;
    }

    public int GetId()
    {
      return _id;
    }
    public string GetName()
    {
      return _name;
    }

    public override bool Equals(System.Object otherStylists)
    {
      if(!(otherStylists is Stylists))
      {
        return false;
      }
      else
      {
        Stylists newStylists = (Stylists) otherStylists;
        bool idEquality = (this.GetId() == newStylists.GetId());
        bool nameEquality = (this.GetName() == newStylists.GetName());
        return (idEquality && nameEquality);
      }
    }

    public override int GetHashCode()
    {
      return this.GetName().GetHashCode();
    }

    public static List<Stylists> GetAll()
    {
      List<Stylists> AllStylists = new List<Stylists>{};

      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM stylists;", conn);
      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int stylistId = rdr.GetInt32(0);
        string stylistName = rdr.GetString(1);
        Stylists newStylists = new Stylists(stylistName, stylistId);
        AllStylists.Add(newStylists);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return AllStylists;
    }

    public void Save()
    {
     SqlConnection conn = DB.Connection();
     conn.Open();

     SqlCommand cmd = new SqlCommand("INSERT INTO stylists (name) OUTPUT INSERTED.id VALUES (@stylistsName);", conn);

     SqlParameter namePara = new SqlParameter("@stylistsName", this.GetName());

     cmd.Parameters.Add(namePara);
     SqlDataReader rdr = cmd.ExecuteReader();

     while(rdr.Read())
     {
       this._id = rdr.GetInt32(0);
     }
     if (rdr != null)
     {
       rdr.Close();
     }
     if (conn != null)
     {
       conn.Close();
     }
    }

    public static Stylists Find(int id)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM stylists WHERE id = @StylistsId;", conn);
      SqlParameter stylistsIdPara = new SqlParameter("@StylistsId", id.ToString());
      cmd.Parameters.Add(stylistsIdPara);
      SqlDataReader rdr = cmd.ExecuteReader();

      int foundStylistsId = 0;
      string foundStylistsName = null;

      while(rdr.Read())
      {
        foundStylistsId = rdr.GetInt32(0);
        foundStylistsName = rdr.GetString(1);

      }
      Stylists foundStylists = new Stylists(foundStylistsName, foundStylistsId);

      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return foundStylists;
    }

    // public List<Clients> GetClients()
    // {
    //  SqlConnection conn = DB.Connection();
    //  conn.Open();
    //
    //  SqlCommand cmd = new SqlCommand("SELECT * FROM clients WHERE stylist_id = @StylistId;", conn);
    //  SqlParameter stylistIdPara = new SqlParameter("@StylistId", this.GetId());
    //  cmd.Parameters.Add(stylistIdPara);
    //  SqlDataReader rdr = cmd.ExecuteReader();
    //
    //  List<Clients> AllClients = new List<Clients> {};
    //  while(rdr.Read())
    //  {
    //    int ClientId = rdr.GetInt32(0);
    //    string ClientName = rdr.GetString(1);
    //    int StylistId = rdr.GetInt32(2);
    //    Clients newClients = new Clients(ClientName, StylistId, ClientId);
    //    Clients.Add(newClients);
    //  }
    //  if (rdr != null)
    //  {
    //    rdr.Close();
    //  }
    //  if (conn != null)
    //  {
    //    conn.Close();
    //  }
    //  return AllClients;
    // }






    public static void DeleteAll()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();
      SqlCommand cmd = new SqlCommand("DELETE FROM stylists;", conn);
      cmd.ExecuteNonQuery();
      conn.Close();
    }
  }

}
