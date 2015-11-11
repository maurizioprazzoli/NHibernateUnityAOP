using Microsoft.Practices.Unity;
using NHibernate.Bytecode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryNH.Test
{
    class UnityObjectsFactory : IObjectsFactory 
    {
        private IUnityContainer container;

        public UnityObjectsFactory(Microsoft.Practices.Unity.IUnityContainer container)
        {
            // TODO: Complete member initialization
            this.container = container;
        }
        public object CreateInstance(Type type, params object[] ctorArgs)
        {
            if (container.IsRegistered(type))
            {
                throw new NotImplementedException();
            }
            else
            {
                return Activator.CreateInstance(type, ctorArgs);
            }
        }

        public object CreateInstance(Type type, bool nonPublic)
        {
            if (nonPublic)
            {
                return Activator.CreateInstance(type, nonPublic);
            }
            else
            {
                return CreateInstance(type);
            }
        }

        public object CreateInstance(Type type)
        {
            if (container.IsRegistered(type))
            {
                return container.Resolve(type);
            }
            else
            {
                return Activator.CreateInstance(type);
            }
        }
    }
}
