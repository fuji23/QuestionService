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
            return NHibernateHelper.RetrieveEntities<Question>(el => el.OrderBy(q => Guid.NewGuid()).Take(amount));
        }

        public IEnumerable<Result> GetResults(int amount)
        {
            return NHibernateHelper.RetrieveEntities<Result>(el => el.OrderByDescending(x => x.Total).Take(amount));
        }

        public Result GetResult(IEnumerable<Answer> answers, string name)
        {
            int result = 0;
            var _result = (Result)null;
            if (answers.Count() >= 1)
            {
                answers.ForEach(el =>
                   {
                       var _answer = NHibernateHelper.RetrieveEntities<Answer>(x => x).SingleOrDefault(e => e.QnId == el.QnId);
                       if (_answer.Proper == el.Proper)  result++;
                   });
                _result = new Result
                    {
                        Date = DateTime.Now.ToString("dd-MMM-yyyy HH:mm"),
                        Total = result,
                        Recipient = name
                    };
                NHibernateHelper.Save(_result);
            }
            return _result;
        }
    }
}
