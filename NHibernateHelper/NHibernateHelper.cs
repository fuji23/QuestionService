using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHibernate;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate.Tool.hbm2ddl;
using System.Configuration;
using NHibernate.Context;
using System.Reflection;

namespace NHibernateHelper
{
    /// <summary>
    /// You must implement IEtity interface in all your entity classes 
    /// for compatibility with NHibernateHelper data provider
    /// </summary>
    public class NHibernateHelper
    {
        private static ISessionFactory _sessionFactory;

        private static ISessionFactory SessionFactory
        {
            get
            {
                if (_sessionFactory == null)
                    InitializeSessionFactory();

                return _sessionFactory;
            }
        }

        private static void InitializeSessionFactory()
        {
            _sessionFactory = Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2008
                              .ConnectionString(
                                 connstr => connstr.FromConnectionStringWithKey("DB-connect"))
                )
                .Mappings(m =>
                                {
                                    foreach (var item in ConfigurationManager.AppSettings.AllKeys)
                                    {
                                        if (item.StartsWith("entityAssembly"))
                                            m.FluentMappings.AddFromAssembly(
                                                Assembly.Load(ConfigurationManager.AppSettings.Get(item.ToString()))
                                                );
                                    }
                                }
            )
                .ExposeConfiguration(cfg => new SchemaUpdate(cfg)
                                                .Execute(false, true)).CurrentSessionContext<WebSessionContext>()
                .BuildConfiguration().BuildSessionFactory();
        }

        public static ISession OpenSession()
        {
            return SessionFactory.OpenSession();
        }

        public static IEnumerable<T> RetrieveEntities<T>(Func<IEnumerable<T>, IEnumerable<T>> func) where T : class
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                return func(session.QueryOver<T>().List());
            }
        }

        public static void Save<T>(T objtosave)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    session.Save(objtosave);
                    transaction.Commit();
                }
            }
        }

        public static void Update<T>(int index) where T : class, IEntity
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    var objtosave = RetrieveEntities<T>(el => el).First(t => t.Id == index);
                    session.Update(objtosave);
                    transaction.Commit();
                }
            }
        }
    }
}
