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


        public Guid Id { get; set; } = Guid.Empty;
        public int UserId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public double Quantity { get; set; }
        public double TotalAmount { get; set; }
        public bool IsPaid { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime LastModified { get; set; }
        public int CreatedBy { get; set; }
        public int LastModifiedBy { get; set; }

        public Bill(Guid id, int userId, DateTime startDate, DateTime endDate, float quantity, double totalAmount, bool isPaid, DateTime createdOn, DateTime lastModified, int createdBy, int lastModifiedBy)
        {
            Id = id;
            UserId = userId;
            StartDate = startDate;
            EndDate = endDate;
            Quantity = quantity;
            TotalAmount = totalAmount;
            IsPaid = isPaid;
            CreatedOn = createdOn;
            LastModified = lastModified;
            CreatedBy = createdBy;
            LastModifiedBy = lastModifiedBy;
        }

        public Bill(int userId, DateTime startDate, DateTime endDate, float quantity, double totalAmount, bool isPaid, DateTime createdOn, DateTime lastModified, int createdBy, int lastModifiedBy)
        {
            UserId = userId;
            StartDate = startDate;
            EndDate = endDate;
            Quantity = quantity;
            TotalAmount = totalAmount;
            IsPaid = isPaid;
            CreatedOn = createdOn;
            LastModified = lastModified;
            CreatedBy = createdBy;
            LastModifiedBy = lastModifiedBy;
        }
        private void Load(SqlDataReader reader)
        {
            Id = reader.GetGuid(0);
            UserId = reader.GetInt32(1);
            StartDate = reader.GetDateTime(2);
            EndDate = reader.GetDateTime(3);
            Quantity = reader.GetDouble(4);
            TotalAmount = reader.GetDouble(5);
            IsPaid = reader.GetBoolean(6);
            CreatedOn = reader.GetDateTime(7);
            CreatedBy = reader.GetInt32(8);
            LastModified = reader.GetDateTime(9);
            LastModifiedBy = reader.GetInt32(10);
        }
        public Bill(SqlDataReader reader)
        {
            Load(reader);
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
                    Load(reader);
                }
            }
            finally
            {
                connection.Close();
            }
        }

        public Bill(int userId, DateTime startDate, DateTime endDate, string connectionString)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            string sql = "Select * from Bill where UserId=@UserId and Cast(StartDate as date)=Cast(@StartDate as Date) and Cast(EndDate as date)=Cast(@EndDate as Date)";
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@UserId", userId);
            command.Parameters.AddWithValue("@StartDate", startDate);
            command.Parameters.AddWithValue("@EndDate", endDate);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Load(reader);
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
                Id= Guid.NewGuid();
                sql = "Insert into Bill (Id,UserId, StartDate, EndDate, Quantity, TotalAmount, IsPaid, CreatedOn, LastModified, CreatedBy, LastModifiedBy) values (@Id, @UserId, @StartDate, @EndDate, @Quantity, @TotalAmount, @IsPaid, @CreatedOn, @LastModified, @CreatedBy, @LastModifiedBy)";
            }
            else
            {
                sql = "Update Bill set UserId = @UserId, StartDate = @StartDate, EndDate = @EndDate, Quantity = @Quantity, TotalAmount = @TotalAmount, IsPaid = @IsPaid, CreatedOn = @CreatedOn, LastModified = @LastModified, CreatedBy = @CreatedBy, LastModifiedBy = @LastModifiedBy where Id = @Id";
            }
            SqlCommand cmd = new SqlCommand(sql, con);

            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Parameters.AddWithValue("Id", Id);
            cmd.Parameters.AddWithValue("UserId", UserId);
            cmd.Parameters.AddWithValue("StartDate", StartDate);
            cmd.Parameters.AddWithValue("EndDate", EndDate);
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

        public static List<Bill> GetBills(int userId,String connectionString)
        {
            List<Bill> bills = new List<Bill>();
            var sql = "Select * from Bill where UserId=@UserId order by StartDate desc";
            SqlConnection connection=new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@UserId", userId);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    bills.Add( new Bill(reader));
                }
            }
            finally
            {
                connection.Close();
            }
            return bills;
        }

    }
}
