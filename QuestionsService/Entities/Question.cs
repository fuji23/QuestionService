using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace QuestionsService.Entities
{
    [DataContract]
    public class Question
    {
        [DataMember]
        public virtual int QuestionId { get; protected set; }
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