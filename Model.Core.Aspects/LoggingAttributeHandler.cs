using Framework.Aspect.BaseClasses;
using Microsoft.Practices.Unity.InterceptionExtension;
using System;

namespace Model.Core.Aspects
{
    public class LoggingAttributeHandler : BaseInterceptionAttributeHandler
    {
        protected override void OnEntry(IMethodInvocation input)
        {
            Console.WriteLine("OnEntry");
        }

        protected override void OnSuccess(IMethodInvocation input)
        {
            Console.WriteLine("OnSuccess");
        }

        protected override void OnException(IMethodInvocation input)
        {
            Console.WriteLine("OnException");
        }

        protected override void OnExit(IMethodInvocation input)
        {
            Console.WriteLine("OnError");
        }

    }
}
