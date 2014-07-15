using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NHibernate;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using QuestionsService.Entities;
using NHibernate.Tool.hbm2ddl;
using System.Web.Configuration;
using Oracle.DataAccess;
using System.Configuration;
using NHibernate.Context;


namespace QuestionsService.Mapping
{
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
                          m.FluentMappings
                              .AddFromAssemblyOf<Question>())
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

    }
}