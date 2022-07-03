using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Core.Model
{
    class Address
    {

        public Guid Id { get; set; }
        public string LandMark { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
        public Address(string landMark, string street, string city, string state, string country, string zipCode)
        {
            Id = Guid.Empty;
            LandMark = landMark;
            Street = street;
            City = city;
            State = state;
            Country = country;
            ZipCode = zipCode;
        }

        public Address(Guid id,string connectionString)
        {
            Id = id;
            SqlConnection connection = new SqlConnection(connectionString);
            string sql = "SELECT * FROM Address WHERE Id = @Id";
            SqlCommand cmd = new SqlCommand(sql, connection);
            cmd.Parameters.AddWithValue("@Id", Id);
            try
            {
                connection.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                    Load(reader);
                else
                    throw new Exception("Address not found");

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


        public bool Save(string connectioString,bool isNew=false)
        {
            SqlConnection connection = new SqlConnection(connectioString);
            string sql="";
            if(Id == Guid.Empty || isNew)
            {
                sql = "INSERT INTO Address (LandMark, Street, City, State, Country, ZipCode) VALUES (@LandMark, @Street, @City, @State, @Country, @ZipCode)";
            }
            else
            {
                sql = "UPDATE Address SET LandMark = @LandMark, Street = @Street, City = @City, State = @State, Country = @Country, ZipCode = @ZipCode WHERE Id = @Id";
            }
            SqlCommand cmd = new SqlCommand(sql, connection);
            cmd.Parameters.AddWithValue("@LandMark", LandMark);
            cmd.Parameters.AddWithValue("@Street", Street);
            cmd.Parameters.AddWithValue("@City", City);
            cmd.Parameters.AddWithValue("@State", State);
            cmd.Parameters.AddWithValue("@Country", Country);
            cmd.Parameters.AddWithValue("@ZipCode", ZipCode);
            if (Id == Guid.Empty)
            {
                cmd.Parameters.AddWithValue("@Id", Id);
            }
            int result = 0;
            try
            {
                connection.Open();
                result = cmd.ExecuteNonQuery();
            }
            finally
            {
                connection.Close();
            }
            return result == 1 ;
        }

        private void Load(SqlDataReader reader)
        {
            if (reader.Read())
            {
                Id = reader.GetGuid(0);
                LandMark = reader.GetString(1);
                Street = reader.GetString(2);
                City = reader.GetString(3);
                State = reader.GetString(4);
                Country = reader.GetString(5);
                ZipCode = reader.GetString(6);
            }
        }
    }
}
