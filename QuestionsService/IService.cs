using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using QuestionsService.Entities;

namespace QuestionsService
{
    [ServiceContract]
    public interface IService
    {
        [WebInvoke(Method = "POST",
        RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "addquestion")]
        void NewQuestion(Question question);

        [WebGet(RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "questions?id={amount}")]
        IEnumerable<Question> GetQuestions(int amount);

        [WebGet(RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "allresults?id={amount}")]
        IEnumerable<Result> GetResults(int amount);

        [WebInvoke(Method = "POST",
        RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json,
        UriTemplate = "getresult?name={name}")]
        Result GetResult(IEnumerable<Answer> answers, string name);
    }
}
