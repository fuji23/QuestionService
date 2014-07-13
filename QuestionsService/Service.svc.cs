using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Text;
using QuestionsService.Entities;
using QuestionsService.Mapping;
using NHibernate.Linq;

namespace QuestionsService
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class Service : IService
    {
        public void NewQuestion(Question question)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    session.Save(question);
                    transaction.Commit();
                }
            }
        }

        public IEnumerable<Question> GetQuestions(int amount)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                IEnumerable<Question> questions = (from user in session.Linq<Question>() select user).
                     ToList().OrderBy(q => Guid.NewGuid()).Take(amount);
                return questions;
            }
        }

        public IEnumerable<Result> GetResults(int amount)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                //{
                //    IEnumerable<Result> questions = session.QueryOver<Result>().OrderBy(el => el.Total).
                //        Desc.Take(amount).List<Result>(); 
                //    return questions;
                //}
                //return NHibernateHelper.RetrieveEntities<Result>(session.QueryOver<Result>().OrderBy(el => el.Total).
                //       Desc.Take(amount), x => x.List<Result>());
                return NHibernateHelper.RetrieveEntities(el => el.List().OrderByDescending(x => x.Total).Take(amount).ToList());
                    
            }
        }

        public Result GetResult(IEnumerable<Answer> answers, string name)
        {
            int result = 0;
            var _result = (Result)null;
            if (answers.Count() >= 1)
            {
                using (var session = NHibernateHelper.OpenSession())
                {
                    answers.ForEach(el =>
                    {
                        var query = (from ans in session.Query<Answer>() where ans.QnId == el.QnId select ans);
                        if (query.Any(x => x.Proper == el.Proper)) result++;
                    });
                    using (var transaction = session.BeginTransaction())
                    {
                        _result = new Result { Date = DateTime.Now.ToString("dd-MMM-yyyy HH:mm"), Total = result, Recipient = name };
                        session.Save(_result);
                        transaction.Commit();
                    }
                }
            }
            return _result;
        }
    }
}
