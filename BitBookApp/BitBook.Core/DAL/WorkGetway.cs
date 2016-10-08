using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using BitBookApp.Models;

namespace BitBookApp.BitBook.Core.DAL
{
    public class WorkGetway
    {
        string connectionString = "Server=MOSADDIK-PC\\SQLEXPRESS;Database=BitBookDb;Integrated Security=true";
        public bool SaveWork(Work work)
        {

            bool isSave = false;
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            string qrey = "INSERT INTO work (company,position,city,description)values('" + work.Company + "','" + work.Position + "','" + work.City + "','" + work.Description + "')";
            SqlCommand command = new SqlCommand(qrey, connection);
            int rowsEffected = command.ExecuteNonQuery();
            if (rowsEffected > 0)
            {
                isSave = true;
            }
            connection.Close();
            return isSave;
        }

        public Work GetRecentAddedData()
        {
            SqlConnection connection = new SqlConnection(connectionString);

            connection.Open();

            string qrey = "SELECT TOP(1) * from dbo.work order by id Desc";
            SqlCommand command = new SqlCommand(qrey, connection);
            SqlDataReader reader = command.ExecuteReader();
            var work = new Work();
            if (reader.HasRows)
            {
                while (reader.Read())
                {

                    work.Id = Convert.ToInt32(reader["id"]);
                    work.Company = Convert.ToString(reader["company"]);
                    work.Position = Convert.ToString(reader["position"]);
                    work.City = Convert.ToString(reader["city"]);
                    work.Description = Convert.ToString(reader["description"]);
                }

            }
            connection.Close();
            return work;
        }

        public bool SetAOneToOneConnectionToUser(User user)
        {
            bool isSave = false;
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            string qrey = "UPDATE  users SET workId ='" + user.Work.Id + "'WHERE id='" + user.Id + "' ";
            SqlCommand command = new SqlCommand(qrey, connection);
            int rowsEffected = command.ExecuteNonQuery();
            if (rowsEffected > 0)
            {
                isSave = true;
            }
            connection.Close();
            return isSave;
        }

        public bool UpdateWork(Work work)
        {
            bool isSave = false;
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            string qrey = "UPDATE  work SET company='" + work.Company + "', position='" +
                          work.Position + "',city='" + work.City + "',description='" + work.Description + "' WHERE id='" + work.Id + "'   ";


            SqlCommand command = new SqlCommand(qrey, connection);
            int rowsEffected = command.ExecuteNonQuery();
            if (rowsEffected > 0)
            {
                isSave = true;
            }
            connection.Close();
            return isSave;
        }
        public Work GetWorkByUser(User user)
        {
            SqlConnection connection = new SqlConnection(connectionString);

            connection.Open();
            var work = new Work();
            string qrey = "SELECT * from dbo.work WHERE id='" + user.Work.Id + "' ";
            SqlCommand command = new SqlCommand(qrey, connection);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {

                    try
                    {
                        work.Id = Convert.ToInt32(reader["id"]);
                        work.Company = Convert.ToString(reader["company"]);
                        work.Position = Convert.ToString(reader["position"]);
                        work.City = Convert.ToString(reader["city"]);
                        work.Description = Convert.ToString(reader["description"]);
                    }
                    catch (Exception)
                    {
                        return work;

                    }

                }


            }
            connection.Close();
            return work;

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
                        selectedUser.Work.Id = Convert.ToInt32(reader["workId"]);
                    }
                    catch (Exception)
                    {

                        selectedUser.Work.Id = 0;

                    }

                }


            }
            connection.Close();
            return selectedUser;


        }


    }
}