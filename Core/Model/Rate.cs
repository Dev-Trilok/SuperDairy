using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
namespace Core.Model
{
    public class Rate
    {

        public int Id { get; set; }
        public MilkType MilkType { get; set; }
        public float Fat { get; set; }
        public float Price { get; set; }
        public Rate( MilkType milkType, float fat, float price)
        { 
            MilkType = milkType;
            Fat = fat;
            Price = price;
        }
        
        public Rate(int id,string connectionString)
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
        public void Load(SqlDataReader reader)
        {
            Id = reader.GetInt32(0);
            MilkType = (MilkType)reader.GetInt32(1);
            Fat = reader.GetFloat(2);
            Price = reader.GetFloat(3);
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
            return false;
        }



    }
    
}
