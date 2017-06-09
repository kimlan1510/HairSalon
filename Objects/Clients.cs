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

    public void Save()
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("INSERT INTO clients (name, stylist_id) OUTPUT INSERTED.id VALUES (@name, @stylist_id);", conn);

      SqlParameter nameParam = new SqlParameter("@name", this.GetName());

      SqlParameter stylist_idParam = new SqlParameter("@stylist_id", this.GetStylistId());

      cmd.Parameters.Add(nameParam);
      cmd.Parameters.Add(stylist_idParam);

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

    public static Clients Find(int id)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("SELECT * FROM clients WHERE id = @id;", conn);
      SqlParameter idParameter = new SqlParameter("@id", id.ToString());

      cmd.Parameters.Add(idParameter);
      SqlDataReader rdr = cmd.ExecuteReader();

      int foundId = 0;
      string name = null;
      int stylist_id = 0;
      while(rdr.Read())
      {
        foundId = rdr.GetInt32(0);
        name = rdr.GetString(1);
        stylist_id = rdr.GetInt32(2);
      }
      Clients foundClients = new Clients(name, stylist_id, foundId);
      if (rdr != null)
      {
        rdr.Close();
      }
      if (conn != null)
      {
        conn.Close();
      }
      return foundClients;
    }

    public void Update(string newName)
    {
      SqlConnection conn = DB.Connection();
      conn.Open();

      SqlCommand cmd = new SqlCommand("UPDATE clients SET name = @newName WHERE id = @clientId;", conn);

      SqlParameter newNamePara = new SqlParameter("@newName", newName);
      SqlParameter clientIdPara = new SqlParameter("@clientId", this.GetId());

      cmd.Parameters.Add(newNamePara);
      cmd.Parameters.Add(clientIdPara);
      this._name = newName;
      cmd.ExecuteNonQuery();
      conn.Close();
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
