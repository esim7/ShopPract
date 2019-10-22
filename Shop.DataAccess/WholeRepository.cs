using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using System.Data.SqlClient;
using System.Linq;

namespace Shop.DataAccess
{
    public class WholeRepository : IDisposable
    {
        private readonly DbConnection connection;
        private readonly DbProviderFactory providerFactory;

        public UserRepository Users { get; set; }
        //public ItemRepository Items { get; set; }
        public CategoryRepository Categories { get; set; }
        public WholeRepository(string providerName, string connectionString)
        {
            providerFactory = DbProviderFactories.GetFactory(providerName);
            connection = providerFactory.CreateConnection();
            connection.ConnectionString = connectionString;
            

            Users = new UserRepository(connection);
            ////Items = new ItemRepository(connection);
            Categories = new CategoryRepository(connection);
            connection.Open();
        }

        public void Dispose()
        {
            connection.Close();
        }

        //public void Find()
        //{
        //    IQueryable<WholeRepository> result = from c in  
        //}
    }
}
