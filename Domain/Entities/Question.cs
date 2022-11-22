using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Question : AuditableEntity
    {
       
        public int SurveyId { get; set; }
        public string Description { get; set; }
        public int NumberOfPage { get; set; }

        public int AnswerType { get; set; }
    }
}
