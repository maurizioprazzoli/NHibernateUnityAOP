using Microsoft.Practices.Unity;
using NHibernate;
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
        private bool isForceUseOneSessionForTransaction;

        public UnitOfWorkFactory(Dictionary<string, string> configuration, Func<IUnitOfWorkForContextDictionary> getUnitOfWorkForContextDictionary, IUnityContainer container)
        {
            this.configuration = configuration;

            this.getUnitOfWorkForContextDictionary = getUnitOfWorkForContextDictionary;
            this.sessionFactory = NHConfiguration.ConfigureNHibernate(configuration, container);

            this.isForceUseOneSessionForTransaction = Convert.ToBoolean(configuration["IsForceUseSessionForTransaction"]);
        }

        private void validateConfiguration()
        {

        }

        public IUnitOfWork CreateUnitOfWork
        {
            get
            {
                return new UnitOfWork(getNHSession(), isForceUseOneSessionForTransaction);
            }
        }

        private ISession getNHSession()
        {
            if (!this.isForceUseOneSessionForTransaction)
            {
                if (!getUnitOfWorkForContextDictionary().ContainsKey("currentSession"))
                {
                    getUnitOfWorkForContextDictionary()["currentSession"] = sessionFactory.OpenSession();
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
