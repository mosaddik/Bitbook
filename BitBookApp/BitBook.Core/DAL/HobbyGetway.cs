using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using BitBookApp.Models;

namespace BitBookApp.BitBook.Core.DAL
{
    public class HobbyGetway
    {
        string connectionString = "Server=MOSADDIK-PC\\SQLEXPRESS;Database=BitBookDb;Integrated Security=true";
        public bool Save(Hobby hobby)
        {

            bool isSave = false;
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            string qrey = "INSERT INTO hobby(hobby)values('" + hobby.UserHobby + "') ";
            SqlCommand command = new SqlCommand(qrey, connection);
            int rowsEffected = command.ExecuteNonQuery();
            if (rowsEffected > 0)
            {
                isSave = true;
            }
            connection.Close();
            return isSave;
        }

        public Hobby GetRecentAddedData()
        {
            SqlConnection connection = new SqlConnection(connectionString);

            connection.Open();

            string qrey = "SELECT TOP(1) * from dbo.hobby order by id Desc";
            SqlCommand command = new SqlCommand(qrey, connection);
            SqlDataReader reader = command.ExecuteReader();
            var hobby = new Hobby();
            if (reader.HasRows)
            {
                while (reader.Read())
                {

                    hobby.Id = Convert.ToInt32(reader["id"]);
                    hobby.UserHobby = Convert.ToString(reader["hobby"]);
               
                }

            }
            connection.Close();
            return hobby;
        }

        public bool SetAOneToOneConnectionToUser(User user)
        {
            bool isSave = false;
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            string qrey = "UPDATE  users SET hobbyId ='" + user.Hobby.Id + "'WHERE id='" + user.Id + "' ";
            SqlCommand command = new SqlCommand(qrey, connection);
            int rowsEffected = command.ExecuteNonQuery();
            if (rowsEffected > 0)
            {
                isSave = true;
            }
            connection.Close();
            return isSave;
        }

        public bool Update(Hobby hobby)
        {
            bool isSave = false;
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            string qrey = "UPDATE  hobby SET hobby='" + hobby.UserHobby + "'  WHERE id='" +
                          hobby.Id + "'   ";


            SqlCommand command = new SqlCommand(qrey, connection);
            int rowsEffected = command.ExecuteNonQuery();
            if (rowsEffected > 0)
            {
                isSave = true;
            }
            connection.Close();
            return isSave;
        }

        public Hobby GetHobyByUser(User user)
        {
            SqlConnection connection = new SqlConnection(connectionString);

            connection.Open();
            var hobby = new Hobby();
            string qrey = "SELECT * from dbo.hobby WHERE id='" + user.Hobby.Id + "' ";
            SqlCommand command = new SqlCommand(qrey, connection);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    try
                    {

                        hobby.Id = Convert.ToInt32(reader["id"]);
                        hobby.UserHobby = Convert.ToString(reader["hobby"]);
                    }
                    catch (Exception)
                    {
                        return hobby;

                    }

                }



            }
            connection.Close();
            return hobby;
        }

        public User GetUserInfoById(User user)
        {
            SqlConnection connection = new SqlConnection(connectionString);

            connection.Open();
            var selectedUser = new User();
            string qrey = "SELECT * from dbo.users WHERE id='" + user.Id + "' ";
            SqlCommand command = new SqlCommand(qrey, connection);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {

                    try
                    {
                        selectedUser.Hobby.Id = Convert.ToInt32(reader["hobbyId"]);
                    }
                    catch (Exception)
                    {

                        selectedUser.Hobby.Id = 0;

                    }

                }


            }
            connection.Close();
            return selectedUser;


        }









    }
}