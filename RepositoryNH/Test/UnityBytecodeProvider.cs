using Microsoft.Practices.Unity;
using NHibernate.Bytecode;
using NHibernate.Type;
using System;

namespace RepositoryNH.Test
{
    class UnityBytecodeProvider : IBytecodeProvider, IInjectableCollectionTypeFactoryClass
    {

        IUnityContainer container;
        UnityObjectsFactory unityObjectsFactory;
        protected ICollectionTypeFactory unityCollectionTypeFactory;

        public UnityBytecodeProvider(IUnityContainer container)
        {
            this.container = container;
            this.unityObjectsFactory = new UnityObjectsFactory(container);
            this.unityCollectionTypeFactory = new DefaultCollectionTypeFactory();
        }

        public ICollectionTypeFactory CollectionTypeFactory
        {
            get { return unityCollectionTypeFactory; }
        }

        public IReflectionOptimizer GetReflectionOptimizer(System.Type clazz, NHibernate.Properties.IGetter[] getters, NHibernate.Properties.ISetter[] setters)
        {
            return new UnityReflectionOptimizer(container, clazz, getters, setters);
        }

        public IObjectsFactory ObjectsFactory
        {
            get { return unityObjectsFactory; }
        }

        public IProxyFactoryFactory ProxyFactoryFactory
        {
            get { return new UnityProxyFactoryFactory(container); }
        }

        public void SetCollectionTypeFactoryClass(System.Type type)
        {
            this.unityCollectionTypeFactory = (ICollectionTypeFactory)Activator.CreateInstance(type);
        }

        public void SetCollectionTypeFactoryClass(string typeAssemblyQualifiedName)
        {
            ((IInjectableCollectionTypeFactoryClass)this).SetCollectionTypeFactoryClass(System.Type.GetType(typeAssemblyQualifiedName, true));
        }
    }
}
