using FluentNHibernate;
using FluentNHibernate.Mapping;
using QuestionsService.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuestionsService.Mapping
{
    public class AnswerMap : ClassMap<Answer>
    {
        public AnswerMap()
        {
            Id(x => x.QnId).GeneratedBy.Native().Column("QnId");
            Map(x => x.Proper);
            Table("Answers");
        }
    }
}