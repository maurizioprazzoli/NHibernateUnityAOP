using Framework.Aspect.UnityExtensionMethod;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using Model.Core;
using Repository;
using Repository.Contract;
using RepositoryNHUnity;
using System;
using System.Collections.Generic;

namespace ConsoleApplicationTestNHibernate
{
    class Program
    {
        static void Main(string[] args)
        {
            // Define unity repository
            var container = new UnityContainer();

            // RegisterUnitOfwork
            Dictionary<string, string> configuration = new Dictionary<string, string>();
            configuration.Add("DataSource", "ITCPC1MAPR1");
            configuration.Add("UserId", "sa");
            configuration.Add("Password", "maurizio");
            configuration.Add("Database", "NHibernateUnityAOP");
            configuration.Add("ConnectTimeout", "30");
            configuration.Add("DropAndCreateDatabaseSchema", "True");
            container.RegisterType<IUnitOfWorkForContextDictionary, UnitOfWorkForContextDictionary>(new PerThreadLifetimeManager(), new InjectionMember[] { });
            Func<IUnitOfWorkForContextDictionary> getUnitOfWorkForContextDictionary = () =>
            {
                return container.Resolve<IUnitOfWorkForContextDictionary>();
            };
            container.RegisterType<IUnitOfWorkFactory, UnitOfWorkFactory>(new InjectionMember[] {
                                                                                                 new InjectionConstructor(new object[] {configuration, getUnitOfWorkForContextDictionary, container})
                                                                                                }
                                                                          );
            // Add interception component
            container.AddNewExtension<Interception>();

            // Register Item entity into container injecting Intercepted interface
            container.RegisterTypeInterception<Item>();

            Console.WriteLine("Setup complete. Press any key to continue.");
            Console.ReadKey();

            Guid item_guid = Guid.NewGuid();

            //item.PlaceBid("bidDescription1", 1);
            //item.PlaceBid("bidDescription1", 2);

            Item item = container.Resolve<Item>();
            item.Id = item_guid;
            item.PlaceBid("bidDescription1", 1);
            item.PlaceBid("bidDescription1", 2);

            using (IUnitOfWork unitOfWork = container.Resolve<IUnitOfWorkFactory>().CreateUnitOfWork)
            {
                unitOfWork.ItemRepository.Add(item);
                unitOfWork.Commit();
            }

        }
    }
}
