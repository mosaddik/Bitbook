using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using BitBookApp.Models;

namespace BitBookApp.BitBook.Core.DAL
{
    public class ParentGetway
    {
        string connectionString = "Server=MOSADDIK-PC\\SQLEXPRESS;Database=BitBookDb;Integrated Security=true";
        public bool SaveProfile(Parents parents)
        {

            bool isSave = false;
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            string qrey = "INSERT INTO parents (fatherName,motherName)values('" + parents.FatherName + "','" + parents.MotherName + "')";
            SqlCommand command = new SqlCommand(qrey, connection);
            int rowsEffected = command.ExecuteNonQuery();
            if (rowsEffected > 0)
            {
                isSave = true;
            }
            connection.Close();
            return isSave;
        }



        public Parents GetParentById(int id)
        {
        
            string qrey = "SELECT * FROM parents WHERE id = '"+id+"' ";
            Parents parents = null;
            SqlConnection connection =  new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command  = new SqlCommand(qrey,connection);
            SqlDataReader reader = command.ExecuteReader();

            
            if (reader.HasRows)
            {
                while (reader.Read())
                {

                    parents = new Parents()
                    {
                    Id = Convert.ToInt32(reader["Id"]),
                    FatherName = Convert.ToString(reader["fatherName"]),
                    MotherName = Convert.ToString(reader["motherName"]),
                    };
                }
             
            }
            connection.Close();
            return parents;
        }



        public bool UpdateParents(Parents parents)
        {
            bool isSave = false;
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            string qrey = "UPDATE  parents SET fatherName='" + parents.FatherName + "', motherName='" +
                          parents.MotherName + "' WHERE id='"+parents.Id+"'   ";
            
            
             
            SqlCommand command = new SqlCommand(qrey, connection);
            int rowsEffected = command.ExecuteNonQuery();
            if (rowsEffected > 0)
            {
                isSave = true;
            }
            connection.Close();
            return isSave;
        }

        public bool SetRelationShipToUser(User user)
        {
            bool isSave = false;
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            string qrey = "UPDATE  users SET parentId ='"+user.Parents.Id+"'WHERE id='"+user.Id+"' ";
            SqlCommand command = new SqlCommand(qrey, connection);
            int rowsEffected = command.ExecuteNonQuery();
            if (rowsEffected > 0)
            {
                isSave = true;
            }
            connection.Close();
            return isSave;
        }


        public int GetParentId(User user)
        {
            SqlConnection connection = new SqlConnection(connectionString);

            connection.Open();
            var parents = new Parents();
            parents.Id = 0;
            string qrey = "SELECT * from dbo.users WHERE id='"+user.Id+"' ";
            SqlCommand command = new SqlCommand(qrey, connection);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
             
                    try
                    {
                        parents.Id = Convert.ToInt32(reader["parentId"]);
                    }
                    catch (Exception)
                    {

                        parents.Id = 0;
                    }
                    
                }
                
                
            }
            connection.Close();
            return parents.Id;
        }

        public Parents GetRecentAddedData()
        {
            SqlConnection connection = new SqlConnection(connectionString);

            connection.Open();

            string qrey = "SELECT TOP(1) * from dbo.parents order by id Desc";
            SqlCommand command = new SqlCommand(qrey, connection);
            SqlDataReader reader = command.ExecuteReader();
            var parents = new Parents();
            if (reader.HasRows)
            {
                while (reader.Read())
                {

                    parents.Id = Convert.ToInt32(reader["Id"]);
                    parents.FatherName = Convert.ToString(reader["fatherName"]);
                    parents.MotherName = Convert.ToString(reader["motherName"]);
                }

            }
            connection.Close();
            return parents;
        }





        public Parents GetParentById(User user)
        {
            SqlConnection connection = new SqlConnection(connectionString);

            connection.Open();
            var parents = new Parents();
            string qrey = "SELECT * from dbo.parents WHERE id='" + user.Parents.Id + "' ";
            SqlCommand command = new SqlCommand(qrey, connection);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {

                    try
                    {
                        parents.Id = Convert.ToInt32(reader["id"]);
                        parents.MotherName = Convert.ToString(reader["fatherName"]);
                        parents.FatherName = Convert.ToString(reader["motherName"]);
                    }
                    catch (Exception)
                    {

                        parents.Id = 0;
                    }

                }


            }
            connection.Close();
            return parents;
        }



    }
}