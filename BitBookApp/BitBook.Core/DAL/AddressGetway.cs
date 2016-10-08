using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using BitBookApp.Models;

namespace BitBookApp.BitBook.Core.DAL
{
    public class AddressGetway
    {

        string connectionString = "Server=MOSADDIK-PC\\SQLEXPRESS;Database=BitBookDb;Integrated Security=true";
           public bool SaveAddress(Address address)
        {

            bool isSave = false;
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            string qrey = "INSERT INTO address (city,hometown,country)values('" + address.City + "','" + address.Hometown + "','"+address.Country+"')";
            SqlCommand command = new SqlCommand(qrey, connection);
            int rowsEffected = command.ExecuteNonQuery();
            if (rowsEffected > 0)
            {
                isSave = true;
            }
            connection.Close();
            return isSave;
        }



        public Address GetAddressById(User currentUser, int id)
        {
        
            string qrey = "SELECT * FROM address WHERE id = '"+id+"' ";
            Address address = null;
            SqlConnection connection =  new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command  = new SqlCommand(qrey,connection);
            SqlDataReader reader = command.ExecuteReader();

            
            if (reader.HasRows)
            {
                while (reader.Read())
                {

                    address = new Address()
                    {
                    Id = Convert.ToInt32(reader["Id"]),
                    City = Convert.ToString(reader["city"]),
                    Hometown = Convert.ToString(reader["hometown"]),
                    Country = Convert.ToString(reader["hometown"]),
                    };
                }
             
            }
            connection.Close();
            return address;
        }



        public bool UpdateAddress(Address address)
        {
            bool isSave = false;
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            string qrey = "UPDATE  address SET city='" + address.City + "', homeTown='" +
                          address.Hometown + "',country='"+address.Country+"' WHERE id='"+address.Id+"'   ";
            
            
             
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
            string qrey = "UPDATE  users SET addressId ='"+user.Address.Id+"'WHERE id='"+user.Id+"' ";
            SqlCommand command = new SqlCommand(qrey, connection);
            int rowsEffected = command.ExecuteNonQuery();
            if (rowsEffected > 0)
            {
                isSave = true;
            }
            connection.Close();
            return isSave;
        }


        public int GetAddressId(User user)
        {
            SqlConnection connection = new SqlConnection(connectionString);

            connection.Open();
            var address = new Address();
            address.Id = 0;
            string qrey = "SELECT * from dbo.users WHERE id='"+user.Id+"' ";
            SqlCommand command = new SqlCommand(qrey, connection);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
             
                    try
                    {
                        address.Id = Convert.ToInt32(reader["addressId"]);
                    }
                    catch (Exception)
                    {

                        address.Id = 0;
                    }
                    
                }
                
                
            }
            connection.Close();
            return address.Id;
        }

        public Address GetRecentAddedData()
        {
            SqlConnection connection = new SqlConnection(connectionString);

            connection.Open();

            string qrey = "SELECT TOP(1) * from dbo.address order by id Desc";
            SqlCommand command = new SqlCommand(qrey, connection);
            SqlDataReader reader = command.ExecuteReader();
            var address = new Address();
            if (reader.HasRows)
            {
                while (reader.Read())
                {

                    address.Id = Convert.ToInt32(reader["id"]);
                    address.City = Convert.ToString(reader["city"]);
                    address.Hometown = Convert.ToString(reader["homeTown"]);
                    address.Country = Convert.ToString(reader["country"]);
                }

            }
            connection.Close();
            return address;
        }





        public Address GetAddressById(User user)
        {
            SqlConnection connection = new SqlConnection(connectionString);

            connection.Open();
            var address = new Address();
            string qrey = "SELECT * from dbo.address WHERE id='" + user.Address.Id + "' ";
            SqlCommand command = new SqlCommand(qrey, connection);
            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {

                    try
                    {
                        address.Id = Convert.ToInt32(reader["id"]);
                        address.City = Convert.ToString(reader["city"]);
                        address.Hometown = Convert.ToString(reader["homeTown"]);
                        address.Country = Convert.ToString(reader["country"]);
                    }
                    catch (Exception)
                    {

                        address.Id = 0;
                    }

                }


            }
            connection.Close();
            return address;
        }



    



    }
}