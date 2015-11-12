using NHibernate;
using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Tool.hbm2ddl;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Reflection;

namespace RepositoryNH
{
    public class NHConfiguration
    {
        public static ISessionFactory ConfigureNHibernate(Dictionary<string, string> configuration)
        {
            SqlConnectionStringBuilder connectionStringBuilder = new SqlConnectionStringBuilder();
            connectionStringBuilder.DataSource = configuration["DataSource"];
            connectionStringBuilder.UserID = configuration["UserId"];
            connectionStringBuilder.Password = configuration["Password"];
            connectionStringBuilder.InitialCatalog = configuration["Database"];
            connectionStringBuilder.ConnectTimeout = Int32.Parse(configuration["ConnectTimeout"]);

            var dropAndCreateDatabaseSchema = Convert.ToBoolean(configuration["DropAndCreateDatabaseSchema"]);

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

            cfg.SetProperty("cache.use_second_level_cache", "false");
            cfg.SetProperty("cache.default_expiration", "100000");
            cfg.SetProperty("cache.provider_class", "NHibernate.Caches.SysCache.SysCacheProvider, NHibernate.Caches.SysCache");

            // Create session factory from configuration object
            return cfg.BuildSessionFactory();
        }
    }
}
