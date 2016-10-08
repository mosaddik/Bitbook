using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using BitBookApp.Models;

namespace BitBookApp.BitBook.Core.DAL
{
    public class FriendRequestGetway
    {
        string connectionString = "Server=MOSADDIK-PC\\SQLEXPRESS;Database=BitBookDb;Integrated Security=true";


        public bool Add(Models.FriendRequest friendRequest)
        {
            bool isSave = false;
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            string qrey = "INSERT INTO friendRequest (user1,user2)values('" + friendRequest.User1.Id + "','" +
                          friendRequest.User2.Id + "')";
            SqlCommand command = new SqlCommand(qrey, connection);
            int rowsEffected = command.ExecuteNonQuery();
            if (rowsEffected > 0)
            {
                isSave = true;
            }
            connection.Close();
            return isSave;
        }

        public bool SetRelationShiptoUser(User user)
        {
            bool isSave = false;
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            string qrey = "UPDATE friendReqeust Set user1='" + user.Id + "',user2='" + user.Id + "' ";
            SqlCommand command = new SqlCommand(qrey, connection);
            int rowsEffected = command.ExecuteNonQuery();
            if (rowsEffected > 0)
            {
                isSave = true;
            }
            connection.Close();
            return isSave;
        }

        public bool Remove(int user1 ,  int user2)
        {
            bool isDeleted = false;
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            string qrey = "DELETE FROM friendRequest Where user1='" + user1 + "' AND user2='"+user2+"'";
            SqlCommand command = new SqlCommand(qrey, connection);
            int rowsEffected = command.ExecuteNonQuery();
            if (rowsEffected > 0)
            {
                isDeleted = true;
            }
            connection.Close();
            return isDeleted;
        }
        public bool FullRemove(int user1, int user2)
        {
            bool isDeleted = false;
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            string qrey = "DELETE FROM friendRequest Where user2='" + user1 + "' AND user1='" + user2 + "'";
            SqlCommand command = new SqlCommand(qrey, connection);
            int rowsEffected = command.ExecuteNonQuery();
            if (rowsEffected > 0)
            {
                isDeleted = true;
            }
            connection.Close();
            return isDeleted;
        }


        public List<FriendRequest> GetAllFriendReqeust(int id)
        {
            SqlConnection connection = new SqlConnection(connectionString);

            connection.Open();


            var friendRequestList = new List<FriendRequest>();


            string qrey = "SELECT * from dbo.friendRequest where user1='" + id + "'";
            SqlCommand command = new SqlCommand(qrey, connection);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    FriendRequest friendRequest = new FriendRequest();
                    friendRequest.Id = Convert.ToInt32(reader["id"]);
                    friendRequest.User1.Id = Convert.ToInt32(reader["user1"]);
                    friendRequest.User2.Id = Convert.ToInt32(reader["user2"]);
                    friendRequestList.Add(friendRequest);
                }
            }
            connection.Close();
            return friendRequestList;

        }


        public List<FriendRequest> GetAllFriendRequestsById(int reciverRequesterId)
        {
            SqlConnection connection = new SqlConnection(connectionString);

            connection.Open();


            var friendRequestList = new List<FriendRequest>();


            string qrey = "SELECT * from dbo.friendRequest where user2='" + reciverRequesterId + "'";
            SqlCommand command = new SqlCommand(qrey, connection);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    FriendRequest friendRequest = new FriendRequest();
                    friendRequest.Id = Convert.ToInt32(reader["id"]);
                    friendRequest.User1.Id = Convert.ToInt32(reader["user1"]);
                    friendRequest.User2.Id = Convert.ToInt32(reader["user2"]);
                    friendRequestList.Add(friendRequest);
                }
            }
            connection.Close();
            return friendRequestList;            
        }
    }
}