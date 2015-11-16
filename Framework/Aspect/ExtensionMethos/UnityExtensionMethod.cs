using Framework.Aspect.Interfaces;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using System;
using System.Linq;

namespace Framework.Aspect.UnityExtensionMethod
{
    public static class UnityExtensionMethod
    {
        private const string IINTERCEPTABLE_INTERFACE_NAME = "IInterceptable";

        public static void RegisterTypeInterception(this IUnityContainer container, Type type)
        {
            // Before alloe to register an interception we must ensure of follow:

            // 1- type MUST be Implement IInterceptable interface
            if (type.GetInterface(IINTERCEPTABLE_INTERFACE_NAME) == null)
            {
                throw new Exception(String.Format("Cannot register interception to object of type {0}. The object not implement {1} interface.", typeof(Type), IINTERCEPTABLE_INTERFACE_NAME));
            }

            // 2 - type must not have any pubblic constructor
            // @ToDo

            // Construct closed generic interface starting from type to inject
            var closedTypeInterfaceToInject = typeof(IIntercepted<>).MakeGenericType(type);
            // Registed new type inro container injecting specific interface for eneable
            // tracking of injected objects
            container.RegisterType(type,
                                   type,
                                   new InterceptionBehavior<PolicyInjectionBehavior>(),
                                   new Interceptor<VirtualMethodInterceptor>(),
                                   new AdditionalInterface(closedTypeInterfaceToInject));
        }

        public static void RegisterTypeInterception<TTypeToIntercept>(this IUnityContainer container)
        {
            RegisterTypeInterception(container, typeof(TTypeToIntercept));
        }

        public static bool IsRegisteredTypeInterception(this IInterceptable type)
        {
            var interceptedType = type.GetType().GetInterfaces().Where(i => i.IsGenericType
                                                                       && i.GetGenericTypeDefinition() == typeof(IIntercepted<>)
                                                                       && i.GetGenericArguments().Count() == 1).SingleOrDefault();

            return interceptedType != null;
        }

    }
}
