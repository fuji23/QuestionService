using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestionsService.Entities
{
    public interface IAnswer
    {
        int QnId { get; set; }
        string Proper { get; set; }
    }
}
