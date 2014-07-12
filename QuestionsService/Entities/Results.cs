using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace QuestionsService.Entities
{
    [DataContract]
    public class Result : IResult
    {
        [DataMember]
        public virtual int Id { get; protected set; }
        [DataMember]
        public virtual string Recipient { get; set; }
        [DataMember]
        public virtual int Total { get; set; }
        [DataMember]
        public virtual string Date { get; set; }
    }
}