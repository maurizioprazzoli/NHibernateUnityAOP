using Framework.Aspect.StateTransaction;
using Framework.UnityExtensionMethod;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model.Core;
using NHibernate.Stat;
using Repository;
using Repository.Contract;
using RepositoryNHUnity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TestInterception
{
    [TestClass]
    public class TestRetrievingEntitiesSecondLevelCache
    {
        // Define unity repository
        private static IUnityContainer container = new UnityContainer();
        private static NHibernateStatisticsHelper nHibernateStatisticsHelper;

        [ClassInitialize]
        public static void InitializeTest(TestContext testContext)
        {
            // RegisterUnitOfwork
            Dictionary<string, string> configuration = new Dictionary<string, string>();
            configuration.Add("DataSource", "ITCPC1MAPR1");
            configuration.Add("UserId", "sa");
            configuration.Add("Password", "maurizio");
            configuration.Add("Database", "NHibernateUnityAOP");
            configuration.Add("ConnectTimeout", "30");
            configuration.Add("DropAndCreateDatabaseSchema", "True");
            configuration.Add("UseSecondLevelCache", "True");
            configuration.Add("GenerateStatistics", "True");

            container.RegisterType<IUnitOfWorkForContextDictionary, UnitOfWorkForContextDictionary>(new PerThreadLifetimeManager(), new InjectionMember[] { });
            Func<IUnitOfWorkForContextDictionary> getUnitOfWorkForContextDictionary = () =>
            {
                return container.Resolve<IUnitOfWorkForContextDictionary>();
            };
            container.RegisterType<IUnitOfWorkFactory, UnitOfWorkFactory>(  new ExternallyControlledLifetimeManager(),
                                                                            new InjectionMember[] {
                                                                                                     new InjectionConstructor(new object[] {configuration, getUnitOfWorkForContextDictionary, container})
                                                                                                   }
                                                                          );
            // Add interception component
            container.AddNewExtension<Interception>();

            // Register Item entity into container injecting Intercepted interface
            container.RegisterTypeInterception<Item>();

            nHibernateStatisticsHelper = new NHibernateStatisticsHelper(container.Resolve<IUnitOfWorkFactory>());
        }

        [TestMethod]
        public void TestInterceptionMethodIsCalledWhenObjectIsConstructedFromContainer()
        {
            Guid item_guid = Guid.NewGuid();

            Item item = container.Resolve<Item>();
            item.Id = item_guid;
            Assert.IsTrue(((IStateTransactionctionable<ItemStatus>)item).State == ItemStatus.ITNUL);
            item.PlaceBid("bidDescription1", 1);
            Assert.IsTrue(((IStateTransactionctionable<ItemStatus>)item).State == ItemStatus.ITINS);
        }

        [TestMethod]
        public void TestInterceptedObjectCanBeInsertedIntoDatabase()
        {
            var numberOfExpectedQuery = 3;

            var numberOfQueryAtStart = nHibernateStatisticsHelper.PrepareStatementCount();
            
            Guid item_guid = Guid.NewGuid();

            Item item = container.Resolve<Item>();
            item.Id = item_guid;
            item.PlaceBid("bidDescription1", 1);

            // Two query Expected
            using (IUnitOfWork unitOfWork = container.Resolve<IUnitOfWorkFactory>().CreateUnitOfWork)
            {
                unitOfWork.ItemRepository.Add(item);
                unitOfWork.Commit();
            }

            var numberOfExecutedQuery = nHibernateStatisticsHelper.PrepareStatementCount() - numberOfQueryAtStart;

            Assert.IsTrue(numberOfExpectedQuery == numberOfExecutedQuery);
        }

        [TestMethod]
        public void TestInterceptedObjectCanBeRetrievedFromDatabase()
        {
            var numberOfExpectedQuery = 5;
            var numberOfQueryAtStart = nHibernateStatisticsHelper.PrepareStatementCount();
            
            Guid item_guid = Guid.NewGuid();

            Item item = container.Resolve<Item>();
            item.Id = item_guid;
            item.PlaceBid("bidDescription1", 1);

            // Two queries expected
            using (IUnitOfWork unitOfWork = container.Resolve<IUnitOfWorkFactory>().CreateUnitOfWork)
            {
                unitOfWork.ItemRepository.Add(item);
                unitOfWork.Commit();
            }

            Item itemRetrieved;

            using (IUnitOfWork unitOfWork = container.Resolve<IUnitOfWorkFactory>().CreateUnitOfWork)
            {
                itemRetrieved = unitOfWork.ItemRepository.GetById(item_guid);
                unitOfWork.Commit();
            }

            Assert.IsTrue(itemRetrieved != null);

            var numberOfExecutedQuery = nHibernateStatisticsHelper.PrepareStatementCount() - numberOfQueryAtStart;

            Assert.IsTrue(numberOfExpectedQuery == numberOfExecutedQuery);
        }

        [TestMethod]
        public void TestInterceptedMethodCanCalledForObjectRetrievedFromDatabase()
        {
            var numberOfExpectedQuery = 3;
            var numberOfQueryAtStart = nHibernateStatisticsHelper.PrepareStatementCount();
            
            Guid item_guid = Guid.NewGuid();

            using (IUnitOfWork unitOfWork = container.Resolve<IUnitOfWorkFactory>().CreateUnitOfWork)
            {
                Item item = container.Resolve<Item>();
                item.Id = item_guid;
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

            var numberOfExecutedQuery = nHibernateStatisticsHelper.PrepareStatementCount() - numberOfQueryAtStart;

            Assert.IsTrue(numberOfExpectedQuery == numberOfExecutedQuery);
        }
    }
}
