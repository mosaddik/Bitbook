
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using BitBookApp.Models;

namespace BitBookApp.BitBook.Core.DAL
{
    public class EducationGetway
    {
        string connectionString = "Server=MOSADDIK-PC\\SQLEXPRESS;Database=BitBookDb;Integrated Security=true";
        public bool Save(Education education)
        {

            bool isSave = false;
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            string qrey = "INSERT INTO education (school,college,university)values('" + education.School + "','" +
                          education.College + "','" + education.University + "')";
            SqlCommand command = new SqlCommand(qrey, connection);
            int rowsEffected = command.ExecuteNonQuery();
            if (rowsEffected > 0)
            {
                isSave = true;
            }
            connection.Close();
            return isSave;
        }

        public Education GetRecentAddedData()
        {
            SqlConnection connection = new SqlConnection(connectionString);

            connection.Open();

            string qrey = "SELECT TOP(1) * from dbo.education order by id Desc";
            SqlCommand command = new SqlCommand(qrey, connection);
            SqlDataReader reader = command.ExecuteReader();
            var education = new Education();
            if (reader.HasRows)
            {
                while (reader.Read())
                {

                    education.Id = Convert.ToInt32(reader["id"]);
                    education.School = Convert.ToString(reader["school"]);
                    education.College = Convert.ToString(reader["college"]);
                    education.University = Convert.ToString(reader["university"]);
                }

            }
            connection.Close();
            return education;
        }

        public bool SetAOneToOneConnectionToUser(User user)
        {
            bool isSave = false;
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            string qrey = "UPDATE  users SET educationId ='" + user.Education.Id + "'WHERE id='" + user.Id + "' ";
            SqlCommand command = new SqlCommand(qrey, connection);
            int rowsEffected = command.ExecuteNonQuery();
            if (rowsEffected > 0)
            {
                isSave = true;
            }
            connection.Close();
            return isSave;
        }

        public bool Update(Education education)
        {
            bool isSave = false;
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            string qrey = "UPDATE  education SET school='" + education.School + "', college='" +
                          education.College + "',university='" + education.University + "' WHERE id='" +
                          education.Id + "'   ";


            SqlCommand command = new SqlCommand(qrey, connection);
            int rowsEffected = command.ExecuteNonQuery();
            if (rowsEffected > 0)
            {
                isSave = true;
            }
            connection.Close();
            return isSave;
        }

        public Education GetEducationByUser(User user)
        {
            SqlConnection connection = new SqlConnection(connectionString);

            connection.Open();
            var education = new Education();
            string qrey = "SELECT * from dbo.education WHERE id='" + user.Education.Id + "' ";
            SqlCommand command = new SqlCommand(qrey, connection);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {

                    try
                    {
                        education.Id = Convert.ToInt32(reader["id"]);
                        education.School = Convert.ToString(reader["school"]);
                        education.College = Convert.ToString(reader["college"]);
                        education.University = Convert.ToString(reader["university"]);
                    }
                    catch (Exception)
                    {
                        return education;

                    }

                }



            }
            connection.Close();
            return education;
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
                        selectedUser.Education.Id = Convert.ToInt32(reader["educationId"]);
                    }
                    catch (Exception)
                    {

                        selectedUser.Education.Id = 0;

                    }

                }


            }
            connection.Close();
            return selectedUser;


        }
    }
}



