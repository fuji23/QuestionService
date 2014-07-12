using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace QuestionsService.Entities
{
    [DataContract]
    public class Answer : IAnswer
    {
        [DataMember]
        public virtual int QnId { get; set; }
        [DataMember]
        public virtual string Proper { get; set; }
    }
}