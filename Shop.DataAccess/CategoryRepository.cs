using Shop.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using Shop.DataAccess.Abstract;
using System.Data.SqlClient;
using System.Data.Common;

namespace Shop.DataAccess
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DbConnection connection;

        public CategoryRepository(DbConnection connection)
        {
            this.connection = connection;
        }

        public void Add(Category category)
        {
            //using (DbConnection connection = providerFactory.CreateConnection())
            using (DbCommand dbCommand = connection.CreateCommand())
            {
                string query = $"insert into Categories (id, creationDate, name, imagePath) values(@Id, " +
                        $"@CreationDate, " +
                        $"@Name," +
                        $"@ImagePath);";
                dbCommand.CommandText = query;

                DbParameter parameter = dbCommand.CreateParameter();
                parameter.DbType = System.Data.DbType.Guid;
                parameter.ParameterName = "@Id";
                parameter.Value = category.Id;
                dbCommand.Parameters.Add(parameter);

                parameter = dbCommand.CreateParameter();
                parameter.DbType = System.Data.DbType.DateTime;
                parameter.ParameterName = "@CreationDate";
                parameter.Value = category.CreationDate;
                dbCommand.Parameters.Add(parameter);

                parameter = dbCommand.CreateParameter();
                parameter.DbType = System.Data.DbType.String;
                parameter.ParameterName = "@Name";
                parameter.Value = category.Name;
                dbCommand.Parameters.Add(parameter);

                parameter = dbCommand.CreateParameter();
                parameter.DbType = System.Data.DbType.String;
                parameter.ParameterName = "@ImagePath";
                parameter.Value = category.ImagePath;
                dbCommand.Parameters.Add(parameter);

                using (DbTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        dbCommand.Transaction = transaction;
                        dbCommand.ExecuteNonQuery();
                        // и так далее тоже самое с другими командами

                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                    }
                }
            }
        }

        public void Delete(Guid categoryId)
        {
            throw new NotImplementedException();
        }

        public ICollection<Category> GetAll()
        {
            //using (DbConnection connection = providerFactory.CreateConnection())
            using (DbCommand dbCommand = connection.CreateCommand())
            {
                string query = "select * from Categories;";
                dbCommand.CommandText = query;

                //connection.ConnectionString = connectionString;
                //connection.Open();
                DbDataReader bdDataReader = dbCommand.ExecuteReader();

                List<Category> categories = new List<Category>();
                while (bdDataReader.Read())
                {
                    categories.Add(new Category
                    {
                        Id = Guid.Parse(bdDataReader["id"].ToString()),
                        CreationDate = DateTime.Parse(bdDataReader["creationDate"].ToString()),
                        //DeletedDate = DateTime.Parse(sqlDataReader["deletedDate"].ToString()),
                        Name = bdDataReader["name"].ToString(),
                        ImagePath = bdDataReader["imagePath"].ToString()
                    });
                }
                bdDataReader.Close();
                return categories;
            }
        }

        public void Update(Category category, string column, string newInformation)
        {
            string query = $"update Categories set {column} = '{newInformation}' where id = '{category.Id}';";
            using (DbCommand dbCommand = connection.CreateCommand())
            {
                dbCommand.CommandText = query;
                dbCommand.ExecuteNonQuery();
            }
        }
    }
}
