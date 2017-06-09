using System.Collections.Generic;
using System;
using System.Data;
using System.Data.SqlClient;

namespace HairSalon
{
  public class Clients
  {
    private int _id;
    private int _stylistId;
    private string _name;

    public Clients(string name, int stylistId, int id = 0)
    {
      _name = name;
      _stylistId = stylistId;
      _id = id;
    }

    public override bool Equals(System.Object otherClients)
    {
      if(!(otherClients is Clients))
      {
        return false;
      }
      else
      {
        Clients newClients = (Clients) otherClients;
        bool idEquality = (this.GetId() == newClients.GetId());
        bool nameEquality = (this.GetName() == newClients.GetName());
        bool stylist_idEquality = (this.GetStylistId() == newClients.GetStylistId());
        return (idEquality && nameEquality && stylist_idEquality);
      }
    }

    public override int GetHashCode()
    {
      return this.GetName().GetHashCode();
    }

    public int GetId()
    {
      return _id;
    }
    public int GetStylistId()
    {
      return _stylistId;
    }
    public string GetName()
    {
      return _name;
    }

    public static List<Clients> GetAll()
    {
      List<Clients> AllClients = new List<Clients>{};

      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM clients", conn);
      SqlDataReader rdr = cmd.ExecuteReader();

      while(rdr.Read())
      {
        int id = rdr.GetInt32(0);
        string name = rdr.GetString(1);
        int stylist_id = rdr.GetInt32(2);
        Clients newClients = new Clients(name, stylist_id, id);
        AllClients.Add(newClients);
      }
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return AllClients;
    }


    public static void DeleteAll()
    {
     SqlConnection conn = DB.Connection();
     conn.Open();
     SqlCommand cmd = new SqlCommand("DELETE FROM clients;", conn);
     cmd.ExecuteNonQuery();
     conn.Close();
    }
  }
}
