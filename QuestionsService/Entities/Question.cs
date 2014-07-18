using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using NHibernateHelper;

namespace QuestionsService.Entities
{
    [DataContract]
    public class Question : IEntity
    {
        public Question()
        {
        }

        public Question(string _content, string a, string b, string c)
        {
            Content = _content; OptionA = a; OptionB = b; OptionC = c;
        }

        [DataMember]
        public virtual int Id { get; protected set; }
        [DataMember]
        public virtual string OptionA { get; set; }
        [DataMember]
        public virtual string OptionB { get; set; }
        [DataMember]
        public virtual string OptionC { get; set; }
        [DataMember]
        public virtual string Content { get; set; }
    }
}