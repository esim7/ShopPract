using Shop.DataAccess.Abstract;
using Shop.Domain;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace Shop.DataAccess
{
    public class UserRepository : IUserRepository
    {
        private readonly DbConnection connection;

        public UserRepository(DbConnection connection)
        {
            this.connection = connection;
        }
        public void Add(User user)
        {
            using (DbCommand dbCommand = connection.CreateCommand())
            {
                string query = $"insert into Users (id, creationDate, login, phoneNumber, email, address, password, verificationCode) values(@Id, " +
                        $"@CreationDate, " +
                        $"@Login, " +
                        $"@PhoneNumber, " +
                        $"@Email, " +
                        $"@Address, " +
                        $"@Password, " +
                        $"@VerificationCode);";

                dbCommand.CommandText = query;

                DbParameter parameter = dbCommand.CreateParameter();
                parameter.DbType = System.Data.DbType.Guid;
                parameter.ParameterName = "@Id";
                parameter.Value = user.Id;
                dbCommand.Parameters.Add(parameter);

                parameter = dbCommand.CreateParameter();
                parameter.DbType = System.Data.DbType.DateTime;
                parameter.ParameterName = "@CreationDate";
                parameter.Value = user.CreationDate;
                dbCommand.Parameters.Add(parameter);

                parameter = dbCommand.CreateParameter();
                parameter.DbType = System.Data.DbType.String;
                parameter.ParameterName = "@Login";
                parameter.Value = user.Login;
                dbCommand.Parameters.Add(parameter);

                parameter = dbCommand.CreateParameter();
                parameter.DbType = System.Data.DbType.String;
                parameter.ParameterName = "@PhoneNumber";
                parameter.Value = user.PhoneNumber;
                dbCommand.Parameters.Add(parameter);

                parameter = dbCommand.CreateParameter();
                parameter.DbType = System.Data.DbType.String;
                parameter.ParameterName = "@Email";
                parameter.Value = user.Email;
                dbCommand.Parameters.Add(parameter);

                parameter = dbCommand.CreateParameter();
                parameter.DbType = System.Data.DbType.String;
                parameter.ParameterName = "@Address";
                parameter.Value = user.Address;
                dbCommand.Parameters.Add(parameter);

                parameter = dbCommand.CreateParameter();
                parameter.DbType = System.Data.DbType.String;
                parameter.ParameterName = "@Password";
                parameter.Value = user.Password;
                dbCommand.Parameters.Add(parameter);
                
                parameter = dbCommand.CreateParameter();
                parameter.DbType = System.Data.DbType.String;
                parameter.ParameterName = "@VerificationCode";
                parameter.Value = user.VerificationCode;
                dbCommand.Parameters.Add(parameter);

                using (DbTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        dbCommand.Transaction = transaction;
                        dbCommand.ExecuteNonQuery();

                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                    }
                }
            }
        }

        public void Delete(Guid userId)
        {
            throw new NotImplementedException();
        }

        public ICollection<User> GetAll()
        {
            using (DbCommand dbCommand = connection.CreateCommand())
            {
                string query = "select * from Users;";
                dbCommand.CommandText = query;

                DbDataReader bdDataReader = dbCommand.ExecuteReader();

                List<User> users = new List<User>();
                while (bdDataReader.Read())
                {
                    users.Add(new User
                    {
                        Id = Guid.Parse(bdDataReader["id"].ToString()),
                        CreationDate = DateTime.Parse(bdDataReader["creationDate"].ToString()),
                        //DeletedDate = DateTime.Parse(sqlDataReader["deletedDate"].ToString()),
                        Login = bdDataReader["login"].ToString(),
                        PhoneNumber = bdDataReader["phoneNumber"].ToString(),
                        Email = bdDataReader["email"].ToString(),
                        Address = bdDataReader["address"].ToString(),
                        Password = bdDataReader["password"].ToString(),
                        VerificationCode = bdDataReader["verificationCode"].ToString()
                    });
                }
                bdDataReader.Close();
                return users;
            }
        }

        public void Update(User user, string column, string newInformation)
        {
            string query = $"update Users set {column} = '{newInformation}' where id = '{user.Id}';";
            using (DbCommand dbCommand = connection.CreateCommand())
            {
                dbCommand.CommandText = query;
                dbCommand.ExecuteNonQuery();
            }
        }
    }
}
