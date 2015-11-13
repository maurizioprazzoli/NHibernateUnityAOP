using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace TestInterceptionNoLeazyLoading
{
    [TestClass]
    public class TestRetrievingEntitiesSecondLevelCache : BaseTestInterceptionClass
    {
        public override void ComposeConfiguration(Dictionary<string, string> configuration)
        {
            configuration.Add("DataSource", "ITCPC1MAPR1");
            configuration.Add("UserId", "sa");
            configuration.Add("Password", "maurizio");
            configuration.Add("Database", "NHibernateUnityAOP");
            configuration.Add("ConnectTimeout", "30");
            configuration.Add("DropAndCreateDatabaseSchema", "True");
            configuration.Add("UseSecondLevelCache", "True");
            configuration.Add("UseNHibernateSimpleProfiler", "True");
            configuration.Add("IsForceUseSessionForTransaction", "False");
            configuration.Add("ConfigurationAssembly", "TestInterceptionNoLeazyLoading");
        }
    }
}