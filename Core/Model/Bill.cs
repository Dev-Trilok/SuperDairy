using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace Core.Model
{
    public class Bill
    {


        public Guid Id { get; set; }
        public int UserId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public MilkType MilkType { get; set; }
        public float Quantity { get; set; }
        public double TotalAmount { get; set; }
        public bool IsPaid { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime LastModified { get; set; }
        public int CreatedBy { get; set; }
        public int LastModifiedBy { get; set; }

        public Bill(Guid id, int userId, DateTime startDate, DateTime endDate, MilkType milkType, float quantity, double totalAmount, bool isPaid, DateTime createdOn, DateTime lastModified, int createdBy, int lastModifiedBy)
        {
            Id = id;
            UserId = userId;
            StartDate = startDate;
            EndDate = endDate;
            MilkType = milkType;
            Quantity = quantity;
            TotalAmount = totalAmount;
            IsPaid = isPaid;
            CreatedOn = createdOn;
            LastModified = lastModified;
            CreatedBy = createdBy;
            LastModifiedBy = lastModifiedBy;
        }

        public Bill(Guid id,string connectionString)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            string sql = "Select * from Bill where Id=@Id";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@Id", id);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Id = reader.GetGuid(0);
                    UserId = reader.GetInt32(1);
                    StartDate = reader.GetDateTime(2);
                    EndDate = reader.GetDateTime(3);
                    MilkType = (MilkType)reader.GetInt32(4);
                    Quantity = reader.GetFloat(5);
                    TotalAmount = reader.GetDouble(6);
                    IsPaid = reader.GetBoolean(7);
                    CreatedOn = reader.GetDateTime(8);
                    CreatedBy = reader.GetInt32(9);
                    LastModified = reader.GetDateTime(10);
                    LastModifiedBy = reader.GetInt32(11);
                }
            }
            finally
            {
                connection.Close();
            }
        }

        public bool Save(string connectionString, bool isNew = false)
        {
            SqlConnection con = new SqlConnection(connectionString);
            string sql;
            if (Id.Equals(Guid.Empty) || isNew)
            {
                sql = "Insert into Bill (UserId, StartDate, EndDate, MilkType, Quantity, TotalAmount, IsPaid, CreatedOn, LastModified, CreatedBy, LastModifiedBy) values (@UserId, @StartDate, @EndDate, @MilkType, @Quantity, @TotalAmount, @IsPaid, @CreatedOn, @LastModified, @CreatedBy, @LastModifiedBy)";
            }
            else
            {
                sql = "Update Bill set UserId = @UserId, StartDate = @StartDate, EndDate = @EndDate, MilkType = @MilkType, Quantity = @Quantity, TotalAmount = @TotalAmount, IsPaid = @IsPaid, CreatedOn = @CreatedOn, LastModified = @LastModified, CreatedBy = @CreatedBy, LastModifiedBy = @LastModifiedBy where Id = @Id";
            }
            SqlCommand cmd = new SqlCommand(sql, con);

            cmd.CommandType = System.Data.CommandType.Text;
            if (!Id.Equals(Guid.Empty)) { 
                cmd.Parameters.AddWithValue("Id", Id);
            }
            cmd.Parameters.AddWithValue("Id", Id);
            cmd.Parameters.AddWithValue("UserId", UserId);
            cmd.Parameters.AddWithValue("StartDate", StartDate);
            cmd.Parameters.AddWithValue("EndDate", EndDate);
            cmd.Parameters.AddWithValue("MilkType", MilkType);
            cmd.Parameters.AddWithValue("Quantity", Quantity);
            cmd.Parameters.AddWithValue("TotalAmount", TotalAmount);
            cmd.Parameters.AddWithValue("IsPaid", IsPaid);
            cmd.Parameters.AddWithValue("CreatedOn", CreatedOn);
            cmd.Parameters.AddWithValue("LastModified", LastModified);
            cmd.Parameters.AddWithValue("CreatedBy", CreatedBy);
            cmd.Parameters.AddWithValue("LastModifiedBy", LastModifiedBy);
            bool result = false;
            try
            {
                con.Open();
                result =cmd.ExecuteNonQuery() == 1;
            }
            finally
            {
                con.Close();
            }
            return result;
            
        }

    }
}
