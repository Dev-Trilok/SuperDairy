using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Core.Model
{
    public class DairyInfo
    {
       
        public Guid Id { get; set; }
        
        [Required]
        [Display(Name = "Dairy Name")]
        public string Name { get; set; }

        [Display(Name = "Address")]
        public string Address { get; set; }
        [Display(Name = "Phone No")]
        public string PhoneNo { get; set; }
        
        [Display(Name = "Email")]
        public string Email { get; set; }
        
        [Display(Name = "Last Modified")]
        public DateTime LastModified { get; set; }
        [Display(Name = "Owner Name")]
        public string OwnerName { get; set; }

        public DairyInfo()
        {

        }
        public DairyInfo(Guid id, string name, string address, string phoneNo, string email, DateTime lastMod , string ownerName)
        {
            Id = id;
            Name = name;
            Address = address;
            PhoneNo = phoneNo;
            Email = email;
            LastModified= lastMod;
            OwnerName = ownerName;
        }
        

        public DairyInfo(Guid id, string connectionString)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            string sql = "Select * from DairyInfo where Id=@Id";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@Id", id);
            connection.Open();
            SqlDataReader reader = (SqlDataReader)command.ExecuteScalar();
            if (reader.Read())
            {
                Load(reader);
            }
            connection.Close();
        }

        public DairyInfo(SqlDataReader reader)
        {
            Load(reader);
        }

        public DairyInfo(string name, string connectionString)
        {
            DairyInfo dairyInfo;
            SqlConnection connection = new SqlConnection(connectionString);
            string sql;
            sql = "select * from dairyinfo WHERE name = @name";
            SqlCommand command = new SqlCommand(sql, connection);
           command.Parameters.AddWithValue("@name", name);
            try
            {
                connection.Open();
                var reader = command.ExecuteReader();
                if (reader.Read())
                    Load(reader);
                else
                    throw new Exception("Dairy not found");

            }
            catch (SqlException)
            {
                throw;
            }
            finally
            {
                connection.Close();
            }

        }
        public void Load(SqlDataReader reader)
        {
            Id = reader.GetGuid(0);
            Name = reader.GetString(1);
            Address = reader.GetString(2);
            PhoneNo = reader.GetString(3);
            Email = reader.GetString(4);
            LastModified = reader.GetDateTime(5);
            OwnerName = reader.GetString(6);
        }
       

        public bool Save(string connectionString)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            string sql;
            sql = "Update DairyInfo set name=@Name,Address=@Address,PhoneNo=@PhoneNo,Email=@Email, LastModified=getDate(), OwnerName= @OwnerName where Id=" + Id;
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@Name", Name);
            command.Parameters.AddWithValue("@Address", Address);
            command.Parameters.AddWithValue("@PhoneNo", PhoneNo);
            command.Parameters.AddWithValue("@Email", Email);
            command.Parameters.AddWithValue("@OwnerName", OwnerName);
            try
            {
                connection.Open();
                int result = command.ExecuteNonQuery();
                connection.Close();
                return result > 0;
            }
            finally
            {
                connection.Close();
            }
        }
       



    }
}
