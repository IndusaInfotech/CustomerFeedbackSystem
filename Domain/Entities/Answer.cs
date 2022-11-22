using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Answer : AuditableEntity
    {
      public int Type { get; set; }
      public int QuestionId { get; set; }

      public string OpetionText { get; set; }




       
    }
}
