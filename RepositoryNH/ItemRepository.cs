using Model.Core;
using NHibernate;
using NHibernate.Linq;
using Repository.Contract;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;


namespace RepositoryNH
{
    class SchedulerWorkspaceRepository : IItemRepository
    {
        private ISession session;

        public SchedulerWorkspaceRepository(ISession session)
        {
            this.session = session;
        }

        public void Add(Item entity)
        {
            session.Save(entity);
        }

        public void Update(Item entity)
        {
            session.Update(entity);
        }

        public void Delete(Item entity)
        {
            session.Delete(entity);
        }

        public Item GetById(Guid id)
        {
            return session.Get<Item>(id);
        }

        public IEnumerable<Item> GetAll()
        {
            return session.Query<Item>();
        }

        public IEnumerable<Item> GetMany(Expression<Func<Item, bool>> where)
        {
            throw new System.NotImplementedException();
        }
    }
}
