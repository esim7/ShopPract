using Shop.DataAccess.Abstract;
using Shop.Domain;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace Shop.DataAccess
{
    public class ItemRepository : IItemRepository
    {
        private readonly DbConnection connection;

        public ItemRepository(DbConnection connection)
        {
            this.connection = connection;
        }

        public void Add(Item item)
        {
            using (DbCommand dbCommand = connection.CreateCommand())
            {
                string query = $"insert into Items (id, creationDate, name, imagePath, price, description, categoryId) values(@Id, " +
                        $"@CreationDate, " +
                        $"@Name, " +
                        $"@ImagePath, " +
                        $"@Price, " +
                        $"@Description, " +
                        $"@CategoryId);";
                dbCommand.CommandText = query;

                DbParameter parameter = dbCommand.CreateParameter();
                parameter.DbType = System.Data.DbType.Guid;
                parameter.ParameterName = "@Id";
                parameter.Value = item.Id;
                dbCommand.Parameters.Add(parameter);

                parameter = dbCommand.CreateParameter();
                parameter.DbType = System.Data.DbType.DateTime;
                parameter.ParameterName = "@CreationDate";
                parameter.Value = item.CreationDate;
                dbCommand.Parameters.Add(parameter);

                parameter = dbCommand.CreateParameter();
                parameter.DbType = System.Data.DbType.String;
                parameter.ParameterName = "@Name";
                parameter.Value = item.Name;
                dbCommand.Parameters.Add(parameter);

                parameter = dbCommand.CreateParameter();
                parameter.DbType = System.Data.DbType.String;
                parameter.ParameterName = "@ImagePath";
                parameter.Value = item.ImagePath;
                dbCommand.Parameters.Add(parameter);

                parameter = dbCommand.CreateParameter();
                parameter.DbType = System.Data.DbType.Int32;
                parameter.ParameterName = "@Price";
                parameter.Value = item.Price;
                dbCommand.Parameters.Add(parameter);

                parameter = dbCommand.CreateParameter();
                parameter.DbType = System.Data.DbType.String;
                parameter.ParameterName = "@Description";
                parameter.Value = item.Description;
                dbCommand.Parameters.Add(parameter);

                parameter = dbCommand.CreateParameter();
                parameter.DbType = System.Data.DbType.Guid;
                parameter.ParameterName = "@CategoryId";
                parameter.Value = item.CategoryId;
                dbCommand.Parameters.Add(parameter);

                //for(int i = 0; i < item.Rating.Count; i++)
                //{
                //    parameter = dbCommand.CreateParameter();
                //    parameter.DbType = System.Data.DbType.Guid;
                //    parameter.ParameterName = "@Id";
                //    parameter.Value = item.Rating[i].Id;
                //    dbCommand.Parameters.Add(parameter);

                //    parameter = dbCommand.CreateParameter();
                //    parameter.DbType = System.Data.DbType.DateTime;
                //    parameter.ParameterName = "@CreationDate";
                //    parameter.Value = item.Rating[i].CreationDate;
                //    dbCommand.Parameters.Add(parameter);

                //    parameter = dbCommand.CreateParameter();
                //    parameter.DbType = System.Data.DbType.String;
                //    parameter.ParameterName = "@UserName";
                //    parameter.Value = item.Rating[i].UserName;
                //    dbCommand.Parameters.Add(parameter);

                //    parameter = dbCommand.CreateParameter();
                //    parameter.DbType = System.Data.DbType.Guid;
                //    parameter.ParameterName = "@ItemId";
                //    parameter.Value = item.Rating[i].ItemId;
                //    dbCommand.Parameters.Add(parameter);

                //    parameter = dbCommand.CreateParameter();
                //    parameter.DbType = System.Data.DbType.Int32;
                //    parameter.ParameterName = "@Mark";
                //    parameter.Value = item.Rating[i].Mark;
                //    dbCommand.Parameters.Add(parameter);
                //}
                //parameter = dbCommand.CreateParameter();
                //parameter.DbType = System.Data.DbType.String;
                //parameter.ParameterName = "@Comentary";
                //parameter.Value = item.Comentary;
                //dbCommand.Parameters.Add(parameter);


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

        public void Delete(Guid itemId)
        {
            throw new NotImplementedException();
        }

        public ICollection<Item> GetAll()
        {
            using (DbCommand dbCommand = connection.CreateCommand())
            {
                string query = "select * from Items;";
                dbCommand.CommandText = query;

                //connection.ConnectionString = connectionString;
                //connection.Open();
                DbDataReader bdDataReader = dbCommand.ExecuteReader();
                

                List<Item> items = new List<Item>();
                while (bdDataReader.Read())
                {
                    items.Add(new Item
                    {
                        Id = Guid.Parse(bdDataReader["id"].ToString()),
                        CreationDate = DateTime.Parse(bdDataReader["creationDate"].ToString()),
                        //DeletedDate = DateTime.Parse(sqlDataReader["deletedDate"].ToString()),
                        Name = bdDataReader["name"].ToString(),
                        ImagePath = bdDataReader["imagePath"].ToString(),
                        Price = Int32.Parse(bdDataReader["Price"].ToString()),
                        Description = bdDataReader["description"].ToString(),
                        CategoryId = Guid.Parse(bdDataReader["categoryId"].ToString())
                    });
                }
                bdDataReader.Close();
                return items;
            }
        }

        public void Update(Item item, string column, string newInformation)
        {
            string query = $"update Items set {column} = '{newInformation}' where id = '{item.Id}';";
            using (DbCommand dbCommand = connection.CreateCommand())
            {
                dbCommand.CommandText = query;
                dbCommand.ExecuteNonQuery();
            }
        }
        /// <summary>
        /// Метод организующий постраничный вывод информации из БД(пагинация)
        /// </summary>
        /// <param name="pageNumber">принимает номер страницы которую следует вывести на экран</param>
        /// <returns></returns>
        public ICollection <Item>GetDataInDb(int pageNumber)
        {
            int objectPerPageSize = 3;
            --pageNumber;
            if (pageNumber < 0)
            {
                throw new ArgumentOutOfRangeException();
            }
            using (DbCommand dbCommand = connection.CreateCommand())
            {
                string query = "SELECT * FROM Items ORDER BY id OFFSET ((" + pageNumber + ") * " + objectPerPageSize + ") " +
                "ROWS FETCH NEXT " + objectPerPageSize + " ROWS ONLY";
                dbCommand.CommandText = query;

                DbDataReader bdDataReader = dbCommand.ExecuteReader();
                List<Item> paginationShow = new List<Item>();
                while (bdDataReader.Read())
                {
                    paginationShow.Add(new Item
                    {
                        Id = Guid.Parse(bdDataReader["id"].ToString()),
                        Name = bdDataReader["name"].ToString(),
                        Price = Int32.Parse(bdDataReader["price"].ToString()),
                        ImagePath = bdDataReader["imagePath"].ToString(),
                        Description = bdDataReader["description"].ToString()
                    });
                }
                bdDataReader.Close();
                return paginationShow;
            }
        }

        public ICollection<Item> Find(int pageNumber, string itemName)
        {
            int objectPerPageSize = 3;
            --pageNumber;
            if (pageNumber < 0)
            {
                throw new ArgumentOutOfRangeException();
            }
            using (DbCommand dbCommand = connection.CreateCommand())
            {
                //string query = "SELECT * FROM Items ORDER BY id OFFSET ((" + pageNumber + ") * " + objectPerPageSize + ") " +
                //"ROWS FETCH NEXT " + objectPerPageSize + " ROWS ONLY";

                string query = $"select * from Items where [name] like '%{itemName}%' ORDER BY id OFFSET ((" + pageNumber + ") * " + objectPerPageSize + ") " +
                    "ROWS FETCH NEXT " + objectPerPageSize + "ROWS ONLY;";
                dbCommand.CommandText = query;

                DbDataReader bdDataReader = dbCommand.ExecuteReader();
                List<Item> paginationShow = new List<Item>();
                while (bdDataReader.Read())
                {
                    paginationShow.Add(new Item
                    {
                        Id = Guid.Parse(bdDataReader["id"].ToString()),
                        Name = bdDataReader["name"].ToString(),
                        Price = Int32.Parse(bdDataReader["price"].ToString()),
                        ImagePath = bdDataReader["imagePath"].ToString(),
                        Description = bdDataReader["description"].ToString()
                    });
                }
                bdDataReader.Close();
                return paginationShow;
            }
        }
    }
}
