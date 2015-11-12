using NHibernate;
using Repository.Contract;
using System;

namespace RepositoryNH
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private ISession session;
        private ITransaction transaction;

        private IItemRepository itemRepository;

        public UnitOfWork(ISession session)
        {
            this.session = session;
            this.transaction = session.BeginTransaction();
        }

        public void Commit()
        {
            try
            {
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        public IItemRepository ItemRepository
        {
            get
            {
                if (this.itemRepository == null)
                {
                    this.itemRepository = new SchedulerWorkspaceRepository(session);

                }
                return this.itemRepository;
            }
        }

        public void RollBack()
        {
            transaction.Rollback();
        }

        public void Dispose()
        {
            if (session.Transaction.IsActive)
            {
                //transaction.Rollback();
            }
            //session.Close();
        }

    }
}
