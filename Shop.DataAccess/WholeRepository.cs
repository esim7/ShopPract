using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Data.SqlClient;


namespace Shop.DataAccess
{
    public class WholeRepository : IDisposable
    {
        private readonly DbConnection connection;
        private readonly DbProviderFactory providerFactory;

        public UserRepository Users { get; set; }
        //public ItemRepository Items { get; set; }
        //public CategoryRepository Categories { get; set; }
        // в конструкторе открываем подключение
        // реализуем айдиспозабл
        // создаем переменные для каждого из наших репозиториев, которые пользуются единим подключение
        public WholeRepository(string providerName, string connectionString)
        {
            providerFactory = DbProviderFactories.GetFactory(providerName);
            connection = providerFactory.CreateConnection();
            connection.ConnectionString = connectionString;
            

            Users = new UserRepository(connection);
            ////Items = new ItemRepository(connection);
            //Categories = new CategoryRepository(connection);
            connection.Open();
        }

        public void Dispose()
        {
            connection.Close();
        }
    }
}
