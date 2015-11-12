using Framework.Aspect.Interfaces;
using Microsoft.Practices.Unity;
using NHibernate;
using System;
using System.Linq;

namespace RepositoryNHUnity
{
    public class UnityDataBindingIntercepter : EmptyInterceptor
    {
        private readonly IUnityContainer container;

        public ISessionFactory SessionFactory { set; get; }

        public UnityDataBindingIntercepter(IUnityContainer container)
        {
            this.container = container;
        }

        public override object Instantiate(string clazz, EntityMode entityMode, object id)
        {
            if (entityMode == EntityMode.Poco)
            {
                Type type = Type.GetType(SessionFactory.GetClassMetadata(clazz).GetMappedClass(EntityMode.Poco).AssemblyQualifiedName);
                if (type != null && container.IsRegistered(type))
                {
                    var instance = container.Resolve(type);
                    SessionFactory.GetClassMetadata(clazz).SetIdentifier(instance, id, entityMode);
                    return instance;
                }
            }
            return base.Instantiate(clazz, entityMode, id);
        }

        public override string GetEntityName(object entity)
        {
            var interceptedType = entity.GetType().GetInterfaces().Where(i => i.IsGenericType
                                                                         && i.GetGenericTypeDefinition() == typeof(IIntercepted<>)
                                                                         && i.GetGenericArguments().Count() == 1).SingleOrDefault();
            if (interceptedType != null)
            {
                return interceptedType.GetGenericArguments()[0].FullName;
            }
            else
            {
                return base.GetEntityName(entity);
            }

        }
    }
}
