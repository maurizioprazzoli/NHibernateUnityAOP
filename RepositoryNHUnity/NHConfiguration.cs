using Microsoft.Practices.Unity;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Tool.hbm2ddl;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Reflection;

namespace RepositoryNHUnity
{
    public class NHConfiguration
    {
        public static ISessionFactory ConfigureNHibernate(Dictionary<string, string> configuration, IUnityContainer container)
        {
            SqlConnectionStringBuilder connectionStringBuilder = new SqlConnectionStringBuilder();
            connectionStringBuilder.DataSource = configuration["DataSource"];
            connectionStringBuilder.UserID = configuration["UserId"];
            connectionStringBuilder.Password = configuration["Password"];
            connectionStringBuilder.InitialCatalog = configuration["Database"];
            connectionStringBuilder.ConnectTimeout = Int32.Parse(configuration["ConnectTimeout"]);

            var dropAndCreateDatabaseSchema = Convert.ToBoolean(configuration["DropAndCreateDatabaseSchema"]);
            var useSecondLevelCache = Convert.ToBoolean(configuration["UseSecondLevelCache"]);
            var generateStatistics = Convert.ToBoolean(configuration["GenerateStatistics"]);

            // Initialize
            Configuration cfg = new Configuration().DataBaseIntegration(db =>
                                                   {
                                                       db.ConnectionString = connectionStringBuilder.ConnectionString;
                                                       db.Dialect<MsSql2008Dialect>();
                                                   })
                                                   .AddAssembly(Assembly.GetExecutingAssembly());

            if (dropAndCreateDatabaseSchema)
            {
                var schemaExport = new SchemaExport(cfg);
                schemaExport.Drop(false, true);
                schemaExport.Create(false, true);
            }

            if (useSecondLevelCache)
            {
                cfg.SetProperty("cache.use_second_level_cache", "true");
                cfg.SetProperty("cache.provider_class", "NHibernate.Caches.SysCache.SysCacheProvider, NHibernate.Caches.SysCache");
            }
            else
            {
                cfg.SetProperty("cache.use_second_level_cache", "false");
            }

            if (generateStatistics)
            {
                cfg.SetProperty(NHibernate.Cfg.Environment.GenerateStatistics, "true");
            }

            var intercepter = new UnityDataBindingIntercepter(container);
            cfg.SetInterceptor(intercepter);

            // Create session factory from configuration object
            var sessionFactory = cfg.BuildSessionFactory();

            intercepter.SessionFactory = sessionFactory;

            return sessionFactory;
        }
    }
}
