using Microsoft.Practices.Unity;
using NHibernate.Bytecode;
using NHibernate.Proxy;

namespace RepositoryNH.Test
{
    class UnityProxyFactoryFactory : IProxyFactoryFactory
    {
        private readonly IUnityContainer _container;


        public UnityProxyFactoryFactory()
            : this(new UnityContainer())
        {
        }

        public UnityProxyFactoryFactory(IUnityContainer container)
        {
            _container = container;
        }

        #region IProxyFactoryFactory Members

        public IProxyFactory BuildProxyFactory()
        {
            return new UnityProxyFactory();
        }

        public IProxyValidator ProxyValidator
        {
            get { return new DynProxyTypeValidator(); }
        }

        public bool IsInstrumented(System.Type entityClass)
        {
            return true;
        }

        public bool IsProxy(object entity)
        {
            return entity is INHibernateProxy;
        }

        #endregion
    }
}
