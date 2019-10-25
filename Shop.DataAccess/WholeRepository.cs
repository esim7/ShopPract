using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Data.SqlClient;
using System.Linq;

namespace Shop.DataAccess
{
    /// <summary>
    /// Единый инструмент организующий манипуляции в базе данных для всех сущностей
    /// </summary>
    public class WholeRepository : IDisposable
    {
        public readonly DbConnection connection;
        private readonly DbProviderFactory providerFactory;

        public UserRepository Users { get; set; }
        public ItemRepository Items { get; set; }
        public CategoryRepository Categories { get; set; }
        public RatingRepository Ratings { get; set; }
        public OrderRepository Orders { get; set; }
        public ShopBasketRepository ShopBaskets { get; set; }
        public WholeRepository(string providerName, string connectionString)
        {
            providerFactory = DbProviderFactories.GetFactory(providerName);
            connection = providerFactory.CreateConnection();
            connection.ConnectionString = connectionString;
            

            Users = new UserRepository(connection);
            Categories = new CategoryRepository(connection);
            Items = new ItemRepository(connection);
            Ratings = new RatingRepository(connection);
            Orders = new OrderRepository(connection);
            ShopBaskets = new ShopBasketRepository(connection);
            connection.Open();
        }

        public void Dispose()
        {
            connection.Close();
        }
    }
}
