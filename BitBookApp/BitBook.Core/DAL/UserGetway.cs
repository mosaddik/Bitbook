using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using BitBookApp.Models;


namespace BitBookApp.BitBook.Core.DAL
{
    public class UserGetway
    {
        string connectionString = "Server=MOSADDIK-PC\\SQLEXPRESS;Database=BitBookDb;Integrated Security=true";        

        public bool UpdatePassword(User user)
        {
       
            bool isSave = false;
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            string qrey = "UPDATE  users SET password ='" + user.Password + "'WHERE id='" + user.Id + "' ";
            SqlCommand command = new SqlCommand(qrey, connection);
            int rowsEffected = command.ExecuteNonQuery();
            if (rowsEffected > 0)
            {
                isSave = true;
            }
            connection.Close();
            return isSave;
        }

            
        public bool SaveUser(Models.User user)
        {
            bool isSave = false;
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            string qrey = "INSERT INTO users (email,password,profileId)values('" + user.Email + "','" + user.Password +
                          "','" + user.Profile.Id + "' )";
            SqlCommand command = new SqlCommand(qrey, connection);
            int rowsEffected = command.ExecuteNonQuery();
            if (rowsEffected > 0)
            {
                isSave = true;
            }
            connection.Close();
            return isSave;

        }

        public List<User> GetUsers()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            List<User> userList = new List<User>();
            string qrey = "SELECT * FROM users";
            SqlCommand command = new SqlCommand(qrey, connection);
            SqlDataReader reader = command.ExecuteReader();

            try
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var user = new User()
                        {
                            Id = Convert.ToInt32(reader["id"]),
                            Email = Convert.ToString(reader["email"]),
                            Password = Convert.ToString(reader["password"]),
                        };
                        user.Profile.Id = Convert.ToInt32(reader["profileId"]);

                        userList.Add(user);
                    }
                }
            }
            catch (Exception e)
            {
                return userList = null;
            }
            connection.Close();
            return userList;
        }

        public User GetListofUserByProfileId(int profileId)
        {

            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            List<User> userList = new List<User>();
            string qrey = "SELECT * FROM users where profileId='"+profileId+"'";
            SqlCommand command = new SqlCommand(qrey, connection);
            SqlDataReader reader = command.ExecuteReader();
            User user = new User();
         
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                         user = new User()
                        {
                            Id = Convert.ToInt32(reader["id"]),
                        };
                    }
                }

                 connection.Close();
                 return  user;
                }
           
          
        }
     


        
    }
