using Shop.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.DataAccess.Abstract
{
    public interface IItemRepository
    {
        void Add(Item item);
        void Delete(Guid itemId);
        void Update(Item item);
        ICollection<Item> GetAll();
    }
}
