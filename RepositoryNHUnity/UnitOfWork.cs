using NHibernate;
using Repository.Contract;
using System;

namespace RepositoryNHUnity
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private ISession session;
        private ITransaction transaction;

        private IItemRepository itemRepository;
        bool isUseOneSessionForTransaction;

        public UnitOfWork(ISession session, bool isUseOneSessionForTransaction)
        {
            this.session = session;
            this.transaction = session.BeginTransaction();
            this.isUseOneSessionForTransaction = isUseOneSessionForTransaction;
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
                    this.itemRepository = new ItemRepository(session);

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
                transaction.Rollback();
            }

            if (isUseOneSessionForTransaction)
            {
                session.Close();
            }
        }

    }
}
