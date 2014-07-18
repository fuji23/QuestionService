using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using NHibernateHelper;

namespace QuestionsService.Entities
{
    [DataContract]
    public class Answer : IEntity
    {
        public Answer()
        {
        }

        [DataMember]
        public virtual int Id { get; protected set; }
        [DataMember]
        public virtual string Proper { get; set; }
    }
}