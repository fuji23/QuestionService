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
                                 connstr => connstr.FromConnectionStringWithKey("Test"))
                )
                .Mappings(m =>
                          m.FluentMappings
                              .AddFromAssemblyOf<Question>())
                .ExposeConfiguration(cfg => new SchemaUpdate(cfg)
                                                .Execute(false, true))
                .BuildConfiguration().BuildSessionFactory();
        }

        public static ISession OpenSession()
        {
            return SessionFactory.OpenSession();
        }

        public static IEnumerable<Result> RetrieveEntities(
            //IQueryOver<T> entities,
            Func<IQueryOver<Result>, IEnumerable<Result>> func)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                return func(session.QueryOver<Result>());
            }
        }

        internal static IEnumerable<Result> RetrieveEntities<T1>(Func<IQueryOver<Result>, IEnumerable<Result>> func)
        {
            throw new NotImplementedException();
        }
    }
}