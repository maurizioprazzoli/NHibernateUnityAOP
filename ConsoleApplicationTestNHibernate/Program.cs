using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using Model.Core;
using RepositoryNH;
using System;

namespace ConsoleApplicationTestNHibernate
{
    class Program
    {
        static void Main(string[] args)
        {

            HibernatingRhinos.Profiler.Appender.NHibernate.NHibernateProfiler.Initialize();

            var container = new UnityContainer();
            container.AddNewExtension<Interception>();

            container.RegisterType<Item, Item>(new InterceptionBehavior<PolicyInjectionBehavior>(),
                                               new Interceptor<VirtualMethodInterceptor>(),
                                               new AdditionalInterface<IInterceptable<Item>>());

            Console.WriteLine("Start");
            Console.ReadKey();

            new Repository(container);

            Console.WriteLine("End");

            Console.ReadKey();

        }
    }
}
