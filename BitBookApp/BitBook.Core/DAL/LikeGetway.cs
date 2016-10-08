using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using BitBookApp.Models;

namespace BitBookApp.BitBook.Core.DAL
{
    public class LikeGetway
    {
        string connectionString = "Server=MOSADDIK-PC\\SQLEXPRESS;Database=BitBookDb;Integrated Security=true";

        public bool LikeAPost(int userId, int postId)
        {
        

            bool isSave = false;
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            string qrey = "INSERT INTO [dbo].[like] (userId,postId)values('" + userId + "','" + postId + "')";
            SqlCommand command = new SqlCommand(qrey, connection);
            int rowsEffected = command.ExecuteNonQuery();
            if (rowsEffected > 0)
            {
                isSave = true;
            }
            connection.Close();
            return isSave;
        }

        public List<Like> GetLikesofAPost(int postId)
        {
            
            SqlConnection connection = new SqlConnection(connectionString);

            connection.Open();

            string qrey = "SELECT  * from [dbo].[like] where postId ='" + postId + "'";
            SqlCommand command = new SqlCommand(qrey, connection);
            SqlDataReader reader = command.ExecuteReader();
            List<Like> likelist = new List<Like>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {                      
                    Like like =  new Like();                    
                    like.UserId.Id = Convert.ToInt32(reader["userId"]);
                    likelist.Add(like);
                }

            }
            connection.Close();
            return likelist;
        }

        public bool GetLikeByPostIdAndUseId(int userId,int postId)
        {

            SqlConnection connection = new SqlConnection(connectionString);

            connection.Open();

            string qrey = "SELECT  * from [dbo].[like] where userId ='" + userId + "' AND postId='"+postId+"' ";
            SqlCommand command = new SqlCommand(qrey, connection);
            SqlDataReader reader = command.ExecuteReader();
            List<Like> likelist = new List<Like>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    return true;
                }

            }
            connection.Close();
            return false;
        }

        public bool RemoveMyLike(int userId, int postId)
        {

            bool isSave = false;
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            string qrey = "DELETE FROM [dbo].[like] WHERE userId='" + userId + "' AND postId='" + postId + "' ";
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
