using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
namespace Core.Model
{
    public class MilkInventory
    {

        public Guid Id { get; set; }
        public int UserId{ get; set; }
        public DateTime Date{ get; set; }
        public TimeBatch Batch{ get; set; }
        public MilkType MilkType{ get; set; }
        public float Fat{ get; set; }
        public float Price { get; set; }
        public float Quantity{ get; set; }
        public float Amount{ get; set; }
        public string Comment{ get; set; }
        public int Status{ get; set; }
        public DateTime CreatedOn{ get; set; }
        public int CreatedBy{ get; set; }
        public DateTime UpdatedOn{ get; set; }
        public int UpdatedBy { get; set; }

        public MilkInventory(int userId, DateTime date, TimeBatch batch, MilkType milkType, float fat, float price, float quantity, float amount, string comment, int status, DateTime createdOn, int createdBy, DateTime updatedOn, int updatedBy)
        { 
            UserId = userId;
            Date = date;
            Batch = batch;
            MilkType = milkType;
            Fat = fat;
            Price = price;
            Quantity = quantity;
            Amount = amount;
            Comment = comment;
            Status = status;
            CreatedOn = createdOn;
            CreatedBy = createdBy;
            UpdatedOn = updatedOn;
            UpdatedBy = updatedBy;
        }

        public MilkInventory(Guid id,string connectionString)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            string sql = "Select * from MilkInventory where Id=@Id";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@Id", id);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
                Load(reader);
            connection.Close();
        }

        public void Load(SqlDataReader reader)
        {
            Id = reader.GetGuid(0);
            UserId = reader.GetInt32(1);
            Date = reader.GetDateTime(2);
            Batch = (TimeBatch)reader.GetInt32(3);
            MilkType = (MilkType)reader.GetInt32(4);
            Fat = reader.GetFloat(5);
            Price = reader.GetFloat(6);
            Quantity = reader.GetFloat(7);
            Amount = reader.GetFloat(8);
            Comment = reader.GetString(9);
            Status = reader.GetInt32(10);
            CreatedOn = reader.GetDateTime(11);
            CreatedBy = reader.GetInt32(12);
            UpdatedOn = reader.GetDateTime(13);
            UpdatedBy = reader.GetInt32(14);
        }
        
        public bool Save(string connectionString,bool isNew = false)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            string sql = "";
            if (isNew)
            {
                sql = "Insert into MilkInventory(UserId,Date,Batch,MilkType,Fat,Price,Quantity,Amount,Comment,Status,CreatedOn,CreatedBy,UpdatedOn,UpdatedBy) values(@UserId,@Date,@Batch,@MilkType,@Fat,@Price,@Quantity,@Amount,@Comment,@Status,@CreatedOn,@CreatedBy,@UpdatedOn,@UpdatedBy)";
            }
            else
            {
                sql = "Update MilkInventory set UserId=@UserId,Date=@Date,Batch=@Batch,MilkType=@MilkType,Fat=@Fat,Price=@Price,Quantity=@Quantity,Amount=@Amount,Comment=@Comment,Status=@Status,CreatedOn=@CreatedOn,CreatedBy=@CreatedBy,UpdatedOn=@UpdatedOn,UpdatedBy=@UpdatedBy where Id=@Id";
            }
            SqlCommand command = new SqlCommand(sql, connection);
            if (!isNew | Id != Guid.Empty)
                command.Parameters.AddWithValue("@Id", Id);
            command.Parameters.AddWithValue("@UserId", UserId);
            command.Parameters.AddWithValue("@Date", Date);
            command.Parameters.AddWithValue("@Batch", Batch);
            command.Parameters.AddWithValue("@MilkType", MilkType);
            command.Parameters.AddWithValue("@Fat", Fat);
            command.Parameters.AddWithValue("@Price", Price);
            command.Parameters.AddWithValue("@Quantity", Quantity);
            command.Parameters.AddWithValue("@Amount", Amount);
            command.Parameters.AddWithValue("@Comment", Comment);
            command.Parameters.AddWithValue("@Status", Status);
            command.Parameters.AddWithValue("@CreatedOn", CreatedOn);
            command.Parameters.AddWithValue("@CreatedBy", CreatedBy);
            command.Parameters.AddWithValue("@UpdatedOn", UpdatedOn);
            command.Parameters.AddWithValue("@UpdatedBy", UpdatedBy);
            connection.Open();
            int rowsAffected = command.ExecuteNonQuery();
            connection.Close();
            return rowsAffected > 0;
        }
    }
}
