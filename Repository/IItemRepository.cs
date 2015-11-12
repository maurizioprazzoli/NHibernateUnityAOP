using Model.Core;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Repository.Contract
{
    public interface IItemRepository
    {
        void Add(Item entity);
        void Update(Item entity);
        void Delete(Item entity);

        Item GetById(Guid id);

        IEnumerable<Item> GetAll();
        IEnumerable<Item> GetMany(Expression<Func<Item, bool>> where);

    }
}
