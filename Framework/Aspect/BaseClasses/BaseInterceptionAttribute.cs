
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using System;

namespace Framework.Aspect.BaseClasses
{

    public class BaseInterceptionAttribute : HandlerAttribute
    {
        private Type typeOfIterceptionAttributeHandler;
        private object[] args;

        public BaseInterceptionAttribute(Type typeOfIterceptionAttributeHandler)
            : this(typeOfIterceptionAttributeHandler, null)
        { }

        public BaseInterceptionAttribute(Type typeOfIterceptionAttributeHandler, object[] args)
        {
            this.typeOfIterceptionAttributeHandler = typeOfIterceptionAttributeHandler;
            this.args = args;
        }

        public override ICallHandler CreateHandler(IUnityContainer container)
        {
            return (ICallHandler)Activator.CreateInstance(typeOfIterceptionAttributeHandler, args);
        }
    }
}
