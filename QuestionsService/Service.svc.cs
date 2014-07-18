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
using System.Runtime.CompilerServices;
using NHibernateHelp = NHibernateHelper;

namespace QuestionsService
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class Service : IService
    {
        public void NewQuestion(Question question)
        {
            NHibernateHelp.NHibernateHelper.Save(question);
        }

        public IEnumerable<Question> GetQuestions(int amount)
        {
            return NHibernateHelp.NHibernateHelper.RetrieveEntities<Question>(el => el.OrderBy(q => Guid.NewGuid()).Take(amount));
        }

        public IEnumerable<Result> GetResults(int amount)
        {
            return NHibernateHelp.NHibernateHelper.RetrieveEntities<Result>(el => el.OrderByDescending(x => x.Total).Take(amount));
        }

        public Result GetResult(IEnumerable<Answer> answers, string name)
        {
            int result = 0;
            var _result = (Result)null;
            if (answers.Count() >= 1)
            {
                answers.ForEach(el =>
                   {
                       var _answer = NHibernateHelp.NHibernateHelper.RetrieveEntities<Answer>(x => x).SingleOrDefault(e => e.Id == el.Id);
                       if (_answer.Proper == el.Proper) result++;
                   });
                _result = new Result(name, result, DateTime.Now.ToString("dd-MMM-yyyy HH:mm"));
                NHibernateHelp.NHibernateHelper.Save(_result);
            }
            return _result;
        }
    }
}
