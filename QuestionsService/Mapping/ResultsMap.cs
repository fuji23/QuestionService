using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using QuestionsService.Entities;

namespace QuestionsService.Mapping
{
    public class ResultMap : ClassMap<Result>
    {
        public ResultMap()
        {
            Id(i => i.Id).GeneratedBy.Native().Column("ResultId");
            Map(x => x.Recipient).Column("Recipient");
            Map(x => x.Total).Column("Total");
            Map(x => x.Date).Column("Date");
            Table("Results");
        }
    }
}