using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using BitBookApp.Models;

namespace BitBookApp.BitBook.Core.DAL
{
    public class FriendsGetway
    {
        string connectionString = "Server=MOSADDIK-PC\\SQLEXPRESS;Database=BitBookDb;Integrated Security=true";
        public bool Add(Models.FriendRequest friends)
        {
            bool isSave = false;
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            string qrey = "INSERT INTO friends (friend1,friend2)values('" + friends.User1.Id + "','" + friends.User2.Id + "')";
            SqlCommand command = new SqlCommand(qrey, connection);
            int rowsEffected = command.ExecuteNonQuery();
            if (rowsEffected > 0)
            {
                isSave = true;
            }
            connection.Close();
            return isSave;
        }

        
        public bool Remove(Models.Friends friends)
        {
            bool isDeleted = false;
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            string qrey = "DELETE FROM friends Where friend1='" + friends.Friend1.Id + "' AND friend2='" + friends.Friend2.Id + "' ";
            SqlCommand command = new SqlCommand(qrey, connection);
            int rowsEffected = command.ExecuteNonQuery();
            if (rowsEffected > 0)
            {
                isDeleted = true;
            }
            connection.Close();
            return isDeleted;
        }

        public List<Friends> GetAllFriendList(int currentUser)
        {
            SqlConnection connection = new SqlConnection(connectionString);

            connection.Open();


            var friendlist =  new List<Friends>();


            string qrey = "SELECT * from dbo.friends where friend1='"+currentUser+"'";
            SqlCommand command = new SqlCommand(qrey, connection);
            SqlDataReader reader = command.ExecuteReader();
            var hobby = new Hobby();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Friends friends = new Friends();
                    friends.Id = Convert.ToInt32(reader["id"]);
                    friends.Friend1.Id = Convert.ToInt32(reader["friend1"]);
                    friends.Friend2.Id = Convert.ToInt32(reader["friend2"]);
                    friendlist.Add(friends);
                }
            }
            connection.Close();
            return friendlist;
        }
        public List<Friends> GetMoreFriend(int currentUser)
        {
            SqlConnection connection = new SqlConnection(connectionString);

            connection.Open();


            var friendlist = new List<Friends>();


            string qrey = "SELECT * from dbo.friends where friend2='" + currentUser + "'";
            SqlCommand command = new SqlCommand(qrey, connection);
            SqlDataReader reader = command.ExecuteReader();
            var hobby = new Hobby();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Friends friends = new Friends();
                    friends.Id = Convert.ToInt32(reader["id"]);
                    friends.Friend1.Id = Convert.ToInt32(reader["friend2"]);
                    friends.Friend2.Id = Convert.ToInt32(reader["friend1"]);
                    friendlist.Add(friends);
                }
            }
            connection.Close();
            return friendlist;
        }
        

    }
}