
namespace Repository.Contract
{
    public interface IUnitOfWorkFactory
    {
        IUnitOfWork CreateUnitOfWork { get; }
    }
}
