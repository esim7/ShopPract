//using Shop.DataAccess.Abstract;
//using Shop.Domain;
//using System;
//using System.Collections.Generic;
//using System.Data.Common;
//using System.Text;

//namespace Shop.DataAccess
//{
//    public class ItemRepository : IItemRepository
//    {
//        private readonly DbConnection connection;

//        public ItemRepository(DbConnection connection)
//        {
//            //this.connection = connection;
//        }

//        //public void Add(Item item)
//        //{
//        //    using (DbConnection connection = providerFactory.CreateConnection())
//        //    using (DbCommand sqlCommand = connection.CreateCommand())
//        //    {
//        //        string query = $"insert into Items (id, creationDate, name, imagePath, price, description, categoryId) values(@Id, " +
//        //                $"@CreationDate, " +
//        //                $"@Name," +
//        //                $"@ImagePath" +
//        //                $"Price" +
//        //                $"Description" +
//        //                $"CategoryId);";
//        //        sqlCommand.CommandText = query;

//        //        DbParameter parameter = providerFactory.CreateParameter();
//        //        parameter.DbType = System.Data.DbType.Guid;
//        //        parameter.ParameterName = "@Id";
//        //        parameter.Value = item.Id;
//        //        sqlCommand.Parameters.Add(parameter);

//        //        parameter = providerFactory.CreateParameter();
//        //        parameter.DbType = System.Data.DbType.DateTime;
//        //        parameter.ParameterName = "@CreationDate";
//        //        parameter.Value = item.CreationDate;
//        //        sqlCommand.Parameters.Add(parameter);

//        //        parameter = providerFactory.CreateParameter();
//        //        parameter.DbType = System.Data.DbType.String;
//        //        parameter.ParameterName = "@Name";
//        //        parameter.Value = item.Name;
//        //        sqlCommand.Parameters.Add(parameter);

//        //        parameter = providerFactory.CreateParameter();
//        //        parameter.DbType = System.Data.DbType.String;
//        //        parameter.ParameterName = "@ImagePath";
//        //        parameter.Value = item.ImagePath;
//        //        sqlCommand.Parameters.Add(parameter);

//        //        parameter = providerFactory.CreateParameter();
//        //        parameter.DbType = System.Data.DbType.Int32;
//        //        parameter.ParameterName = "@Price";
//        //        parameter.Value = item.Price;
//        //        sqlCommand.Parameters.Add(parameter);

//        //        parameter = providerFactory.CreateParameter();
//        //        parameter.DbType = System.Data.DbType.String;
//        //        parameter.ParameterName = "@Description";
//        //        parameter.Value = item.Description;
//        //        sqlCommand.Parameters.Add(parameter);

//        //        parameter = providerFactory.CreateParameter();
//        //        parameter.DbType = System.Data.DbType.Guid;
//        //        parameter.ParameterName = "@CategoryId";
//        //        parameter.Value = item.CategoryId;
//        //        sqlCommand.Parameters.Add(parameter);

//        //        connection.ConnectionString = connectionString;
//        //        connection.Open();

//        //        using (DbTransaction transaction = connection.BeginTransaction())
//        //        {
//        //            try
//        //            {
//        //                sqlCommand.Transaction = transaction;
//        //                sqlCommand.ExecuteNonQuery();
//        //                // и так далее тоже самое с другими командами

//        //                transaction.Commit();
//        //            }
//        //            catch
//        //            {
//        //                transaction.Rollback();
//        //            }
//        //        }
//        //    }
//        //}

//        public void Delete(Guid itemId)
//        {
//            throw new NotImplementedException();
//        }

//        //public ICollection<Item> GetAll()
//        //{
//        //    using (DbConnection connection = providerFactory.CreateConnection())
//        //    using (DbCommand sqlCommand = connection.CreateCommand())
//        //    {
//        //        string query = "select * from Items;";
//        //        sqlCommand.CommandText = query;

//        //        connection.ConnectionString = connectionString;
//        //        connection.Open();
//        //        DbDataReader sqlDataReader = sqlCommand.ExecuteReader();

//        //        List<Item> items = new List<Item>();
//        //        while (sqlDataReader.Read())
//        //        {
//        //            items.Add(new Item
//        //            {
//        //                Id = Guid.Parse(sqlDataReader["id"].ToString()),
//        //                CreationDate = DateTime.Parse(sqlDataReader["creationDate"].ToString()),
//        //                //DeletedDate = DateTime.Parse(sqlDataReader["deletedDate"].ToString()),
//        //                //DeletedDate = DateTime.Parse(sqlDataReader["deletedDate"].ToString()),
//        //                Name = sqlDataReader["name"].ToString(),
//        //                ImagePath = sqlDataReader["imagePath"].ToString(),
//        //                Price = Int32.Parse(sqlDataReader["price"].ToString()),
//        //                Description = sqlDataReader["description"].ToString(),
//        //                CategoryId = Guid.Parse(sqlDataReader["categoryId"].ToString())
//        //            });
//        //        }
//        //        return items;
//        //    }
//        //}

//        public void Update(Item item)
//        {
//            throw new NotImplementedException();
//        }
//    }
//}
