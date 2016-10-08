using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls.WebParts;
using BitBookApp.Models;

namespace BitBookApp.BitBook.Core.DAL
{
    public class ProfileGetWay
    {
        string connectionString = "Server=MOSADDIK-PC\\SQLEXPRESS;Database=BitBookDb;Integrated Security=true";
        public bool SaveProfile(Profile profile)
        {

            bool isSave = false;
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            string qrey = "INSERT INTO profiles (fristName,lastName,gender,dateOfbirth)values('" + profile.FristName + "','" + profile.LastName + "' ,'" + profile.Gender + "','" + profile.BirthDay + "')";
            SqlCommand command = new SqlCommand(qrey, connection);
            int rowsEffected = command.ExecuteNonQuery();
            if (rowsEffected > 0)
            {
                isSave = true;
            }
            connection.Close();
            return isSave;
        }



        public Profile GetRecentAddedData()
        {
            SqlConnection connection = new SqlConnection(connectionString);

            connection.Open();

            string qrey = "SELECT TOP(1) * from dbo.profiles order by id Desc";
            SqlCommand command = new SqlCommand(qrey, connection);
            SqlDataReader reader = command.ExecuteReader();
            var profile = new Profile();
            if (reader.HasRows)
            {
                while (reader.Read())
                {

                    profile.Id = Convert.ToInt32(reader["Id"]);
                    profile.FristName = Convert.ToString(reader["FristName"]);
                    profile.LastName = Convert.ToString(reader["LastName"]);
                    profile.Gender = Convert.ToString(reader["gender"]);
                    profile.Picture = Convert.ToString(reader["picture"]);
                    profile.BirthDay = Convert.ToDateTime(reader["dateOfbirth"]);
                }

            }
            connection.Close();
            return profile;


        }

        public Profile GetProfileById(int id)
        {
            SqlConnection connection = new SqlConnection(connectionString);

            connection.Open();

            string qrey = "SELECT * from dbo.profiles WHERE Id='"+id+"' ";
            SqlCommand command = new SqlCommand(qrey, connection);
            SqlDataReader reader = command.ExecuteReader();
            var profile = new Profile();
            if (reader.HasRows)
            {
                while (reader.Read())
                {

                    profile.Id = Convert.ToInt32(reader["Id"]);
                    profile.FristName = Convert.ToString(reader["FristName"]);
                    profile.LastName = Convert.ToString(reader["LastName"]);
                    profile.Gender = Convert.ToString(reader["gender"]);
                    profile.Picture = Convert.ToString(reader["picture"]);
                    profile.BirthDay = Convert.ToDateTime(reader["dateOfbirth"]);
                    profile.CoverPicture = Convert.ToString(reader["CoverPhoto"]);
                }

            }
            connection.Close();



            return profile;
        }

        public bool UpdateProfile(User currentUser, Profile profile)
        {
            bool isSave = false;
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            string qrey = "UPDATE  profiles SET fristName ='" + profile.FristName + "',lastName='" + profile.LastName + "'  WHERE id='" + currentUser.Profile.Id + "' ";
            SqlCommand command = new SqlCommand(qrey, connection);
            int rowsEffected = command.ExecuteNonQuery();
            if (rowsEffected > 0)
            {
                isSave = true;
            }
            connection.Close();
            return isSave;
        }

        public int GetProfileId(int id)
        {

            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            List<User> userList = new List<User>();
            string qrey = "SELECT * FROM users WHERE id='"+id+"'";
            SqlCommand command = new SqlCommand(qrey, connection);
            SqlDataReader reader = command.ExecuteReader();
            var user = new User();
            try
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {

                        user.Profile.Id = Convert.ToInt32(reader["profileId"]);
                    }
                }
            }
            catch (Exception e)
            {
                return 0;
            }
            connection.Close();
            return user.Profile.Id;



        } 
        
        public List<Profile> GetProfilesByName(string name)
        {
            var profilelist = new List<Profile>();
        
            string qrey = "SELECT * FROM dbo.profiles WHERE fristName like '"+name+"%'";

            SqlConnection connection =  new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command  = new SqlCommand(qrey,connection);
            SqlDataReader reader = command.ExecuteReader();

            
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    var profile = new Profile()
                    {
                    Id = Convert.ToInt32(reader["Id"]),
                    FristName = Convert.ToString(reader["FristName"]),
                    LastName = Convert.ToString(reader["LastName"]),
                    Gender = Convert.ToString(reader["gender"]),
                    Picture = Convert.ToString(reader["picture"]),
                    BirthDay = Convert.ToDateTime(reader["dateOfbirth"]),
                    CoverPicture = Convert.ToString(reader["CoverPhoto"])
                    };
                    profilelist.Add(profile);

                }
             
            }
            connection.Close();
            return profilelist;
        }


        public bool UpdateProfilePicture(Profile profile)
        {

            bool isSave = false;
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            string qrey = "UPDATE  profiles SET picture='"+profile.Picture+"'  WHERE id='" + profile.Id + "' ";
            SqlCommand command = new SqlCommand(qrey, connection);
            int rowsEffected = command.ExecuteNonQuery();
            if (rowsEffected > 0)
            {
                isSave = true;
            }
            connection.Close();
            return isSave;
        }

        public bool UpdateCoverPhoto(Profile profile)
        {
            bool isSave = false;
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            string qrey = "UPDATE  profiles SET CoverPhoto='"+profile.Picture+"' WHERE id='" + profile.Id + "' ";
            SqlCommand command = new SqlCommand(qrey, connection);
            int rowsEffected = command.ExecuteNonQuery();
            if (rowsEffected > 0)
            {
                isSave = true;
            }
            connection.Close();
            return isSave;



        } 
    }
}