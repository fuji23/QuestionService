using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QuestionsService.Entities;

namespace QuestionsService.Mapping
{
    public class QuestionMap : ClassMap<Question>
    {
        public QuestionMap()
        {
            Id(x => x.Id).GeneratedBy.Native();
            Map(x => x.OptionA).Column("A");
            Map(x => x.OptionB).Column("B");
            Map(x => x.OptionC).Column("C");
            Map(x => x.Content).Column("Content");
            Table("Questions");
        }
    }
}