using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using NHibernateHelper;

namespace QuestionsService.Entities
{
    [DataContract]
    public class Result : IEntity
    {
        public Result()
        {
        }

        public Result(string _recipient, int _total, string _date)
        {
            Recipient = _recipient;
            Total = _total;
            Date = _date;
        }

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