using Microsoft.Practices.Unity;
using NHibernate.Properties;
using System;

namespace RepositoryNH.Test
{
    class UnityReflectionOptimizer : NHibernate.Bytecode.Lightweight.ReflectionOptimizer
    {
        protected IUnityContainer container;

        public UnityReflectionOptimizer(IUnityContainer container, Type mappedType, IGetter[] getters, ISetter[] setters)
            : base(mappedType, getters, setters)
        {
            this.container = container;
        }

        /// <summary>
        /// Ignore this check
        /// </summary>
        /// <param name="type"></param>
        protected override void ThrowExceptionForNoDefaultCtor(Type type)
        {
        }

        public override object CreateInstance()
        {
            return container.IsRegistered(mappedType) ? container.Resolve(mappedType) : base.CreateInstance();
        }

    }
}
