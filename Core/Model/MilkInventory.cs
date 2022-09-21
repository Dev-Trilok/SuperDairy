using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using System.ComponentModel.DataAnnotations;
namespace Core.Model
{
    public class MilkInventory
    {
        #region Fields

        public Guid Id { get; set; } = Guid.Empty;
        [Display(Name ="User")]
        public int UserId { get; set; }
        public DateTime Date { get; set; }
        public TimeBatch Batch { get; set; }
        public MilkType MilkType { get; set; }
        public float Fat { get; set; }
        public float Price { get; set; }
        public float Quantity { get; set; }
        public float Amount { get; set; }
        
        public string Comment { get; set; }
        public MilkCollectStatus Status { get; set; } =MilkCollectStatus.COLLECTED;
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public int CreatedBy { get; set; }
        public DateTime UpdatedOn { get; set; } = DateTime.Now;
        public int UpdatedBy { get; set; }
        #endregion

        public MilkInventory(int userId, DateTime date, TimeBatch batch, MilkType milkType, float fat, float price, float quantity, float amount, string comment, MilkCollectStatus status, DateTime createdOn, int createdBy, DateTime updatedOn, int updatedBy)
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
        
        public MilkInventory(SqlDataReader reader)
        {
            Load(reader);
        }
        public MilkInventory(Guid id, string connectionString)
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
            Fat = (float)reader.GetDouble(5);
            Price = (float)reader.GetDouble(6);
            Quantity = (float)reader.GetDouble(7);
            Amount = (float)reader.GetDouble(8);
            Comment = reader.GetString(9);
            Status = (MilkCollectStatus)reader.GetInt32(10);
            CreatedOn = reader.GetDateTime(11);
            CreatedBy = reader.GetInt32(12);
            UpdatedOn = reader.GetDateTime(13);
            UpdatedBy = reader.GetInt32(14);
        }
        public static double GetTodayMilkCount(string Conn, int MilkType = -1)
        {
            SqlConnection con = new SqlConnection(Conn);
            con.Open();
            string sql;
            if (MilkType != -1)
                sql = "select sum (quantity) from MilkInventory where CAST(date AS DATE) = CAST(GETDATE() AS DATE) and MilkType=" + MilkType;
            else
                sql = "select sum (quantity) from MilkInventory where CAST(date AS DATE) = CAST(GETDATE() AS DATE) ";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.CommandType = System.Data.CommandType.Text;
            double milkCount = 0;
            try
            {
                milkCount = (double)cmd.ExecuteScalar();
            }
            catch
            {
                return 0;
            }
            finally
            {
                con.Close();
            }
            return milkCount;

        }
        public static double GetTodayAvgFat(string Conn, int MilkType = -1)
        {
            SqlConnection con = new SqlConnection(Conn);
            con.Open();
            string sql;
            if (MilkType != -1)
                sql = "select avg(fat) from MilkInventory where CAST(date AS DATE) = CAST(GETDATE() AS DATE) and MilkType=" + MilkType;
            else
                sql = "select avg(fat) from MilkInventory where CAST(date AS DATE) = CAST(GETDATE() AS DATE) ";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.CommandType = System.Data.CommandType.Text;
            double avgFat = 0;
            try
            {
                avgFat = (double)cmd.ExecuteScalar();
            }
            catch
            {
                return 0;
            }
            finally
            {
                con.Close();
            }
            return avgFat;

        }
        public bool Save(string connectionString, bool isNew = false)
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
            if (!isNew || Id != Guid.Empty)
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

        public static List<MilkInventory> GetInventoryList(int userId,DateTime date,string connectionString)
        {
            List<MilkInventory> inventories = new List<MilkInventory>();
            SqlConnection connection = new SqlConnection(connectionString);
            string sql = "select * from MilkInventory where Cast(Date as date) = Cast(@Date as date) and UserId=@UserId";
            SqlCommand cmd = new SqlCommand(sql, connection);
            cmd.Parameters.AddWithValue("@Date", date);
            cmd.Parameters.AddWithValue("@UserId", userId);
            try
            {
                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    inventories.Add(new MilkInventory(reader));
                }
            }
            catch (SqlException e){
                Console.WriteLine(e.Errors);
            }
            finally
            {
                connection.Close();
            }
            return inventories;
        }
        public static List<MilkInventory> GetInventoryList(int userId,DateTime startDate,DateTime endDate,string connectionString)
        {
            List<MilkInventory> inventories=new List<MilkInventory>();
            SqlConnection conn = new SqlConnection(connectionString);
            string sql = "select * from MilkInventory where UserId=@UserId and Cast(Date as date)>=Cast(@StartDate as Date) and Cast(Date as date)<Cast(@EndDate as Date) order by Date";
            SqlCommand command = new SqlCommand(sql, conn);
            command.Parameters.AddWithValue("@StartDate", startDate);
            command.Parameters.AddWithValue("@EndDate",endDate);
            command.Parameters.AddWithValue("@UserId",userId);
            try
            {
                conn.Open();
                SqlDataReader reader=command.ExecuteReader();
                while (reader.Read())
                    inventories.Add(new MilkInventory(reader));
            }
            catch
            {
            }
            finally
            {
                conn.Close();
            }


            return inventories;
        }

    }
}
