using Framework.Aspect.StateTransaction;
using Framework.Aspect.UnityExtensionMethod;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model.Core;
using Repository;
using Repository.Contract;
using RepositoryNHUnity;
using System;
using System.Collections.Generic;

namespace TestInterceptionLeazyLoading
{
    public abstract class BaseTestInterceptionClass
    {
        // Define unity repository
        protected IUnityContainer container = new UnityContainer();


        public BaseTestInterceptionClass()
        {
            // RegisterUnitOfwork
            Dictionary<string, string> configuration = new Dictionary<string, string>();

            ComposeConfiguration(configuration);

            container.RegisterType<IUnitOfWorkForContextDictionary, UnitOfWorkForContextDictionary>(new PerThreadLifetimeManager(), new InjectionMember[] { });
            Func<IUnitOfWorkForContextDictionary> getUnitOfWorkForContextDictionary = () =>
            {
                return container.Resolve<IUnitOfWorkForContextDictionary>();
            };
            container.RegisterType<IUnitOfWorkFactory, UnitOfWorkFactory>(new ExternallyControlledLifetimeManager(),
                                                                            new InjectionMember[] {
                                                                                                     new InjectionConstructor(new object[] {configuration, getUnitOfWorkForContextDictionary, container})
                                                                                                   }
                                                                          );
            // Add interception component
            container.AddNewExtension<Interception>();

            // Register Item entity into container injecting Intercepted interface
            container.RegisterTypeInterception<Item>();

            container.Resolve<IUnitOfWorkFactory>();
        }

        [TestMethod]
        public virtual void InterceptedMethodCanCalledForObjectInsertedAndRetrievedFromDatabase()
        {
            Guid item_guid = Guid.NewGuid();

            using (IUnitOfWork unitOfWork = container.Resolve<IUnitOfWorkFactory>().CreateUnitOfWork)
            {
                Item item = container.Resolve<Item>();
                item.Id = item_guid;
                item.PlaceBid("BidDescription", 1);
                unitOfWork.ItemRepository.Add(item);
                unitOfWork.Commit();
            }

            Item itemRetrieved;

            using (IUnitOfWork unitOfWork = container.Resolve<IUnitOfWorkFactory>().CreateUnitOfWork)
            {
                itemRetrieved = unitOfWork.ItemRepository.GetById(item_guid);
                unitOfWork.Commit();
            }

            Assert.IsTrue(((IStateTransactionctionable<ItemStatus>)itemRetrieved).State == ItemStatus.ITNUL);
            itemRetrieved.PlaceBid("bidDescription1", 1);
            Assert.IsTrue(((IStateTransactionctionable<ItemStatus>)itemRetrieved).State == ItemStatus.ITINS);
        }

        public abstract void ComposeConfiguration(Dictionary<string, string> configuration);
    }
}
