using Microsoft.Practices.Unity;
using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Tool.hbm2ddl;

namespace RepositoryNHUnity
{
    public class Repository
    {
        public Repository(IUnityContainer container)
        {
            var cfg = new Configuration();
            cfg.DataBaseIntegration(db =>
               {
                   db.ConnectionString = @"Data Source=.\SQLEXPRESS; Initial Catalog=NHibernateTest; Trusted_Connection=true";
                   db.Dialect<MsSql2008Dialect>();
                   db.BatchSize = 500;
               });
            cfg.AddAssembly("RepositoryNH");
            cfg.SetProperty("hbm2ddl.keywords", "auto-quote");

            //cfg.SetProperty("cache.use_second_level_cache", "true");
            //cfg.SetProperty("cache.default_expiration", "60000");
            //cfg.SetProperty("cache.provider_class", "NHibernate.Caches.SysCache.SysCacheProvider, NHibernate.Caches.SysCache");
            //cfg.SetProperty("connection.isolation", "ReadCommitted");

            var intercepter = new UnityDataBindingIntercepter(container);
            cfg.SetInterceptor(intercepter);

            cfg.SetProperty("max_fetch_depth", "10");


            var sessionFactory = cfg.BuildSessionFactory();

            intercepter.SessionFactory = sessionFactory;

            new SchemaExport(cfg).Create(true, true);
        }
    }
}
