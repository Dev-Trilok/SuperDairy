using Microsoft.Data.SqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Text;

namespace Core.Model
{
    public class User
    {
        #region Fields
        public int Id { get; set; }

        [Display(Name = "Full Name")]
        [Required]

        public string Name { get; set; }
        [Required]
        [Display(Name = "Contact No")]
        [RegularExpression(@"^(\+\d{1,3}[- ]?)?\d{10}$", ErrorMessage = "Invalid Contact No.")]
        [StringLength(10,MinimumLength =10,ErrorMessage ="Invalid Contact No.")]
        public string ContactNo { get; set; }
        [Required]
        [StringLength(30, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]

        public string Password { get; set; }
        
        public string Address { get; set; }

        public UserRole Role { get; set; }
        [Display(Name = "Created Date")]
        public DateTime CreatedDate { get; set; }
        [Display(Name = "Last Modified")]
        public DateTime LastModified { get; set; }
        [Display(Name = "Is Active User")]

        public bool IsActive { get; set; } 
        #endregion


        public User(SqlDataReader reader)
        {
            Load(reader);
        }
        public User() { }
        public void Load(SqlDataReader reader)
        {
            Id = reader.GetInt32("ID");
            Name = reader.GetString("Name");
            ContactNo = reader.GetString("ContactNo");
            Password = reader.GetString("Password");
            Address = reader.GetString("Address");
            Role = (UserRole )reader.GetInt32("Role");
            CreatedDate = reader.GetDateTime("Created");
            LastModified = reader.GetDateTime("LastModified");
            IsActive = reader.GetBoolean("IsActive");

        }

        public User(int id, string name, string address, string contactNo, string password, UserRole role, DateTime createdDate, DateTime lastModified, bool isActive)
        {
            Id = id;
            Name = name;
            Address = address;
            ContactNo = contactNo;
            Password = password;
            Role = role;
            CreatedDate = createdDate;
            LastModified = lastModified;
            IsActive = isActive;
        }

        public static bool DeleteUser(int id, string connectionString)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            string sql = "delete from users where Id=" + id;
            SqlCommand cmd = new(sql, connection);
            bool result = false;
            try
            {
                connection.Open();
                result = cmd.ExecuteNonQuery()>0;
            }
            finally { connection.Close(); }
            return result;
        }

        public User(int id, string connectionString)
        {
            Id = id;
            SqlConnection connection = new SqlConnection(connectionString);
            string sql = "SELECT * FROM Users WHERE Id = @Id";
            SqlCommand cmd = new SqlCommand(sql, connection);
            cmd.Parameters.AddWithValue("@Id", Id);
            try
            {
                connection.Open();
                var reader = cmd.ExecuteReader();
                if (reader.Read())
                    Load(reader);
                else
                    throw new Exception("User not found");

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

        public static List<User> GetUsers(string Conn, int Role = -1)
        {
            SqlConnection con = new SqlConnection(Conn);
            con.Open();
            string sql;
            if (Role != -1)
                sql = "select * from Users where Role=" + Role;
            else
                sql = "select * from Users";
            SqlCommand cmd = new SqlCommand(sql, con);

            cmd.CommandType = System.Data.CommandType.Text;
            //cmd.Parameters.AddWithValue("Role", Role);
            List<User> list = new List<User>();
            try
            {
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new User(reader));
                }

            }
            catch (Exception)
            {
                throw;
            }
            con.Close();
            return list;
        }
        public static int GetUserCount(string Conn, int Role = -1)
        {
            SqlConnection con = new SqlConnection(Conn);
            con.Open();
            string sql;
            if (Role != -1)
                sql = "select count (id) from Users where Role=" + Role;
            else
                sql = "select * from Users";
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.CommandType = System.Data.CommandType.Text;
            Int32 UserCount = 0;
            try
            {
                 UserCount = (Int32)cmd.ExecuteScalar();
            }
            catch 
            {
                return 0;
            }
            finally
            {
                con.Close();
            }
            return UserCount;

        }
        public static bool CheckPhoneExists(string Conn, string ContactNo)
        {
            SqlConnection con = new SqlConnection(Conn);

            SqlCommand cmd = new SqlCommand("select count(*) as c from Users where ContactNo=@ContactNo", con);
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Parameters.AddWithValue("ContactNo", ContactNo);
            con.Open();
            SqlDataReader dr=cmd.ExecuteReader();
            int result = 0;
            if (dr.Read())
            {
                result = (int)dr["c"];
            }
            con.Close();
            return result == 1;
        }

        public static UserInfo? UserLogin(string Conn, string contactNo, string Password)
        {
            SqlConnection con = new SqlConnection(Conn);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;            
            con.Open();
            cmd.Connection = con;
            cmd.CommandText = "select * from Users where contactNo='" + contactNo + "' AND Password='" + Password + "'";
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                UserInfo u = new UserInfo( (int)(dr["id"]), (UserRole)dr["Role"], dr["Name"].ToString()?? "");
                con.Close();
                return u;
            }
            con.Close();
            return null;

        }

        public bool Save(string Conn,bool isNew=false)
        {
            SqlConnection con = new SqlConnection(Conn);
            string sql;
            if (Id == 0 || isNew)
                sql = "insert into Users values(@Name,@ContactNo,@Password,@Address,@Role,@Created,@LastModified,@IsActive)";
            else if(Password==null)
                sql = "update Users set Name=@Name,ContactNo=@ContactNo,Address=@Address,Role=@Role,Created=@Created,LastModified=@LastModified,IsActive=@IsActive where Id="+Id;
            else
                sql = "update Users set Name=@Name,ContactNo=@ContactNo,Password=@Password,Address=@Address,Role=@Role,Created=@Created,LastModified=@LastModified,IsActive=@IsActive where Id=" + Id;
            SqlCommand cmd = new SqlCommand(sql, con);
            cmd.CommandType = System.Data.CommandType.Text;

            cmd.Parameters.AddWithValue("Name", Name);
            cmd.Parameters.AddWithValue("ContactNo", ContactNo);
            if(Password!=null)
                cmd.Parameters.AddWithValue("Password", Password);
            cmd.Parameters.AddWithValue("Address", Address);
            cmd.Parameters.AddWithValue("Role", Role);
            cmd.Parameters.AddWithValue("Created", CreatedDate);
            cmd.Parameters.AddWithValue("LastModified", LastModified);
            cmd.Parameters.AddWithValue("IsActive", IsActive);
            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();
            return i>0;
        }
    }



}
