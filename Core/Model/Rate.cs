using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
namespace Core.Model
{
    public class Rate
    {

        public int Id { get; set; }
       
        [Display(Name = "Fat Type")]
        public MilkType MilkType { get; set; }
        [Display(Name = "Fat")]
        [Required]
        public float Fat { get; set; }
        [Display(Name = "Price")]
        [Required]
        public float Price { get; set; }
        public Rate()
        {

        } 
        public Rate( MilkType milkType, float fat, float price)
        { 
            MilkType = milkType;
            Fat = fat;
            Price = price;
        }
        
        public Rate(int id, string connectionString)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            string sql = "Select * from Rate where Id=@Id";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@Id", id);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                Load(reader);
            }
            connection.Close();

        }

        public Rate(SqlDataReader reader)
        {
            Load(reader);
        }

        public static List<Rate> GetRates(MilkType? milkType, string connectionString)
        {
            List<Rate> rates = new();
            SqlConnection connection = new SqlConnection(connectionString);
            string sql;
            if (milkType!=null)
                sql = "select * from Rate WHERE MilkType = @milkType";
            
            else
                sql = "select * from Rate";
            SqlCommand command = new SqlCommand(sql, connection);
            if(milkType!=null)
                command.Parameters.AddWithValue("@milkType", milkType);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            while(reader.Read())
            {
               rates.Add( new Rate(reader));
            }
            connection.Close();
            return rates;

        }
        public void Load(SqlDataReader reader)
        {
            Id = reader.GetInt32(0);
            MilkType = (MilkType)reader.GetInt32(1);
            Fat = (float)reader.GetDouble(2);
            Price = (float)reader.GetDouble(3);
        }

        public bool Save(string connectionString,bool isNew=false)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            string sql;
            if (Id==0 || isNew)
                sql = "Insert into Rate values(@MilkType,@Fat,@Price)";
            else
                sql = "Update Rate set MilkType=@MilkType,Fat=@Fat,Price=@Price where Id="+Id;
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@MilkType", (int)MilkType);
            command.Parameters.AddWithValue("@Fat", Fat);
            command.Parameters.AddWithValue("@Price", Price);
            try { 
                connection.Open();
                int result=command.ExecuteNonQuery();
                connection.Close();
                return result > 0;
            }
            finally
            {
                connection.Close();
            }
        }
        public static bool DeleteRate(int id, string connectionString)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            string sql = "delete from rate where Id=" + id;
            SqlCommand cmd = new(sql, connection);
            bool result = false;
            try
            {
                connection.Open();
                result = cmd.ExecuteNonQuery() > 0;
            }
            finally { connection.Close(); }
            return result;
        }



    }
    
}
