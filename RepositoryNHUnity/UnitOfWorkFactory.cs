using Microsoft.Practices.Unity;
using NHibernate;
using NHibernate.Stat;
using Repository;
using Repository.Contract;
using System;
using System.Collections.Generic;

namespace RepositoryNHUnity
{
    public class UnitOfWorkFactory : IUnitOfWorkFactory
    {
        ISessionFactory sessionFactory;

        private Dictionary<string, string> configuration { get; set; }
        Func<IUnitOfWorkForContextDictionary> getUnitOfWorkForContextDictionary;
        private ISession currentSession;

        public UnitOfWorkFactory(Dictionary<string, string> configuration, Func<IUnitOfWorkForContextDictionary> getUnitOfWorkForContextDictionary, IUnityContainer container)
        {
            this.configuration = configuration;
            this.getUnitOfWorkForContextDictionary = getUnitOfWorkForContextDictionary;
            this.sessionFactory = NHConfiguration.ConfigureNHibernate(configuration, container);
        }

        public IUnitOfWork CreateUnitOfWork
        {
            get
            {
                return new UnitOfWork(getNHSession());
            }
        }

        private ISession getNHSession()
        {
            return sessionFactory.OpenSession();
            
            if (getUnitOfWorkForContextDictionary != null)
            {
                if (!getUnitOfWorkForContextDictionary().ContainsKey("currentSession"))
                {
                    getUnitOfWorkForContextDictionary()["currentSession"] = sessionFactory.OpenSession();
                    currentSession = (ISession)getUnitOfWorkForContextDictionary()["currentSession"];
                }
                return (ISession)getUnitOfWorkForContextDictionary()["currentSession"];
            }
            else
            {
                return sessionFactory.OpenSession();
            }
        }

        public IStatistics Statistics()
        {
            return this.sessionFactory.Statistics;
        }
    }
}
