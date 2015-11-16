using NHibernate;
using Repository;
using Repository.Contract;
using System;
using System.Collections.Generic;

namespace RepositoryNH
{
    public class UnitOfWorkFactory : IUnitOfWorkFactory
    {
        ISessionFactory sessionFactory;

        private Dictionary<string, string> configuration { get; set; }
        Func<IUnitOfWorkForContextDictionary> getUnitOfWorkForContextDictionary;
        private ISession currentSession;

        public UnitOfWorkFactory(Dictionary<string, string> configuration, Func<IUnitOfWorkForContextDictionary> getUnitOfWorkForContextDictionary)
        {
            this.configuration = configuration;
            this.getUnitOfWorkForContextDictionary = getUnitOfWorkForContextDictionary;
            this.sessionFactory = NHConfiguration.ConfigureNHibernate(configuration);
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
            if (getUnitOfWorkForContextDictionary != null)
            {
                 Console.WriteLine("ThreadID: {0}, GetHashCode: {1}, HaveKey {2}", System.Threading.Thread.CurrentThread.ManagedThreadId, getUnitOfWorkForContextDictionary().GetHashCode(), getUnitOfWorkForContextDictionary().ContainsKey("currentSession"));
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
    }
}
