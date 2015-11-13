using Framework.Aspect.StateTransaction;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model.Core;
using Repository.Contract;
using System;
using System.Collections.Generic;

namespace TestInterceptionLeazyLoading
{
    [TestClass]
    public class TestRetrievingEntitiesNoSecondLevelCacheForceUseSessionForTransaction : BaseTestInterceptionClass
    {
        public override void ComposeConfiguration(Dictionary<string, string> configuration)
        {
            configuration.Add("DataSource", "ITCPC1MAPR1");
            configuration.Add("UserId", "sa");
            configuration.Add("Password", "maurizio");
            configuration.Add("Database", "NHibernateUnityAOP");
            configuration.Add("ConnectTimeout", "30");
            configuration.Add("DropAndCreateDatabaseSchema", "True");
            configuration.Add("UseSecondLevelCache", "False");
            configuration.Add("UseNHibernateSimpleProfiler", "True");
            configuration.Add("IsForceUseSessionForTransaction", "True");
            configuration.Add("ConfigurationAssembly", "TestWithoutInterceptionLeazyLoading");
            configuration.Add("UseUnityInterception", "False");
        }

        [TestMethod]
        public override void InterceptedMethodCanCalledForObjectInsertedAndRetrievedFromDatabase()
        {
            Guid item_guid = Guid.NewGuid();

            using (IUnitOfWork unitOfWork = container.Resolve<IUnitOfWorkFactory>().CreateUnitOfWork)
            {
                Item item = new Item();
                item.Id = item_guid;
                item.PlaceBid("bidDescription1", 1);
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
            itemRetrieved.PlaceBid("bidDescription2", 1);
        }
    }
}