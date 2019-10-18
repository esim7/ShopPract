using Shop.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.DataAccess.Abstract
{
   public interface IUserRepository
    {
        void Add(User user);
        void Delete(Guid userId);
        void Update(User user, string column, string newInformation);
        ICollection<User> GetAll();
    }
}
