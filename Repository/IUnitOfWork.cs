using System;

namespace Repository.Contract
{
    public interface IUnitOfWork : IDisposable
    {
        IItemRepository ItemRepository { get; }
        void Commit();
        void RollBack();
    }
}
