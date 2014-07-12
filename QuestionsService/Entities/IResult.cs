using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestionsService.Entities
{
   public interface IResult
    {
        int Id { get; }
        string Recipient { get; set; }
        int Total { get; set; }
        string Date { get; set; }
    }
}
