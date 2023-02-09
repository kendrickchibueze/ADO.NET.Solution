using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhatsAppChatDAL.Model;

namespace WhatsAppChatDAL
{
    public class WhatsAppService : IWhatsAppService
    {

        private WhatsAppDbContext _dbContext;

        private bool _disposed;


        public WhatsAppService(WhatsAppDbContext dbContext)
        {
            _dbContext = dbContext;

        }
        public async Task<long> CreateUser(UserViewModel user)
        {
            SqlConnection sqlConn = await _dbContext.OpenConnection();

            string insertQuery = $"INSERT INTO USERS(Name, Email, PhoneNumber, ProfileImage)" +
                                 $" VALUES (@Name, @Email, @PhoneNumber, @Image); SELECT CAST (SCOPE_IDENTITY() AS BIGINT)";


            await using SqlCommand command = new SqlCommand(insertQuery, sqlConn);

            command.Parameters.AddRange(new SqlParameter[]
            {

                   new SqlParameter
                   {
                        ParameterName = "@Name",
                        Value = user.Name,
                        SqlDbType = SqlDbType.NVarChar,
                        Direction = ParameterDirection.Input,
                        Size = 50
                   },

                    new SqlParameter
                    {
                        ParameterName = "@Email",
                        Value = user.Email,
                        SqlDbType = SqlDbType.NVarChar,
                        Direction = ParameterDirection.Input,
                        Size = 50
                    },

                    new SqlParameter
                    {
                        ParameterName = "@PhoneNumber",
                        Value = user.PhoneNumber,
                        SqlDbType = SqlDbType.NVarChar,
                        Direction = ParameterDirection.Input,
                        Size = 50
                    },

                    new SqlParameter
                    {
                        ParameterName = "@Image",
                        Value = user.ProfilePhoto,
                        SqlDbType = SqlDbType.NVarChar,
                        Direction = ParameterDirection.Input,
                        Size = 50
                    }

            });

            long userId =  (long)await command.ExecuteScalarAsync();

            return userId;
        }

     

      

        public async Task<bool> UpdateUser(int userId, UserViewModel user)
        {
            SqlConnection sqlConn = await _dbContext.OpenConnection();

            string updateQuery = "UPDATE USERS SET Name = @Name, Email = @Email, PhoneNumber = @Phone WHERE UserID = @UserId ";

            await using SqlCommand command = new SqlCommand(updateQuery, sqlConn);

            command.Parameters.AddRange(new SqlParameter[] 
            {

                new SqlParameter
                {
                    ParameterName = "@Name",
                    Value = user.Name,
                    SqlDbType = SqlDbType.NVarChar,
                    Direction = ParameterDirection.Input,
                    Size = 50

                },
                new SqlParameter
                {
                    ParameterName = "@Email",
                    Value = user.Email,
                    SqlDbType = SqlDbType.NVarChar,
                    Direction = ParameterDirection.Input,
                    Size = 50

                },

                new SqlParameter
                {
                    ParameterName = "@Phone",
                    Value = user.PhoneNumber,
                    SqlDbType = SqlDbType.NVarChar,
                    Direction = ParameterDirection.Input,
                    Size = 50

                },

                  new SqlParameter
                   {
                        ParameterName = "@UserId",
                        Value = userId,
                        SqlDbType = SqlDbType.NVarChar,
                        Direction = ParameterDirection.Input,
                        Size = 50
                   }





            });

            var result = command.ExecuteNonQuery();

            return (result == 0) ? false : true;

        }

        public async Task<bool> DeleteUser(int UserId)
        {

            SqlConnection sqlConn = await _dbContext.OpenConnection();


            string deleteQuery = "DELETE FROM USERS WHERE UserId = @UserId";


            SqlCommand command = new SqlCommand(deleteQuery, sqlConn);

            command.Parameters.AddRange(new SqlParameter[] {


                    new SqlParameter
                   {
                        ParameterName = "@UserId",
                        Value = UserId,
                        SqlDbType = SqlDbType.NVarChar,
                        Direction = ParameterDirection.Input,
                        Size = 50
                   }

            });

            var result = command.ExecuteNonQuery();

            //return (result == 0) ? false : true;

            return (result != 0);



        }



        public async Task<UserViewModel> GetUser(int id)
        {

            SqlConnection sqlConn = await _dbContext.OpenConnection();


            string getUserQuery = $"SELECT Users.Name,Users.Email,Users.PhoneNumber,Users.ProfileImage FROM Users WHERE UserId = @UserId "; 


            SqlCommand command = new SqlCommand(getUserQuery, sqlConn);


            command.Parameters.AddRange(new SqlParameter[] {

                    new SqlParameter
                    {
                        ParameterName = "@UserId",
                        Value = id,
                        SqlDbType = SqlDbType.NVarChar,
                        Direction = ParameterDirection.Input,
                        Size = 50
                    }



            });

            UserViewModel user = new UserViewModel();

            using(SqlDataReader dataReader = await command.ExecuteReaderAsync())
            {

                while (dataReader.Read())
                {
                    user.Name = dataReader["Name"].ToString();
                    user.Email = dataReader["Email"].ToString();
                    user.PhoneNumber = dataReader["PhoneNumber"].ToString();
                    user.ProfilePhoto = dataReader["ProfileImage"].ToString();


                }

            }
            return user;
        }

        public async Task<IEnumerable<UserViewModel>> GetUsers()
        {

            SqlConnection sqlConn = await _dbContext.OpenConnection();


            string getUsersQuery = $"SELECT Users.Name,Users.Email,Users.PhoneNumber,Users.ProfileImage FROM Users ";

            await using SqlCommand command = new SqlCommand(getUsersQuery, sqlConn);

            List<UserViewModel> users = new List<UserViewModel>();  

            using(SqlDataReader dataReader = await command.ExecuteReaderAsync())
            {
                while (dataReader.Read())
                {
                    users.Add(

                        new UserViewModel
                        {
                            Name = dataReader["Name"].ToString(),
                            Email = dataReader["Email"].ToString(),
                            PhoneNumber = dataReader["PhoneNumber"].ToString(),
                            ProfilePhoto = dataReader["ProfileImage"].ToString()
                        }
                
                        
                        );
                }
            }


            return users;




        }

        protected virtual void Dispose(bool disposing)
        {

            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                _dbContext.Dispose();
            }

            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
