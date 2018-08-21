using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;

namespace HairSalon.Models
{
  public class Stylist
  {
    private int _stylistId;
    private string _stylistName;

    public Stylist(string StylistName, int StylistId = 0)
    {
      _stylistId = StylistId;
      _stylistName = StylistName;
    }
    public int GetStylistId()
    {
      return _stylistId;
    }
    public string GetStylistName()
    {
      return _stylistName;
    }

    public override bool Equals(System.Object otherStylist)
    {
      if (!(otherStylist is Stylist))
      {
        return false;
      }
      else
      {
        Stylist newStylist = (Stylist) otherStylist;
        bool idEquality = (this.GetStylistId() == newStylist.GetStylistId());
        bool nameEquality = (this.GetStylistName() == newStylist.GetStylistName());
        return (idEquality && nameEquality);
      }

    }
    public override int GetHashCode()
  {
    return this.GetStylistId().GetHashCode();
  }

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO `Stylists` (`name`) VALUES (@name);";

      MySqlParameter name = new MySqlParameter();
      name.ParameterName = "@name";
      name.Value = this._stylistName;
      cmd.Parameters.Add(name);

      cmd.ExecuteNonQuery();
      _stylistId = (int) cmd.LastInsertedId;    // This line is new!

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
    public static List<Stylist> GetAll()
    {
      // return _instances;
      List<Stylist> allStylists = new List<Stylist> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM stylists;";
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int stylistId = rdr.GetInt32(0);
        string stylistName = rdr.GetString(1);
        Stylist newStylist = new Stylist(stylistName, stylistId);
        allStylists.Add(newStylist);
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allStylists;
    }
    public static Stylist Find(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM stylists WHERE id = @thisId;";
      // more logic will go here!
      MySqlParameter thisId = new MySqlParameter();
      thisId.ParameterName = "@thisId";
      thisId.Value = id;
      cmd.Parameters.Add(thisId);
      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      int stylistId = 0;
      string stylistName = "";

      while (rdr.Read())
      {
        stylistId = rdr.GetInt32(0);
        stylistName = rdr.GetString(1);
      }
      Stylist foundStylist= new Stylist(stylistName, stylistId);

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return foundStylist;
    }
    public void Edit(string newName)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"UPDATE stylists SET name = @newName WHERE id = @searchId;";

      cmd.Parameters.Add(new MySqlParameter("@searchId", _stylistId));
      cmd.Parameters.Add(new MySqlParameter("@newName", newName));

      cmd.ExecuteNonQuery();
      _stylistName = newName;

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
    public static void DeleteAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM stylists; DELETE FROM stylists_specialties WHERE stylist_id = @thisId;";

      cmd.ExecuteNonQuery();

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
    public void Delete()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM stylists WHERE id = @thisId; DELETE FROM stylists_specialties WHERE stylist_id = @thisId;";

      cmd.Parameters.Add(new MySqlParameter("@thisID", _stylistId));

      cmd.ExecuteNonQuery();

      conn.Close();
      if(conn != null)
      {
        conn.Dispose();
      }
    }
    public void AddSpecialty(Specialty newSpecialty)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO stylists_specialties (stylist_id, specialty_id) VALUES (@StylistId, @SpecialtyId);";

      cmd.Parameters.Add(new MySqlParameter("@StylistId", _stylistId));
      cmd.Parameters.Add(new MySqlParameter("@SpecialtyId", newSpecialty.GetSpecialtyId()));

      cmd.ExecuteNonQuery();
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
    public List<Specialty> GetCategories()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT specialty_id FROM specialties_items WHERE item_id = @itemId;";

      MySqlParameter itemIdParameter = new MySqlParameter();
      itemIdParameter.ParameterName = "@itemId";
      itemIdParameter.Value = _stylistId;
      cmd.Parameters.Add(itemIdParameter);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;

      List<int> specialtyIds = new List<int> {};
      while(rdr.Read())
      {
        int specialtyId = rdr.GetInt32(0);
        specialtyIds.Add(specialtyId);
      }
      rdr.Dispose();

      List<Specialty> specialties = new List<Specialty> {};
      foreach (int specialtyId in specialtyIds)
      {
        var specialtyQuery = conn.CreateCommand() as MySqlCommand;
        specialtyQuery.CommandText = @"SELECT * FROM specialties WHERE id = @SpecialtyId;";

        MySqlParameter specialtyIdParameter = new MySqlParameter();
        specialtyIdParameter.ParameterName = "@SpecialtyId";
        specialtyIdParameter.Value = specialtyId;
        specialtyQuery.Parameters.Add(specialtyIdParameter);

        var specialtyQueryRdr = specialtyQuery.ExecuteReader() as MySqlDataReader;
        while(specialtyQueryRdr.Read())
        {
          int thisSpecialtyId = specialtyQueryRdr.GetInt32(0);
          string specialtyName = specialtyQueryRdr.GetString(1);
          Specialty foundSpecialty = new Specialty(specialtyName, thisSpecialtyId);
          specialties.Add(foundSpecialty);
        }
        specialtyQueryRdr.Dispose();
      }
      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return specialties;
    }
  }
}
