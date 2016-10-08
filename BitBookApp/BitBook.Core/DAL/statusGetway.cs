using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using BitBookApp.Models;

namespace BitBookApp.BitBook.Core.DAL
{
    public class StatusGetway
    {
        string connectionString = "Server=MOSADDIK-PC\\SQLEXPRESS;Database=BitBookDb;Integrated Security=true"; 
        bool isSave = false;
        public bool StatusPost(Post post,int currentUser)
        {
            try
            {
               
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                string qrey = "INSERT INTO post(userPost,picture,userId) values('" + post.UserPost + "' , '" +
                              post.PostPicture + "','" + currentUser + "')";

                SqlCommand command = new SqlCommand(qrey, connection);
                int rowsEffected = command.ExecuteNonQuery();
                if (rowsEffected > 0)
                {
                    isSave = true;
                }
                connection.Close();
                return isSave;
            }
            catch (Exception)
            {
                isSave = false;

            }

            return isSave;
        }

        public List<Post> GetStatusByUserId(int currentUser)
        {

            SqlConnection connection = new SqlConnection(connectionString);
            List<Post> postList =  new List<Post>();
            
            connection.Open();
            string qrey = "SELECT * from dbo.post where userId='" + currentUser + "' ";
            SqlCommand command = new SqlCommand(qrey, connection);
            SqlDataReader reader = command.ExecuteReader();
            var hobby = new Hobby();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    var post = new Post();
                    post =  new Post();
                    post.Id = Convert.ToInt32(reader["id"]);
                    post.PostPicture = Convert.ToString(reader["picture"]);
                    post.UserPost = Convert.ToString(reader["userPost"]);
                    post.User.Id = Convert.ToInt32(reader["userId"]);
                    postList.Add(post);
                }
            }
            connection.Close();
            return postList;
        }
       
        public bool RemovePost(int postId)
        {
            bool isSave = false;
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            string qrey = "DELETE FROM post Where id='" + postId + "' "; 
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