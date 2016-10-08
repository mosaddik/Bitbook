using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using BitBookApp.Models;

namespace BitBookApp.BitBook.Core.DAL
{
    public class CommentsGetway
    {



        string connectionString = "Server=MOSADDIK-PC\\SQLEXPRESS;Database=BitBookDb;Integrated Security=true";
        public bool SaveAComment(Comment comments)
        {
            // Update the demographics for a store, which is stored  
            // in an xml column.  
            string commandText = "INSERT INTO [comments](comment,userId,postId)VALUES(@comment,@user,@post)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(commandText, connection);
                command.Parameters.AddWithValue("@comment", comments.UserComment);
                

                command.Parameters.Add("@user", SqlDbType.Int);
                command.Parameters["@user"].Value = comments.User.Id;

                command.Parameters.Add("@post", SqlDbType.Int);
                command.Parameters["@post"].Value = comments.post.Id;


                // Use AddWithValue to assign Demographics. 

                // SQL Server will implicitly convert strings into XML

                try
                {
                    connection.Open();
                    Int32 rowsAffected = command.ExecuteNonQuery();
                    Console.WriteLine("RowsAffected: {0}", rowsAffected);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return true;
        }

        public List<Comment> GetCommentsOfAPost(int postId)
        {

            SqlConnection connection = new SqlConnection(connectionString);

            connection.Open();

            string qrey = "SELECT  * from [dbo].[comments] where postId ='" + postId + "'";
            SqlCommand command = new SqlCommand(qrey, connection);
            SqlDataReader reader = command.ExecuteReader();
            List<Comment> comments = new List<Comment>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Comment comment = new Comment();
                    comment.UserComment = Convert.ToString(reader["comment"]);
                    comment.User.Id = Convert.ToInt32(reader["userId"]);
                    comments.Add(comment);
                }

            }
            connection.Close();
            return comments;
        }
    }
}